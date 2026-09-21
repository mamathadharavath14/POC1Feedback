using System.Text;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;

namespace POC1Feedback.Logic.Services;

public class BlobStorageService
{
    private readonly BlobContainerClient _containerClient;

    public BlobStorageService(IConfiguration configuration)
    {
        var connectionString =
            configuration["BlobStorageConnection"];

        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

        _containerClient =
            blobServiceClient.GetBlobContainerClient(
                "feedback-container");
    }

    public async Task UploadFeedbackAsync(string fileName, string content)
    {
        var blobClient = _containerClient.GetBlobClient(fileName);

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        await blobClient.UploadAsync( stream,overwrite: true);
    }
}