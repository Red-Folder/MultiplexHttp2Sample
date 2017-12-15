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

                var wrapper1 = new RequestWrapper(httpClient, request1, 1);
                var thread1 = new Thread(wrapper1.SendRequest);

                var wrapper2 = new RequestWrapper(httpClient, request2, 2);
                var thread2 = new Thread(wrapper2.SendRequest);

                thread1.Start();
                thread2.Start();

                // Move inside the using to avoid HttpClient being disposed before threads complete
                Console.ReadLine();
            }

        }

        private class RequestWrapper
        {
            private readonly HttpClient _client;
            private readonly HttpRequestMessage _request;
            private readonly int _id;

            public RequestWrapper(HttpClient client, HttpRequestMessage request, int id)
            {
                _client = client;
                _request = request;
                _id = id;
            }

            public void SendRequest()
            {
                var response = _client.SendAsync(_request).Result;
                Console.WriteLine($"Response {_id} - Http Version: {response.Version}, Http Status Code: {response.StatusCode}");
            }
        }
    }
}
