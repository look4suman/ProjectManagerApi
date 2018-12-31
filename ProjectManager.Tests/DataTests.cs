using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using System.Data.Entity;
using ProjectManager.Data;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class DataTests
    {
        [Test]
        public void GetTasksData()
        {
            var data = new List<Task>
            {
                new Task { TaskName = "BBB" },
                new Task { TaskName = "ZZZ" },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Task>>();
            mockSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Tasks).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            var tasks = service.GetTasks();

            Assert.AreEqual(data.Count(), tasks.Count());
            Assert.AreEqual(data.First().TaskName, tasks.First().TaskName);
        }
        [Test]

        public void GetProjectData()
        {
            var data = new List<Project>
            {
                new Project { ProjectName = "BBB" },
                new Project { ProjectName = "ZZZ" },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Project>>();
            mockSet.As<IQueryable<Project>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Projects).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            var projects = service.GetProjects();

            Assert.AreEqual(data.Count(), projects.Count());
            Assert.AreEqual(data.First().ProjectName, projects.First().ProjectName);
        }

        [Test]

        public void GetUsersData()
        {
            var data = new List<User>
            {
                new User { FirstName = "BBB" },
                new User { FirstName = "ZZZ" },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            var users = service.GetUsers();

            Assert.AreEqual(data.Count(), users.Count());
            Assert.AreEqual(data.First().FirstName, users.First().FirstName);
        }
        [Test]

        public void GetParentData()
        {
            var data = new List<ParentTask>
            {
                new ParentTask { TaskName = "BBB" },
                new ParentTask { TaskName = "ZZZ" },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<ParentTask>>();
            mockSet.As<IQueryable<ParentTask>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ParentTask>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ParentTask>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ParentTask>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.ParentTasks).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            var ptasks = service.GetParentTasks();

            Assert.AreEqual(data.Count(), ptasks.Count());
            Assert.AreEqual(data.First().TaskName, ptasks.First().TaskName);
        }


        [Test]
        public void GetSpecificTasksData()
        {
            var data = new List<Task>
            {
                new Task { TaskName = "BBB", Task_ID=5 },
                new Task { TaskName = "ZZZ" },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Task>>();
            mockSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Tasks).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            var task = service.GetSpecificTask(5);

            Assert.AreEqual(data.First().TaskName, task.TaskName);
        }

        [Test]
        public void GetSpecificUserData()
        {
            var data = new List<User>
            {
                new User { FirstName = "BBB", Employee_ID=5, User_ID=5 },
                new User { FirstName = "ZZZ", Employee_ID=6 },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            var usr = service.GetSpecificUser(5);

            Assert.AreEqual(data.First().FirstName, usr.FirstName);
        }

        [Test]
        public void AddTaskData()
        {
            var mockSet = new Mock<DbSet<Task>>();

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            service.AddTask(new Task() { });

            mockSet.Verify(m => m.Add(It.IsAny<Task>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void AddUserData()
        {
            var mockSet = new Mock<DbSet<User>>();

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            service.AddUser(new User() { });

            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void AddProjectData()
        {
            var mockSet = new Mock<DbSet<Project>>();

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            service.AddProject(new Project() { });

            mockSet.Verify(m => m.Add(It.IsAny<Project>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void AddParentTask()
        {
            var mockSet = new Mock<DbSet<ParentTask>>();

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(m => m.ParentTasks).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            service.AddParentTask("Test");

            mockSet.Verify(m => m.Add(It.IsAny<ParentTask>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void UpdateTaskData()
        {
            var mockSet = new Mock<DbSet<Task>>();
            var data = new List<Task>
            {
                new Task { TaskName = "BBB", Task_ID=5 },
                new Task { TaskName = "ZZZ" },

            }.AsQueryable();


            mockSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Tasks).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            service.UpdateTask(It.IsAny<Task>());

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void UpdateProjectData()
        {
            var mockSet = new Mock<DbSet<Project>>();
            var data = new List<Project>
            {
                new Project { ProjectName = "BBB", Project_ID=5 },
                new Project { ProjectName = "ZZZ" },

            }.AsQueryable();


            mockSet.As<IQueryable<Project>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Projects).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            service.UpdateProject(data.FirstOrDefault());

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void UpdateUserData()
        {
            var mockSet = new Mock<DbSet<User>>();
            var data = new List<User>
            {
                new User { FirstName = "BBB", Employee_ID=5 },
                new User { FirstName = "ZZZ", Employee_ID =6 },

            }.AsQueryable();


            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            service.UpdateUser(data.FirstOrDefault());

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void EndTaskData()
        {
            var mockSet = new Mock<DbSet<Task>>();
            var data = new List<Task>
            {
                new Task { TaskName = "BBB", Task_ID=5 },
                new Task { TaskName = "ZZZ" },

            }.AsQueryable();

            mockSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Tasks).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            service.EndTask(It.IsAny<int>());

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void EndProjectData()
        {
            var mockSet = new Mock<DbSet<Project>>();
            var data = new List<Project>
            {
                new Project { ProjectName = "BBB", Project_ID=5 },
                new Project { ProjectName = "ZZZ" },

            }.AsQueryable();

            mockSet.As<IQueryable<Project>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Projects).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            service.EndProject(It.IsAny<int>());

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }


        [Test]
        public void DeleteUserData()
        {
            var mockSet = new Mock<DbSet<User>>();
            var data = new List<User>
            {
                new User { FirstName = "BBB", User_ID=5 },
                new User { FirstName = "ZZZ" },

            }.AsQueryable();

            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<LocalDBEntities>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            service.DeleteUser(5);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
