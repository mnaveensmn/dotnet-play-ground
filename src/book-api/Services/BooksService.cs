using book_api.Models;

namespace book_api.Services;

public class BooksService : IBooksService
{
    public List<Book> GetAllBooks()
    {
        return
        [
            new Book()
            {
                Id = "1",
                Name = "Book1",
                Author = "Author1"
            },

            new Book()
            {
                Id = "2",
                Name = "Book2",
                Author = "Author2"
            }
        ];
    }
}