using SchedulerAPI.Dtos;
using SchedulerUI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchedulerUI.Services
{
    public class JobService : IJobService
    {
        public Task<HttpResponseMessage> AddJob(JobDto job)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteJob(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> EditJob(JobDto job)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetJob(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetJobs(string sort = null, int pageNumber = 5, int pageSize = 20, bool onlyJobs = false)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetQuotes(string sort = null, int pageNumber = 5, int pageSize = 20, bool onlyJobs = false)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SearchJobs(string jobNo)
        {
            throw new NotImplementedException();
        }
    }
}
