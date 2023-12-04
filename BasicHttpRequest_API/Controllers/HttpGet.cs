using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace BasicHttpRequest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpGet : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string apiUrl = "https://jsonplaceholder.typicode.com/posts";
            List<ResponseModel> data = HttpHelper.GetDataFromApi<List<ResponseModel>>(apiUrl).Result;
            return Ok(data);
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
