using Microsoft.AspNetCore.Mvc;
using QnAWithOpenAIEmbedding.Models;
using QnAWithOpenAIEmbedding.Services;
using System.Net;

namespace QnAWithOpenAIEmbedding.Controllers
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

        [HttpPost("/api/ask")]
        public async Task<IActionResult> GetGptResponse([FromBody] UserInputModel model)
        {
            try
            {
                await _openAIConnector.AskAsync(model.Query);
                return Ok(new ApiResponse { Message = "Success" });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse { Message = e.Message });
            }

        }
    }
}
