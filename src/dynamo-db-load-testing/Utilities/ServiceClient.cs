using System.Security.Authentication;

namespace dynamo_db_load_testing.Utilities;

public class ServiceClient
{
    private const string Url = $"https://qr-manager-service-perf.css.rxweb-dev.com/v1/qr-profiles?groupId=AUTOM23&shortCode=4W4M94";
    
    public async Task SendRequest()
    {
        var handler = new HttpClientHandler
        {
            SslProtocols = SslProtocols.Tls13 | SslProtocols.Tls12,
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
        };

        using var client = new HttpClient(handler);

        var response = await client.GetAsync(Url);
        var responseBody = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Response Body: " + responseBody);
        }
        else
        {
            Console.WriteLine("Error: " + responseBody);
        }
        Console.WriteLine();
    }
}