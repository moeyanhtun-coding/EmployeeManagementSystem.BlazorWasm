USE [master]
GO
/****** Object:  Database [EmployeeManagement]    Script Date: 5/12/2025 6:23:55 PM ******/
CREATE DATABASE [EmployeeManagement] ON  PRIMARY 
( NAME = N'EmployeeManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EmployeeManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EmployeeManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EmployeeManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmployeeManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmployeeManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmployeeManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmployeeManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmployeeManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmployeeManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmployeeManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EmployeeManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmployeeManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmployeeManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmployeeManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmployeeManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmployeeManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmployeeManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmployeeManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmployeeManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EmployeeManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmployeeManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmployeeManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmployeeManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmployeeManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmployeeManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmployeeManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmployeeManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EmployeeManagement] SET  MULTI_USER 
GO
ALTER DATABASE [EmployeeManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmployeeManagement] SET DB_CHAINING OFF 
GO
USE [EmployeeManagement]
GO
/****** Object:  Table [dbo].[Tbl_AttendanceLog]    Script Date: 5/12/2025 6:23:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_AttendanceLog](
	[AttendanceId] [int] IDENTITY(1,1) NOT NULL,
	[AttendanceCode] [nvarchar](max) NOT NULL,
	[EmployeeCode] [nvarchar](max) NOT NULL,
	[LogTime] [datetime] NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Tbl_AttendanceLog] PRIMARY KEY CLUSTERED 
(
	[AttendanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Employee]    Script Date: 5/12/2025 6:23:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](14) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[DepartmentCode] [nvarchar](max) NOT NULL,
	[PositionCode] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[DateOfJoining] [datetime] NOT NULL,
 CONSTRAINT [PK_Tbl_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_RefreshToken]    Script Date: 5/12/2025 6:23:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_RefreshToken](
	[RefreshTokenId] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [nvarchar](max) NOT NULL,
	[RefreshToken] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Tbl_RefreshToken] PRIMARY KEY CLUSTERED 
(
	[RefreshTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Role]    Script Date: 5/12/2025 6:23:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tbl_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_User]    Script Date: 5/12/2025 6:23:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Tbl_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_UserRole]    Script Date: 5/12/2025 6:23:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_UserRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [nvarchar](max) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Tbl_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tbl_AttendanceLog] ON 

INSERT [dbo].[Tbl_AttendanceLog] ([AttendanceId], [AttendanceCode], [EmployeeCode], [LogTime], [Type]) VALUES (1, N'AID-N5KBPGSE0J', N'EID-R3XSQ4E76S', CAST(N'2025-05-09T20:23:44.917' AS DateTime), N'CheckIn')
INSERT [dbo].[Tbl_AttendanceLog] ([AttendanceId], [AttendanceCode], [EmployeeCode], [LogTime], [Type]) VALUES (2, N'AID-NHWB55FFRW', N'EID-R3XSQ4E76S', CAST(N'2025-05-09T20:30:27.320' AS DateTime), N'CheckOut')
INSERT [dbo].[Tbl_AttendanceLog] ([AttendanceId], [AttendanceCode], [EmployeeCode], [LogTime], [Type]) VALUES (3, N'AID-Q489RWG15S', N'EID-R3XSQ4E76S', CAST(N'2025-05-09T20:57:58.003' AS DateTime), N'CheckOut')
SET IDENTITY_INSERT [dbo].[Tbl_AttendanceLog] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl_Employee] ON 

INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (34, N'EID-R3XSQ4E76S', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (35, N'EID-R3XYH0P049', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (36, N'EID-R3Y387CVPW', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (37, N'EID-R3Y8FT3HNB', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (38, N'EID-R3YCVBWX5A', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (39, N'EID-R3YHDJEVER', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (40, N'EID-R3YNXV9RD9', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (41, N'EID-R3YTE79B9Y', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (42, N'EID-R3YYAW10VH', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (43, N'EID-R3Z1VRSZCH', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (44, N'EID-R3Z6DBS06K', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (45, N'EID-R3ZAYD66ZB', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (46, N'EID-R3ZG0M8CMC', N'string', N'string', N'user@example.com', N'1234567909', N'string', N'string', N'string', CAST(N'2025-04-27T13:48:38.880' AS DateTime), CAST(N'2025-04-27T13:48:38.880' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (47, N'EID-RHBVAJVHR5', N'Moe Yan', N'Htun', N'moeyanhtun@gmail.com', N'09890630456', N'Myeik Nout Lge', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (48, N'EID-STAHMZGXHX', N'Moe Yan', N'Htun', N'moeyanhtun@gmail.com', N'09890630456', N'Myeik Nout Lge', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (49, N'EID-SX1A747KDM', N'Moe Yan', N'Htun', N'moeyanhtun@gmail.com', N'09890630456', N'Myeik Nout Lge', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (50, N'EID-SXGTDN2343', N'Moe Yan', N'Htun', N'moeyanhtun@gmail.com', N'09890630456', N'Myeik Nout Lge', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (51, N'EID-SY1KG4SR1F', N'Moe Yan', N'Htun', N'moeyanhtun@gmail.com', N'09890630456', N'Myeik Nout Lge', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (53, N'EID-T2W8VKGFZ8', N'Moe Yan', N'Htun', N'moeyanhtun@gmail.com', N'09890630456', N'Myeik Nout Lge', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (54, N'EID-T5F19D3M8D', N'Moe Yan', N'Htun', N'moeyanhtun@gmail.com', N'09890630456', N'Myeik Nout Lge', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (55, N'EID-T7QWVRD4DE', N'Moe Yan', N'Htun', N'moeyanhtun@gmail.com', N'09890630456', N'Myeik Nout Lge', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (56, N'EID-TCK47E1MDG', N'Moe Yan', N'Htun', N'moeyanhtun@gmail.com', N'09890630456', N'Myeik Nout Lge', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (57, N'EID-TE8EE92ZMA', N'Moe Yan', N'Htun', N'moeyanhtun@gmail.com', N'09890630456', N'Myeik Nout Lge', N'IT', N'Team Lead', CAST(N'2025-05-09T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (59, N'EID-V1E6JY0FG6', N'update', N'update', N'moeyanhtun@gmail.com', N'4651328465', N'Yangon Myeik', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (60, N'EID-V2K4D292B8', N'moe yan htun12', N'121', N'moeyanhtun@gmail.com', N'4651328465', N'Yangon Myeik', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (61, N'EID-V71TJD3F1T', N'moe yan htun12', N'121', N'moeyanhtun@gmail.com', N'4651328465', N'Yangon Myeik', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (62, N'EID-V7BHHMXXB7', N'moe yan htun12', N'121', N'moeyanhtun@gmail.com', N'4651328465', N'Yangon Myeik', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (63, N'EID-V7MWMCZVMB', N'moe yan htun12', N'121', N'moeyanhtun@gmail.com', N'4651328465', N'Yangon Myeik', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (64, N'EID-V7YXFGSB0R', N'Update Update', N'121', N'moeyanhtun@gmail.com', N'4651328465', N'Yangon Myeik', N'IT', N'Team Lead', CAST(N'2025-04-27T00:00:00.000' AS DateTime), CAST(N'2025-04-27T00:00:00.000' AS DateTime))
INSERT [dbo].[Tbl_Employee] ([EmployeeId], [EmployeeCode], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [DepartmentCode], [PositionCode], [DateOfBirth], [DateOfJoining]) VALUES (66, N'EID-RG3D35B7RE', N'Moe Yan', N'Yan Yan', N'moeyanhtun@gmail.com', N'09890630456', N'Yangon Myeik', N'IT', N'Team Lead', CAST(N'2025-05-06T00:00:00.000' AS DateTime), CAST(N'2025-05-06T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Tbl_Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl_RefreshToken] ON 

INSERT [dbo].[Tbl_RefreshToken] ([RefreshTokenId], [UserCode], [RefreshToken]) VALUES (1, N'UID-9VDK998BD7', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiV2luIFNhbmRhciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoic2FuZGFyQGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiVUlELTlWREs5OThCRDciLCJleHAiOjE3NDY4MDA0MDksImlzcyI6Im1vZVlhbiIsImF1ZCI6Im1vZVlhbiJ9.mJcb89iL-QOHdnqHzWbfWkfgS5V9vMXvavHhtgPVor8')
SET IDENTITY_INSERT [dbo].[Tbl_RefreshToken] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl_Role] ON 

INSERT [dbo].[Tbl_Role] ([RoleId], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Tbl_Role] ([RoleId], [RoleName]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Tbl_Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl_User] ON 

INSERT [dbo].[Tbl_User] ([UserId], [UserCode], [UserName], [Email], [Password], [CreatedAt], [UpdatedAt]) VALUES (1, N'UID-9VDK998BD7', N'Win Sandar', N'sandar@gmail.com', N'$2a$11$8Y92e.hyzoT0ifU1SexaeO1HECh4tl7wHtlZh4QaaP80gPUAU1To.', CAST(N'2025-05-03T21:17:20.707' AS DateTime), CAST(N'2025-05-03T21:17:20.707' AS DateTime))
SET IDENTITY_INSERT [dbo].[Tbl_User] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl_UserRole] ON 

INSERT [dbo].[Tbl_UserRole] ([UserRoleId], [UserCode], [RoleId]) VALUES (1, N'UID-9VDK998BD7', 1)
SET IDENTITY_INSERT [dbo].[Tbl_UserRole] OFF
GO
USE [master]
GO
ALTER DATABASE [EmployeeManagement] SET  READ_WRITE 
GO
