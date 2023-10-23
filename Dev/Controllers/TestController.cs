using DICSharpDev.Services;
using DICSharpDev.Services.Multiple;
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
        private readonly IEnumerable<IMultiService> _multiService;
        private readonly IMultiService _oneMultiService;
        public TestController(ILoggerService logger, IValidatorService<string> stringVal, IValidatorService<long> longVal, INewLogServices newLog, IEnumerable<IMultiService> multiService, IMultiService oneMultiService)
        {
            _logger = logger;
            _stringValidator = stringVal;
            _longValidator = longVal;
            _newLogger = newLog;
            _multiService = multiService;
            _oneMultiService = oneMultiService;
        }

        [HttpGet("~/Log")]
        public void Log()
        {
            _logger.LogUsage();
        }

        [HttpGet("~/LogMultiService")]
        public void LogMultiService()
        {
            foreach (var s in _multiService)
            {
                s.LogMultiServiceID();
            }
            _oneMultiService.LogMultiServiceID();
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