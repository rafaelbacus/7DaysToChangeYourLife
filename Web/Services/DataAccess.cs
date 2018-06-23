using System;
using Microsoft.Extensions.DependencyInjection;
using DAL;

namespace Web.Services
{
    public static class DataAccess
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<BlogDAL>();
            services.AddScoped<CommentDAL>();
            services.AddScoped<PostDAL>();

            return services;
        }
    }
}