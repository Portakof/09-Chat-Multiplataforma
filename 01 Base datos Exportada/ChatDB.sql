USE [master]
GO
/****** Object:  Database [ChatDB]    Script Date: 18/10/2019 10:07:37 p. m. ******/
CREATE DATABASE [ChatDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ChatDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\ChatDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ChatDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\ChatDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ChatDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ChatDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ChatDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ChatDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ChatDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ChatDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ChatDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ChatDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ChatDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ChatDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ChatDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ChatDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ChatDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ChatDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ChatDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ChatDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ChatDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ChatDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ChatDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ChatDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ChatDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ChatDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ChatDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ChatDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ChatDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ChatDB] SET  MULTI_USER 
GO
ALTER DATABASE [ChatDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ChatDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ChatDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ChatDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ChatDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ChatDB', N'ON'
GO
ALTER DATABASE [ChatDB] SET QUERY_STORE = OFF
GO
USE [ChatDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ChatDB]
GO
/****** Object:  Table [dbo].[cState]    Script Date: 18/10/2019 10:07:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cState](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
 CONSTRAINT [PK_cState] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[message]    Script Date: 18/10/2019 10:07:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[message](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idRoom] [int] NOT NULL,
	[idUser] [int] NOT NULL,
	[text] [varchar](1000) NOT NULL,
	[date_create] [datetime] NOT NULL,
	[idState] [int] NOT NULL,
 CONSTRAINT [PK_message] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[room]    Script Date: 18/10/2019 10:07:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[room](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[idState] [int] NOT NULL,
	[date_create] [datetime] NOT NULL,
	[description] [varchar](200) NOT NULL,
 CONSTRAINT [PK_room] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 18/10/2019 10:07:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[idState] [int] NOT NULL,
	[date_create] [datetime] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[city] [varchar](50) NULL,
	[access_token] [varchar](50) NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cState] ON 

INSERT [dbo].[cState] ([id], [name]) VALUES (1, N'Activo')
INSERT [dbo].[cState] ([id], [name]) VALUES (2, N'Inactivo')
INSERT [dbo].[cState] ([id], [name]) VALUES (3, N'Eliminado')
INSERT [dbo].[cState] ([id], [name]) VALUES (4, N'Borrador')
SET IDENTITY_INSERT [dbo].[cState] OFF
SET IDENTITY_INSERT [dbo].[message] ON 

INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (30, 3, 3007, N'no entendi', CAST(N'2019-10-06T19:23:52.017' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (59, 4, 3007, N'que pasa', CAST(N'2019-10-06T19:33:10.757' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (60, 4, 3007, N'esta raro', CAST(N'2019-10-06T19:33:17.507' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (61, 1, 1, N'nueva prueba omeeeee', CAST(N'2019-10-06T20:14:32.067' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (62, 3, 3007, N'ahora si veo algo falta', CAST(N'2019-10-06T20:14:46.327' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (63, 1, 3007, N'seguir revisando', CAST(N'2019-10-06T20:15:10.113' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (64, 1, 3007, N'hay un problemita', CAST(N'2019-10-06T20:15:20.990' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (65, 1, 1, N'es necesario revisalro', CAST(N'2019-10-06T20:15:32.793' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (66, 4, 1, N'aca no pasa', CAST(N'2019-10-06T20:16:00.943' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (67, 1, 3007, N'parece que porfin', CAST(N'2019-10-09T23:45:18.870' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (68, 1, 1, N'esto es un gran avance', CAST(N'2019-10-09T23:45:35.000' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (69, 4, 3007, N'ahora si pasa', CAST(N'2019-10-09T23:46:10.353' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (70, 4, 3007, N'asi es ome', CAST(N'2019-10-09T23:46:20.810' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (71, 1, 1, N'joda primo', CAST(N'2019-10-09T23:46:27.190' AS DateTime), 1)
INSERT [dbo].[message] ([id], [idRoom], [idUser], [text], [date_create], [idState]) VALUES (72, 3, 1, N'el mejor del mundo', CAST(N'2019-10-09T23:46:48.820' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[message] OFF
SET IDENTITY_INSERT [dbo].[room] ON 

INSERT [dbo].[room] ([id], [name], [idState], [date_create], [description]) VALUES (1, N'Animales', 1, CAST(N'2019-09-20T00:00:00.000' AS DateTime), N'Una sala donde hablan solo de animales')
INSERT [dbo].[room] ([id], [name], [idState], [date_create], [description]) VALUES (3, N'Solo chicas', 1, CAST(N'2019-09-20T00:00:00.000' AS DateTime), N'Una sala para hablar, solo chicas, 0 hombres por favor')
INSERT [dbo].[room] ([id], [name], [idState], [date_create], [description]) VALUES (4, N'Solo chicos', 1, CAST(N'2019-09-20T00:00:00.000' AS DateTime), N'Una sala para hablar, solo chicos, 0 mujeres por favor')
INSERT [dbo].[room] ([id], [name], [idState], [date_create], [description]) VALUES (8, N'Tecnologia', 1, CAST(N'2019-09-20T00:00:00.000' AS DateTime), N'Una sala para hablar solo de tecnologia')
INSERT [dbo].[room] ([id], [name], [idState], [date_create], [description]) VALUES (9, N'Deportes', 1, CAST(N'2019-09-20T00:00:00.000' AS DateTime), N'Una sala para hablar de deportes')
SET IDENTITY_INSERT [dbo].[room] OFF
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([id], [email], [password], [idState], [date_create], [name], [city], [access_token]) VALUES (1, N'pepe@mail.com', N'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, CAST(N'2018-08-09T00:00:00.000' AS DateTime), N'pepito', N'gdl', N'18192e48-010a-40d5-96df-d9321f7b6c3b')
INSERT [dbo].[user] ([id], [email], [password], [idState], [date_create], [name], [city], [access_token]) VALUES (3007, N'mario@mail.com', N'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, CAST(N'2019-09-26T00:00:00.000' AS DateTime), N'mario', N'zapopan', N'c0497260-0159-4690-b9a6-43865bae94b8')
INSERT [dbo].[user] ([id], [email], [password], [idState], [date_create], [name], [city], [access_token]) VALUES (3011, N'niko@mail.com', N'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 1, CAST(N'2019-10-07T00:00:00.000' AS DateTime), N'niko', N'barranca', NULL)
INSERT [dbo].[user] ([id], [email], [password], [idState], [date_create], [name], [city], [access_token]) VALUES (3012, N'a@mail.com', N'123', 1, CAST(N'2019-10-15T21:58:57.160' AS DateTime), N'por', N'bar', NULL)
INSERT [dbo].[user] ([id], [email], [password], [idState], [date_create], [name], [city], [access_token]) VALUES (3013, N'a@mail.com', N'123', 1, CAST(N'2019-10-15T22:02:49.497' AS DateTime), N'por', N'bar', NULL)
INSERT [dbo].[user] ([id], [email], [password], [idState], [date_create], [name], [city], [access_token]) VALUES (3014, N'b@mail.com', N'456', 1, CAST(N'2019-10-15T22:06:25.527' AS DateTime), N'tor', N'bog', NULL)
INSERT [dbo].[user] ([id], [email], [password], [idState], [date_create], [name], [city], [access_token]) VALUES (3015, N'c@mail.com', N'789', 1, CAST(N'2019-10-15T22:16:33.150' AS DateTime), N'rew', N'qwe', NULL)
SET IDENTITY_INSERT [dbo].[user] OFF
ALTER TABLE [dbo].[message]  WITH CHECK ADD  CONSTRAINT [FK_message_cState] FOREIGN KEY([idState])
REFERENCES [dbo].[cState] ([id])
GO
ALTER TABLE [dbo].[message] CHECK CONSTRAINT [FK_message_cState]
GO
ALTER TABLE [dbo].[message]  WITH CHECK ADD  CONSTRAINT [FK_message_room] FOREIGN KEY([idRoom])
REFERENCES [dbo].[room] ([id])
GO
ALTER TABLE [dbo].[message] CHECK CONSTRAINT [FK_message_room]
GO
ALTER TABLE [dbo].[message]  WITH CHECK ADD  CONSTRAINT [FK_message_user] FOREIGN KEY([idUser])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[message] CHECK CONSTRAINT [FK_message_user]
GO
ALTER TABLE [dbo].[room]  WITH CHECK ADD  CONSTRAINT [FK_room_cState] FOREIGN KEY([idState])
REFERENCES [dbo].[cState] ([id])
GO
ALTER TABLE [dbo].[room] CHECK CONSTRAINT [FK_room_cState]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_cState] FOREIGN KEY([idState])
REFERENCES [dbo].[cState] ([id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_cState]
GO
USE [master]
GO
ALTER DATABASE [ChatDB] SET  READ_WRITE 
GO
