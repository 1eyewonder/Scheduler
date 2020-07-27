using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IJobsViewModel : IAsyncInitialization
    {
        /// <summary>
        /// List of jobs in the database
        /// </summary>
        public List<Job> JobList { get; set; }

        /// <summary>
        /// Refreshes data sources
        /// </summary>
        /// <returns></returns>
        Task Refresh();

        /// <summary>
        /// Shows Edit Job form for the given job
        /// </summary>
        /// <returns></returns>
        Task ShowEditJob(int jobId);
    }
}
