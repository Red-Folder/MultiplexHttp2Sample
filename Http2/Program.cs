using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Http2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                // Setup first connection
                var request0 = new HttpRequestMessage(HttpMethod.Get, "https://www.google.com");
                request0.Version = new Version(2, 0);

                var task0 = httpClient.SendAsync(request0);
                var response0 = task0.Result;

                Console.WriteLine($"Response 0 - Http Version: {response0.Version}, Http Status Code: {response0.StatusCode}");

                // Now send the multiplexed requests
                var request1 = new HttpRequestMessage(HttpMethod.Get, "https://www.google.com");
                request1.Version = new Version(2, 0);

                var request2 = new HttpRequestMessage(HttpMethod.Get, "https://www.google.com");
                request2.Version = new Version(2, 0);

                var task1 = httpClient.SendAsync(request1);
                var task2 = httpClient.SendAsync(request2);

                Task.WaitAll(task1, task2);

                var response1 = task1.Result;
                var response2 = task2.Result;

                Console.WriteLine($"Response 1 - Http Version: {response1.Version}, Http Status Code: {response1.StatusCode}");
                Console.WriteLine($"Response 2 - Http Version: {response2.Version}, Http Status Code: {response2.StatusCode}");
            }

            Console.ReadLine();
        }
    }
}
