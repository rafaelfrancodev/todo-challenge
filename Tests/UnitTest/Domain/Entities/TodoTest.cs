using AutoFixture;
using DomainTest = Domain.Entities;
using FluentAssertions;
using Xunit;

namespace UnitTest.Domain.Entities
{
    public class TodoTest
    {
        private Fixture _fixture;
        public TodoTest()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void ShouldReturnInstance()
        {
            // prepare
            var entity = _fixture.Create<DomainTest.Todo>();

            // act           
            var entityToCompare = new DomainTest.Todo()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                DueDate = entity.DueDate,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };

            // assert
            entityToCompare.Should().BeEquivalentTo(entity);
        }
    }
}
