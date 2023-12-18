using FileUploadApp.Implementation.Converters;
using FileUploadApp.Implementation.Factory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadApp.Tests.Implementation.Factory
{
    public class ConverterFactoryTests
    {
        [Fact]
        public void CreateConverter_SupportedTypes_MustReturnConverter()
        {
            // Arrange
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(sp => sp.GetService(typeof(XmlToJsonConverter)))
                               .Returns(new XmlToJsonConverter());
            var factory = new ConverterFactory(mockServiceProvider.Object);

            // Act
            var converter = factory.CreateConverter("xml", "json");

            // Assert
            Assert.NotNull(converter);
            Assert.IsType<XmlToJsonConverter>(converter);
        }

        [Fact]
        public void CreateConverter_UnsupportedTypes_MustThrowNotSupportedException()
        {
            // Arrange
            var mockServiceProvider = new Mock<IServiceProvider>();
            var factory = new ConverterFactory(mockServiceProvider.Object);

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => factory.CreateConverter("unsupported", "type"));
        }
    }
}
