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
    public sealed record CreateBookCommand(string Title, string Author, DateTime? PublishedDate, double Price) : ICommand<Guid>;
    public sealed class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, Guid>
    {
        private readonly IBookRepository _bookRepository;
        public CreateBookCommandHandler(IBookRepository bookRepository) 
        { 
            _bookRepository = bookRepository;
        }
        public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = Book.Create(Guid.NewGuid(), request.Title, request.Author, request.PublishedDate, request.Price);

            await _bookRepository.AddAsync(book);

            return Result<Guid>.Success(book.Id);
        }
    }
}
