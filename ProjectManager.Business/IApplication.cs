using ProjectManager.Entities;
using System.Collections.Generic;

namespace ProjectManager.Business
{
    public interface IApplication
    {
        void AddUser(UserModel userModel);
        List<UserModel> GetUsers();
        void UpdateUser(UserModel userModel);
        void DeleteUser(int uiserId);
        void AddProject(ProjectModel project);
        List<ProjectModel> GetProjects();
        void EndProject(int projectId);
        UserModel GetSpecificUser(int userId);
        void UpdateProject(ProjectModel prj);
        void AddParentTask(string task);
        List<ParentTaskModel> GetParentTasks();
        List<TaskModel> GetTasks();
        void AddTask(TaskModel taskModel);
        void UpdateTask(TaskModel taskModel);
        void EndTask(int taskId);
        TaskModel GetSpecificTask(int taskId);
    }
}
