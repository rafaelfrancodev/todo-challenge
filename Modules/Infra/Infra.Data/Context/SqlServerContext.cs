using Domain.Entities;
using Infra.Data.Config;
using Infra.Data.Config.Mappers.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Data.Context
{
    [ExcludeFromCodeCoverage]
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoSqlServerConfig());
        }

        public DbSet<Todo> ToDos { get; set; }
     
        public override int SaveChanges()
        {
            ConfigPropertieDefault.SaveDefaultPropertiesChanges(ChangeTracker);
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            ConfigPropertieDefault.SaveDefaultPropertiesChanges(ChangeTracker);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
