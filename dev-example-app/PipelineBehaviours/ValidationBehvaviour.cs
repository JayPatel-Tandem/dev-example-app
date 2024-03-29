﻿using FluentValidation;
using MediatR;

namespace dev_example_app.PipelineBehaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(opt => opt.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(results => results.Errors).Where(error => error != null).ToList();

                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }
            }
            return await next();
        }

    }
}
