using Application.AppServices.Todo.Inputs;
using Application.AppServices.Todo.ViewModel;
using AutoFixture;
using FluentAssertions;
using IntegratedTest.Config;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Xunit;

namespace IntegratedTest.Scenarios.Users
{
    public class InsertTodoTest
    {
        private Fixture _fixture;
        public InsertTodoTest()
        {
            _fixture = new Fixture();
        }

        [Fact]      
        public async Task InsertUserAsyncOK()
        {
            // arr
            var userInsertId = Guid.Empty;
            var input = _fixture.Create<TodoInput>();
          

            await using var application = new ApplicationHost();

            IServiceCollection services = new ServiceCollection();             

            //act
            using var client = application.CreateClient();
            using var response = await client.PostAsync("/api/v1/todo", ContentHelper<object>.FormatStringContent(input));

            //assert
            var result = await ContentHelper<TodoViewModel>.GetResponse(response);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Error.Should().BeFalse();
            userInsertId = result.Data.Id;           
        }
    }
}
