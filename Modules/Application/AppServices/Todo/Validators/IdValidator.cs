using Infra.CrossCutting.Notification.Interfaces;

namespace Application.AppServices.Todo.Validators
{
    public static class IdValidator
    {
        public static bool IsValid(this Guid id, ISmartNotification notification)
        {

            if (id == Guid.Empty)
            {
                notification.NewNotificationBadRequest(new string[] { id.ToString() }, "The Id '{0}' is invalid.");
                return false;
            }

            return true;
        }
    }
}
