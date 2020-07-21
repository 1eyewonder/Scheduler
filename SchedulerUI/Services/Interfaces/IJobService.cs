using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchedulerUI.Services.Interfaces
{
    public interface IJobService
    {
        Task<List<Job>> GetJobs();
        Task<Job> GetJob(int id);
        Task<HttpResponseMessage> UpdateJob(JobDto job);
    }
}
