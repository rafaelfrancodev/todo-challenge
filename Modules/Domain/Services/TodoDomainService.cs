using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.UoW;
using Infra.CrossCutting.Domain.Services;
using Infra.CrossCutting.Notification.Interfaces;
using Infra.CrossCutting.Notification.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Services
{
    public class TodoDomainService : DomainService<Todo, Guid, IUnitOfWork>, ITodoDomainService
    {
        private readonly ITodoRepository _todoRepository;
        private ISmartNotification _notification;
        ILogger<TodoDomainService> _logger;

        public TodoDomainService(
            ITodoRepository todoRepository,
            ISmartNotification notification,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> messageHandler,
            ILogger<TodoDomainService> logger
        ) : base(todoRepository, unitOfWork, messageHandler)
        {
            _todoRepository = todoRepository;
            _notification = notification;
            _logger = logger;
        }
    }
}
