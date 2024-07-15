using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_exploration.Basics
{
    public class Hello
    {
        public static async Task GetPageLengthAsync(string endpoint)
        {
            var client = new HttpClient();
            var uri = new Uri(endpoint);
            byte[] content = await client.GetByteArrayAsync(uri);
            Console.WriteLine(content.Length);
        }

        public static async Task<string> GetPageContentAsString(string endpoint)
        {
            try
            {
                var client = new HttpClient();
                using HttpResponseMessage response = await client.GetAsync("http://www.contoso.com/");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }

        }
    }
}