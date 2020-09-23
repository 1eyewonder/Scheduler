using SchedulerAPI.Models;
using SchedulerUI.Services.Interfaces;
using SchedulerUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchedulerUI.ViewModels.Customers
{
    public class CustomersViewModel: ICustomersViewModel
    {
        #region Fields
        private readonly ICustomerService _customerService;
        private int _currentCustomerId;
        #endregion

        #region Properties
        public List<Customer> CustomersList { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsRunning { get; set; }
        public Task Initialization { get; private set; }
        public int TotalPageQuantity { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public bool DeleteDialogIsOpen { get; set; }
        #endregion

        public CustomersViewModel(ICustomerService customerService)
        {
            // Inject services
            _customerService = customerService;

            // Initialize
            CustomersList = new List<Customer>();
            ErrorMessage = null;
            IsRunning = false;
            CurrentPage = 1;
            PageSize = 10;

            //Retrieves data async
            Initialization = InitializeAsync();
        }

        /// <summary>
        /// Method executed upon class initialization to allow for async data access
        /// </summary>
        /// <returns></returns>
        private async Task InitializeAsync()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            //Get jobs from database
            try
            {
                CustomersList = await GetCustomers();
            }
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
            }

            //Re-enables actions
            IsRunning = false;
        }

        public async Task SelectedPage(int page)
        {
            CurrentPage = page;
            CustomersList = await GetCustomers(10, page);
        }

        private async Task<List<Customer>> GetCustomers(int recordsPerPage = 10, int pageNumber = 1)
        {
            var response = await _customerService.GetCustomers(recordsPerPage, pageNumber);
            TotalPageQuantity = response.TotalPagesQuantity;
            return response.Items;
        }

        public async Task Refresh()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            //Get jobs from database
            try
            {
                CustomersList = await GetCustomers(10, CurrentPage);
            }
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
            }

            //Re-enables actions
            IsRunning = false;
        }

        public void OpenDeleteDialog(int entityId)
        {
            DeleteDialogIsOpen = true;
            _currentCustomerId = entityId;
        }

        /// <summary>
        /// Deletes specified job from the database
        /// </summary>
        /// <returns></returns>
        public async Task DeleteEntity()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            try
            {
                //Attempts to update job in the database
                var response = await _customerService.DeleteCustomer(_currentCustomerId);

                //If http call was successful
                if (response.IsSuccessStatusCode)
                {
                    DeleteDialogIsOpen = false;
                    await Refresh();
                }

                //If http call was not successful
                else
                {
                    ErrorMessage = response.Content.ReadAsAsync<HttpError>().Result.ToString();
                }
            }

            //If other error occurred
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
            }

            //Re-enables actions
            IsRunning = false;
        }

        public void CancelDelete()
        {
            IsRunning = false;
            ErrorMessage = null;
            DeleteDialogIsOpen = false;
        }

        public Task AddEntity()
        {
            throw new NotImplementedException();
        }
    }
}
