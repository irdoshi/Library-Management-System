

namespace LMS.Domain.Interface
    {
    using LMS.Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
  

    public interface IUserDomain
        {

        public Task<IActionResult> CheckBookOut( CheckoutBook checkout);
        public Task<IActionResult> DeleteUserBookAssociation(int id);
        }
    }
