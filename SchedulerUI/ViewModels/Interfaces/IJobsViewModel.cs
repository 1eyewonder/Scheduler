using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IJobsViewModel : IAsyncInitialization, IViewModelState, IPaginate, IEntityManagement
    {
        /// <summary>
        /// List of jobs in the database
        /// </summary>
        public List<Job> JobList { get; set; }
    }
}
