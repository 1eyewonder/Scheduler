using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface INewForm
    {
        /// <summary>
        /// Flag signifying is the new dialog is open
        /// </summary>
        bool NewDialogIsOpen { get; set; }

        /// <summary>
        /// Initiates process for opening new entity modal
        /// </summary>
        void OpenNewDialog();

        /// <summary>
        /// Saves new entry to database
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveNewEntity();
    }
}
