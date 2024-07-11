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
    }
}