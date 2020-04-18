﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace open_cue_service.Controllers
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
            return ProfileManager.GetProfile(name);
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
