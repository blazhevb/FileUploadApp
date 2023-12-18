using FileUploadApp.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileProcessController : ControllerBase
    {
        private readonly ILogger<FileProcessController> _logger;
        private readonly IFileProcessor _fileProcessor;

        public FileProcessController(IFileProcessor fileProcessor, ILogger<FileProcessController> logger)
        {
            _fileProcessor = fileProcessor;
            _logger = logger;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file, string filename)
        {
            if(file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            IProcessingResult result;

            using (var stream = file.OpenReadStream())
            {
                stream.Seek(0, SeekOrigin.Begin);

                result = await _fileProcessor.Process(stream, file.FileName, filename);
            }
            
            return Ok(result);
        }
    }
}
