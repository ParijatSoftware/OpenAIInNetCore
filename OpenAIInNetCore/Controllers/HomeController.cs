using InstantBedtimeStory.Models;
using InstantBedtimeStory.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InstantBedtimeStory.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOpenAIConnector _openAIConnector;

        public HomeController(IOpenAIConnector openAIConnector)
        {
            _openAIConnector = openAIConnector;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/api/getbedtimestory")]
        public async Task<IActionResult> GetStory(string topic)
        {
            try
            {
                await _openAIConnector.GetStoryAsync(topic);
                return Ok(new ApiResponse { Message = "Success" });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse { Message = e.Message });
            }

        }
    }
}
