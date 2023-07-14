

namespace LMS.Domain.Implementation
    {
 
    using LMS.Domain.Interface;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using LMS.Common.Models;
    using System.Collections.Generic;
    using LMS.Repository.Interfaces;

    public class LibraryDomain : ILibraryDomain
        {
        private readonly ILibraryRepository libraryRepository;

        public LibraryDomain(ILibraryRepository libraryRepository)
            {
            this.libraryRepository = libraryRepository;

            }

        public async Task<ActionResult<IEnumerable<Library>>> GetLibrary()
            {
  
            return await libraryRepository.GetLibrary();
            }
        }
    }
