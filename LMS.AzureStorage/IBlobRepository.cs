namespace LMS.AzureStorage.Interface
{
    using LMS.Common.Models;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface IBlobRepository
        {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="Cover"></param>
        /// <returns></returns>
        public Task AddCover(IFormFile file, Book book);
        }          
       
}
