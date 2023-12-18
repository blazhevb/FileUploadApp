using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadApp.Contracts
{
    public interface IProcessingResult
    {
        bool Success { get; set; }
        string ErrorMessage { get; set; }
    }
}
