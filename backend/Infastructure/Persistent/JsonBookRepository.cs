using Application.Abstractions.Caching;
using Domain.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infastructure.Persistent
{
    public class JsonBookRepository: IBookRepository
    {
        private List<Book> _bookData;
        private ICacheService _cacheService;

        private readonly string _jsonFilePath;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public JsonBookRepository(RepositoryOptions options, ICacheService cacheService, CacheOptions caching)
        {
            _cacheService = cacheService;
            _jsonFilePath = options.JsonFilePath;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

            if (!File.Exists(_jsonFilePath))
            {
                File.WriteAllText(_jsonFilePath, "[]");
            }
            ReadBooksFromFile();
        }
        public async Task<IEnumerable<Book>> GetAsync() => _bookData;

        public async Task<Book?> GetByIdAsync(Guid id) => _bookData.Where(e => e.Id == id).SingleOrDefault();
        public async Task<Guid> AddAsync(Book book)
        {
            _bookData.Add(book);

            await WriteBooksToFile();
            return book.Id;
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            var existingBookIndex = _bookData.FindIndex(e => e.Id == book.Id);

            if (existingBookIndex != -1)
            {
                _bookData[existingBookIndex] = book;
                await WriteBooksToFile();
                return true;
            }
            return false;
        }

        public async Task RemoveAsync(Guid id)
        {
            var bookToRemove = _bookData.FirstOrDefault(e => e.Id == id);

            if (bookToRemove != null)
            {
                _bookData.Remove(bookToRemove);
                await WriteBooksToFile();
            }
        }
        private void ReadBooksFromFile()
        {
            var cacheKey = $"jsonBooks";

            var readDataFromFileFunc = async () =>
            {
                var books = new List<Book>();

                var json = await File.ReadAllTextAsync(_jsonFilePath);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    books = JsonSerializer.Deserialize<IEnumerable<JsonBook>>(json, _jsonSerializerOptions)?.Select(e => Book.Create(e.Id, e.Title, e.Author, e.PublishDate, e.Price)).ToList();
                }

                return books;
            };

            _bookData = _cacheService.GetOrCreateAsync(cacheKey, readDataFromFileFunc).Result;

        }
        private async Task WriteBooksToFile()
        {
            using var fileStream = File.Create(_jsonFilePath);
            await JsonSerializer.SerializeAsync(fileStream, _bookData, _jsonSerializerOptions);
        }
    }
}
