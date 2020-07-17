using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using SchedulerAPI.Dtos;
using SchedulerUI.Services.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerUI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _storageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public bool IsLoggedIn { get; set; }

        public UserService(HttpClient httpClient,
            ILocalStorageService storageService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _storageService = storageService;
            _authenticationStateProvider = authenticationStateProvider;
            IsLoggedIn = false;
        }

        public async Task<HttpResponseMessage> Login(UserLoginDto userLogin)
        {
            //Sends user info to api and awaits response
            var json = JsonConvert.SerializeObject(userLogin);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("users/login", data);

            if (response.IsSuccessStatusCode)
            {
                //Gets authentication information back from api
                var userInfo = JsonConvert.DeserializeObject<AuthenticationDto>(response.Content.ReadAsStringAsync().Result);

                //Stores token to local storage and reports to user success
                await _storageService.SetItemAsync("User", userInfo);
                IsLoggedIn = true;

                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(userLogin.Email);
                await SetAuthorizationHeader();
                return response;
            }
            else
            {
                IsLoggedIn = false;
                return response;
            }
        }

        public async Task LogOut()
        {
            IsLoggedIn = false;
            await _storageService.RemoveItemAsync("User");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task SetAuthorizationHeader()
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                var token = await _storageService.GetItemAsync<AuthenticationDto>("User");

                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.AccessToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public async Task<HttpResponseMessage> Register(UserDto user)
        {
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("users/register", data);

            return response;
        }

        public async Task<bool> MinimumAuthRequired(int authLevel)
        {
            var result = await _storageService.GetItemAsync<AuthenticationDto>("User");
            return result.AuthorizationLevel >= authLevel;
        }
    }
}