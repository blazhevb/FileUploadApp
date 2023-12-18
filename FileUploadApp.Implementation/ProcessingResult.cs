using FileUploadApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadApp.Implementation
{
    public class ProcessingResult : IProcessingResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
