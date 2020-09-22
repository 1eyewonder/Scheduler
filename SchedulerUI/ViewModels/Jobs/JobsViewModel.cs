using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
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
    public sealed class JobsViewModel : IJobsViewModel
    {

        #region Fields
        private readonly IJobService _jobService;
        private int _currentJobId;
        #endregion

        #region Properties
        public List<Job> JobList { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsRunning { get; set; }
        public Task Initialization { get; private set; }
        public int TotalPageQuantity { get; set; }
        public int CurrentPage { get; set; }
        public string NewJobNumber { get; set; }
        public bool DeleteDialogIsOpen { get; set; }
        #endregion

        public JobsViewModel(IJobService jobService)
        {
            //Injects services
            _jobService = jobService;

            //Initialize
            JobList = new List<Job>();
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
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            //Get jobs from database
            try
            {
                JobList = await GetJobs();
            }
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
            }

            //Re-enables actions
            IsRunning = false;
        }

        public async Task SelectedPage(int page)
        {
            CurrentPage = page;
            JobList = await GetJobs(10, page);
        }

        /// <summary>
        /// Gets list of jobs from the database
        /// </summary>
        /// <returns></returns>
        private async Task<List<Job>> GetJobs(int recordsPerPage = 10, int pageNumber = 1)
        {
            var response = await _jobService.GetJobs(recordsPerPage, pageNumber);
            TotalPageQuantity = response.TotalPagesQuantity;
            return response.Items;
        }

        public async Task Refresh()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            //Get jobs from database
            try
            {
                JobList = await GetJobs(10, CurrentPage);
            }
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
            }            

            //Re-enables actions
            IsRunning = false;
        }

        /// <summary>
        /// Adds a job to the database
        /// </summary>
        /// <returns></returns>
        public async Task AddEntity()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            // Creates new random quote number
            var job = new JobDto()
            {
                // Creates random 8 digit number until logic is added later
                QuoteNumber = new Random().Next(10000000, 99999999).ToString()
            };

            NewJobNumber = job.QuoteNumber;

            // Adds job to database
            try
            {               
                var response = await _jobService.AddJob(job);

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

        public void OpenDeleteDialog(int entityId)
        {
            DeleteDialogIsOpen = true;
            _currentJobId = entityId;
        }

        /// <summary>
        /// Deletes specified job from the database
        /// </summary>
        /// <returns></returns>
        public async Task DeleteEntity()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            try
            {
                //Attempts to update job in the database
                var response = await _jobService.DeleteJob(_currentJobId);

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

        public void CancelDelete()
        {
            IsRunning = false;
            ErrorMessage = null;
            DeleteDialogIsOpen = false;
        }

    }
}
