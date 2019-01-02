using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.Data
{
    public class Repository : IRepository
    {
        private LocalDBEntities _entity;

        public Repository() : this(new LocalDBEntities()) { }
        public Repository(LocalDBEntities entity)
        {
            _entity = entity;
        }

        public void AddUser(User user)
        {
            _entity.Users.Add(user);
            _entity.SaveChanges();
        }

        public List<User> GetUsers()
        {
            return _entity.Users.ToList();
        }

        public void UpdateUser(User user)
        {
            var usr = _entity.Users.FirstOrDefault(x => x.User_ID == user.User_ID);
            usr.FirstName = user.FirstName;
            usr.LastName = user.LastName;
            usr.Employee_ID = user.Employee_ID;
            _entity.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            _entity.Users.Remove(_entity.Users.FirstOrDefault(x => x.User_ID == userId));
            _entity.SaveChanges();
        }

        public User GetSpecificUser(int userId)
        {
            return _entity.Users.FirstOrDefault(x => x.User_ID == userId);
        }

        public void AddProject(Project project, int? userId = null, int? taskId = null)
        {
            if (userId.HasValue)
            {
                var user = _entity.Users.FirstOrDefault(x => x.User_ID == userId.Value);
                if (user != null)
                {
                    project.Users.Add(user);
                }
            }

            if (taskId.HasValue)
            {
                project.Tasks.Add(_entity.Tasks.FirstOrDefault(x => x.Task_ID == taskId.GetValueOrDefault()));
            }
            _entity.Projects.Add(project);
            _entity.SaveChanges();

        }

        public void UpdateProject(Project prj, int? userId = null)
        {
            User user = null;
            var project = _entity.Projects.FirstOrDefault(x => x.Project_ID == prj.Project_ID);
            project.ProjectName = prj.ProjectName;
            project.Priority = prj.Priority;
            project.StartDate = prj.StartDate;
            project.EndDate = prj.EndDate;
            if (userId.HasValue && userId.Value > 0)
            {
                user = _entity.Users.FirstOrDefault(x => x.User_ID == userId.Value);
                project.Users.Add(user);
            }
            _entity.SaveChanges();
        }

        public List<Project> GetProjects()
        {
            return _entity.Projects.ToList();
        }

        public void EndProject(int projectId)
        {
            var project = _entity.Projects.FirstOrDefault(x => x.Project_ID == projectId);
            project.EndDate = DateTime.Now;
            _entity.SaveChanges();
        }

        public List<ParentTask> GetParentTasks()
        {
            return _entity.ParentTasks.ToList();
        }

        public List<Task> GetTasks()
        {
            return _entity.Tasks.ToList();
        }

        public void AddParentTask(string taskTitle)
        {
            var parent = new ParentTask
            {
                TaskName = taskTitle
            };
            _entity.ParentTasks.Add(parent);
            _entity.SaveChanges();
        }

        public void AddTask(Task task, int? parentId = null, int? userId = null, int? projectId = null)
        {
            if (userId.HasValue && userId.Value > 0)
            {
                var user = _entity.Users.FirstOrDefault(x => x.User_ID == userId.Value);
                if (user != null)
                {
                    task.Users.Add(user);
                }                
            }

            task.Project_ID = projectId;

            if (parentId.HasValue && parentId.Value > 0)
            {
                task.Parent_ID = parentId;
            }

            _entity.Tasks.Add(task);
            _entity.SaveChanges();
        }

        public void UpdateTask(Task task, int? parentId = null, int? userId = null)
        {
            if (userId.HasValue && userId.Value > 0)
            {
                var user = _entity.Users.FirstOrDefault(x => x.User_ID == userId.Value);
                if (user != null)
                {
                    task.Users.Add(user);
                }                
            }

            if (parentId.HasValue && parentId.Value > 0)
            {
                task.Parent_ID = parentId;
            }

            _entity.SaveChanges();
        }

        public void EndTask(int taskId)
        {
            var task = _entity.Tasks.FirstOrDefault(x => x.Task_ID == taskId);
            task.Status = "Complete";
            _entity.SaveChanges();
        }

        public Task GetSpecificTask(int taskId)
        {
            var task = _entity.Tasks.FirstOrDefault(x => x.Task_ID == taskId);
            return task;
        }
    }
}
