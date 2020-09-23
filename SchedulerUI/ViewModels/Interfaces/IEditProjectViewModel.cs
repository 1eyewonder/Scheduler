using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IEditProjectViewModel: IAsyncInitialization, IViewModelState, IEditForm, INewForm
    {
        public ProjectDto ProjectDto { get; set; }
        public Project Project { get; set; }
        public List<Customer> Customers { get; set; }

        /// <summary>
        /// Opens modal to allow user to edit project
        /// </summary>
        /// <param name="project"></param>
        void OpenEditDialog(Project project);
    }
}
