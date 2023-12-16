using FileUploadApp.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadApp.Implementation.FileSystem
{
    public class FileSystemManager : IFileSystemManager
    {
        public async Task SaveFileToDiskAsync(string content)
        {

            await File.WriteAllTextAsync("", content, Encoding.UTF8);
        }
    }
}
