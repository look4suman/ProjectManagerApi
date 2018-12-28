using ProjectManager.Data;
using ProjectManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.Business
{
    public class Application
    {
        private IRepository _repository;

        public Application() : this(new Repository()) { }
        public Application(IRepository repository)
        {
            _repository = repository;
        }

        public void AddUser(UserModel userModel)
        {
            var user = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Employee_ID = userModel.EmployeeId
            };
            _repository.AddUser(user);
        }

        public List<UserModel> GetUsers()
        {
            var users = _repository.GetUsers();
            var userModels = new List<UserModel>();
            foreach (var usr in users)
            {
                var usrModel = new UserModel()
                {
                    User_ID = usr.User_ID,
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    EmployeeId = usr.Employee_ID.GetValueOrDefault()
                };
                userModels.Add(usrModel);
            }
            return userModels;
        }

        public void UpdateUser(UserModel userModel)
        {
            var user = new User
            {
                User_ID = userModel.User_ID,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Employee_ID = userModel.EmployeeId
            };
            _repository.UpdateUser(user);
        }

        public void DeleteUser(int uiserId)
        {
            _repository.DeleteUser(uiserId);
        }

        public void AddProject(ProjectModel project)
        {
            var projectDb = new Project
            {
                ProjectName = project.Project,
                Priority = project.Priority
            };

            if (project.StartDate != null)
                projectDb.StartDate = Convert.ToDateTime(project.StartDate);

            if (project.EndDate != null)
                projectDb.EndDate = Convert.ToDateTime(project.EndDate);

            _repository.AddProject(projectDb, project.UserId);
        }

        public List<ProjectModel> GetProjects()
        {
            var projects = _repository.GetProjects();
            var projectModels = new List<ProjectModel>();

            foreach (var pr in projects)
            {
                var prModel = new ProjectModel
                {
                    Project = pr.ProjectName,
                    Project_ID = pr.Project_ID,
                    Priority = pr.Priority.GetValueOrDefault(),
                    StartDate = pr.StartDate.ToString(),
                    EndDate = pr.EndDate.ToString(),
                    TaskCount = pr.Tasks.Count(),
                    UserId = pr.Users.Count() > 0 ? pr.Users.FirstOrDefault().User_ID : 0,
                    CompletedTasks = pr.Tasks != null ? pr.Tasks.Count(x => x.Status == "Complete") : 0
                };
                projectModels.Add(prModel);
            }
            return projectModels;
        }

        public void EndProject(int projectId)
        {
            _repository.EndProject(projectId);
        }

        public UserModel GetSpecificUser(int userId)
        {
            var userDb = _repository.GetSpecificUser(userId);
            var user = new UserModel() { FirstName = userDb.FirstName, LastName = userDb.LastName, EmployeeId = userDb.Employee_ID.GetValueOrDefault(), User_ID = userDb.User_ID };
            return user;
        }

        public void UpdateProject(ProjectModel prj)
        {
            Project project = new Project
            {
                Project_ID = prj.Project_ID,
                ProjectName = prj.Project,
                Priority = prj.Priority,
                StartDate = Convert.ToDateTime(prj.StartDate),
                EndDate = Convert.ToDateTime(prj.EndDate)
            };
            _repository.UpdateProject(project, prj.UserId);
        }

        public void AddParentTask(string task)
        {
            _repository.AddParentTask(task);
        }

        public List<ParentTaskModel> GetParentTasks()
        {
            var parentTasks = _repository.GetParentTasks();
            var result = new List<ParentTaskModel>();

            foreach (var pt in parentTasks)
            {
                var ptask = new ParentTaskModel() { Parent_ID = pt.Parent_ID, Parent_Task = pt.TaskName };
                result.Add(ptask);
            }
            return result;
        }

        public List<TaskModel> GetTasks()
        {
            var tasks = _repository.GetTasks();
            var result = new List<TaskModel>();
            foreach (var ts in tasks)
            {
                var ptask = new TaskModel()
                {
                    Task_ID = ts.Task_ID,
                    Task = ts.TaskName,
                    Parent_ID = ts.Parent_ID.GetValueOrDefault(),
                    Project_ID = ts.Project_ID.GetValueOrDefault(),
                    User_ID = ts.Users.FirstOrDefault() != null ? ts.Users.FirstOrDefault().User_ID : 0,
                    StartDate = Convert.ToString(ts.Start_Date),
                    EndDate = Convert.ToString(ts.End_Date),
                    Priority = ts.Priority.GetValueOrDefault(),
                    TaskStatus = ts.Status,
                    IsEditable = string.IsNullOrEmpty(ts.Status) ? true : ts.Status == "Complete" ? false : true,
                    ParentTask = ts.ParentTask != null ? ts.ParentTask.TaskName : "This task has no parent."
                };
                result.Add(ptask);
            }
            return result;
        }

        public void AddTask(TaskModel taskModel)
        {
            var task = new Task
            {
                TaskName = taskModel.Task,
                Priority = taskModel.Priority
            };

            if (taskModel.StartDate != null)
                task.Start_Date = Convert.ToDateTime(taskModel.StartDate);

            if (taskModel.EndDate != null)
                task.End_Date = Convert.ToDateTime(taskModel.EndDate);

            _repository.AddTask(task, taskModel.Parent_ID, taskModel.User_ID, taskModel.Project_ID);
        }

        public void UpdateTask(TaskModel taskModel)
        {
            var task = _repository.GetSpecificTask(taskModel.Task_ID);
            task.TaskName = taskModel.Task;
            task.Priority = taskModel.Priority;

            if (taskModel.StartDate != null)
                task.Start_Date = Convert.ToDateTime(taskModel.StartDate);

            if (taskModel.EndDate != null)
                task.End_Date = Convert.ToDateTime(taskModel.EndDate);

            _repository.UpdateTask(task, taskModel.Parent_ID, taskModel.User_ID);
        }

        public void EndTask(int taskId)
        {
            _repository.EndTask(taskId);
        }

        public TaskModel GetSpecificTask(int taskId)
        {
            var task = _repository.GetSpecificTask(taskId);
            var taskModel = new TaskModel()
            {
                Task = task.TaskName,
                Task_ID = task.Task_ID,
                Priority = task.Priority.GetValueOrDefault(),
                StartDate = task.Start_Date == null ? string.Empty : task.Start_Date.Value.ToString(),
                EndDate = task.End_Date == null ? string.Empty : task.End_Date.Value.ToString(),
                ParentTask = task.ParentTask == null ? string.Empty : task.ParentTask.TaskName,
                Parent_ID = task.Parent_ID.GetValueOrDefault(),
                Project_ID = task.Project_ID.GetValueOrDefault(),
                User_ID = task.Users.FirstOrDefault() == null ? 0 : task.Users.FirstOrDefault().User_ID,
                Project = task.Project == null ? string.Empty : task.Project.ProjectName,
                User = task.Users.FirstOrDefault() == null ? string.Empty : task.Users.FirstOrDefault().FirstName + ' ' + task.Users.FirstOrDefault().LastName
            };
            return taskModel;
        }
    }
}
