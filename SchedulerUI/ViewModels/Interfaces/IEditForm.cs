using Blazored.Modal;
using Blazored.Modal.Services;
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
        Task Cancel();

        /// <summary>
        /// Closes the edit form and sends a result, if any
        /// </summary>
        /// <param name="modalResult"></param>
        /// <returns></returns>
        Task Close(ModalResult modalResult = null);

        /// <summary>
        /// Injects data from view into view model to allow manipulation of model data and modal accessibility
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task InitializeAsync(int id, BlazoredModalInstance blazoredModal);
    }
}
