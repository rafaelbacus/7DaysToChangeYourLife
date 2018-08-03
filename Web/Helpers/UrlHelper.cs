using System;
using Microsoft.AspNetCore.Http;

namespace Web.Helpers
{
    public static class UrlHelper 
    {
        public static string GetRequestUrl(HttpContext context)
        {
            return context.Request.Host.Value + 
                   context.Request.Path.Value + 
                   context.Request.QueryString.Value;
        }
    }
}