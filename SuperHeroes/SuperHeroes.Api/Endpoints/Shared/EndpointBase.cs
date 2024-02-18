using System.Net;
using FastEndpoints;
using MediatR;
using SuperHeroes.Application.Shared;
using SuperHeroes.Core.Exceptions;

namespace SuperHeroes.Api.Endpoints.Shared;

/// <summary>
/// Base class for all endpoints in the application.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public abstract class EndpointBase<TRequest, TResponse> : Endpoint<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : ResponseBase
{
    private const string DefaultNotFoundMessage = "Entity not found";
    private const string FailedToHandleRequest = "Endpoint {endpoint} failed to handle the request: {request}";
    private const string InternalServerErrorDetails = "An unexpected server error occurred.";
    
    private readonly ILogger _logger;
    private readonly Http _httpVerb;
    private readonly string _route;
    private readonly string _summary;
    private readonly HttpStatusCode _successStatusCode;
    private readonly string _handlerName;
    
    private readonly ISender _mediator;

    protected EndpointBase(ISender mediator, ILoggerFactory loggerFactory, Http httpVerb, string route, string summary, HttpStatusCode successStatusCode)
    {
        _httpVerb = httpVerb;
        _route = route;
        _summary = summary;
        _successStatusCode = successStatusCode;
        _mediator = mediator;
        _logger = loggerFactory.CreateLogger(GetType());
        _handlerName = GetType().Name;
    }
    
    /// <summary>
    /// Configures the endpoint.
    /// </summary>
    public sealed override void Configure()
    {
        Verbs(_httpVerb);
        Routes(_route);
        DefinePermissions();
        Summary(s =>
        {
            s.Summary = _summary;
            s.Description = _summary;
        });
    }
    
    protected virtual void DefinePermissions()
    {
        AllowAnonymous();
    }
    
    
    /// <summary>
    /// Handles the request and sends the response (template method pattern)
    /// </summary>
    /// <param name="req"></param>
    /// <param name="ct"></param>
    public sealed async override Task HandleAsync(TRequest req, CancellationToken ct)
    {
        try
        {
            TResponse response = await _mediator.Send(req, ct);

            if (!response.RequestValid)
            {
                await SendErrorResponse(response.Title, response.Details, HttpStatusCode.BadRequest, ct);
            }
            else if (StatusCodeWritable(_successStatusCode))
            {
                await SendAsync(response, (int)_successStatusCode, cancellation: ct);
            }
        }
        catch (FluentValidation.ValidationException e)
        {
            await HandleValidationException(e, ct);
        }
        catch (System.ComponentModel.DataAnnotations.ValidationException e)
        {
            await HandleValidationException(e, ct);
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogWarning(e, e.Message);
            await SendErrorResponse(nameof(HttpStatusCode.Forbidden), e.Message, HttpStatusCode.Forbidden, ct);
        }
        catch (IntegrationReadException e)
        {
            _logger.LogError(e, e.Message);
            await SendErrorResponse(nameof(HttpStatusCode.ServiceUnavailable), e.Message, HttpStatusCode.ServiceUnavailable, ct);
        }
        catch (Exception e)
            when (TryRetrieveEntityNotFoundException(e) is not null)
        {
            _logger.LogWarning(e, e.Message);
            await SendErrorResponse(nameof(HttpStatusCode.NotFound), RetrieveEntityNotFoundExceptionMessage(e),
                HttpStatusCode.NotFound, ct);
        }
        catch (Exception e)
        {
            _logger.LogError(e, FailedToHandleRequest, _handlerName, req);
            await SendErrorResponse(nameof(HttpStatusCode.InternalServerError), InternalServerErrorDetails, HttpStatusCode.InternalServerError, ct);
        }
    }

    private async Task HandleValidationException(Exception e, CancellationToken ct)
    {
        _logger.LogWarning(e, e.Message);
        await SendErrorResponse(nameof(HttpStatusCode.BadRequest), e.Message, HttpStatusCode.BadRequest, ct);
    }
    
    private Task SendErrorResponse(string title, string details, HttpStatusCode errorCode, CancellationToken ct)
    {
        HttpContext.MarkResponseStart();
        HttpContext.Response.StatusCode = (int)errorCode;
        HttpContext.Response.ContentType = "application/json";
        HttpContext.Response.WriteAsJsonAsync(new ResponseBase(false, title, details, false), ct);
        return HttpContext.Response.StartAsync(ct);
    }

    private static bool StatusCodeWritable(HttpStatusCode code)
    {
        return code != HttpStatusCode.NoContent;
    }
    
    private string RetrieveEntityNotFoundExceptionMessage(Exception e)
    {
        EntityNotFoundException? entityNotFoundException = TryRetrieveEntityNotFoundException(e);
        if (entityNotFoundException is null)
        {
            _logger.LogError("Could not parse exception as EntityNotFoundException: {exception}", e);
            return DefaultNotFoundMessage;
        }

        return entityNotFoundException.Message;
    }
    
    private static EntityNotFoundException? TryRetrieveEntityNotFoundException(Exception e)
    {
        if (e is EntityNotFoundException notFoundException)
        {
            return notFoundException;
        }

        if (e is AggregateException aggregateException)
        {
            AggregateException flattenedAggrExc = aggregateException.Flatten();

            return (EntityNotFoundException?)flattenedAggrExc.InnerExceptions
                .FirstOrDefault(ie => ie is EntityNotFoundException);
        }

        return null;
    }
}
