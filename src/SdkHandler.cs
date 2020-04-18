using System;
using Microsoft.Extensions.Options;

namespace open_cue_service
{
    public class SdkHandler
    {
        public SdkHandler(IOptions<Config> config)
        {
            PerformProtocolHandshake();
            RequestControl();
            SetGame(config.Value.Game);
        }

        // Implement all CgSDK proxy functions
        public SdkError GetLastError()
        {
            return new SdkError(CgSdkInterop.GetLastError());
        }
        public void PerformProtocolHandshake()
        {
            CgSdkInterop.PerformProtocolHandshake();
        }
        public void RequestControl()
        {
            WithErrorHandling(CgSdkInterop.RequestControl());
        }
        public void ReleaseControl()
        {
            WithErrorHandling(CgSdkInterop.ReleaseControl());
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
