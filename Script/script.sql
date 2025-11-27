USE [master]
GO
/****** Object:  Database [ClinicAppointmentSystem]    Script Date: 11-26-2025 20:55:27 ******/
CREATE DATABASE [ClinicAppointmentSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClinicAppointmentSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ClinicAppointmentSystem.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ClinicAppointmentSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ClinicAppointmentSystem_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ClinicAppointmentSystem] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClinicAppointmentSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ClinicAppointmentSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET  MULTI_USER 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ClinicAppointmentSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ClinicAppointmentSystem] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ClinicAppointmentSystem]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 11-26-2025 20:55:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[AppointmentId] [int] IDENTITY(1,1) NOT NULL,
	[DoctorId] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
	[AppointmentDateTime] [datetime] NULL,
	[DurationInMinutes] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 11-26-2025 20:55:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[DoctorId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Specialization] [nvarchar](200) NULL,
	[Qualification] [nvarchar](200) NULL,
	[Gender] [nvarchar](10) NULL,
	[IsActive] [bit] NULL DEFAULT ((1)),
	[IsDeleted] [bit] NULL DEFAULT ((0)),
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[DoctorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Patient]    Script Date: 11-26-2025 20:55:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[PatientId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[FileNo] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](10) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Appointment] ON 

INSERT [dbo].[Appointment] ([AppointmentId], [DoctorId], [PatientId], [AppointmentDateTime], [DurationInMinutes], [Status], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (1, 1, 1, CAST(N'2025-11-26 15:00:00.000' AS DateTime), 30, N'Cancelled', 1, 0, N'Affaf', CAST(N'2025-11-26 14:20:03.077' AS DateTime), N'Affaf', CAST(N'2025-11-26 17:27:34.677' AS DateTime))
INSERT [dbo].[Appointment] ([AppointmentId], [DoctorId], [PatientId], [AppointmentDateTime], [DurationInMinutes], [Status], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (2, 2, 4, CAST(N'2025-11-26 16:00:00.000' AS DateTime), 60, N'Scheduled', 1, 0, N'Affaf', CAST(N'2025-11-26 15:44:38.620' AS DateTime), NULL, NULL)
INSERT [dbo].[Appointment] ([AppointmentId], [DoctorId], [PatientId], [AppointmentDateTime], [DurationInMinutes], [Status], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (3, 3, 5, CAST(N'2025-11-26 10:30:00.000' AS DateTime), 30, N'Scheduled', 1, 0, N'Affaf', CAST(N'2025-11-26 17:29:18.770' AS DateTime), NULL, NULL)
INSERT [dbo].[Appointment] ([AppointmentId], [DoctorId], [PatientId], [AppointmentDateTime], [DurationInMinutes], [Status], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (4, 3, 4, CAST(N'2025-11-27 15:30:00.000' AS DateTime), 60, N'Scheduled', 1, 0, N'Affaf', CAST(N'2025-11-26 17:31:05.503' AS DateTime), NULL, NULL)
INSERT [dbo].[Appointment] ([AppointmentId], [DoctorId], [PatientId], [AppointmentDateTime], [DurationInMinutes], [Status], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (5, 3, 4, CAST(N'2025-11-27 10:30:00.000' AS DateTime), 30, N'Scheduled', 1, 0, N'Affaf', CAST(N'2025-11-26 17:36:38.303' AS DateTime), NULL, NULL)
INSERT [dbo].[Appointment] ([AppointmentId], [DoctorId], [PatientId], [AppointmentDateTime], [DurationInMinutes], [Status], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (6, 2, 6, CAST(N'2025-11-28 12:30:00.000' AS DateTime), 30, N'Scheduled', 1, 0, N'Affaf', CAST(N'2025-11-26 19:30:37.967' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Appointment] OFF
SET IDENTITY_INSERT [dbo].[Doctor] ON 

INSERT [dbo].[Doctor] ([DoctorId], [Name], [Specialization], [Qualification], [Gender], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (1, N'Dr. Abdulla Karim', N'Cardiology', N'MBBS, MD', N'Male', 1, 0, N'Affaf', CAST(N'2025-11-25 22:54:47.570' AS DateTime), NULL, NULL)
INSERT [dbo].[Doctor] ([DoctorId], [Name], [Specialization], [Qualification], [Gender], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (2, N'Dr. Sara Khan', N'Dermatology', N'MBBS, DDVL', N'Female', 1, 0, N'Affaf', CAST(N'2025-11-25 22:54:47.570' AS DateTime), NULL, NULL)
INSERT [dbo].[Doctor] ([DoctorId], [Name], [Specialization], [Qualification], [Gender], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (3, N'Dr. Sajjad Khan ', N'Orthopedics', N'MBBS, MS Ortho', N'Male', 1, 0, N'Affaf', CAST(N'2025-11-25 22:54:47.570' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Doctor] OFF
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([PatientId], [Name], [Phone], [FileNo], [Gender], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (1, N'Fayyaz Ur Rahman Shaikh', N'9421025542', N'PFNo-2025-0001', N'Male', 1, 0, N'Affaf', CAST(N'2025-11-26 11:00:37.997' AS DateTime), N'Affaf', CAST(N'2025-11-26 15:52:13.723' AS DateTime))
INSERT [dbo].[Patient] ([PatientId], [Name], [Phone], [FileNo], [Gender], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (4, N'Sana kulsum', N'9421025542', N'PFNo-2025-0002', N'Female', 1, 0, N'Affaf', CAST(N'2025-11-26 11:16:27.300' AS DateTime), NULL, NULL)
INSERT [dbo].[Patient] ([PatientId], [Name], [Phone], [FileNo], [Gender], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (5, N'Derick', N'9130600500', N'PFNo-2025-0003', N'Male', 1, 0, N'Affaf', CAST(N'2025-11-26 15:53:08.377' AS DateTime), NULL, NULL)
INSERT [dbo].[Patient] ([PatientId], [Name], [Phone], [FileNo], [Gender], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (6, N'Erick James Parker', N'8999523725', N'PFNo-2025-0004', N'Male', 1, 0, N'Affaf', CAST(N'2025-11-26 19:20:14.173' AS DateTime), N'Affaf', CAST(N'2025-11-26 19:21:37.683' AS DateTime))
INSERT [dbo].[Patient] ([PatientId], [Name], [Phone], [FileNo], [Gender], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (7, N'Ali', N'8999523725', N'PFNo-2025-0005', N'Male', 1, 0, N'Affaf', CAST(N'2025-11-26 19:50:27.780' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Patient] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Patient__6F0CEAD9C3EA07D5]    Script Date: 11-26-2025 20:55:27 ******/
ALTER TABLE [dbo].[Patient] ADD UNIQUE NONCLUSTERED 
(
	[FileNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Doctor] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctor] ([DoctorId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Doctor]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([PatientId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Patient]
GO
USE [master]
GO
ALTER DATABASE [ClinicAppointmentSystem] SET  READ_WRITE 
GO
