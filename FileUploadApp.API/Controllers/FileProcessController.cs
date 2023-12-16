using Microsoft.AspNetCore.Mvc;

namespace FileUploadApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileProcessController : ControllerBase
    {
        private readonly ILogger<FileProcessController> _logger;

        public FileProcessController(ILogger<FileProcessController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
