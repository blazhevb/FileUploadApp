﻿using FileUploadApp.Contracts.Converter;
using Newtonsoft.Json;
using System.Xml;

namespace FileUploadApp.Implementation.Converters
{
    public class XmlToJsonConverter : IFileConverter
    {

        public string Convert(Stream fileStream)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fileStream);

            string json = JsonConvert.SerializeXmlNode(xmlDoc);

            return json;
        }
    }
}
