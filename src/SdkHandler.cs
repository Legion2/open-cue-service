using System;
using Microsoft.Extensions.Options;

namespace OpenCueService
{
    using CgSdk;
    public class SdkHandler
    {
        private CorsairGameSdk CorsairGameSdk;

        private readonly string Game;
        public SdkHandler(IOptions<Config> config)
        {
            Game = config.Value.Game;
            CorsairGameSdk = new CorsairGameSdk(Game);
        }
        public void Reconnect() {
            CorsairGameSdk = new CorsairGameSdk(Game);
        }
        public CorsairProtocolDetails GetCorsairProtocolDetails()
        {
            return CorsairGameSdk.GetCorsairProtocolDetails();
        }

        public string GetGame()
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
