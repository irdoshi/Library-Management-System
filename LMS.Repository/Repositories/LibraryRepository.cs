namespace LMS.Repository.Repositories
    {
    
    using Microsoft.AspNetCore.Mvc;
    using LMS.Common.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using LMS.Repository.Interfaces;

   public class LibraryRepository : ControllerBase, ILibraryRepository
        {

            private readonly CoreDbContext _context;

        public LibraryRepository(CoreDbContext context)
            {
            _context = context;

            }
            public async Task<ActionResult<IEnumerable<Library>>> GetLibrary()
                {
                return await _context.Library.ToListAsync();
                }
            }
        }
