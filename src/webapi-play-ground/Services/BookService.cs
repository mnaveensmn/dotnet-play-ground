using webapi_play_ground.Models;

namespace webapi_play_ground.Services;

public class BookService(TimeProvider timeProvider) : IBookService
{
    public List<Book> GetAllBooks()
    {
        List<Book> books = [];

        for (var i = 0; i < 10; i++)
        {
            books.Add(new Book()
            {
                Id = Guid.NewGuid().ToString(),
                Title = $"Book Title {i}",
                BookType = $"Book Type {i % 3}",
                CreatedAt = timeProvider.GetUtcNow().UtcDateTime.AddHours(1),
                UpdatedAt = DateTime.UtcNow
            });
        }

        return books;
    }
}