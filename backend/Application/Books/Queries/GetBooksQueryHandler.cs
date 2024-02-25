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
    public sealed record GetBooksQuery() : IQuery<IEnumerable<Book>>;

    public sealed class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, IEnumerable<Book>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICacheService _cacheService;
        public GetBooksQueryHandler(IBookRepository bookRepository, ICacheService cacheService)
        {
            _bookRepository = bookRepository;
            _cacheService = cacheService;
        }

        public async Task<Result<IEnumerable<Book>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"GetBooksQuery";

            var Books = await _cacheService.GetOrCreateAsync(cacheKey, async () => await _bookRepository.GetAsync());

            return new Result<IEnumerable<Book>>(Books);
        }
    }
}
