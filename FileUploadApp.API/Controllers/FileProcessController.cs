using FileUploadApp.Contracts;
using FileUploadApp.Implementation;
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
        public async Task<IActionResult> Upload(IFormFile file, [FromForm] string filename = null)
        {
            IProcessingResult result = new ProcessingResult();
            
            if(file == null || file.Length == 0)
            {
                result.Success = false;
                result.ErrorMessage = "No file uploaded or empty file.";
                return Ok(result);
            }

            using (var stream = file.OpenReadStream())
            {
                stream.Seek(0, SeekOrigin.Begin);

                result = await _fileProcessor.Process(stream, file.FileName, filename);
            }
            
            return Ok(result);
        }
    }
}
