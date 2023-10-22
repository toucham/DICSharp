using DICSharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DICSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILoggerService _logger;
        public LogController(ILoggerService logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Log")]
        public void Log()
        {
            _logger.LogUsage();
        }
    }
}