using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Data.Config.Mappers.SqlServer
{
    [ExcludeFromCodeCoverage]
    public class TodoSqlServerConfig : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("Todo_s", "dbo");
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.Title).HasColumnName("Title");
            builder.Property(c => c.Description).HasColumnName("Description");
            builder.Property(c => c.DueDate).HasColumnName("DueDate");          
            builder.Property(c => c.CreatedAt).HasColumnName("CreatedAt");            
            builder.Property(c => c.UpdatedAt).HasColumnName("UpdatedAt");            
            builder.HasKey(k => k.Id);
        }
    }
}
