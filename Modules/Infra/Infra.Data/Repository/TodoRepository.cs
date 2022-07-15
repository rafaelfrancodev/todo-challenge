using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.CrossCutting.Repository;
using Infra.Data.Context;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Data.Repository
{
    [ExcludeFromCodeCoverage]
    public class TodoRepository : GenericRepository<Todo, Guid, SqlServerContext>, ITodoRepository
    {

        public TodoRepository(SqlServerContext context) : base(context)
        {
            _context = context;
        }
    }
}
