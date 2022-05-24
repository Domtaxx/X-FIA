using Azure.Storage.Blobs;
using REST_API_XFIA.Modules.Service;

namespace REST_API_XFIA.Modules.Service
{
    public class StorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        public StorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        public string Upload(IFormFile formFile)
        {
            var containerName = _configuration.GetSection("Storage:ContainerName").Value;

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(formFile.FileName);

            using var stream = formFile.OpenReadStream();
            blobClient.Upload(stream, true);
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
