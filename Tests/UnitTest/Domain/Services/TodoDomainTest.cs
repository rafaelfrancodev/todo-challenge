using AutoFixture;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UoW;
using FluentAssertions;
using Infra.CrossCutting.Notification.Handler;
using Infra.CrossCutting.Notification.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using DomainTest = Domain.Services;

namespace UnitTest.Domain
{
    public class TodoDomainTest
    {
        private Mock<ITodoRepository> _repositoryMock;       
        private Mock<ISmartNotification> _smartNotificationMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private DomainTest.TodoDomainService _target;
        private Mock<ILogger<DomainTest.TodoDomainService>> _loggerMock;
        private readonly Fixture _fixture;

        public TodoDomainTest()
        {
            _repositoryMock = new Mock<ITodoRepository>();
            _smartNotificationMock = new Mock<ISmartNotification>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<DomainTest.TodoDomainService>>();
            _smartNotificationMock.Setup(x => x.Invoke()).Returns(_smartNotificationMock.Object);
            _target = new DomainTest.TodoDomainService(_repositoryMock.Object, _smartNotificationMock.Object, _unitOfWorkMock.Object, new DomainNotificationHandler(), _loggerMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldCreateInstance()
        {
            _target.Should().NotBeNull();
        }


    }
}
