using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IEditProjectViewModel: IViewModelState, IEditForm
    {
        public ProjectDto ProjectDto { get; set; }
        public Project Project { get; set; }
        public List<Customer> Customers { get; set; }
        Task OpenEditDialog(Project project);
    }
}
