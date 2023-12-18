using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadApp.Contracts.FileSystem
{
    public interface IFileSystemManager
    {
        Task SaveFileToDiskAsync(string content, string fileName, string extension);
    }
}
