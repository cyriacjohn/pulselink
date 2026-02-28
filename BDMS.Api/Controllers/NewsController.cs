using Microsoft.AspNetCore.Mvc;

namespace BDMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController: ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        
        public NewsController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            var apiKey = _config["NewsApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                return BadRequest("API Key not found");
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("User-Agent", "BDMS-App");
            var url = $"http://newsapi.org/v2/everything?q=blood donation OR blood bank&sortBy=publishedAt&apiKey={apiKey}";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
    }
}
