using Domain.Interfaces.Repositories;
using Domain.Interfaces.UoW;
using Infra.CrossCutting.UoW.Models;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Data.UoW
{
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork
    {
        readonly SqlServerContext _context;
        public IServiceProvider _services { get; set; }

        public UnitOfWork(SqlServerContext context, IServiceProvider serviceProvider)
        {
            var serviceCollection = new ServiceCollection();
            _services = serviceProvider;
            _context = context;
        }

        public IDbContextTransaction DbTransaction { get; private set; }

        public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            return DbTransaction ??= _context.Database.BeginTransaction(isolationLevel);
        }

        public CommandResponse Commit()
        {
            if (DbTransaction == null)
                return new CommandResponse(false);

            _context.SaveChanges();
            _context.Database.CurrentTransaction.Commit();

            DbTransaction = null;
            return new CommandResponse(true);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool CurrentTransaction()
        {
            return _context.Database.CurrentTransaction != null;
        }

        public ITodoRepository Todo => _services.GetRequiredService<ITodoRepository>();       
    }
}
