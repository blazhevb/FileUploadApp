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
            var result = new ProcessingResult();

            string inputFileExtension = Path.GetExtension(originalFileName).TrimStart('.');
            if(string.IsNullOrEmpty(inputFileExtension))
            {
                result.Success = false;
                result.ErrorMessage = "Invalid file extension";
                return result;
            }

            string outputFileExtension = DEFAULT_TARGET_FORMAT;
            string inputFileName = string.IsNullOrWhiteSpace(paramFileName) ? Path.GetFileNameWithoutExtension(originalFileName) : paramFileName;


            try
            {
                IFileConverter converter = _converterFactory.CreateConverter(inputFileExtension, DEFAULT_TARGET_FORMAT);
                
                string content = converter.Convert(stream);

                await _fileSystemManager.SaveFileToDiskAsync(content, inputFileName, outputFileExtension);

                result.Success = true;
            }
            catch(NotSupportedException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            catch (InvalidDataException ex)
            {   
                result.ErrorMessage = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                result.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error has occured. Please try again later.";
            }            

            return result;
        }
    }
}
