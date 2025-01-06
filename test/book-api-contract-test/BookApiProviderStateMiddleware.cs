using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace book_api_contract_test;

public class BookApiProviderStateMiddleware
{
    private const string ConsumerName = "Books-UI";
    private readonly RequestDelegate _next;
    private readonly IDictionary<string, Action> _providerStates;

    public BookApiProviderStateMiddleware(RequestDelegate next)
    {
        _next = next;
        _providerStates = new Dictionary<string, Action>
        {
            {
                "There are books in the book table",
                AddData
            }
        };
    }


    private void AddData()
    {
    }


    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine("Endpoint hit");
        if (context.Request.Path.Value == "/provider-states")
        {
            HandleProviderStatesRequest(context);
            await context.Response.WriteAsync(string.Empty);
        }
        else
        {
            await _next(context);
        }
    }


    private void HandleProviderStatesRequest(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.OK;

        if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() &&
            context.Request.Body != null)
        {
            var jsonRequestBody = string.Empty;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                jsonRequestBody = reader.ReadToEnd();
            }

            var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

            if (providerState != null && !string.IsNullOrEmpty(providerState.State) &&
                providerState.Consumer == ConsumerName)
                _providerStates[providerState.State].Invoke();
        }
    }
}