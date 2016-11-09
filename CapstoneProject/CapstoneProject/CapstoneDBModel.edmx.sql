
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/08/2016 13:27:35
-- Generated from EDMX file: C:\Users\Karlo\Source\Repos\Capstone\CapstoneProject\CapstoneProject\CapstoneDBModel.edmx
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
IF OBJECT_ID(N'[dbo].[FK_ProjectCriteria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_ProjectCriteria];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupSkillset]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Skillsets] DROP CONSTRAINT [FK_GroupSkillset];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentSkillset]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Skillsets] DROP CONSTRAINT [FK_StudentSkillset];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientFeedback]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feedbacks] DROP CONSTRAINT [FK_ClientFeedback];
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
IF OBJECT_ID(N'[dbo].[Criteria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Criteria];
GO
IF OBJECT_ID(N'[dbo].[Skillsets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Skillsets];
GO
IF OBJECT_ID(N'[dbo].[Feedbacks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feedbacks];
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
    [Pin] nchar(5)  NOT NULL,
    [Owner] int  NULL
);
GO

-- Creating table 'Programs'
CREATE TABLE [dbo].[Programs] (
    [ProgramId] int IDENTITY(1,1) NOT NULL,
    [ProgramName] nvarchar(25)  NOT NULL
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
    [ClientUserId] int  NOT NULL,
    [Criteria_Id] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(25)  NOT NULL,
    [LastName] nvarchar(25)  NOT NULL,
    [PhoneNumber] nvarchar(10)  NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Type] nvarchar(25)  NULL,
    [Lock] bit  NOT NULL
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
    [ProgramId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [Group_GroupId] int  NULL
);
GO

-- Creating table 'Criteria'
CREATE TABLE [dbo].[Criteria] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Goal] nvarchar(max)  NOT NULL,
    [Storage] bit  NOT NULL,
    [Application] bit  NOT NULL,
    [Website] bit  NOT NULL,
    [Mobile] bit  NOT NULL,
    [StorageComment] nvarchar(max)  NULL,
    [ApplicationComment] nvarchar(max)  NULL,
    [WebsiteComment] nvarchar(max)  NULL,
    [MobileComment] nvarchar(max)  NULL
);
GO

-- Creating table 'Skillsets'
CREATE TABLE [dbo].[Skillsets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CSharp] float  NOT NULL,
    [Java] float  NOT NULL,
    [Database] float  NOT NULL,
    [WebDev] float  NOT NULL,
    [MobileDev] float  NOT NULL,
    [ApplDev] float  NOT NULL,
    [UIDesign] float  NOT NULL,
    [Group_GroupId] int  NULL,
    [Student_UserId] int  NULL
);
GO

-- Creating table 'Feedbacks'
CREATE TABLE [dbo].[Feedbacks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [Rating] float  NOT NULL,
    [ClientUserId] int  NOT NULL
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

-- Creating primary key on [Id] in table 'Criteria'
ALTER TABLE [dbo].[Criteria]
ADD CONSTRAINT [PK_Criteria]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Skillsets'
ALTER TABLE [dbo].[Skillsets]
ADD CONSTRAINT [PK_Skillsets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [PK_Feedbacks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
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

-- Creating foreign key on [Criteria_Id] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_ProjectCriteria]
    FOREIGN KEY ([Criteria_Id])
    REFERENCES [dbo].[Criteria]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectCriteria'
CREATE INDEX [IX_FK_ProjectCriteria]
ON [dbo].[Projects]
    ([Criteria_Id]);
GO

-- Creating foreign key on [Group_GroupId] in table 'Skillsets'
ALTER TABLE [dbo].[Skillsets]
ADD CONSTRAINT [FK_GroupSkillset]
    FOREIGN KEY ([Group_GroupId])
    REFERENCES [dbo].[Groups]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupSkillset'
CREATE INDEX [IX_FK_GroupSkillset]
ON [dbo].[Skillsets]
    ([Group_GroupId]);
GO

-- Creating foreign key on [Student_UserId] in table 'Skillsets'
ALTER TABLE [dbo].[Skillsets]
ADD CONSTRAINT [FK_StudentSkillset]
    FOREIGN KEY ([Student_UserId])
    REFERENCES [dbo].[Users_Student]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentSkillset'
CREATE INDEX [IX_FK_StudentSkillset]
ON [dbo].[Skillsets]
    ([Student_UserId]);
GO

-- Creating foreign key on [ClientUserId] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [FK_ClientFeedback]
    FOREIGN KEY ([ClientUserId])
    REFERENCES [dbo].[Users_Client]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientFeedback'
CREATE INDEX [IX_FK_ClientFeedback]
ON [dbo].[Feedbacks]
    ([ClientUserId]);
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