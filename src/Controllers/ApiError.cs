using System;

namespace OpenCueService
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
