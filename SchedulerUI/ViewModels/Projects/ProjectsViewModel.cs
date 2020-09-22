using Microsoft.AspNetCore.Components;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using SchedulerUI.Services.Interfaces;
using SchedulerUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchedulerUI.ViewModels.Projects
{
    public sealed class ProjectsViewModel : IProjectsViewModel
    {

        #region Fields
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly NavigationManager _navigation;
        private int _currentProjectId;
        #endregion

        #region Properties
        public List<Project> ProjectList { get; set; }
        public Task Initialization { get; private set; }
        public string ErrorMessage { get; set; }
        public bool IsRunning { get; set; }
        public int TotalPageQuantity { get; set; }
        public int CurrentPage { get; set; }      
        public bool DeleteDialogIsOpen { get; set; }
        #endregion

        public ProjectsViewModel(IProjectService projectService)
        {
            //Inject services
            _projectService = projectService;

            //Initialize
            ProjectList = new List<Project>();
            ErrorMessage = null;
            IsRunning = false;
            CurrentPage = 1;

            //Retrieves data async
            Initialization = InitializeAsync();
        }

        /// <summary>
        /// Method executed upon class initialization to allow for async data access
        /// </summary>
        /// <returns></returns>
        private async Task InitializeAsync()
        {
            ProjectList = await GetProjects();
        }

        public async Task SelectedPage(int page)
        {
            CurrentPage = page;
            ProjectList = await GetProjects(10, page);
        }

        /// <summary>
        /// Gets list of projects from the database
        /// </summary>
        /// <returns></returns>
        private async Task<List<Project>> GetProjects(int recordsPerPage = 10, int pageNumber = 1)
        {
            var response = await _projectService.GetProjects(recordsPerPage, pageNumber);
            return response.Items;
        }

        /// <summary>
        /// Adds a project to the database
        /// </summary>
        /// <returns></returns>
        public async Task AddEntity()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            // Creates new random quote number
            var project = new ProjectDto()
            {
                // Creates random 8 digit number until logic is added later
                Name = "NewProject",
                Number = new Random().Next(10000, 99999).ToString(),
                CustomerId = 1
            };

            // Adds job to database
            try
            {
                var response = await _projectService.AddProject(project);

                // If post is successful
                if (response.IsSuccessStatusCode)
                {
                    await Refresh();
                }
                else
                {
                    ErrorMessage = response.ReasonPhrase;
                }

            }
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
            }

            //Re-enables actions
            IsRunning = false;
        }

        public async Task Refresh()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            //Get jobs from database
            try
            {
                ProjectList = await GetProjects(10, CurrentPage);
            }
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
            }

            //Re-enables actions
            IsRunning = false;
        }

        public async Task DeleteEntity()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            try
            {
                //Attempts to update job in the database
                var response = await _projectService.DeleteProject(_currentProjectId);

                //If http call was successful
                if (response.IsSuccessStatusCode)
                {
                    DeleteDialogIsOpen = false;
                    await Refresh();
                }

                //If http call was not successful
                else
                {
                    ErrorMessage = response.Content.ReadAsAsync<HttpError>().Result.ToString();
                }
            }

            //If other error occurred
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
            }

            //Re-enables actions
            IsRunning = false;
        }

        public void OpenDeleteDialog(int entityId)
        {
            DeleteDialogIsOpen = true;
            _currentProjectId = entityId;
        }

        public void CancelDelete()
        {
            IsRunning = false;
            ErrorMessage = null;
            DeleteDialogIsOpen = false;
        }
    }
}
