namespace LMS.Repository.Interfaces
    {
    using LMS.Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
   public interface IUserRepository
        {

        public Task<IActionResult> DeleteUserBookAssociation(int id);
        public Task<IActionResult> CheckBookOut(CheckoutBook checkout);
        }
    }
