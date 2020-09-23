using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IPaginate
    {
        int TotalPageQuantity { get; set; }
        int CurrentPage { get; set; }
        int PageSize { get; set; }

        Task SelectedPage(int page);
    }
}
