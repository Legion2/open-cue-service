using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OpenCueService.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfileManager ProfileManager;

        public ProfilesController(ProfileManager profileManager)
        {
            ProfileManager = profileManager;
        }

        //Get all Profils
        [HttpGet]
        public IEnumerable<Profile> GetAllProfiles()
        {
            return ProfileManager.GetAllProfiles().Values;
        }

        //Get a profile
        [HttpGet]
        [Route("{name}")]
        public Profile GetProfile(string name)
        {
            var profile = ProfileManager.GetProfile(name);
            if (profile == null)
            {
                throw new ApiError(404, "Profile not found");
            }
            return profile;
        }

        //Trigger a profile as event
        [HttpPost]
        [Route("{name}/trigger")]
        public Profile TriggerProfile(string name)
        {
            var profile = GetProfile(name);
            return ProfileManager.TriggerProfile(profile);
        }

        [HttpGet]
        [Route("{name}/state")]
        public bool GetState(string name)
        {
            return GetProfile(name).State;
        }
        //Set the state of a profile
        [HttpPost]
        [Route("{name}/state/{value:bool}")]
        public Profile SetState(string name, bool value)
        {
            var profile = GetProfile(name);
            return ProfileManager.ActivateProfile(profile, value);
        }
    }
}
