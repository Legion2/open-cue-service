using System;
using Microsoft.Extensions.Options;

namespace OpenCueService
{
    using CgSdk;
    public class SdkHandler
    {
        private readonly CorsairProtocolDetails corsairProtocolDetails;

        private bool hasControl = false;

        private readonly string Game;
        public SdkHandler(IOptions<Config> config)
        {
            Game = config.Value.Game;
            corsairProtocolDetails = PerformProtocolHandshake();
            Console.WriteLine(corsairProtocolDetails.SdkVersion);
            RequestControl();
            SetGame(Game);
        }
        public CorsairProtocolDetails GetCorsairProtocolDetails()
        {
            return corsairProtocolDetails;
        }

        public string GetGame()
        {
            return Game;
        }

        public bool HasControl()
        {
            return hasControl;
        }

        // Implement all CgSDK proxy functions
        public SdkError GetLastError()
        {
            return new SdkError(CgSdkInterop.GetLastError());
        }
        private CorsairProtocolDetails PerformProtocolHandshake()
        {
            return new CorsairProtocolDetails(CgSdkInterop.PerformProtocolHandshake());
        }
        public void RequestControl()
        {
            WithErrorHandling(CgSdkInterop.RequestControl());
            hasControl = true;
        }
        public void ReleaseControl()
        {
            WithErrorHandling(CgSdkInterop.ReleaseControl());
            hasControl = false;
        }

        public void SetGame(string gameName)
        {
            WithErrorHandling(CgSdkInterop.SetGame(gameName));
        }

        public void SetState(string stateName)
        {
            WithErrorHandling(CgSdkInterop.SetState(stateName));
        }

        public void SetEvent(string eventName)
        {
            WithErrorHandling(CgSdkInterop.SetEvent(eventName));
        }

        public void ClearState(string stateName)
        {
            WithErrorHandling(CgSdkInterop.ClearState(stateName));
        }

        public void ClearAllStates()
        {
            WithErrorHandling(CgSdkInterop.ClearAllStates());
        }

        public void ClearAllEvents()
        {
            WithErrorHandling(CgSdkInterop.ClearAllEvents());
        }

        private void WithErrorHandling(bool returnValue)
        {
            if (!returnValue)
            {
                throw GetLastError();
            }
        }
    }

    public class SdkError : Exception
    {
        public SdkError(int errorCode)
           : base($"Sdk Error occurred: {errorCode}")
        {
            ErrorCode = errorCode;
        }

        public readonly int ErrorCode;
    }
}
