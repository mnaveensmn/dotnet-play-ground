namespace book_api_consumer;

public class ApiClient
{
    private readonly Uri BaseUri;

    public ApiClient(Uri baseUri)
    {
        this.BaseUri = baseUri;
    }

    public async Task<HttpResponseMessage> GetAllBooks()
    {
        using (var client = new HttpClient { BaseAddress = BaseUri })
        {
            try
            {
                var response = await client.GetAsync($"/v1/books");
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem connecting to Products API.", ex);
            }
        }
    }
}