using System;
using System.Collections.Generic;
using System.Text;

namespace COVID_19_LFT_Logging_System
{
    class BuildScript
    {
        public const string DATABASE_BUILD_SCRIPT = @"
CREATE TABLE [dbo].[Patient] (
    [PatientID]       INT          IDENTITY (1, 1) NOT NULL,
    [FirstName]       TEXT         NOT NULL,
    [Surname]         TEXT         NOT NULL,
    [DateOfBirth]     DATE         NOT NULL,
    [GenderID]        INT          NOT NULL,
    [EthnicGroupID]   INT          NOT NULL,
    [NHSNumber]       CHAR (10)    NULL,
    [CountryID]       INT          NOT NULL,
    [Postcode]        VARCHAR (10) NOT NULL,
    [Address]         TEXT         NOT NULL,
    [CurrentlyInWork] BIT          NOT NULL,
    [AreaOfWorkID]    INT          NULL,
    [OccupationID]    INT          NULL,
    [Employer]        TEXT         NULL,
    [EmailAddress]    TEXT         NOT NULL,
    [MobileNumber]    VARCHAR (13) NULL,
    [PatientGroupID]  INT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED ([PatientID] ASC)
);


CREATE TABLE [dbo].[Test] (
    [TestID]              INT          IDENTITY (1, 1) NOT NULL,
    [Barcode]             VARCHAR (11) NOT NULL,
    [DateOfTest]          DATETIME     NOT NULL,
    [DailyContactTesting] BIT          NOT NULL,
    [ShowingSymptoms]     BIT          NOT NULL,
    [SymptomsDate]        DATE         NULL,
    [PatientID]           INT          NULL,
    UNIQUE NONCLUSTERED ([Barcode] ASC),
    CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED ([TestID] ASC)
);


CREATE TABLE [dbo].[PatientGroup] (
    [PatientGroupID]          INT          IDENTITY (0,1) NOT NULL,
    [PatientGroupName]        VARCHAR (50) DEFAULT ('unknown group') NOT NULL,
    [PatientGroupDescription] TEXT         NULL,
    CONSTRAINT [PK_PatientGroup] PRIMARY KEY CLUSTERED ([PatientGroupID] ASC)
);
INSERT INTO [dbo].[PatientGroup] ([PatientGroupName]) VALUES ('unknown group');


CREATE TABLE [dbo].[AreaOfWork] (
    [AreaOfWorkID] INT  NOT NULL,
    [String]       TEXT NOT NULL,
    CONSTRAINT [PK_AreaOfWork] PRIMARY KEY CLUSTERED ([AreaOfWorkID] ASC)
);
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (0, N'Prefer not to say')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (1, N'Teaching and education')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (2, N'Health and social care')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (3, N'Transport')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (4, N'Retail')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (5, N'Hospitality')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (6, N'Hair and beauty professionals')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (7, N'Information and communication')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (8, N'Financial services and insurance')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (9, N'Manufacturing or construction')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (10, N'Civil services or local government')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (11, N'Arts, entertainment or recreation')
INSERT INTO [dbo].[AreaOfWork] ([AreaOfWorkID], [String]) VALUES (12, N'Other')


CREATE TABLE [dbo].[Country] (
    [CountryID] INT  NOT NULL,
    [String]    TEXT NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([CountryID] ASC)
);
INSERT INTO [dbo].[Country] ([CountryID], [String]) VALUES (0, N'England')
INSERT INTO [dbo].[Country] ([CountryID], [String]) VALUES (1, N'Scotland')
INSERT INTO [dbo].[Country] ([CountryID], [String]) VALUES (2, N'Northern Ireland')
INSERT INTO [dbo].[Country] ([CountryID], [String]) VALUES (3, N'Wales')


CREATE TABLE [dbo].[EthnicGroup] (
    [EthnicGroupID] INT  NOT NULL,
    [String]        TEXT NOT NULL,
    CONSTRAINT [PK_EthnicGroup] PRIMARY KEY CLUSTERED ([EthnicGroupID] ASC)
);
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (0, N'Prefer not to say')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (1, N'Bangladeshi')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (2, N'Chinese')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (3, N'Indian')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (4, N'Pakistani')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (5, N'Another Asian background')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (6, N'Asian or Asian British')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (7, N'African')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (8, N'Caribbean')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (9, N'Another Black background')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (10, N'Black, African, Black British or Caribbean')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (11, N'Asian and White')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (12, N'Black African and White')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (13, N'Another Mixed background')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (14, N'Mixed or multiple ethnic groups')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (15, N'British, English, Northern Irish, Scottish, or Welsh')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (16, N'Irish')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (17, N'Irish Traveller or Gypsy')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (18, N'Another White background')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (19, N'White')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (20, N'Arab')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (21, N'Another ethnic background')
INSERT INTO [dbo].[EthnicGroup] ([EthnicGroupID], [String]) VALUES (22, N'Another ethnic group')


CREATE TABLE [dbo].[Gender] (
    [GenderID] INT         NOT NULL,
    [String]   VARCHAR (6) NOT NULL,
    CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED ([GenderID] ASC)
);
INSERT INTO [dbo].[Gender] ([GenderID], [String]) VALUES (0, N'Male')
INSERT INTO [dbo].[Gender] ([GenderID], [String]) VALUES (1, N'Female')


CREATE TABLE [dbo].[Occupation] (
    [OccupationID] INT  NOT NULL,
    [String]       TEXT NOT NULL,
    CONSTRAINT [PK_Occupation] PRIMARY KEY CLUSTERED ([OccupationID] ASC)
);
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (0, N'Prefer not to say')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (1, N'4111 - National government administrative occupation')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (2, N'1172 - Social services manager or director')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (3, N'1231 - Health care practice manager')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (4, N'1232 - Residential, day or domiciliary care manager or proprietor')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (5, N'2211 - Generalist medical practitioner')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (6, N'2212 - Specialist medical practitioner')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (7, N'2253 - Dental practitioner')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (8, N'4159 - Other administrative occupation')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (9, N'1231 - Health care practice managers')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (10, N'2232 - Community nurses')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (11, N'2234 - Nurse practitioners')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (12, N'2235 - Mental health nurses')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (13, N'2461 - Social worker')
INSERT INTO [dbo].[Occupation] ([OccupationID], [String]) VALUES (14, N'0000 - I can''t find my occupation')

";
    }
}
