using DICSharpDev.Lib;
using DICSharpDev.Services.Scoped;
using DICSharpDev.Services.Singleton;
using DICSharpDev.Services.Transient;

namespace DICSharpDev.Services
{
    [DILifetime(ServiceLifetime.Scoped)]
    public class LoggerService : ILoggerService
    {
        private readonly ISingletonService _singletonService;
        private readonly ITransientService _transientService;
        private readonly IScopedService _scopedService;
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ISingletonService singleton, ITransientService transient, IScopedService scoped, ILogger<LoggerService> logger)
        {
            _scopedService = scoped;
            _transientService = transient;
            _singletonService = singleton;
            _logger = logger;
        }

        public void LogUsage()
        {
            _logger.LogInformation($"""
                Singleton ID: {_singletonService.Id} 
                Transient ID: {_transientService.Id} 
                Scoped ID: {_scopedService.Id}
                """);
        }
    }
}