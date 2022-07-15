using Infra.CrossCutting.Notification.Enum;
using Infra.CrossCutting.Notification.Handler;
using Infra.CrossCutting.Notification.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Infra.CrossCutting.Controllers
{

    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _messageHandler;
        protected BaseController(INotificationHandler<DomainNotification> notification)
        {
            _messageHandler = (DomainNotificationHandler)notification;
        }

        protected IActionResult InternalServerError(Exception ex) => StatusCode((int)HttpStatusCode.InternalServerError, ex);

        protected bool HasNotification()
        {
            return _messageHandler.HasNotifications();
        }

        protected IActionResult NotificationBusiness()
        {
            var notifications = _messageHandler.GetNotifications();
            var domainNotificationType = notifications?.FirstOrDefault()?.DomainNotificationType;
            domainNotificationType = (domainNotificationType != null) ? domainNotificationType : DomainNotificationType.BadRequest;

            return new JsonResult(new ExceptionResponse(notifications?.ToList(), (HttpStatusCode)domainNotificationType))
            {
                StatusCode = (int) domainNotificationType
            };
        }

        protected IActionResult NotificationBusinessOK<T>(T result = default(T))
        {
            var notifications = _messageHandler.GetNotifications();

            return Ok(new Result<T>
            {
                Error = true,
                Data = result,
                Messages = notifications?.ToList().Select(x => x.Value).ToArray()
            });
        }

        protected IActionResult HttpResponse(object response, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (_messageHandler.HasNotifications())
            {
                var notifications = _messageHandler.GetNotifications();

                var domainNotificationType = notifications?.FirstOrDefault()?.DomainNotificationType;
                if (domainNotificationType != null)
                {
                    return new JsonResult(new ExceptionResponse(notifications.ToList(), (HttpStatusCode) domainNotificationType))
                    {
                        StatusCode = (int)domainNotificationType
                    };
                }
            }

            return new JsonResult(response)
            {
                StatusCode = (int)statusCode
            };
        }
     
        protected IActionResult OkOrDefault<T>(T result = default(T))
        {

            if (!HasNotification())
            {
                return Ok(new Result<T>
                {
                    Error = false,
                    Data = result
                });               
            }

            return NotificationBusinessOK<T>();
        }

        protected IActionResult OkOrNotFound<T>(T result = default(T))
        {

            if (!HasNotification())
            {
                if (result != null)
                {
                    if (result is bool)
                        return Ok();

                    return Ok(new Result<T>
                    {
                        Error = false,
                        Data = result
                    });
                }

                return NotFound();

            }

            return NotificationBusiness();
        }

        protected IActionResult OkOrNoContent<T>(T result = default(T))
        {
            if (!HasNotification() && result != null)
            {
                if (result is IEnumerable<T>)
                {
                    if (((ICollection<T>)result).Count > 0)
                    {
                        return Ok(new Result<T>
                        {
                            Error = false,
                            Data = result
                        });
                    }

                    return NoContent();
                }

                return Ok(new Result<T>
                {
                    Error = false,
                    Data = result
                });
            }

            if (!HasNotification() && result == null)
            {
                return NoContent();
            }

            return NotificationBusiness();
        }

        protected IActionResult AcceptedContent<T>(T result = default(T))
        {
            if (!HasNotification())
            {
                if (result != null)
                    return Accepted(new Result<T>
                    {
                        Error = false,
                        Data = result
                    });

                return NotFound();
            }

            return NotificationBusiness();
        }

        protected IActionResult AcceptedOrContent<T>(T result = default(T))
        {
            if (!HasNotification())
            {
                if (result != null)
                    return Accepted(new Result<T>
                    {
                        Error = false,
                        Data = result
                    });

                return Accepted();
            }

            return NotificationBusiness();
        }

        protected IActionResult Error(string Message)
            {
            return Ok(new Result() { Error = true, Messages = new string[] { Message } });
            }

        protected IActionResult Error(string[] Messages)
            {
            return Ok(new Result() { Error = true, Messages = Messages });
            }

        protected IActionResult Error(IEnumerable<string> Messages)
            {
            return Error(Messages.ToArray());
            }

        protected IActionResult CreatedContent<T>(string rota, T result = default(T))
        {
            if (!HasNotification())
            {
                if (result != null)
                    return Created(rota, new Result<T>
                    {
                        Error = false,
                        Data = result
                    });

                return Created(rota, new Result
                {
                    Error = false
                });
            }

            return NotificationBusiness();
        }
    }
    public class Result
    {
        public bool Error { get; set; }
        public string[] Messages { get; set; }

    }
    public class Result<T> : Result
    {
        public T Data { get; set; }
    }

}
