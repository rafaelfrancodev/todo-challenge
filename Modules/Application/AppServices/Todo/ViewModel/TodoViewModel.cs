using Infra.CrossCutting.UoW.Models;

namespace Application.AppServices.Todo.ViewModel
{
    public class TodoViewModel : BaseResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime UpdatedAt { get; set; }

        public TodoViewModel()
        {
        }
    }
}
