using DICSharpDev.Lib;

namespace DICSharpDev.Services.Singleton
{
    [DILifetime(ServiceLifetime.Singleton)]
    public class LongValidatorService : IValidatorService<long>
    {
        private readonly ILogger<LongValidatorService> _logger;
        public LongValidatorService(ILogger<LongValidatorService> logger)
        {
            _logger = logger;
        }
        public void ValidateType(long input) => _logger.LogInformation(input.ToString());
    }
}