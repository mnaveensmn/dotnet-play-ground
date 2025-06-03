using book_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace book_api.Controllers;

[ApiController]
[Route("v1/books")]
public class BooksController(IBooksService booksService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetBooks()
    {
        return Ok(booksService.GetAllBooks());
    }
}