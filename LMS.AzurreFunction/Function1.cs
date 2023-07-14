using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace LMS.AzurreFunction
    {
    public static class Function1
        {
        [FunctionName("Function1")]
        public static void Run([BlobTrigger("covers/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
            [Blob("myoutput/{name}", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream myOutput, string name, ILogger log)
            {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            myBlob.CopyTo(myOutput);
            }
        }
    }
