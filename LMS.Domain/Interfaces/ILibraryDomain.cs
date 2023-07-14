

namespace LMS.Domain.Interface
    {
    using LMS.Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

   public  interface ILibraryDomain
        {
        public Task<ActionResult<IEnumerable<Library>>> GetLibrary();
        }
    }
