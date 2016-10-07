
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/06/2016 20:12:21
-- Generated from EDMX file: C:\Users\karlo\Source\Repos\Capstone\CapstoneProject\CapstoneProject\CapstoneDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Capstone];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_StudentCoop]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Coops] DROP CONSTRAINT [FK_StudentCoop];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_ClientProject];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectGroup_Project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectGroup] DROP CONSTRAINT [FK_ProjectGroup_Project];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectGroup_Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectGroup] DROP CONSTRAINT [FK_ProjectGroup_Group];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Student] DROP CONSTRAINT [FK_StudentGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_Admin_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Admin] DROP CONSTRAINT [FK_Admin_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Client_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Client] DROP CONSTRAINT [FK_Client_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Coop_Advisor_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Coop_Advisor] DROP CONSTRAINT [FK_Coop_Advisor_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Management_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Management] DROP CONSTRAINT [FK_Management_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Student_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Student] DROP CONSTRAINT [FK_Student_inherits_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Coops]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Coops];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[Programs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Programs];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Users_Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Admin];
GO
IF OBJECT_ID(N'[dbo].[Users_Client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Client];
GO
IF OBJECT_ID(N'[dbo].[Users_Coop_Advisor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Coop_Advisor];
GO
IF OBJECT_ID(N'[dbo].[Users_Management]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Management];
GO
IF OBJECT_ID(N'[dbo].[Users_Student]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Student];
GO
IF OBJECT_ID(N'[dbo].[ProjectGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectGroup];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Coops'
CREATE TABLE [dbo].[Coops] (
    [CoopId] int IDENTITY(1,1) NOT NULL,
    [CompanyName] nvarchar(25)  NOT NULL,
    [JobTitle] nvarchar(25)  NOT NULL,
    [Description] nvarchar(255)  NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [Comments] nvarchar(255)  NULL,
    [StudentUserId] int  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [GroupId] int IDENTITY(1,1) NOT NULL,
    [GroupName] nvarchar(25)  NOT NULL,
    [Description] nvarchar(255)  NULL,
    [Status] nvarchar(25)  NULL,
    [Pin] nchar(5)  NOT NULL
);
GO

-- Creating table 'Programs'
CREATE TABLE [dbo].[Programs] (
    [ProgramId] int IDENTITY(1,1) NOT NULL,
    [ProgramName] nvarchar(25)  NOT NULL,
    [Campus] nvarchar(25)  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ProjectId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(25)  NOT NULL,
    [Type] nvarchar(25)  NOT NULL,
    [Description] nvarchar(255)  NOT NULL,
    [StartDate] datetime  NULL,
    [State] nvarchar(max)  NOT NULL,
    [Grade] int  NULL,
    [DateCompleted] datetime  NULL,
    [ClientUserId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(25)  NOT NULL,
    [LastName] nvarchar(25)  NOT NULL,
    [PhoneNumber] nvarchar(10)  NULL,
    [Email] nvarchar(25)  NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Title] nvarchar(25)  NULL
);
GO

-- Creating table 'Users_Admin'
CREATE TABLE [dbo].[Users_Admin] (
    [AdminId] int IDENTITY(1,1) NOT NULL,
    [ProgramId] int  NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Users_Client'
CREATE TABLE [dbo].[Users_Client] (
    [ClientId] int IDENTITY(1,1) NOT NULL,
    [CompanyName] nvarchar(25)  NOT NULL,
    [CompanyAddress] nvarchar(50)  NOT NULL,
    [CompanyDescription] nvarchar(255)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Users_Coop_Advisor'
CREATE TABLE [dbo].[Users_Coop_Advisor] (
    [CoopAdvisorId] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Users_Management'
CREATE TABLE [dbo].[Users_Management] (
    [ManagementId] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Users_Student'
CREATE TABLE [dbo].[Users_Student] (
    [StudentId] int IDENTITY(1,1) NOT NULL,
    [StudentNumber] int  NOT NULL,
    [Interests] nvarchar(150)  NULL,
    [ProgramId] int  NULL,
    [UserId] int  NOT NULL,
    [Group_GroupId] int  NULL
);
GO

-- Creating table 'ProjectGroup'
CREATE TABLE [dbo].[ProjectGroup] (
    [Projects_ProjectId] int  NOT NULL,
    [Groups_GroupId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CoopId] in table 'Coops'
ALTER TABLE [dbo].[Coops]
ADD CONSTRAINT [PK_Coops]
    PRIMARY KEY CLUSTERED ([CoopId] ASC);
GO

-- Creating primary key on [GroupId] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([GroupId] ASC);
GO

-- Creating primary key on [ProgramId] in table 'Programs'
ALTER TABLE [dbo].[Programs]
ADD CONSTRAINT [PK_Programs]
    PRIMARY KEY CLUSTERED ([ProgramId] ASC);
GO

-- Creating primary key on [ProjectId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ProjectId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users_Admin'
ALTER TABLE [dbo].[Users_Admin]
ADD CONSTRAINT [PK_Users_Admin]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users_Client'
ALTER TABLE [dbo].[Users_Client]
ADD CONSTRAINT [PK_Users_Client]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users_Coop_Advisor'
ALTER TABLE [dbo].[Users_Coop_Advisor]
ADD CONSTRAINT [PK_Users_Coop_Advisor]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users_Management'
ALTER TABLE [dbo].[Users_Management]
ADD CONSTRAINT [PK_Users_Management]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users_Student'
ALTER TABLE [dbo].[Users_Student]
ADD CONSTRAINT [PK_Users_Student]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [Projects_ProjectId], [Groups_GroupId] in table 'ProjectGroup'
ALTER TABLE [dbo].[ProjectGroup]
ADD CONSTRAINT [PK_ProjectGroup]
    PRIMARY KEY CLUSTERED ([Projects_ProjectId], [Groups_GroupId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StudentUserId] in table 'Coops'
ALTER TABLE [dbo].[Coops]
ADD CONSTRAINT [FK_StudentCoop]
    FOREIGN KEY ([StudentUserId])
    REFERENCES [dbo].[Users_Student]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentCoop'
CREATE INDEX [IX_FK_StudentCoop]
ON [dbo].[Coops]
    ([StudentUserId]);
GO

-- Creating foreign key on [ClientUserId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_ClientProject]
    FOREIGN KEY ([ClientUserId])
    REFERENCES [dbo].[Users_Client]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProject'
CREATE INDEX [IX_FK_ClientProject]
ON [dbo].[Projects]
    ([ClientUserId]);
GO

-- Creating foreign key on [Projects_ProjectId] in table 'ProjectGroup'
ALTER TABLE [dbo].[ProjectGroup]
ADD CONSTRAINT [FK_ProjectGroup_Project]
    FOREIGN KEY ([Projects_ProjectId])
    REFERENCES [dbo].[Projects]
        ([ProjectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Groups_GroupId] in table 'ProjectGroup'
ALTER TABLE [dbo].[ProjectGroup]
ADD CONSTRAINT [FK_ProjectGroup_Group]
    FOREIGN KEY ([Groups_GroupId])
    REFERENCES [dbo].[Groups]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectGroup_Group'
CREATE INDEX [IX_FK_ProjectGroup_Group]
ON [dbo].[ProjectGroup]
    ([Groups_GroupId]);
GO

-- Creating foreign key on [Group_GroupId] in table 'Users_Student'
ALTER TABLE [dbo].[Users_Student]
ADD CONSTRAINT [FK_StudentGroup]
    FOREIGN KEY ([Group_GroupId])
    REFERENCES [dbo].[Groups]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentGroup'
CREATE INDEX [IX_FK_StudentGroup]
ON [dbo].[Users_Student]
    ([Group_GroupId]);
GO

-- Creating foreign key on [UserId] in table 'Users_Admin'
ALTER TABLE [dbo].[Users_Admin]
ADD CONSTRAINT [FK_Admin_inherits_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'Users_Client'
ALTER TABLE [dbo].[Users_Client]
ADD CONSTRAINT [FK_Client_inherits_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'Users_Coop_Advisor'
ALTER TABLE [dbo].[Users_Coop_Advisor]
ADD CONSTRAINT [FK_Coop_Advisor_inherits_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'Users_Management'
ALTER TABLE [dbo].[Users_Management]
ADD CONSTRAINT [FK_Management_inherits_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'Users_Student'
ALTER TABLE [dbo].[Users_Student]
ADD CONSTRAINT [FK_Student_inherits_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------