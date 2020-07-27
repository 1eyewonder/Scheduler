using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    /// <summary>
    /// Interface for state visibility
    /// </summary>
    public interface IViewModelState
    {
        /// <summary>
        /// Error message, if any
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// States if actions are executing to prevent multiple simultaneous calls
        /// </summary>
        public bool IsRunning { get; set; }
    }
}
