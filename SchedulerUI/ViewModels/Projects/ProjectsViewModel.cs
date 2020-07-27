using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Server.IIS.Core;
using SchedulerAPI.Interfaces;
using SchedulerAPI.Models;
using SchedulerUI.Pages.Jobs;
using SchedulerUI.Services.Interfaces;
using SchedulerUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Projects
{
    public sealed class ProjectsViewModel : IProjectsViewModel, IViewModelState
    {

        #region Fields
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly NavigationManager _navigation;
        private readonly IModalService _modal;
        #endregion

        #region Properties
        public List<Project> ProjectList { get; set; }
        public Task Initialization { get; private set; }
        public string ErrorMessage { get; set; }
        public bool IsRunning { get; set; }
        #endregion

        public ProjectsViewModel(IUserService userService, IProjectService projectService,
            NavigationManager navigation, IModalService modal)
        {
            //Inject services
            _userService = userService;
            _projectService = projectService;
            _navigation = navigation;
            _modal = modal;

            //Initialize
            ProjectList = new List<Project>();
            ErrorMessage = null;
            IsRunning = false;

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

        /// <summary>
        /// Gets list of projects from the database
        /// </summary>
        /// <returns></returns>
        private async Task<List<Project>> GetProjects()
        {
            var projects = await _projectService.GetProjects();
            return projects;
        }

        public async Task Refresh()
        {
            ProjectList = await GetProjects();
        }

        public async Task ShowEditProject(int projectId)
        {
            //Fake code to allow for compile
            ProjectList = await GetProjects();

            ////Adds parameters to pass into modal
            //var parameters = new ModalParameters();
            //parameters.Add(nameof(EditJob.Id), projectId);

            ////Set modal options
            //var options = new ModalOptions()
            //{
            //    HideCloseButton = false,
            //    DisableBackgroundCancel = true
            //};

            //try
            //{
            //    //Shows modal and awaits user input
            //    var modalForm = _modal.Show<EditJob>("Edit Project", parameters, options);
            //    var result = await modalForm.Result;

            //    //If user cancels edit
            //    if (result.Cancelled)
            //    {

            //    }

            //    //Refreshes data with update
            //    else
            //    {
            //        await Refresh();
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.Write(e.ToString());
            //}
        }
    }
}
