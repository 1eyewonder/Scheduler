using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using SchedulerAPI.Models;
using SchedulerUI.Pages;
using SchedulerUI.Pages.Jobs;
using SchedulerUI.Services.Interfaces;
using SchedulerUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Jobs
{
    public sealed class JobsViewModel : IJobsViewModel, IViewModelState
    {

        #region Fields
        private readonly IUserService _userService;
        private readonly IJobService _jobService;
        private readonly NavigationManager _navigation;
        private readonly IModalService _modal;
        #endregion

        #region Properties
        public List<Job> JobList { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsRunning { get; set; }
        public Task Initialization { get; private set; }
        #endregion

        public JobsViewModel(IUserService userService, IJobService jobService,
            NavigationManager navigation, IModalService modal)
        {
            //Injects services
            _userService = userService;
            _jobService = jobService;
            _navigation = navigation;
            _modal = modal;

            //Initialize
            JobList = new List<Job>();
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
            JobList = await GetJobs();
        }

        /// <summary>
        /// Gets list of jobs from the database
        /// </summary>
        /// <returns></returns>
        private async Task<List<Job>> GetJobs()
        {
            var jobs = await _jobService.GetJobs();
            return jobs;
        }

        public async Task Refresh()
        {
            JobList = await GetJobs();
        }

        public async Task ShowEditJob(int jobId)
        {
            //Adds parameters to pass into modal
            var parameters = new ModalParameters();
            parameters.Add(nameof(EditJob.Id), jobId);

            //Set modal options
            var options = new ModalOptions()
            {
                HideCloseButton = false,
                DisableBackgroundCancel = true
            };

            try
            {
                //Shows modal and awaits user input
                var modalForm = _modal.Show<EditJob>("Edit Job", parameters, options);
                var result = await modalForm.Result;

                //If user cancels edit
                if (result.Cancelled)
                {

                }

                //Refreshes data with update
                else
                {
                    await Refresh();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
        }
    }
}
