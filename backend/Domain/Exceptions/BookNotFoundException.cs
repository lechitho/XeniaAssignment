using Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(Guid id) : base($"Book with id {id} was not found") { }
    }
}
