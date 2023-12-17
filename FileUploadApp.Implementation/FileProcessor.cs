using FileUploadApp.Contracts;

namespace FileUploadApp.Implementation
{
    public class FileProcessor : IFileProcessor
    {
        public IProcessingResult Process(Stream stream, string originalFileName, string paramFileName)
        {
            throw new NotImplementedException();
        }
    }
}
