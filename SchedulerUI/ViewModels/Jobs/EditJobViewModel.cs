using AutoMapper;
using Microsoft.AspNetCore.Components;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using SchedulerUI.Pages;
using SchedulerUI.Services.Interfaces;
using SchedulerUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchedulerUI.ViewModels.Jobs
{
    public sealed class EditJobViewModel : IEditJobViewModel
    {
        #region Fields
        private readonly IJobService _jobService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly IJobsViewModel _jobsViewModel;
        #endregion

        #region Properties
        public JobDto JobDto { get; set; }
        public Job Job { get; set; }
        public List<Project> Projects { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsRunning { get; set; }
        public Task Initialization { get; private set; }
        public bool EditDialogIsOpen { get; set; }
        public bool NewDialogIsOpen { get; set; }
        #endregion

        public EditJobViewModel(IJobService jobService, 
            IProjectService projectService, 
            IMapper mapper, 
            IJobsViewModel jobsViewModel)
        {
            //Inject services
            _jobService = jobService;
            _projectService = projectService;
            _mapper = mapper;
            _jobsViewModel = jobsViewModel;

            //Initialize
            Job = new Job();
            JobDto = new JobDto();
            Projects = new List<Project>();
            IsRunning = false;
            ErrorMessage = null;
            EditDialogIsOpen = false;

            // Retrieves data async
            Initialization = InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            //Get projects
            var response = await _projectService.GetProjects();
            Projects = response.Items;
        }

       public void OpenEditDialog(Job job)
        {
            _mapper.Map(job, this.JobDto);
            EditDialogIsOpen = true;
        }

        public void OpenNewDialog()
        {
            JobDto = new JobDto()
            {
                // Creates random 8 digit number until logic is added later
                QuoteNumber = new Random().Next(10000000, 99999999).ToString()
            };
            NewDialogIsOpen = true;
        }

        public async Task<bool> SaveNewEntity()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            try
            {
                //Attempts to update job in the database
                var response = await _jobService.AddJob(JobDto);

                //If http call was successful
                if (response.IsSuccessStatusCode)
                {
                    // Closes modal and refreshes table
                    NewDialogIsOpen = false;
                    await _jobsViewModel.Refresh();

                    // Re-enables actions and returns success
                    IsRunning = false;
                    return true;
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

            // Re-enables actions and returns failure
            IsRunning = false;
            return false;
        }

        public async Task SaveChanges()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            try
            {
                //Attempts to update job in the database
                var response = await _jobService.UpdateJob(JobDto);

                //If http call was successful
                if (response.IsSuccessStatusCode)
                {
                    EditDialogIsOpen = false;
                    await _jobsViewModel.Refresh();
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

        public void Cancel()
        {
            IsRunning = false;
            ErrorMessage = null;
            EditDialogIsOpen = false;
            NewDialogIsOpen = false;
        }
    }
}
