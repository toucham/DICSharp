using DICSharpDev.Lib;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace OtherServices;

[DILifetime(ServiceLifetime.Singleton)]
public class NewLogServices : INewLogServices
{
    private readonly ILogger<NewLogServices> _logger;
    public NewLogServices(ILogger<NewLogServices> logger)
    {
        _logger = logger;
    }
    public void LogMessage(string msg)
    {
        _logger.LogInformation($"from new logger: {msg}");
    }
}
