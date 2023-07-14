
using LMS.Common.Models;
using LMS.Common.ViewModels;
using LMS.WebApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.Extensions.Azure;
using Azure.Storage.Queues;
using Azure.Storage.Blobs;
using Azure.Core.Extensions;
using System;
using StackExchange.Redis;

namespace LMS.WebApplication
    {
    public class Startup
        {
        public Startup(IConfiguration configuration)
            {
            Configuration = configuration;
            }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
            {
            services.AddDistributedMemoryCache();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.HandleSameSiteCookieCompatibility();
            });

            services.AddOptions();

            services.AddMicrosoftIdentityWebAppAuthentication(Configuration)
                    .EnableTokenAcquisitionToCallDownstreamApi(new string[] { Configuration["LMS:Scope"] })
                    .AddInMemoryTokenCaches();

           
            // Add APIs
            services.AddHttpClient<IHttpClientService<Library>, HttpClientService<Library>>();
            services.AddHttpClient<IHttpClientService<User>, HttpClientService<User>>();
            services.AddHttpClient<IHttpClientService<ViewBookAvailable>, HttpClientService<ViewBookAvailable>>();
            services.AddHttpClient<IHttpClientService<ViewBookWithDate>, HttpClientService<ViewBookWithDate>>();
            services.AddHttpClient<IHttpClientService<Book>, HttpClientService<Book>>();

            services.AddControllersWithViews(options =>
             {
                 var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
                 options.Filters.Add(new AuthorizeFilter(policy));
             }).AddMicrosoftIdentityUI();

            services.AddRazorPages();
            services.AddAuthorization(options => {
                options.AddPolicy("Administrator", policyBuilder => policyBuilder.RequireClaim("groups", Configuration.GetValue<string>("AzureSecurityGroup:AdminObjectId")));
            });
            services.AddAuthorization(options => {
                options.AddPolicy("Students", policyBuilder => policyBuilder.RequireClaim("groups", Configuration.GetValue<string>("AzureSecurityGroup:StudentsObjectId")));
            });
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);
            services.AddApplicationInsightsTelemetry(o =>
            {
                o.InstrumentationKey = "845425ed-5bac-4c74-bcf2-2a9c4cfe40a1";
                o.EnableAdaptiveSampling = false;
                o.EnableActiveTelemetryConfigurationSetup = true;
            });
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(Configuration["ConnectionStrings:storageconnectionstring:blob"], preferMsi: true);
                builder.AddQueueServiceClient(Configuration["ConnectionStrings:storageconnectionstring:queue"], preferMsi: true);
            });
            /*services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "RedisConnectionString";
                //options.InstanceName = "your-instance-name-here";
            });*/
            /* services.AddDistributedRedisCache(option =>

             {

                 option.Configuration = Configuration.GetConnectionString("RedisConnection");

             });*/
            /*services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("RedisConnectionString");
               // options.InstanceName = "master";
            });
*/
            }
                // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
                public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
            if (env.IsDevelopment())
                {
                app.UseDeveloperExceptionPage();
                }
            else
                {
                app.UseExceptionHandler("/Home/Error");
      
                app.UseHsts();
                }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
            }
        }
    internal static class StartupExtensions
        {
        public static IAzureClientBuilder<BlobServiceClient, BlobClientOptions> AddBlobServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
            {
            if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
                {
                return builder.AddBlobServiceClient(serviceUri);
                }
            else
                {
                return builder.AddBlobServiceClient(serviceUriOrConnectionString);
                }
            }
        public static IAzureClientBuilder<QueueServiceClient, QueueClientOptions> AddQueueServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
            {
            if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
                {
                return builder.AddQueueServiceClient(serviceUri);
                }
            else
                {
                return builder.AddQueueServiceClient(serviceUriOrConnectionString);
                }
            }
        }
    }
