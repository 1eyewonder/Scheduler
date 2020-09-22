using SchedulerAPI.Dtos;
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
    /// Service for handling communication with api in regards to projects
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Gets list of projects from database
        /// </summary>
        /// <returns></returns>
        Task<PaginationResponseDto<Project>> GetProjects(int recordsPerPage = 10, int pageNumber = 1);

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

        /// <summary>
        /// Adds a new project to the database
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> AddProject(ProjectDto project);

        /// <summary>
        /// Removes the given project from the database
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteProject(int projectId);
    }
}
