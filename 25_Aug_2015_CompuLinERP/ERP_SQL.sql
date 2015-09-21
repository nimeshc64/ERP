USE [InventoryManagement]
GO

/****** Object:  Table [dbo].[INV_MAST]    Script Date: 04/11/2015 01:01:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GRN_MAST](
	[COMPCODE] [varchar](50) NOT NULL,
	[LOCA] [varchar](5) NOT NULL,
	[GRN_NO] [varchar](10) NOT NULL,
	[GRN_TYPE] [varchar](5) NOT NULL,
	[SUPP_CODE] [varchar](50) NOT NULL,
	[SUPP_NAME] [nvarchar](100) NULL,
	[TXN_DATE] [datetime] NULL,
	[GRN_DATE] [datetime] NULL,
	[TXN_TIME] [varchar](50) NULL,
	[REF] [varchar](50) NULL,
	[DN_NO] [varchar](50) NULL,
	[TOTAL] [numeric](18, 2) NULL,
	[IS_DISCOUNT_PER] [numeric](1, 0) NULL,
	[TOTAL_AFTER_DISCOUNT] [numeric](18, 2) NULL,
	[VAT_AMOUNT] [numeric](18, 2) NULL,
	[NBT_AMOUNT] [numeric](18, 2) NULL,
	[COST_AMOUNT] [numeric](18, 2) NULL,
	[DEDUCTIONS] [numeric](18, 2) NULL,
	[PAYABLE_AMOUNT] [numeric](18, 2) NULL,
	[PAY_BALANCE] [numeric](18, 2) NULL,
	[PAY_STATUS] [numeric](1, 0) NULL,
	[PAY_IN_PROGRESS] [numeric](1, 0) NULL,
	[VAT_CODE1] [varchar](50) NULL,
	[VAT_CODE2] [varchar](50) NULL,
	[BRANCH_CODE] [varchar](50) NULL,
	[TERMINAL_ID] [varchar](50) NULL,	
	[USERCODE] [varchar](50) NULL,
	[CANCELLED] [int] NULL,
	[CANCEL_USERCODE] [varchar](50) NULL,
	[CANCEL_DATE] [datetime] NULL,
	[CANCEL_TIME] [varchar](1) NULL,
	[PRINTED] [numeric](1, 0) NULL,
	[DISCOUNT] [numeric](18, 2) NULL,
	[DISCOUNT_AMT] [numeric](18, 2) NULL,
 CONSTRAINT [PK_GRN_MAST] PRIMARY KEY CLUSTERED 
(
	[COMPCODE] ASC,
	[LOCA] ASC,	
	[GRN_NO] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[INV_DETAIL]    Script Date: 04/11/2015 01:05:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GRN_DETAIL](
	[COMPCODE] [varchar](50) NOT NULL,
	[LOCA] [varchar](5) NOT NULL,
	[GRN_NO] [varchar](10) NOT NULL,
	[ITEM_LOCA] [varchar](5) NOT NULL,
	[ITEM] [varchar](35) NOT NULL,
	[ITEM_DESC] [varchar](100) NOT NULL,
	[QTY] [numeric](18, 2) NULL,
	[BULK_UNIT] [nvarchar](100) NULL,
	[LOOSE_UNIT] [nvarchar](100) NULL,
	[BULK_SALES_PRICE] [numeric](18, 2) NULL,
	[LOOSE_SALES_PRICE] [numeric](18, 2) NULL,
	[BULK_QTY] [numeric](18, 2) NULL,
	[LOOSE_QTY] [numeric](18, 2) NULL,
	[IS_DISCOUNT_PER] [numeric](1, 0) NOT NULL,
	[DISCOUNT] [numeric](18, 2) NULL,
	[TOTAL] [numeric](18, 2) NOT NULL,
	[DISCOUNT_AMT] [numeric](18, 2) NULL,
 CONSTRAINT [PK_GRN_DETAIL] PRIMARY KEY CLUSTERED 
(
	[COMPCODE] ASC,
	[LOCA] ASC,
	[GRN_NO] ASC,
	[ITEM_LOCA] ASC,
	[ITEM] ASC	
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



Alter table dbo.LOCA_MAST add GRN_NO varchar(50) not null default 0;