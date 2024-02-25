using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands
{
    public sealed record DeleteBookCommand(Guid Id) : ICommand<Unit>;

    public sealed class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand, Unit>
    {
        private readonly IBookRepository _bookRepository;
        public DeleteBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<Result<Unit>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await _bookRepository.RemoveAsync(request.Id);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
