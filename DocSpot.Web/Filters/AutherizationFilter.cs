using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DocSpot.Web.Filters
{
    public class AutherizationFilter
    {
        public class AdminAuthFilter : Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var AdminId = context.HttpContext.Session.GetInt32("AdminId");
                if (AdminId == null || AdminId == 0)
                {
                    context.Result = new RedirectToActionResult("Login", "Account", new { });
                }
            }
            

        }

        public class DoctorAuthFilter : Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var DoctorId = context.HttpContext.Session.GetInt32("DoctorId");
                if (DoctorId == null || DoctorId == 0)
                {
                    context.Result = new RedirectToActionResult("Login", "Account", new { });
                }
            }
        }

        public class PatientAuthFilter : Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var PatientId = context.HttpContext.Session.GetInt32("PatientId");
                if (PatientId == null || PatientId == 0)
                {
                    context.Result = new RedirectToActionResult("Login", "Account", new { });
                }
            }
        }
    }
}
