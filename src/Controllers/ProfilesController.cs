using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace open_cue_service.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : ControllerBase
    {
        private readonly ILogger<ProfilesController> _logger;
        private readonly ProfileManager _profileManager;

        public ProfilesController(ILogger<ProfilesController> logger, ProfileManager ProfileManager)
        {
            _logger = logger;
            _profileManager = ProfileManager;
        }

        //Get all Profils
        [HttpGet]
        public IEnumerable<Profile> GetAllProfiles()
        {
            return _profileManager.GetAllProfiles().Values;
        }

        //Get a profile
        [HttpGet]
        [Route("{name}")]
        public Profile GetProfile(string name)
        {
            return _profileManager.GetProfile(name);
        }

        //Trigger a profile as event
        [HttpPost]
        [Route("{name}/trigger")]
        public Profile TriggerProfile(string name)
        {
            var profile = GetProfile(name);
            return _profileManager.TriggerProfile(profile);
        }

        //Activate a profile
        [HttpPost]
        [Route("{name}/activate/{value:bool}")]
        public Profile ActivateProfile(string name, bool value)
        {
            var profile = GetProfile(name);
            return _profileManager.ActivateProfile(profile, value);
        }

    }
}
