using System;
using System.Net.Http;

namespace programm 
{
 class Program
    {
        static void Main(string[] args)
        {
            GetRequest("http://api.apixu.com/v1/current.json?key=b93ff2ec5f8c4396b9583203182801&q=Astana");



            Console.ReadKey();
        }

        async static void GetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage responce = awat client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        content.mycontent = await content.ReadAsStringAsync();
                        Console.WriteLine(mycontent);
                    }
                }
            }
        }

    }
}