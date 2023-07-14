namespace LMS.Repository.Repositories
    {
    using LMS.Common.Models;
    using LMS.Repository.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
   public class UserRepository : ControllerBase, IUserRepository
        {
        private readonly CoreDbContext _context;

        public UserRepository(CoreDbContext context)
            {
            _context = context;

            }

        public async Task<IActionResult> DeleteUserBookAssociation(int id)
            {
            var userbookassoc = await _context.UserBookAssociation.FindAsync(id);
            if (userbookassoc == null)
                return NotFound();

            _context.UserBookAssociation.Remove(userbookassoc);
            await _context.SaveChangesAsync();
            return Ok(true); 
            }

        public async Task<IActionResult> CheckBookOut( CheckoutBook checkout)
            {
            var user = _context.User.FirstOrDefault(x => x.UserId == checkout.userId);
            if (user == null)
                {
                var us = new User();
                _context.User.Add(us);
                us.UserId = checkout.userId;
                us.RoleId = 2;
                await _context.SaveChangesAsync();
                }
            var booklibassoc = _context.BookLibraryAssociation.FirstOrDefault(x => x.BookId == checkout.bookId);

            if (booklibassoc != null && booklibassoc.IsAvailable == "true" && booklibassoc.IsCheckedOut == "false")
                {
                booklibassoc.IsCheckedOut = "true";
                int booklibassocId = booklibassoc.BookLibraryAssociationId;
                DateTime dueDate = DateTime.Now.AddMonths(3);
                var ub = new UserBookAssociation();
                _context.UserBookAssociation.Add(ub);
                ub.UserId = checkout.userId;
                ub.BookLibraryAssociationId = booklibassocId;
                ub.DueDate = dueDate;
                await _context.SaveChangesAsync();
                }

            else
                {
                return BadRequest(false);
                }

            return Ok(true);
            }
        }
        }
