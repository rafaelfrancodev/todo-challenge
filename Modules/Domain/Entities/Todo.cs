using Infra.CrossCutting.Repository;

namespace Domain.Entities
{
    public class Todo : BaseEntityDates<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
