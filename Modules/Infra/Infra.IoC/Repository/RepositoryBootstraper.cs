using Domain.Interfaces.Repositories;
using Domain.Interfaces.UoW;
using Infra.Data.Context;
using Infra.Data.Repository;
using Infra.Data.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infra.IoC.Repository
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryBootstraper
    {
        public static void RegisterRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITodoRepository, TodoRepository>();
        }
    }
}
