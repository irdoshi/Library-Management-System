namespace LMS.AzureFunctions
    {
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.Table;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /*    public class BookBlobToTable : TableEntity
            {
            public int BookId { get; set; }
            public string FileURL { get; set; }

            [FunctionName("BookBlobToTable")]
            [return: Table("metadatatable")]
            public static BookBlobToTable TableOutput([BlobTrigger("covers/{name}", Connection = "AzureWebJobsStorage")] CloudBlockBlob myBlob)
                {
                var metadata = myBlob.Metadata;
                return new BookBlobToTable
                    {
                    PartitionKey = metadata["BookId"],// doubt
                    RowKey = Guid.NewGuid().ToString(),
                    BookId = int.Parse(metadata["BookId"]),           
                    FileURL = myBlob.Uri.AbsoluteUri
                    };
                }
            }
        }*/

    public static class TableInsert
        {
        public class BookDetails
            {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public string URL { get; set; }

            }

        [FunctionName("TableInsert")]
        [return: Table("metadatatable")]
        public static BookDetails Run([BlobTrigger("covers/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name,
            Uri uri, IDictionary<string, string> metaData, ILogger log)
            {

            log.LogInformation($@"
                blobName      {name}
                uri           {uri}
                metaData      {metaData.Count}");

            return new BookDetails { PartitionKey = "BookDetails", RowKey = Guid.NewGuid().ToString(), URL = uri.ToString() };
            }
        }
    }
    
