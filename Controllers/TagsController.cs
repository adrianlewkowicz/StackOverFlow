using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using StackOverflow.Entity;

namespace StackOverflow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController: ControllerBase
	{
        private readonly ILogger<TagsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public TagsController(ILogger<TagsController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var parameters = new Dictionary<string, string>
            {
                ["site"] = "stackoverflow",
                ["pagesize"] = "1000"
            };

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(QueryHelpers.AddQueryString("https://api.stackexchange.com/2.3/tags?order=desc&sort=popular&site=stackoverflow&pagesize=100", parameters));

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(json);
            var tags = result["items"].Select(item => item["name"].ToString());

            return tags;
        }
    }
}


