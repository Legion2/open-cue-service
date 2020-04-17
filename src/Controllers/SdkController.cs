using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace open_cue_service.Controllers
{
    [ApiController]
    [Route("api/sdk")]
    public class SdkController : ControllerBase
    {
        private readonly ILogger<ProfilesController> _logger;
        private readonly SdkHandler _sdk;

        public SdkController(ILogger<ProfilesController> logger, SdkHandler sdk)
        {
            _logger = logger;
            _sdk = sdk;
        }


        [HttpPost]
        [Route("control/{value:bool}")]
        public bool Control(bool value)
        {
            if (value)
            {
                _sdk.RequestControl();
            }
            else
            {
                _sdk.ReleaseControl();
            }
            return value;
        }
    }
}
