using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchedulerUI.Services.Interfaces
{
    /// <summary>
    /// Service for handling communication with api in regards to projects
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Gets list of projects from database
        /// </summary>
        /// <returns></returns>
        Task<List<Project>> GetProjects();

        /// <summary>
        /// Gets project with given id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Project> GetProject(int id);

        /// <summary>
        /// Updates given project in the database
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> UpdateProject(ProjectDto project);
    }
}
