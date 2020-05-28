using DAL.Request;
using DAL.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.News;

namespace Web.Service
{
    public class NewsApiClient : INewsApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NewsApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                             IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task <IEnumerable<NewsModel>> GetAll()
        {
            
            var response = await getClient().GetAsync("/api/news");
            var body = await response.Content.ReadAsStringAsync();

            var listOfNews = JsonConvert.DeserializeObject<IEnumerable<NewsModel>>(body);

            return listOfNews;
        }

        public async Task<bool> Create(NewsRequest model)
        {

            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await getClient().PostAsync($"/api/news", httpContent);
            
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(NewsResponse model)
        {

            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await getClient().PutAsync($"/api/news", httpContent);

            return response.IsSuccessStatusCode;
        }



        public async Task<NewsResponse> GetById(int id)
        {
            var response = await getClient().GetAsync($"/api/news/{id}");

            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<NewsResponse>(body);

            return null;
        }

        private HttpClient getClient()
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (session == null)
            {
                session = "";
            }
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            return client;
        }

        public async Task<bool> Delete(int id)
        {
            var response = await getClient().DeleteAsync($"/api/news/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
