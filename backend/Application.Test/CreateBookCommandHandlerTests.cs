using Application.Books.Commands;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test
{
    public class CreateBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;

        public CreateBookCommandHandlerTests()
        {
            _bookRepositoryMock = new();
        }

        [Fact]
        public async Task Handler_Should_ReturnTrueResult_WhenCreateANewBook()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand("The Women: A Novel", "Kristin Hannah", DateTime.UtcNow, 14.99);
            var handler = new CreateBookCommandHandler(_bookRepositoryMock.Object);

            //Act
            var result = await handler.Handle(createBookCommand, CancellationToken.None);

            //Assert
            _bookRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Once);
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBe(Guid.Empty);
        }
        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateBook()
        {
            // Arrange
            var BookRepositoryMock = new Mock<IBookRepository>();
            var createBookCommand = new CreateBookCommand("Narcotopia: In Search of the Asian Drug Cartel That Survived the CIA", "Patrick Winn", DateTime.UtcNow, 18.99);
            var handler = new CreateBookCommandHandler(BookRepositoryMock.Object);

            // Act
            var result = await handler.Handle(createBookCommand, CancellationToken.None);

            // Assert
            BookRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Once);
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Task Handle_InvalidName_ShouldReturnThrowExceptionResult()
        {
            // Arrange
            var BookRepositoryMock = new Mock<IBookRepository>();
            var createBookCommand = new CreateBookCommand("", "Patrick Winn", DateTime.UtcNow, 18.99);
            var handler = new CreateBookCommandHandler(BookRepositoryMock.Object);

            // Act
            var result = handler.Handle(createBookCommand, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<BookCannotCreatedException>(() => result);
        }

        [Fact]
        public async Task Handle_InvalidSalary_ShouldReturnThrowExceptionResult()
        {
            // Arrange
            var BookRepositoryMock = new Mock<IBookRepository>();
            var createBookCommand = new CreateBookCommand("Narcotopia: In Search of the Asian Drug Cartel That Survived the CIA", "Patrick Winn", DateTime.UtcNow, -18.00);
            var handler = new CreateBookCommandHandler(BookRepositoryMock.Object);

            // Act
            var result = handler.Handle(createBookCommand, CancellationToken.None);

            // Assert

            await Assert.ThrowsAsync<BookCannotCreatedException>(() => result);
        }

        [Fact]
        public async Task Handle_InvalidHiringDate_ShouldReturnThrowExceptionResult()
        {
            // Arrange
            var BookRepositoryMock = new Mock<IBookRepository>();
            var createBookCommand = new CreateBookCommand("Narcotopia: In Search of the Asian Drug Cartel That Survived the CIA", "Patrick Winn", null, 18.99);
            var handler = new CreateBookCommandHandler(BookRepositoryMock.Object);

            // Act
            var result = handler.Handle(createBookCommand, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<BookCannotCreatedException>(() => result);
        }


    }
}
