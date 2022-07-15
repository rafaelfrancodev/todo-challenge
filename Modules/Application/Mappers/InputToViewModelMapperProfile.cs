using Application.AppServices.Todo.Inputs;
using Application.AppServices.Todo.ViewModel;
using System.Diagnostics.CodeAnalysis;

namespace Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public class InputToViewModelMapperProfile : AutoMapper.Profile
    {
        public InputToViewModelMapperProfile()
        {
            CreateMap<TodoInput, TodoViewModel>();

        }
    }
}
