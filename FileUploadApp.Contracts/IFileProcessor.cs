using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadApp.Contracts
{
    public interface IFileProcessor
    {
        Task<IProcessingResult> Process(Stream stream, string originalFileName, string paramFileName);
    }
}
