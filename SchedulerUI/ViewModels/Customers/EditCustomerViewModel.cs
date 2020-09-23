using AutoMapper;
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
    public class EditCustomerViewModel : IEditCustomerViewModel
    {
        #region Fields
        private readonly ICustomerService _customerService;
        private readonly ICustomersViewModel _customersViewModel;
        #endregion

        #region Properties
        public Customer Customer { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsRunning { get; set; }
        public bool EditDialogIsOpen { get; set; }

        #endregion

        public EditCustomerViewModel(ICustomerService customerService,
            ICustomersViewModel customersViewModel)
        {
            _customerService = customerService;
            _customersViewModel = customersViewModel;

            //Initialize
            Customer = new Customer();
            IsRunning = false;
            ErrorMessage = null;
            EditDialogIsOpen = false;
        }

        public void OpenEditDialog(Customer customer)
        {
            Customer = customer;
            EditDialogIsOpen = true;
        }

        public async Task SaveChanges()
        {
            //Disables actions and clears any error messages
            IsRunning = true;
            ErrorMessage = null;

            try
            {
                //Attempts to update job in the database
                var response = await _customerService.UpdateCustomer(Customer);

                //If http call was successful
                if (response.IsSuccessStatusCode)
                {
                    EditDialogIsOpen = false;
                    await _customersViewModel.Refresh();
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

        public void Cancel()
        {
            IsRunning = false;
            ErrorMessage = null;
            EditDialogIsOpen = false;
        }
    }
}
