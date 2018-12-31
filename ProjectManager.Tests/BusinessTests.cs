using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using ProjectManager.Data;
using ProjectManager.Business;
using ProjectManager.Entities;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class BusinessTests
    {
        [Test]
        public void GetTasks()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            var tasks = new List<Task>();
            tasks.Add(new Task() { TaskName = "Test", Start_Date = DateTime.Now, End_Date = DateTime.Now.AddDays(7), Priority = 7 });
            mockObj.Setup(x => x.GetTasks()).Returns(tasks);
            var actualTasks = application.GetTasks();
            Assert.AreEqual(tasks.Count(), actualTasks.Count());
            Assert.AreEqual(tasks.FirstOrDefault().TaskName, actualTasks.FirstOrDefault().Task);
        }
        [Test]
        public void GetProjects()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            var projects = new List<Project>();
            projects.Add(new Project() { ProjectName = "Test", Priority = 5, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2) });
            mockObj.Setup(x => x.GetProjects()).Returns(projects);
            var actualProjects = application.GetProjects();
            Assert.AreEqual(projects.Count(), actualProjects.Count());
            Assert.AreEqual(projects.FirstOrDefault().ProjectName, actualProjects.FirstOrDefault().Project);
        }

        [Test]
        public void GetUsers()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            var users = new List<User>();
            users.Add(new User() { FirstName = "Test", LastName = "Test", Employee_ID = 459430 });
            mockObj.Setup(x => x.GetUsers()).Returns(users);
            var actualUsers = application.GetUsers();
            Assert.AreEqual(users.Count(), actualUsers.Count());
            Assert.AreEqual(users.FirstOrDefault().FirstName, actualUsers.FirstOrDefault().FirstName);
        }

        [Test]
        public void GetParentTasks()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            var ptasks = new List<ParentTask>();
            ptasks.Add(new ParentTask() { TaskName = "PTask" });
            mockObj.Setup(x => x.GetParentTasks()).Returns(ptasks);
            var actualParents = application.GetParentTasks();
            Assert.AreEqual(ptasks.Count(), actualParents.Count());
            Assert.AreEqual(ptasks.FirstOrDefault().TaskName, actualParents.FirstOrDefault().Parent_Task);
        }

        [Test]
        public void GetSpecificTask()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            var tasks = new Task() { TaskName = "TestTest", Start_Date = DateTime.Now, End_Date = DateTime.Now.AddDays(7), Parent_ID = 5 };
            mockObj.Setup(x => x.GetSpecificTask(It.IsAny<int>())).Returns(tasks);
            var actualTasks = application.GetSpecificTask(It.IsAny<int>());
            Assert.AreEqual(tasks.TaskName, actualTasks.Task);

        }

        [Test]
        public void GetSpecificUser()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            var users = new User() { FirstName = "Test", LastName = "Test", Employee_ID = 459430 };
            mockObj.Setup(x => x.GetSpecificUser(It.IsAny<int>())).Returns(users);
            var actualUsers = application.GetSpecificUser(It.IsAny<int>());
            Assert.AreEqual(users.FirstName, actualUsers.FirstName);
        }

        [Test]
        public void EndTask()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            application.EndTask(5);
            mockObj.Verify(x => x.EndTask(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void EndProject()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            application.EndProject(5);
            mockObj.Verify(x => x.EndProject(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void AddTask()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            application.AddTask(new TaskModel() { Task_ID = 5 });
            mockObj.Verify(x => x.AddTask(It.IsAny<Task>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>()), Times.Once);
        }

        [Test]
        public void UpdateTask()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            var taskModel = new TaskModel() { Task_ID = 5, Priority = 2, StartDate = DateTime.Now.ToString(), EndDate = DateTime.Now.AddDays(7).ToString() };
            mockObj.Setup(x => x.GetSpecificTask(It.IsAny<int>())).Returns(new Task() { TaskName = "test" });
            application.UpdateTask(taskModel);
            mockObj.Verify(x => x.GetSpecificTask(It.IsAny<int>()), Times.Once);
            mockObj.Verify(x => x.UpdateTask(It.IsAny<Task>(), It.IsAny<int?>(), It.IsAny<int?>()), Times.Once);
        }

        [Test]
        public void DeleteUser()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            application.DeleteUser(5);
            mockObj.Verify(x => x.DeleteUser(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void AddUser()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            application.AddUser(new UserModel() { User_ID = 5 });
            mockObj.Verify(x => x.AddUser(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void UpdateUser()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            application.UpdateUser(new UserModel() { User_ID = 5 });
            mockObj.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void AddProject()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            application.AddProject(new ProjectModel() { Project_ID = 5 });
            mockObj.Verify(x => x.AddProject(It.IsAny<Project>(), It.IsAny<int?>(), It.IsAny<int?>()), Times.Once);
        }

        [Test]
        public void UpdateProject()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            application.UpdateProject(new ProjectModel() { Project_ID=5});
            mockObj.Verify(x => x.UpdateProject(It.IsAny<Project>(), It.IsAny<int?>()), Times.Once);
        }

        [Test]
        public void AddParentTask()
        {
            var mockObj = new Mock<IRepository>();
            var application = new Application(mockObj.Object);
            application.AddParentTask("test");
            mockObj.Verify(x => x.AddParentTask(It.IsAny<string>()), Times.Once);
        }
    }
}
