using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Options;

namespace OpenCueService
{
    public class ProfileManager
    {
        private static readonly string GameSkdEffectsPath = @"c:\Program Files (x86)\Corsair\CORSAIR iCUE Software\GameSdkEffects";

        public ProfileManager(IOptions<Config> config, SdkHandler sdkHandler)
        {
            Config = config.Value;
            Sdk = sdkHandler;
            Profiles = loadPriorities().ToDictionary(entry => entry.Key, entry => new Profile
            {
                Name = entry.Key,
                Priority = entry.Value,
                State = false
            });
        }

        private readonly Config Config;
        private readonly SdkHandler Sdk;
        private readonly IDictionary<string, Profile> Profiles;

        private Profile? lastTriggeredProfile = null;

        private IDictionary<string, int> loadPriorities()
        {
            var ProfilePrioritiesFile = Path.Combine(GameSkdEffectsPath, Config.Game, "priorities.cfg");
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

        public IDictionary<string, Profile> GetAllProfiles()
        {
            return Profiles;
        }

        public Profile? GetProfile(string name)
        {
            var Profile = Profiles.Values.FirstOrDefault(profile => profile.Name.Equals(name));
            if (Profile == default(Profile))
            {
                return null;
            }
            else
            {
                return Profile;
            }
        }

        public Profile TriggerProfile(Profile profile)
        {
            Sdk.SetEvent(profile.Name);
            lastTriggeredProfile = profile;
            profile.State = false;
            return profile;
        }

        public Profile ActivateProfile(Profile profile, bool activate)
        {
            if (activate)
            {
                Sdk.SetState(profile.Name);
                profile.State = true;
            }
            else
            {
                Sdk.ClearState(profile.Name);
                profile.State = false;
            }
            return profile;
        }

        public bool Control(bool value)
        {
            if (Sdk.HasControl() == value)
            {
                return value;
            }

            if (value)
            {
                Sdk.RequestControl();
                foreach (var profile in Profiles.Values)
                {
                    if (profile.State)
                    {
                        Sdk.SetState(profile.Name);
                    }
                }
            }
            else
            {
                Sdk.ReleaseControl();
            }
            return value;
        }

        public void DeactivateAllProfiles()
        {
            Sdk.ClearAllStates();
        }

        public void StopAllEvents()
        {
            Sdk.ClearAllEvents();
        }
    }
}