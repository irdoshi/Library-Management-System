
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LMS.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using LMS.API.Controllers;
using LMS.Domain.Interface;
using LMS.Common.Models;

namespace LMS.Tests
    {
    [TestClass]
    public class TestBookController
        {
        private readonly BookController bookcontroller;
        private readonly IBookDomain bookDomain;

        public TestBookController(BookController bookcontroller, IBookDomain bookDomain)
            {
            this.bookcontroller = bookcontroller;
            this.bookDomain = bookDomain;
            }
        [Fact]
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