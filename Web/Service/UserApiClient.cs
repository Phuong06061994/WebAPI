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

namespace Web.Service
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<string> Authenticate(AuthenticateModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("/api/user/login", httpContent);
            var token = await response.Content.ReadAsStringAsync();

            return token;
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",sessions);
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync("/api/user");
            var body = await response.Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(body);

            return users;
        }

        public async Task<UserModel> GetById(Guid id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/user/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UserModel>(body);

            return null;
        }

        public async Task<bool> Create(UserModel registerRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(registerRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/user", httpContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/user/{id}/role", httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(result);

            return JsonConvert.DeserializeObject<bool>(result);
        }
    }
}

