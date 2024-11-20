using webapi_play_ground.Models;

namespace webapi_play_ground.Services;

public interface IBookService
{
    public List<Book> GetAllBooks();
}