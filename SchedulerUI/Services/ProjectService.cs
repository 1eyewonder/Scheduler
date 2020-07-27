using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using SchedulerUI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerUI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _storageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public ProjectService(HttpClient httpClient, ILocalStorageService storageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _storageService = storageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Project> GetProject(int id)
        {
            var response = await _httpClient.GetAsync("projects/" + id);

            if (response.IsSuccessStatusCode)
            {
                var project = JsonConvert.DeserializeObject<Project>(response.Content.ReadAsStringAsync().Result);
                return project;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Project>> GetProjects()
        {
            var response = await _httpClient.GetAsync("projects/");

            if (response.IsSuccessStatusCode)
            {
                var projects = JsonConvert.DeserializeObject<List<Project>>(response.Content.ReadAsStringAsync().Result);
                return projects;
            }
            else
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> UpdateProject(ProjectDto project)
        {
            var json = JsonConvert.SerializeObject(project);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("projects/" + project.Id, data);

            return response;
        }
    }
}
