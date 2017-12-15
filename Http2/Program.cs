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
                var request1 = new HttpRequestMessage(HttpMethod.Get, "https://www.google.com");
                request1.Version = new Version(2, 0);

                var request2 = new HttpRequestMessage(HttpMethod.Get, "https://www.google.com");
                request2.Version = new Version(2, 0);

                var task1 = httpClient.SendAsync(request1);
                var task2 = httpClient.SendAsync(request2);

                Task.WaitAll(task1, task2);

                var response1 = task1.Result;
                var response2 = task2.Result;

                Console.WriteLine($"Response 1 - Http Verion: {response1.Version}, Http Status Code: {response1.StatusCode}");
                Console.WriteLine($"Response 2 - Http Verion: {response2.Version}, Http Status Code: {response2.StatusCode}");
            }

            Console.ReadLine();
        }
    }
}
