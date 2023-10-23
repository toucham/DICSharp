using DICSharpDev.Lib;

namespace DICSharpDev.Services.Multiple
{
    [DILifetime(ServiceLifetime.Transient)]
    public class AMultiService : IMultiService
    {
        private ILogger<AMultiService> _logger;
        private readonly long RandomInt;

        public AMultiService(ILogger<AMultiService> logger)
        {
            _logger = logger;
            RandomInt = new Random().NextInt64();
        }
        public void LogMultiServiceID()
        {
            _logger.LogInformation(RandomInt.ToString());
        }
    }
}