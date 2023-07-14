using LMS.API.Controllers;
using LMS.Common.Models;
using LMS.Domain.Interface;
using NUnit.Framework;
using System.Collections.Generic;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
namespace LMS.Test
    {
    
    public class TestBookController
        {
        private readonly BookController bookcontroller;
        private readonly IBookDomain bookDomain;

        public TestBookController(BookController bookcontroller, IBookDomain bookDomain)
            {
            this.bookcontroller = bookcontroller;
            this.bookDomain = bookDomain;
            }

        /*[SetUp]
        public void Setup()
            {
            }*/

        [Test]
         public void returnBooksAvailable()
            {
            // Act
            var resultfromget = bookcontroller.BooksAvailable();
            var okResult = resultfromget as IEnumerable<Book>;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult);
            }
        }
    }