using Microsoft.Extensions.Options;
using OpenCue.Sdk;

namespace OpenCue.Service
{
    public class SdkHandler
    {
        private CorsairGameSdk CorsairGameSdk;

        private readonly string ProfilesDirectoryName;
        public SdkHandler(IOptions<Config> config)
        {
            ProfilesDirectoryName = config.Value.ProfilesDirectoryName;
            CorsairGameSdk = new CorsairGameSdk(ProfilesDirectoryName);
        }
        public void Reconnect()
        {
            CorsairGameSdk = new CorsairGameSdk(ProfilesDirectoryName);
        }
        public CorsairProtocolDetails GetCorsairProtocolDetails()
        {
            return CorsairGameSdk.GetCorsairProtocolDetails();
        }

        // Map ProfilesDirectoryName to Game
        public string GetProfilesDirectoryName()
        {
            return CorsairGameSdk.GetGame();
        }

        public bool HasControl()
        {
            return CorsairGameSdk.HasControl();
        }
        public void RequestControl()
        {
            CorsairGameSdk.RequestControl();
        }
        public void ReleaseControl()
        {
            CorsairGameSdk.ReleaseControl();
        }

        public void SetState(string stateName)
        {
            CorsairGameSdk.SetState(stateName);
        }

        public void SetEvent(string eventName)
        {
            CorsairGameSdk.SetEvent(eventName);
        }

        public void ClearState(string stateName)
        {
            CorsairGameSdk.ClearState(stateName);
        }

        public void ClearAllStates()
        {
            CorsairGameSdk.ClearAllStates();
        }

        public void ClearAllEvents()
        {
            CorsairGameSdk.ClearAllEvents();
        }
    }
}
