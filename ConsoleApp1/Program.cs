//using System;
//using System.Net;
//using System.Collections.Specialized;

//namespace ConsoleApplication
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string url = "http://api.apixu.com/v1/current.json?key=b93ff2ec5f8c4396b9583203182801&q=Astana";

//            using (var webClient = new WebClient())
//            {
//                // Создаём коллекцию параметров
//                var pars = new NameValueCollection();

//                // Добавляем необходимые параметры в виде пар ключ, значение
//                pars.Add("format", "json");

//                // Посылаем параметры на сервер
//                // Может быть ответ в виде массива байт
//                var response = webClient.UploadValues(url, pars);

//                Console.WriteLine(response);
//            }



//            Console.ReadKey();
//        }
//    }
//}

using System;
using System.Collections.Generic;
//using System.Net.Header;
using System.Net.Http;

namespace programm2017
{
    class Program
    {
        static void Main(string[] args)
        {
            //PostRequest("https://posttestserver.com/post.php");
            GetRequest("http://api.apixu.com/v1/current.json?key=b93ff2ec5f8c4396b9583203182801&q=Astana");
            //GetRequest("http://www.google.com");


            Console.ReadKey();
        }

        async static void GetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        //content.mycontent = await content.ReadAsStringAsync();
                        Console.WriteLine(mycontent);
                    }

                }
            }
        }
    }
}

//        async static void PostRequest(string url)
//        {
//            IEnumerable<KeyVakuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
//            {
//                new KeyValuePair<string, string> ("query1","jamal");
//            new KeyValuePair<string, string>("query2", "hussain");

//        };

//        HttpContent q = new FormUrlEncodedContent();

//            using (HttpClient client = new HttpClient())
//            {
//                using (HttpResponseMessage response = await client.PostAsync(url,))
//                {
//                    using (HttpContent content = response.Content)
//                    {
//                        string mycontent = await content.ReadAsStringAsync();
//    //content.mycontent = await content.ReadAsStringAsync();

//    //HttpContentHeaders headers = content.Headers;


//    Console.WriteLine(headers);
//                    }
//                }
//            }
//        }

//    }
//}






//using System;
//using System.Net.Http;

//namespace programm
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            GetRequest("http://api.apixu.com/v1/current.json?key=b93ff2ec5f8c4396b9583203182801&q=Astana");



//            Console.ReadKey();
//        }

//        async static void GetRequest(string url)
//        {
//            using (HttpClient client = new HttpClient())
//            {
//                using (HttpResponseMessage responce = await client.GetAsync(url))
//                {
//                    using (HttpContent content = response.Content)
//                    {
//                        content.mycontent = await content.ReadAsStringAsync();
//                        Console.WriteLine(mycontent);
//                    }
//                }
//            }
//        }

//    }
//}

//using System;
//using System.Net;

//namespace ConsoleApplication
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            // Адрес ресурса, к которому выполняется запрос
//            string url = "http://api.apixu.com/v1/current.json?key=b93ff2ec5f8c4396b9583203182801&q=Astana";

//            // Создаём объект WebClient
//            using (var webClient = new WebClient())
//            {
//                // Выполняем запрос по адресу и получаем ответ в виде строки
//                var response = webClient.DownloadString(url);



//            }

//            Console.ReadKey();

//        }
//    }
//}