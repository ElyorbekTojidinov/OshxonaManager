using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace RestoranManager.Filter
{
    public class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {

        public string name { get; set; }
        public string password { get; set; }
        ClaimsPrincipal principal = new ClaimsPrincipal();

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (principal.FindFirstValue(ClaimTypes.Name) == null)
            {
                throw new Exception("Claim is not valid");
            }
        }
    }
}
