using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IEditCustomerViewModel : IViewModelState, IEditForm
    {
        public Customer Customer { get; set; }
        void OpenEditDialog(Customer customer);
    }
}
