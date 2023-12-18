namespace FileUploadApp.Contracts
{
    public interface IFileProcessor
    {
        Task<IProcessingResult> Process(Stream stream, string originalFileName, string paramFileName);
    }
}
