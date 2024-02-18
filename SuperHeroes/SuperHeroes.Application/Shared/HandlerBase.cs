using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace SuperHeroes.Application.Shared;

/// <summary>
/// Represents the base class for all handlers (commands and queries)
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public abstract class HandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : ResponseBase
{
    private const string ValidationFailed = "Invalid request";
    private readonly IValidator<TRequest> _validator;
    
    protected HandlerBase(IValidator<TRequest> validator)
    {
        _validator = validator;
    }
    
    /// <summary>
    /// Validates the request before processing it (utilizing the template method pattern)
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, CancellationToken ct)
    {
        ValidationResult? validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
            return ProduceValidationErrorResponse(validationResult);
        
        return await HandleInternal(request, ct);
    }
    
    private TResponse ProduceValidationErrorResponse(ValidationResult validationResult)
    {
        // we construct a response with the validation errors:
        var details = string.Join(". ", validationResult.Errors.Select(x => x.ErrorMessage));
        return ConstructSpecificValidationErrorResponse(ValidationFailed, details, validationResult.IsValid);
    }
    
    /// <summary>
    /// Constructs a specific validation error response (in derived classes)
    /// </summary>
    /// <param name="errorTitle"></param>
    /// <param name="details"></param>
    /// <param name="isValid"></param>
    /// <returns></returns>
    protected abstract TResponse ConstructSpecificValidationErrorResponse(string errorTitle, string details, bool isValid);
    protected abstract Task<TResponse> HandleInternal(TRequest request, CancellationToken ct);
}
