using DICSharpDev.Lib;

namespace DICSharpDev.Services.Singleton
{
    [DILifetime(ServiceLifetime.Singleton)]
    public class StringValidatorService : IValidatorService<string>
    {
        private readonly ILogger<StringValidatorService> _logger;
        public StringValidatorService(ILogger<StringValidatorService> logger)
        {
            _logger = logger;
        }

        public void ValidateType(string input) => _logger.LogInformation(input);
    }
}