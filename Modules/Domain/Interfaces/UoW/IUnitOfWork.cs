using Domain.Interfaces.Repositories;
using Infra.CrossCutting.UoW.Interfaces;

namespace Domain.Interfaces.UoW
{
    public interface IUnitOfWork : IUnitOfWorkBase
    {
        ITodoRepository Todo { get; }

    }
}
