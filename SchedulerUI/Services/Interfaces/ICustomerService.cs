using SchedulerAPI.Models;
using SchedulerUI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchedulerUI.Services.Interfaces
{
    /// <summary>
    /// Service for handling communication with api in regards to customers
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Gets list of customers from the database
        /// </summary>
        /// <param name="recordsPerPage"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<PaginationResponseDto<Customer>> GetCustomers(int recordsPerPage = 10, int pageNumber = 1);

        /// <summary>
        /// Gets customer with given id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Customer> GetCustomer(int id);
        
        /// <summary>
        /// Updates given customer in the database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> UpdateCustomer(Customer customer);

        /// <summary>
        /// Adds customer to the database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> AddCustomer(Customer customer);

        /// <summary>
        /// Removes given customer from the database
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteCustomer(int customerId);
    }
}
