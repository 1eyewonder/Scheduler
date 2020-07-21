using AutoMapper;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.MappingProfiles
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<Job, JobDto>();
            CreateMap<JobDto, Job>();
        }
    }
}
