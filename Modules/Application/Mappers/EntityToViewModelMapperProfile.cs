using Application.AppServices.Todo.ViewModel;
using Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public class EntityToViewModelMapperProfile : AutoMapper.Profile
    {
        public EntityToViewModelMapperProfile()
        {
            CreateMap<Todo, TodoViewModel>();

        }
    }
}
