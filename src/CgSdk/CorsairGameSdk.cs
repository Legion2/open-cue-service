using System;

namespace OpenCueService.CgSdk
{
    public class CorsairGameSdk
    {
        private readonly CorsairProtocolDetails CorsairProtocolDetails;
        private readonly string Game;
        private bool hasControl = false;

        // Create a connection to the iCUE, there can only be one connection per process.
        // If iCUE is restarted the connection must also be recreated.
        public CorsairGameSdk(String game)
        {
            Game = game;
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
            if (Enum.IsDefined(typeof(CorsairError), RawErrorCode))
            {
                return new SdkError((CorsairError)RawErrorCode);
            }
            else
            {
                return new UnknownSdkError(RawErrorCode);
            }
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

        private void SetGame(string gameName)
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
                var Error = GetLastError();
                if (Error.CorsairError != CorsairError.CE_Success && Error.CorsairError != CorsairError.CE_StateNotSet) {
                    throw Error;
                }
            }
        }
    }

    public class SdkError : Exception
    {
        public SdkError(CorsairError corsairError)
           : this(corsairError, $"Sdk Error occurred: {corsairError.GetMessage()}") { }

        public SdkError(CorsairError corsairError, string message)
           : base(message)
        {
            CorsairError = corsairError;
        }

        public readonly CorsairError CorsairError;
    }

    public class UnknownSdkError : SdkError
    {
        public UnknownSdkError(int rawCorsairError)
           : base(CorsairError.Unknown, $"Unknown Sdk Error occurred: {rawCorsairError}")
        {
            RawCorsairError = rawCorsairError;
        }

        public readonly int RawCorsairError;
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
                case CorsairError.CE_InvalidArguments: return "InvalidArguments - the profiles directory does not exist or is not configured correctly";
                case CorsairError.CE_ProfilesConfigurationProblem: return "ProfilesConfigurationProblem - problem related to profiles and priorities file";
                case CorsairError.CE_StateNotSet: return "StateNotSet - developer is calling clear but state is not set";
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
        CE_ProfilesConfigurationProblem = 7,
        CE_StateNotSet = 8,
        Unknown = -1
    }
}
