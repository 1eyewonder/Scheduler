using AutoMapper;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using SchedulerUI.Services.Interfaces;
using SchedulerUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchedulerUI.ViewModels.Projects
{
    public class EditProjectViewModel: IEditProjectViewModel
    {
        #region Fields
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly IProjectsViewModel _projectsViewModel;
        private readonly ICustomerService _customerService;
        #endregion

        #region Properties
        public ProjectDto ProjectDto { get; set; }
        public Project Project { get; set; }
        public List<Customer> Customers { get; set; }
        public Task Initialization { get; private set; }
        public string ErrorMessage { get; set; }
        public bool IsRunning { get; set; }
        public bool EditDialogIsOpen { get; set; }
        public bool NewDialogIsOpen { get; set; }
        #endregion

        public EditProjectViewModel(IProjectService projectService, 
            IMapper mapper, 
            IProjectsViewModel projectsViewModel,
            ICustomerService customerService)
        {
            //Inject services
            _projectService = projectService;
            _customerService = customerService;
            _mapper = mapper;
            _projectsViewModel = projectsViewModel;     

            //Initialize objects
            Project = new Project();
            ProjectDto = new ProjectDto();
            Customers = new List<Customer>();
            IsRunning = false;
            ErrorMessage = null;
            EditDialogIsOpen = false;

            //Retrieves data async
            Initialization = InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            //Get customers
            var response = await _customerService.GetCustomers();
            Customers = response.Items;
        }
        public void OpenEditDialog(Project project)
        {
            _mapper.Map(project, ProjectDto);
            EditDialogIsOpen = true;
        }

        public void OpenNewDialog()
        {
            ProjectDto = new ProjectDto()
            {
                Number = new Random().Next(10000, 99999).ToString()             
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
                var response = await _projectService.AddProject(ProjectDto);

                //If http call was successful
                if (response.IsSuccessStatusCode)
                {
                    // Closes modal and refreshes page
                    NewDialogIsOpen = false;
                    await _projectsViewModel.Refresh();

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
                var response = await _projectService.UpdateProject(ProjectDto);

                //If http call was successful
                if (response.IsSuccessStatusCode)
                {
                    EditDialogIsOpen = false;
                    await _projectsViewModel.Refresh();
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
