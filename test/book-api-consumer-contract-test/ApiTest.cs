using PactNet;
using Xunit.Abstractions;
using System.Net;
using PactNet.Matchers;
using book_api_consumer;

namespace book_api_consumer_contract_test;

public class ApiTest
{
    private IPactBuilderV3 pact;
    private readonly ApiClient ApiClient;
    private readonly int port = 9000;
    private readonly List<object> books;

    public ApiTest(ITestOutputHelper output)
    {
        books = new List<object>()
        {
            new { id = "1", name = "Book1", author = "Author1" },
            new { id = "2", name = "Book2", author = "Author2" }
        };

        var config = new PactConfig
        {
            PactDir = Path.Join("..", "..", "..", "..", "..", "pacts"),
            Outputters = new[] { new XUnitOutput(output) },
            LogLevel = PactLogLevel.Debug
        };

        pact = Pact.V3("ApiClient", "BookApi", config).WithHttpInteractions(port);
        ApiClient = new ApiClient(new System.Uri($"http://localhost:{port}"));
    }

    [Fact]
    public async Task GetAllProducts()
    {
        // Arrange
        pact.UponReceiving("A valid request for all books")
            .Given("books exist")
            .WithRequest(HttpMethod.Get, "/v1/books")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithHeader("Content-Type", "application/json; charset=utf-8")
            .WithJsonBody(new TypeMatcher(books));

        await pact.VerifyAsync(async ctx =>
        {
            var response = await ApiClient.GetAllBooks();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        });
    }
}