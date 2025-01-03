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
                Id = "Id1",
                Name = "Book1",
                Author = "Book1 Author"
            },

            new Book()
            {
                Id = "Id2",
                Name = "Book2",
                Author = "Book2 Author"
            }
        ];
    }
}