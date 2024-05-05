// MIT License

using DotNetNuke.Services.Localization;
using DnnReactModule.Utilities.API;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DnnReactModule.Utilities.FiltersAttribute
{
    /// <summary>
    /// Custom action filter attribute for validating model state before executing an action.
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private string resourceFileRoot;

        private string ResourceFileRoot
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.resourceFileRoot))
                    this.resourceFileRoot = HostingEnvironment.MapPath($"~/DesktopModules/{GlobalParameter.ModuleName}/resources/App_LocalResources/ModelValidation.resx");

                return this.resourceFileRoot;
            }
        }

        /// <summary>
        /// When the action executes, validates the model state.
        /// </summary>
        /// <param name="HttpActionContext">The context of the action.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var sb = new StringBuilder();
            if (actionContext.ModelState.IsValid == false)
            {
                var localization = new LocalizationProvider();
                foreach (var value in actionContext.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        var localized = localization.GetString(error.ErrorMessage, this.ResourceFileRoot);
                        if (string.IsNullOrWhiteSpace(localized))
                        {
                            localized = error.ErrorMessage;
                        }

                        sb.AppendLine(localized);
                    }
                }

                WriteToResponse();
            }

            void WriteToResponse()
            {
                var result = new ApiResult(false, ApiResultStatusCode.BadRequest, sb.ToString());
                var json = JsonConvert.SerializeObject(result);

                // This fallback prevents exposing details about
                // unexpected exceptions for security reasons.
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(json),
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
        }
    }
}
