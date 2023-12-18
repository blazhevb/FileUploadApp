using FileUploadApp.Contracts.Converter;
using FileUploadApp.Contracts.Factory;
using FileUploadApp.Contracts.FileSystem;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadApp.Tests.Implementation.FileProcessor
{
    public class FileProcessorTests
    {
        private readonly FileUploadApp.Implementation.FileProcessor _fileProcessor;
        private readonly Mock<IConverterFactory> _mockConverterFactory;
        private readonly Mock<IFileSystemManager> _mockFileSystemManager;
        private readonly Mock<IFileConverter> _mockFileConverter;

        public FileProcessorTests()
        {
            _mockConverterFactory = new Mock<IConverterFactory>();
            _mockFileSystemManager = new Mock<IFileSystemManager>();
            _mockFileConverter = new Mock<IFileConverter>();
            _fileProcessor = new FileUploadApp.Implementation.FileProcessor(_mockConverterFactory.Object, _mockFileSystemManager.Object);
        }

        [Fact]
        public async Task Process_ValidFile_MustSaveSuccessfully()
        {
            // Arrange
            var stream = new MemoryStream();
            string originalFileName = "test.xml";
            string paramFileName = "output";

            _mockConverterFactory.Setup(f => f.CreateConverter(It.IsAny<string>(), It.IsAny<string>()))
                                 .Returns(_mockFileConverter.Object);
            _mockFileConverter.Setup(c => c.Convert(It.IsAny<Stream>())).Returns("json content");

            // Act
            var result = await _fileProcessor.Process(stream, originalFileName, paramFileName);

            // Assert
            Assert.True(result.Success);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public async Task Process_UnsupportedFormat_MustReturnError()
        {
            // Arrange
            var mockStream = new MemoryStream();
            string originalFileName = "test.1234";
            string paramFileName = "output";

            _mockConverterFactory.Setup(f => f.CreateConverter(It.IsAny<string>(), It.IsAny<string>()))
                                 .Throws(new NotSupportedException());

            // Act
            var result = await _fileProcessor.Process(mockStream, originalFileName, paramFileName);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task Process_InvalidData_MustThrowInvalidDataException()
        {
            // Arrange
            var mockStream = new MemoryStream();
            string originalFileName = "test.xml";
            string paramFileName = "output";

            _mockConverterFactory.Setup(f => f.CreateConverter(It.IsAny<string>(), It.IsAny<string>()))
                                 .Returns(_mockFileConverter.Object);
            _mockFileConverter.Setup(c => c.Convert(It.IsAny<Stream>()))
                              .Throws(new InvalidDataException());

            // Act
            var result = await _fileProcessor.Process(mockStream, originalFileName, paramFileName);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task Process_FileAlreadyExists_MustThrowInvalidOperationException()
        {
            // Arrange
            var mockStream = new MemoryStream();
            string originalFileName = "test.xml";
            string paramFileName = "output";

            _mockConverterFactory.Setup(f => f.CreateConverter(It.IsAny<string>(), It.IsAny<string>()))
                                 .Returns(_mockFileConverter.Object);
            _mockFileConverter.Setup(c => c.Convert(It.IsAny<Stream>())).Returns("json content");
            _mockFileSystemManager.Setup(f => f.SaveFileToDiskAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                  .Throws(new InvalidOperationException());

            // Act
            var result = await _fileProcessor.Process(mockStream, originalFileName, paramFileName);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task Process_GeneralException_MustReturnGenericError()
        {
            // Arrange
            var mockStream = new MemoryStream();
            string originalFileName = "test.xml";
            string paramFileName = "output";

            _mockConverterFactory.Setup(f => f.CreateConverter(It.IsAny<string>(), It.IsAny<string>()))
                                 .Returns(_mockFileConverter.Object);
            _mockFileConverter.Setup(c => c.Convert(It.IsAny<Stream>()))
                              .Throws(new Exception());

            // Act
            var result = await _fileProcessor.Process(mockStream, originalFileName, paramFileName);

            // Assert
            Assert.False(result.Success);
        }
    }
}
