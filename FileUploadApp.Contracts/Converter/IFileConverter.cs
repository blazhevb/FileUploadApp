namespace FileUploadApp.Contracts.Converter
{
    public interface IFileConverter
    {
        string Convert(Stream fileStream);
    }
}
