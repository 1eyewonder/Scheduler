using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IProjectsViewModel : IAsyncInitialization
    {
        /// <summary>
        /// List of projects in the database
        /// </summary>
        public List<Project> ProjectList { get; set; }

        /// <summary>
        /// Refreshes data sources
        /// </summary>
        /// <returns></returns>
        Task Refresh();

        /// <summary>
        /// Shows Edit Project form for the given project
        /// </summary>
        /// <returns></returns>
        Task ShowEditProject(int projectId);
    }
}
