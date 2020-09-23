using AutoMapper;
using Blazored.LocalStorage;
using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchedulerAPI.Profiles;
using SchedulerUI.Data;
using SchedulerUI.Services;
using SchedulerUI.Services.Interfaces;
using SchedulerUI.ViewModels.Customers;
using SchedulerUI.ViewModels.Interfaces;
using SchedulerUI.ViewModels.Jobs;
using SchedulerUI.ViewModels.Projects;
using SchedulerUI.ViewModels.UserManagement;
using System.Net.Http;

namespace SchedulerUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            //Nuget services
            services.AddBlazoredLocalStorage();

            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });

            //Automapper profiles
            services.AddAutoMapper(typeof(SchedulerProfile)); //profile found in api
            services.AddScoped<IMapper, Mapper>();

            //My services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ICustomerService, CustomerService>();

            //ViewModel services
            services.AddScoped<ILoginViewModel, LoginViewModel>();
            services.AddScoped<IJobsViewModel, JobsViewModel>();
            services.AddScoped<IEditJobViewModel, EditJobViewModel>();
            services.AddScoped<IProjectsViewModel, ProjectsViewModel>();
            services.AddScoped<IEditProjectViewModel, EditProjectViewModel>();
            services.AddScoped<ICustomersViewModel, CustomersViewModel>();
            services.AddScoped<IEditCustomerViewModel, EditCustomerViewModel>();
           
            //Http services
            services.AddSingleton<HttpClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {         
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}