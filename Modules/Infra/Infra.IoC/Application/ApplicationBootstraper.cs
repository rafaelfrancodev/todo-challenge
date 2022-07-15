using Application.AppServices.Todo;
using Infra.CrossCutting.Notification.Implements;
using Infra.CrossCutting.Notification.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infra.IoC.Application
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationBootstraper
    {
        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //APPLICATION
            services.AddScoped<ISmartNotification, SmartNotification>();
            services.AddScoped<ITodoApplication, TodoApplication>();
        }
    }
}
