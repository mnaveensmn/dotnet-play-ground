namespace book_api_consumer;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var ApiClient = new ApiClient(new Uri("http://localhost:5091"));
        var response = ApiClient.GetAllBooks().GetAwaiter().GetResult();
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        Console.WriteLine($"Response.Code={response.StatusCode}, Response.Body={responseBody}\n\n");
    }
}