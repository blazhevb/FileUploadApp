using FileUploadApp.Contracts.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadApp.Contracts.Factory
{
    public interface IConverterFactory
    {
        IFileConverter CreateConverter(string sourceType, string targetType);
    }
}
