USE [master]
GO
/****** Object:  Database [BenjaminDB]    Script Date: 27/11/2024 5:23:44 am ******/
CREATE DATABASE [BenjaminDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BenjaminDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BenjaminDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BenjaminDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BenjaminDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BenjaminDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BenjaminDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BenjaminDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BenjaminDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BenjaminDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BenjaminDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BenjaminDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BenjaminDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BenjaminDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BenjaminDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BenjaminDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BenjaminDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BenjaminDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BenjaminDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BenjaminDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BenjaminDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BenjaminDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BenjaminDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BenjaminDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BenjaminDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BenjaminDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BenjaminDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BenjaminDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BenjaminDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BenjaminDB] SET RECOVERY FULL 
GO
ALTER DATABASE [BenjaminDB] SET  MULTI_USER 
GO
ALTER DATABASE [BenjaminDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BenjaminDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BenjaminDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BenjaminDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BenjaminDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BenjaminDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BenjaminDB', N'ON'
GO
ALTER DATABASE [BenjaminDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [BenjaminDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BenjaminDB]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 27/11/2024 5:23:45 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Gender] [varchar](100) NULL,
	[Age] [int] NULL,
	[BirthDate] [varchar](100) NULL,
	[PhoneNumber] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[HireDate] [varchar](100) NULL,
	[Role] [varchar](100) NULL,
	[EmpID] [varchar](100) NULL,
	[EmpPW] [varchar](100) NULL,
	[AccessLevel] [varchar](100) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 27/11/2024 5:23:45 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Price] [decimal](10, 2) NULL,
	[Stock] [int] NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [BenjaminDB] SET  READ_WRITE 
GO