namespace LMS.Repository.Interfaces
    {
    using LMS.Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILibraryRepository
        {
        public Task<ActionResult<IEnumerable<Library>>> GetLibrary();
        }
    }