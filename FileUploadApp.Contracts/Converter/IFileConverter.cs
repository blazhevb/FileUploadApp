﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadApp.Contracts.Converter
{
    public interface IFileConverter
    {
        string Convert(Stream fileStream);
    }
}
