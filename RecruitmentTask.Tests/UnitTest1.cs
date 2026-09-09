using Moq;
using RecruitmentTask.Client;
using RecruitmentTask.File;
using RecruitmentTask.Model;
using RecruitmentTask.Service;

namespace RecruitmentTask.Tests;

public class UnitTest1
{
    [Fact]
    public async Task TestApplicationService()
    {
        // Arrange
        CatFact expectedFact = new("Test fact", 9);

        Mock<IApiClient> apiClientMock = new();
        apiClientMock.Setup(x => x.GetDataAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectedFact);
        IApiClient apiClient = apiClientMock.Object;

        Mock<IFileWriter> fileWriterMock = new();
        IFileWriter fileWriter = fileWriterMock.Object;

        ApplicationService applicationService = new(apiClient, fileWriter);

        // Act
        await applicationService.ExecuteAsync(CancellationToken.None);

        // Assert
        fileWriterMock.Verify(x => x.AppendAsync(expectedFact, It.IsAny<CancellationToken>()), Times.Once);
    }
}
