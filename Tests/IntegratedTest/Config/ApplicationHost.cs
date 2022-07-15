using Infra.Data.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntegratedTest.Config
{
    internal class ApplicationHost  : WebApplicationFactory<Program>
    {
        private readonly string _environment;

        public ApplicationHost(string environment = "Development")
        {
            _environment = environment;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

            // Add mock/test services to the builder here
            builder.ConfigureServices(services =>
            {
                var configuration = ConnectionString.GetConnection();

                services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(configuration["ConnectionStrings:SqlServer"]));

            });

            return base.CreateHost(builder);
        }
    }

}
