using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class Book: Entity
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public DateTime PublishedDate { get; private set; }
        public double Price { get; private set; }

        public Book(Guid id, string title, string author, DateTime publishedDate, double price) :base(id)
        {
            Title = title;
            Author = author;
            PublishedDate = publishedDate;
            Price = price;
        }

        public static Book Create(Guid id, string title, string author, DateTime? publishedDate, double price)
        {
            if (string.IsNullOrEmpty(title) || publishedDate is null || price < 0)
                throw new BookCannotCreatedException();

            return new Book(id, title, author, publishedDate.Value, price);
        }

    }
}
