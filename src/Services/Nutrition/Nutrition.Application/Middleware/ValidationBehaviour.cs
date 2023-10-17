using FluentValidation;
using MediatR;

namespace Nutrition.Application.Middleware
{
    public class ValidationBehaviour<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            foreach (var validator in _validators)
            {
                var result = validator.Validate(request);

                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
            }

            return await next();
        }
    }
}
