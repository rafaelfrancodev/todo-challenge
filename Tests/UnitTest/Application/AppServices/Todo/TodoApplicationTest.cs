using Application.AppServices.Todo.Inputs;
using Application.AppServices.Todo.ViewModel;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Domain.Interfaces.Services;
using FluentAssertions;
using Infra.CrossCutting.Notification.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Test = Application.AppServices.Todo;
using TestDomain = Domain.Entities;

namespace UnitTest.Application.AppServices.Todo
{
    public class TodoApplicationTest
    {
        private Mock<ITodoDomainService> _todoDomainServiceMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ISmartNotification> _smartNotificationMock;
        private Test.TodoApplication _target;
        private readonly Fixture _fixture;

        public TodoApplicationTest()
        {
            // configure
            _todoDomainServiceMock = new Mock<ITodoDomainService>();
            _mapperMock = new Mock<IMapper>();
            _smartNotificationMock = new Mock<ISmartNotification>();
            _smartNotificationMock.Setup(x => x.Invoke()).Returns(_smartNotificationMock.Object);
            _target = new Test.TodoApplication(_todoDomainServiceMock.Object, _smartNotificationMock.Object, _mapperMock.Object);
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }

        [Fact]
        public async Task ShouldUpdateTodoSuccessfulyAsync()
        {
            // arrange
            var input = _fixture.Create<TodoInput>();
            var entity = _fixture.Create<TestDomain.Todo>();
            var viewModel = _fixture.Create<TodoViewModel>();
           
            _mapperMock.Setup(x => x.Map<TestDomain.Todo>(input)).Returns(entity);
            _mapperMock.Setup(x => x.Map<TodoViewModel>(entity)).Returns(viewModel);
            _todoDomainServiceMock.Setup(x => x.UpdateAsync(It.IsAny<TestDomain.Todo>()))
                .ReturnsAsync(entity);
            
            // act
            var result = await _target.UpdateAsync(Guid.NewGuid(), input);

            // assert
            result.Title.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldPostTodoSuccessfulyAsync()
        {
            // arrange
            var input = _fixture.Create<TodoInput>();
            var entity = _fixture.Create<TestDomain.Todo>();
            var viewModel = _fixture.Create<TodoViewModel>();

            _mapperMock.Setup(x => x.Map<TestDomain.Todo>(input)).Returns(entity);
            _mapperMock.Setup(x => x.Map<TodoViewModel>(entity)).Returns(viewModel);
            _todoDomainServiceMock.Setup(x => x.InsertAsync(It.IsAny<TestDomain.Todo>()))
                .ReturnsAsync(entity);

            // act
            var result = await _target.InsertAsync(input);

            // assert
            result.Title.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldGetTodoSuccessfulyAsync()
        {
            // arrange
            var input = _fixture.Create<TodoInput>();
            var entity = _fixture.Create<TestDomain.Todo>();
            var viewModel = _fixture.Create<TodoViewModel>();

            _mapperMock.Setup(x => x.Map<TestDomain.Todo>(input)).Returns(entity);
            _mapperMock.Setup(x => x.Map<TodoViewModel>(entity)).Returns(viewModel);
            _todoDomainServiceMock.Setup(x => x.SelectByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(entity);

            // act
            var result = await _target.GetAsync(Guid.NewGuid());

            // assert
            result.Title.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldGetAllTodoSuccessfulyAsync()
        {
            // arrange
            var entity = _fixture.CreateMany<TestDomain.Todo>();
            var viewModel = _fixture.CreateMany<TodoViewModel>();
            var entityOrdered = entity.OrderByDescending(x => x.CreatedAt);

            _mapperMock.Setup(x => x.Map<IEnumerable<TodoViewModel>>(entityOrdered)).Returns(viewModel);
            _todoDomainServiceMock.Setup(x => x.SelectFilterAsync(It.IsAny<Expression<Func<TestDomain.Todo, bool>>>()))
                .ReturnsAsync(entity);

            // act
            var result = await _target.GetAllAsync();

            // assert
            result.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task ShouldDeleteTodoSuccessfulyAsync()
        {
            // arrange
            _todoDomainServiceMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            // act
            var result = await _target.DeleteAsync(Guid.NewGuid());

            // assert
            result.Should().BeTrue();
        }
    }
}
