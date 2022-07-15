using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace Infra.CrossCutting.UoW.Models
{
    public class BaseResult
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }
    }
}
