using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagement.Application.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;
            string unqiueId = Guid.NewGuid().ToString();

            _logger.LogInformation($"Begin Request Id:{unqiueId}, request name:{requestName}");

            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();

            _logger.LogInformation($"Begin Request Id:{unqiueId}, request name:{requestName}, total request time:{timer.ElapsedMilliseconds}");

            return response;
        }
    }
}
