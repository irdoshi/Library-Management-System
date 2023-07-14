namespace LMS.AzureStorage
    {
    using Azure;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using LMS.AzureStorage.Interface;
    using LMS.Common.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    public class BlobRepository : IBlobRepository
        {
        IConfiguration config;
        //string containerName;

        public BlobRepository(IConfiguration configuration)
            {
            config = configuration;
           // containerName = config["AZ_Blob"];
            }


        public async Task AddCover(IFormFile file, Book book)
            {
            var containerClient = new BlobContainerClient(
                connectionString: "DefaultEndpointsProtocol=https;AccountName=lmsdeepdivestorage;AccountKey=DqUSmDLEd5yk/3dtzCG03VySbTk6JU7JzdNyMi1EfA6GnIa/pe+BIolyxuipx41XhAHvDdxIXm6GcuIpamJ14w==;EndpointSuffix=core.windows.net",
                blobContainerName: "covers"); 

            string blobName = book.BookId + "_cover." + file.ContentType.Substring(file.ContentType.IndexOf('/') + 1);

            try
                {
                var blobClient = containerClient.GetBlobClient(blobName);

                await blobClient.UploadAsync(
                    file.OpenReadStream(),
                    new BlobHttpHeaders
                        {
                        ContentType = file.ContentType,
                        CacheControl = "public"
                        });

                IDictionary<string, string> metadata = new Dictionary<string, string>
                    {
                    ["BookId"] = book.BookId.ToString(),
                    ["Title"] = book.Title.ToString(),
                    ["Author"] = book.Author.ToString(),
                    ["Genre"] = book.Genre.ToString(),
                    ["Price"] = book.Price.ToString(),
                    ["UploaderID"] = "1"
                    };

                await blobClient.SetMetadataAsync(metadata);
                }
            catch (RequestFailedException e)
                {
                Debug.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
                Debug.WriteLine(e.Message);
                }
            }

       
        }
}
