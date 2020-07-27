using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    /// <summary>
    /// Marks a type as requiring asynchronous initialization and provides the result of that initialization
    /// </summary>
    /// The initialization is started in the constructor when we call InitializeAsync
    /// The completion of the initialization is exposed via the Initialization property
    /// Any exceptions raised from the async initialization will be captured and placed on the Initialization property
    public interface IAsyncInitialization
    {
        /// <summary>
        /// The result of the asynchronous initialization of this instance
        /// </summary>
        Task Initialization { get; }
    }
}
