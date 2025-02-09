using book_api_contract_test.Helpers;
using Microsoft.AspNetCore;
using PactNet.Infrastructure.Outputters;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace book_api_contract_test;

public class BooksApiTest(ITestOutputHelper output)
{
    private string _pactServiceUri = "http://localhost:9226";
    private ITestOutputHelper _outputHelper { get; } = output;

    [Fact]
    public void EnsureProviderApiHonoursPactWithConsumer()
    {
        var config = new PactVerifierConfig
        {
            LogLevel = PactNet.PactLogLevel.Debug,
            Outputters = new List<IOutput>
            {
                new XUnitOutput(_outputHelper)
            }
        };

        using (var _webHost = WebHost.CreateDefaultBuilder().UseStartup<TestStartup>().UseUrls(_pactServiceUri).Build())
        {
            _webHost.Start();

            IPactVerifier pactVerifier = new PactVerifier(config);
            var pactFile =
                new FileInfo(Path.Join("..", "..", "..", "..", "..", "pacts", "ApiClient-BookApi.json"));

            pactVerifier.ServiceProvider("BookApi", new Uri(_pactServiceUri))
                .WithFileSource(pactFile)
                .Verify();
        }
    }
}