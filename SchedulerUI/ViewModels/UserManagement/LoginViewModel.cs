using Microsoft.AspNetCore.Components;
using SchedulerAPI.Dtos;
using SchedulerUI.Services.Interfaces;
using SchedulerUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.ViewModels.UserManagement
{
    public class LoginViewModel : ILoginViewModel
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly NavigationManager _navigation;
        #endregion

        #region Properties
        public UserLoginDto UserLogin { get; set; }
        public UserDto User { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsRunning { get; set; }
        public string ConfirmPassword { get; set; }

        #endregion

        public LoginViewModel(IUserService userService, NavigationManager navigation)
        {
            //Inject services
            _userService = userService;
            _navigation = navigation;

            //Default login for testing
            UserLogin = new UserLoginDto()
            {
                Name = "admin",
                Password = "password"
            };

            //Initialize
            User = new UserDto();
            IsRunning = false;
            ErrorMessage = null;

            //Removes previous user data from local storage upon open
            _userService.LogOut();
        }

        public async Task Login()
        {
            // Removes error messages and disables additional actions
            IsRunning = true;
            ErrorMessage = null;

            try
            {
                //Verifies user credentials
                var result = await _userService.Login(UserLogin);

                //If successful, navigate to home page
                if (result.IsSuccessStatusCode)
                {
                    _navigation.NavigateTo("/");
                }
                else
                {
                    ErrorMessage = result.ReasonPhrase;
                }
            }

            // Catches issues such as not being able to connect to server
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
            }

            // Re-enables actions
            IsRunning = false;
        }

        public async Task Register()
        {
            // Removes error messages and disables additional actions
            IsRunning = true;
            ErrorMessage = null;

            //Confirms passwords match to ensure user registers the correct password
            if (ConfirmPassword == User.Password)
            {
                //Posts new user to database
                var result = await _userService.Register(User);

                //If successful, navigates to login page
                if (result.IsSuccessStatusCode)
                {
                    _navigation.NavigateTo("/login");
                }
                else
                {
                    ErrorMessage = result.StatusCode.ToString();
                }
            }
            else
            {
                ErrorMessage = "Passwords do not match. Please, try again.";
            }

            // Re-enables actions
            IsRunning = false;
        }

        public async Task LogOut()
        {
            // Removes error messages and disables additional actions
            IsRunning = true;
            ErrorMessage = null;

            // Logs user out and clears local storage of user data
            await _userService.LogOut();

            // Re-enables actions
            IsRunning = false;
        }
    }
}
