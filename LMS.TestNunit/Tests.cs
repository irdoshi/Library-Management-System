using LMS.API.Controllers;
using LMS.Common.Models;
using LMS.Domain.Interface;
using LMS.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace LMS.TestNunit
    {
    public class Tests
        {
       private Mock<IBookRepository> bookrepository;
        private IBookDomain bookdomain;
        private  BookController bookcontroller;
        private List<Book>books;
      
        [SetUp]
        public void SetUp()
            {
           /* bookrepository = new Mock<IBookRepository>();
            books = new List<Book>();*/
           // books.Add(new Book { Author = "Roald Dahl", Title = "James and the Giant Peach", Genre = "Children", Price = 300.0 });
            }
        [Test]
        public void TestLibraries()
            {
            BookController book = new BookController(bookdomain);
            var resultfromget = book.GetBooksbyLibrary(1);

            Assert.IsNotNull(resultfromget);
            }
        [Test]
        public void TestBookController()
            {
            BookController bookcontroller = new BookController(bookdomain);
            var resultfromget = bookcontroller.BooksAvailable();
           
            Assert.IsNotNull(resultfromget);
            }
       /* [Test]
        public void TestBookControllerfornull()
            {
            BookController bookcontroller = new BookController(null);


            Assert.Throws<Exception>(() => bookcontroller.BooksAvailable());
            }*/
       [Test]
       public async Task TestMethod()
            {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44387/api/Book/BooksAvailable");
            var result= await httpClient.GetAsync("");
            Console.WriteLine(await result.Content.ReadAsStringAsync());
            }

        }
    }