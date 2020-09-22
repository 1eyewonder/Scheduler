using Microsoft.AspNetCore.Components;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IEditJobViewModel : IViewModelState, IEditForm
    {
        public JobDto JobDto { get; set; }
        public Job Job { get; set; }
        public List<Project> Projects { get; set; }
        Task OpenEditDialog(Job job);
    }
}
