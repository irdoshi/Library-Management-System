
namespace LMS.API
    {
    using Azure.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Azure.KeyVault;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.AzureKeyVault;
    using Microsoft.Extensions.Hosting;

    public class Program
        {
        public static void Main(string[] args)
            {
            CreateHostBuilder(args).Build().Run();
            }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            
             .ConfigureAppConfiguration((context, config) =>
             {
                 var builtConfig = config.Build();
                 var vaultName = builtConfig["Vault"];
                 var keyVaultClient = new KeyVaultClient(
                     async (authority, resource, scope) =>
                     {
                         var credential = new DefaultAzureCredential(true);
                         var token = credential.GetToken(
                             new Azure.Core.TokenRequestContext(
                                 new[] { "https://vault.azure.net/.default" }));
                         return token.Token;
                     });
                 config.AddAzureKeyVault(
                     vaultName,
                     keyVaultClient,
                     new DefaultKeyVaultSecretManager());
             });
        }
    }
        
