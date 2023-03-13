using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponce> : IPipelineBehavior<TRequest, TResponce>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponce> Handle(TRequest request, RequestHandlerDelegate<TResponce> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResult = await Task.WhenAll(_validators.Select(q => q.ValidateAsync(context, cancellationToken)));

                var failures = validationResult.SelectMany(q => q.Errors).Where(q => q is not null).ToList();

                if (failures.Count > 0)
                {
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
