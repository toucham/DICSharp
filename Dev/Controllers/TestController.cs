using DICSharpDev.Services;
using DICSharpDev.Services.Singleton;
using Microsoft.AspNetCore.Mvc;
using OtherServices;

namespace DICSharpDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly INewLogServices _newLogger;
        private readonly IValidatorService<string> _stringValidator;
        private readonly IValidatorService<long> _longValidator;
        public TestController(ILoggerService logger, IValidatorService<string> stringVal, IValidatorService<long> longVal, INewLogServices newLog)
        {
            _logger = logger;
            _stringValidator = stringVal;
            _longValidator = longVal;
            _newLogger = newLog;
        }

        [HttpGet("~/Log")]
        public void Log()
        {
            _logger.LogUsage();
        }

        [HttpPost("~/NewLog")]
        public string NewLog([FromBody] string body)
        {
            _newLogger.LogMessage(body);
            return "OK";
        }
        [HttpGet("~/Validator/String")]
        public void ValidatorString()
        {
            _stringValidator.ValidateType("string");
        }

        [HttpGet("~/Validator/Long")]
        public void ValidatorLong()
        {
            _longValidator.ValidateType(123);
        }
    }
}