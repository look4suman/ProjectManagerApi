CREATE TABLE ParentTask
(
  Parent_ID int primary key identity(1,1), 
  TaskName varchar(400)
);

CREATE TABLE Project
(
  Project_ID int primary key identity(1,1), 
  ProjectName varchar(400) not null,
  StartDate datetime,
  EndDate datetime,
  [Priority] int
);

CREATE TABLE Task
(
  Task_ID int primary key identity(1,1), 
  TaskName varchar(400), 
  [Status] varchar(400), 
  [Priority] int,
  Parent_ID int,
  Project_ID int,
  [Start_Date] datetime,
  End_Date datetime,
  FOREIGN KEY(Parent_ID) REFERENCES ParentTask(Parent_ID),
  FOREIGN KEY(Project_ID) REFERENCES Project(Project_ID),
);

CREATE TABLE [User]
(
  [User_ID] int primary key identity(1,1), 
  FirstName varchar(400), 
  LastName varchar(400), 
  Employee_ID int,
  Task_ID int,
  Project_ID int,
  FOREIGN KEY(Task_ID) REFERENCES Task(Task_ID),
  FOREIGN KEY(Project_ID) REFERENCES Project(Project_ID),
);


