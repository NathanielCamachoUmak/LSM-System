USE [BenjaminDB]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/4/2024 1:49:41 AM ******/
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
/****** Object:  Table [dbo].[Item]    Script Date: 12/4/2024 1:49:42 AM ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 12/4/2024 1:49:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [int] NOT NULL,
	[DeviceName] [varchar](100) NULL,
	[Status] [varchar](50) NULL,
	[Employee] [varchar](100) NULL,
	[Problem] [text] NULL,
	[OtherNotes] [text] NULL,
	[CustName] [varchar](100) NULL,
	[CustEmail] [varchar](100) NULL,
	[CustPhoneNum] [varchar](25) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
