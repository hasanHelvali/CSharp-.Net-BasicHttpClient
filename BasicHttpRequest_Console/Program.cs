using Newtonsoft.Json;

namespace BasicHttpRequest_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string apiUrl = "https://jsonplaceholder.typicode.com/posts";

            var data = HttpHelper.GetDataFromApi<List<ResponseModel>>(apiUrl).Result;

            foreach (var post in data)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine(post.Id);
                Console.WriteLine(post.UserId);
                Console.WriteLine(post.Title);
                Console.WriteLine(post.Body);
                Console.WriteLine("---------------------------");
            }
            Console.Read();
        }
    }
    public class HttpHelper
    {
        public static async Task<T> GetDataFromApi<T>(string url)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }
    }

    public class ResponseModel
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
