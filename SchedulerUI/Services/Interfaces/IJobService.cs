using SchedulerAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchedulerUI.Services.Interfaces
{
    public interface IJobService
    {
        Task<HttpResponseMessage> GetJobs(string sort = null, int pageNumber = 5, int pageSize = 20, bool onlyJobs = false);
        Task<HttpResponseMessage> GetQuotes(string sort = null, int pageNumber = 5, int pageSize = 20, bool onlyJobs = false);
        Task<HttpResponseMessage> GetJob(int id);
        Task<HttpResponseMessage> SearchJobs(string jobNo);
        Task<HttpResponseMessage> AddJob(JobDto job);
        Task<HttpResponseMessage> EditJob(JobDto job);
        Task<HttpResponseMessage> DeleteJob(int id);

    }
}
