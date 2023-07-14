
namespace LMS.Domain.Implementation
    {

    using System.Collections.Generic;
    using LMS.Domain.Interface;
    using LMS.Common.Models;
    using LMS.Common.ViewModels;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using LMS.Repository.Interfaces;

    public class BookDomain : IBookDomain
        {
         private readonly IBookRepository bookRepository;
        public BookDomain(IBookRepository bookRepository)
            {
            this.bookRepository = bookRepository;
           
            }

        public async Task<IEnumerable<ViewBookWithDate>> ShowBooksbyDate(int id)
            {
         
            return await bookRepository.ShowBooksbyDate(id);
            }
        public async Task<IEnumerable<Book>> BooksAvailable()
            {
           var booksavailable = bookRepository.BooksAvailable();
            return await booksavailable;
            }

        public async Task<IActionResult> ChangeAvailability(int id)
            {
            var changeavailability = bookRepository.ChangeAvailability(id);
            return await changeavailability;
            }

        public async Task<IEnumerable<ViewBookAvailable>> GetBooksbyLibrary(int id)
            {

            return await (bookRepository.GetBooksbyLibrary(id));
            }

        public async Task<IActionResult> ReturnBook(int id)
            {
            var returnbook = bookRepository.ReturnBook(id);
            return await returnbook;
            }

        public async Task<IActionResult> ChangeIsCheckedOut(int id)
            {
            var changeischeckedout = bookRepository.ChangeIsCheckedOut(id);
            return await changeischeckedout;
            }
        public async Task<IActionResult>PostBook(Book book, int libid)
            {
            var postbook = bookRepository.PostBook( book, libid);
            return await postbook;
            }

        public async Task<IActionResult> PutImage(ImageDetails imageDetails)
            {
            var putbook = bookRepository.PutImage(imageDetails);
            return await putbook;
            }
        }
    }


  
