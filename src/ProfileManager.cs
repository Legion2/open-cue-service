using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Options;

namespace open_cue_service
{
    public class ProfileManager
    {
        private static readonly string GameSkdEffectsPath = @"c:\Program Files (x86)\Corsair\CORSAIR iCUE Software\GameSdkEffects";

        public ProfileManager(IOptions<Config> config, SdkHandler sdkHandler)
        {
            _config = config.Value;
            _sdk = sdkHandler;
            Profiles = loadPriorities().ToDictionary(entry => entry.Key, entry => new Profile
            {
                Name = entry.Key,
                Priority = entry.Value,
                Active = false
            });
        }

        private readonly Config _config;
        private readonly SdkHandler _sdk;
        private readonly Dictionary<string, Profile> Profiles;

        private Profile? lastTriggeredProfile = null;

        private Dictionary<string, int> loadPriorities()
        {
            var ProfilePrioritiesFile = Path.Combine(GameSkdEffectsPath, _config.Game, "priorities.cfg");
            var Priorities = new Dictionary<string, int>();
            foreach (var row in File.ReadAllLines(ProfilePrioritiesFile))
            {
                var KeyAndValue = row.Split('=');
                if (KeyAndValue.Length != 2)
                {
                    throw new Exception("Bad priorities.cfg");
                }
                if (Int32.TryParse(KeyAndValue[1], out int priority))
                {
                    Priorities.Add(KeyAndValue[0], priority);
                }
                else
                {
                    throw new Exception("Bad priorities.cfg");
                }
            }
            return Priorities;
        }

        public Dictionary<string, Profile> GetAllProfiles()
        {
            return Profiles;
        }

        public Profile GetProfile(string name)
        {
            return Profiles.Values.First(profile => profile.Name.Equals(name));
        }

        public Profile TriggerProfile(Profile profile) {
            _sdk.SetEvent(profile.Name);
            lastTriggeredProfile = profile;
            return profile;
        }

        public Profile ActivateProfile(Profile profile, bool activate)
        {
            if (activate)
            {
                _sdk.SetState(profile.Name);
                profile.Active = true;
            }
            else
            {
                _sdk.ClearState(profile.Name);
                profile.Active = false;
            }
            return profile;
        }

    }
}