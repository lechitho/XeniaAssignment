using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class BookCannotCreatedException : Exception
    {
        public BookCannotCreatedException()
        { 
        }
        public BookCannotCreatedException(string? message) : base(message)
        {
        }
        public BookCannotCreatedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
