using FileUploadApp.Contracts.Converter;

namespace FileUploadApp.Contracts.Factory
{
    public interface IConverterFactory
    {
        IFileConverter CreateConverter(string sourceType, string targetType);
    }
}
