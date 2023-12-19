using FileUploadApp.API.Controllers;
using FileUploadApp.Contracts;
using FileUploadApp.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

public class FileProcessControllerTests
{
    private readonly Mock<IFileProcessor> _mockFileProcessor;
    private readonly Mock<ILogger<FileProcessController>> _mockLogger;
    private readonly FileProcessController _controller;

    public FileProcessControllerTests()
    {
        _mockFileProcessor = new Mock<IFileProcessor>();
        _mockLogger = new Mock<ILogger<FileProcessController>>();
        _controller = new FileProcessController(_mockFileProcessor.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Upload_NullFile_MustReturnOkWithErrorMessage()
    {
        // Act
        var result = await _controller.Upload(null, "testfile");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var processingResult = Assert.IsType<ProcessingResult>(okResult.Value);
        Assert.False(processingResult.Success);
        Assert.False(string.IsNullOrEmpty(processingResult.ErrorMessage));
    }

    [Fact]
    public async Task Upload_ZeroLengthFile_MustReturnOkWithErrorMessage()
    {
        // Arrange
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(0);

        // Act
        var result = await _controller.Upload(mockFile.Object, "testfile");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var processingResult = Assert.IsType<ProcessingResult>(okResult.Value);
        Assert.False(processingResult.Success);
        Assert.False(string.IsNullOrEmpty(processingResult.ErrorMessage));
    }

    [Fact]
    public async Task Upload_ValidFile_MustReturnOk()
    {
        // Arrange
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(100);
        mockFile.Setup(f => f.OpenReadStream()).Returns(new MemoryStream());
        var processingResult = Mock.Of<IProcessingResult>();
        _mockFileProcessor.Setup(p => p.Process(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(processingResult);

        // Act
        var result = await _controller.Upload(mockFile.Object, "testfile");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(processingResult, okResult.Value);
    }
}
