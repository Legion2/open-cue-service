using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Options;

namespace OpenCueService
{
    using CgSdk;
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

        private bool Connected;

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

        public bool IsConnected()
        {
            return Connected;
        }

        public IDictionary<string, Profile> GetAllProfiles()
        {
            return Profiles;
        }

        // Get profile by name
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
            profile.State = false;
            lastTriggeredProfile = profile;
            Sdk.SetEvent(profile.Name);
            return profile;
        }

        public Profile ActivateProfile(Profile profile, bool activate)
        {
            profile.State = activate;
            if (activate)
            {
                Sdk.SetState(profile.Name);
            }
            else
            {
                Sdk.ClearState(profile.Name);
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
            }
            else
            {
                Sdk.ReleaseControl();
            }
            return value;
        }

        public void DeactivateAllProfiles()
        {
            foreach (var profile in Profiles.Values)
            {
                profile.State = false;
            }
            Sdk.ClearAllStates();
        }

        public void StopAllEvents()
        {
            Sdk.ClearAllEvents();
        }

        public void TriggerAutoSync()
        {
            if (!Sdk.HasControl())
            {
                return;
            }

            if (Sdk.GetCorsairProtocolDetails().ServerProtocolVersion == 0)
            {
                Connected = false;
            }

            if (Connected && Config.AutoSyncProfileName != "")
            {
                try
                {
                    Sdk.SetState(Config.AutoSyncProfileName);
                }
                catch (SdkError e)
                {
                    if (e.CorsairError == CorsairError.CE_ServerNotFound)
                    {
                        Connected = false;
                    }
                    else
                    {
                        throw e;
                    }
                }
            }

            if (!Connected)
            {
                Sdk.Reconnect();

                if (Sdk.GetCorsairProtocolDetails().ServerProtocolVersion > 0)
                {
                    Connected = true;
                    Sync();
                }
            }
        }

        // Sync the state with the SDK
        public void Sync()
        {
            foreach (var profile in Profiles.Values)
            {
                if (profile.State)
                {
                    Sdk.SetState(profile.Name);
                }
                else
                {
                    Sdk.ClearState(profile.Name);
                }
            }
        }
    }
}