using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IProjectsViewModel : IAsyncInitialization, IViewModelState, IPaginate, IEntityManagement
    {
        /// <summary>
        /// List of projects in the database
        /// </summary>
        public List<Project> ProjectList { get; set; }

    }
}
