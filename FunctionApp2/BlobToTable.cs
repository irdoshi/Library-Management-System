namespace LMS.AzureFunction
    {
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.Table;
    class BlobToTable : TableEntity
        {
        public int BookId { get; set; }
        public string FileURL { get; set; }
        [FunctionName("MetadataToTable")]
        [return: Table("metadataTable")]
        public static BlobToTable TableOutput([BlobTrigger("covers/{name}", Connection = "AzureWebJobsStorage")] CloudBlockBlob myBlob)
            {
            var metadata = myBlob.Metadata;
            return new BlobToTable
                {
                PartitionKey = metadata["BookId"],
                RowKey = Guid.NewGuid().ToString(),
                BookId = int.Parse(metadata["BookId"]),
                FileURL = myBlob.Uri.AbsoluteUri
                };
            }
        }
    }
