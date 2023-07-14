namespace LMS.WebApplication.Controllers
    {
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Net.Http;
    using LMS.Common.Models;
    using LMS.Common.ViewModels;
    using Newtonsoft.Json;
    using System.Text;
    using LMS.WebApplication.Service;
    using Microsoft.AspNetCore.Authorization;
    using System;
    using StackExchange.Redis;
    using System.Collections.Generic;
    [Authorize(Policy = "Students")]
    public class UserController : Controller
        {
        static int bookid;
        static int userid;
        private readonly IHttpClientService<Book> bookService;
        private readonly IHttpClientService<ViewBookWithDate> bookWithDateService;
        
        public UserController(IHttpClientService<Book> bookService, IHttpClientService<ViewBookWithDate> bookWithDateService)
            {

            this.bookService = bookService;
            this.bookWithDateService = bookWithDateService;

            }
        public IActionResult Index()
            {
            return View();
            }
        public IActionResult AddUserId(Book book)
            {
            bookid = book.BookId;
            return View("AddUserId");
            }

        public async Task<IActionResult> BooksForCheckout()
            {
             IDatabase cache = Connection.GetDatabase();
            cache.StringSet("key1", "value");
            Connection.GetDatabase(1).StringSet("key2", 25, TimeSpan.FromMinutes(1));

            var booksavailable = await bookService.GetAll("Book/BooksAvailable");
            cache.StringSet("book", JsonConvert.SerializeObject(booksavailable));
            var cached=JsonConvert.DeserializeObject<List<Book>>(cache.StringGet("book"));
            return View(cached);
            //return View(booksavailable);

            }
        public async Task<IActionResult> UserBooks()
            {

            var booksofauser = await bookWithDateService.GetAll("Book/ShowBooksbyDate/" + userid.ToString());
            return View(booksofauser);
            }

        public async Task<IActionResult> CheckBookOut(User user)
            {
            try
                {
                userid = user.UserId;
                using (var client = new HttpClient())
                    {

                    var bookcontent = new StringContent(JsonConvert.SerializeObject(new CheckoutBook { bookId = bookid, userId = userid }), Encoding.UTF8, "application/json");
                    await client.PostAsync($"https://lmsapideepdive.azurewebsites.net/api/User/CheckBookOut", bookcontent);
                    }
                return RedirectToAction("UserBooks");
                }

            catch (Exception)
                {
                return View("Error");
                }
            }


        public async Task<IActionResult> ReturnBook(Book book)
            {
            try
                {
                bookid = book.BookId;
                using (var client = new HttpClient())
                    {

                    var bookcontent = new StringContent(JsonConvert.SerializeObject(new CheckoutBook { bookId = bookid, userId = userid }), Encoding.UTF8, "application/json");
                    await client.PostAsync("https://lmsapideepdive.azurewebsites.net/api/Book/ReturnBook/" + book.BookId.ToString(), bookcontent);
                    }
                return RedirectToAction("UserBooks");
                }

            catch (Exception)
                {
                return View("Error");
                }
            }
     
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            /*string cacheConnection = ConfigurationManager.AppSettings["RedisConnectionString"].ToString();
            return ConnectionMultiplexer.Connect(cacheConnection);*/
           // return ConnectionMultiplexer.Connect("RedisConnectionString");
           return ConnectionMultiplexer.Connect("LMS-Redis.redis.cache.windows.net:6380,password=xoRM+YFw46T41m2jaV4ZGp834BrhVKd3v489WIaoC3I=,ssl=True,abortConnect=False");
        });
        public static ConnectionMultiplexer Connection
            {
            get
                {
                return lazyConnection.Value;
                }
            }
        }
    }

   
 
    
