using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using SchedulerAPI.Models;
using SchedulerUI.Dtos;
using SchedulerUI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerUI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _storageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public CustomerService(HttpClient httpClient,
            ILocalStorageService storageService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _storageService = storageService;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<PaginationResponseDto<Customer>> GetCustomers(int recordsPerPage = 10, int pageNumber = 1)
        {
            // Awaits response from database
            var response = await _httpClient.GetAsync($"Customers/GetCustomers?quantityPerPage={recordsPerPage}&page={pageNumber}");

            // If successful
            if (response.IsSuccessStatusCode)
            {
                var totalPageQuantity = int.Parse(response.Headers.GetValues("pagesQuantity").FirstOrDefault());
                var customers = JsonConvert.DeserializeObject<List<Customer>>(response.Content.ReadAsStringAsync().Result);

                // Creates dto from response
                var paginationResponse = new PaginationResponseDto<Customer>()
                {
                    TotalPagesQuantity = totalPageQuantity,
                    Items = customers
                };

                // Returns dto
                return paginationResponse;
            }
            else
            {
                return null;
            }
        }
        public async Task<Customer> GetCustomer(int id)
        {
            // Attempts to get project data from database
            var response = await _httpClient.GetAsync("customers/" + id);

            // If response is successful
            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<Customer>(response.Content.ReadAsStringAsync().Result);
                return customer;
            }
            else
            {
                return null;
            }
        }
        public async Task<HttpResponseMessage> UpdateCustomer(Customer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("customers/" + customer.Id, data);

            return response;
        }

        public async Task<HttpResponseMessage> AddCustomer(Customer customer)
        {
            // Converts user object to json for http post
            var json = JsonConvert.SerializeObject(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Calls web api job post
            var response = await _httpClient.PostAsync("customers/", data);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteCustomer(int customerId)
        {
            // Attempts to delete job from database
            var response = await _httpClient.DeleteAsync($"customers/{customerId}");

            // Returns response
            return response;
        }   
    }
}
