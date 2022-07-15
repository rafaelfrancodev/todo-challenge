using Application.AppServices.Todo.Validators;
using FluentValidation.Results;
using Infra.CrossCutting.Validators;
using System.ComponentModel.DataAnnotations;

namespace Application.AppServices.Todo.Inputs
{
    public class TodoInput : ValidationInput
    {       
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        
        public override bool IsValid()
        {
            ValidationResult = new TudoInputValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
