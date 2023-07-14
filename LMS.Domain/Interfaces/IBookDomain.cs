

namespace LMS.Domain.Interface
    {
    using LMS.Common.Models;
    using LMS.Common.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookDomain
        {

        public Task<IActionResult> ChangeIsCheckedOut(int id);
        public Task<IEnumerable<ViewBookWithDate>> ShowBooksbyDate(int id);
        public Task<IEnumerable<Book>> BooksAvailable();
        public Task<IEnumerable<ViewBookAvailable>> GetBooksbyLibrary(int id);
        public Task<IActionResult> ReturnBook(int id);
        public Task<IActionResult> ChangeAvailability(int id);
        public Task<IActionResult> PostBook(Book book, int libid);
        public Task<IActionResult>PutImage(ImageDetails imageDetails);
        }
    }
