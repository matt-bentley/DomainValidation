using CSharpFunctionalExtensions;
using DomainValidation.Core;
using DomainValidation.Core.Entities.Abstract;
using FluentValidation;

namespace DomainValidation.Application.Commands.Validators
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, TElement> MustBeEntity<T, TElement, TEntity>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TEntity, Error>> factoryMethod)
            where TEntity : EntityBase
        {
            return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
            {
                Result<TEntity, Error> result = factoryMethod(value);

                if (result.IsFailure)
                {
                    context.AddFailure(result.Error.Serialize());
                }
            });
        }

        public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TValueObject, Error>> factoryMethod)
            where TValueObject : ValueObject
        {
            return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject, Error> result = factoryMethod(value);

                if (result.IsFailure)
                {
                    context.AddFailure(result.Error.Serialize());
                }
            });
        }
    }
}
