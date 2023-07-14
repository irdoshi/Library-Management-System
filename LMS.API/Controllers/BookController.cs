namespace LMS.API.Controllers
    {
    using LMS.Common.Models;
    using LMS.Common.ViewModels;
    using LMS.Domain.Interface;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

   
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
        {

        private readonly IBookDomain bookDomain;

        public BookController(IBookDomain bookDomain)
            {
            this.bookDomain = bookDomain;
            }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ViewBookAvailable>>> GetBooksbyLibrary(int id)
            {
            try
                {
                var books = await bookDomain.GetBooksbyLibrary(id);
                if (books != null)
                    {
                    return Ok(books);
                    }
                }
            catch (Exception e)
                {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
                }
            return BadRequest();
            }

        [HttpPut("{id}")]
        public async Task<ActionResult> ChangeIsCheckedOut(int id)
            {
            try
                {
                var books = await bookDomain.ChangeIsCheckedOut(id);
                if (books != null)
                    {
                    return Ok(books);
                    }
                }
            catch (Exception e)
                {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
                }
            return BadRequest();
            }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutImage([FromBody] ImageDetails imageDetails)
            {
            try
                {
                var book = await bookDomain.PutImage(imageDetails);
                if (book != null)
                    {
                    return Ok(book);
                    }
                }
            catch (Exception e)
                {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
                }
            return BadRequest();
            }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> BooksAvailable()
            {
            try
                {
                var booksavailable = await bookDomain.BooksAvailable();
                if (booksavailable != null)
                    {
                    return Ok(booksavailable);
                    }
                }
            catch (Exception e)
                {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
                }
            return BadRequest();
            }
        [HttpPost("{id}")]
        public async Task<ActionResult<Book>> PostBook([FromBody]Book book, int id)
            {
            try
                {
                var post = await bookDomain.PostBook(book, id);
                if (post != null)
                    {
                    return Ok(post);
                    }
                }
            catch (Exception e)
                {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
                }
            return BadRequest();
            }


        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeAvailability(int id)
            {
            try
                {
                var changeavailability = await bookDomain.ChangeAvailability(id);
                if (changeavailability != null)
                    {
                    return Ok(changeavailability);
                    }
                }
            catch (Exception e)
                {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
                }
            return BadRequest();
            }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ViewBookWithDate>>> ShowBooksbyDate(int id)
            {
            try
                {
                var showbook = await bookDomain.ShowBooksbyDate(id);
                if (showbook != null)
                    {
                    return Ok(showbook);
                    }
                }
            catch (Exception e)
                {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
                }
            return BadRequest();
            }

        [HttpPost("{id}")]
        public async Task<IActionResult> ReturnBook(int id)
            {
            try
                {
                var returnbook = await bookDomain.ReturnBook(id);
                if (returnbook != null)
                    {
                    return Ok(returnbook);
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
