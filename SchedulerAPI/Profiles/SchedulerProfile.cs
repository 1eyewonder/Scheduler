using AutoMapper;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Profiles
{
    public class SchedulerProfile : Profile
    {
        /// <summary>
        /// Mapping profiles for Automapper
        /// </summary>
        ///  Add "ForMember(x => x.Id, opt => opt.Ignore())" when mapping from dtos to model class
        ///  to prevent Entity errors for trying to edit the Id
        public SchedulerProfile()
        {
            CreateMap<Job, JobDto>();
            CreateMap<JobDto, Job>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
