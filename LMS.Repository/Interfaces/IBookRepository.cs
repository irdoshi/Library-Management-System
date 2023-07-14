
namespace LMS.Repository.Interfaces
    {
    using System.Collections.Generic;
    using LMS.Common.Models;
    using LMS.Common.ViewModels;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IBookRepository
        {
        /// <summary>
        /// Method to Add Book to DB
        /// </summary>
        /// <param name="book">book To Add</param>
        /// <returns>book model</returns>
        /// 
        public Task<IActionResult> ChangeIsCheckedOut(int id);

        public Task<IEnumerable<Book>> BooksAvailable();

        public Task<IActionResult> PostBook(Book book, int libid);

        public Task<IActionResult> ChangeAvailability(int id);

        public Task<IEnumerable<ViewBookWithDate>> ShowBooksbyDate(int id);

        public Task<IActionResult> ReturnBook(int id);

        public Task<IEnumerable<ViewBookAvailable>> GetBooksbyLibrary(int id);

        public Task<IActionResult> PutImage(ImageDetails imageDetails);
        }
    }
