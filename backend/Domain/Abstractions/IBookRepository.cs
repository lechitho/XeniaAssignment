using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(Book book);
        Task<bool> UpdateAsync(Book book);
        Task RemoveAsync(Guid id);
    }
}
