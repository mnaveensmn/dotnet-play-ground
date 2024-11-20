using Microsoft.Extensions.Time.Testing;
using webapi_play_ground.Services;

namespace webapi_play_ground_test.Services;

public class BookServiceTest
{
    private readonly FakeTimeProvider _fakeTimeProvider;
    private readonly BookService _bookService;

    public BookServiceTest()
    {
        _fakeTimeProvider = new FakeTimeProvider();
        _bookService = new BookService(_fakeTimeProvider);
    }

    [Fact]
    public void ShouldReturnAllBooks()
    {
        var createdAt = DateTime.UtcNow;
        var createdAtOffset = DateTime.SpecifyKind(createdAt, DateTimeKind.Utc);
        _fakeTimeProvider.SetUtcNow(createdAtOffset);
        var result = _bookService.GetAllBooks();

        var book = result.First();
        Assert.Equivalent(createdAt.AddHours(1), book.CreatedAt);
    }
}