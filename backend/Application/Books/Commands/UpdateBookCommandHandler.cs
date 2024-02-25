using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands
{
    public sealed record UpdateBookCommand(Guid id, Book book) : ICommand<Book>;
    public sealed class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand, Book>
    {
        private readonly IBookRepository _bookRepository;
        public UpdateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var updatedBook = Book.Create(request.id, request.book.Title, request.book.Author, request.book.PublishedDate, request.book.Price);

            var success = await _bookRepository.UpdateAsync(updatedBook);

            return success ? Result<Book>.Success(updatedBook) : Result<Book>.Failure(new Error("Book.NotFound", $"Could not find book {request.id} to update"));
        }
    }
}
