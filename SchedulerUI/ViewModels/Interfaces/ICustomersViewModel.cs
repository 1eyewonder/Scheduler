using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface ICustomersViewModel: IAsyncInitialization, IViewModelState, IPaginate, IEntityManagement
    {
        public List<Customer> CustomersList { get; set; }
    }
}
