using ProjectManager.Business;
using ProjectManager.Entities;
using System.Collections.Generic;
using System.Web.Http;

namespace ProjectManager.Api.Controllers
{
    [RoutePrefix("api")]
    public class ApplicationController : ApiController
    {
        private IApplication _application;

        public ApplicationController():this(new Application()) { }
        public ApplicationController(IApplication application)
        {
            _application = application;
        }

        [HttpPost]
        [Route("AddUser")]
        public void AddUser(UserModel user)
        {
            _application.AddUser(user);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public void UpdateUser(UserModel user)
        {
            _application.UpdateUser(user);
        }

        [HttpDelete]
        [Route("DeleteUser/{userId}")]
        public void DeleteUser(int userId)
        {
            _application.DeleteUser(userId);
        }

        [HttpGet]
        [Route("User/{userId}")]
        public UserModel GetUser(int userId)
        {
            return _application.GetSpecificUser(userId);
        }

        [HttpGet]
        [Route("GetUser")]
        public List<UserModel> GetUser()
        {
            return _application.GetUsers();
        }

        [HttpPost]
        [Route("AddProject")]
        public void AddProject(ProjectModel prj)
        {
            _application.AddProject(prj);
        }

        [HttpPost]
        [Route("UpdateProject")]
        public void UpdateProject(ProjectModel prj)
        {
            _application.UpdateProject(prj);
        }

        [HttpGet]
        [Route("GetProject")]
        public List<ProjectModel> GetProject()
        {
            return _application.GetProjects();
        }

        [HttpPut]
        [Route("EndProject")]
        public void EndProject(ProjectModel project)
        {
            _application.EndProject(project.Project_ID);
        }

        [HttpPost]
        [Route("AddParentTask")]
        public void AddParentTask(ParentTaskModel pt)
        {
            _application.AddParentTask(pt.Parent_Task);
        }

        [HttpPost]
        [Route("AddTask")]
        public void AddTask(TaskModel task)
        {
            _application.AddTask(task);
        }

        [HttpGet]
        [Route("GetParentTasks")]
        public List<ParentTaskModel> GetParentTasks()
        {
            return _application.GetParentTasks();
        }

        [HttpGet]
        [Route("GetTasks")]
        public List<TaskModel> GetTasks()
        {
            return _application.GetTasks();
        }

        [HttpPut]
        [Route("EndTask")]
        public void EndTask(TaskModel task)
        {
            _application.EndTask(task.Task_ID);
        }

        [HttpGet]
        [Route("Task/{taskId}")]
        public TaskModel GetSpecificTask(int taskId)
        {
            return _application.GetSpecificTask(taskId);
        }

        [HttpPost]
        [Route("UpdateTask")]
        public void UpdateTask(TaskModel tsk)
        {
            _application.UpdateTask(tsk);
        }
    }
}
