using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using UploadFile.Interfaces;

namespace UploadFile.Service;

public class UploadService : IBlobStorage
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<UploadService> _logger;
    private readonly string blobContainerName = "reenbitcontainer";
    private readonly string blobStorageconnection = string.Empty;


    public UploadService(IConfiguration configuration, ILogger<UploadService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        blobStorageconnection = _configuration.GetConnectionString("AzureBlobConnectionString");
    }
    public async Task<string> UploadFilesAsync(string? filename, string? contecntType, Stream fileStream)
    {
        try
        {
            var container = new BlobContainerClient(blobStorageconnection, blobContainerName);
            var filteredBlobs = container.GetBlobs()
                .Where(blob => blob.Properties.ContentType == "application/doc");

            var createResponse = await container.CreateIfNotExistsAsync();
            if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                await container.SetAccessPolicyAsync(PublicAccessType.Blob);
            var blob = container.GetBlobClient(filename);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contecntType });
            var urlString = blob.Uri.ToString();
            return urlString;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            throw;
        }
    }
}