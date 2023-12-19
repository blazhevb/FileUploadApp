using FileUploadApp.Contracts.FileSystem;
using Microsoft.Extensions.Options;

namespace FileUploadApp.Implementation.FileSystem
{
    public class FileSystemManager : IFileSystemManager
    {
        private readonly string _targetSaveDirectory;
        public FileSystemManager(IOptions<FileSettings> fileSettings) 
        {
            var workDir = Directory.GetCurrentDirectory();
            _targetSaveDirectory = Path.GetFullPath(Path.Combine(workDir, fileSettings.Value.RelativeSaveDirectory));
        }

        public async Task SaveFileToDiskAsync(string content, string fileName, string extension)
        {
            //Creates directory if not exists
            Directory.CreateDirectory(_targetSaveDirectory);

            string fullFileName = $"{fileName}.{extension}";
            string fullPath = Path.Combine(_targetSaveDirectory, fullFileName);

            if(File.Exists(fullPath))
            {
                throw new InvalidOperationException("A file with the same name already exists.");
            }

            try
            {
                await File.WriteAllTextAsync(fullPath, content);
            }
            catch 
            { 
                throw new InvalidOperationException($"Failed to save file: {fullFileName}");
            }          
        }
    }
}
