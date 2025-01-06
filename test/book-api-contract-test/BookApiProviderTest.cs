using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PactNet.Infrastructure.Outputters;
using PactNet.Output.Xunit;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace book_api_contract_test;

public class BookApiProviderTest : IDisposable
{
    private ITestOutputHelper OutputHelper { get; }
    private readonly IHost _server;
    private Uri ServerUri { get; }
    
    public BookApiProviderTest(ITestOutputHelper output)
    {
        OutputHelper = output;

        ServerUri = new Uri("http://localhost:9226");
        _server = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseEnvironment("test");
                webBuilder.UseUrls(ServerUri.ToString());
                webBuilder.UseStartup<TestStartup>();
            }).Build();
        _server.Start();
    }
    
    [Fact]
    public async Task ShouldRunContractTest()
    {
        var config = new PactVerifierConfig
        {
            Outputters = new List<IOutput>
            {
                new XunitOutput(OutputHelper)
            },
            LogLevel = PactNet.PactLogLevel.Debug
        };

        //Act / Assert
        IPactVerifier pactVerifier = new PactVerifier(config);
        var pactFile = new FileInfo(Path.Join("../../../UI_Pact.json"));

        pactVerifier
            .ServiceProvider("BooksApi", ServerUri)
            .WithFileSource(pactFile)
            .WithProviderStateUrl(new Uri(ServerUri, "/provider-states"))
            .Verify();
    }

    private async Task TestSetup()
    {
        using var httpClient = new HttpClient();
        var url = "http://127.0.0.1:9226/v1/books";

        try
        {
            var response = await httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode(); 
            var responseData = await response.Content.ReadAsStringAsync(); 
            OutputHelper.WriteLine(responseData);
        }
        catch (HttpRequestException ex)
        {
            OutputHelper.WriteLine($"Request error: {ex.Message}");
        }
    }
    
    

    
    #region IDisposable Support

    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _server.StopAsync().GetAwaiter().GetResult();
                _server.Dispose();
            }

            disposedValue = true;
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
    }

    #endregion
}