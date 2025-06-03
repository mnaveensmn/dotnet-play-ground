using Microsoft.AspNetCore.Mvc;
using webapi_play_ground.Filters;
using webapi_play_ground.Models;
using webapi_play_ground.Services;

namespace webapi_play_ground.Controllers
{
    [ApiController]
    [ServiceFilter<TestAsyncActionFilterOne>]
    [Route("api/[controller]")]
    public class BookController(IBookService bookService,
        ITestService1 testService1,
        ITestService2 testService2,
        ITestService3 testService3) : ControllerBase
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
            Console.WriteLine(testService1.GetHashCode());
            Console.WriteLine(testService2.GetHashCode());
            Console.WriteLine(testService3.GetHashCode());
            
            Console.WriteLine("BookController: Request Received");
            return Ok(bookService.GetAllBooks());
        }
    }
}