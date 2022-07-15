using Domain.Interfaces.Services;
using Domain.Services;
using Infra.CrossCutting.Notification.Handler;
using Infra.CrossCutting.Notification.Model;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infra.IoC.Domain
{
    [ExcludeFromCodeCoverage]
    public static class DomainBootstraper
    {
        public static void RegisterDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();          
            services.AddScoped<ITodoDomainService, TodoDomainService>();

        }
    }
}
