using DICSharp.Services.Scoped;
using DICSharp.Services.Singleton;
using DICSharp.Services.Transient;

namespace DICSharp.Services
{
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
            _logger.LogDebug("Singleton ID: " + _singletonService.Id.ToString());
            _logger.LogDebug("Transient ID: " + _transientService.Id.ToString());
            _logger.LogDebug("Scoped ID: " + _scopedService.Id.ToString());
        }
    }
}