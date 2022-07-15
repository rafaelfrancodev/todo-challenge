using Application.AppServices.Todo.Inputs;
using Application.AppServices.Todo.ViewModel;

namespace Application.UseCases.Todo
{
    public interface ITodoApplication
    {
        Task<IEnumerable<TodoViewModel>> GetAllAsync();
        Task<TodoViewModel> GetAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<TodoViewModel> InsertAsync(TodoInput userInput);
        Task<TodoViewModel> UpdateAsync(Guid id, TodoInput userInput);
    }
}
