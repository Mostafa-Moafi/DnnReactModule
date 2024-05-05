// MIT License

using DotNetNuke.Web.Api;
using DnnReactModule.Utilities.API;
using DnnReactModule.Utilities.Helper;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace DnnReactModule.Utilities.FiltersAttribute
{
    /// <summary>
    /// Custom authorization attribute that extends AuthorizeAttributeBase and implements authorization logic based on DnnAuthorizeAttribute.
    /// </summary>
    public class ResultAuthorizeAttribute : AuthorizeAttributeBase
    {
        public override bool IsAuthorized(AuthFilterContext context)
        {
            DnnAuthorizeAttribute dnnAuthorizeAttribute = new DnnAuthorizeAttribute();
            return dnnAuthorizeAttribute.IsAuthorized(context);
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.Forbidden;
            ApiResultStatusCode apiStatusCode = ApiResultStatusCode.Forbidden;
            string message = "access has been denied for this request.";

            if (base.SkipAuthorization(actionContext))
            {
                return;
            }
            var authFilterContext = new AuthFilterContext(actionContext, message);
            if (!this.IsAuthorized(authFilterContext))
            {
                WriteToResponseAsync();
            }

            void WriteToResponseAsync()
            {
                var result = new ApiResult(true, apiStatusCode, apiStatusCode.ToDisplay());
                var json = JsonConvert.SerializeObject(result);

                // This fallback prevents exposing details about
                // unexpected exceptions for security reasons.
                actionContext.Response = new HttpResponseMessage(httpStatusCode)
                {
                    Content = new StringContent(json),
                    StatusCode = httpStatusCode
                };
            }
        }
    }
}