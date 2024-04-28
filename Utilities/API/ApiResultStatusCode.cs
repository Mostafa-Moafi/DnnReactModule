using System.ComponentModel.DataAnnotations;

namespace DnnReactDemo.Utilities.API
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "Success")]
        Success = 200,

        [Display(Name = "NoContent")]
        NoContent = 204,

        [Display(Name = "BadRequest")]
        BadRequest = 400,

        [Display(Name = "UnAuthorized")]
        UnAuthorized = 401,

        [Display(Name = "Forbidden")]
        Forbidden = 403,

        [Display(Name = "NotFound")]
        NotFound = 404,

        [Display(Name = "ServerError")]
        ServerError = 500,
    }
}
