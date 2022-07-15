using Domain.Entities;
using Infra.CrossCutting.Domain.Interfaces;

namespace Domain.Interfaces.Services
{
    public interface ITodoDomainService : IDomainService<Todo, Guid>
    {      
    }
}
