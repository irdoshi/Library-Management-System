
    namespace LMS.API.Controllers
        {
        using LMS.Common.Models;
        using LMS.Domain.Interface;
        using Microsoft.AspNetCore.Http;
        using Microsoft.AspNetCore.Mvc;
        using System;
        using System.Threading.Tasks;


        [Route("api/[controller]/[action]")]
        [ApiController]
        public class UserController : ControllerBase
            {
            private readonly IUserDomain userDomain;

            public UserController(IUserDomain userDomain)
                {
                this.userDomain = userDomain;
                }
      
        [HttpPost]
        public async Task<ActionResult<UserBookAssociation>> CheckBookOut([FromBody] CheckoutBook checkout)
            {
            try
                {
                var checkbook = await userDomain.CheckBookOut(checkout);
                if (checkbook != null)
                    {
                    return Ok(checkbook);
                    }
                }
            catch (Exception e)
                {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
                }
            return BadRequest();
            }
        [HttpPost]
        public async Task<ActionResult<UserBookAssociation>> DeleteUserBookAssociation(int id)
            {  
            try
                {
                var deleteuserbookassociation = await userDomain.DeleteUserBookAssociation(id);
                if (deleteuserbookassociation != null)
                    {
                    return Ok(deleteuserbookassociation);
                    }
                }
            catch (Exception e)
                {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
                }
            return BadRequest();
            }

        }
    }
