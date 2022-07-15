using Application.AppServices.Todo.Inputs;
using Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public class InputToEntityMapperProfile : AutoMapper.Profile
    {
        public InputToEntityMapperProfile()
        {
            CreateMap<TodoInput, Todo>();

        }
    }
}
