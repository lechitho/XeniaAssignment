namespace BookManagement.API.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public double Price { get; set; }
    }
}
