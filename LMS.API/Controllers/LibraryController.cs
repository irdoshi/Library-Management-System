namespace LMS.API.Controllers
    {
    using LMS.Common.Models;
    using LMS.Domain.Interface;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibraryController : ControllerBase
        {

        private readonly ILibraryDomain libraryDomain;

        public LibraryController(ILibraryDomain libraryDomain)
            {
            this.libraryDomain = libraryDomain;
            }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Library>>> GetLibrary()
            {
            try
                {
                var library = await libraryDomain.GetLibrary();
                if (library != null)
                    {
                    return library;
                    }
                }
            catch (Exception e)
                {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
                }
            return BadRequest();
            }
        }
    }
