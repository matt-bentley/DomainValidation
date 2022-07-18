using CSharpFunctionalExtensions;
using DomainValidation.Application.ReadModels;
using DomainValidation.Core;
using FluentValidation;

namespace DomainValidation.Application.Behaviors
{
    public class CreatedValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TResponse : IResult<CreatedReadModel, Error>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public CreatedValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failures.Count != 0)
                {
                    // boxing required here to compile
                    object result = Result.Failure<CreatedReadModel, Error>(Error.Deserialize(failures.First().ErrorMessage));
                    return (TResponse)result;
                }
            }
            return await next();
        }
    }

    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IUnitResult<Error>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failures.Count != 0)
                {
                    // boxing required here to compile
                    object result = UnitResult.Failure(Error.Deserialize(failures.First().ErrorMessage));
                    return (TResponse)result;
                }
            }
            return await next();
        }
    }
}
