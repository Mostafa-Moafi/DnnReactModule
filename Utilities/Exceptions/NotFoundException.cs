// MIT License

using DnnReactModule.Utilities.API;
using System;

namespace DnnReactModule.Utilities.Exceptions
{
    /// <summary>
    /// Custom exception class for handling "Not Found" errors in the application.
    /// </summary>
    /// <returns>
    /// NotFoundException instance with different constructors for providing message, additional data, and exception details.
    /// </returns>
    public class NotFoundException : AppException
    {
        public NotFoundException()
            : base(ApiResultStatusCode.NotFound, System.Net.HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(ApiResultStatusCode.NotFound, message, System.Net.HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(object additionalData)
            : base(ApiResultStatusCode.NotFound, null, System.Net.HttpStatusCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, object additionalData)
            : base(ApiResultStatusCode.NotFound, message, System.Net.HttpStatusCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(ApiResultStatusCode.NotFound, message, exception, System.Net.HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.NotFound, message, System.Net.HttpStatusCode.NotFound, exception, additionalData)
        {
        }
    }

}
