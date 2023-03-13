using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponce> : IPipelineBehavior<TRequest, TResponce>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponce> Handle(TRequest request, RequestHandlerDelegate<TResponce> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogCritical(ex, $"Application Request: Unhandled Exception for Request {requestName} {request}");
                throw;
            }
        }
    }
}
