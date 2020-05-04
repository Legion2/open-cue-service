using Microsoft.AspNetCore.Mvc;

namespace OpenCueService.Controllers
{
    using CgSdk;
    [ApiController]
    [Route("api/sdk")]
    public class SdkController : ControllerBase
    {
        private readonly SdkHandler Sdk;
        private readonly ProfileManager ProfileManager;

        public SdkController(SdkHandler sdk, ProfileManager profileManager)
        {
            Sdk = sdk;
            ProfileManager = profileManager;
        }

        [HttpGet]
        [Route("connection")]
        public bool GetConnection()
        {
            return ProfileManager.IsConnected();
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

        [HttpPut]
        [Route("control/{value:bool}")]
        public bool Control(bool value)
        {
            return ProfileManager.Control(value);
        }

        [HttpPost]
        [Route("stop-all-events")]
        public void StopAllEvents()
        {
            ProfileManager.StopAllEvents();
        }
        [HttpPost]
        [Route("deactivate-all-profiles")]
        public void DeactivateAllProfiles()
        {
            ProfileManager.DeactivateAllProfiles();
        }
    }
}
