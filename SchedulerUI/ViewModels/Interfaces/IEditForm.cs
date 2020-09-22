using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IEditForm
    {
        /// <summary>
        /// Saves changes to database
        /// </summary>
        /// <returns></returns>
        Task SaveChanges();

        /// <summary>
        /// Cancels editing of entity
        /// </summary>
        /// <returns></returns>
        void Cancel();

        /// <summary>
        /// Flag signifying is the edit dialog is open
        /// </summary>
        bool EditDialogIsOpen { get; set; }
    }
}
