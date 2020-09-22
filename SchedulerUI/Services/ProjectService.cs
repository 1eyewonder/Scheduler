using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using SchedulerUI.Dtos;
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
            // Attempts to get project data from database
            var response = await _httpClient.GetAsync("projects/" + id);

            // If response is successful
            if (response.IsSuccessStatusCode)
            {
                // Gets object from response and returns it
                var project = JsonConvert.DeserializeObject<Project>(response.Content.ReadAsStringAsync().Result);
                return project;
            }
            else
            {
                return null;
            }
        }

        public async Task<PaginationResponseDto<Project>> GetProjects(int recordsPerPage = 10, int pageNumber = 1)
        {
            // Awaits response from database
            var response = await _httpClient.GetAsync($"projects/GetProjects?quantityPerPage={recordsPerPage}&page={pageNumber}");

            // If successful
            if (response.IsSuccessStatusCode)
            {
                var totalPageQuantity = int.Parse(response.Headers.GetValues("pagesQuantity").FirstOrDefault());
                var projects = JsonConvert.DeserializeObject<List<Project>>(response.Content.ReadAsStringAsync().Result);

                // Creates dto from response
                var paginationResponse = new PaginationResponseDto<Project>()
                {
                    TotalPagesQuantity = totalPageQuantity,
                    Items = projects
                };

                // Returns dto
                return paginationResponse;
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

        public async Task<HttpResponseMessage> AddProject(ProjectDto project)
        {
            // Converts user object to json for http post
            var json = JsonConvert.SerializeObject(project);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Calls web api job post
            var response = await _httpClient.PostAsync("projects/", data);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteProject(int projectId)
        {

            // Attempts to delete job from database
            var response = await _httpClient.DeleteAsync($"projects/{projectId}");

            // Returns response
            return response;
        }
    }
}
