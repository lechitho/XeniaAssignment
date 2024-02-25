using Application.Books.Commands;
using Domain.Abstractions;
using Domain.Entities;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test
{
    public class UpdateBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;

        public UpdateBookCommandHandlerTests()
        {
            _bookRepositoryMock = new();
        }

        [Fact]
        public async Task Handler_Should_ReturnTrueResult_WhenUpdateBook()
        {
            // Arrange
            var BookId = Guid.NewGuid();
            var updateBookCommand = new UpdateBookCommand(BookId, Book.Create(BookId, "Narcotopia", "Patrick", DateTime.UtcNow, 17.5));
            _bookRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Book>())).ReturnsAsync(true);
            var handler = new UpdateBookCommandHandler(_bookRepositoryMock.Object);

            // Act
            var result = await handler.Handle(updateBookCommand, CancellationToken.None);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Book>()), Times.Once);
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Title.ShouldBe("Narcotopia");
        }

        [Fact]
        public async Task Handler_Should_ReturnFail_WhenUpdateANotExistedBook()
        {
            // Arrange
            var BookId = Guid.NewGuid();
            var updateBookCommand = new UpdateBookCommand(BookId, Book.Create(BookId, "Narcotopia", "Patrick", DateTime.UtcNow, 17.5));

            _bookRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Book>())).ReturnsAsync(false);

            var handler = new UpdateBookCommandHandler(_bookRepositoryMock.Object);

            // Act
            var result = await handler.Handle(updateBookCommand, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeFalse(); // Check that the result indicates failure
            result.Error.ShouldNotBeNull();
            result.Error.Message.ShouldNotBeEmpty();
        }
    }
}
