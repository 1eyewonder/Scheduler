using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface IEntityManagement
    {
        /// <summary>
        /// Adds a new entry to the given entity table
        /// </summary>
        /// <returns></returns>
        Task AddEntity();

        /// <summary>
        /// Refreshes the data source of the entity table
        /// </summary>
        /// <returns></returns>
        Task Refresh();

        /// <summary>
        /// Removes entity from database
        /// </summary>
        /// <returns></returns>
        Task DeleteEntity();

        /// <summary>
        /// Closes the delete dialog without saving changes to the database
        /// </summary>
        void CancelDelete();

        /// <summary>
        /// Flags if the delete dialog is open
        /// </summary>
        bool DeleteDialogIsOpen { get; set; }

        /// <summary>
        /// Opens the delete dialog
        /// </summary>
        /// <param name="entityId">Entity id to be deleted</param>
        void OpenDeleteDialog(int entityId);
    }
}
