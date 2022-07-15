using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Infra.CrossCutting.Validators
{
    [ExcludeFromCodeCoverage]
    public abstract class ValidationInput
    {
        [JsonIgnore]
        [NotMapped]
        public ValidationResult? ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}
