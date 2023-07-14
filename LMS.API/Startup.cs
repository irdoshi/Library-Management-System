
namespace LMS.API
    {
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using LMS.Common.Models;
    using Microsoft.EntityFrameworkCore;
    using LMS.Domain.Implementation;
    using LMS.Domain.Interface;
    using LMS.Domain;
    using Microsoft.Identity.Web;
    using Microsoft.AspNetCore.Mvc;

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
            /*services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);*/

            services.AddMicrosoftIdentityWebApiAuthentication(Configuration); //AAD
            services.AddDbContext<CoreDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));
            services.AddControllers();

            services.AddScoped<IBookDomain, BookDomain>();
            services.AddScoped<ILibraryDomain, LibraryDomain>();
            services.AddScoped<IUserDomain, UserDomain>();
            DomainInjection.Inject(services);
            services.AddRazorPages();
            services.AddHttpClient();
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
            if (env.IsDevelopment())
                {
                app.UseDeveloperExceptionPage();
                }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            }
        }
    }
