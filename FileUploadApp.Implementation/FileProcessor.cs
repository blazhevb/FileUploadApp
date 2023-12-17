using FileUploadApp.Contracts;
using FileUploadApp.Contracts.Converter;
using FileUploadApp.Contracts.Factory;
using FileUploadApp.Contracts.FileSystem;

namespace FileUploadApp.Implementation
{
    public class FileProcessor : IFileProcessor
    {
        private readonly IConverterFactory _converterFactory;
        private readonly IFileSystemManager _fileSystemManager;
        private const string DEFAULT_TARGET_FORMAT = "json";

        public FileProcessor(IConverterFactory converterFactory, IFileSystemManager fileSystemManager) 
        { 
            this._converterFactory = converterFactory;
            this._fileSystemManager = fileSystemManager;
        }

        public async Task<IProcessingResult> Process(Stream stream, string originalFileName, string paramFileName)
        {
            string inputFileExtension = Path.GetExtension(originalFileName);
            string outputFileExtension = DEFAULT_TARGET_FORMAT;
            string inputFileName = string.IsNullOrWhiteSpace(paramFileName) ? Path.GetFileNameWithoutExtension(originalFileName) : paramFileName;
            
            IFileConverter converter =  _converterFactory.CreateConverter(inputFileExtension, DEFAULT_TARGET_FORMAT);
            string content = converter.Convert(stream);
            await _fileSystemManager.SaveFileToDiskAsync(content, outputFileExtension);

            return null;
        }
    }
}
