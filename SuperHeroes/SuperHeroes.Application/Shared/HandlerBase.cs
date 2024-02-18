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
    
    public async Task<TResponse> Handle(TRequest request, CancellationToken ct)
    {
        ValidationResult? validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
            return ProduceValidationErrorResponse(validationResult);
        
        return await HandleInternal(request, ct);
    }
    
    private TResponse ProduceValidationErrorResponse(ValidationResult validationResult)
    {
        var details = string.Join(". ", validationResult.Errors.Select(x => x.ErrorMessage));
        return ConstructSpecificValidationErrorResponse(ValidationFailed, details, validationResult.IsValid);
    }
    protected abstract TResponse ConstructSpecificValidationErrorResponse(string errorTitle, string details, bool isValid);
    protected abstract Task<TResponse> HandleInternal(TRequest request, CancellationToken ct);
}
