using System;
using Microsoft.Extensions.Options;
using Cg = CgSDK_Interop.CgSDK;

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
            return new SdkError(Cg.GetLastError());
        }
        public void PerformProtocolHandshake()
        {
            Cg.PerformProtocolHandshake();
        }
        public void RequestControl()
        {
            WithErrorHandling(Cg.RequestControl());
        }
        public void ReleaseControl()
        {
            WithErrorHandling(Cg.ReleaseControl());
        }

        public void SetGame(string gameName)
        {
            WithErrorHandling(Cg.SetGame(gameName));
        }

        public void SetState(string stateName)
        {
            WithErrorHandling(Cg.SetState(stateName));
        }

        public void SetEvent(string eventName)
        {
            WithErrorHandling(Cg.SetEvent(eventName));
        }

        public void ClearState(string stateName)
        {
            WithErrorHandling(Cg.ClearState(stateName));
        }

        public void ClearAllStates()
        {
            WithErrorHandling(Cg.ClearAllStates());
        }

        public void ClearAllEvents()
        {
            WithErrorHandling(Cg.ClearAllEvents());
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
