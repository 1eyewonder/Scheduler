using AutoMapper;
using Blazored.Modal;
using Blazored.Modal.Services;
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
        #endregion

        #region Properties
        public JobDto JobDto { get; set; }
        public Job Job { get; set; }
        public List<Project> Projects { get; set; }
        public int StartingIndex { get; set; }
        [CascadingParameter] public BlazoredModalInstance BlazoredModalInstance { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsRunning { get; set; }
        #endregion

        public EditJobViewModel(IJobService jobService, IProjectService projectService, IMapper mapper)
        {
            //Inject services
            _jobService = jobService;
            _projectService = projectService;
            _mapper = mapper;

            //Initialize
            BlazoredModalInstance = new BlazoredModalInstance();
            Job = new Job();
            JobDto = new JobDto();
            Projects = new List<Project>();
            IsRunning = false;
            ErrorMessage = null;
            StartingIndex = 1;
        }

        public async Task InitializeAsync(int id, BlazoredModalInstance blazoredModal)
        {
            //Gets modal instance from UI
            BlazoredModalInstance = blazoredModal;

            //Get job details and map to dto
            Job = await _jobService.GetJob(id);
            _mapper.Map(Job, JobDto);

            //Get projects
            Projects = await _projectService.GetProjects();
            StartingIndex = Projects.IndexOf(Projects.Where(p => p.Id == JobDto.ProjectId).FirstOrDefault());
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
                    await Close(ModalResult.Ok($"Job was successfully updated."));
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

        public async Task Cancel()
        {
            await BlazoredModalInstance.Cancel();
        }

        public async Task Close(ModalResult modalResult = null)
        {
            await BlazoredModalInstance.Close(modalResult);
        }
    }
}
