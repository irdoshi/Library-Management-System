
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LMS.WebApplication.Service
    {
    public class HttpClientService<TEntity> : IHttpClientService<TEntity>
        {

        private readonly HttpClient httpClient;
        
        public HttpClientService(HttpClient httpClient, IConfiguration configuration)
            {
                this.httpClient = httpClient;
                this.httpClient.BaseAddress = new Uri(configuration["LMS:APIBaseAddress"]);
            }

        public async Task<TEntity> Get(string endpoint)
            {
                try
                    {
                    var result = await httpClient.GetAsync(endpoint);
                    if (result.IsSuccessStatusCode)
                        {
                        string responseStream = await result.Content.ReadAsStringAsync();
                        TEntity entity = JsonConvert.DeserializeObject<TEntity>(responseStream);
                        return entity;
                        }
                    }
                catch (Exception e)
                    {
                    Debug.WriteLine(e.Message);
                    }

                return default;
            }

        public async Task<IEnumerable<TEntity>> GetAll(string endpoint)
            {
                try
                    {
                    var result = await httpClient.GetAsync(endpoint);
                    if (result.IsSuccessStatusCode)
                        {
                        string responseStream = await result.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(responseStream);
                        }
                    }

                catch (Exception e)
                    {
                    Debug.WriteLine(e.Message);
                    }

                return default;
            }

        public async Task<bool> Post(string endpoint, TEntity entity)
            {
            try
                {
                //await PrepareAuthenticatedClient();
                HttpContent httpContent = entity != null
                    ? new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")
                    : null;
                var result = await httpClient.PostAsync(endpoint, httpContent);
                return result.IsSuccessStatusCode;
                }

            catch (Exception e)
                {
                Debug.WriteLine(e.Message);
                }

            return false;
            }

        public async Task<bool> Put(string endpoint, TEntity entity)
            {
            try
                {
                HttpContent httpContent = entity != null
                    ? new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")
                    : null;
                var result = await httpClient.PutAsync(endpoint, httpContent);
                return result.IsSuccessStatusCode;
                }

            catch (Exception e)
                {
                Debug.WriteLine(e.Message);
                }

            return false;
            }
  
        }
    }
