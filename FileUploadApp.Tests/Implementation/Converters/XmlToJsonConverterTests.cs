using FileUploadApp.Implementation.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadApp.Tests.Implementation.Converters
{
    public class XmlToJsonConverterTests
    {
        [Fact]
        public void Convert_ValidXml_MustReturnJson()
        {
            // Arrange
            var xmlConverter = new XmlToJsonConverter();
            var xml = "<Root><Element>Value</Element></Root>";
            using var xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xml));

            // Act
            var result = xmlConverter.Convert(xmlStream);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("Value", result);
        }

        [Fact]
        public void Convert_InvalidXml_MustThrowInvalidDataException()
        {
            // Arrange
            var xmlConverter = new XmlToJsonConverter();
            var invalidXml = "<Root><Element>Value</Element>"; 
            using var xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(invalidXml));

            // Act & Assert
            Assert.Throws<InvalidDataException>(() => xmlConverter.Convert(xmlStream));
        }
    }
}
