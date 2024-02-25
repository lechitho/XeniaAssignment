using Application.Books.Commands;
using Application.Books.Queries;
using BookManagement.API.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBooks()
        {
            var query = new GetBooksQuery();

            var BooksResult = await _mediator.Send(query);

            var Books = BooksResult.Value;

            if (Books is null) return NotFound();

            return Ok(BooksResult.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel>> GetBook(Guid id)
        {
            var query = new GetBookByIdQuery(id);
            var BookResult = await _mediator.Send(query);

            if (BookResult is null ||
                (!BookResult.IsSuccess
                    && BookResult.Error is not null
                    && BookResult.Error.Type.Equals("Book.NotFound", StringComparison.CurrentCultureIgnoreCase)))
                return NotFound();

            return Ok(BookResult.Value);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook(BookModel book)
        {
            var createBookCommand = new CreateBookCommand(book.Title, book.Author, book.PublishedDate, book.Price);
            var id = (await _mediator.Send(createBookCommand)).Value;
            return CreatedAtAction("GetBook", new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookModel book)
        {
            var command = new UpdateBookCommand(id, Book.Create(id, book.Title, book.Author, book.PublishedDate, book.Price));

            if (id != command.id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            await _mediator.Send(new DeleteBookCommand(id));
            return NoContent();
        }
    }
}
