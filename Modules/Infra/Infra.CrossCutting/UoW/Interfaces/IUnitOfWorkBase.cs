using Infra.CrossCutting.UoW.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading.Tasks;

namespace Infra.CrossCutting.UoW.Interfaces
{
    public interface IUnitOfWorkBase
    {
        IDbContextTransaction DbTransaction { get; }
        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        CommandResponse Commit();
        bool CurrentTransaction();
        Task SaveChangesAsync();
    }
}
