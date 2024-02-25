using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Queries
{
    public sealed record GetBookByIdQuery(Guid id) : IQuery<Book>;
    public sealed class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICacheService _cacheService;
        public GetBookByIdQueryHandler(IBookRepository bookRepository, ICacheService cacheService)
        {
            _bookRepository = bookRepository;
            _cacheService = cacheService;
        }

        public async Task<Result<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"GetBookById_{request.id}";

            var book = await _cacheService.GetOrCreateAsync(cacheKey, async () => await _bookRepository.GetByIdAsync(request.id));

            if (book is null)
                return Result<Book>.Failure(new Error("Book.NotFound", $"The book with Id {request.id} was not found"));

            return new Result<Book>(book);
        }
    }
}
