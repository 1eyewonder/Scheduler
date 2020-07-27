using SchedulerAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.Interfaces
{
    public interface ILoginViewModel : IViewModelState
    {
        /// <summary>
        /// Dto for transferring user information to database
        /// </summary>
        public UserLoginDto UserLogin { get; set; }

        /// <summary>
        /// Dto for transferring user registration information to database
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// String for verifying password upon registration
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Verifies the user's login credentials
        /// </summary>
        /// <returns></returns>
        Task Login();

        /// <summary>
        /// Logs user out and removes credentials from local storage
        /// </summary>
        /// <returns></returns>
        Task LogOut();

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <returns></returns>
        Task Register();

    }
}
