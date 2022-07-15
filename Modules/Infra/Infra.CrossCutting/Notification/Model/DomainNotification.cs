using Infra.CrossCutting.Notification.Enum;
using MediatR;
using System.Text.Json.Serialization;

namespace Infra.CrossCutting.Notification.Model
{
    public class DomainNotification : INotification
    {
        public string Value { get; }

        [JsonIgnore]
        public DomainNotificationType DomainNotificationType { get; }

        public DomainNotification(
            string value,
            DomainNotificationType type = DomainNotificationType.Conflict)
        {
            Value = value;
            DomainNotificationType = type;
        }
    }
}
