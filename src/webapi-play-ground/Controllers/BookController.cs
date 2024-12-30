using Microsoft.AspNetCore.Mvc;
using webapi_play_ground.Models;
using webapi_play_ground.Services;

namespace webapi_play_ground.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController(IBookService bookService) : ControllerBase
    {
        [HttpPost]
        public OkObjectResult SaveBook([FromBody] Book book)
        {
            Console.WriteLine(book.BookType);
            return Ok(book);
        }

        [HttpGet]
        public OkObjectResult GetBooks()
        {
            Console.WriteLine("BookController: Request Received");
            return Ok(bookService.GetAllBooks());
        }
    }
}