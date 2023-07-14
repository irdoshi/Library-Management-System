
namespace LMS.WebApplication.Controllers
    {
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using LMS.Common.Models;
    using LMS.Common.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using LMS.WebApplication.Service;
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using LMS.WebApplication.Models;
    using System.Diagnostics;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Azure;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;

    [Authorize(Policy = "Administrator")]
    public class AdminController : Controller
        {
        private IConfiguration _configuration;
        static int LID;
        private readonly IHttpClientService<Library> libraryService;
        private readonly IHttpClientService<ViewBookAvailable> viewbooksService;
        private readonly IHttpClientService<Book> bookService;

        public AdminController(IConfiguration Configuration, IHttpClientService<Library> libraryService, IHttpClientService<ViewBookAvailable> viewbooksService, IHttpClientService<Book> bookService)
            {
            _configuration = Configuration;
            this.libraryService = libraryService;
            this.viewbooksService = viewbooksService;
            this.bookService = bookService;

            }
        public ActionResult AddBook()
            {
            return View("AddBook");
            }

        public async Task<IActionResult> AdminView(Library libid)
            {
            LID = libid.LibraryId;
            var booksbylibrary = await viewbooksService.GetAll("Book/GetBooksbyLibrary/" + LID.ToString());
            return View(booksbylibrary);
            }

        public async Task<IActionResult> AdminView2()
            {

            var booksbylibrary = await viewbooksService.GetAll("Book/GetBooksbyLibrary/" + LID.ToString());
            return View(booksbylibrary);

            }

        public async Task<IActionResult> LibraryView()
            {

            var libraries = await libraryService.GetAll("Library/GetLibrary");
            return View(libraries);

            }


        public async Task<IActionResult> ChangeAvailability(Book book)
            {

            var result = await bookService.Put(endpoint: $"Book/ChangeAvailability/" + book.BookId.ToString(), entity: book);

            if (result)
                {
                return RedirectToAction("AdminView2");

                }
            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

            return RedirectToAction("AdminView2");

            }

        public async Task<IActionResult> Create(Book book )//I,FormFile file)
            {
            await bookService.Post(endpoint: $"Book/PostBook/" + LID.ToString(), entity: book);
            return RedirectToAction("AdminView2");
            /*try
                {

                using (var client = new HttpClient())
                    {

                    var bookcontent = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                    await client.PostAsync($"https://localhost:44387/api/Book/PostBook/" + LID.ToString(), bookcontent);

                   // await AddCover(file, response.ToString());
                    }
                }
            catch (Exception)
                {
                return View("Error");
                }
            return RedirectToAction("AdminView2");*/

            }
        

        public async Task<IActionResult> AddCover(IFormFile file, string bookId)
            {
            if (!file.ContentType.Contains("image"))
                {
                return View("UploadError");
                }
            else
                {
                var containerClient = new BlobContainerClient(
                    connectionString: _configuration["storageconnectionstring"],
                    blobContainerName: "covers");
                var guid = Guid.NewGuid().ToString();
                //  var book = JsonConvert.DeserializeObject<ViewBookAvailable>(bookwithid);
                string blobName = bookId + "cover"+guid +"."+ file.ContentType.Substring(file.ContentType.IndexOf('/') + 1);

                try
                    {
                    var blobClient = containerClient.GetBlobClient(blobName);
                    var blobUrl = blobClient.Uri.AbsoluteUri;
                    await blobClient.UploadAsync(
                        file.OpenReadStream(),
                        new BlobHttpHeaders
                            {
                            ContentType = file.ContentType,
                            CacheControl = "public"
                            });

                    IDictionary<string, string> metadata = new Dictionary<string, string>
                        {
                        ["BookId"] = bookId,
                        /* ["Title"] = book.Title.ToString(),
                         ["Author"] = book.Author.ToString(),
                         ["Genre"] = book.Genre.ToString(),
                         ["Price"] = book.Price.ToString(),
                        */
                        };
                    await blobClient.SetMetadataAsync(metadata);
                    await PutImage(blobUrl, bookId);

                    }
                catch (RequestFailedException e)
                    {
                    Debug.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
                    Debug.WriteLine(e.Message);
                    }

                return RedirectToAction("AdminView2");

                }
            }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        public async Task<IActionResult> PutImage(string blobUrl, string bookId)
            {
            using (var client = new HttpClient())
                {
                var bookcontent = new StringContent(JsonConvert.SerializeObject(new ImageDetails { bookId=bookId, blobUrl = blobUrl }), Encoding.UTF8, "application/json");
                await client.PutAsync("https://lmsapideepdive.azurewebsites.net/api/Book/PutImage/" + bookId, bookcontent);
            //https://localhost:44387/api/Book/PutImage/               
                }
  
            return RedirectToAction("AdminView2");
            }
        }
    }


