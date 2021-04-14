using System;

namespace OpenCue.Service
{
    public class ApiError : Exception
    {
        public readonly int StatusCode;
        public readonly string HttpMessage;
        public ApiError(int httpCode, string message)
           : base($"{httpCode} - {message}")
        {
            StatusCode = httpCode;
            HttpMessage = message;
        }

    }
}
