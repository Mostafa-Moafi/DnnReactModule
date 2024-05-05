// MIT License

using DotNetNuke.Instrumentation;
using DnnReactModule.Utilities.API;
using DnnReactModule.Utilities.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace DnnReactModule.Utilities.FiltersAttribute
{
    /// <summary>
    /// Handles exceptions on API endpoint calls.
    /// </summary>
    public class ModuleExceptionAttribute : ExceptionFilterAttribute
    {
        private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof(ModuleExceptionAttribute));
        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            ApiResultStatusCode apiStatusCode = ApiResultStatusCode.ServerError;
            string message = null;

            var exception = actionExecutedContext.Exception;
            var apiEx = exception as AppException;

            if (apiEx != null)
                httpStatusCode = ConvertApiStatusCodeToHttpCode(apiEx.ApiStatusCode);

            //if (HttpStatusCode.InternalServerError == httpStatusCode)
            Logger.Error(exception.Message, exception);

            if (exception is null)
                WriteToResponse();

            if (exception is AppException)
            {
                apiStatusCode = ((AppException)exception).ApiStatusCode;
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    //["StackTrace"] = exception.StackTrace,
                };
                if (exception.InnerException != null)
                {
                    dic.Add("InnerException.Exception", exception.InnerException.Message);
                    //dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                }
                if (exception.Data.Count > 0)
                    dic.Add("AdditionalData", JsonConvert.SerializeObject(exception.Data));

                message = JsonConvert.SerializeObject(dic);
                WriteToResponse();
                await Task.CompletedTask;
            }
            else
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    //["StackTrace"] = exception.StackTrace,
                };

                message = JsonConvert.SerializeObject(dic);
                WriteToResponse();
            }
            void WriteToResponse()
            {
                var result = new ApiResult(false, apiStatusCode, message);
                var json = JsonConvert.SerializeObject(result);

                // Deserialize the JSON string
                JObject jsonObject = JsonConvert.DeserializeObject<JObject>(json);
                // Convert the nested JSON string inside "Message" to a JObject
                string messageJson = jsonObject["Message"] != null ? jsonObject["Message"].ToString() : "";
                if (messageJson != "")
                {
                    JObject messageObject = JsonConvert.DeserializeObject<JObject>(messageJson);

                    // Update the original object
                    jsonObject["Message"] = messageObject;
                }

                // Convert the updated object back to JSON
                string outputJson = JsonConvert.SerializeObject(jsonObject);

                // This fallback prevents exposing details about
                // unexpected exceptions for security reasons.
                actionExecutedContext.Response = new HttpResponseMessage(httpStatusCode)
                {
                    Content = new StringContent(outputJson),
                    StatusCode = httpStatusCode
                };
            }
        }
        HttpStatusCode ConvertApiStatusCodeToHttpCode(ApiResultStatusCode statusCode)
        {
            switch (statusCode)
            {
                case ApiResultStatusCode.Success:
                    return HttpStatusCode.OK;

                case ApiResultStatusCode.NoContent:
                    return HttpStatusCode.NoContent;

                case ApiResultStatusCode.BadRequest:
                    return HttpStatusCode.BadRequest;

                case ApiResultStatusCode.UnAuthorized:
                    return HttpStatusCode.Unauthorized;

                case ApiResultStatusCode.NotFound:
                    return HttpStatusCode.NotFound;

                case ApiResultStatusCode.ServerError:
                    return HttpStatusCode.BadRequest;

                default:
                    return HttpStatusCode.BadRequest;
            }
        }
    }
}
