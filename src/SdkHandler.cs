using System;
using Microsoft.Extensions.Options;

namespace OpenCueService
{
    using CgSdk;
    public class SdkHandler
    {
        private readonly CorsairProtocolDetails CorsairProtocolDetails;

        private bool hasControl = false;

        private readonly string Game;
        public SdkHandler(IOptions<Config> config)
        {
            Game = config.Value.Game;
            CorsairProtocolDetails = PerformProtocolHandshake();
            RequestControl();
            SetGame(Game);
        }
        public CorsairProtocolDetails GetCorsairProtocolDetails()
        {
            return CorsairProtocolDetails;
        }

        public string GetGame()
        {
            return Game;
        }

        public bool HasControl()
        {
            return hasControl;
        }
        public SdkError GetLastError()
        {
            var RawErrorCode = CgSdkInterop.GetLastError();
            var ErrorCode = Enum.IsDefined(typeof(CorsairError), RawErrorCode) ? (CorsairError)RawErrorCode : OpenCueService.CorsairError.Unknown;
            return new SdkError(ErrorCode);
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
        public SdkError(CorsairError corsairError)
           : base($"Sdk Error occurred: {corsairError.GetMessage()}")
        {
            CorsairError = corsairError;
        }

        public readonly CorsairError CorsairError;
    }

    public static class Extensions
    {
        public static string GetMessage(this CorsairError corsairError)
        {
            switch (corsairError)
            {
                case CorsairError.CE_Success: return "Success - Previously called function was completed successfully";
                case CorsairError.CE_ServerNotFound: return "ServerNotFound - CUE is not running or was shut down or third-party control is disabled in CUE settings";
                case CorsairError.CE_NoControl: return "NoControl - some other client has or took over exclusive control";
                case CorsairError.CE_ProtocolHandshakeMissing: return "ProtocolHandshakeMissing - developer did not perform protocol handshake";
                case CorsairError.CE_IncompatibleProtocol: return "IncompatibleProtocol - developer is calling the function that is not supported by the server (either protocol has been broken by server or client or the function is new and server is too old. Check CorsairProtocolDetails for details)";
                case CorsairError.CE_InvalidArguments: return "InvalidArguments - developer supplied invalid arguments to the function (for specifics look at function descriptions)";
                case CorsairError.Unknown:
                default: return "Unknown - this error code is unknown";
            }
        }
    }

    public enum CorsairError : int
    {
        CE_Success = 0,
        CE_ServerNotFound = 1,
        CE_NoControl = 2,
        CE_ProtocolHandshakeMissing = 3,
        CE_IncompatibleProtocol = 4,
        CE_InvalidArguments = 5,
        Unknown = -1
    }
}
