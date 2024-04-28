// MIT License

using DnnReactDemo.Utilities.Helper;
using Newtonsoft.Json;
using System.Web.Http.Results;

namespace DnnReactDemo.Utilities.API
{
    /// <summary>
    /// Represents the result of an API operation, including success status, status code, and optional message.
    /// Provides implicit conversion operators for different types of ActionResult in ASP.NET Core.
    /// </summary>
    /// <returns>
    /// An instance of ApiResult with the specified success status, status code, and message.
    /// </returns>
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode StatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message ?? statusCode.ToDisplay();
        }

        #region Implicit Operators
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.Success);
        }


        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.BadRequest);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.NoContent);
        }
        #endregion
    }

    /// <summary>
    /// Represents an API result with generic data of type TData.
    /// </summary>
    /// <typeparam name="TData">The type of data to be included in the API result.</typeparam>
    /// <param name="isSuccess">A boolean indicating if the operation was successful.</param>
    /// <param name="statusCode">The status code of the API result.</param>
    /// <param name="data">The data to be included in the API result.</param>
    /// <param name="message">An optional message to be included in the API result.</param>
    /// <returns>
    /// An instance of ApiResult with the specified data and properties.
    /// </returns>
    public class ApiResult<TData> : ApiResult
        where TData : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }

        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null)
            : base(isSuccess, statusCode, message)
        {
            Data = data;
        }

        #region Implicit Operators
        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, data);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, null);
        }

        public static implicit operator ApiResult<TData>(OkNegotiatedContentResult<TData> result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, (TData)result.Content);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null);
        }

        public static implicit operator ApiResult<TData>(BadRequestErrorMessageResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null, result.Message);
        }
        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.NoContent, null);
        }
        #endregion
    }
}
