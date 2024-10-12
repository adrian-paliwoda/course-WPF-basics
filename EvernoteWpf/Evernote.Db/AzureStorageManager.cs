using System;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;

namespace Evernote.Db
{
    public class AzureStorageManager
    {
        public static async Task<string> Update(string filePath, string fileName)
        {
            string connectionString =
                "DefaultEndpointsProtocol=https;AccountName=evernotestoragelpa;AccountKey=Fzfxxeh0QrgMr+5y+pCOYg+4WKjn5jH1J+hGMyBQV5zChWCo6caPt083dTcWMsx75fQLa+ZEeem4lKai1eBZFA==;EndpointSuffix=core.windows.net";

            string containerName = "notes";

            var container = new BlobContainerClient(connectionString, containerName);
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlobClient(fileName);
            await blob.UploadAsync(filePath);

            return $"https://evernotestoragelpa.blob.core.windows.net/notes/{fileName}";
        }
        
        public static async Task<Response> Download(string fileLocation, string downloadPath)
        {
            return await new BlobClient(new Uri(fileLocation)).DownloadToAsync(downloadPath);
        }
    }
}