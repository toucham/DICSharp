
using DICSharpDev.Lib;

namespace DICSharpDev.Services.Multiple
{
    [DILifetime(ServiceLifetime.Transient)]
    public class BMultiService : IMultiService
    {
        private ILogger<BMultiService> _logger;
        private readonly long RandomInt;

        public BMultiService(ILogger<BMultiService> logger)
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