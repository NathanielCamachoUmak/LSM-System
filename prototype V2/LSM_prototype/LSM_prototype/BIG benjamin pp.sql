USE [BenjaminDB]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/14/2024 2:01:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(10001,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Gender] [varchar](100) NULL,
	[BirthDate] [datetime] NULL,
	[PhoneNumber] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[HireDate] [datetime] NULL,
	[EmpID] [varchar](100) NULL,
	[EmpPW] [varchar](100) NULL,
	[AccessLevel] [varchar](100) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 12/14/2024 2:01:59 AM ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 12/14/2024 2:01:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(10001,1) NOT NULL,
	[AccountID] [int] NOT NULL,
	[DeviceType] [varchar](100) NULL,
	[DeviceName] [varchar](100) NULL,
	[Status] [varchar](50) NULL,
	[Employee] [varchar](100) NULL,
	[OtherNotes] [text] NULL,
	[CustName] [varchar](100) NULL,
	[CustEmail] [varchar](100) NULL,
	[CustPhoneNum] [varchar](25) NULL,
	[StartDate] [date] NULL,
	[Discounted] [bit] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SelectableItem]    Script Date: 12/14/2024 2:01:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SelectableItem](
	[SelectableItemID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[IsSelected] [bit] NULL,
 CONSTRAINT [PK_SelectableItem] PRIMARY KEY CLUSTERED 
(
	[SelectableItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceOptions]    Script Date: 12/14/2024 2:01:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceOptions](
	[ServiceOptionsID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[Name] [varchar](100) NULL,
	[DurationValue] [int] NULL,
	[DurationText] [varchar](100) NULL,
	[Price] [decimal](10, 2) NULL,
	[IsSelected] [bit] NULL,
 CONSTRAINT [PK_ServiceOptions] PRIMARY KEY CLUSTERED 
(
	[ServiceOptionsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SelectableItem]  WITH CHECK ADD  CONSTRAINT [FK_SelectableItem_Item] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[SelectableItem] CHECK CONSTRAINT [FK_SelectableItem_Item]
GO
ALTER TABLE [dbo].[SelectableItem]  WITH CHECK ADD  CONSTRAINT [FK_SelectableItem_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[SelectableItem] CHECK CONSTRAINT [FK_SelectableItem_Orders]
GO
ALTER TABLE [dbo].[ServiceOptions]  WITH CHECK ADD  CONSTRAINT [FK_ServiceOptions_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[ServiceOptions] CHECK CONSTRAINT [FK_ServiceOptions_Orders]
GO
