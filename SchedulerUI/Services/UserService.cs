using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchedulerAPI.Dtos;
using SchedulerUI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerUI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> Login(UserLoginDto userLogin)
        {
            var json = JsonConvert.SerializeObject(userLogin);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("users/login", data);

            return response;
        }

        public async Task<HttpResponseMessage> Register(UserDto user)
        {
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("users/register", data);

            return response;
        }
    }
}
