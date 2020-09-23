using Microsoft.AspNetCore.Components;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IEditJobViewModel : IAsyncInitialization, IViewModelState, IEditForm, INewForm
    {
        public JobDto JobDto { get; set; }
        public Job Job { get; set; }
        public List<Project> Projects { get; set; }

        /// <summary>
        /// Opens modal to allow user to edit job
        /// </summary>
        /// <param name="job"></param>
        void OpenEditDialog(Job job);
    }
}
