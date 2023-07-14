using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.WebApplication.Service
    {
    public interface IHttpClientService<TEntity>
        {
        /// <summary>
        /// HttpClientService Method to Call Get API for Single Result
        /// </summary>
        /// <param name="endpoint">Define The API Endpoint (Eg: baseURL/"endpoint")</param>
        /// <returns>TEntity</returns>
        public Task<TEntity> Get(string endpoint);

        /// <summary>
        /// HttpClientService Method to Call Get API for All Results
        /// </summary>
        /// <param name="endpoint">Define The API Endpoint (Eg: baseURL/"endpoint")</param>
        /// <returns>IEnumerable<TEntity></returns>
        public Task<IEnumerable<TEntity>> GetAll(string endpoint);

        /// <summary>
        /// HttpClientService Method Call to Post APIs
        /// </summary>
        /// <param name="endpoint">Define The API Endpoint (Eg: baseURL/"endpoint")</param>
        /// <param name="entity">Modeled Data to be Posted</param>
        /// <returns>HttpResponseMessage.IsSuccessStatusCode: bool</returns>
        public Task<bool> Post(string endpoint, TEntity entity);

        /// <summary>
        /// HttpClientService Method Call to Put APIs
        /// </summary>
        /// <param name="endpoint">Define The API Endpoint (Eg: baseURL/"endpoint")</param>
        /// <param name="entity">Modeled Data to be Put</param>
        /// <returns>HttpResponseMessage.IsSuccessStatusCode: bool</returns>
        public Task<bool> Put(string endpoint, TEntity entity);

        /// <summary>
        /// HttpClientService Method Call to Delete APIs
        /// </summary>
        /// <param name="endpoint">Define The API Endpoint (Eg: baseURL/"endpoint")</param>
        /// <returns>HttpResponseMessage.IsSuccessStatusCode: b
        }
    }
