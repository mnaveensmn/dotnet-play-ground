using book_api.Models;

namespace book_api.Services;

public interface IBooksService
{
    public List<Book> GetAllBooks();
}