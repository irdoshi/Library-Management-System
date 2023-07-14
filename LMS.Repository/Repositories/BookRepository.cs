namespace LMS.Repository.Repositories
    {
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LMS.Common.Models;
    using LMS.Common.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using LMS.Repository.Interfaces;
    public class BookRepository : ControllerBase, IBookRepository
        {
        private readonly CoreDbContext _context;
        public BookRepository(CoreDbContext context)
            {
            _context = context;
            }   
        public async Task<IActionResult> ChangeIsCheckedOut(int id)
            {
            var assoc = _context.BookLibraryAssociation.FirstOrDefault(x => x.BookId == id);

            if (assoc == null)
                {
                return NotFound();
                }

            if (id != assoc.BookLibraryAssociationId)
                {
                return BadRequest();
                }

            assoc.IsCheckedOut = assoc.IsCheckedOut.ToString() == "true" ? "false" : "true";

            _context.Entry(assoc).State = EntityState.Modified;

            try
                {
                await _context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!BookExists(id))
                    {
                    return NotFound();
                    }
                else
                    {
                    throw;
                    }
                }

            return NoContent();
            }
        public async Task<IEnumerable<Book>> BooksAvailable()
            {
            try
                {
                var isavailablequery = (from bookvar in _context.Book
                                        join booklibassocvar in _context.BookLibraryAssociation on
                                        bookvar.BookId equals booklibassocvar.BookId
                                        where booklibassocvar.IsAvailable == "true"
                                        where booklibassocvar.IsCheckedOut == "false"

                                        select bookvar).ToListAsync();
                return await isavailablequery;
                }

            catch(Exception)
                {
                return (IEnumerable<Book>)NoContent();
                }
         
            }
        public async Task<IEnumerable<ViewBookAvailable>>GetBooksbyLibrary(int id)
            {
            var getbbyl = (from b in _context.Book
                           join blassocvar in _context.BookLibraryAssociation
                           on b.BookId equals blassocvar.BookId
                           where blassocvar.LibraryId == id
                           select new ViewBookAvailable
                               {
                               BookId = b.BookId,
                               ImageUrl = b.ImageUrl,
                               Title = b.Title,
                               Author = b.Author,
                               Price = b.Price,
                               Genre = b.Genre,
                               IsAvailable = blassocvar.IsAvailable
                               }).ToListAsync();
            return await getbbyl;
            }
  
        public async Task<IEnumerable<ViewBookWithDate>> ShowBooksbyDate(int id)
            {
            var query = (from bookvar in _context.Book
                        join booklibraryassociationvar in _context.BookLibraryAssociation
                        on bookvar.BookId equals booklibraryassociationvar.BookId
                        join userbookassocvar in _context.UserBookAssociation
                        on booklibraryassociationvar.BookLibraryAssociationId equals userbookassocvar.BookLibraryAssociationId
                        orderby userbookassocvar.DueDate ascending
                        where booklibraryassociationvar.IsCheckedOut == "true"
                        where userbookassocvar.UserId == id
                        select new ViewBookWithDate
                            {
                            BookId = bookvar.BookId,
                            ImageUrl = bookvar.ImageUrl,
                            Title = bookvar.Title,
                            Author = bookvar.Author,
                            Price = bookvar.Price,
                            Genre = bookvar.Genre,
                            DueDate = (DateTime)userbookassocvar.DueDate
                            }).ToListAsync();

            return await query;
            }

        public async Task<IActionResult> ReturnBook(int id)
            {

            var booklibassoc = _context.BookLibraryAssociation.FirstOrDefault(x => x.BookId == id);

            if (booklibassoc != null && booklibassoc.IsCheckedOut == "true")
                {
                booklibassoc.IsCheckedOut = "false";

                foreach (var row in _context.UserBookAssociation)
                    {
                    if (row.BookLibraryAssociationId == booklibassoc.BookLibraryAssociationId)
                        {
                        _context.UserBookAssociation.Remove(row);
                        }
                    }

                await _context.SaveChangesAsync();
                }
            else
                {
                return BadRequest(false);
                }
            return Ok(true);
            }
        public async Task<IActionResult> ChangeAvailability(int id)
            {
            try
                {
                var association = _context.BookLibraryAssociation.FirstOrDefault(x => x.BookId == id);

                if (association == null)
                    {
                    return NotFound();
                    }

                association.IsAvailable = association.IsAvailable.ToString() == "true" ? "false" : "true";

                _context.Entry(association).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!BookExists(id))
                    {
                    return NotFound();
                    }
                else
                    {
                    throw;
                    }
                }

            return NoContent();
            }

        public async Task<IActionResult> PostBook(Book book, int libid)
            {
            if (ModelState.IsValid)
                {
              _context.Book.Add(book);
              await _context.SaveChangesAsync();

                var booklibassoc = new BookLibraryAssociation();
                _context.BookLibraryAssociation.Add(booklibassoc);
               
               booklibassoc.BookId = book.BookId;
                booklibassoc.LibraryId = libid;
                booklibassoc.IsAvailable = "true";
                booklibassoc.IsCheckedOut = "false";

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBooks", new { id = book.BookId }, book);
                }
            else
                {
                return BadRequest(400);
                }
            }

        public async Task<IActionResult> PutImage(ImageDetails imageDetails)
            {
            try
                {
                var book = _context.Book.FirstOrDefault(x => x.BookId == int.Parse(imageDetails.bookId));

                if (book == null)
                    {
                    return NotFound();
                    }

                book.ImageUrl = imageDetails.blobUrl;

                _context.Entry(book).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                }
            catch (Exception)
                { 
                    return NotFound();              
                }

            return NoContent();
            }

    private bool BookExists(int id)
            {
            return _context.Book.Any(e => e.BookId == id);
            }
        }
    }