
namespace LMS.Domain.Implementation
    {

    using LMS.Domain.Interface;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using LMS.Common.Models;
    using LMS.Repository.Interfaces;
  

    public class UserDomain : IUserDomain
        {
        private readonly IUserRepository userRepository;
        public UserDomain(IUserRepository userRepository)
            {
            this.userRepository = userRepository;
            }

        public async Task<IActionResult> DeleteUserBookAssociation(int id)
            {
            var deleteuserbookassoc = userRepository.DeleteUserBookAssociation(id);
            return await deleteuserbookassoc;         
            }

        public async Task<IActionResult> CheckBookOut(CheckoutBook checkout)
            {
            var checkbookout = userRepository.CheckBookOut(checkout);
            return await checkbookout;
            }
        }
    }

