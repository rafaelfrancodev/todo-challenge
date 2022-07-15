using Application.AppServices.Todo.Inputs;
using FluentValidation;

namespace Application.AppServices.Todo.Validators
{
    public class TudoInputValidator : AbstractValidator<TodoInput>
    {
        public TudoInputValidator()
        {
            RuleFor(doc => doc.Title).NotNull().WithMessage("The title is required.");
            RuleFor(doc => doc.Title).Length(3, 100).WithMessage("The title must be between 3 and 100 characters long.");
            RuleFor(doc => doc.Description).NotNull().WithMessage("The description is required.");
            RuleFor(doc => doc.Description).Length(3, 255).WithMessage("The description must be between 3 and 255 characters long.");
            RuleFor(doc => doc.DueDate).NotNull().WithMessage("The due date is required.");
            RuleFor(doc => doc.DueDate).NotEqual(DateTime.MinValue).WithMessage("The due date is invalid.");
        }
    }
}
