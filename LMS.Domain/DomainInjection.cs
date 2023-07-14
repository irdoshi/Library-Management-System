

namespace LMS.Domain
    {
    using LMS.Repository.Interfaces;
    using LMS.Repository.Repositories;
    using Microsoft.Extensions.DependencyInjection;
   
     public static class DomainInjection
        {
        public static void Inject(IServiceCollection services)
            {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();

            }
        }
    }
