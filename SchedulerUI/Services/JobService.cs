using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using SchedulerAPI.Models;
using SchedulerUI.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchedulerUI.Services
{
    public class JobService : IJobService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _storageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public JobService(HttpClient httpClient,
            ILocalStorageService storageService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _storageService = storageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<List<Job>> GetJobs()
        {
            var response = await _httpClient.GetAsync("jobs/GetJobs");

            if (response.IsSuccessStatusCode)
            {
                var jobs = JsonConvert.DeserializeObject<List<Job>>(response.Content.ReadAsStringAsync().Result);
                return jobs;
            }
            else
            {
                return null;
            }
        }
    }
}