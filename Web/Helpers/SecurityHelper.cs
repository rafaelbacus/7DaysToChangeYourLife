using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace Web.Helpers
{
    public static class SecurityHelper
    {
        public static string GetAntiForgeryToken(HttpContext httpContext)
        {
            string token = string.Empty;
            var antiForgeryService = httpContext.RequestServices.GetService(typeof(IAntiforgery)) as IAntiforgery;
            if (antiForgeryService != null)
            {
                token = antiForgeryService.GetAndStoreTokens(httpContext).RequestToken;
            }

            return token;
        }
    }
}