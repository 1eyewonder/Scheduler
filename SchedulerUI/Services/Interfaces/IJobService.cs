using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using SchedulerUI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchedulerUI.Services.Interfaces
{
    /// <summary>
    /// Service for handling communication with api in regards to jobs
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// Gets list of jobs from database
        /// </summary>
        /// <returns></returns>
        Task<PaginationResponseDto<Job>> GetJobs(int recordsPerPage = 10, int pageNumber = 1);

        /// <summary>
        /// Gets job with given id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Job> GetJob(int id);

        /// <summary>
        /// Updates given job in the database
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> UpdateJob(JobDto job);

        /// <summary>
        /// Adds job to database
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> AddJob(JobDto job);

        /// <summary>
        /// Removes the given job from the database
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteJob(int jobId);
    }
}
