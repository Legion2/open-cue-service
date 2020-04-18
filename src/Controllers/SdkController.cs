using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace open_cue_service.Controllers
{
    using CgSdk;
    [ApiController]
    [Route("api/sdk")]
    public class SdkController : ControllerBase
    {
        private readonly ILogger<ProfilesController> _logger;
        private readonly SdkHandler Sdk;
        private readonly ProfileManager ProfileManager;

        public SdkController(ILogger<ProfilesController> logger, SdkHandler sdk, ProfileManager profileManager)
        {
            _logger = logger;
            Sdk = sdk;
            ProfileManager = profileManager;
        }

        [HttpGet]
        [Route("game")]
        public string GetGame()
        {
            return Sdk.GetGame();
        }

        [HttpGet]
        [Route("details")]
        public CorsairProtocolDetails GetDetails()
        {
            return Sdk.GetCorsairProtocolDetails();
        }

        [HttpGet]
        [Route("control")]
        public bool GetControl()
        {
            return Sdk.HasControl();
        }

        [HttpPost]
        [Route("control/{value:bool}")]
        public bool Control(bool value)
        {
            return ProfileManager.Control(value);
        }

        [HttpPost]
        [Route("stop-events")]
        public void StopAllEvents()
        {
            ProfileManager.StopAllEvents();
        }
        [HttpPost]
        [Route("deactivate-profiles")]
        public void DeactivateAllProfiles()
        {
            ProfileManager.DeactivateAllProfiles();
        }
    }
}
