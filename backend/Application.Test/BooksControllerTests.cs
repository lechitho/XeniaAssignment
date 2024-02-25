using Application.Test;
using BookManagement.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using BookManagement.API;

namespace API.Tests
{
    public class BooksControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public BooksControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }
        [Fact, TestPriority(1)]
        public async Task CreateBook_WithValidBook_ReturnsCreatedAtActionAndId()
        {
            // Arrange
            var newBook = new BookModel
            {
                Title = "Narcotopia: In Search of the Asian Drug Cartel That Survived the CIA",
                Author = "Patrick Winn",
                PublishedDate = DateTime.Now,
                Price = 18.99
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/books", newBook);

            // Assert
            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadFromJsonAsync<Guid>();
            Assert.NotEqual(Guid.Empty, id);
        }
        [Fact, TestPriority(2)]
        public async Task GetBooks_ReturnsOkAndListOfBooks()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("api/books");

            // Assert
            response.EnsureSuccessStatusCode();
            var actualBooks = await response.Content.ReadFromJsonAsync<IEnumerable<BookModel>>();

        }


        [Fact, TestPriority(3)]
        public async Task UpdateBook_WithValidIdAndBook_ReturnsNoContent()
        {
            // Arrange
            var existingBookId = Guid.NewGuid();
            var updatedBook = new BookModel { Id = existingBookId, Title = "Narcotopia", Author = "Patrick Winn", PublishedDate = DateTime.Now, Price = 45.15 };

            // Act
            var response = await _client.PutAsJsonAsync($"api/books/{existingBookId}", updatedBook);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact, TestPriority(4)]
        public async Task DeleteBook_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var existingBookId = Guid.NewGuid();

            // Act
            var response = await _client.DeleteAsync($"api/books/{existingBookId}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
