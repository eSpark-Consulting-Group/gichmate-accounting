

/****** Object:  UserDefinedTableType [dbo].[UT_KOTOrderSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
CREATE TYPE [dbo].[UT_KOTOrderSettings] AS TABLE(
	[KOTSettingsId] [int] NULL,
	[FK_KOTMasterId] [int] NULL,
	[ProductId] [int] NULL,
	[Quantity] [decimal](24, 4) NULL,
	[UnitId] [int] NULL,
	[Rate] [decimal](24, 4) NULL,
	[NETAmount] [decimal](24, 4) NULL,
	[Barcode] [nvarchar](100) NULL,
	[ItemNote] [nvarchar](500) NULL,
	[CookingTime] [int] NULL,
	[ProPriority] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[UT_POSHoldSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
CREATE TYPE [dbo].[UT_POSHoldSettings] AS TABLE(
	[SalesSettingsId] [int] NULL,
	[SalesInvId] [int] NULL,
	[PartyAccountId] [int] NULL,
	[ProductId] [int] NULL,
	[ProQty] [decimal](18, 4) NULL,
	[UnitId] [int] NULL,
	[ProRate] [decimal](18, 4) NULL,
	[ProDiscont] [decimal](18, 4) NULL,
	[DiscAmnt] [decimal](18, 4) NULL,
	[GrossAmnt] [decimal](18, 4) NULL,
	[NetAmnt] [decimal](18, 4) NULL,
	[Date] [datetime] NULL,
	[HSN] [nvarchar](20) NULL,
	[GST] [decimal](18, 4) NULL,
	[GSTAMOUNT] [decimal](18, 4) NULL,
	[SGST] [decimal](18, 4) NULL,
	[SGSTAmount] [decimal](18, 4) NULL,
	[CGST] [decimal](18, 4) NULL,
	[CGSTAmount] [decimal](18, 4) NULL,
	[IGST] [decimal](18, 4) NULL,
	[IGSTAMOUNT] [decimal](18, 4) NULL,
	[FK_BatchId] [int] NULL,
	[Barcode] [nvarchar](20) NULL,
	[ItemCalory] [decimal](18, 4) NULL,
	[EmployeeID] [int] NULL,
	[EmployeeCommision] [decimal](18, 4) NULL,
	[AgentId] [int] NULL,
	[AgentCommission] [decimal](18, 4) NULL,
	[ProductName] [nvarchar](50) NULL,
	[ItemSerialNumber] [nvarchar](100) NULL,
	[FieldOne] [nvarchar](100) NULL,
	[FieldTwo] [nvarchar](100) NULL,
	[FieldThree] [nvarchar](100) NULL,
	[FieldFour] [nvarchar](100) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[UT_SalesSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
CREATE TYPE [dbo].[UT_SalesSettings] AS TABLE(
	[SalesSettingsId] [int] NULL,
	[SalesInvId] [int] NULL,
	[PartyAccountId] [int] NULL,
	[ProductId] [int] NULL,
	[ProQty] [decimal](18, 4) NULL,
	[UnitId] [int] NULL,
	[ProRate] [decimal](18, 4) NULL,
	[ProDiscont] [decimal](18, 4) NULL,
	[DiscAmnt] [decimal](18, 4) NULL,
	[GrossAmnt] [decimal](18, 4) NULL,
	[NetAmnt] [decimal](18, 4) NULL,
	[Date] [datetime] NULL,
	[HSN] [nvarchar](20) NULL,
	[GST] [decimal](18, 4) NULL,
	[GSTAMOUNT] [decimal](18, 4) NULL,
	[SGST] [decimal](18, 4) NULL,
	[SGSTAmount] [decimal](18, 4) NULL,
	[CGST] [decimal](18, 4) NULL,
	[CGSTAmount] [decimal](18, 4) NULL,
	[IGST] [decimal](18, 4) NULL,
	[IGSTAMOUNT] [decimal](18, 4) NULL,
	[FK_BatchId] [int] NULL,
	[Barcode] [nvarchar](20) NULL,
	[ItemCalory] [decimal](18, 4) NULL,
	[EmployeeID] [int] NULL,
	[EmployeeCommision] [decimal](18, 4) NULL,
	[AgentId] [int] NULL,
	[AgentCommission] [decimal](18, 4) NULL,
	[ProductName] [nvarchar](120) NULL,
	[ItemSerialNumber] [nvarchar](100) NULL,
	[FieldOne] [nvarchar](100) NULL,
	[FieldTwo] [nvarchar](100) NULL,
	[FieldThree] [nvarchar](100) NULL,
	[FieldFour] [nvarchar](100) NULL
)
GO
/****** Object:  Table [dbo].[Account]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AGroupId] [int] NULL,
	[RootId] [int] NULL,
	[PCategoryId] [int] NULL,
	[AccountName] [nvarchar](200) NULL,
	[OpeningBalance] [decimal](18, 4) NULL,
	[DebitOrCridt] [nvarchar](5) NULL,
	[AddressOne] [nvarchar](50) NULL,
	[AddressTwo] [nvarchar](50) NULL,
	[AddressThree] [nvarchar](50) NULL,
	[PhoneOne] [nvarchar](20) NULL,
	[MobileNo] [nvarchar](20) NULL,
	[FaxNo] [nvarchar](20) NULL,
	[TinNo] [nvarchar](20) NULL,
	[CstNo] [nvarchar](20) NULL,
	[EMailOne] [nvarchar](50) NULL,
	[Remarks] [nvarchar](400) NULL,
	[BankName] [nvarchar](50) NULL,
	[BankAddress] [nvarchar](50) NULL,
	[AccountNo] [nvarchar](50) NULL,
	[isActive] [bit] NULL,
	[IsBuiltIn] [bit] NULL,
	[DebitLimit] [decimal](24, 4) NULL,
	[IsLimitApplicable] [bit] NULL,
	[AddressFour] [nvarchar](50) NULL,
	[FieldOne] [nvarchar](50) NULL,
	[FieldTwo] [nvarchar](50) NULL,
	[FieldThree] [nvarchar](50) NULL,
	[FieldFour] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountGroup]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountGroup](
	[AGroupId] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NULL,
	[Under] [nvarchar](50) NULL,
	[Remarks] [nvarchar](400) NULL,
	[isBuilIn] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[AGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttenMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttenMaster](
	[AttendanceMasterId] [int] IDENTITY(1,1) NOT NULL,
	[AtteInvNo] [int] NOT NULL,
	[AtteDate] [datetime] NOT NULL,
	[Remarks] [nvarchar](400) NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AttendanceMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttenSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttenSettings](
	[AttenSettingsId] [int] IDENTITY(1,1) NOT NULL,
	[AttendanceMasterId] [int] NULL,
	[EmpID] [int] NULL,
	[Morning] [bit] NULL,
	[Evening] [bit] NULL,
	[AttenType] [decimal](18, 1) NULL,
	[EmpName] [nvarchar](50) NULL,
	[AtteDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[AttenSettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BarcodeDTL]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BarcodeDTL](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Text] [nvarchar](100) NULL,
	[Value] [nvarchar](100) NULL,
	[isIncluded] [bit] NULL,
	[Position_x] [int] NULL,
	[Position_y] [int] NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Alignment] [nvarchar](50) NULL,
	[RotationPoint] [nvarchar](50) NULL,
	[fontFamily] [nvarchar](50) NULL,
	[fontSize] [int] NULL,
	[fStyle] [nvarchar](50) NULL,
	[Format] [nvarchar](50) NULL,
	[bAlignment] [nvarchar](50) NULL,
	[FK_BarcodeHDR] [int] NULL,
	[Type] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BarcodeHDR]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BarcodeHDR](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[FirstMargin] [int] NULL,
	[TopMargin] [int] NULL,
	[RightMargin] [int] NULL,
	[BottomMargin] [int] NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Accross] [int] NULL,
	[Down] [int] NULL,
	[TotalPerPage] [int] NULL,
	[PrinterName] [nvarchar](50) NULL,
	[PaperName] [nvarchar](50) NULL,
	[PSWidth] [int] NULL,
	[PSHieght] [int] NULL,
	[isBorderIncluded] [bit] NULL,
	[isCuttingEdge] [bit] NULL,
	[BarcodeField] [int] NULL,
	[isBarcode] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BarcodeScale]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BarcodeScale](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[IPAddress] [nvarchar](100) NULL,
	[Remarks] [nvarchar](400) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Batch]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[FK_PurInvId] [int] NULL,
	[Remarks] [nvarchar](100) NULL,
	[FK_BranchId] [int] NULL,
	[RefNo] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](500) NULL,
	[Address] [nvarchar](400) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[PostalCode] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](100) NULL,
	[MobileNumber] [nvarchar](100) NULL,
	[ContactPerson] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](50) NULL,
	[Description] [nvarchar](400) NULL,
PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChequeDetails]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChequeDetails](
	[CheqDtlsId] [int] IDENTITY(1,1) NOT NULL,
	[VchrId] [int] NULL,
	[VchrType] [nvarchar](50) NULL,
	[AccountId] [int] NULL,
	[BankAcctId] [int] NULL,
	[Amount] [decimal](24, 4) NULL,
	[Bank] [nvarchar](200) NULL,
	[Branch] [nvarchar](500) NULL,
	[CheqNo] [nvarchar](25) NULL,
	[CheqDate] [nvarchar](20) NULL,
	[Status] [nvarchar](10) NULL,
	[RefNo] [nvarchar](20) NULL,
	[Remarks] [nvarchar](500) NULL,
	[TranDate] [datetime] NULL,
	[JeId] [int] NULL,
	[Type] [nvarchar](50) NULL,
	[CollectedDate] [datetime] NULL,
	[Date] [datetime] NULL,
	[ChqRtnAmount] [decimal](24, 4) NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CheqDtlsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CostCenter]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CostCenter](
	[CostCenterID] [int] IDENTITY(1,1) NOT NULL,
	[CostCenterName] [nvarchar](50) NULL,
	[Description] [nvarchar](400) NULL,
PRIMARY KEY CLUSTERED 
(
	[CostCenterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CulenderPlanning]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CulenderPlanning](
	[CPlaningId] [int] IDENTITY(1,1) NOT NULL,
	[CplanninDate] [datetime] NULL,
	[Description] [image] NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CPlaningId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CurrencyInfo]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrencyInfo](
	[CurrencyID] [int] IDENTITY(1,1) NOT NULL,
	[CurrencyCode] [nvarchar](10) NULL,
	[IsCurrencyNameFeminine] [bit] NULL,
	[EnglishCurrencyName] [nvarchar](25) NULL,
	[EnglishPluralCurrencyName] [nvarchar](25) NULL,
	[EnglishCurrencyPartName] [nvarchar](25) NULL,
	[EnglishPluralCurrencyPartName] [nvarchar](25) NULL,
	[Arabic1CurrencyName] [nvarchar](25) NULL,
	[Arabic2CurrencyName] [nvarchar](25) NULL,
	[Arabic310CurrencyName] [nvarchar](25) NULL,
	[Arabic1199CurrencyName] [nvarchar](25) NULL,
	[Arabic1CurrencyPartName] [nvarchar](25) NULL,
	[Arabic2CurrencyPartName] [nvarchar](25) NULL,
	[Arabic310CurrencyPartName] [nvarchar](25) NULL,
	[Arabic1199CurrencyPartName] [nvarchar](25) NULL,
	[PartPrecision] [int] NULL,
	[IsCurrencyPartNameFeminine] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[CurrencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomQuery]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomQuery](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CustmQryName] [nvarchar](100) NULL,
	[Query] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DebitCreditCardMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DebitCreditCardMaster](
	[CardId] [int] IDENTITY(1,1) NOT NULL,
	[CardName] [nvarchar](50) NULL,
	[BankComm] [decimal](24, 4) NULL,
	[CardComm] [decimal](24, 4) NULL,
	[Remarks] [nvarchar](400) NULL,
PRIMARY KEY CLUSTERED 
(
	[CardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeDetails]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeDetails](
	[EmpID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[EmpName] [nvarchar](400) NULL,
	[AddressOne] [nvarchar](400) NULL,
	[AddressTwo] [nvarchar](400) NULL,
	[AddressThree] [nvarchar](400) NULL,
	[PhNumber] [nvarchar](20) NULL,
	[EmailId] [nvarchar](50) NULL,
	[Designation] [nvarchar](50) NULL,
	[Salary] [decimal](24, 4) NULL,
	[CasualLeave] [decimal](24, 4) NULL,
	[Gender] [nvarchar](10) NULL,
	[Qualification] [nvarchar](400) NULL,
	[ParentName] [nvarchar](50) NULL,
	[Relation] [nvarchar](50) NULL,
	[ParentAddress] [nvarchar](400) NULL,
	[ParentPhNo] [nvarchar](20) NULL,
	[Remarks] [nvarchar](400) NULL,
	[isActive] [bit] NULL,
	[AdharNo] [nvarchar](400) NULL,
	[SalaryAccntId] [int] NULL,
	[SalaryAdvAccntId] [int] NULL,
	[SalaryPayableId] [int] NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinancialYear]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialYear](
	[FyearId] [int] IDENTITY(1,1) NOT NULL,
	[FyearName] [nvarchar](100) NULL,
	[DateFrom] [nvarchar](50) NULL,
	[DateTo] [nvarchar](50) NULL,
	[Old] [bit] NULL,
	[Closed] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[FyearId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GdwnStckTranLog]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GdwnStckTranLog](
	[GSTranId] [int] IDENTITY(1,1) NOT NULL,
	[GodwnId] [int] NULL,
	[GodownName] [nvarchar](100) NULL,
	[ProductId] [int] NULL,
	[ProductName] [nvarchar](100) NULL,
	[VchrId] [int] NULL,
	[VchrNo] [int] NULL,
	[VchrDate] [datetime] NULL,
	[VchrType] [nvarchar](100) NULL,
	[InQuantity] [decimal](24, 4) NULL,
	[OutQuantity] [decimal](24, 4) NULL,
	[UnitId] [int] NULL,
	[Narration] [nvarchar](400) NULL,
	[UserId] [int] NULL,
	[TranDate] [datetime] NULL,
	[FK_BatchId] [int] NULL,
	[FK_BranchId] [int] NULL,
	[Barcode] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[GSTranId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Godown]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Godown](
	[GodwnId] [int] IDENTITY(1,1) NOT NULL,
	[GodownName] [nvarchar](50) NULL,
	[AddressOne] [nvarchar](50) NULL,
	[AddressTwo] [nvarchar](50) NULL,
	[AddressThree] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Description] [nvarchar](400) NULL,
PRIMARY KEY CLUSTERED 
(
	[GodwnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Institute]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Institute](
	[InstituteId] [int] IDENTITY(1,1) NOT NULL,
	[InstituteName] [nvarchar](100) NULL,
	[Place] [nvarchar](50) NULL,
	[Address] [nvarchar](250) NULL,
	[Country] [nvarchar](50) NULL,
	[Pincod] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[MobNo] [nvarchar](50) NULL,
	[FaxNo] [nvarchar](50) NULL,
	[website] [nvarchar](50) NULL,
	[EmailId] [nvarchar](50) NULL,
	[Sector] [nvarchar](50) NULL,
	[Logo] [image] NULL,
	[LogoPath] [nvarchar](100) NULL,
	[Productserial] [nvarchar](50) NULL,
	[GSTNo] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[StateCode] [int] NULL,
	[BankName] [nvarchar](50) NULL,
	[BankBranch] [nvarchar](50) NULL,
	[IFSC] [nvarchar](50) NULL,
	[AccNo] [nvarchar](50) NULL,
	[LocalName] [nvarchar](100) NULL,
	[CompanyId] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[InstituteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemNote]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemNote](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Note] [nvarchar](200) NULL,
	[IsMain] [bit] NULL,
	[FieldOne] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KOTOrderMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KOTOrderMaster](
	[KOTMasterId] [int] IDENTITY(1,1) NOT NULL,
	[TockenNo] [int] NULL,
	[DateAndTime] [datetime] NULL,
	[CookingTime] [decimal](24, 4) NULL,
	[Terminal] [nvarchar](100) NULL,
	[SerialNo] [int] NULL,
	[FK_FYearId] [int] NULL,
	[FK_CustomerId] [int] NULL,
	[FK_UserId] [int] NULL,
	[FK_StaffId] [int] NULL,
	[TableId] [int] NULL,
	[Remarks] [nvarchar](400) NULL,
	[Notes] [nvarchar](600) NULL,
	[FK_KOTPrinterId] [int] NULL,
	[OrderType] [nvarchar](100) NULL,
	[TotalQuantity] [decimal](24, 4) NULL,
	[TotalAmount] [decimal](24, 4) NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[KOTMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KOTOrderSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KOTOrderSettings](
	[KOTSettingsId] [int] IDENTITY(1,1) NOT NULL,
	[FK_KOTMasterId] [int] NULL,
	[ProductId] [int] NULL,
	[Quantity] [decimal](24, 4) NULL,
	[UnitId] [int] NULL,
	[Rate] [decimal](24, 4) NULL,
	[NETAmount] [decimal](24, 4) NULL,
	[Barcode] [nvarchar](100) NULL,
	[ItemNote] [nvarchar](500) NULL,
	[ProPriority] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[KOTSettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KOTPrinter]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KOTPrinter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KOTName] [nvarchar](100) NULL,
	[PrinterName] [nvarchar](100) NULL,
	[ConnectionType] [nvarchar](100) NULL,
	[IPaddress] [nvarchar](100) NULL,
	[Mode] [nvarchar](100) NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logged]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logged](
	[UserId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManufactureDtls]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManufactureDtls](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FK_ManufactureHdrId] [int] NULL,
	[FK_ProductId] [int] NULL,
	[Quantity] [decimal](24, 4) NULL,
	[FK_UnitId] [int] NULL,
	[PurchasePrice] [decimal](24, 4) NULL,
	[SalesePrice] [decimal](24, 4) NULL,
	[RawCost] [decimal](24, 4) NULL,
	[Expenses] [decimal](24, 4) NULL,
	[Profit] [decimal](24, 4) NULL,
	[FinalAmount] [decimal](24, 4) NULL,
	[NewPurchaseRate] [decimal](24, 4) NULL,
	[NewSalesRate] [decimal](24, 4) NULL,
	[FK_BatchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManufactureHdr]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManufactureHdr](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvNo] [int] NULL,
	[Date] [datetime] NULL,
	[FK_GodownId] [int] NULL,
	[Remarks] [nvarchar](400) NULL,
	[FK_UserId] [int] NULL,
	[EntryDate] [datetime] NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManufacturingExp]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManufacturingExp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FK_ExpenseAccountId] [int] NULL,
	[FK_PaymentAccountId] [int] NULL,
	[Date] [datetime] NULL,
	[Amount] [decimal](24, 4) NULL,
	[FK_ManufactureHdrId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterValue]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterValue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
	[MasterType] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuDetails]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[MenuCaption] [nvarchar](100) NULL,
	[ParentId] [int] NULL,
	[AssemblyName] [nvarchar](100) NULL,
	[ClassName] [nvarchar](100) NULL,
	[IsForm] [bit] NULL,
	[Icon] [image] NULL,
	[FK_ModuleId] [int] NULL,
	[IsDialog] [bit] NULL,
	[Status] [bit] NULL,
	[Position] [int] NULL,
	[IsCrudMenu] [bit] NULL,
	[MenuUrl] [nvarchar](50) NULL,
	[IsFavorite] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](1000) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POSHoldMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POSHoldMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesInvNo] [int] NULL,
	[InvType] [nvarchar](50) NULL,
	[SalesDate] [datetime] NULL,
	[SalesAccountId] [int] NULL,
	[PartyAccountId] [int] NULL,
	[RouteId] [int] NULL,
	[VehId] [int] NULL,
	[StafId] [int] NULL,
	[PriceCategoryId] [int] NULL,
	[AditionalAmount] [decimal](18, 4) NULL,
	[Frieght] [decimal](18, 4) NULL,
	[TotalDiscount] [decimal](18, 4) NULL,
	[BillDiscount] [decimal](18, 4) NULL,
	[GrandDiscount] [decimal](18, 4) NULL,
	[JEId] [int] NULL,
	[EntryDate] [datetime] NULL,
	[Remarks] [nvarchar](50) NULL,
	[IGSTAmount] [decimal](18, 4) NULL,
	[SGSTAmount] [decimal](18, 4) NULL,
	[CGSTAmount] [decimal](18, 4) NULL,
	[TaxableAmount] [decimal](18, 4) NULL,
	[FYearID] [int] NULL,
	[FloodCess] [decimal](18, 4) NULL,
	[TotalTaxAmount] [decimal](18, 4) NULL,
	[FK_BranchId] [int] NULL,
	[CntrInvNo] [nvarchar](10) NULL,
	[SequenceNo] [int] NULL,
	[CashPayment] [decimal](24, 8) NULL,
	[CardPayment] [decimal](24, 8) NULL,
	[Credit] [decimal](24, 8) NULL,
	[FielOne] [nvarchar](100) NULL,
	[FieldTwo] [nvarchar](100) NULL,
	[FieldThree] [nvarchar](100) NULL,
	[FieldFour] [nvarchar](100) NULL,
	[Terminal] [nvarchar](25) NULL,
	[IsClosed] [bit] NULL,
	[FreeAmount] [decimal](24, 4) NULL,
	[ReturnAmount] [decimal](24, 4) NULL,
	[OtherPayment] [decimal](24, 4) NULL,
	[CashAccountId] [int] NULL,
	[CardAccountId] [int] NULL,
	[OnlineAccountId] [int] NULL,
	[IsSettiled] [bit] NULL,
	[TotalPayment] [decimal](24, 8) NULL,
	[VoucherNumber] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POSHoldSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POSHoldSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesInvId] [int] NULL,
	[PartyAccountId] [int] NULL,
	[ProductId] [int] NULL,
	[ProQty] [decimal](18, 4) NULL,
	[UnitId] [int] NULL,
	[ProRate] [decimal](18, 4) NULL,
	[ProDiscont] [decimal](18, 4) NULL,
	[DiscAmnt] [decimal](18, 4) NULL,
	[GrossAmnt] [decimal](18, 4) NULL,
	[NetAmnt] [decimal](18, 4) NULL,
	[Date] [datetime] NULL,
	[HSN] [nvarchar](20) NULL,
	[GST] [decimal](18, 4) NULL,
	[GSTAMOUNT] [decimal](18, 4) NULL,
	[SGST] [decimal](18, 4) NULL,
	[SGSTAmount] [decimal](18, 4) NULL,
	[CGST] [decimal](18, 4) NULL,
	[CGSTAmount] [decimal](18, 4) NULL,
	[IGST] [decimal](18, 4) NULL,
	[IGSTAMOUNT] [decimal](18, 4) NULL,
	[FK_BatchId] [int] NULL,
	[Barcode] [nvarchar](20) NULL,
	[ItemCalory] [decimal](18, 4) NULL,
	[EmployeeID] [int] NULL,
	[EmployeeCommision] [decimal](18, 4) NULL,
	[AgentId] [int] NULL,
	[AgentCommission] [decimal](18, 4) NULL,
	[ProductName] [nvarchar](50) NULL,
	[ItemSerialNumber] [nvarchar](100) NULL,
	[FieldOne] [nvarchar](100) NULL,
	[FieldTwo] [nvarchar](100) NULL,
	[FieldThree] [nvarchar](100) NULL,
	[FieldFour] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrevilegeCard]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrevilegeCard](
	[PrevilageCardId] [int] IDENTITY(1,1) NOT NULL,
	[ConsumerCode] [nvarchar](50) NULL,
	[CustomerId] [int] NULL,
	[PriceCategoryId] [int] NULL,
	[NomineeAccountId] [int] NULL,
	[ExpairyDate] [datetime] NULL,
	[RenewalDate] [datetime] NULL,
	[DOBofCustomer] [datetime] NULL,
	[Remarks] [nvarchar](400) NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PrevilageCardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceCategory]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceCategory](
	[PCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[PCategoryName] [nvarchar](50) NULL,
	[Percentage] [decimal](24, 4) NULL,
	[SDiscountPercentage] [decimal](24, 4) NULL,
PRIMARY KEY CLUSTERED 
(
	[PCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrintDesignMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintDesignMaster](
	[printMasterId] [int] IDENTITY(1,1) NOT NULL,
	[DesignName] [nvarchar](50) NULL,
	[VochrType] [nvarchar](50) NULL,
	[isDefault] [bit] NULL,
	[isLandScape] [bit] NULL,
	[PageHieght] [int] NULL,
	[PageWidth] [int] NULL,
	[LinesPerPage] [int] NULL,
	[SpaceBtwnLines] [int] NULL,
	[BackgroundImgPath] [nvarchar](300) NULL,
	[PrinterName] [nvarchar](50) NULL,
	[PrintType] [nvarchar](50) NULL,
	[upScroll] [int] NULL,
	[downScroll] [int] NULL,
	[Riverse] [nvarchar](10) NULL,
	[Bold_On] [nvarchar](10) NULL,
	[Bold_Off] [nvarchar](10) NULL,
	[Pitch] [nvarchar](10) NULL,
	[FormType] [nvarchar](50) NULL,
	[SpaceInLines] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[printMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrintDesignSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintDesignSettings](
	[PrintDesignSettingsID] [int] IDENTITY(1,1) NOT NULL,
	[printMasterId] [int] NULL,
	[ControleName] [nvarchar](50) NULL,
	[Type] [nvarchar](20) NULL,
	[FeildText] [nvarchar](500) NULL,
	[X] [int] NULL,
	[Y] [int] NULL,
	[FontSize] [int] NULL,
	[Font] [nvarchar](50) NULL,
	[isBold] [bit] NULL,
	[width] [int] NULL,
	[Alaingmnet] [nvarchar](10) NOT NULL,
	[Position] [nvarchar](25) NULL,
	[TabIndex] [int] NULL,
	[iIndex] [int] NULL,
	[Height] [int] NULL,
	[Spec] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[PrintDesignSettingsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProCombMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProCombMaster](
	[ProCombMasterId] [int] IDENTITY(1,1) NOT NULL,
	[CombProName] [nvarchar](50) NULL,
	[ProCombVchrNo] [int] NULL,
	[Date] [datetime] NULL,
	[ProQty] [decimal](24, 4) NULL,
	[UnitId] [int] NULL,
	[ProSaleRate] [decimal](24, 4) NULL,
	[ProPurRate] [decimal](24, 4) NULL,
	[MRP] [decimal](24, 4) NULL,
	[CombType] [nvarchar](50) NULL,
	[Barcode] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProCombMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProCombSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProCombSettings](
	[ProCombSettingsId] [int] IDENTITY(1,1) NOT NULL,
	[ProCombMasterId] [int] NULL,
	[ProductId] [int] NULL,
	[ProQty] [decimal](24, 4) NULL,
	[UnitId] [int] NULL,
	[SaleRate] [decimal](24, 4) NULL,
	[PurRate] [decimal](24, 4) NULL,
	[MRP] [decimal](24, 4) NULL,
	[Barcode] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProCombSettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [int] NULL,
	[ProductName] [nvarchar](120) NULL,
	[Specification] [nvarchar](50) NULL,
	[PGroupId] [int] NULL,
	[UnitId] [int] NULL,
	[BrandId] [int] NULL,
	[TCategoryId] [int] NULL,
	[HSNNo] [nvarchar](10) NULL,
	[PurchasePrice] [decimal](24, 4) NULL,
	[SellingPrice] [decimal](24, 4) NULL,
	[MRP] [decimal](24, 4) NULL,
	[OpeningStock] [decimal](24, 4) NULL,
	[OppStockDate] [datetime] NULL,
	[ReOrderStock] [decimal](24, 4) NULL,
	[AccountId] [int] NULL,
	[GodwnId] [int] NULL,
	[SelfId] [int] NULL,
	[PurchaseDis] [decimal](24, 4) NULL,
	[SaleDis] [decimal](24, 4) NULL,
	[AliasCode] [nvarchar](50) NULL,
	[AliasName] [nvarchar](120) NULL,
	[Remarks] [nvarchar](500) NULL,
	[LocalName] [nvarchar](500) NULL,
	[PurchaseUnit] [int] NULL,
	[SalesUnit] [int] NULL,
	[ReportUnit] [int] NULL,
	[image] [image] NULL,
	[filePath] [nvarchar](400) NULL,
	[description] [nvarchar](5) NULL,
	[ProductType] [nvarchar](400) NULL,
	[isActive] [bit] NULL,
	[isInclusive] [bit] NULL,
	[Barcode] [nvarchar](20) NULL,
	[IsExpairing] [bit] NULL,
	[ExpairingDays] [decimal](24, 4) NULL,
	[MinimumMargin] [decimal](24, 4) NULL,
	[NormalMargin] [decimal](24, 4) NULL,
	[RetailMargin] [decimal](24, 4) NULL,
	[WholesaleMargin] [decimal](24, 4) NULL,
	[MRPMargin] [decimal](24, 4) NULL,
	[IsMarginBasedPercentage] [bit] NULL,
	[LoyalityPont] [int] NULL,
	[AgentCommision] [decimal](24, 4) NULL,
	[SalesmanCommision] [decimal](24, 4) NULL,
	[IsCommisionBasedPercentage] [bit] NULL,
	[FieldOne] [nvarchar](100) NULL,
	[FieldTwo] [nvarchar](100) NULL,
	[FieldThree] [nvarchar](100) NULL,
	[FieldFour] [nvarchar](100) NULL,
	[FieldFive] [nvarchar](100) NULL,
	[IsScale] [bit] NULL,
	[ListOrder] [int] NULL,
	[IsAllowMultiple] [bit] NULL,
	[isRateBasedWeight] [bit] NULL,
	[PriorityStatus] [nvarchar](25) NULL,
	[CookingTime] [int] NULL,
	[IsFavorite] [bit] NULL,
	[RestaurantItemType] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductBarcode]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductBarcode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Barcode] [nvarchar](100) NULL,
	[BarcodeType] [nvarchar](100) NULL,
	[ExpairyDate] [datetime] NULL,
	[ManufDate] [datetime] NULL,
	[FK_ProductId] [int] NULL,
	[FK_UnitId] [int] NULL,
	[FK_BatchId] [int] NULL,
	[FK_BranchId] [int] NULL,
	[Remarks] [nvarchar](100) NULL,
	[PurchasePrice] [decimal](24, 4) NULL,
	[SellingPrice] [decimal](24, 4) NULL,
	[MRP] [decimal](24, 4) NULL,
	[ProductCost] [decimal](24, 4) NULL,
	[FieldOne] [nvarchar](100) NULL,
	[FieldTwo] [nvarchar](100) NULL,
	[FieldThree] [nvarchar](100) NULL,
	[FieldFour] [nvarchar](100) NULL,
	[MinimuPrice] [decimal](24, 4) NULL,
	[RetailPrice] [decimal](24, 4) NULL,
	[WholesalePrice] [decimal](24, 4) NULL,
	[ProductBarcodeName] [nvarchar](50) NULL,
	[LocalName] [nvarchar](50) NULL,
	[image] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductBarcodeScale]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductBarcodeScale](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FK_ProductId] [int] NULL,
	[FK_BarcodeScale] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductGroup]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductGroup](
	[PGroupId] [int] IDENTITY(1,1) NOT NULL,
	[PGroupName] [nvarchar](50) NULL,
	[Under] [nvarchar](50) NULL,
	[SalesInsentive] [decimal](24, 4) NULL,
	[SalesDis] [decimal](24, 4) NULL,
	[PurchDis] [decimal](24, 4) NULL,
	[Remarks] [nvarchar](1000) NULL,
	[ListOrder] [int] NULL,
	[isShowInGrid] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductPriceCategory]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPriceCategory](
	[ProPrceCatId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[PCategoryId] [int] NULL,
	[UnitId] [int] NULL,
	[PGroupId] [int] NULL,
	[SPrice] [decimal](24, 4) NULL,
	[SPDisc] [decimal](24, 4) NULL,
	[PPrice] [decimal](24, 4) NULL,
	[PDisc] [decimal](24, 4) NULL,
	[Barcode] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProPrceCatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductPrinter]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPrinter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FK_KOTPrinterId] [int] NULL,
	[FK_ProductId] [int] NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductUnit]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductUnit](
	[ProUnitId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[UnitId] [int] NULL,
	[UnitType] [nvarchar](100) NULL,
	[Barcode] [nvarchar](20) NULL,
	[UnitQty] [decimal](24, 4) NULL,
	[ProductUnitName] [nvarchar](100) NULL,
	[IsInclusive] [bit] NULL,
	[OpeningStock] [decimal](24, 4) NULL,
	[SalesPrice] [decimal](24, 4) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurInvMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurInvMaster](
	[PurInvId] [int] IDENTITY(1,1) NOT NULL,
	[PurInvNo] [int] NULL,
	[InvType] [nvarchar](50) NULL,
	[PurDate] [datetime] NULL,
	[PurActId] [int] NULL,
	[PartyAccId] [int] NULL,
	[PriceCategoryId] [int] NULL,
	[AdditionalAmnt] [decimal](24, 4) NULL,
	[Frieght] [decimal](24, 4) NULL,
	[TotalDiscount] [decimal](24, 4) NULL,
	[BillDisc] [decimal](24, 4) NULL,
	[TotalTax] [decimal](24, 4) NULL,
	[TotalAmnt] [decimal](24, 4) NULL,
	[JEId] [int] NULL,
	[EntryDate] [datetime] NULL,
	[Remarks] [nvarchar](250) NULL,
	[IGSTAmount] [decimal](24, 4) NULL,
	[SGSTAmount] [decimal](24, 4) NULL,
	[CGSTAmount] [decimal](24, 4) NULL,
	[RefInvNo] [nvarchar](50) NULL,
	[Taxable] [decimal](24, 4) NULL,
	[FK_BranchId] [int] NULL,
	[CashPayment] [decimal](24, 4) NULL,
	[CardPayment] [decimal](24, 4) NULL,
	[Credit] [decimal](24, 4) NULL,
	[FreeAmount] [decimal](24, 4) NULL,
	[ReturnAmount] [decimal](24, 4) NULL,
	[IsClosed] [bit] NULL,
	[FielOne] [nvarchar](100) NULL,
	[FieldTwo] [nvarchar](100) NULL,
	[FieldThree] [nvarchar](100) NULL,
	[FieldFour] [nvarchar](100) NULL,
	[CashAccountId] [int] NULL,
	[CardAccountId] [int] NULL,
	[OnlineAccountId] [int] NULL,
	[IsSettiled] [bit] NULL,
	[TotalPayment] [decimal](24, 8) NULL,
	[VoucherNumber] [nvarchar](25) NULL,
	[SequenceNo] [int] NULL,
	[Terminal] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[PurInvId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurInvSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurInvSettings](
	[PurSettingsId] [int] IDENTITY(1,1) NOT NULL,
	[PurInvId] [int] NULL,
	[PartyAccId] [int] NULL,
	[ProId] [int] NULL,
	[ProQty] [decimal](24, 4) NULL,
	[UnitId] [int] NULL,
	[ProRate] [decimal](24, 4) NULL,
	[Disc] [decimal](24, 4) NULL,
	[DiscAmnt] [decimal](24, 4) NULL,
	[Gross] [decimal](24, 4) NULL,
	[NetAmnt] [decimal](24, 4) NULL,
	[Date] [datetime] NULL,
	[HSN] [nvarchar](20) NULL,
	[GST] [decimal](24, 4) NULL,
	[GSTAMOUNT] [decimal](24, 4) NULL,
	[SGST] [decimal](24, 4) NULL,
	[SGSTAmount] [decimal](24, 4) NULL,
	[CGST] [decimal](24, 4) NULL,
	[CGSTAmount] [decimal](24, 4) NULL,
	[IGST] [decimal](24, 4) NULL,
	[IGSTAMOUNT] [decimal](24, 4) NULL,
	[FK_BatchId] [int] NULL,
	[Barcode] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[PurSettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurRtnInvMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurRtnInvMaster](
	[PurRtnInvId] [int] IDENTITY(1,1) NOT NULL,
	[PurRtnInvNo] [int] NULL,
	[PurRtnDate] [datetime] NULL,
	[PurRtnAcId] [int] NULL,
	[PartyAcId] [int] NULL,
	[PriceCategoryId] [int] NULL,
	[AdditionalAmnt] [decimal](24, 4) NULL,
	[Frieght] [decimal](24, 4) NULL,
	[TotalDiscount] [decimal](24, 4) NULL,
	[BillDisc] [decimal](24, 4) NULL,
	[TotalTax] [decimal](24, 4) NULL,
	[TotalAmnt] [decimal](24, 4) NULL,
	[JEId] [int] NULL,
	[EntryDate] [datetime] NULL,
	[Remarks] [nvarchar](400) NULL,
	[IGSTAmount] [decimal](24, 4) NULL,
	[SGSTAmount] [decimal](24, 4) NULL,
	[CGSTAmount] [decimal](24, 4) NULL,
	[Taxable] [decimal](24, 4) NULL,
	[FK_BranchId] [int] NULL,
	[SequenceNo] [int] NULL,
	[Terminal] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[PurRtnInvId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurRtnInvSttings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurRtnInvSttings](
	[PurRtnSettId] [int] IDENTITY(1,1) NOT NULL,
	[PurRtnInvId] [int] NULL,
	[ProId] [int] NULL,
	[ProQty] [decimal](24, 4) NULL,
	[UnitId] [int] NULL,
	[ProRate] [decimal](24, 4) NULL,
	[Disc] [decimal](24, 4) NULL,
	[DiscAmnt] [decimal](24, 4) NULL,
	[Gross] [decimal](24, 4) NULL,
	[Tax] [decimal](24, 4) NULL,
	[TaxAmnt] [decimal](24, 4) NULL,
	[NetAmnt] [decimal](24, 4) NULL,
	[PartyAccId] [int] NULL,
	[Date] [datetime] NULL,
	[HSN] [nvarchar](20) NULL,
	[GST] [decimal](24, 4) NULL,
	[GSTAMOUNT] [decimal](24, 4) NULL,
	[SGST] [decimal](24, 4) NULL,
	[SGSTAmount] [decimal](24, 4) NULL,
	[CGST] [decimal](24, 4) NULL,
	[CGSTAmount] [decimal](24, 4) NULL,
	[IGST] [decimal](24, 4) NULL,
	[IGSTAMOUNT] [decimal](24, 4) NULL,
	[FK_BatchId] [int] NULL,
	[Barcode] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[PurRtnSettId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RateFormat]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RateFormat](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [int] NULL,
	[Value] [nvarchar](1) NULL,
	[Alternative] [nvarchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RawmaterialMapping]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RawmaterialMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FK_ProductId] [int] NULL,
	[Quantity] [decimal](24, 4) NULL,
	[FK_UnitId] [int] NULL,
	[Rate] [decimal](24, 4) NULL,
	[NetAmount] [decimal](24, 4) NULL,
	[FK_ManufactureHdrId] [int] NULL,
	[FK_BatchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiptAndPayment]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiptAndPayment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsSettiled] [bit] NULL,
	[CntrInvNo] [nvarchar](15) NULL,
	[SalesDate] [datetime] NULL,
	[GrandDiscount] [decimal](24, 4) NULL,
	[TotalPayment] [decimal](24, 4) NULL,
	[Credit] [decimal](24, 4) NULL,
	[Payment] [decimal](24, 4) NULL,
	[Balance] [decimal](24, 4) NULL,
	[VchrNo] [nvarchar](15) NULL,
	[InvoiceId] [int] NULL,
	[VchrId] [int] NULL,
	[InvoiceType] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReminderTB]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReminderTB](
	[ReminderId] [int] IDENTITY(1,1) NOT NULL,
	[RemainderHead] [nvarchar](50) NULL,
	[Remainder] [nvarchar](400) NULL,
	[CurrentDate] [nvarchar](20) NULL,
	[ReminderDate] [nvarchar](20) NULL,
	[UserId] [int] NULL,
	[TargetUserId] [int] NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReminderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RestaurantTable]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestaurantTable](
	[TableId] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](100) NULL,
	[ChairCount] [int] NULL,
	[FloorName] [nvarchar](100) NULL,
	[XPosition] [int] NULL,
	[YPostion] [int] NULL,
	[Image] [image] NULL,
	[FK_EmplyeeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Root]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Root](
	[RootId] [int] IDENTITY(1,1) NOT NULL,
	[RootName] [nvarchar](50) NULL,
	[WeekDay] [nvarchar](20) NULL,
	[SalesManId] [int] NULL,
	[DriverId] [int] NULL,
	[VehicleId] [int] NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[RootId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalaryProcessMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalaryProcessMaster](
	[SPrcsMasterID] [int] IDENTITY(1,1) NOT NULL,
	[SPrcsVchrNo] [int] NULL,
	[SPrcsDate] [datetime] NULL,
	[SlryPrd] [nvarchar](50) NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[StaffType] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
	[CashOrBankAcctId] [int] NULL,
	[CashOrBankAcctName] [nvarchar](50) NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SPrcsMasterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesCounter]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesCounter](
	[SalesCounterId] [int] IDENTITY(1,1) NOT NULL,
	[SalesCounterName] [nvarchar](50) NULL,
	[CashAccountId] [int] NULL,
	[Remarks] [nvarchar](400) NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SalesCounterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesEstimateMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesEstimateMaster](
	[SalesInvId] [int] IDENTITY(1,1) NOT NULL,
	[SalesInvNo] [int] NULL,
	[InvType] [nvarchar](50) NULL,
	[SalesDate] [datetime] NULL,
	[SalesAccountId] [int] NULL,
	[PartyAccountId] [int] NULL,
	[RouteId] [int] NULL,
	[VehId] [int] NULL,
	[StafId] [int] NULL,
	[PriceCategoryId] [int] NULL,
	[AditionalAmount] [decimal](24, 4) NULL,
	[Frieght] [decimal](24, 4) NULL,
	[TotalDiscount] [decimal](24, 4) NULL,
	[BillDiscount] [decimal](24, 4) NULL,
	[GrandDiscount] [decimal](24, 4) NULL,
	[JEId] [int] NULL,
	[EntryDate] [datetime] NULL,
	[Remarks] [nvarchar](50) NULL,
	[IGSTAmount] [decimal](24, 4) NULL,
	[SGSTAmount] [decimal](24, 4) NULL,
	[CGSTAmount] [decimal](24, 4) NULL,
	[TaxableAmount] [decimal](24, 4) NULL,
	[FYearID] [int] NULL,
	[FloodCess] [decimal](24, 4) NULL,
	[TotalTaxAmount] [decimal](24, 4) NULL,
	[FK_BranchId] [int] NULL,
	[CntrInvNo] [nvarchar](10) NULL,
	[SequenceNo] [int] NULL,
	[CashPayment] [decimal](24, 8) NULL,
	[CardPayment] [decimal](24, 8) NULL,
	[Credit] [decimal](24, 8) NULL,
	[FielOne] [nvarchar](100) NULL,
	[FieldTwo] [nvarchar](100) NULL,
	[FieldThree] [nvarchar](100) NULL,
	[FieldFour] [nvarchar](100) NULL,
	[Terminal] [nvarchar](25) NULL,
	[IsClosed] [bit] NULL,
	[FreeAmount] [decimal](24, 4) NULL,
	[ReturnAmount] [decimal](24, 4) NULL,
	[OtherPayment] [decimal](24, 4) NULL,
	[CashAccountId] [int] NULL,
	[CardAccountId] [int] NULL,
	[OnlineAccountId] [int] NULL,
	[IsSettiled] [bit] NULL,
	[TotalPayment] [decimal](24, 8) NULL,
	[VoucherNumber] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[SalesInvId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesEstimateSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesEstimateSettings](
	[SalesSettingsId] [int] IDENTITY(1,1) NOT NULL,
	[SalesInvId] [int] NULL,
	[PartyAccountId] [int] NULL,
	[ProductId] [int] NULL,
	[ProQty] [decimal](18, 4) NULL,
	[UnitId] [int] NULL,
	[ProRate] [decimal](18, 4) NULL,
	[ProDiscont] [decimal](18, 4) NULL,
	[DiscAmnt] [decimal](18, 4) NULL,
	[GrossAmnt] [decimal](18, 4) NULL,
	[NetAmnt] [decimal](18, 4) NULL,
	[Date] [datetime] NULL,
	[HSN] [nvarchar](20) NULL,
	[GST] [decimal](18, 4) NULL,
	[GSTAMOUNT] [decimal](18, 4) NULL,
	[SGST] [decimal](18, 4) NULL,
	[SGSTAmount] [decimal](18, 4) NULL,
	[CGST] [decimal](18, 4) NULL,
	[CGSTAmount] [decimal](18, 4) NULL,
	[IGST] [decimal](18, 4) NULL,
	[IGSTAMOUNT] [decimal](18, 4) NULL,
	[FK_BatchId] [int] NULL,
	[Barcode] [nvarchar](20) NULL,
	[ItemCalory] [decimal](18, 4) NULL,
	[EmployeeID] [int] NULL,
	[EmployeeCommision] [decimal](18, 4) NULL,
	[AgentId] [int] NULL,
	[AgentCommission] [decimal](18, 4) NULL,
	[ProductName] [nvarchar](120) NULL,
	[ItemSerialNumber] [nvarchar](100) NULL,
	[FieldOne] [nvarchar](100) NULL,
	[FieldTwo] [nvarchar](100) NULL,
	[FieldThree] [nvarchar](100) NULL,
	[FieldFour] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[SalesSettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrderMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderMaster](
	[SalesOrderMasterId] [int] IDENTITY(1,1) NOT NULL,
	[VchrNo] [int] NULL,
	[CustomerId] [int] NULL,
	[SalesAccountId] [int] NULL,
	[SalesManId] [int] NULL,
	[RouteId] [int] NULL,
	[PriceCategoryId] [int] NULL,
	[Remarks] [nvarchar](400) NULL,
	[NetAmount] [decimal](24, 4) NULL,
	[AdditionalAmnt] [decimal](24, 4) NULL,
	[Freight] [decimal](24, 4) NULL,
	[BillDiscount] [decimal](24, 4) NULL,
	[AddDiscount] [decimal](24, 4) NULL,
	[GrandTotal] [decimal](24, 4) NULL,
	[InvType] [nvarchar](50) NULL,
	[JEid] [int] NULL,
	[EntryDate] [datetime] NULL,
	[InvDate] [datetime] NULL,
	[Status] [bit] NULL,
	[Taxable] [decimal](24, 4) NULL,
	[TotalTax] [decimal](24, 4) NULL,
	[FK_BranchId] [int] NULL,
	[TargetDate] [datetime] NULL,
	[AdvanceAmount] [decimal](24, 4) NULL,
	[FieldOne] [nvarchar](100) NULL,
	[FieldTwo] [nvarchar](100) NULL,
	[FieldThree] [nvarchar](100) NULL,
	[FieldFour] [nvarchar](100) NULL,
	[FieldFive] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[SalesOrderMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrderSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderSettings](
	[SalesOrderSettingsId] [int] IDENTITY(1,1) NOT NULL,
	[SalesOrderMasterId] [int] NULL,
	[PartyAccountId] [int] NULL,
	[ProductId] [int] NULL,
	[ProQty] [decimal](24, 4) NULL,
	[UnitId] [int] NULL,
	[ProRate] [decimal](24, 4) NULL,
	[ProDiscont] [decimal](24, 4) NULL,
	[DiscAmnt] [decimal](24, 4) NULL,
	[GrossAmnt] [decimal](24, 4) NULL,
	[NetAmnt] [decimal](24, 4) NULL,
	[Taxable] [decimal](24, 4) NULL,
	[Tax] [decimal](24, 4) NULL,
	[TaxAmount] [decimal](24, 4) NULL,
	[FK_BatchId] [int] NULL,
	[Barcode] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[SalesOrderSettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesRtnMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesRtnMaster](
	[SlsRtrnId] [int] IDENTITY(1,1) NOT NULL,
	[SlRtnInvNo] [int] NULL,
	[SlsRtnDate] [datetime] NULL,
	[SlsRtnAccountId] [int] NULL,
	[PartyAccountId] [int] NULL,
	[RouteId] [int] NULL,
	[VehId] [int] NULL,
	[StaffId] [int] NULL,
	[PriceCategoryId] [int] NULL,
	[AditionalAmnt] [decimal](24, 4) NULL,
	[Frieght] [decimal](24, 4) NULL,
	[TotalDiscount] [decimal](24, 4) NULL,
	[BillDiscount] [decimal](24, 4) NULL,
	[TotalAmount] [decimal](24, 4) NULL,
	[JeId] [int] NULL,
	[EntryDate] [datetime] NULL,
	[Remarks] [nvarchar](400) NULL,
	[IGSTAmount] [decimal](24, 4) NULL,
	[SGSTAmount] [decimal](24, 4) NULL,
	[CGSTAmount] [decimal](24, 4) NULL,
	[FYearID] [int] NULL,
	[TotalTax] [decimal](24, 4) NULL,
	[Taxable] [decimal](24, 4) NULL,
	[FK_BranchId] [int] NULL,
	[SequenceNo] [int] NULL,
	[Terminal] [nvarchar](25) NULL,
	[RefInvNo] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[SlsRtrnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SelectedFYear]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SelectedFYear](
	[SlectedFY] [int] IDENTITY(1,1) NOT NULL,
	[FyearId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SlectedFY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Self]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Self](
	[SelfId] [int] IDENTITY(1,1) NOT NULL,
	[GodownId] [int] NULL,
	[SelfName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SelfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[Caption] [nvarchar](100) NULL,
	[Remarks] [nvarchar](100) NULL,
	[BitValue] [bit] NULL,
	[StringValue] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SlryPrcsSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SlryPrcsSettings](
	[SPrcsStngsID] [int] IDENTITY(1,1) NOT NULL,
	[SprcsMasterID] [int] NULL,
	[EmpID] [int] NULL,
	[EmpName] [nvarchar](50) NULL,
	[LeavsTaken] [decimal](24, 4) NULL,
	[CasualLeave] [decimal](24, 4) NULL,
	[BasicSalary] [decimal](24, 4) NULL,
	[LeaveDiduction] [decimal](24, 4) NULL,
	[Incentive] [decimal](24, 4) NULL,
	[OtherAmnt] [decimal](24, 4) NULL,
	[SalaryCut] [decimal](24, 4) NULL,
	[OtherDiduction] [decimal](24, 4) NULL,
	[NetSalary] [decimal](24, 4) NULL,
	[SlryAdvance] [decimal](24, 4) NULL,
	[PaidAmnt] [decimal](24, 4) NULL,
	[Balance] [decimal](24, 4) NULL,
	[Remarks] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[SPrcsStngsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SlsRtnSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SlsRtnSettings](
	[SlsrtnSettingsID] [int] IDENTITY(1,1) NOT NULL,
	[SlsRtnId] [int] NULL,
	[ProductId] [int] NULL,
	[ProQty] [decimal](24, 4) NULL,
	[UnitId] [int] NULL,
	[ProRate] [decimal](24, 4) NULL,
	[ProDis] [decimal](24, 4) NULL,
	[DiscAmnt] [decimal](24, 4) NULL,
	[GrossAmnt] [decimal](24, 4) NULL,
	[isDamaged] [bit] NULL,
	[NetAmnt] [decimal](24, 4) NULL,
	[PartyAccountId] [int] NULL,
	[Date] [datetime] NULL,
	[HSN] [int] NULL,
	[GST] [decimal](24, 4) NULL,
	[GSTAMOUNT] [decimal](24, 4) NULL,
	[SGST] [decimal](24, 4) NULL,
	[SGSTAmount] [decimal](24, 4) NULL,
	[CGST] [decimal](24, 4) NULL,
	[CGSTAmount] [decimal](24, 4) NULL,
	[IGST] [decimal](24, 4) NULL,
	[IGSTAMOUNT] [decimal](24, 4) NULL,
	[FK_BatchId] [int] NULL,
	[Barcode] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[SlsrtnSettingsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockAdjusterMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockAdjusterMaster](
	[stockAdjusterMasterId] [int] IDENTITY(1,1) NOT NULL,
	[StockAdjusterMasterCode] [int] NULL,
	[Date] [datetime] NULL,
	[Remarks] [nvarchar](400) NULL,
	[ProtGroup] [int] NULL,
	[ProBrand] [int] NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[stockAdjusterMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockAdjustSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockAdjustSettings](
	[stockAdjustSettingsId] [int] IDENTITY(1,1) NOT NULL,
	[stockAdjusterMasterId] [int] NULL,
	[productId] [int] NULL,
	[currSystemStock] [decimal](24, 4) NULL,
	[curActualStock] [decimal](24, 4) NULL,
	[unitId] [int] NULL,
	[FK_BatchId] [int] NULL,
	[Barcode] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[stockAdjustSettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockTranLog]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockTranLog](
	[STranId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[VchrId] [int] NULL,
	[VchrNo] [int] NULL,
	[VchrDate] [datetime] NULL,
	[VchrType] [nvarchar](50) NULL,
	[VchrFormType] [nvarchar](50) NULL,
	[AccountId] [int] NULL,
	[InQuantity] [decimal](24, 4) NULL,
	[OutQuantity] [decimal](24, 4) NULL,
	[UnitId] [int] NULL,
	[Narration] [nvarchar](400) NULL,
	[UserId] [int] NULL,
	[TranDate] [datetime] NULL,
	[FK_BatchId] [int] NULL,
	[FK_BranchId] [int] NULL,
	[Barcode] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[STranId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockTranSettings]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockTranSettings](
	[StockTranSettingsId] [int] IDENTITY(1,1) NOT NULL,
	[StockTransferId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductName] [nvarchar](50) NULL,
	[ProQuantity] [decimal](24, 4) NULL,
	[UnitId] [int] NULL,
	[CompanyName] [nvarchar](50) NULL,
	[CompanyPlace] [nvarchar](50) NULL,
	[ProRate] [decimal](24, 4) NULL,
	[Total] [decimal](24, 4) NULL,
	[FK_BatchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StockTranSettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockTransferMaster]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockTransferMaster](
	[StockTransferId] [int] IDENTITY(1,1) NOT NULL,
	[StockTransferCode] [int] NOT NULL,
	[TransferDate] [datetime] NULL,
	[FromGodownId] [int] NULL,
	[FromGodownName] [nvarchar](400) NULL,
	[ToGodownId] [int] NULL,
	[ToGodownName] [nvarchar](400) NULL,
	[Remarks] [nvarchar](400) NULL,
	[UserId] [int] NULL,
	[EntryDate] [datetime] NULL,
	[CompanyName] [nvarchar](50) NULL,
	[CompanyPlace] [nvarchar](50) NULL,
	[TotalAmnt] [decimal](24, 4) NULL,
	[TotalQty] [decimal](24, 4) NULL,
	[TranType] [nvarchar](50) NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StockTransferId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxCategory]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxCategory](
	[TCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[TCategoryName] [nvarchar](20) NULL,
	[SalesVat] [decimal](24, 4) NULL,
	[PurVat] [decimal](24, 4) NULL,
	[SaleCst] [decimal](24, 4) NULL,
	[PurCst] [decimal](24, 4) NULL,
	[SaleCess] [decimal](24, 4) NULL,
	[PurCess] [decimal](24, 4) NULL,
	[SalesXDuty] [decimal](24, 4) NULL,
	[PurXDuty] [decimal](24, 4) NULL,
PRIMARY KEY CLUSTERED 
(
	[TCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionLog]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionLog](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[VchrId] [int] NULL,
	[VchrNo] [int] NULL,
	[VchrType] [nvarchar](50) NULL,
	[VchrDate] [datetime] NULL,
	[AccountId] [int] NULL,
	[OppAccountId] [int] NULL,
	[DebitAmount] [decimal](24, 4) NULL,
	[CreditAmount] [decimal](24, 4) NULL,
	[Narration] [nvarchar](400) NULL,
	[UserID] [int] NULL,
	[TranDate] [datetime] NULL,
	[Status] [nvarchar](10) NULL,
	[RefNo] [nvarchar](20) NULL,
	[VchrName] [nvarchar](50) NULL,
	[FK_BranchId] [int] NULL,
	[SequenceNo] [int] NULL,
	[Terminal] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Translator]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Translator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Value] [nvarchar](100) NULL,
	[Form] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[UnitId] [int] IDENTITY(1,1) NOT NULL,
	[UnitName] [nvarchar](10) NULL,
	[UnitNo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[PrevilageId] [int] NULL,
	[UserName] [nvarchar](50) NULL,
	[PassWord] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPrevilegeDtl]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPrevilegeDtl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FK_UserPrevilegeHdrId] [int] NULL,
	[FK_MenuDetailsId] [int] NULL,
	[Visible] [bit] NULL,
	[Enable] [bit] NULL,
	[SaveData] [bit] NULL,
	[EditData] [bit] NULL,
	[DeleteData] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPrevilegeHdr]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPrevilegeHdr](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VchrTranLog]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VchrTranLog](
	[VchrTranId] [int] IDENTITY(1,1) NOT NULL,
	[VchrNo] [int] NULL,
	[VchrDate] [datetime] NULL,
	[VchrAmount] [decimal](24, 4) NULL,
	[VchrType] [nvarchar](50) NULL,
	[FromAccountId] [int] NULL,
	[ToAccountId] [int] NULL,
	[Narration] [nvarchar](400) NULL,
	[SalesManId] [int] NULL,
	[UserId] [int] NULL,
	[TranDate] [datetime] NULL,
	[VchrName] [nvarchar](50) NULL,
	[FK_BranchId] [int] NULL,
	[SequenceNo] [int] NULL,
	[Terminal] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[VchrTranId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 3/29/2022 7:15:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle](
	[VanId] [int] IDENTITY(1,1) NOT NULL,
	[VehName] [nvarchar](50) NULL,
	[Manufacturer] [nvarchar](20) NULL,
	[Model] [nvarchar](20) NULL,
	[YearOfModel] [nvarchar](20) NULL,
	[RegNo] [nvarchar](20) NULL,
	[Ownership] [nvarchar](50) NULL,
	[RegisterdOwner] [nvarchar](50) NULL,
	[EngineNo] [nvarchar](50) NULL,
	[ChasisNo] [nvarchar](50) NULL,
	[InsurenceDate] [datetime] NULL,
	[TaxDate] [datetime] NULL,
	[PolusionDate] [datetime] NULL,
	[BColor] [nvarchar](20) NULL,
	[LoanEMIDate] [datetime] NULL,
	[LoanEMIAmount] [nvarchar](200) NULL,
	[Remarks] [nvarchar](400) NULL,
	[FK_BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[VanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (1, 18, NULL, NULL, N'CASH ACCOUNT', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (2, 19, NULL, NULL, N'BANK ACCOUNT', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (3, 24, NULL, NULL, N'SALES ACCOUNT', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (4, 26, NULL, NULL, N'SALES RETURN ACCOUNT', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (5, 23, NULL, NULL, N'PURCHASE ACCOUNT', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (6, 25, NULL, NULL, N'PURCHASE RETURN ACCOUNT', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (7, 22, NULL, NULL, N'SAMPLE SUPPLIERS', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (8, 21, NULL, NULL, N'SAMPLE CUSTOMER', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (9, 32, NULL, NULL, N'OTHER DISCOUNT GIVEN', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (10, 31, NULL, NULL, N'OTHER DISCOUNT RECEIVED', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (11, 28, NULL, NULL, N'FREIGHT ON PURCHASE', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (12, 13, NULL, NULL, N'CHEQUE RETURN EXPENSES', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (13, 12, NULL, NULL, N'CHEQUE RETURN INCOME', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (14, 42, NULL, NULL, N'TAX PAYABLE', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (15, 43, NULL, NULL, N'TAX RECEIVABLE', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (16, 32, NULL, NULL, N'BILL DISCOUNT GIVEN', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (17, 31, NULL, NULL, N'BILL DISCOUNT RECEIVED', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (20, 13, 1, 1, N'office rent', CAST(1500.0000 AS Decimal(18, 4)), N'DR', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', 1, 0, CAST(0.0000 AS Decimal(24, 4)), 0, N'', NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (21, 13, NULL, NULL, N'REDA Salary Account', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(24, 4)), 0, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (22, 13, NULL, NULL, N'REDA Salary Advance Account', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(24, 4)), 0, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([AccountId], [AGroupId], [RootId], [PCategoryId], [AccountName], [OpeningBalance], [DebitOrCridt], [AddressOne], [AddressTwo], [AddressThree], [PhoneOne], [MobileNo], [FaxNo], [TinNo], [CstNo], [EMailOne], [Remarks], [BankName], [BankAddress], [AccountNo], [isActive], [IsBuiltIn], [DebitLimit], [IsLimitApplicable], [AddressFour], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (23, 37, NULL, NULL, N'REDA Salary Payable Account', CAST(1500.0000 AS Decimal(18, 4)), N'DR', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(24, 4)), 0, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[AccountGroup] ON 

INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (1, N'ACCOUNT', NULL, NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (2, N'BALANCE SHEET', N'ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (3, N'TRADING ACCOUNT', N'ACCOUNT', N'', 0)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (4, N'PROFIT & LOSE ACCOUNT', N'ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (5, N'ASSET', N'BALANCE SHEET', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (6, N'LIABILITY', N'BALANCE SHEET', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (7, N'SALES ACCOUNT', N'TRADING ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (8, N'PURCHASE ACCOUNT', N'TRADING ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (9, N'SERVICES ACCOUNT', N'TRADING ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (10, N'DIRECT INCOME', N'TRADING ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (11, N'DIRECT EXPENSES', N'TRADING ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (12, N'INDIRECT INCOME', N'PROFIT & LOSE ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (13, N'INDIRECT EXPENSES', N'PROFIT & LOSE ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (14, N'CURRENT ASSET', N'ASSET', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (15, N'FIXED ASSET', N'ASSET', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (16, N'CURRENT LIABILITY', N'LIABILITY', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (17, N'LONG TERM LIABILITY', N'LIABILITY', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (18, N'CASH IN HAND', N'CURRENT ASSET', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (19, N'CASH AT BANK', N'CURRENT ASSET', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (20, N'CAPITAL ACCOUNT', N'LONG TERM LIABILITY', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (21, N'CUSTOMER/SUNDRY DEBTORS', N'CURRENT ASSET', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (22, N'SUPPLIERS/SUNDRY CREDITORS', N'CURRENT LIABILITY', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (23, N'PURCHASE ACCOUNT', N'PURCHASE ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (24, N'SALES ACCOUNT', N'SALES ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (25, N'PURCHASE RETURN ACCOUNT', N'PURCHASE ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (26, N'SALES RETURN ACCOUNT', N'SALES ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (27, N'DIRECT INCOME', N'DIRECT INCOME', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (28, N'DIRECT EXPENSES', N'DIRECT EXPENSES', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (29, N'RENTAL ACCOUNT', N'TRADING ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (30, N'RENTAL ACCOUNT', N'RENTAL ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (31, N'DISCOUNT RECEIVED', N'DIRECT INCOME', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (32, N'DISCOUNT GIVEN', N'DIRECT EXPENSES', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (33, N'FURNITURE ACCOUNT', N'FIXED ASSET', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (34, N'VEHICLES', N'FIXED ASSET', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (35, N'INVESTMENT', N'FIXED ASSET', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (36, N'DUTIES AND TAXES PAYABLE', N'CURRENT LIABILITY', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (37, N'SALARY PAYABLE', N'CURRENT LIABILITY', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (38, N'PROVISIONS', N'CURRENT LIABILITY', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (39, N'SUSPENSE ACCOUNT', N'CURRENT LIABILITY', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (40, N'STAFF DEPOSIT', N'CURRENT LIABILITY', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (41, N'DRAWINGS ACCOUNT', N'CAPITAL ACCOUNT', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (42, N'TAX PAYABLE', N'CURRENT LIABILITY', NULL, 1)
INSERT [dbo].[AccountGroup] ([AGroupId], [GroupName], [Under], [Remarks], [isBuilIn]) VALUES (43, N'TAX RECEIVABLE', N'CURRENT ASSET', NULL, 1)
SET IDENTITY_INSERT [dbo].[AccountGroup] OFF
GO
SET IDENTITY_INSERT [dbo].[BarcodeDTL] ON 

INSERT [dbo].[BarcodeDTL] ([id], [Name], [Text], [Value], [isIncluded], [Position_x], [Position_y], [Width], [Height], [Alignment], [RotationPoint], [fontFamily], [fontSize], [fStyle], [Format], [bAlignment], [FK_BarcodeHDR], [Type]) VALUES (1, N'29', N'Barcode_Image', N'', NULL, 2, 23, 140, 40, N'Left', N'360', N'Open Sans', 9, N'Regular', N'Code 128', N'Rotate180FlipXY', 1, N'Barcode')
INSERT [dbo].[BarcodeDTL] ([id], [Name], [Text], [Value], [isIncluded], [Position_x], [Position_y], [Width], [Height], [Alignment], [RotationPoint], [fontFamily], [fontSize], [fStyle], [Format], [bAlignment], [FK_BarcodeHDR], [Type]) VALUES (2, N'2', N'Product Name', N'', NULL, 2, 68, 150, 15, N'Center', N'360', N'[FontFamily: Name=Calibri]', 6, N'Regular', N'Code 128', N'Rotate180FlipXY', 1, N'string')
INSERT [dbo].[BarcodeDTL] ([id], [Name], [Text], [Value], [isIncluded], [Position_x], [Position_y], [Width], [Height], [Alignment], [RotationPoint], [fontFamily], [fontSize], [fStyle], [Format], [bAlignment], [FK_BarcodeHDR], [Type]) VALUES (3, N'8', N'Sales Rate', N'Vat-Inc. SAR:', NULL, 3, 88, 150, 15, N'Center', N'360', N'[FontFamily: Name=Calibri]', 9, N'Bold', N'Code 128', N'Rotate180FlipXY', 1, N'string')
INSERT [dbo].[BarcodeDTL] ([id], [Name], [Text], [Value], [isIncluded], [Position_x], [Position_y], [Width], [Height], [Alignment], [RotationPoint], [fontFamily], [fontSize], [fStyle], [Format], [bAlignment], [FK_BarcodeHDR], [Type]) VALUES (4, N'11', N'Company Name', N'', NULL, 2, 2, 150, 18, N'Center', N'360', N'[FontFamily: Name=Calibri]', 6, N'Regular', N'Code 128', N'Rotate180FlipXY', 1, N'string')
SET IDENTITY_INSERT [dbo].[BarcodeDTL] OFF
GO
SET IDENTITY_INSERT [dbo].[BarcodeHDR] ON 

INSERT [dbo].[BarcodeHDR] ([Id], [name], [FirstMargin], [TopMargin], [RightMargin], [BottomMargin], [Width], [Height], [Accross], [Down], [TotalPerPage], [PrinterName], [PaperName], [PSWidth], [PSHieght], [isBorderIncluded], [isCuttingEdge], [BarcodeField], [isBarcode]) VALUES (1, N'barcode', 0, 0, 0, 0, 150, 110, 1, 1, 1, N'Wondershare PDFelement', N'A4', 150, 110, 0, 0, 0, 1)
SET IDENTITY_INSERT [dbo].[BarcodeHDR] OFF
GO
SET IDENTITY_INSERT [dbo].[Batch] ON 

INSERT [dbo].[Batch] ([Id], [Name], [FK_PurInvId], [Remarks], [FK_BranchId], [RefNo]) VALUES (1, N'FirstBatch', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Batch] OFF
GO
SET IDENTITY_INSERT [dbo].[Branch] ON 

INSERT [dbo].[Branch] ([Id], [Name], [Description], [Address], [City], [State], [Country], [PostalCode], [PhoneNumber], [MobileNumber], [ContactPerson], [Status], [Active]) VALUES (1, N'Primary', N'Default', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Default', 1)
INSERT [dbo].[Branch] ([Id], [Name], [Description], [Address], [City], [State], [Country], [PostalCode], [PhoneNumber], [MobileNumber], [ContactPerson], [Status], [Active]) VALUES (5, N'EGYPT ', N'', N'capital', N'UNKNOWN', N'UNKNOWN', N'EGYPT ', N'3100', N'012336555', N'UNKNOWN', N'', NULL, 1)
INSERT [dbo].[Branch] ([Id], [Name], [Description], [Address], [City], [State], [Country], [PostalCode], [PhoneNumber], [MobileNumber], [ContactPerson], [Status], [Active]) VALUES (6, N'NEW BRANC', N'', N'', N'', N'', N'', N'', N'', N'', N'', NULL, 1)
SET IDENTITY_INSERT [dbo].[Branch] OFF
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([BrandId], [BrandName], [Description]) VALUES (1, N'GENERAL', NULL)
INSERT [dbo].[Brand] ([BrandId], [BrandName], [Description]) VALUES (2, N'GROCERRY', N'')
INSERT [dbo].[Brand] ([BrandId], [BrandName], [Description]) VALUES (3, N'BASHMOTI', N'')
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[CostCenter] ON 

INSERT [dbo].[CostCenter] ([CostCenterID], [CostCenterName], [Description]) VALUES (1, N'AA', N'')
SET IDENTITY_INSERT [dbo].[CostCenter] OFF
GO
SET IDENTITY_INSERT [dbo].[CurrencyInfo] ON 

INSERT [dbo].[CurrencyInfo] ([CurrencyID], [CurrencyCode], [IsCurrencyNameFeminine], [EnglishCurrencyName], [EnglishPluralCurrencyName], [EnglishCurrencyPartName], [EnglishPluralCurrencyPartName], [Arabic1CurrencyName], [Arabic2CurrencyName], [Arabic310CurrencyName], [Arabic1199CurrencyName], [Arabic1CurrencyPartName], [Arabic2CurrencyPartName], [Arabic310CurrencyPartName], [Arabic1199CurrencyPartName], [PartPrecision], [IsCurrencyPartNameFeminine]) VALUES (1, N'SAR', 0, N'Riyal', N'Riyals', N'Halala', N'Halalas', N'ريال سعودي', N'ريالان سعوديان', N'ريالات سعودية', N'ريالاً سعودياً', N'هللة', N'هللتان', N'هللات', N'هللة', 3, 1)
INSERT [dbo].[CurrencyInfo] ([CurrencyID], [CurrencyCode], [IsCurrencyNameFeminine], [EnglishCurrencyName], [EnglishPluralCurrencyName], [EnglishCurrencyPartName], [EnglishPluralCurrencyPartName], [Arabic1CurrencyName], [Arabic2CurrencyName], [Arabic310CurrencyName], [Arabic1199CurrencyName], [Arabic1CurrencyPartName], [Arabic2CurrencyPartName], [Arabic310CurrencyPartName], [Arabic1199CurrencyPartName], [PartPrecision], [IsCurrencyPartNameFeminine]) VALUES (2, N'TK', 0, N'BDT', N'Taka', N'Paisa', N'Paisa', N'', N'', N'', N'', N'', N'', N'', N'', 2, 0)
INSERT [dbo].[CurrencyInfo] ([CurrencyID], [CurrencyCode], [IsCurrencyNameFeminine], [EnglishCurrencyName], [EnglishPluralCurrencyName], [EnglishCurrencyPartName], [EnglishPluralCurrencyPartName], [Arabic1CurrencyName], [Arabic2CurrencyName], [Arabic310CurrencyName], [Arabic1199CurrencyName], [Arabic1CurrencyPartName], [Arabic2CurrencyPartName], [Arabic310CurrencyPartName], [Arabic1199CurrencyPartName], [PartPrecision], [IsCurrencyPartNameFeminine]) VALUES (3, N'Rs', 0, N'Rupee', N'Rupees', N'Paisa', N'Paisa', N'', N'', N'', N'', N'', N'', N'', N'', 2, 0)
SET IDENTITY_INSERT [dbo].[CurrencyInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomQuery] ON 

INSERT [dbo].[CustomQuery] ([id], [CustmQryName], [Query]) VALUES (1, N'', N'')
INSERT [dbo].[CustomQuery] ([id], [CustmQryName], [Query]) VALUES (2, N'', N'')
INSERT [dbo].[CustomQuery] ([id], [CustmQryName], [Query]) VALUES (3, N'', N'')
SET IDENTITY_INSERT [dbo].[CustomQuery] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeDetails] ON 

INSERT [dbo].[EmployeeDetails] ([EmpID], [EmployeeID], [EmpName], [AddressOne], [AddressTwo], [AddressThree], [PhNumber], [EmailId], [Designation], [Salary], [CasualLeave], [Gender], [Qualification], [ParentName], [Relation], [ParentAddress], [ParentPhNo], [Remarks], [isActive], [AdharNo], [SalaryAccntId], [SalaryAdvAccntId], [SalaryPayableId], [FK_BranchId]) VALUES (1, 1, N'SM', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([EmpID], [EmployeeID], [EmpName], [AddressOne], [AddressTwo], [AddressThree], [PhNumber], [EmailId], [Designation], [Salary], [CasualLeave], [Gender], [Qualification], [ParentName], [Relation], [ParentAddress], [ParentPhNo], [Remarks], [isActive], [AdharNo], [SalaryAccntId], [SalaryAdvAccntId], [SalaryPayableId], [FK_BranchId]) VALUES (2, 1, N'REDA', N'cite mahyidin', N'', N'', N'0551350010', N'', N'', CAST(100000.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'Male', N'directur', N'boumaref mahmoud', N'023551503', N'', N'', N'', 1, N'123456789', 21, 22, 23, 1)
SET IDENTITY_INSERT [dbo].[EmployeeDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[FinancialYear] ON 

INSERT [dbo].[FinancialYear] ([FyearId], [FyearName], [DateFrom], [DateTo], [Old], [Closed]) VALUES (1, N'2021', N'2021-01-01T08:53:22', N'2021-12-31T08:53:22', 0, 0)
SET IDENTITY_INSERT [dbo].[FinancialYear] OFF
GO
SET IDENTITY_INSERT [dbo].[GdwnStckTranLog] ON 

INSERT [dbo].[GdwnStckTranLog] ([GSTranId], [GodwnId], [GodownName], [ProductId], [ProductName], [VchrId], [VchrNo], [VchrDate], [VchrType], [InQuantity], [OutQuantity], [UnitId], [Narration], [UserId], [TranDate], [FK_BatchId], [FK_BranchId], [Barcode]) VALUES (1104, 1, N'PRIMARY', 21, N'Mobile', 8, 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'B2C', CAST(20.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), 1, N'Purchase Voucher', 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), NULL, 1, N'123')
INSERT [dbo].[GdwnStckTranLog] ([GSTranId], [GodwnId], [GodownName], [ProductId], [ProductName], [VchrId], [VchrNo], [VchrDate], [VchrType], [InQuantity], [OutQuantity], [UnitId], [Narration], [UserId], [TranDate], [FK_BatchId], [FK_BranchId], [Barcode]) VALUES (1105, 1, N'PRIMARY', 21, N'Mobile', 1032, 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'B2C', CAST(0.0000 AS Decimal(24, 4)), CAST(18.0000 AS Decimal(24, 4)), 1, N'Sales Voucher', 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), NULL, 1, N'123')
INSERT [dbo].[GdwnStckTranLog] ([GSTranId], [GodwnId], [GodownName], [ProductId], [ProductName], [VchrId], [VchrNo], [VchrDate], [VchrType], [InQuantity], [OutQuantity], [UnitId], [Narration], [UserId], [TranDate], [FK_BatchId], [FK_BranchId], [Barcode]) VALUES (1106, 1, N'PRIMARY', 21, N'Mobile', 1033, 2, CAST(N'2022-03-16T17:43:08.000' AS DateTime), N'B2C', CAST(0.0000 AS Decimal(24, 4)), CAST(1.0000 AS Decimal(24, 4)), 1, N'POS Entry', 1, CAST(N'2022-03-16T00:00:00.000' AS DateTime), NULL, 1, N'123')
INSERT [dbo].[GdwnStckTranLog] ([GSTranId], [GodwnId], [GodownName], [ProductId], [ProductName], [VchrId], [VchrNo], [VchrDate], [VchrType], [InQuantity], [OutQuantity], [UnitId], [Narration], [UserId], [TranDate], [FK_BatchId], [FK_BranchId], [Barcode]) VALUES (1107, 1, N'PRIMARY', 21, N'Mobile', 9, 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'B2C', CAST(1.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), 1, N'Purchase Voucher', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), NULL, 6, N'123')
INSERT [dbo].[GdwnStckTranLog] ([GSTranId], [GodwnId], [GodownName], [ProductId], [ProductName], [VchrId], [VchrNo], [VchrDate], [VchrType], [InQuantity], [OutQuantity], [UnitId], [Narration], [UserId], [TranDate], [FK_BatchId], [FK_BranchId], [Barcode]) VALUES (1108, 1, N'PRIMARY', 21, N'Mobile', 1034, 1, CAST(N'2022-03-17T17:49:01.000' AS DateTime), N'B2C', CAST(0.0000 AS Decimal(24, 4)), CAST(1.0000 AS Decimal(24, 4)), 1, N'POS Entry', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), NULL, 6, N'123')
SET IDENTITY_INSERT [dbo].[GdwnStckTranLog] OFF
GO
SET IDENTITY_INSERT [dbo].[Godown] ON 

INSERT [dbo].[Godown] ([GodwnId], [GodownName], [AddressOne], [AddressTwo], [AddressThree], [Phone], [Description]) VALUES (1, N'PRIMARY', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Godown] ([GodwnId], [GodownName], [AddressOne], [AddressTwo], [AddressThree], [Phone], [Description]) VALUES (2, N'DAMAGE GODOWN', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Godown] OFF
GO
SET IDENTITY_INSERT [dbo].[Institute] ON 

INSERT [dbo].[Institute] ([InstituteId], [InstituteName], [Place], [Address], [Country], [Pincod], [PhoneNo], [MobNo], [FaxNo], [website], [EmailId], [Sector], [Logo], [LogoPath], [Productserial], [GSTNo], [State], [StateCode], [BankName], [BankBranch], [IFSC], [AccNo], [LocalName], [CompanyId]) VALUES (1, N'WAA TECHNOLOGIES', N'', N'', N'', N'', N'', N'', N'', N'waatech.org', N'ceo@waatech.org', N'Private Ltd', NULL, N'', N'20.0.1015', N'', N'', 0, N'', N'', N'', N'', N'اسم الشركة', N'')
SET IDENTITY_INSERT [dbo].[Institute] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemNote] ON 

INSERT [dbo].[ItemNote] ([Id], [Note], [IsMain], [FieldOne]) VALUES (1, N'3', 0, NULL)
SET IDENTITY_INSERT [dbo].[ItemNote] OFF
GO
SET IDENTITY_INSERT [dbo].[KOTOrderMaster] ON 

INSERT [dbo].[KOTOrderMaster] ([KOTMasterId], [TockenNo], [DateAndTime], [CookingTime], [Terminal], [SerialNo], [FK_FYearId], [FK_CustomerId], [FK_UserId], [FK_StaffId], [TableId], [Remarks], [Notes], [FK_KOTPrinterId], [OrderType], [TotalQuantity], [TotalAmount], [FK_BranchId]) VALUES (1, 1, CAST(N'2021-12-22T13:45:17.000' AS DateTime), CAST(0.0000 AS Decimal(24, 4)), N'DESKTOP-MO', 1, 1, 1, NULL, NULL, 1, N'KOT', NULL, NULL, N'Take Away', CAST(3.0000 AS Decimal(24, 4)), CAST(360.0000 AS Decimal(24, 4)), 1)
SET IDENTITY_INSERT [dbo].[KOTOrderMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[KOTOrderSettings] ON 

INSERT [dbo].[KOTOrderSettings] ([KOTSettingsId], [FK_KOTMasterId], [ProductId], [Quantity], [UnitId], [Rate], [NETAmount], [Barcode], [ItemNote], [ProPriority]) VALUES (1, 1, 2, CAST(1.0000 AS Decimal(24, 4)), 1, CAST(10.0000 AS Decimal(24, 4)), CAST(10.0000 AS Decimal(24, 4)), N'2055', NULL, NULL)
INSERT [dbo].[KOTOrderSettings] ([KOTSettingsId], [FK_KOTMasterId], [ProductId], [Quantity], [UnitId], [Rate], [NETAmount], [Barcode], [ItemNote], [ProPriority]) VALUES (2, 1, 6, CAST(1.0000 AS Decimal(24, 4)), 1, CAST(150.0000 AS Decimal(24, 4)), CAST(150.0000 AS Decimal(24, 4)), N'010203', NULL, NULL)
INSERT [dbo].[KOTOrderSettings] ([KOTSettingsId], [FK_KOTMasterId], [ProductId], [Quantity], [UnitId], [Rate], [NETAmount], [Barcode], [ItemNote], [ProPriority]) VALUES (3, 1, 7, CAST(1.0000 AS Decimal(24, 4)), 1, CAST(200.0000 AS Decimal(24, 4)), CAST(200.0000 AS Decimal(24, 4)), N'01', NULL, NULL)
SET IDENTITY_INSERT [dbo].[KOTOrderSettings] OFF
GO
SET IDENTITY_INSERT [dbo].[MasterValue] ON 

INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (1, N'B2C', N'B2C', N'GSTFormTypes')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (2, N'B2B', N'B2B', N'GSTFormTypes')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (3, N'Interstate', N'Interstate', N'GSTFormTypes')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (4, N'Export', N'Export', N'GSTFormTypes')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (5, N'B2C', N'B2C', N'FormType')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (6, N'B2B', N'B2B', N'FormType')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (7, N'240', N'50', N'GButtonSize')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (8, N'135', N'95', N'PButtonSize')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (9, N'0', N'0', N'LastInvNoExported')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (10, N'Take Away', N'Take Away', N'DefaultSalesFormType')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (11, N'Take Away', N'Take Away', N'SalesFormType')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (12, N'URL', N'Secret', N'RemoteServer')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (13, N'1,500.00', N'DESKTOP-SF0K0SQ', N'CounterOppeningCash')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (14, N'DESKTOP-SF0K0SQ', N'1', N'SN')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (15, N'sa', N'ash@Pro#', N'RemoteCredential')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (16, N'103.174.102.109', N'Unipos', N'RemoteAddress')
INSERT [dbo].[MasterValue] ([Id], [Name], [Description], [MasterType]) VALUES (17, N'DESKTOP-WAATECH', N'1', N'SN')
SET IDENTITY_INSERT [dbo].[MasterValue] OFF
GO
SET IDENTITY_INSERT [dbo].[MenuDetails] ON 

INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (1, N'Firm', N'Firm', 0, N'Ashpro', N'', 0, NULL, 1, 0, 1, 1, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (2, N'Masters', N'Masters', 0, N'Ashpro', N'', 0, NULL, 1, 0, 1, 2, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (3, N'Transaction', N'Transaction', 0, N'Ashpro', N'', 0, NULL, 1, 0, 1, 3, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (4, N'Reports', N'Reports', 0, N'Ashpro', N'', 0, NULL, 1, 0, 1, 4, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (5, N'Staff', N'Staff/User', 0, N'Ashpro', N'', 0, NULL, 1, 0, 1, 5, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (6, N'Options', N'Options', 0, N'Ashpro', N'', 0, NULL, 1, 0, 1, 6, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (7, N'About', N'About', 0, N'Ashpro', N'', 0, NULL, 1, 0, 1, 7, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (8, N'AdminSettings', N'Admin Settings', 1, N'Ashpro', N'Admin', 1, NULL, 1, 0, 1, 1, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (9, N'CompanyDetails', N'Company Details', 1, N'Ashpro', N'CompanyDetails', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (10, N'FinancialPeriod', N'Financial Year', 1, N'Ashpro', N'FinancialPeriod', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (11, N'SelectedFyear', N'Selected Financial Year', 1, N'Ashpro', N'SelectedFyear', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (12, N'AccountGroup', N'Account Group', 2, N'Ashpro', N'AccountGroup', 1, NULL, 1, 0, 1, 1, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (13, N'Account', N'Account/Ledger', 2, N'Ashpro', N'Account', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (14, N'Customer', N'Customer/Party', 2, N'Ashpro', N'Account', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (15, N'Supplier', N'Supplier/Vendor', 2, N'Ashpro', N'Account', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (16, N'Expense', N'Expense Account', 2, N'Ashpro', N'Account', 1, NULL, 1, 0, 1, 5, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (17, N'Bank', N'Bank', 2, N'Ashpro', N'Account', 1, NULL, 1, 0, 1, 6, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (18, N'ProductGroup', N'Product Group', 2, N'Ashpro', N'ProductGroup', 1, NULL, 1, 0, 1, 7, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (19, N'Product', N'Product', 2, N'Ashpro', N'Product', 1, NULL, 1, 0, 1, 8, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (20, N'Brand', N'Brand', 2, N'Ashpro', N'Brand', 1, NULL, 1, 0, 1, 9, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (21, N'Unit', N'Unit', 2, N'Ashpro', N'Unit', 1, NULL, 1, 0, 1, 10, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (22, N'Godown', N'Godown', 2, N'Ashpro', N'Godown', 1, NULL, 1, 0, 1, 11, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (23, N'Shelfs', N'Shelf', 2, N'Ashpro', N'Shelfs', 1, NULL, 1, 0, 1, 12, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (24, N'PriceCategory', N'Price Category', 2, N'Ashpro', N'PriceCategory', 1, NULL, 1, 0, 1, 13, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (25, N'TaxCategory', N'Tax Category', 2, N'Ashpro', N'TaxCategory', 1, NULL, 1, 0, 1, 14, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (26, N'Route', N'Route', 2, N'Ashpro', N'Route', 1, NULL, 1, 0, 1, 15, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (27, N'Vehicle', N'Vehicle', 2, N'Ashpro', N'Vehicle', 1, NULL, 1, 0, 1, 16, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (28, N'SalesCounter', N'Sales Counter', 2, N'Ashpro', N'SalesCounter', 1, NULL, 1, 0, 1, 17, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (29, N'Cost_Centres', N'Cost Centres', 2, N'Ashpro', N'Cost_Centres', 1, NULL, 1, 0, 1, 18, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (30, N'CustomerPrevilegeCard', N'Customer PrevilegeCard', 2, N'Ashpro', N'CustomerPrevilegeCard', 1, NULL, 1, 0, 1, 19, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (31, N'DebitCreditCardMaster', N'Debit Credit Card Master', 2, N'Ashpro', N'DebitCreditCardMaster', 1, NULL, 1, 0, 1, 20, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (32, N'Sales', N'Sales', 3, N'Ashpro', N'Sales', 0, NULL, 1, 0, 1, 1, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (33, N'Purchase', N'Purchase', 3, N'Ashpro', N'Purchase', 0, NULL, 1, 0, 1, 2, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (34, N'Inventory', N'Inventory', 3, N'Ashpro', N'Inventory', 0, NULL, 1, 0, 1, 3, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (35, N'Account', N'Account', 3, N'Ashpro', N'Account', 0, NULL, 1, 0, 1, 4, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (36, N'POS', N'POS', 32, N'Ashpro', N'POSView', 1, NULL, 1, 0, 1, 1, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (37, N'SalesInvoice', N'Sales Invoice', 32, N'Ashpro', N'SalesInvoiceView', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (38, N'SalesEstimate', N'Sales Estimate', 32, N'Ashpro', N'SalesEstimateView', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (39, N'SalesReturn', N'Sales Return', 32, N'Ashpro', N'SalesReturnView', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (40, N'SalesOrder', N'Sales Order', 32, N'Ashpro', N'SalesOrder', 1, NULL, 1, 0, 1, 5, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (41, N'SalesQuotation', N'Sales Quotation', 32, N'Ashpro', N'SalesQuotation', 1, NULL, 1, 0, 1, 6, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (42, N'SalesView', N'Sales View', 32, N'Ashpro', N'SalesView', 1, NULL, 1, 0, 1, 7, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (43, N'BillPrint', N'Bill Print', 32, N'Ashpro', N'BillPrint', 1, NULL, 1, 0, 1, 8, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (44, N'TransactionWithoutInventory', N'Transaction Without Inventory', 32, N'Ashpro', N'TransactionWithoutInventory', 1, NULL, 1, 0, 1, 9, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (45, N'PurchaseInvoice', N'Purchase Invoice', 33, N'Ashpro', N'PurchaseInvoiceView', 1, NULL, 1, 0, 1, 1, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (46, N'PurchaseReturn', N'Purchase Return', 33, N'Ashpro', N'PurchaseReturnView', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (47, N'PurchaseOrder', N'Purchase Order', 33, N'Ashpro', N'PurchaseOrder', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (48, N'FirstPurchase', N'First Purchase', 33, N'Ashpro', N'FirstPurchase', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (49, N'Packing', N'Packing', 34, N'Ashpro', N'Packing', 1, NULL, 1, 0, 1, 1, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (50, N'StockTrancfer', N'Stock Trancfer', 34, N'Ashpro', N'StockTrancfer', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (51, N'DamageEntry', N'Damage Entry', 34, N'Ashpro', N'DamageEntry', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (52, N'StockAdjuster', N'Stock Adjuster', 34, N'Ashpro', N'StockAdjusterView', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (53, N'ProductEditing', N'Product Editing', 34, N'Ashpro', N'ProductEditing', 1, NULL, 1, 0, 1, 5, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (54, N'ProductCombination', N'Product Combination', 34, N'Ashpro', N'ProductCombination', 1, NULL, 1, 0, 1, 6, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (55, N'BarcodeDesign', N'Barcode Design', 34, N'Ashpro', N'BarcodeDesign', 1, NULL, 1, 0, 1, 7, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (56, N'PrintBarcode', N'Print Barcode', 34, N'Ashpro', N'PrintBarcode', 1, NULL, 1, 0, 1, 8, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (57, N'ProductManufacturing', N'Product Manufacturing', 34, N'Ashpro', N'ProductManufacturingView', 1, NULL, 1, 0, 1, 9, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (58, N'CashPayment', N'Cash Payment', 35, N'Ashpro', N'CashPayment', 1, NULL, 1, 0, 1, 1, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (59, N'CashReceipt', N'Cash Receipt', 35, N'Ashpro', N'CashReceipt', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (60, N'BankPayment', N'Bank Payment', 35, N'Ashpro', N'BankPayment', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (61, N'BankReceipt', N'Bank Receipt', 35, N'Ashpro', N'BankReceipt', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (62, N'JournalEntry', N'Journal Entry', 35, N'Ashpro', N'JournalEntryView', 1, NULL, 1, 0, 1, 5, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (63, N'OpeningBalance', N'Opening Balance', 35, N'Ashpro', N'OpeningBalance', 1, NULL, 1, 0, 1, 6, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (64, N'DebitNote', N'Debit Note', 35, N'Ashpro', N'DebitNoteView', 1, NULL, 1, 0, 1, 7, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (65, N'CreditNote', N'Credit Note', 35, N'Ashpro', N'CreditNoteView', 1, NULL, 1, 0, 1, 8, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (66, N'ChequeRecipt', N'Cheque Recipt', 35, N'Ashpro', N'ChequeRecipt', 1, NULL, 1, 0, 1, 9, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (67, N'ChequePayment', N'Cheque Payment', 35, N'Ashpro', N'ChequePayment', 1, NULL, 1, 0, 1, 10, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (68, N'Transaction', N'Transaction', 4, N'Ashpro', N'Transaction', 0, NULL, 1, 0, 1, 1, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (69, N'Inventory', N'Inventory', 4, N'Ashpro', N'Inventory', 0, NULL, 1, 0, 1, 2, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (70, N'Accounts', N'Accounts', 4, N'Ashpro', N'Accounts', 0, NULL, 1, 0, 1, 3, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (71, N'BankReconciliation', N'Bank Reconciliation', 35, N'Ashpro', N'BankReconciliation', 1, NULL, 1, 0, 1, 11, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (72, N'AdvanceReport', N'Advance Report', 68, N'Ashpro', N'AdvanceReport', 0, NULL, 1, 0, 1, 1, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (73, N'SalesReport', N'Sales Report', 68, N'Ashpro', N'SalesReportView', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (74, N'SalesEstimateReport', N'Sales Estimate Report', 68, N'Ashpro', N'SalesEstimateReportView', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (75, N'SalesReturnReport', N'Sales Return Report', 68, N'Ashpro', N'SalesReturnReportView', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (76, N'SalesOrderReport', N'Sales Order Report', 68, N'Ashpro', N'SalesOrderReportView', 1, NULL, 1, 0, 1, 5, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (77, N'SalesQuatationReport', N'Sales Quatation Report', 68, N'Ashpro', N'SalesQuotationReportView', 1, NULL, 1, 0, 1, 6, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (78, N'PurchaseReport', N'Purchase Report', 68, N'Ashpro', N'PurchaseReportView', 1, NULL, 1, 0, 1, 7, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (79, N'PurchaseReturnReport', N'Purchase Return Report', 68, N'Ashpro', N'PurchaseReturnReportView', 1, NULL, 1, 0, 1, 8, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (80, N'PurchaseOrderReport', N'Purchase Order Report', 68, N'Ashpro', N'SalesOrderReportView', 1, NULL, 1, 0, 1, 9, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (81, N'SalesAndSalesReturnReport', N'Sales And Sales Return Report', 68, N'Ashpro', N'SalesAndSalesReturnReportView', 1, NULL, 1, 0, 1, 10, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (82, N'PurchaseAndPurchaseReturnReport', N'Purchase And Purchase Return Report', 68, N'Ashpro', N'PurchaseAndPurchaseReturnReportView', 1, NULL, 1, 0, 1, 11, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (83, N'CollectionReport', N'Collection Report', 68, N'Ashpro', N'CollectionReportView', 1, NULL, 1, 0, 1, 12, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (84, N'GSTSalesRpt', N'GST Sales Report', 68, N'Ashpro', N'GSTSalesRptView', 1, NULL, 1, 0, 1, 13, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (85, N'GSTPurchaseRpt', N'GST Purchase Report', 68, N'Ashpro', N'GSTPurchaseRptView', 1, NULL, 1, 0, 1, 14, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (86, N'GroupWiseSalesReport', N'Group Wise Sales Report', 72, N'Ashpro', N'GroupWiseSalesReportView', 1, NULL, 1, 0, 1, 1, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (87, N'PriceCategoryWiseSalesReport', N'Price Category Wise Sales Report', 72, N'Ashpro', N'PriceCategoryWiseSalesReportView', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (88, N'RouteWiseSalesReport', N'Route Wise Sales Report', 72, N'Ashpro', N'RouteWiseSalesReportView', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (89, N'SalesmanWiseSalesReport', N'Salesman Wise Sales Report', 72, N'Ashpro', N'SalesmanWiseSalesReportView', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (90, N'PartyWiseSalesReport', N'Party Wise Sales Report', 72, N'Ashpro', N'PartyWiseSalesReportView', 1, NULL, 1, 0, 1, 5, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (91, N'GroupWisePurchasereport', N'Group Wise Purchase report', 72, N'Ashpro', N'GroupWisePurchasereportView', 1, NULL, 1, 0, 1, 6, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (92, N'SupplierWisePurchaseReport', N'Supplier Wise Purchase Report', 72, N'Ashpro', N'SupplierWisePurchaseReportView', 1, NULL, 1, 0, 1, 7, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (93, N'ExicutiveWiseCollectionReport', N'Exicutive Wise Collection Report', 72, N'Ashpro', N'ExicutiveWiseCollectionReportView', 1, NULL, 1, 0, 1, 8, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (94, N'PendingOrderReport', N'Pending Order Report', 72, N'Ashpro', N'PendingOrderReportView', 1, NULL, 1, 0, 1, 9, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (95, N'SalesSummaryReport', N'Sales Summary Report', 72, N'Ashpro', N'SalesSummaryReportView', 1, NULL, 1, 0, 1, 10, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (96, N'SalesReturnSummary Report', N'Sales Return Summary Report', 72, N'Ashpro', N'SalesReturnSummaryView', 1, NULL, 1, 0, 1, 11, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (97, N'PurchaseSummaryReport', N'Purchase Summary Report', 72, N'Ashpro', N'PurchaseSummaryReportView', 1, NULL, 1, 0, 1, 12, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (98, N'PurchaseReturnSummaryReport', N'Purchase Return Summary Report', 72, N'Ashpro', N'PurchaseReturnSummaryReportView', 1, NULL, 1, 0, 1, 13, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (99, N'StockReport', N'Stock Report', 69, N'Ashpro', N'StockReportView', 1, NULL, 1, 0, 1, 1, 1, NULL, NULL)
GO
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (100, N'ItemWiseSalesReport', N'Item Wise Sales Report', 69, N'Ashpro', N'ItemWiseSalesReportView', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (101, N'ItemWisePurchaseReport', N'Item Wise Purchase Report', 69, N'Ashpro', N'ItemWisePurchaseReportView', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (102, N'ProductLedgerReport', N'Product Ledger', 69, N'Ashpro', N'ProductLedgerReportView', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (103, N'StockTrnasferReport', N'Stock Trnasfer Report', 69, N'Ashpro', N'StockTrnasferReportView', 1, NULL, 1, 0, 1, 5, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (104, N'InventorySalesRegister', N'Inventory Sales Register', 69, N'Ashpro', N'InventorySalesRegisterView', 1, NULL, 1, 0, 1, 6, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (105, N'InventorySalesReturnRegister', N'Inventory Sales Return Register', 69, N'Ashpro', N'InventorySalesReturnRegisterView', 1, NULL, 1, 0, 1, 7, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (106, N'InventoryPurchaseRegister', N'Inventory Purchase Register', 69, N'Ashpro', N'InventoryPurchaseRegisterView', 1, NULL, 1, 0, 1, 8, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (107, N'InventoryPurchaseReturnRegister', N'Inventory Purchase Return Register', 69, N'Ashpro', N'InventoryPurchaseReturnRegisterView', 1, NULL, 1, 0, 1, 9, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (108, N'ProductFlowReport', N'Product Flow Report', 69, N'Ashpro', N'Product_Flow_Report_Settings', 1, NULL, 1, 0, 1, 10, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (109, N'UnSoldProductReport', N'UnSold Product Report', 69, N'Ashpro', N'UnSoldProductReportView', 1, NULL, 1, 0, 1, 11, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (110, N'GodownStockReport', N'Godown Stock Report', 69, N'Ashpro', N'GodownStockReportView', 1, NULL, 1, 0, 1, 12, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (111, N'ProductRegister', N'Product Register', 69, N'Ashpro', N'Product_Register_Settings', 1, NULL, 1, 0, 1, 13, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (112, N'PriceList', N'Price List', 69, N'Ashpro', N'PriceListView', 1, NULL, 1, 0, 1, 14, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (113, N'LedgerReport', N'Ledger Report', 70, N'Ashpro', N'LedgerReport', 1, NULL, 1, 0, 1, 1, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (114, N'CashBook', N'Cash Book', 70, N'Ashpro', N'CashBook', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (115, N'BankBook', N'Bank Book', 70, N'Ashpro', N'BankBook', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (116, N'DailyAccount', N'Day Book', 70, N'Ashpro', N'DailyAccountView', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (117, N'PaymentRegister', N'Payment Register', 70, N'Ashpro', N'PaymentRegister', 1, NULL, 1, 0, 1, 5, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (118, N'ReceiptRegister', N'Receipt Register', 70, N'Ashpro', N'ReceiptRegister', 1, NULL, 1, 0, 1, 6, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (119, N'JournalEntryReport', N'Journal Entry Report', 70, N'Ashpro', N'JournalEntryReportView', 1, NULL, 1, 0, 1, 7, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (120, N'TransactionReport', N'Transaction Report', 70, N'Ashpro', N'TransactionReportView', 1, NULL, 1, 0, 1, 8, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (121, N'PartyList', N'Party List', 70, N'Ashpro', N'PartyListView', 1, NULL, 1, 0, 1, 9, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (122, N'OutstandingReport', N'Amount Payable And Receivable Report', 70, N'Ashpro', N'OutstandingReportView', 1, NULL, 1, 0, 1, 10, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (123, N'TrialBalance', N'Trial Balance', 70, N'Ashpro', N'TrialBalanceView', 1, NULL, 1, 0, 1, 11, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (124, N'ProfitAndLoseAccount', N'Profit And Lose Account', 70, N'Ashpro', N'Profit_and_Lose_Account', 1, NULL, 1, 0, 1, 12, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (125, N'BalanceSheet', N'Balance Sheet', 70, N'Ashpro', N'Balance_Sheet', 1, NULL, 1, 0, 1, 13, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (126, N'ProductWiseGSTReport', N'GST-R Report', 70, N'Ashpro', N'ProductWiseGSTReportView', 1, NULL, 1, 0, 1, 14, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (127, N'ChangePassword', N'Change Password', 5, N'Ashpro', N'ChangePassword', 1, NULL, 1, 0, 1, 1, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (128, N'UserPrevilege', N'User Previlege', 5, N'Ashpro', N'UserPrevilegeView', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (129, N'UserManagement', N'User Management', 5, N'Ashpro', N'UserManagement', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (130, N'EmployeeDetails', N'Staff Registration', 5, N'Ashpro', N'EmployeeDetails', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (131, N'SalaryProcess', N'Salary Process', 5, N'Ashpro', N'SalaryProcessView', 1, NULL, 1, 0, 1, 5, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (132, N'StaffAttendance', N'Staff Attendance', 5, N'Ashpro', N'StaffAttendanceView', 1, NULL, 1, 0, 1, 6, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (133, N'ExportData', N'Export Data', 6, N'Ashpro', N'ExportData', 1, NULL, 1, 0, 1, 1, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (134, N'ImportData', N'Import Data', 6, N'Ashpro', N'ImportData', 1, NULL, 1, 0, 1, 2, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (135, N'ChangeDate', N'Change Date', 6, N'Ashpro', N'ChangeDate', 1, NULL, 1, 0, 1, 3, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (136, N'Reminder', N'Reminder', 6, N'Ashpro', N'ReminderView', 1, NULL, 1, 0, 1, 4, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (137, N'ApplicationSettings', N'Application Settings', 6, N'Ashpro', N'Application_Settings', 1, NULL, 1, 0, 1, 5, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (138, N'PrintDesign', N'Print Design', 6, N'Ashpro', N'PrintDesignView', 1, NULL, 1, 0, 1, 6, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (139, N'CustomTransReport', N'Custom Transaction Report', 6, N'Ashpro', N'CustomTransReport', 1, NULL, 1, 1, 1, 7, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (140, N'BackupDatabase', N'Backup Database', 6, N'Ashpro', N'BackupDatabase', 1, NULL, 1, 0, 1, 8, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (141, N'RestoreDatabase', N'Restore Database', 6, N'Ashpro', N'RestoreDatabase', 1, NULL, 1, 0, 1, 9, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (142, N'DatabaseReset', N'Database Reset', 6, N'Ashpro', N'DatabaseReset', 1, NULL, 1, 0, 1, 10, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (143, N'BillGeneration', N'Bill Generation', 6, N'Ashpro', N'BillGeneration', 1, NULL, 1, 0, 1, 11, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (144, N'DayClose', N'Day Close', 6, N'Ashpro', N'DayClose', 1, NULL, 1, 0, 1, 12, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (145, N'Calendar', N'Calendar', 6, N'Ashpro', N'Calendar', 1, NULL, 1, 0, 1, 13, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (146, N'SendMail', N'Send Mail', 6, N'Ashpro', N'SendMail', 1, NULL, 1, 0, 1, 14, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (147, N'OtherOption', N'Other Option', 6, N'Ashpro', N'OtherOption', 0, NULL, 1, 0, 1, 15, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (148, N'VideoTutorial', N'Video Tutorial', 6, N'Ashpro', N'VideoTutorial', 1, NULL, 1, 0, 1, 16, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (149, N'DisableModernDashboard', N'Disable Modern Dashboard', 6, N'Ashpro', N'DisableModernDashboard', 0, NULL, 1, 0, 1, 17, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (150, N'Calculator', N'Calculator', 147, N'Ashpro', N'Calculator', 1, NULL, 1, 1, 1, 1, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (151, N'Excel', N'Excel', 147, N'Windows', N'Excel.exe', 0, NULL, 1, 0, 1, 2, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (152, N'Notepad', N'Notepad', 147, N'Windows', N'Notepad.exe', 0, NULL, 1, 0, 1, 3, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (153, N'Paint', N'Paint', 147, N'Windows', N'mspaint.exe', 0, NULL, 1, 0, 1, 4, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (154, N'About', N'About Us', 7, N'Ashpro', N'About', 1, NULL, 1, 0, 1, 1, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (155, N'ContactUs', N'Contact Us', 7, N'Ashpro', N'ContactUs', 1, NULL, 1, 0, 1, 2, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (156, N'ReportBugs', N'Report Buggs', 7, N'Ashpro', N'ReportBugs', 1, NULL, 1, 0, 1, 3, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (157, N'QRCodeGenerator', N'QRCode Generator', 6, N'Ashpro', N'QRCodeGenerator', 1, NULL, 1, 0, 1, 19, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (158, N'BranchView', N'Branch', 2, N'Ashpro', N'BranchView', 1, NULL, 1, 0, 1, 21, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (159, N'BatchView', N'Batch', 2, N'Ashpro', N'BatchView', 1, NULL, 1, 0, 1, 22, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (160, N'LabelScale', N'Barcode Label Scale', 6, N'Ashpro', N'LabelScale', 0, NULL, 1, 0, 1, 18, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (161, N'ProductUnitCombination', N'Product Unit Combination', 6, N'Ashpro', N'ProductUnitCombination', 1, NULL, 1, 0, 1, 10, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (162, N'BulkReceiptView', N'BulkReceipt', 35, N'Ashpro', N'BulkReceiptView', 1, NULL, 1, 0, 1, 11, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (163, N'CurrencyView', N'Currency', 2, N'Ashpro', N'CurrencyView', 1, NULL, 1, 0, 1, 21, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (164, N'KOTPrinterView', N'KOT Printer', 2, N'Ashpro', N'KOTPrinterView', 1, NULL, 1, 0, 1, 22, 1, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (165, N'ProductPrinterAllocation', N'Product Printer Allocation', 6, N'Ashpro', N'ProductPrinterAllocation', 0, NULL, 1, 0, 1, 19, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (166, N'FavouriteProduct', N'Favourite Product Allocation', 6, N'Ashpro', N'FavouriteProduct', 0, NULL, 1, 0, 1, 20, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (167, N'NoteMasterView', N'Note Creation', 2, N'Ashpro', N'NoteMasterView', 0, NULL, 1, 0, 1, 23, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (168, N'CloseCash', N'Close Cash', 6, N'Ashpro', N'CloseCash', 1, NULL, 1, 0, 1, 21, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (169, N'Stock', N'Stock', 6, N'Ashpro', N'Stock', 1, NULL, 1, 0, 1, 22, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (170, N'RestaurantForm', N'Restaurant', 6, N'Ashpro', N'RestaurantForm', 1, NULL, 1, 0, 1, 23, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (171, N'POSSales', N'Sales Report', 68, N'Ashpro', N'Sales', 1, NULL, 1, 0, 1, 15, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (172, N'InventoryReport', N'POS Inventory Report', 68, N'Ashpro', N'InventoryReportView', 1, NULL, 1, 0, 1, 16, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (173, N'Customers', N'POS Customer Report', 68, N'Ashpro', N'CustomersReportView', 1, NULL, 1, 0, 1, 17, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (174, N'POSSupplier', N'POS Supplier Report', 68, N'Ashpro', N'SupplierReportView', 1, NULL, 1, 0, 1, 18, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (175, N'Payments', N'POS Payments Report', 68, N'Ashpro', N'PaymentsReportView', 1, NULL, 1, 0, 1, 19, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (176, N'POSTransaction', N'POS Transaction Report', 68, N'Ashpro', N'TransactionReportView', 1, NULL, 1, 0, 1, 20, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (177, N'Sales Summary', N'POS Sales Summary', 68, N'Ashpro', N'SalesSummaryView', 1, NULL, 1, 0, 1, 21, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (178, N'Taxes', N'POS Taxes', 68, N'Ashpro', N'TaxReportView', 1, NULL, 1, 0, 1, 22, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (179, N'Tax Category', N'POS Tax Category', 68, N'Ashpro', N'TaxCategoryReportView', 1, NULL, 1, 0, 1, 23, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (180, N'Stock and Profit', N'POS Stock and Profit', 68, N'Ashpro', N'StockAndProfitReportView', 1, NULL, 1, 0, 1, 24, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (181, N'Vat Report', N'POS Vat Report', 68, N'Ashpro', N'VatReportView', 1, NULL, 1, 0, 1, 25, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (182, N'Category Summary', N'POS Category Summary', 68, N'Ashpro', N'CategorySummaryReportView', 1, NULL, 1, 0, 1, 26, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (183, N'Sales Profit', N'POS Sales Profit', 68, N'Ashpro', N'SalesProfitReportView', 1, NULL, 1, 0, 1, 27, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (184, N'Sales by Product', N'POS Sales by Product', 68, N'Ashpro', N'SalesByProductReportView', 1, NULL, 1, 0, 1, 28, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (185, N'Cash Closed', N'POS Cash Closed', 68, N'Ashpro', N'CashClosedReportView', 1, NULL, 1, 0, 1, 29, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (186, N'Cash-Flow', N'POS Cash-Flow', 68, N'Ashpro', N'Cash-FlowReportView', 1, NULL, 1, 0, 1, 30, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (187, N'Users Sale', N'POS Users Sale', 68, N'Ashpro', N'UsersSaleReportView', 1, NULL, 1, 0, 1, 31, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (188, N'Products', N'POS Products', 68, N'Ashpro', N'ProductsReportView', 1, NULL, 1, 0, 1, 32, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (189, N'Inventory Diary', N'POS Inventory Diary', 68, N'Ashpro', N'InventoryDiaryReportView', 1, NULL, 1, 0, 1, 33, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (190, N'Inventory : Difference', N'POS Inventory Difference', 68, N'Ashpro', N'InventoryDifferenceReportView', 1, NULL, 1, 0, 1, 34, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (191, N'Opening Report', N'POS Opening Report', 68, N'Ashpro', N'OpeningReportView', 1, NULL, 1, 0, 1, 35, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (192, N'Customer Sales', N'POS Customer Sales', 68, N'Ashpro', N'CustomerSalesReportView', 1, NULL, 1, 0, 1, 37, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (193, N'Item Sales', N'POS Item Sales', 68, N'Ashpro', N'ItemSalesReportView', 1, NULL, 1, 0, 1, 38, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (194, N'Suppliers Report', N'POS Suppliers Report', 68, N'Ashpro', N'SuppliersReportView', 1, NULL, 1, 0, 1, 39, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (195, N'Supplier Diary', N'POS Supplier Diary', 68, N'Ashpro', N'SupplierDiaryReportView', 1, NULL, 1, 0, 1, 40, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (196, N'Products wise Purchase', N'POS Products wise Purchase', 68, N'Ashpro', N'ProductsWisePurchaseReportView', 1, NULL, 1, 0, 1, 41, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (197, N'Purchase Summary Report', N'POS Purchase Summary Report', 68, N'Ashpro', N'PurchaseSummaryReportView', 1, NULL, 1, 0, 1, 42, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (198, N'POSReports', N'POS Mini Report', 68, N'Ashpro', N'POSReports', 1, NULL, 1, 0, 1, 43, 0, NULL, NULL)
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (199, N'RemoteSyncView', N'Remote Sync', 6, N'Ashpro', N'RemoteSyncView', 1, NULL, 1, 0, 1, 21, 0, NULL, NULL)
GO
INSERT [dbo].[MenuDetails] ([Id], [Name], [MenuCaption], [ParentId], [AssemblyName], [ClassName], [IsForm], [Icon], [FK_ModuleId], [IsDialog], [Status], [Position], [IsCrudMenu], [MenuUrl], [IsFavorite]) VALUES (200, N'ActivationTransfer', N'Activation Transfer', 7, N'Ashpro', N'ActivationTransfer', 1, NULL, 1, 0, 1, 5, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MenuDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Modules] ON 

INSERT [dbo].[Modules] ([Id], [Name], [Description], [Status]) VALUES (1, N'Accounting And Inventory', N'Main Module', 1)
SET IDENTITY_INSERT [dbo].[Modules] OFF
GO
SET IDENTITY_INSERT [dbo].[PriceCategory] ON 

INSERT [dbo].[PriceCategory] ([PCategoryId], [PCategoryName], [Percentage], [SDiscountPercentage]) VALUES (1, N'REGULAR', NULL, NULL)
SET IDENTITY_INSERT [dbo].[PriceCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[PrintDesignMaster] ON 

INSERT [dbo].[PrintDesignMaster] ([printMasterId], [DesignName], [VochrType], [isDefault], [isLandScape], [PageHieght], [PageWidth], [LinesPerPage], [SpaceBtwnLines], [BackgroundImgPath], [PrinterName], [PrintType], [upScroll], [downScroll], [Riverse], [Bold_On], [Bold_Off], [Pitch], [FormType], [SpaceInLines]) VALUES (7, N'KOT1', N'KOT', 0, 0, 5000, 290, 40, 16, N'', N'Wondershare PDFelement', N'Normal', NULL, NULL, N'j1', N'E', N'F', N'P', N'KOT', 12)
INSERT [dbo].[PrintDesignMaster] ([printMasterId], [DesignName], [VochrType], [isDefault], [isLandScape], [PageHieght], [PageWidth], [LinesPerPage], [SpaceBtwnLines], [BackgroundImgPath], [PrinterName], [PrintType], [upScroll], [downScroll], [Riverse], [Bold_On], [Bold_Off], [Pitch], [FormType], [SpaceInLines]) VALUES (8, N'KOR2', N'KOT', 1, 0, 5000, 290, 40, 18, N'', N'Wondershare PDFelement', N'Normal', NULL, NULL, N'j1', N'E', N'F', N'P', N'KOT', 12)
INSERT [dbo].[PrintDesignMaster] ([printMasterId], [DesignName], [VochrType], [isDefault], [isLandScape], [PageHieght], [PageWidth], [LinesPerPage], [SpaceBtwnLines], [BackgroundImgPath], [PrinterName], [PrintType], [upScroll], [downScroll], [Riverse], [Bold_On], [Bold_Off], [Pitch], [FormType], [SpaceInLines]) VALUES (44, N'tst', N'Sales Invoice', 0, 0, 30000, 590, 40, 18, N'', N'Microsoft Print to PDF', N'Dotmatrix', NULL, NULL, N'j1', N'E', N'F', N'P', N'B2C', 12)
INSERT [dbo].[PrintDesignMaster] ([printMasterId], [DesignName], [VochrType], [isDefault], [isLandScape], [PageHieght], [PageWidth], [LinesPerPage], [SpaceBtwnLines], [BackgroundImgPath], [PrinterName], [PrintType], [upScroll], [downScroll], [Riverse], [Bold_On], [Bold_Off], [Pitch], [FormType], [SpaceInLines]) VALUES (46, N'INV', N'Sales Invoice', 0, 0, 1100, 850, 11, 12, N'E:\invoice\Nagadan Print Design\Back Img.JPG', N'Microsoft Print to PDF', N'Invoice ', NULL, NULL, N'j1', N'F', N'F', N'P', N'B2C', 382)
INSERT [dbo].[PrintDesignMaster] ([printMasterId], [DesignName], [VochrType], [isDefault], [isLandScape], [PageHieght], [PageWidth], [LinesPerPage], [SpaceBtwnLines], [BackgroundImgPath], [PrinterName], [PrintType], [upScroll], [downScroll], [Riverse], [Bold_On], [Bold_Off], [Pitch], [FormType], [SpaceInLines]) VALUES (49, N'R-POS', N'Sales Invoice', 0, 0, 30000, 590, 40, 18, N'', N'Microsoft Print to PDF', N'Normal', NULL, NULL, N'j1', N'E', N'F', N'P', N'B2C', 12)
INSERT [dbo].[PrintDesignMaster] ([printMasterId], [DesignName], [VochrType], [isDefault], [isLandScape], [PageHieght], [PageWidth], [LinesPerPage], [SpaceBtwnLines], [BackgroundImgPath], [PrinterName], [PrintType], [upScroll], [downScroll], [Riverse], [Bold_On], [Bold_Off], [Pitch], [FormType], [SpaceInLines]) VALUES (50, N'POS', N'Sales Invoice', 1, 0, 30000, 590, 40, 16, N'', N'EPSON TM-T20III Receipt', N'Normal', NULL, NULL, N'j1', N'E', N'F', N'P', N'B2C', 12)
SET IDENTITY_INSERT [dbo].[PrintDesignMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[PrintDesignSettings] ON 

INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (889, 7, N'TOCKEN#_lbl', N'LabelControl', N'TOCKEN#', 3, 35, 10, N'Microsoft Sans Serif', 1, 100, N'Left', N'Invoice Section', 1, 1, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (890, 7, N'TockenNo_val', N'ValueControle', N'TockenNo', 107, 35, 10, N'Microsoft Sans Serif', 1, 160, N'Left', N'Invoice Section', 2, 2, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (891, 7, N' _lbl', N'LabelControl', N'', 4, 58, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Invoice Section', 3, 3, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (892, 7, N'CookingTime_val', N'ValueControle', N'CookingTime', 107, 57, 8, N'Microsoft Sans Serif', 0, 160, N'Left', N'Invoice Section', 4, 4, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (893, 7, N'Date_lbl', N'LabelControl', N'Date', 3, 80, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Invoice Section', 5, 5, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (894, 7, N'Date_val', N'ValueControle', N'Date', 106, 81, 10, N'Microsoft Sans Serif', 0, 160, N'Left', N'Invoice Section', 6, 6, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (895, 7, N'Notes_val', N'ValueControle', N'Notes', 1, 108, 13, N'Microsoft Sans Serif', 1, 280, N'Center', N'Invoice Section', 7, 7, 30, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (896, 7, N'Notes_val', N'ValueControle', N'Notes', 1, 108, 10, N'Microsoft Sans Serif', 0, 280, N'Left', N'Invoice Section', 8, 8, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (897, 7, N'Line1', N'LineLabelControl', N'Line1', 1, 132, 8, N'Microsoft Sans Serif', 0, 290, N'Left', N'Invoice Section', 9, 9, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (898, 7, N'Itme_lbl', N'LabelControl', N'Itme', 2, 141, 10, N'Microsoft Sans Serif', 0, 120, N'Left', N'Invoice Details Header ', 10, 10, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (899, 7, N'Qty_lbl', N'LabelControl', N'Qty', 161, 141, 8, N'Microsoft Sans Serif', 0, 60, N'Right', N'Invoice Details Header ', 11, 11, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (900, 7, N'Rate_lbl', N'LabelControl', N'Rate', 222, 141, 10, N'Microsoft Sans Serif', 0, 60, N'Right', N'Invoice Details Header ', 12, 12, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (901, 7, N'Line2', N'LineLabelControl', N'Line2', 1, 162, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Invoice Details Header ', 13, 13, 4, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (902, 7, N'ProductName_val', N'ValueControle', N'ProductName', 1, 168, 11, N'Calibri', 1, 150, N'Left', N'Details Area', 14, 14, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (903, 7, N'Quantity_val', N'ValueControle', N'Quantity', 153, 170, 12, N'Microsoft Sans Serif', 1, 50, N'Right', N'Details Area', 15, 15, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (904, 7, N'NETAmount_val', N'ValueControle', N'NETAmount', 219, 170, 12, N'Microsoft Sans Serif', 1, 60, N'Right', N'Details Area', 16, 16, 25, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (905, 7, N'Line3', N'LineLabelControl', N'Line3', 2, 205, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 17, 17, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (906, 7, N'Total Quantity_lbl', N'LabelControl', N'Total Quantity', 1, 215, 10, N'Microsoft Sans Serif', 0, 150, N'Left', N'Footer Section', 18, 18, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (907, 7, N'TotalQuantity_val', N'ValueControle', N'TotalQuantity', 152, 215, 12, N'Microsoft Sans Serif', 1, 80, N'Right', N'Footer Section', 19, 19, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (908, 7, N'Total_lbl', N'LabelControl', N'Total', 2, 239, 12, N'Microsoft Sans Serif', 1, 150, N'Left', N'Footer Section', 20, 20, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (909, 7, N'GrandTotal_val', N'ValueControle', N'GrandTotal', 153, 239, 12, N'Microsoft Sans Serif', 1, 90, N'Right', N'Footer Section', 21, 21, 25, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (910, 7, N'LocalName_val', N'ValueControle', N'LocalName', 1, 189, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Details Area', 22, 22, 20, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (911, 7, N'KOTPrinterName_val', N'ValueControle', N'KOTPrinterName', 5, 4, 14, N'Microsoft Sans Serif', 1, 290, N'Center', N'Invoice Section', 22, 22, 30, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (912, 8, N'KOTTitle_val', N'ValueControle', N'KOTTitle', 1, 1, 12, N'Microsoft Sans Serif', 1, 290, N'Center', N'Invoice Section', 0, 0, 30, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (913, 8, N'TOCKEN#_lbl', N'LabelControl', N'TOCKEN#', 3, 35, 10, N'Microsoft Sans Serif', 1, 100, N'Left', N'Invoice Section', 1, 1, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (914, 8, N'TockenNo_val', N'ValueControle', N'TockenNo', 107, 35, 12, N'Microsoft Sans Serif', 1, 160, N'Left', N'Invoice Section', 2, 2, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (915, 8, N' _lbl', N'LabelControl', N'', 4, 58, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Invoice Section', 3, 3, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (916, 8, N'Date_lbl', N'LabelControl', N'Date', 3, 80, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Invoice Section', 5, 5, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (917, 8, N'Date_val', N'ValueControle', N'Date', 106, 81, 10, N'Microsoft Sans Serif', 0, 160, N'Left', N'Invoice Section', 6, 6, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (918, 8, N'Notes_val', N'ValueControle', N'Notes', 1, 108, 13, N'Microsoft Sans Serif', 1, 280, N'Center', N'Invoice Section', 7, 7, 30, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (919, 8, N'Notes_val', N'ValueControle', N'Notes', 1, 108, 10, N'Microsoft Sans Serif', 0, 280, N'Left', N'Invoice Section', 8, 8, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (920, 8, N'Line1', N'LineLabelControl', N'Line1', 1, 132, 8, N'Microsoft Sans Serif', 0, 290, N'Left', N'Invoice Section', 9, 9, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (921, 8, N'Itme_lbl', N'LabelControl', N'Itme', 2, 141, 10, N'Microsoft Sans Serif', 0, 120, N'Left', N'Invoice Details Header ', 10, 10, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (922, 8, N'Qty_lbl', N'LabelControl', N'Qty', 161, 141, 8, N'Microsoft Sans Serif', 0, 60, N'Right', N'Invoice Details Header ', 11, 11, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (923, 8, N'Rate_lbl', N'LabelControl', N'Rate', 222, 141, 10, N'Microsoft Sans Serif', 0, 60, N'Right', N'Invoice Details Header ', 12, 12, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (924, 8, N'Line2', N'LineLabelControl', N'Line2', 1, 162, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Invoice Details Header ', 13, 13, 4, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (925, 8, N'ProductName_val', N'ValueControle', N'ProductName', 1, 168, 11, N'Calibri', 1, 150, N'Left', N'Details Area', 14, 14, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (926, 8, N'Quantity_val', N'ValueControle', N'Quantity', 153, 170, 12, N'Microsoft Sans Serif', 1, 50, N'Right', N'Details Area', 15, 15, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (927, 8, N'NETAmount_val', N'ValueControle', N'NETAmount', 219, 170, 12, N'Microsoft Sans Serif', 1, 60, N'Right', N'Details Area', 16, 16, 25, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (928, 8, N'Line3', N'LineLabelControl', N'Line3', 2, 205, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 17, 17, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (929, 8, N'Total Quantity_lbl', N'LabelControl', N'Total Quantity', 1, 215, 10, N'Microsoft Sans Serif', 0, 150, N'Left', N'Footer Section', 18, 18, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (930, 8, N'TotalQuantity_val', N'ValueControle', N'TotalQuantity', 152, 215, 12, N'Microsoft Sans Serif', 1, 80, N'Right', N'Footer Section', 19, 19, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (931, 8, N'Total_lbl', N'LabelControl', N'Total', 2, 239, 12, N'Microsoft Sans Serif', 1, 150, N'Left', N'Footer Section', 20, 20, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (932, 8, N'GrandTotal_val', N'ValueControle', N'GrandTotal', 153, 239, 12, N'Microsoft Sans Serif', 1, 90, N'Right', N'Footer Section', 21, 21, 25, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (933, 8, N'LocalName_val', N'ValueControle', N'LocalName', 1, 189, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Details Area', 22, 22, 20, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (934, 8, N'SalesFormType_val', N'ValueControle', N'SalesFormType', 106, 58, 14, N'Microsoft Sans Serif', 1, 160, N'Left', N'Invoice Section', 22, 22, 30, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4213, 44, N'CompanyName_val', N'ValueControle', N'CompanyName', 1, 1, 12, N'Calibri', 1, 290, N'Center', N'Company Section', 0, 0, 23, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4214, 44, N'LocalName_val', N'ValueControle', N'LocalName', 1, 22, 12, N'Calibri', 1, 290, N'Center', N'Company Section', 1, 1, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4215, 44, N'Place_val', N'ValueControle', N'Place', 1, 41, 10, N'Open Sans Semibold', 1, 290, N'Center', N'Company Section', 2, 2, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4216, 44, N'Address_val', N'ValueControle', N'Address', 0, 60, 10, N'Open Sans Semibold', 0, 290, N'Center', N'Company Section', 3, 3, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4217, 44, N' _lbl', N'LabelControl', N'TOCKEN#', 5, 130, 14, N'Microsoft Sans Serif', 1, 120, N'Left', N'Invoice Section', 4, 4, 30, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4218, 44, N'PhoneNo_val', N'ValueControle', N'PhoneNo', 1, 81, 10, N'Open Sans Semibold', 0, 290, N'Center', N'Company Section', 5, 5, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4219, 44, N'TockenNo_val', N'ValueControle', N'TockenNo', 107, 130, 18, N'Microsoft Sans Serif', 1, 150, N'Right', N'Invoice Section', 6, 6, 35, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4220, 44, N'TaxId_val', N'ValueControle', N'TaxId', 1, 102, 10, N'Open Sans Semibold', 1, 290, N'Center', N'Company Section', 7, 7, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4221, 44, N'Line1', N'LineLabelControl', N'Line1', 1, 121, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Company Section', 8, 8, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4222, 44, N'رقم الفاتورة_lbl', N'LabelControl', N'رقم الفاتورة', 205, 276, 8, N'Microsoft Sans Serif', 0, 80, N'Right', N'Invoice Section', 10, 10, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4223, 44, N'Bill No_lbl', N'LabelControl', N'Bill No', 2, 276, 8, N'Open Sans Semibold', 0, 100, N'Left', N'Invoice Section', 11, 11, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4224, 44, N'InvoiceNo_val', N'ValueControle', N'InvoiceNo', 58, 276, 10, N'Open Sans Semibold', 0, 180, N'Center', N'Invoice Section', 12, 12, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4225, 44, N'Date_lbl', N'LabelControl', N'Date', 2, 299, 9, N'Open Sans Semibold', 0, 100, N'Left', N'Invoice Section', 13, 13, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4226, 44, N'تاريخ_lbl', N'LabelControl', N'تاريخ', 205, 299, 9, N'Open Sans Semibold', 0, 80, N'Right', N'Invoice Section', 14, 14, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4227, 44, N'Date_val', N'ValueControle', N'Date', 57, 299, 10, N'Open Sans Semibold', 0, 180, N'Center', N'Invoice Section', 15, 15, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4228, 44, N'Line2', N'LineLabelControl', N'Line2', 2, 321, 8, N'Microsoft Sans Serif', 0, 290, N'Left', N'Invoice Section', 16, 16, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4229, 44, N'Name_lbl', N'LabelControl', N'Name', 2, 328, 10, N'Open Sans Semibold', 0, 50, N'Left', N'Party Section', 17, 17, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4230, 44, N'عميل_lbl', N'LabelControl', N'عميل', 245, 328, 8, N'Open Sans Semibold', 0, 40, N'Right', N'Party Section', 18, 18, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4231, 44, N'Name_val', N'ValueControle', N'Name', 54, 328, 10, N'Open Sans Semibold', 0, 200, N'Center', N'Party Section', 19, 20, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4232, 44, N'ProductName_val', N'ValueControle', N'ProductName', 1, 408, 10, N'Open Sans Semibold', 0, 140, N'Left', N'Details Area', 30, 30, 17, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4233, 44, N'Quantity_val', N'ValueControle', N'Quantity', 141, 408, 10, N'Open Sans Semibold', 0, 40, N'Right', N'Details Area', 31, 31, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4234, 44, N'Rate_val', N'ValueControle', N'Rate', 181, 408, 8, N'Open Sans Semibold', 0, 40, N'Right', N'Details Area', 32, 32, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4235, 44, N'NETAmount_val', N'ValueControle', N'NETAmount', 223, 408, 10, N'Open Sans Semibold', 0, 63, N'Right', N'Details Area', 33, 33, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4236, 44, N'LocalName_val', N'ValueControle', N'LocalName', 2, 428, 10, N'Open Sans Semibold', 0, 285, N'Right', N'Details Area', 34, 34, 17, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4237, 44, N'Line5', N'LineLabelControl', N'Line5', 1, 449, 8, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 35, 35, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4238, 44, N'Total Qty_lbl', N'LabelControl', N'Total Qty', 1, 457, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 36, 36, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4239, 44, N'الكمية الإجمالية_lbl', N'LabelControl', N'الكمية الإجمالية', 205, 457, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 37, 37, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4240, 44, N'TotalQuantity_val', N'ValueControle', N'TotalQuantity', 72, 457, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 38, 38, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4241, 44, N'Sub Total_lbl', N'LabelControl', N'Sub Total', 1, 479, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 39, 39, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4242, 44, N'المجموع الفرعي_lbl', N'LabelControl', N'المجموع الفرعي', 205, 479, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 40, 40, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4243, 44, N'Taxable_val', N'ValueControle', N'Taxable', 72, 479, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 41, 41, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4244, 44, N'VAT_lbl', N'LabelControl', N'VAT 15%', 1, 501, 8, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 42, 42, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4245, 44, N'ضريبة_lbl', N'LabelControl', N'ضريبة 15 %', 205, 501, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 43, 43, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4246, 44, N'VatAmount_val', N'ValueControle', N'VatAmount', 72, 501, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 44, 44, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4247, 44, N'Discount_lbl', N'LabelControl', N'Discount', 1, 523, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 45, 45, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4248, 44, N'خصم_lbl', N'LabelControl', N'خصم', 205, 523, 8, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 46, 46, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4249, 44, N'BillDiscount_val', N'ValueControle', N'BillDiscount', 71, 523, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 47, 47, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4250, 44, N'TOTAL_lbl', N'LabelControl', N'TOTAL', 1, 545, 12, N'Open Sans Semibold', 1, 70, N'Left', N'Footer Section', 48, 48, 24, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4251, 44, N'مجموع_lbl', N'LabelControl', N'مجموع', 205, 545, 13, N'Open Sans Semibold', 1, 80, N'Right', N'Footer Section', 49, 49, 24, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4252, 44, N'GrandTotal_val', N'ValueControle', N'GrandTotal', 72, 545, 12, N'Open Sans Semibold', 1, 130, N'Center', N'Footer Section', 50, 50, 24, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4253, 44, N'Line6', N'LineLabelControl', N'Line6', 1, 570, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 51, 51, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4254, 44, N'Cash_lbl', N'LabelControl', N'Cash', 1, 574, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 52, 52, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4255, 44, N'السيولة النقدية_lbl', N'LabelControl', N'السيولة النقدية', 205, 574, 8, N'Open Sans Semibold', 0, 80, N'Left', N'Footer Section', 53, 53, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4256, 44, N'CashPayment_val', N'ValueControle', N'CashPayment', 73, 574, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 54, 54, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4257, 44, N'Card_lbl', N'LabelControl', N'Card', 1, 596, 10, N'Microsoft Sans Serif', 0, 70, N'Left', N'Footer Section', 55, 55, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4258, 44, N'بطاقة_lbl', N'LabelControl', N'بطاقة', 205, 596, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 56, 56, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4259, 44, N'CardPayment_val', N'ValueControle', N'CardPayment', 74, 596, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 57, 57, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4260, 44, N'Online_lbl', N'LabelControl', N'Online', 0, 620, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 58, 58, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4261, 44, N'عبر الانترنت_lbl', N'LabelControl', N'عبر الانترنت', 206, 620, 8, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 59, 59, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4262, 44, N'OtherPayment_val', N'ValueControle', N'OtherPayment', 74, 620, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 60, 60, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4263, 44, N'Line7', N'LineLabelControl', N'Line7', 2, 641, 8, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 61, 61, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4264, 44, N'Line7', N'LineLabelControl', N'Line7', 1, 351, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Party Section', 61, 61, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4265, 44, N'Tendered_lbl', N'LabelControl', N'Tendered', 1, 645, 10, N'Microsoft Sans Serif', 0, 70, N'Left', N'Footer Section', 62, 62, 20, N'')
GO
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4266, 44, N'Item_lbl', N'LabelControl', N'Item', 1, 350, 10, N'Microsoft Sans Serif', 0, 120, N'Left', N'Invoice Details Header ', 62, 62, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4267, 44, N'مقدار وسطي_lbl', N'LabelControl', N'مقدار وسطي', 207, 645, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 63, 63, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4268, 44, N'Qty_lbl', N'LabelControl', N'Qty', 139, 350, 10, N'Microsoft Sans Serif', 0, 40, N'Left', N'Invoice Details Header ', 63, 63, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4269, 44, N'Tendered_val', N'ValueControle', N'Tendered', 73, 645, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 64, 64, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4270, 44, N'Rate_lbl', N'LabelControl', N'Rate', 179, 350, 10, N'Microsoft Sans Serif', 0, 40, N'Left', N'Invoice Details Header ', 64, 64, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4271, 44, N'Changes_lbl', N'LabelControl', N'Changes', 1, 666, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 65, 65, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4272, 44, N'Net_lbl', N'LabelControl', N'Net', 225, 351, 10, N'Microsoft Sans Serif', 0, 60, N'Right', N'Invoice Details Header ', 65, 65, 20, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4273, 44, N'مناقصة_lbl', N'LabelControl', N'مناقصة', 207, 666, 8, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 66, 66, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4274, 44, N' اسم المنتج_lbl', N'LabelControl', N' اسم المنتج', 0, 370, 9, N'Tahoma', 0, 120, N'Left', N'Invoice Details Header ', 66, 66, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4275, 44, N'Changes_val', N'ValueControle', N'Changes', 73, 666, 10, N'Microsoft Sans Serif', 0, 130, N'Center', N'Footer Section', 67, 67, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4276, 44, N'كمية_lbl', N'LabelControl', N'كمية', 140, 370, 10, N'Microsoft Sans Serif', 0, 40, N'Left', N'Invoice Details Header ', 67, 67, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4277, 44, N'السعر_lbl', N'LabelControl', N'السعر', 179, 370, 10, N'Microsoft Sans Serif', 0, 40, N'Right', N'Invoice Details Header ', 68, 68, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4278, 44, N'كمية الشبكة_lbl', N'LabelControl', N'كمية الشبكة', 221, 370, 9, N'Microsoft Sans Serif', 0, 65, N'Right', N'Invoice Details Header ', 69, 69, 20, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4279, 44, N'Line9', N'LineLabelControl', N'Line9', 0, 393, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Invoice Details Header ', 70, 70, 4, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4280, 44, N'    _lbl', N'LabelControl', N'', 2, 185, 10, N'Microsoft Sans Serif', 0, 60, N'Left', N'Invoice Section', 70, 70, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4281, 44, N'Simplified Tax Invoice   فاتورة ضریبیة المبسطة_lbl', N'LabelControl', N'Simplified Tax Invoice   فاتورة ضریبیة المبسطة', 0, 0, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Company Section', 71, 71, 20, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4282, 44, N'Return Invoice Against   إعادة الفاتورة مقابل_lbl', N'LabelControl', N'Return Invoice Against   إعادة الفاتورة مقابل', 1, 256, 9, N'Calibri', 0, 220, N'Left', N'Invoice Section', 71, 71, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4283, 44, N'SalesFormType_val', N'ValueControle', N'SalesFormType', 107, 185, 14, N'Microsoft Sans Serif', 1, 150, N'Right', N'Invoice Section', 71, 71, 30, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4284, 44, N'RefNumber_val', N'ValueControle', N'RefNumber', 203, 256, 10, N'Microsoft Sans Serif', 0, 80, N'Right', N'Invoice Section', 72, 72, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (4285, 44, N'InvoiceQRCode_val', N'ValueControle', N'InvoiceQRCode', 78, 689, 10, N'Microsoft Sans Serif', 0, 125, N'Left', N'Footer Section', 72, 72, 125, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6305, 49, N'CompanyName_val', N'ValueControle', N'CompanyName', 1, 1, 12, N'Calibri', 1, 290, N'Center', N'Company Section', 0, 0, 23, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6306, 49, N'LocalName_val', N'ValueControle', N'LocalName', 1, 22, 12, N'Calibri', 1, 290, N'Center', N'Company Section', 1, 1, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6307, 49, N'Place_val', N'ValueControle', N'Place', 1, 41, 10, N'Open Sans Semibold', 1, 290, N'Center', N'Company Section', 2, 2, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6308, 49, N'Address_val', N'ValueControle', N'Address', 0, 60, 10, N'Open Sans Semibold', 0, 290, N'Center', N'Company Section', 3, 3, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6309, 49, N' _lbl', N'LabelControl', N'TOCKEN#', 5, 130, 14, N'Microsoft Sans Serif', 1, 120, N'Left', N'Invoice Section', 4, 4, 30, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6310, 49, N'PhoneNo_val', N'ValueControle', N'PhoneNo', 1, 81, 10, N'Open Sans Semibold', 0, 290, N'Center', N'Company Section', 5, 5, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6311, 49, N'TockenNo_val', N'ValueControle', N'TockenNo', 107, 130, 18, N'Microsoft Sans Serif', 1, 150, N'Right', N'Invoice Section', 6, 6, 35, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6312, 49, N'TaxId_val', N'ValueControle', N'TaxId', 1, 102, 10, N'Open Sans Semibold', 1, 290, N'Center', N'Company Section', 7, 7, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6313, 49, N'Line1', N'LineLabelControl', N'Line1', 1, 121, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Company Section', 8, 8, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6314, 49, N'رقم الفاتورة_lbl', N'LabelControl', N'رقم الفاتورة', 205, 276, 8, N'Microsoft Sans Serif', 0, 80, N'Right', N'Invoice Section', 10, 10, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6315, 49, N'Bill No_lbl', N'LabelControl', N'Bill No', 2, 276, 8, N'Open Sans Semibold', 0, 100, N'Left', N'Invoice Section', 11, 11, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6316, 49, N'InvoiceNo_val', N'ValueControle', N'InvoiceNo', 58, 276, 10, N'Open Sans Semibold', 0, 180, N'Center', N'Invoice Section', 12, 12, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6317, 49, N'Date_lbl', N'LabelControl', N'Date', 2, 299, 9, N'Open Sans Semibold', 0, 100, N'Left', N'Invoice Section', 13, 13, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6318, 49, N'تاريخ_lbl', N'LabelControl', N'تاريخ', 205, 299, 9, N'Open Sans Semibold', 0, 80, N'Right', N'Invoice Section', 14, 14, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6319, 49, N'Date_val', N'ValueControle', N'Date', 57, 299, 10, N'Open Sans Semibold', 0, 180, N'Center', N'Invoice Section', 15, 15, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6320, 49, N'Line2', N'LineLabelControl', N'Line2', 2, 321, 8, N'Microsoft Sans Serif', 0, 290, N'Left', N'Invoice Section', 16, 16, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6321, 49, N'Name_lbl', N'LabelControl', N'Name', 2, 328, 10, N'Open Sans Semibold', 0, 50, N'Left', N'Party Section', 17, 17, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6322, 49, N'عميل_lbl', N'LabelControl', N'عميل', 245, 328, 8, N'Open Sans Semibold', 0, 40, N'Right', N'Party Section', 18, 18, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6323, 49, N'Name_val', N'ValueControle', N'Name', 54, 328, 10, N'Open Sans Semibold', 0, 200, N'Center', N'Party Section', 19, 20, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6324, 49, N'ProductName_val', N'ValueControle', N'ProductName', 1, 408, 10, N'Open Sans Semibold', 0, 140, N'Left', N'Details Area', 30, 30, 17, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6325, 49, N'Quantity_val', N'ValueControle', N'Quantity', 141, 408, 10, N'Open Sans Semibold', 0, 40, N'Right', N'Details Area', 31, 31, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6326, 49, N'Rate_val', N'ValueControle', N'Rate', 181, 408, 8, N'Open Sans Semibold', 0, 40, N'Right', N'Details Area', 32, 32, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6327, 49, N'NETAmount_val', N'ValueControle', N'NETAmount', 223, 408, 10, N'Open Sans Semibold', 0, 63, N'Right', N'Details Area', 33, 33, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6328, 49, N'LocalName_val', N'ValueControle', N'LocalName', 2, 428, 10, N'Open Sans Semibold', 0, 285, N'Right', N'Details Area', 34, 34, 17, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6329, 49, N'Line5', N'LineLabelControl', N'Line5', 1, 449, 8, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 35, 35, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6330, 49, N'Total Qty_lbl', N'LabelControl', N'Total Qty', 1, 457, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 36, 36, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6331, 49, N'الكمية الإجمالية_lbl', N'LabelControl', N'الكمية الإجمالية', 205, 457, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 37, 37, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6332, 49, N'TotalQuantity_val', N'ValueControle', N'TotalQuantity', 72, 457, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 38, 38, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6333, 49, N'Sub Total_lbl', N'LabelControl', N'Sub Total', 1, 479, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 39, 39, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6334, 49, N'المجموع الفرعي_lbl', N'LabelControl', N'المجموع الفرعي', 205, 479, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 40, 40, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6335, 49, N'Taxable_val', N'ValueControle', N'Taxable', 72, 479, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 41, 41, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6336, 49, N'VAT_lbl', N'LabelControl', N'VAT 15%', 1, 501, 8, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 42, 42, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6337, 49, N'ضريبة_lbl', N'LabelControl', N'ضريبة 15 %', 205, 501, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 43, 43, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6338, 49, N'VatAmount_val', N'ValueControle', N'VatAmount', 72, 501, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 44, 44, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6339, 49, N'Discount_lbl', N'LabelControl', N'Discount', 1, 523, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 45, 45, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6340, 49, N'خصم_lbl', N'LabelControl', N'خصم', 205, 523, 8, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 46, 46, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6341, 49, N'BillDiscount_val', N'ValueControle', N'BillDiscount', 71, 523, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 47, 47, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6342, 49, N'TOTAL_lbl', N'LabelControl', N'TOTAL', 1, 545, 12, N'Open Sans Semibold', 1, 70, N'Left', N'Footer Section', 48, 48, 24, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6343, 49, N'مجموع_lbl', N'LabelControl', N'مجموع', 205, 545, 13, N'Open Sans Semibold', 1, 80, N'Right', N'Footer Section', 49, 49, 24, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6344, 49, N'GrandTotal_val', N'ValueControle', N'GrandTotal', 72, 545, 12, N'Open Sans Semibold', 1, 130, N'Center', N'Footer Section', 50, 50, 24, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6345, 49, N'Line6', N'LineLabelControl', N'Line6', 1, 570, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 51, 51, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6346, 49, N'Cash_lbl', N'LabelControl', N'Cash', 1, 574, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 52, 52, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6347, 49, N'السيولة النقدية_lbl', N'LabelControl', N'السيولة النقدية', 205, 574, 8, N'Open Sans Semibold', 0, 80, N'Left', N'Footer Section', 53, 53, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6348, 49, N'CashPayment_val', N'ValueControle', N'CashPayment', 73, 574, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 54, 54, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6349, 49, N'Card_lbl', N'LabelControl', N'Card', 1, 596, 10, N'Microsoft Sans Serif', 0, 70, N'Left', N'Footer Section', 55, 55, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6350, 49, N'بطاقة_lbl', N'LabelControl', N'بطاقة', 205, 596, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 56, 56, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6351, 49, N'CardPayment_val', N'ValueControle', N'CardPayment', 74, 596, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 57, 57, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6352, 49, N'Online_lbl', N'LabelControl', N'Online', 0, 620, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 58, 58, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6353, 49, N'عبر الانترنت_lbl', N'LabelControl', N'عبر الانترنت', 206, 620, 8, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 59, 59, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6354, 49, N'OtherPayment_val', N'ValueControle', N'OtherPayment', 74, 620, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 60, 60, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6355, 49, N'Line7', N'LineLabelControl', N'Line7', 2, 641, 8, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 61, 61, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6356, 49, N'Line7', N'LineLabelControl', N'Line7', 1, 351, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Party Section', 61, 61, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6357, 49, N'Tendered_lbl', N'LabelControl', N'Tendered', 1, 645, 10, N'Microsoft Sans Serif', 0, 70, N'Left', N'Footer Section', 62, 62, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6358, 49, N'Item_lbl', N'LabelControl', N'Item', 1, 350, 10, N'Microsoft Sans Serif', 0, 120, N'Left', N'Invoice Details Header ', 62, 62, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6359, 49, N'مقدار وسطي_lbl', N'LabelControl', N'مقدار وسطي', 207, 645, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 63, 63, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6360, 49, N'Qty_lbl', N'LabelControl', N'Qty', 139, 350, 10, N'Microsoft Sans Serif', 0, 40, N'Left', N'Invoice Details Header ', 63, 63, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6361, 49, N'Tendered_val', N'ValueControle', N'Tendered', 73, 645, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 64, 64, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6362, 49, N'Rate_lbl', N'LabelControl', N'Rate', 179, 350, 10, N'Microsoft Sans Serif', 0, 40, N'Left', N'Invoice Details Header ', 64, 64, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6363, 49, N'Changes_lbl', N'LabelControl', N'Changes', 1, 666, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 65, 65, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6364, 49, N'Net_lbl', N'LabelControl', N'Net', 225, 351, 10, N'Microsoft Sans Serif', 0, 60, N'Right', N'Invoice Details Header ', 65, 65, 20, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6365, 49, N'مناقصة_lbl', N'LabelControl', N'مناقصة', 207, 666, 8, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 66, 66, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6366, 49, N' اسم المنتج_lbl', N'LabelControl', N' اسم المنتج', 0, 370, 9, N'Tahoma', 0, 120, N'Left', N'Invoice Details Header ', 66, 66, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6367, 49, N'Changes_val', N'ValueControle', N'Changes', 73, 666, 10, N'Microsoft Sans Serif', 0, 130, N'Center', N'Footer Section', 67, 67, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6368, 49, N'كمية_lbl', N'LabelControl', N'كمية', 140, 370, 10, N'Microsoft Sans Serif', 0, 40, N'Left', N'Invoice Details Header ', 67, 67, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6369, 49, N'السعر_lbl', N'LabelControl', N'السعر', 179, 370, 10, N'Microsoft Sans Serif', 0, 40, N'Right', N'Invoice Details Header ', 68, 68, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6370, 49, N'كمية الشبكة_lbl', N'LabelControl', N'كمية الشبكة', 221, 370, 9, N'Microsoft Sans Serif', 0, 65, N'Right', N'Invoice Details Header ', 69, 69, 20, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6371, 49, N'Line9', N'LineLabelControl', N'Line9', 0, 393, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Invoice Details Header ', 70, 70, 4, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6372, 49, N'    _lbl', N'LabelControl', N'', 2, 185, 10, N'Microsoft Sans Serif', 0, 60, N'Left', N'Invoice Section', 70, 70, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6373, 49, N'Simplified Tax Invoice   فاتورة ضریبیة المبسطة_lbl', N'LabelControl', N'Simplified Tax Invoice   فاتورة ضریبیة المبسطة', 0, 0, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Company Section', 71, 71, 20, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6374, 49, N'Return Invoice Against   إعادة الفاتورة مقابل_lbl', N'LabelControl', N'Return Invoice Against   إعادة الفاتورة مقابل', 1, 256, 9, N'Calibri', 0, 220, N'Left', N'Invoice Section', 71, 71, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6375, 49, N'SalesFormType_val', N'ValueControle', N'SalesFormType', 107, 185, 14, N'Microsoft Sans Serif', 1, 150, N'Right', N'Invoice Section', 71, 71, 30, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6376, 49, N'RefNumber_val', N'ValueControle', N'RefNumber', 203, 256, 10, N'Microsoft Sans Serif', 0, 80, N'Right', N'Invoice Section', 72, 72, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6377, 49, N'InvoiceQRCode_val', N'ValueControle', N'InvoiceQRCode', 78, 689, 10, N'Microsoft Sans Serif', 0, 125, N'Left', N'Footer Section', 72, 72, 125, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6449, 46, N'Date_val', N'ValueControle', N'Date', 461, 234, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Invoice Section', 1, 1, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6450, 46, N'AddressOne_val', N'ValueControle', N'AddressOne', 162, 235, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Party Section', 3, 3, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6451, 46, N'Mobile_val', N'ValueControle', N'Mobile', 163, 259, 10, N'Microsoft Sans Serif', 0, 200, N'Left', N'Party Section', 4, 4, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6452, 46, N'TaxNoOne_val', N'ValueControle', N'TaxNoOne', 164, 283, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Party Section', 5, 5, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6453, 46, N'SL_val', N'ValueControle', N'SL', 47, 367, 10, N'Microsoft Sans Serif', 0, 30, N'Left', N'Details Area', 6, 6, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6454, 46, N'ProductName_val', N'ValueControle', N'ProductName', 92, 368, 10, N'Microsoft Sans Serif', 0, 300, N'Left', N'Details Area', 7, 7, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6455, 46, N'Quantity_val', N'ValueControle', N'Quantity', 432, 368, 10, N'Microsoft Sans Serif', 0, 60, N'Left', N'Details Area', 8, 8, 20, N'')
GO
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6456, 46, N'Rate_val', N'ValueControle', N'Rate', 523, 367, 10, N'Microsoft Sans Serif', 0, 60, N'Left', N'Details Area', 9, 9, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6457, 46, N'DiscountAmount_val', N'ValueControle', N'DiscountAmount', 615, 367, 10, N'Microsoft Sans Serif', 0, 60, N'Left', N'Details Area', 10, 10, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6458, 46, N'NETAmount_val', N'ValueControle', N'NETAmount', 713, 367, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Details Area', 11, 11, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6459, 46, N'Name_val', N'ValueControle', N'Name', 163, 214, 10, N'Microsoft Sans Serif', 0, 123, N'Left', N'Party Section', 16, 16, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6460, 46, N'InvoiceNo_val', N'ValueControle', N'InvoiceNo', 461, 212, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Invoice Section', 17, 17, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6461, 46, N'AccNo_val', N'ValueControle', N'AccNo', 236, 934, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Company Section', 19, 19, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6462, 46, N'VatAmount_val', N'ValueControle', N'VatAmount', 700, 927, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Footer Section', 19, 19, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6463, 46, N'BankName_val', N'ValueControle', N'BankName', 236, 956, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Company Section', 20, 20, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6464, 46, N'GrandTotal_val', N'ValueControle', N'GrandTotal', 702, 950, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Footer Section', 20, 20, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6465, 46, N'IFSC_val', N'ValueControle', N'IFSC', 235, 982, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Company Section', 21, 21, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6466, 46, N'CashPayment_val', N'ValueControle', N'CashPayment', 701, 972, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Footer Section', 21, 21, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6467, 46, N'RunningBalance_val', N'ValueControle', N'RunningBalance', 702, 992, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Footer Section', 21, 21, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6468, 46, N'SalesMan_val', N'ValueControle', N'SalesMan', 446, 285, 10, N'Microsoft Sans Serif', 0, 123, N'Left', N'Invoice Section', 23, 23, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6469, 46, N'NetAmount_val', N'ValueControle', N'NetAmount', 700, 907, 10, N'Microsoft Sans Serif', 0, 330, N'Left', N'Footer Section', 22, 22, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6470, 46, N'GrandTotalToWords_val', N'ValueControle', N'GrandTotalToWords', 463, 1019, 10, N'Microsoft Sans Serif', 0, 300, N'Left', N'Footer Section', 21, 21, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6471, 46, N'GrandTotalToArabicWords_val', N'ValueControle', N'GrandTotalToArabicWords', 463, 1041, 10, N'Microsoft Sans Serif', 0, 100, N'Left', N'Footer Section', 22, 22, 20, N'01')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6472, 46, N'InvoiceQRCode_val', N'ValueControle', N'InvoiceQRCode', 687, 158, 10, N'Microsoft Sans Serif', 0, 123, N'Left', N'Invoice Section', 23, 23, 123, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6473, 50, N'CompanyName_val', N'ValueControle', N'CompanyName', 1, 1, 12, N'Calibri', 1, 290, N'Center', N'Company Section', 0, 0, 23, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6474, 50, N'LocalName_val', N'ValueControle', N'LocalName', 1, 22, 12, N'Calibri', 1, 290, N'Center', N'Company Section', 1, 1, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6475, 50, N'Place_val', N'ValueControle', N'Place', 1, 41, 10, N'Open Sans Semibold', 1, 290, N'Center', N'Company Section', 2, 2, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6476, 50, N'Address_val', N'ValueControle', N'Address', 0, 60, 10, N'Open Sans Semibold', 0, 290, N'Center', N'Company Section', 3, 3, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6477, 50, N'PhoneNo_val', N'ValueControle', N'PhoneNo', 1, 81, 10, N'Open Sans Semibold', 0, 290, N'Center', N'Company Section', 5, 5, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6478, 50, N'TaxId_val', N'ValueControle', N'TaxId', 1, 102, 10, N'Open Sans Semibold', 1, 290, N'Center', N'Company Section', 7, 7, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6479, 50, N'Line1', N'LineLabelControl', N'Line1', 1, 121, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Company Section', 8, 8, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6480, 50, N'InvoiceQRCode_val', N'ValueControle', N'InvoiceQRCode', 91, 166, 8, N'Microsoft Sans Serif', 1, 123, N'Left', N'Invoice Section', 9, 9, 123, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6481, 50, N'رقم الفاتورة_lbl', N'LabelControl', N'رقم الفاتورة', 205, 276, 8, N'Microsoft Sans Serif', 0, 80, N'Right', N'Invoice Section', 10, 10, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6482, 50, N'Bill No_lbl', N'LabelControl', N'Bill No', 2, 276, 8, N'Open Sans Semibold', 0, 100, N'Left', N'Invoice Section', 11, 11, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6483, 50, N'InvoiceNo_val', N'ValueControle', N'InvoiceNo', 58, 276, 10, N'Open Sans Semibold', 0, 180, N'Center', N'Invoice Section', 12, 12, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6484, 50, N'Date_lbl', N'LabelControl', N'Date', 2, 299, 9, N'Open Sans Semibold', 0, 100, N'Left', N'Invoice Section', 13, 13, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6485, 50, N'تاريخ_lbl', N'LabelControl', N'تاريخ', 205, 299, 9, N'Open Sans Semibold', 0, 80, N'Right', N'Invoice Section', 14, 14, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6486, 50, N'Date_val', N'ValueControle', N'Date', 57, 299, 10, N'Open Sans Semibold', 0, 180, N'Center', N'Invoice Section', 15, 15, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6487, 50, N'Line2', N'LineLabelControl', N'Line2', 2, 321, 8, N'Microsoft Sans Serif', 0, 290, N'Left', N'Invoice Section', 16, 16, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6488, 50, N'Name_lbl', N'LabelControl', N'Name', 2, 328, 10, N'Open Sans Semibold', 0, 50, N'Left', N'Party Section', 17, 17, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6489, 50, N'عميل_lbl', N'LabelControl', N'عميل', 245, 328, 8, N'Open Sans Semibold', 0, 40, N'Right', N'Party Section', 18, 18, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6490, 50, N'Name_val', N'ValueControle', N'Name', 54, 328, 10, N'Open Sans Semibold', 0, 200, N'Center', N'Party Section', 19, 20, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6491, 50, N'ProductName_val', N'ValueControle', N'ProductName', 1, 408, 10, N'Open Sans Semibold', 0, 140, N'Left', N'Details Area', 30, 30, 17, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6492, 50, N'Quantity_val', N'ValueControle', N'Quantity', 141, 408, 10, N'Open Sans Semibold', 0, 40, N'Right', N'Details Area', 31, 31, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6493, 50, N'Rate_val', N'ValueControle', N'Rate', 181, 408, 8, N'Open Sans Semibold', 0, 40, N'Right', N'Details Area', 32, 32, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6494, 50, N'NETAmount_val', N'ValueControle', N'NETAmount', 223, 408, 10, N'Open Sans Semibold', 0, 63, N'Right', N'Details Area', 33, 33, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6495, 50, N'LocalName_val', N'ValueControle', N'LocalName', 2, 428, 10, N'Open Sans Semibold', 0, 240, N'Left', N'Details Area', 34, 34, 17, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6496, 50, N'Line5', N'LineLabelControl', N'Line5', 1, 449, 8, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 35, 35, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6497, 50, N'Total Qty_lbl', N'LabelControl', N'Total Qty', 1, 457, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 36, 36, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6498, 50, N'الكمية الإجمالية_lbl', N'LabelControl', N'الكمية الإجمالية', 205, 457, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 37, 37, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6499, 50, N'TotalQuantity_val', N'ValueControle', N'TotalQuantity', 72, 457, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 38, 38, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6500, 50, N'Sub Total_lbl', N'LabelControl', N'Sub Total', 1, 479, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 39, 39, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6501, 50, N'المجموع الفرعي_lbl', N'LabelControl', N'المجموع الفرعي', 205, 479, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 40, 40, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6502, 50, N'Taxable_val', N'ValueControle', N'Taxable', 72, 479, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 41, 41, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6503, 50, N'VAT_lbl', N'LabelControl', N'VAT 15%', 1, 501, 8, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 42, 42, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6504, 50, N'ضريبة_lbl', N'LabelControl', N'ضريبة 15 %', 205, 501, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 43, 43, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6505, 50, N'VatAmount_val', N'ValueControle', N'VatAmount', 72, 501, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 44, 44, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6506, 50, N'Discount_lbl', N'LabelControl', N'Discount', 1, 523, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 45, 45, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6507, 50, N'خصم_lbl', N'LabelControl', N'خصم', 205, 523, 8, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 46, 46, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6508, 50, N'BillDiscount_val', N'ValueControle', N'BillDiscount', 71, 523, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 47, 47, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6509, 50, N'TOTAL_lbl', N'LabelControl', N'TOTAL', 1, 545, 12, N'Open Sans Semibold', 1, 70, N'Left', N'Footer Section', 48, 48, 24, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6510, 50, N'مجموع_lbl', N'LabelControl', N'مجموع', 205, 545, 13, N'Open Sans Semibold', 1, 80, N'Right', N'Footer Section', 49, 49, 24, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6511, 50, N'GrandTotal_val', N'ValueControle', N'GrandTotal', 72, 545, 12, N'Open Sans Semibold', 1, 130, N'Center', N'Footer Section', 50, 50, 24, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6512, 50, N'Line6', N'LineLabelControl', N'Line6', 1, 570, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 51, 51, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6513, 50, N'Cash_lbl', N'LabelControl', N'Cash', 1, 574, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 52, 52, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6514, 50, N'السيولة النقدية_lbl', N'LabelControl', N'السيولة النقدية', 205, 574, 8, N'Open Sans Semibold', 0, 80, N'Left', N'Footer Section', 53, 53, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6515, 50, N'CashPayment_val', N'ValueControle', N'CashPayment', 73, 574, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 54, 54, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6516, 50, N'Card_lbl', N'LabelControl', N'Card', 1, 596, 10, N'Microsoft Sans Serif', 0, 70, N'Left', N'Footer Section', 55, 55, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6517, 50, N'بطاقة_lbl', N'LabelControl', N'بطاقة', 205, 596, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 56, 56, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6518, 50, N'CardPayment_val', N'ValueControle', N'CardPayment', 74, 596, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 57, 57, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6519, 50, N'Online_lbl', N'LabelControl', N'Online', 0, 620, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 58, 58, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6520, 50, N'عبر الانترنت_lbl', N'LabelControl', N'عبر الانترنت', 206, 620, 8, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 59, 59, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6521, 50, N'OtherPayment_val', N'ValueControle', N'OtherPayment', 74, 620, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 60, 60, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6522, 50, N'Line7', N'LineLabelControl', N'Line7', 2, 641, 8, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 61, 61, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6523, 50, N'Line7', N'LineLabelControl', N'Line7', 1, 351, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Party Section', 61, 61, 4, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6524, 50, N'Tendered_lbl', N'LabelControl', N'Tendered', 1, 645, 10, N'Microsoft Sans Serif', 0, 70, N'Left', N'Footer Section', 62, 62, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6525, 50, N'Item_lbl', N'LabelControl', N'Item', 1, 350, 10, N'Microsoft Sans Serif', 0, 120, N'Left', N'Invoice Details Header ', 62, 62, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6526, 50, N'مقدار وسطي_lbl', N'LabelControl', N'مقدار وسطي', 207, 645, 10, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 63, 63, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6527, 50, N'Qty_lbl', N'LabelControl', N'Qty', 139, 350, 10, N'Microsoft Sans Serif', 0, 40, N'Left', N'Invoice Details Header ', 63, 63, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6528, 50, N'Tendered_val', N'ValueControle', N'Tendered', 73, 645, 10, N'Open Sans Semibold', 0, 130, N'Center', N'Footer Section', 64, 64, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6529, 50, N'Rate_lbl', N'LabelControl', N'Rate', 179, 350, 10, N'Microsoft Sans Serif', 0, 40, N'Left', N'Invoice Details Header ', 64, 64, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6530, 50, N'Changes_lbl', N'LabelControl', N'Changes', 1, 666, 10, N'Open Sans Semibold', 0, 70, N'Left', N'Footer Section', 65, 65, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6531, 50, N'Net_lbl', N'LabelControl', N'Net', 225, 351, 10, N'Microsoft Sans Serif', 0, 60, N'Right', N'Invoice Details Header ', 65, 65, 20, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6532, 50, N'مناقصة_lbl', N'LabelControl', N'مناقصة', 207, 666, 8, N'Open Sans Semibold', 0, 80, N'Right', N'Footer Section', 66, 66, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6533, 50, N' اسم المنتج_lbl', N'LabelControl', N' اسم المنتج', 0, 370, 9, N'Tahoma', 0, 120, N'Left', N'Invoice Details Header ', 66, 66, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6534, 50, N'Changes_val', N'ValueControle', N'Changes', 73, 666, 10, N'Microsoft Sans Serif', 0, 130, N'Center', N'Footer Section', 67, 67, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6535, 50, N'كمية_lbl', N'LabelControl', N'كمية', 140, 370, 10, N'Microsoft Sans Serif', 0, 40, N'Left', N'Invoice Details Header ', 67, 67, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6536, 50, N'Thank you for visiting_lbl', N'LabelControl', N'Thank you for visiting', 1, 688, 10, N'Microsoft Sans Serif', 0, 290, N'Center', N'Footer Section', 68, 68, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6537, 50, N'السعر_lbl', N'LabelControl', N'السعر', 179, 370, 10, N'Microsoft Sans Serif', 0, 40, N'Right', N'Invoice Details Header ', 68, 68, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6538, 50, N'Line8', N'LineLabelControl', N'Line8', 1, 730, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Footer Section', 69, 69, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6539, 50, N'كمية الشبكة_lbl', N'LabelControl', N'كمية الشبكة', 221, 370, 9, N'Microsoft Sans Serif', 0, 65, N'Right', N'Invoice Details Header ', 69, 69, 20, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6540, 50, N'Line9', N'LineLabelControl', N'Line9', 0, 393, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Invoice Details Header ', 70, 70, 4, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6541, 50, N'Simplified Tax Invoice   فاتورة ضریبیة المبسطة_lbl', N'LabelControl', N'Simplified Tax Invoice   فاتورة ضریبیة المبسطة', 0, 0, 10, N'Microsoft Sans Serif', 0, 290, N'Left', N'Company Section', 71, 71, 20, N'M')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6542, 50, N'Return Invoice Against   إعادة الفاتورة مقابل_lbl', N'LabelControl', N'Return Invoice Against   إعادة الفاتورة مقابل', 1, 256, 9, N'Calibri', 0, 220, N'Left', N'Invoice Section', 71, 71, 20, N'')
INSERT [dbo].[PrintDesignSettings] ([PrintDesignSettingsID], [printMasterId], [ControleName], [Type], [FeildText], [X], [Y], [FontSize], [Font], [isBold], [width], [Alaingmnet], [Position], [TabIndex], [iIndex], [Height], [Spec]) VALUES (6543, 50, N'RefNumber_val', N'ValueControle', N'RefNumber', 203, 256, 10, N'Microsoft Sans Serif', 0, 80, N'Right', N'Invoice Section', 72, 72, 20, N'')
SET IDENTITY_INSERT [dbo].[PrintDesignSettings] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [ProductCode], [ProductName], [Specification], [PGroupId], [UnitId], [BrandId], [TCategoryId], [HSNNo], [PurchasePrice], [SellingPrice], [MRP], [OpeningStock], [OppStockDate], [ReOrderStock], [AccountId], [GodwnId], [SelfId], [PurchaseDis], [SaleDis], [AliasCode], [AliasName], [Remarks], [LocalName], [PurchaseUnit], [SalesUnit], [ReportUnit], [image], [filePath], [description], [ProductType], [isActive], [isInclusive], [Barcode], [IsExpairing], [ExpairingDays], [MinimumMargin], [NormalMargin], [RetailMargin], [WholesaleMargin], [MRPMargin], [IsMarginBasedPercentage], [LoyalityPont], [AgentCommision], [SalesmanCommision], [IsCommisionBasedPercentage], [FieldOne], [FieldTwo], [FieldThree], [FieldFour], [FieldFive], [IsScale], [ListOrder], [IsAllowMultiple], [isRateBasedWeight], [PriorityStatus], [CookingTime], [IsFavorite], [RestaurantItemType]) VALUES (21, 1, N'Mobile', N'', 1, 1, 1, 2, N'1', CAST(50.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(N'2022-03-11T00:00:00.000' AS DateTime), CAST(5.0000 AS Decimal(24, 4)), 7, 1, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'1', N'Mobile', N'', N'', 1, 1, 1, NULL, NULL, N'', N'Finished Good', 1, 1, N'123', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, CAST(0.0000 AS Decimal(24, 4)), 1, N'Unchecked', N'1', NULL, NULL, NULL, 0, 1, 0, 0, N'Low', NULL, 0, N'Normal')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductBarcode] ON 

INSERT [dbo].[ProductBarcode] ([Id], [Barcode], [BarcodeType], [ExpairyDate], [ManufDate], [FK_ProductId], [FK_UnitId], [FK_BatchId], [FK_BranchId], [Remarks], [PurchasePrice], [SellingPrice], [MRP], [ProductCost], [FieldOne], [FieldTwo], [FieldThree], [FieldFour], [MinimuPrice], [RetailPrice], [WholesalePrice], [ProductBarcodeName], [LocalName], [image]) VALUES (24, N'123', N'Base Barcode', NULL, NULL, 21, 1, -1, 1, NULL, CAST(50.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(50.0000 AS Decimal(24, 4)), NULL, NULL, NULL, NULL, CAST(60.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), N'Mobile', N'', NULL)
SET IDENTITY_INSERT [dbo].[ProductBarcode] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductGroup] ON 

INSERT [dbo].[ProductGroup] ([PGroupId], [PGroupName], [Under], [SalesInsentive], [SalesDis], [PurchDis], [Remarks], [ListOrder], [isShowInGrid]) VALUES (1, N'GENERAL', N'', CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'', NULL, 1)
INSERT [dbo].[ProductGroup] ([PGroupId], [PGroupName], [Under], [SalesInsentive], [SalesDis], [PurchDis], [Remarks], [ListOrder], [isShowInGrid]) VALUES (2, N'WATER', N'GENERAL', CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'', NULL, 1)
INSERT [dbo].[ProductGroup] ([PGroupId], [PGroupName], [Under], [SalesInsentive], [SalesDis], [PurchDis], [Remarks], [ListOrder], [isShowInGrid]) VALUES (3, N'مشروبات', N'GENERAL', CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'', 1, 1)
INSERT [dbo].[ProductGroup] ([PGroupId], [PGroupName], [Under], [SalesInsentive], [SalesDis], [PurchDis], [Remarks], [ListOrder], [isShowInGrid]) VALUES (4, N'GROCERRY', N'GENERAL', CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'', NULL, 1)
SET IDENTITY_INSERT [dbo].[ProductGroup] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductPriceCategory] ON 

INSERT [dbo].[ProductPriceCategory] ([ProPrceCatId], [ProductId], [PCategoryId], [UnitId], [PGroupId], [SPrice], [SPDisc], [PPrice], [PDisc], [Barcode]) VALUES (21, 21, 1, 1, 1, CAST(60.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(50.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'123')
SET IDENTITY_INSERT [dbo].[ProductPriceCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductUnit] ON 

INSERT [dbo].[ProductUnit] ([ProUnitId], [ProductId], [UnitId], [UnitType], [Barcode], [UnitQty], [ProductUnitName], [IsInclusive], [OpeningStock], [SalesPrice]) VALUES (26, 21, 1, N'Base Unit', N'123', CAST(1.0000 AS Decimal(24, 4)), N'Mobile', 1, CAST(0.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)))
SET IDENTITY_INSERT [dbo].[ProductUnit] OFF
GO
SET IDENTITY_INSERT [dbo].[PurInvMaster] ON 

INSERT [dbo].[PurInvMaster] ([PurInvId], [PurInvNo], [InvType], [PurDate], [PurActId], [PartyAccId], [PriceCategoryId], [AdditionalAmnt], [Frieght], [TotalDiscount], [BillDisc], [TotalTax], [TotalAmnt], [JEId], [EntryDate], [Remarks], [IGSTAmount], [SGSTAmount], [CGSTAmount], [RefInvNo], [Taxable], [FK_BranchId], [CashPayment], [CardPayment], [Credit], [FreeAmount], [ReturnAmount], [IsClosed], [FielOne], [FieldTwo], [FieldThree], [FieldFour], [CashAccountId], [CardAccountId], [OnlineAccountId], [IsSettiled], [TotalPayment], [VoucherNumber], [SequenceNo], [Terminal]) VALUES (8, 1, N'B2C', CAST(N'2022-03-11T00:00:00.000' AS DateTime), 5, 1, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(1000.0000 AS Decimal(24, 4)), 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'', CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'MOb123', CAST(1000.0000 AS Decimal(24, 4)), 1, CAST(1000.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(1000.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(0.00000000 AS Decimal(24, 8)), NULL, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[PurInvMaster] ([PurInvId], [PurInvNo], [InvType], [PurDate], [PurActId], [PartyAccId], [PriceCategoryId], [AdditionalAmnt], [Frieght], [TotalDiscount], [BillDisc], [TotalTax], [TotalAmnt], [JEId], [EntryDate], [Remarks], [IGSTAmount], [SGSTAmount], [CGSTAmount], [RefInvNo], [Taxable], [FK_BranchId], [CashPayment], [CardPayment], [Credit], [FreeAmount], [ReturnAmount], [IsClosed], [FielOne], [FieldTwo], [FieldThree], [FieldFour], [CashAccountId], [CardAccountId], [OnlineAccountId], [IsSettiled], [TotalPayment], [VoucherNumber], [SequenceNo], [Terminal]) VALUES (9, 1, N'B2C', CAST(N'2022-03-17T00:00:00.000' AS DateTime), 5, 1, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(50.0000 AS Decimal(24, 4)), 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'', CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'NewPUrchase', CAST(50.0000 AS Decimal(24, 4)), 6, CAST(50.0000 AS Decimal(24, 4)), CAST(50.0000 AS Decimal(24, 4)), CAST(-50.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(0.00000000 AS Decimal(24, 8)), NULL, 1, N'DESKTOP-WAATECH')
SET IDENTITY_INSERT [dbo].[PurInvMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[PurInvSettings] ON 

INSERT [dbo].[PurInvSettings] ([PurSettingsId], [PurInvId], [PartyAccId], [ProId], [ProQty], [UnitId], [ProRate], [Disc], [DiscAmnt], [Gross], [NetAmnt], [Date], [HSN], [GST], [GSTAMOUNT], [SGST], [SGSTAmount], [CGST], [CGSTAmount], [IGST], [IGSTAMOUNT], [FK_BatchId], [Barcode]) VALUES (11, 8, 1, 21, CAST(20.0000 AS Decimal(24, 4)), 1, CAST(50.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(1000.0000 AS Decimal(24, 4)), CAST(1000.0000 AS Decimal(24, 4)), CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'1', CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), NULL, N'123')
INSERT [dbo].[PurInvSettings] ([PurSettingsId], [PurInvId], [PartyAccId], [ProId], [ProQty], [UnitId], [ProRate], [Disc], [DiscAmnt], [Gross], [NetAmnt], [Date], [HSN], [GST], [GSTAMOUNT], [SGST], [SGSTAmount], [CGST], [CGSTAmount], [IGST], [IGSTAMOUNT], [FK_BatchId], [Barcode]) VALUES (12, 9, 1, 21, CAST(1.0000 AS Decimal(24, 4)), 1, CAST(50.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(50.0000 AS Decimal(24, 4)), CAST(50.0000 AS Decimal(24, 4)), CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'10', CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), NULL, N'123')
SET IDENTITY_INSERT [dbo].[PurInvSettings] OFF
GO
SET IDENTITY_INSERT [dbo].[RateFormat] ON 

INSERT [dbo].[RateFormat] ([Id], [Name], [Value], [Alternative]) VALUES (1, 1, N'A', NULL)
INSERT [dbo].[RateFormat] ([Id], [Name], [Value], [Alternative]) VALUES (2, 2, N'B', NULL)
INSERT [dbo].[RateFormat] ([Id], [Name], [Value], [Alternative]) VALUES (3, 3, N'C', NULL)
INSERT [dbo].[RateFormat] ([Id], [Name], [Value], [Alternative]) VALUES (4, 4, N'D', NULL)
INSERT [dbo].[RateFormat] ([Id], [Name], [Value], [Alternative]) VALUES (5, 5, N'E', NULL)
INSERT [dbo].[RateFormat] ([Id], [Name], [Value], [Alternative]) VALUES (6, 6, N'F', NULL)
INSERT [dbo].[RateFormat] ([Id], [Name], [Value], [Alternative]) VALUES (7, 7, N'G', NULL)
INSERT [dbo].[RateFormat] ([Id], [Name], [Value], [Alternative]) VALUES (8, 8, N'H', NULL)
INSERT [dbo].[RateFormat] ([Id], [Name], [Value], [Alternative]) VALUES (9, 9, N'I', NULL)
INSERT [dbo].[RateFormat] ([Id], [Name], [Value], [Alternative]) VALUES (10, 0, N'J', NULL)
SET IDENTITY_INSERT [dbo].[RateFormat] OFF
GO
SET IDENTITY_INSERT [dbo].[Root] ON 

INSERT [dbo].[Root] ([RootId], [RootName], [WeekDay], [SalesManId], [DriverId], [VehicleId], [FK_BranchId]) VALUES (1, N'ALL', N'MONDAY', 1, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Root] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesEstimateMaster] ON 

INSERT [dbo].[SalesEstimateMaster] ([SalesInvId], [SalesInvNo], [InvType], [SalesDate], [SalesAccountId], [PartyAccountId], [RouteId], [VehId], [StafId], [PriceCategoryId], [AditionalAmount], [Frieght], [TotalDiscount], [BillDiscount], [GrandDiscount], [JEId], [EntryDate], [Remarks], [IGSTAmount], [SGSTAmount], [CGSTAmount], [TaxableAmount], [FYearID], [FloodCess], [TotalTaxAmount], [FK_BranchId], [CntrInvNo], [SequenceNo], [CashPayment], [CardPayment], [Credit], [FielOne], [FieldTwo], [FieldThree], [FieldFour], [Terminal], [IsClosed], [FreeAmount], [ReturnAmount], [OtherPayment], [CashAccountId], [CardAccountId], [OnlineAccountId], [IsSettiled], [TotalPayment], [VoucherNumber]) VALUES (1032, 1, N'B2C', CAST(N'2022-03-11T00:00:00.000' AS DateTime), 3, 1, 1, 1, 1, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(1080.0000 AS Decimal(24, 4)), 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'', CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(1080.0000 AS Decimal(24, 4)), 1, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), 1, N'CH1', 1, CAST(1080.00000000 AS Decimal(24, 8)), CAST(0.00000000 AS Decimal(24, 8)), CAST(0.00000000 AS Decimal(24, 8)), N'', NULL, NULL, NULL, N'DESKTOP-WAATECH', 0, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), NULL, NULL, NULL, 1, CAST(0.00000000 AS Decimal(24, 8)), NULL)
INSERT [dbo].[SalesEstimateMaster] ([SalesInvId], [SalesInvNo], [InvType], [SalesDate], [SalesAccountId], [PartyAccountId], [RouteId], [VehId], [StafId], [PriceCategoryId], [AditionalAmount], [Frieght], [TotalDiscount], [BillDiscount], [GrandDiscount], [JEId], [EntryDate], [Remarks], [IGSTAmount], [SGSTAmount], [CGSTAmount], [TaxableAmount], [FYearID], [FloodCess], [TotalTaxAmount], [FK_BranchId], [CntrInvNo], [SequenceNo], [CashPayment], [CardPayment], [Credit], [FielOne], [FieldTwo], [FieldThree], [FieldFour], [Terminal], [IsClosed], [FreeAmount], [ReturnAmount], [OtherPayment], [CashAccountId], [CardAccountId], [OnlineAccountId], [IsSettiled], [TotalPayment], [VoucherNumber]) VALUES (1033, 2, N'B2C', CAST(N'2022-03-16T17:43:08.000' AS DateTime), 3, 1, 1, NULL, 1, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), 1, CAST(N'2022-03-16T00:00:00.000' AS DateTime), N'', CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), 1, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), 1, N'CH2', 1, CAST(60.00000000 AS Decimal(24, 8)), CAST(0.00000000 AS Decimal(24, 8)), CAST(0.00000000 AS Decimal(24, 8)), N'', NULL, NULL, NULL, N'DESKTOP-WAATECH', 0, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), 1, 2, 2, 1, CAST(0.00000000 AS Decimal(24, 8)), NULL)
INSERT [dbo].[SalesEstimateMaster] ([SalesInvId], [SalesInvNo], [InvType], [SalesDate], [SalesAccountId], [PartyAccountId], [RouteId], [VehId], [StafId], [PriceCategoryId], [AditionalAmount], [Frieght], [TotalDiscount], [BillDiscount], [GrandDiscount], [JEId], [EntryDate], [Remarks], [IGSTAmount], [SGSTAmount], [CGSTAmount], [TaxableAmount], [FYearID], [FloodCess], [TotalTaxAmount], [FK_BranchId], [CntrInvNo], [SequenceNo], [CashPayment], [CardPayment], [Credit], [FielOne], [FieldTwo], [FieldThree], [FieldFour], [Terminal], [IsClosed], [FreeAmount], [ReturnAmount], [OtherPayment], [CashAccountId], [CardAccountId], [OnlineAccountId], [IsSettiled], [TotalPayment], [VoucherNumber]) VALUES (1034, 1, N'B2C', CAST(N'2022-03-17T17:49:01.000' AS DateTime), 3, 1, NULL, NULL, 1, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), NULL, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), 1, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), 6, N'CH1', 1, CAST(60.00000000 AS Decimal(24, 8)), CAST(0.00000000 AS Decimal(24, 8)), CAST(0.00000000 AS Decimal(24, 8)), N'', NULL, NULL, NULL, N'DESKTOP-WAATECH', 0, CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), 1, 2, 2, 1, CAST(0.00000000 AS Decimal(24, 8)), NULL)
SET IDENTITY_INSERT [dbo].[SalesEstimateMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesEstimateSettings] ON 

INSERT [dbo].[SalesEstimateSettings] ([SalesSettingsId], [SalesInvId], [PartyAccountId], [ProductId], [ProQty], [UnitId], [ProRate], [ProDiscont], [DiscAmnt], [GrossAmnt], [NetAmnt], [Date], [HSN], [GST], [GSTAMOUNT], [SGST], [SGSTAmount], [CGST], [CGSTAmount], [IGST], [IGSTAMOUNT], [FK_BatchId], [Barcode], [ItemCalory], [EmployeeID], [EmployeeCommision], [AgentId], [AgentCommission], [ProductName], [ItemSerialNumber], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (1078, 1032, 1, 21, CAST(18.0000 AS Decimal(18, 4)), 1, CAST(60.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(1080.0000 AS Decimal(18, 4)), CAST(1080.0000 AS Decimal(18, 4)), CAST(N'2022-03-11T00:00:00.000' AS DateTime), NULL, CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), 1, N'123', CAST(0.0000 AS Decimal(18, 4)), NULL, CAST(0.0000 AS Decimal(18, 4)), NULL, CAST(0.0000 AS Decimal(18, 4)), N'Mobile', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SalesEstimateSettings] ([SalesSettingsId], [SalesInvId], [PartyAccountId], [ProductId], [ProQty], [UnitId], [ProRate], [ProDiscont], [DiscAmnt], [GrossAmnt], [NetAmnt], [Date], [HSN], [GST], [GSTAMOUNT], [SGST], [SGSTAmount], [CGST], [CGSTAmount], [IGST], [IGSTAMOUNT], [FK_BatchId], [Barcode], [ItemCalory], [EmployeeID], [EmployeeCommision], [AgentId], [AgentCommission], [ProductName], [ItemSerialNumber], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (1079, 1033, 1, 21, CAST(1.0000 AS Decimal(18, 4)), 1, CAST(60.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(60.0000 AS Decimal(18, 4)), CAST(60.0000 AS Decimal(18, 4)), CAST(N'2022-03-16T00:00:00.000' AS DateTime), N'1', CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), 1, N'123', CAST(1.0000 AS Decimal(18, 4)), NULL, CAST(0.0000 AS Decimal(18, 4)), NULL, CAST(0.0000 AS Decimal(18, 4)), N'Mobile', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SalesEstimateSettings] ([SalesSettingsId], [SalesInvId], [PartyAccountId], [ProductId], [ProQty], [UnitId], [ProRate], [ProDiscont], [DiscAmnt], [GrossAmnt], [NetAmnt], [Date], [HSN], [GST], [GSTAMOUNT], [SGST], [SGSTAmount], [CGST], [CGSTAmount], [IGST], [IGSTAMOUNT], [FK_BatchId], [Barcode], [ItemCalory], [EmployeeID], [EmployeeCommision], [AgentId], [AgentCommission], [ProductName], [ItemSerialNumber], [FieldOne], [FieldTwo], [FieldThree], [FieldFour]) VALUES (1080, 1034, 1, 21, CAST(1.0000 AS Decimal(18, 4)), 1, CAST(60.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(60.0000 AS Decimal(18, 4)), CAST(60.0000 AS Decimal(18, 4)), CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'1', CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), 1, N'123', CAST(1.0000 AS Decimal(18, 4)), NULL, CAST(0.0000 AS Decimal(18, 4)), NULL, CAST(0.0000 AS Decimal(18, 4)), N'Mobile', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SalesEstimateSettings] OFF
GO
SET IDENTITY_INSERT [dbo].[SelectedFYear] ON 

INSERT [dbo].[SelectedFYear] ([SlectedFY], [FyearId]) VALUES (1, 1)
SET IDENTITY_INSERT [dbo].[SelectedFYear] OFF
GO
SET IDENTITY_INSERT [dbo].[Self] ON 

INSERT [dbo].[Self] ([SelfId], [GodownId], [SelfName]) VALUES (1, 1, N'PRIMARY - 1')
INSERT [dbo].[Self] ([SelfId], [GodownId], [SelfName]) VALUES (2, 1, N'SECPO')
INSERT [dbo].[Self] ([SelfId], [GodownId], [SelfName]) VALUES (3, 1, N'THIRD')
SET IDENTITY_INSERT [dbo].[Self] OFF
GO
SET IDENTITY_INSERT [dbo].[Settings] ON 

INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (1, N'blnTaxEnabled', N'Enable Tax', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (2, N'blnGSTEnabled', N'Enable GST', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (3, N'blnCessEnabled', N'Enable Cess', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (4, N'blnSalesEstimateEnable', N'Enable Sales Estimate', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (5, N'blnAutoBackUpWhileClosing', N'Auto Backup While Closing', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (6, N'blnShowPOS', N'Show POS', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (7, N'blnShowTransWitInvtry', N'Show Transaction Without Inventory', N'General', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (8, N'blnShowFtPurchase', N'Show First Purchase', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (9, N'blnEnableLogOut', N'Enable Log Off', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (10, N'blnEnableAppLock', N'Enable Application Lock', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (11, N'blnPosBarcodeBased', N'POS Barcode Based', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (12, N'blnInvoicePrintingByRDLC', N'Invoice Printing By RDLC', N'General', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (13, N'strDefaultCompany', N'Default Company', N'General', 0, N'Default')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (14, N'strQuantityFormat', N'Quantity Format', N'General', 0, N'N2')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (15, N'strCurencyFormat', N'Curency Format', N'General', 0, N'N2')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (16, N'strDateFormat', N'Date Format', N'General', 0, N'dd-MMM-yyyy')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (17, N'strDefaultSalesType', N'Default Sales Type', N'General', 0, N'B2C')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (18, N'strEmailProvider', N'Email Provider', N'General', 0, N'Email Provider')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (19, N'strClientName', N'Client Name', N'General', 0, N'Client Name')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (20, N'strClientPort', N'Client Port', N'General', 0, N'Client Port')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (21, N'strEmailAddress', N'Email Address', N'General', 0, N'example@gmail.com')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (22, N'strUserName', N'User Name', N'General', 0, N'example@gmail.com')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (23, N'strPassword', N'Password', N'General', 0, N'Password')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (24, N'strDefaultPurchaseType', N'Default Purchase Type', N'General', 0, N'B2B')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (25, N'strRatebasedBarcode', N'Rate Based Barcode', N'General', 0, N'0')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (26, N'strWeightbasedBarcode', N'Weight Based Barcode', N'General', 0, N'0')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (27, N'blnEditInvoiceNumber', N'Edit Invoice Number', N'Sales Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (28, N'blnPntInvWlSaving', N'Print Invoice While Saving', N'Sales Invoice', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (29, N'blnSndMlWlSaving', N'Send Mail While Saving', N'Sales Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (30, N'blnCnfmtnWlEditing', N'Ask Confirmation While Editing', N'Sales Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (31, N'blnCnfmtnWlDeleting', N'Ask Confirmation While Deleting', N'Sales Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (32, N'blnPwdWlDeleting', N'Ask Password While Deleting', N'Sales Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (33, N'blnShwMsgWlDplctItm', N'Show Message While Entering Duplicate Item', N'Sales Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (34, N'blnPrvntDplctItm', N'Prevent Entering Duplicate Item', N'Sales Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (35, N'blnEditInvoiceNumberSE', N'Edit Invoice Number', N'Sales Estimate', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (36, N'blnPntInvWlSavingSE', N'Print Invoice While Saving', N'Sales Estimate', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (37, N'blnSndMlWlSavingSE', N'Send Mail While Saving', N'Sales Estimate', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (38, N'blnCnfmtnWlEditingSE', N'Ask Confirmation While Editing', N'Sales Estimate', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (39, N'blnCnfmtnWlDeletingSE', N'Ask Confirmation While Deleting', N'Sales Estimate', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (40, N'blnPwdWlDeletingSE', N'Ask Password While Deleting', N'Sales Estimate', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (41, N'blnShwMsgWlDplctItmSE', N'Show Message While Entering Duplicate Item', N'Sales Estimate', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (42, N'blnPrvntDplctItmSE', N'Prevent Entering Duplicate Item', N'Sales Estimate', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (43, N'blnEditInvoiceNumberSO', N'Edit Invoice Number', N'Sales Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (44, N'blnPntInvWlSavingSO', N'Print Invoice While Saving', N'Sales Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (45, N'blnSndMlWlSavingSO', N'Send Mail While Saving', N'Sales Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (46, N'blnCnfmtnWlEditingSO', N'Ask Confirmation While Editing', N'Sales Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (47, N'blnCnfmtnWlDeletingSO', N'Ask Confirmation While Deleting', N'Sales Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (48, N'blnPwdWlDeletingSO', N'Ask Password While Deleting', N'Sales Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (49, N'blnShwMsgWlDplctItmSO', N'Show Message While Entering Duplicate Item', N'Sales Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (50, N'blnPrvntDplctItmSO', N'Prevent Entering Duplicate Item', N'Sales Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (51, N'blnEditInvoiceNumberSQ', N'Edit Invoice Number', N'Sales Quotation', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (52, N'blnPntInvWlSavingSQ', N'Print Invoice While Saving', N'Sales Quotation', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (53, N'blnSndMlWlSavingSQ', N'Send Mail While Saving', N'Sales Quotation', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (54, N'blnCnfmtnWlEditingSQ', N'Ask Confirmation While Editing', N'Sales Quotation', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (55, N'blnCnfmtnWlDeletingSQ', N'Ask Confirmation While Deleting', N'Sales Quotation', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (56, N'blnPwdWlDeletingSQ', N'Ask Password While Deleting', N'Sales Quotation', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (57, N'blnShwMsgWlDplctItmSQ', N'Show Message While Entering Duplicate Item', N'Sales Quotation', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (58, N'blnPrvntDplctItmSQ', N'Prevent Entering Duplicate Item', N'Sales Quotation', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (59, N'blnEditInvoiceNumberSR', N'Edit Invoice Number', N'Sales Return', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (60, N'blnPntInvWlSavingSR', N'Print Invoice While Saving', N'Sales Return', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (61, N'blnSndMlWlSavingSR', N'Send Mail While Saving', N'Sales Return', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (62, N'blnCnfmtnWlEditingSR', N'Ask Confirmation While Editing', N'Sales Return', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (63, N'blnCnfmtnWlDeletingSR', N'Ask Confirmation While Deleting', N'Sales Return', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (64, N'blnPwdWlDeletingSR', N'Ask Password While Deleting', N'Sales Return', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (65, N'blnShwMsgWlDplctItmSR', N'Show Message While Entering Duplicate Item', N'Sales Return', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (66, N'blnPrvntDplctItmSR', N'Prevent Entering Duplicate Item', N'Sales Return', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (67, N'blnEditInvoiceNumberP', N'Edit Invoice Number', N'Purchase Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (68, N'blnPntInvWlSavingP', N'Print Invoice While Saving', N'Purchase Invoice', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (69, N'blnSndMlWlSavingP', N'Send Mail While Saving', N'Purchase Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (70, N'blnCnfmtnWlEditingP', N'Ask Confirmation While Editing', N'Purchase Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (71, N'blnCnfmtnWlDeletingP', N'Ask Confirmation While Deleting', N'Purchase Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (72, N'blnPwdWlDeletingP', N'Ask Password While Deleting', N'Purchase Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (73, N'blnShwMsgWlDplctItmP', N'Show Message While Entering Duplicate Item', N'Purchase Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (74, N'blnPrvntDplctItmP', N'Prevent Entering Duplicate Item', N'Purchase Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (75, N'blnEditInvoiceNumberPO', N'Edit Invoice Number', N'Purchase Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (76, N'blnPntInvWlSavingPO', N'Print Invoice While Saving', N'Purchase Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (77, N'blnSndMlWlSavingPO', N'Send Mail While Saving', N'Purchase Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (78, N'blnCnfmtnWlEditingPO', N'Ask Confirmation While Editing', N'Purchase Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (79, N'blnCnfmtnWlDeletingPO', N'Ask Confirmation While Deleting', N'Purchase Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (80, N'blnPwdWlDeletingPO', N'Ask Password While Deleting', N'Purchase Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (81, N'blnShwMsgWlDplctItmPO', N'Show Message While Entering Duplicate Item', N'Purchase Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (82, N'blnPrvntDplctItmPO', N'Prevent Entering Duplicate Item', N'Purchase Order', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (83, N'blnEditInvoiceNumberPR', N'Edit Invoice Number', N'Purchase Return', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (84, N'blnPntInvWlSavingPR', N'Print Invoice While Saving', N'Purchase Return', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (85, N'blnSndMlWlSavingPR', N'Send Mail While Saving', N'Purchase Return', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (86, N'blnCnfmtnWlEditingPR', N'Ask Confirmation While Editing', N'Purchase Return', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (87, N'blnCnfmtnWlDeletingPR', N'Ask Confirmation While Deleting', N'Purchase Return', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (88, N'blnPwdWlDeletingPR', N'Ask Password While Deleting', N'Purchase Return', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (89, N'blnShwMsgWlDplctItmPR', N'Show Message While Entering Duplicate Item', N'Purchase Return', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (90, N'blnPrvntDplctItmPR', N'Prevent Entering Duplicate Item', N'Purchase Return', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (91, N'blnShowDiscountAmntInInv', N'Show Discount Amount In invoice', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (92, N'blnShowGrossAmntInInv', N'Show Gross Amount In Invoice', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (93, N'blnShowHSNInInv', N'Show Tax Code In Invoice', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (94, N'blnShowSGSTInInv', N'Show SGST In Invoice', N'General', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (95, N'blnShowSGSTAmntInInv', N'Show SGST Amount In Invoice', N'General', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (96, N'blnShowCGSTInInv', N'Show CGST In Invoice', N'General', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (97, N'blnShowCGSTAmntInInv', N'Show CGST Amount In Invoice', N'General', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (98, N'blnShowIGSTInInv', N'Show IGST In Invoice', N'General', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (99, N'blnShowIGSTAmntInInv', N'Show IGST Amount In Invoice', N'General', 0, N'')
GO
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (100, N'blnShowVatInInvoice', N'Show Vat in Invoice', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (101, N'blnShowVatAmountInInvoice', N'Show Vat Amount In Invoice', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (102, N'blnShowGSTInInvoice', N'Show GST In Invoice', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (103, N'blnShowGSTAmountInInvoice', N'Show GST Amount In Invoice', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (104, N'strCessPercentage', N'strCessPercentage', N'General', 0, N'0.1')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (105, N'blnEnableBatch', N'Enable Batch', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (106, N'blnEnableMultiBranch', N'Enable Multi Branch', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (107, N'blnEnableMultiCompany', N'Enable Multi Company', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (108, N'strBackupPath', N'Backup Path', N'General', 0, N'Default')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (109, N'blnUpdateRateFromPurchase', N'Update Purchase Rate From Purchase', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (110, N'blnShowPurchasePrice', N'Show Purchase Price in POS', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (111, N'blnDefaultCellIsBarcode', N'Default Cell Is Barcode In Invoices', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (112, N'strDefaultCashAccountId', N'Default CashAccount', N'General', 1, N'1')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (113, N'strDefaultBankAccountId', N'Default Bank Account', N'General', 1, N'2')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (114, N'strDefaultOnlineAccountId', N'Default Online Account', N'General', 1, N'2')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (115, N'blnShowMsgWlNegativeStock', N'Show Message While Negative Stock', N'Sales Invoice', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (116, N'blnShowMsgWlNStockSE', N'Show Message While Negative Stock', N'Sales Estimate', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (117, N'blnShowMsgWlNStockSQ', N'Show Message While Negative Stock', N'Sales Quotation', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (118, N'blnShowMsgWlNStockPR', N'Show Message While Negative Stock', N'Purchase Return', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (119, N'blnEnableCutomerDisplay', N'Enable Cutomer Display', N'General', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (120, N'blnShowDiscountInInvoice', N'Show Discount In Invoices', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (121, N'strOnlinePayment', N'Online Payment Label Text', N'General', 0, N'Online Payment')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (122, N'strDefaultCurrency', N'Default Currency', N'General', 0, N'2')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (123, N'blnEnableBarbarshopMode', N'Enable Employee Commission', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (124, N'strAddressOneLabel', N'Address One Label', N'General', 0, N'Road')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (125, N'strAddressTwoLabel', N'Address Two Label', N'General', 0, N'Building')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (126, N'strAddressThreeLabel', N'Address Three Label', N'General', 0, N'Block')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (127, N'strAddressFourLabel', N'Address Four Label', N'General', 0, N'Flat')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (128, N'blnShowPriceOnProduct', N'Show Price On Product', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (129, N'blnShowSecondDisplay', N'Show Second Display', N'General', 0, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (130, N'blnEnableToEditInvoice', N'Enable To Edit Invoice', N'Importent', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (131, N'blnShowMsgWlNStockPR', N'Show Message While Negative Stock', N'Purchase Return', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (132, N'blnTaxInclusive', N'Tax Inclusive', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (133, N'blnEnableToDeleteInvoice', N'Enable To Delete Invoice', N'Importent', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (134, N'blnEnableOnlineReport', N'Enable Online Report', N'Importent', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (135, N'blnEnableSRFromInvoice', N'Enable Sales Return From Invoice', N'General', 1, N'')
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (136, N'blnShowMsgWlNStockPR', N'Show Message While Negative Stock', N'Purchase Return', 0, NULL)
INSERT [dbo].[Settings] ([Id], [Name], [Caption], [Remarks], [BitValue], [StringValue]) VALUES (137, N'blnEnableSlsBySupplier', N'Enable Sales By Supplier', N'General', 1, N'')
SET IDENTITY_INSERT [dbo].[Settings] OFF
GO
SET IDENTITY_INSERT [dbo].[StockTranLog] ON 

INSERT [dbo].[StockTranLog] ([STranId], [ProductId], [VchrId], [VchrNo], [VchrDate], [VchrType], [VchrFormType], [AccountId], [InQuantity], [OutQuantity], [UnitId], [Narration], [UserId], [TranDate], [FK_BatchId], [FK_BranchId], [Barcode]) VALUES (1104, 21, 8, 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'Purchase Voucher', N'B2C', 1, CAST(20.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), 1, N'Purchase Voucher', 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), NULL, 1, N'123')
INSERT [dbo].[StockTranLog] ([STranId], [ProductId], [VchrId], [VchrNo], [VchrDate], [VchrType], [VchrFormType], [AccountId], [InQuantity], [OutQuantity], [UnitId], [Narration], [UserId], [TranDate], [FK_BatchId], [FK_BranchId], [Barcode]) VALUES (1105, 21, 1032, 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'Sales Voucher', N'B2C', 1, CAST(0.0000 AS Decimal(24, 4)), CAST(18.0000 AS Decimal(24, 4)), 1, N'Sales Voucher', 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), NULL, 1, N'123')
INSERT [dbo].[StockTranLog] ([STranId], [ProductId], [VchrId], [VchrNo], [VchrDate], [VchrType], [VchrFormType], [AccountId], [InQuantity], [OutQuantity], [UnitId], [Narration], [UserId], [TranDate], [FK_BatchId], [FK_BranchId], [Barcode]) VALUES (1106, 21, 1033, 2, CAST(N'2022-03-16T17:43:08.000' AS DateTime), N'Sales Voucher', N'B2C', 1, CAST(0.0000 AS Decimal(24, 4)), CAST(1.0000 AS Decimal(24, 4)), 1, N'POS Entry', 1, CAST(N'2022-03-16T00:00:00.000' AS DateTime), NULL, 1, N'123')
INSERT [dbo].[StockTranLog] ([STranId], [ProductId], [VchrId], [VchrNo], [VchrDate], [VchrType], [VchrFormType], [AccountId], [InQuantity], [OutQuantity], [UnitId], [Narration], [UserId], [TranDate], [FK_BatchId], [FK_BranchId], [Barcode]) VALUES (1107, 21, 9, 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Purchase Voucher', N'B2C', 1, CAST(1.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), 1, N'Purchase Voucher', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), NULL, 6, N'123')
INSERT [dbo].[StockTranLog] ([STranId], [ProductId], [VchrId], [VchrNo], [VchrDate], [VchrType], [VchrFormType], [AccountId], [InQuantity], [OutQuantity], [UnitId], [Narration], [UserId], [TranDate], [FK_BatchId], [FK_BranchId], [Barcode]) VALUES (1108, 21, 1034, 1, CAST(N'2022-03-17T17:49:01.000' AS DateTime), N'Sales Voucher', N'B2C', 1, CAST(0.0000 AS Decimal(24, 4)), CAST(1.0000 AS Decimal(24, 4)), 1, N'POS Entry', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), NULL, 6, N'123')
SET IDENTITY_INSERT [dbo].[StockTranLog] OFF
GO
SET IDENTITY_INSERT [dbo].[TaxCategory] ON 

INSERT [dbo].[TaxCategory] ([TCategoryId], [TCategoryName], [SalesVat], [PurVat], [SaleCst], [PurCst], [SaleCess], [PurCess], [SalesXDuty], [PurXDuty]) VALUES (1, N'VAT15', CAST(15.0000 AS Decimal(24, 4)), CAST(15.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)))
INSERT [dbo].[TaxCategory] ([TCategoryId], [TCategoryName], [SalesVat], [PurVat], [SaleCst], [PurCst], [SaleCess], [PurCess], [SalesXDuty], [PurXDuty]) VALUES (2, N'VAT0', CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)))
SET IDENTITY_INSERT [dbo].[TaxCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[TransactionLog] ON 

INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1235, 8, 1, N'Purchase Voucher', CAST(N'2022-03-11T00:00:00.000' AS DateTime), 5, 1, CAST(1000.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'Purchase Voucher', 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'Main', N'1', N'B2C', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1236, 8, 1, N'Purchase Voucher', CAST(N'2022-03-11T00:00:00.000' AS DateTime), 1, 5, CAST(0.0000 AS Decimal(24, 4)), CAST(1000.0000 AS Decimal(24, 4)), N'Purchase Voucher', 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'Sub', N'1', N'B2C', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1237, 40, 1, N'Opening Voucher', CAST(N'2022-03-11T01:16:23.000' AS DateTime), 1, 1, CAST(1500.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'Opening Voucher', 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'Main', N'1', N'Debit Voucher', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1238, 1032, 1, N'Sales Voucher', CAST(N'2022-03-11T00:00:00.000' AS DateTime), 3, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(1080.0000 AS Decimal(24, 4)), N'Sales Voucher', 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'Main', N'1', N'B2C', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1239, 1032, 1, N'Sales Voucher', CAST(N'2022-03-11T00:00:00.000' AS DateTime), 1, 3, CAST(1080.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'Sales Voucher', 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'Sub', N'1', N'B2C', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1240, 41, 1, N'Receipt Voucher', CAST(N'2022-03-11T00:00:00.000' AS DateTime), 1, NULL, CAST(0.0000 AS Decimal(24, 4)), CAST(1080.0000 AS Decimal(24, 4)), N'POS1032', 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'Main', N'1', N'Cash Receipt', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1241, 41, 1, N'Receipt Voucher', CAST(N'2022-03-11T00:00:00.000' AS DateTime), NULL, 1, CAST(1080.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'POS1032', 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'Sub', N'1', N'Cash Receipt', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1242, 1033, 2, N'Sales Voucher', CAST(N'2022-03-16T17:43:08.000' AS DateTime), 3, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), N'Sales Voucher', 1, CAST(N'2022-03-16T00:00:00.000' AS DateTime), N'Main', N'2', N'B2C', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1243, 1033, 2, N'Sales Voucher', CAST(N'2022-03-16T17:43:08.000' AS DateTime), 1, 3, CAST(60.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'Sales Voucher', 1, CAST(N'2022-03-16T00:00:00.000' AS DateTime), N'Sub', N'2', N'B2C', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1244, 42, 2, N'Receipt Voucher', CAST(N'2022-03-16T17:43:08.000' AS DateTime), 1, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), N'POS1033', 1, CAST(N'2022-03-16T00:00:00.000' AS DateTime), N'Main', N'2', N'Cash Receipt', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1245, 42, 2, N'Receipt Voucher', CAST(N'2022-03-16T17:43:08.000' AS DateTime), 1, 1, CAST(60.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'POS1033', 1, CAST(N'2022-03-16T00:00:00.000' AS DateTime), N'Sub', N'2', N'Cash Receipt', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1246, 9, 1, N'Purchase Voucher', CAST(N'2022-03-17T00:00:00.000' AS DateTime), 5, 1, CAST(50.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'Purchase Voucher', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Main', N'1', N'B2C', 6, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1247, 9, 1, N'Purchase Voucher', CAST(N'2022-03-17T00:00:00.000' AS DateTime), 1, 5, CAST(0.0000 AS Decimal(24, 4)), CAST(50.0000 AS Decimal(24, 4)), N'Purchase Voucher', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Sub', N'1', N'B2C', 6, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1248, 43, 1, N'Payment Voucher', CAST(N'2022-03-17T00:00:00.000' AS DateTime), 1, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(50.0000 AS Decimal(24, 4)), N'Payment Voucher', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Main', N'1', N'Payment Voucher', 6, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1249, 43, 1, N'Payment Voucher', CAST(N'2022-03-17T00:00:00.000' AS DateTime), 1, 1, CAST(50.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'Payment Voucher', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Sub', N'1', N'Payment Voucher', 6, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1250, 44, 2, N'Payment Voucher', CAST(N'2022-03-17T00:00:00.000' AS DateTime), 2, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(50.0000 AS Decimal(24, 4)), N'Payment Voucher', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Main', N'2', N'Payment Voucher', 6, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1251, 44, 2, N'Payment Voucher', CAST(N'2022-03-17T00:00:00.000' AS DateTime), 1, 2, CAST(50.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'Payment Voucher', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Sub', N'2', N'Payment Voucher', 6, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1252, 1034, 1, N'Sales Voucher', CAST(N'2022-03-17T17:49:01.000' AS DateTime), 3, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), N'Sales Voucher', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Main', N'1', N'B2C', 6, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1253, 1034, 1, N'Sales Voucher', CAST(N'2022-03-17T17:49:01.000' AS DateTime), 1, 3, CAST(60.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'Sales Voucher', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Sub', N'1', N'B2C', 6, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1254, 45, 3, N'Receipt Voucher', CAST(N'2022-03-17T17:49:01.000' AS DateTime), 1, 1, CAST(0.0000 AS Decimal(24, 4)), CAST(60.0000 AS Decimal(24, 4)), N'POS1034', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Main', N'1', N'Cash Receipt', 6, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[TransactionLog] ([TranId], [VchrId], [VchrNo], [VchrType], [VchrDate], [AccountId], [OppAccountId], [DebitAmount], [CreditAmount], [Narration], [UserID], [TranDate], [Status], [RefNo], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (1255, 45, 3, N'Receipt Voucher', CAST(N'2022-03-17T17:49:01.000' AS DateTime), 1, 1, CAST(60.0000 AS Decimal(24, 4)), CAST(0.0000 AS Decimal(24, 4)), N'POS1034', 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Sub', N'1', N'Cash Receipt', 6, 1, N'DESKTOP-WAATECH')
SET IDENTITY_INSERT [dbo].[TransactionLog] OFF
GO
SET IDENTITY_INSERT [dbo].[Translator] ON 

INSERT [dbo].[Translator] ([Id], [Name], [Value], [Form]) VALUES (1, N'lblCompany', N'PSVAT5', N'Godown')
INSERT [dbo].[Translator] ([Id], [Name], [Value], [Form]) VALUES (2, N'lblManufacturer', N'Nagadan Pro', N'Godown')
INSERT [dbo].[Translator] ([Id], [Name], [Value], [Form]) VALUES (3, N'lblDateTime', N'23-12-2021 2:25:08 PM', N'Godown')
INSERT [dbo].[Translator] ([Id], [Name], [Value], [Form]) VALUES (4, N'lblsystem', N'DESKTOP-MO', N'Godown')
INSERT [dbo].[Translator] ([Id], [Name], [Value], [Form]) VALUES (6, N'lblDateTime', N'05/06/43 09:01:59 م', N'FinancialPeriod')
SET IDENTITY_INSERT [dbo].[Translator] OFF
GO
SET IDENTITY_INSERT [dbo].[Unit] ON 

INSERT [dbo].[Unit] ([UnitId], [UnitName], [UnitNo]) VALUES (1, N'PCs', NULL)
INSERT [dbo].[Unit] ([UnitId], [UnitName], [UnitNo]) VALUES (2, N'BOX', NULL)
INSERT [dbo].[Unit] ([UnitId], [UnitName], [UnitNo]) VALUES (3, N'PAIRE', NULL)
INSERT [dbo].[Unit] ([UnitId], [UnitName], [UnitNo]) VALUES (4, N'DZN', NULL)
INSERT [dbo].[Unit] ([UnitId], [UnitName], [UnitNo]) VALUES (5, N'KG', NULL)
INSERT [dbo].[Unit] ([UnitId], [UnitName], [UnitNo]) VALUES (6, N'PKT', NULL)
INSERT [dbo].[Unit] ([UnitId], [UnitName], [UnitNo]) VALUES (7, N'BAG', NULL)
INSERT [dbo].[Unit] ([UnitId], [UnitName], [UnitNo]) VALUES (8, N'Mtr', NULL)
SET IDENTITY_INSERT [dbo].[Unit] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [PrevilageId], [UserName], [PassWord]) VALUES (1, NULL, N'ADMIN', N'741')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[VchrTranLog] ON 

INSERT [dbo].[VchrTranLog] ([VchrTranId], [VchrNo], [VchrDate], [VchrAmount], [VchrType], [FromAccountId], [ToAccountId], [Narration], [SalesManId], [UserId], [TranDate], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (40, 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), CAST(1500.0000 AS Decimal(24, 4)), N'Opening Voucher', 1, NULL, N'', 1, 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'Debit Voucher', NULL, NULL, NULL)
INSERT [dbo].[VchrTranLog] ([VchrTranId], [VchrNo], [VchrDate], [VchrAmount], [VchrType], [FromAccountId], [ToAccountId], [Narration], [SalesManId], [UserId], [TranDate], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (41, 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), CAST(1080.0000 AS Decimal(24, 4)), N'Receipt Voucher', 1, NULL, N'POS1032', 1, 1, CAST(N'2022-03-11T00:00:00.000' AS DateTime), N'Cash Receipt', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[VchrTranLog] ([VchrTranId], [VchrNo], [VchrDate], [VchrAmount], [VchrType], [FromAccountId], [ToAccountId], [Narration], [SalesManId], [UserId], [TranDate], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (42, 2, CAST(N'2022-03-16T17:43:08.000' AS DateTime), CAST(60.0000 AS Decimal(24, 4)), N'Receipt Voucher', 1, 1, N'POS1033', 1, 1, CAST(N'2022-03-16T00:00:00.000' AS DateTime), N'Cash Receipt', 1, 1, N'DESKTOP-WAATECH')
INSERT [dbo].[VchrTranLog] ([VchrTranId], [VchrNo], [VchrDate], [VchrAmount], [VchrType], [FromAccountId], [ToAccountId], [Narration], [SalesManId], [UserId], [TranDate], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (43, 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), CAST(50.0000 AS Decimal(24, 4)), N'Payment Voucher', 1, 1, N'Cash Paid On Purchase', 1, 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Cash Payment', 6, NULL, NULL)
INSERT [dbo].[VchrTranLog] ([VchrTranId], [VchrNo], [VchrDate], [VchrAmount], [VchrType], [FromAccountId], [ToAccountId], [Narration], [SalesManId], [UserId], [TranDate], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (44, 2, CAST(N'2022-03-17T00:00:00.000' AS DateTime), CAST(50.0000 AS Decimal(24, 4)), N'Payment Voucher', 2, 1, N'Cash Paid On Purchase', 1, 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Cash Payment', 6, NULL, NULL)
INSERT [dbo].[VchrTranLog] ([VchrTranId], [VchrNo], [VchrDate], [VchrAmount], [VchrType], [FromAccountId], [ToAccountId], [Narration], [SalesManId], [UserId], [TranDate], [VchrName], [FK_BranchId], [SequenceNo], [Terminal]) VALUES (45, 3, CAST(N'2022-03-17T17:49:01.000' AS DateTime), CAST(60.0000 AS Decimal(24, 4)), N'Receipt Voucher', 1, 1, N'POS1034', 1, 1, CAST(N'2022-03-17T00:00:00.000' AS DateTime), N'Cash Receipt', 6, 1, N'DESKTOP-WAATECH')
SET IDENTITY_INSERT [dbo].[VchrTranLog] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicle] ON 

INSERT [dbo].[Vehicle] ([VanId], [VehName], [Manufacturer], [Model], [YearOfModel], [RegNo], [Ownership], [RegisterdOwner], [EngineNo], [ChasisNo], [InsurenceDate], [TaxDate], [PolusionDate], [BColor], [LoanEMIDate], [LoanEMIAmount], [Remarks], [FK_BranchId]) VALUES (1, N'ALL', NULL, NULL, NULL, N'KL', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Vehicle] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ix_product_productName]    Script Date: 3/29/2022 7:15:32 PM ******/
CREATE NONCLUSTERED INDEX [ix_product_productName] ON [dbo].[Product]
(
	[ProductName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ix_ProductBarcode_Barcode]    Script Date: 3/29/2022 7:15:32 PM ******/
CREATE NONCLUSTERED INDEX [ix_ProductBarcode_Barcode] ON [dbo].[ProductBarcode]
(
	[Barcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ix_SalesEstimateMaster_SalesInvNo]    Script Date: 3/29/2022 7:15:32 PM ******/
CREATE NONCLUSTERED INDEX [ix_SalesEstimateMaster_SalesInvNo] ON [dbo].[SalesEstimateMaster]
(
	[SalesInvNo] ASC,
	[SalesDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ix_SalesEstimateSettings_SalesInvId]    Script Date: 3/29/2022 7:15:32 PM ******/
CREATE NONCLUSTERED INDEX [ix_SalesEstimateSettings_SalesInvId] ON [dbo].[SalesEstimateSettings]
(
	[SalesInvId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [Account_AGroupId] FOREIGN KEY([AGroupId])
REFERENCES [dbo].[AccountGroup] ([AGroupId])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [Account_AGroupId]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [Account_PCategory_Id] FOREIGN KEY([PCategoryId])
REFERENCES [dbo].[PriceCategory] ([PCategoryId])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [Account_PCategory_Id]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [Account_Root_Id] FOREIGN KEY([RootId])
REFERENCES [dbo].[Root] ([RootId])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [Account_Root_Id]
GO
ALTER TABLE [dbo].[AttenMaster]  WITH CHECK ADD  CONSTRAINT [AttenMaster_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[AttenMaster] CHECK CONSTRAINT [AttenMaster_Branch_Id]
GO
ALTER TABLE [dbo].[AttenSettings]  WITH CHECK ADD  CONSTRAINT [AttenSettings_AttendanceMaster_Id] FOREIGN KEY([AttendanceMasterId])
REFERENCES [dbo].[AttenMaster] ([AttendanceMasterId])
GO
ALTER TABLE [dbo].[AttenSettings] CHECK CONSTRAINT [AttenSettings_AttendanceMaster_Id]
GO
ALTER TABLE [dbo].[AttenSettings]  WITH CHECK ADD  CONSTRAINT [AttenSettings_Emp_Id] FOREIGN KEY([EmpID])
REFERENCES [dbo].[EmployeeDetails] ([EmpID])
GO
ALTER TABLE [dbo].[AttenSettings] CHECK CONSTRAINT [AttenSettings_Emp_Id]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [Batch_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [Batch_Branch_Id]
GO
ALTER TABLE [dbo].[Batch]  WITH CHECK ADD  CONSTRAINT [Batch_PurInv_Id] FOREIGN KEY([FK_PurInvId])
REFERENCES [dbo].[PurInvMaster] ([PurInvId])
GO
ALTER TABLE [dbo].[Batch] CHECK CONSTRAINT [Batch_PurInv_Id]
GO
ALTER TABLE [dbo].[ChequeDetails]  WITH CHECK ADD  CONSTRAINT [ChequeDetails_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[ChequeDetails] CHECK CONSTRAINT [ChequeDetails_AccountId]
GO
ALTER TABLE [dbo].[ChequeDetails]  WITH CHECK ADD  CONSTRAINT [ChequeDetails_BankAcctId] FOREIGN KEY([BankAcctId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[ChequeDetails] CHECK CONSTRAINT [ChequeDetails_BankAcctId]
GO
ALTER TABLE [dbo].[ChequeDetails]  WITH CHECK ADD  CONSTRAINT [ChequeDetails_FK_BranchId] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[ChequeDetails] CHECK CONSTRAINT [ChequeDetails_FK_BranchId]
GO
ALTER TABLE [dbo].[ChequeDetails]  WITH CHECK ADD  CONSTRAINT [ChequeDetails_JeId] FOREIGN KEY([JeId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ChequeDetails] CHECK CONSTRAINT [ChequeDetails_JeId]
GO
ALTER TABLE [dbo].[ChequeDetails]  WITH CHECK ADD  CONSTRAINT [ChequeDetails_VchrId] FOREIGN KEY([VchrId])
REFERENCES [dbo].[EmployeeDetails] ([EmpID])
GO
ALTER TABLE [dbo].[ChequeDetails] CHECK CONSTRAINT [ChequeDetails_VchrId]
GO
ALTER TABLE [dbo].[CulenderPlanning]  WITH CHECK ADD  CONSTRAINT [CulenderPlanning_FK_BranchId] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[CulenderPlanning] CHECK CONSTRAINT [CulenderPlanning_FK_BranchId]
GO
ALTER TABLE [dbo].[EmployeeDetails]  WITH CHECK ADD  CONSTRAINT [EmployeeDetails_FK_BranchId] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[EmployeeDetails] CHECK CONSTRAINT [EmployeeDetails_FK_BranchId]
GO
ALTER TABLE [dbo].[EmployeeDetails]  WITH CHECK ADD  CONSTRAINT [EmployeeDetails_SalaryAccntId] FOREIGN KEY([SalaryAccntId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[EmployeeDetails] CHECK CONSTRAINT [EmployeeDetails_SalaryAccntId]
GO
ALTER TABLE [dbo].[EmployeeDetails]  WITH CHECK ADD  CONSTRAINT [EmployeeDetails_SalaryAdvAccntId] FOREIGN KEY([SalaryAdvAccntId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[EmployeeDetails] CHECK CONSTRAINT [EmployeeDetails_SalaryAdvAccntId]
GO
ALTER TABLE [dbo].[EmployeeDetails]  WITH CHECK ADD  CONSTRAINT [EmployeeDetails_SalaryPayableId] FOREIGN KEY([SalaryPayableId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[EmployeeDetails] CHECK CONSTRAINT [EmployeeDetails_SalaryPayableId]
GO
ALTER TABLE [dbo].[GdwnStckTranLog]  WITH CHECK ADD  CONSTRAINT [GdwnStckTranLog_Batch_Id] FOREIGN KEY([FK_BatchId])
REFERENCES [dbo].[Batch] ([Id])
GO
ALTER TABLE [dbo].[GdwnStckTranLog] CHECK CONSTRAINT [GdwnStckTranLog_Batch_Id]
GO
ALTER TABLE [dbo].[GdwnStckTranLog]  WITH CHECK ADD  CONSTRAINT [GdwnStckTranLog_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[GdwnStckTranLog] CHECK CONSTRAINT [GdwnStckTranLog_Branch_Id]
GO
ALTER TABLE [dbo].[GdwnStckTranLog]  WITH CHECK ADD  CONSTRAINT [GdwnStckTranLog_GodwnId] FOREIGN KEY([GodwnId])
REFERENCES [dbo].[Godown] ([GodwnId])
GO
ALTER TABLE [dbo].[GdwnStckTranLog] CHECK CONSTRAINT [GdwnStckTranLog_GodwnId]
GO
ALTER TABLE [dbo].[GdwnStckTranLog]  WITH CHECK ADD  CONSTRAINT [GdwnStckTranLog_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[GdwnStckTranLog] CHECK CONSTRAINT [GdwnStckTranLog_ProductId]
GO
ALTER TABLE [dbo].[GdwnStckTranLog]  WITH CHECK ADD  CONSTRAINT [GdwnStckTranLog_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[GdwnStckTranLog] CHECK CONSTRAINT [GdwnStckTranLog_UnitId]
GO
ALTER TABLE [dbo].[GdwnStckTranLog]  WITH CHECK ADD  CONSTRAINT [GdwnStckTranLog_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[GdwnStckTranLog] CHECK CONSTRAINT [GdwnStckTranLog_UserId]
GO
ALTER TABLE [dbo].[ManufactureDtls]  WITH CHECK ADD  CONSTRAINT [ManufactureDtls_ManufactureHdr_Id] FOREIGN KEY([FK_ManufactureHdrId])
REFERENCES [dbo].[ManufactureHdr] ([Id])
GO
ALTER TABLE [dbo].[ManufactureDtls] CHECK CONSTRAINT [ManufactureDtls_ManufactureHdr_Id]
GO
ALTER TABLE [dbo].[ManufactureDtls]  WITH CHECK ADD  CONSTRAINT [ManufactureDtls_Product_Id] FOREIGN KEY([FK_ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ManufactureDtls] CHECK CONSTRAINT [ManufactureDtls_Product_Id]
GO
ALTER TABLE [dbo].[ManufactureDtls]  WITH CHECK ADD  CONSTRAINT [ManufactureDtls_Unit_Id] FOREIGN KEY([FK_UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[ManufactureDtls] CHECK CONSTRAINT [ManufactureDtls_Unit_Id]
GO
ALTER TABLE [dbo].[ManufactureHdr]  WITH CHECK ADD  CONSTRAINT [ManufactureHdr_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[ManufactureHdr] CHECK CONSTRAINT [ManufactureHdr_Branch_Id]
GO
ALTER TABLE [dbo].[ManufactureHdr]  WITH CHECK ADD  CONSTRAINT [ManufactureHdr_Godown_Id] FOREIGN KEY([FK_GodownId])
REFERENCES [dbo].[Godown] ([GodwnId])
GO
ALTER TABLE [dbo].[ManufactureHdr] CHECK CONSTRAINT [ManufactureHdr_Godown_Id]
GO
ALTER TABLE [dbo].[ManufactureHdr]  WITH CHECK ADD  CONSTRAINT [ManufactureHdr_User_Id] FOREIGN KEY([FK_UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ManufactureHdr] CHECK CONSTRAINT [ManufactureHdr_User_Id]
GO
ALTER TABLE [dbo].[ManufacturingExp]  WITH CHECK ADD  CONSTRAINT [ManufacturingExp_ExpenseAccount_Id] FOREIGN KEY([FK_ExpenseAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[ManufacturingExp] CHECK CONSTRAINT [ManufacturingExp_ExpenseAccount_Id]
GO
ALTER TABLE [dbo].[ManufacturingExp]  WITH CHECK ADD  CONSTRAINT [ManufacturingExp_ManufactureHdr_Id] FOREIGN KEY([FK_ManufactureHdrId])
REFERENCES [dbo].[ManufactureHdr] ([Id])
GO
ALTER TABLE [dbo].[ManufacturingExp] CHECK CONSTRAINT [ManufacturingExp_ManufactureHdr_Id]
GO
ALTER TABLE [dbo].[ManufacturingExp]  WITH CHECK ADD  CONSTRAINT [ManufacturingExp_PaymentAccount_Id] FOREIGN KEY([FK_PaymentAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[ManufacturingExp] CHECK CONSTRAINT [ManufacturingExp_PaymentAccount_Id]
GO
ALTER TABLE [dbo].[MenuDetails]  WITH CHECK ADD  CONSTRAINT [MenuDetails_Module_Id] FOREIGN KEY([FK_ModuleId])
REFERENCES [dbo].[Modules] ([Id])
GO
ALTER TABLE [dbo].[MenuDetails] CHECK CONSTRAINT [MenuDetails_Module_Id]
GO
ALTER TABLE [dbo].[POSHoldMaster]  WITH CHECK ADD  CONSTRAINT [POSHoldMaster_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[POSHoldMaster] CHECK CONSTRAINT [POSHoldMaster_Branch_Id]
GO
ALTER TABLE [dbo].[POSHoldMaster]  WITH CHECK ADD  CONSTRAINT [POSHoldMaster_FYearID] FOREIGN KEY([FYearID])
REFERENCES [dbo].[FinancialYear] ([FyearId])
GO
ALTER TABLE [dbo].[POSHoldMaster] CHECK CONSTRAINT [POSHoldMaster_FYearID]
GO
ALTER TABLE [dbo].[POSHoldMaster]  WITH CHECK ADD  CONSTRAINT [POSHoldMaster_JEId] FOREIGN KEY([JEId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[POSHoldMaster] CHECK CONSTRAINT [POSHoldMaster_JEId]
GO
ALTER TABLE [dbo].[POSHoldMaster]  WITH CHECK ADD  CONSTRAINT [POSHoldMaster_PartyAccountId] FOREIGN KEY([PartyAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[POSHoldMaster] CHECK CONSTRAINT [POSHoldMaster_PartyAccountId]
GO
ALTER TABLE [dbo].[POSHoldMaster]  WITH CHECK ADD  CONSTRAINT [POSHoldMaster_PriceCategoryId] FOREIGN KEY([PriceCategoryId])
REFERENCES [dbo].[PriceCategory] ([PCategoryId])
GO
ALTER TABLE [dbo].[POSHoldMaster] CHECK CONSTRAINT [POSHoldMaster_PriceCategoryId]
GO
ALTER TABLE [dbo].[POSHoldMaster]  WITH CHECK ADD  CONSTRAINT [POSHoldMaster_RouteId] FOREIGN KEY([RouteId])
REFERENCES [dbo].[Root] ([RootId])
GO
ALTER TABLE [dbo].[POSHoldMaster] CHECK CONSTRAINT [POSHoldMaster_RouteId]
GO
ALTER TABLE [dbo].[POSHoldMaster]  WITH CHECK ADD  CONSTRAINT [POSHoldMaster_SalesAccountId] FOREIGN KEY([SalesAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[POSHoldMaster] CHECK CONSTRAINT [POSHoldMaster_SalesAccountId]
GO
ALTER TABLE [dbo].[POSHoldMaster]  WITH CHECK ADD  CONSTRAINT [POSHoldMaster_StafId] FOREIGN KEY([StafId])
REFERENCES [dbo].[EmployeeDetails] ([EmpID])
GO
ALTER TABLE [dbo].[POSHoldMaster] CHECK CONSTRAINT [POSHoldMaster_StafId]
GO
ALTER TABLE [dbo].[POSHoldMaster]  WITH CHECK ADD  CONSTRAINT [POSHoldMaster_VehId] FOREIGN KEY([VehId])
REFERENCES [dbo].[Vehicle] ([VanId])
GO
ALTER TABLE [dbo].[POSHoldMaster] CHECK CONSTRAINT [POSHoldMaster_VehId]
GO
ALTER TABLE [dbo].[POSHoldSettings]  WITH CHECK ADD  CONSTRAINT [POSHoldSettings_Batch_Id] FOREIGN KEY([FK_BatchId])
REFERENCES [dbo].[Batch] ([Id])
GO
ALTER TABLE [dbo].[POSHoldSettings] CHECK CONSTRAINT [POSHoldSettings_Batch_Id]
GO
ALTER TABLE [dbo].[POSHoldSettings]  WITH CHECK ADD  CONSTRAINT [POSHoldSettings_PartyAccountId] FOREIGN KEY([PartyAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[POSHoldSettings] CHECK CONSTRAINT [POSHoldSettings_PartyAccountId]
GO
ALTER TABLE [dbo].[POSHoldSettings]  WITH CHECK ADD  CONSTRAINT [POSHoldSettings_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[POSHoldSettings] CHECK CONSTRAINT [POSHoldSettings_ProductId]
GO
ALTER TABLE [dbo].[POSHoldSettings]  WITH CHECK ADD  CONSTRAINT [POSHoldSettings_SalesInvId] FOREIGN KEY([SalesInvId])
REFERENCES [dbo].[POSHoldMaster] ([Id])
GO
ALTER TABLE [dbo].[POSHoldSettings] CHECK CONSTRAINT [POSHoldSettings_SalesInvId]
GO
ALTER TABLE [dbo].[POSHoldSettings]  WITH CHECK ADD  CONSTRAINT [POSHoldSettings_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[POSHoldSettings] CHECK CONSTRAINT [POSHoldSettings_UnitId]
GO
ALTER TABLE [dbo].[PrintDesignSettings]  WITH CHECK ADD  CONSTRAINT [PrintDesignSettings_printMasterId] FOREIGN KEY([printMasterId])
REFERENCES [dbo].[PrintDesignMaster] ([printMasterId])
GO
ALTER TABLE [dbo].[PrintDesignSettings] CHECK CONSTRAINT [PrintDesignSettings_printMasterId]
GO
ALTER TABLE [dbo].[ProCombMaster]  WITH CHECK ADD  CONSTRAINT [ProCombMaster_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[ProCombMaster] CHECK CONSTRAINT [ProCombMaster_UnitId]
GO
ALTER TABLE [dbo].[ProCombSettings]  WITH CHECK ADD  CONSTRAINT [ProCombSettings_ProCombMasterId] FOREIGN KEY([ProCombMasterId])
REFERENCES [dbo].[ProCombMaster] ([ProCombMasterId])
GO
ALTER TABLE [dbo].[ProCombSettings] CHECK CONSTRAINT [ProCombSettings_ProCombMasterId]
GO
ALTER TABLE [dbo].[ProCombSettings]  WITH CHECK ADD  CONSTRAINT [ProCombSettings_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProCombSettings] CHECK CONSTRAINT [ProCombSettings_ProductId]
GO
ALTER TABLE [dbo].[ProCombSettings]  WITH CHECK ADD  CONSTRAINT [ProCombSettings_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[ProCombSettings] CHECK CONSTRAINT [ProCombSettings_UnitId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_AccountId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_BrandId] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brand] ([BrandId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_BrandId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_GodwnId] FOREIGN KEY([GodwnId])
REFERENCES [dbo].[Godown] ([GodwnId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_GodwnId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_PGroupId] FOREIGN KEY([PGroupId])
REFERENCES [dbo].[ProductGroup] ([PGroupId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_PGroupId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_PurchaseUnit] FOREIGN KEY([PurchaseUnit])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_PurchaseUnit]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_ReportUnit] FOREIGN KEY([ReportUnit])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_ReportUnit]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_SalesUnit] FOREIGN KEY([SalesUnit])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_SalesUnit]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_SelfId] FOREIGN KEY([SelfId])
REFERENCES [dbo].[Self] ([SelfId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_SelfId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_TCategoryId] FOREIGN KEY([TCategoryId])
REFERENCES [dbo].[TaxCategory] ([TCategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_TCategoryId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [Product_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [Product_UnitId]
GO
ALTER TABLE [dbo].[ProductBarcode]  WITH CHECK ADD  CONSTRAINT [ProductBarcode_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[ProductBarcode] CHECK CONSTRAINT [ProductBarcode_Branch_Id]
GO
ALTER TABLE [dbo].[ProductBarcode]  WITH CHECK ADD  CONSTRAINT [ProductBarcode_Product_Id] FOREIGN KEY([FK_ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductBarcode] CHECK CONSTRAINT [ProductBarcode_Product_Id]
GO
ALTER TABLE [dbo].[ProductBarcode]  WITH CHECK ADD  CONSTRAINT [ProductBarcode_Unit_Id] FOREIGN KEY([FK_UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[ProductBarcode] CHECK CONSTRAINT [ProductBarcode_Unit_Id]
GO
ALTER TABLE [dbo].[ProductBarcodeScale]  WITH CHECK ADD  CONSTRAINT [ProductBarcodeScale_Product_Id] FOREIGN KEY([FK_ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductBarcodeScale] CHECK CONSTRAINT [ProductBarcodeScale_Product_Id]
GO
ALTER TABLE [dbo].[ProductPriceCategory]  WITH CHECK ADD  CONSTRAINT [ProductPriceCategory_PCategoryId] FOREIGN KEY([PCategoryId])
REFERENCES [dbo].[PriceCategory] ([PCategoryId])
GO
ALTER TABLE [dbo].[ProductPriceCategory] CHECK CONSTRAINT [ProductPriceCategory_PCategoryId]
GO
ALTER TABLE [dbo].[ProductPriceCategory]  WITH CHECK ADD  CONSTRAINT [ProductPriceCategory_PGroupId] FOREIGN KEY([PGroupId])
REFERENCES [dbo].[ProductGroup] ([PGroupId])
GO
ALTER TABLE [dbo].[ProductPriceCategory] CHECK CONSTRAINT [ProductPriceCategory_PGroupId]
GO
ALTER TABLE [dbo].[ProductPriceCategory]  WITH CHECK ADD  CONSTRAINT [ProductPriceCategory_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductPriceCategory] CHECK CONSTRAINT [ProductPriceCategory_ProductId]
GO
ALTER TABLE [dbo].[ProductPriceCategory]  WITH CHECK ADD  CONSTRAINT [ProductPriceCategory_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[ProductPriceCategory] CHECK CONSTRAINT [ProductPriceCategory_UnitId]
GO
ALTER TABLE [dbo].[ProductUnit]  WITH CHECK ADD  CONSTRAINT [ProductUnit_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductUnit] CHECK CONSTRAINT [ProductUnit_ProductId]
GO
ALTER TABLE [dbo].[ProductUnit]  WITH CHECK ADD  CONSTRAINT [ProductUnit_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[ProductUnit] CHECK CONSTRAINT [ProductUnit_UnitId]
GO
ALTER TABLE [dbo].[PurInvMaster]  WITH CHECK ADD  CONSTRAINT [PurInvMaster_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[PurInvMaster] CHECK CONSTRAINT [PurInvMaster_Branch_Id]
GO
ALTER TABLE [dbo].[PurInvMaster]  WITH CHECK ADD  CONSTRAINT [PurInvMaster_JEId] FOREIGN KEY([JEId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PurInvMaster] CHECK CONSTRAINT [PurInvMaster_JEId]
GO
ALTER TABLE [dbo].[PurInvMaster]  WITH CHECK ADD  CONSTRAINT [PurInvMaster_PartyAccId] FOREIGN KEY([PartyAccId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[PurInvMaster] CHECK CONSTRAINT [PurInvMaster_PartyAccId]
GO
ALTER TABLE [dbo].[PurInvMaster]  WITH CHECK ADD  CONSTRAINT [PurInvMaster_PriceCategoryId] FOREIGN KEY([PriceCategoryId])
REFERENCES [dbo].[PriceCategory] ([PCategoryId])
GO
ALTER TABLE [dbo].[PurInvMaster] CHECK CONSTRAINT [PurInvMaster_PriceCategoryId]
GO
ALTER TABLE [dbo].[PurInvMaster]  WITH CHECK ADD  CONSTRAINT [PurInvMaster_PurActId] FOREIGN KEY([PurActId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[PurInvMaster] CHECK CONSTRAINT [PurInvMaster_PurActId]
GO
ALTER TABLE [dbo].[PurInvSettings]  WITH CHECK ADD  CONSTRAINT [PurInvSettings_PartyAccId] FOREIGN KEY([PartyAccId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[PurInvSettings] CHECK CONSTRAINT [PurInvSettings_PartyAccId]
GO
ALTER TABLE [dbo].[PurInvSettings]  WITH CHECK ADD  CONSTRAINT [PurInvSettings_ProId] FOREIGN KEY([ProId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[PurInvSettings] CHECK CONSTRAINT [PurInvSettings_ProId]
GO
ALTER TABLE [dbo].[PurInvSettings]  WITH CHECK ADD  CONSTRAINT [PurInvSettings_PurInvId] FOREIGN KEY([PurInvId])
REFERENCES [dbo].[PurInvMaster] ([PurInvId])
GO
ALTER TABLE [dbo].[PurInvSettings] CHECK CONSTRAINT [PurInvSettings_PurInvId]
GO
ALTER TABLE [dbo].[PurInvSettings]  WITH CHECK ADD  CONSTRAINT [PurInvSettings_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[PurInvSettings] CHECK CONSTRAINT [PurInvSettings_UnitId]
GO
ALTER TABLE [dbo].[PurRtnInvMaster]  WITH CHECK ADD  CONSTRAINT [PurRtnInvMaster_Batch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[PurRtnInvMaster] CHECK CONSTRAINT [PurRtnInvMaster_Batch_Id]
GO
ALTER TABLE [dbo].[PurRtnInvMaster]  WITH CHECK ADD  CONSTRAINT [PurRtnInvMaster_JEId] FOREIGN KEY([JEId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PurRtnInvMaster] CHECK CONSTRAINT [PurRtnInvMaster_JEId]
GO
ALTER TABLE [dbo].[PurRtnInvMaster]  WITH CHECK ADD  CONSTRAINT [PurRtnInvMaster_PartyAcId] FOREIGN KEY([PartyAcId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[PurRtnInvMaster] CHECK CONSTRAINT [PurRtnInvMaster_PartyAcId]
GO
ALTER TABLE [dbo].[PurRtnInvMaster]  WITH CHECK ADD  CONSTRAINT [PurRtnInvMaster_PriceCategoryId] FOREIGN KEY([PriceCategoryId])
REFERENCES [dbo].[PriceCategory] ([PCategoryId])
GO
ALTER TABLE [dbo].[PurRtnInvMaster] CHECK CONSTRAINT [PurRtnInvMaster_PriceCategoryId]
GO
ALTER TABLE [dbo].[PurRtnInvMaster]  WITH CHECK ADD  CONSTRAINT [PurRtnInvMaster_PurRtnAcId] FOREIGN KEY([PurRtnAcId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[PurRtnInvMaster] CHECK CONSTRAINT [PurRtnInvMaster_PurRtnAcId]
GO
ALTER TABLE [dbo].[PurRtnInvSttings]  WITH CHECK ADD  CONSTRAINT [PurRtnInvSttings_Batch_Id] FOREIGN KEY([FK_BatchId])
REFERENCES [dbo].[Batch] ([Id])
GO
ALTER TABLE [dbo].[PurRtnInvSttings] CHECK CONSTRAINT [PurRtnInvSttings_Batch_Id]
GO
ALTER TABLE [dbo].[PurRtnInvSttings]  WITH CHECK ADD  CONSTRAINT [PurRtnInvSttings_PartyAccId] FOREIGN KEY([PartyAccId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[PurRtnInvSttings] CHECK CONSTRAINT [PurRtnInvSttings_PartyAccId]
GO
ALTER TABLE [dbo].[PurRtnInvSttings]  WITH CHECK ADD  CONSTRAINT [PurRtnInvSttings_ProId] FOREIGN KEY([ProId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[PurRtnInvSttings] CHECK CONSTRAINT [PurRtnInvSttings_ProId]
GO
ALTER TABLE [dbo].[PurRtnInvSttings]  WITH CHECK ADD  CONSTRAINT [PurRtnInvSttings_PurRtnInvId] FOREIGN KEY([PurRtnInvId])
REFERENCES [dbo].[PurRtnInvMaster] ([PurRtnInvId])
GO
ALTER TABLE [dbo].[PurRtnInvSttings] CHECK CONSTRAINT [PurRtnInvSttings_PurRtnInvId]
GO
ALTER TABLE [dbo].[PurRtnInvSttings]  WITH CHECK ADD  CONSTRAINT [PurRtnInvSttings_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[PurRtnInvSttings] CHECK CONSTRAINT [PurRtnInvSttings_UnitId]
GO
ALTER TABLE [dbo].[RawmaterialMapping]  WITH CHECK ADD  CONSTRAINT [RawmaterialMapping_Batch_Id] FOREIGN KEY([FK_BatchId])
REFERENCES [dbo].[Batch] ([Id])
GO
ALTER TABLE [dbo].[RawmaterialMapping] CHECK CONSTRAINT [RawmaterialMapping_Batch_Id]
GO
ALTER TABLE [dbo].[RawmaterialMapping]  WITH CHECK ADD  CONSTRAINT [RawmaterialMapping_FK_ProductId] FOREIGN KEY([FK_ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[RawmaterialMapping] CHECK CONSTRAINT [RawmaterialMapping_FK_ProductId]
GO
ALTER TABLE [dbo].[RawmaterialMapping]  WITH CHECK ADD  CONSTRAINT [RawmaterialMapping_ManufactureHdr_Id] FOREIGN KEY([FK_ManufactureHdrId])
REFERENCES [dbo].[ManufactureHdr] ([Id])
GO
ALTER TABLE [dbo].[RawmaterialMapping] CHECK CONSTRAINT [RawmaterialMapping_ManufactureHdr_Id]
GO
ALTER TABLE [dbo].[RawmaterialMapping]  WITH CHECK ADD  CONSTRAINT [RawmaterialMapping_Unit_Id] FOREIGN KEY([FK_UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[RawmaterialMapping] CHECK CONSTRAINT [RawmaterialMapping_Unit_Id]
GO
ALTER TABLE [dbo].[ReminderTB]  WITH CHECK ADD  CONSTRAINT [ReminderTB_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[ReminderTB] CHECK CONSTRAINT [ReminderTB_Branch_Id]
GO
ALTER TABLE [dbo].[ReminderTB]  WITH CHECK ADD  CONSTRAINT [ReminderTB_TargetUserId] FOREIGN KEY([TargetUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ReminderTB] CHECK CONSTRAINT [ReminderTB_TargetUserId]
GO
ALTER TABLE [dbo].[ReminderTB]  WITH CHECK ADD  CONSTRAINT [ReminderTB_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ReminderTB] CHECK CONSTRAINT [ReminderTB_UserId]
GO
ALTER TABLE [dbo].[Root]  WITH CHECK ADD  CONSTRAINT [Root_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Root] CHECK CONSTRAINT [Root_Branch_Id]
GO
ALTER TABLE [dbo].[Root]  WITH CHECK ADD  CONSTRAINT [Root_DriverId] FOREIGN KEY([DriverId])
REFERENCES [dbo].[EmployeeDetails] ([EmpID])
GO
ALTER TABLE [dbo].[Root] CHECK CONSTRAINT [Root_DriverId]
GO
ALTER TABLE [dbo].[Root]  WITH CHECK ADD  CONSTRAINT [Root_SalesManId] FOREIGN KEY([SalesManId])
REFERENCES [dbo].[EmployeeDetails] ([EmpID])
GO
ALTER TABLE [dbo].[Root] CHECK CONSTRAINT [Root_SalesManId]
GO
ALTER TABLE [dbo].[Root]  WITH CHECK ADD  CONSTRAINT [Root_VehicleId] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicle] ([VanId])
GO
ALTER TABLE [dbo].[Root] CHECK CONSTRAINT [Root_VehicleId]
GO
ALTER TABLE [dbo].[SalaryProcessMaster]  WITH CHECK ADD  CONSTRAINT [SalaryProcessMaster_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[SalaryProcessMaster] CHECK CONSTRAINT [SalaryProcessMaster_Branch_Id]
GO
ALTER TABLE [dbo].[SalesCounter]  WITH CHECK ADD  CONSTRAINT [SalesCounter_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[SalesCounter] CHECK CONSTRAINT [SalesCounter_Branch_Id]
GO
ALTER TABLE [dbo].[SalesEstimateMaster]  WITH CHECK ADD  CONSTRAINT [SalesEstimateMaster_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[SalesEstimateMaster] CHECK CONSTRAINT [SalesEstimateMaster_Branch_Id]
GO
ALTER TABLE [dbo].[SalesEstimateMaster]  WITH CHECK ADD  CONSTRAINT [SalesEstimateMaster_FYearID] FOREIGN KEY([FYearID])
REFERENCES [dbo].[FinancialYear] ([FyearId])
GO
ALTER TABLE [dbo].[SalesEstimateMaster] CHECK CONSTRAINT [SalesEstimateMaster_FYearID]
GO
ALTER TABLE [dbo].[SalesEstimateMaster]  WITH CHECK ADD  CONSTRAINT [SalesEstimateMaster_JEId] FOREIGN KEY([JEId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[SalesEstimateMaster] CHECK CONSTRAINT [SalesEstimateMaster_JEId]
GO
ALTER TABLE [dbo].[SalesEstimateMaster]  WITH CHECK ADD  CONSTRAINT [SalesEstimateMaster_PartyAccountId] FOREIGN KEY([PartyAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[SalesEstimateMaster] CHECK CONSTRAINT [SalesEstimateMaster_PartyAccountId]
GO
ALTER TABLE [dbo].[SalesEstimateMaster]  WITH CHECK ADD  CONSTRAINT [SalesEstimateMaster_PriceCategoryId] FOREIGN KEY([PriceCategoryId])
REFERENCES [dbo].[PriceCategory] ([PCategoryId])
GO
ALTER TABLE [dbo].[SalesEstimateMaster] CHECK CONSTRAINT [SalesEstimateMaster_PriceCategoryId]
GO
ALTER TABLE [dbo].[SalesEstimateMaster]  WITH CHECK ADD  CONSTRAINT [SalesEstimateMaster_RouteId] FOREIGN KEY([RouteId])
REFERENCES [dbo].[Root] ([RootId])
GO
ALTER TABLE [dbo].[SalesEstimateMaster] CHECK CONSTRAINT [SalesEstimateMaster_RouteId]
GO
ALTER TABLE [dbo].[SalesEstimateMaster]  WITH CHECK ADD  CONSTRAINT [SalesEstimateMaster_SalesAccountId] FOREIGN KEY([SalesAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[SalesEstimateMaster] CHECK CONSTRAINT [SalesEstimateMaster_SalesAccountId]
GO
ALTER TABLE [dbo].[SalesEstimateMaster]  WITH CHECK ADD  CONSTRAINT [SalesEstimateMaster_StafId] FOREIGN KEY([StafId])
REFERENCES [dbo].[EmployeeDetails] ([EmpID])
GO
ALTER TABLE [dbo].[SalesEstimateMaster] CHECK CONSTRAINT [SalesEstimateMaster_StafId]
GO
ALTER TABLE [dbo].[SalesEstimateMaster]  WITH CHECK ADD  CONSTRAINT [SalesEstimateMaster_VehId] FOREIGN KEY([VehId])
REFERENCES [dbo].[Vehicle] ([VanId])
GO
ALTER TABLE [dbo].[SalesEstimateMaster] CHECK CONSTRAINT [SalesEstimateMaster_VehId]
GO
ALTER TABLE [dbo].[SalesEstimateSettings]  WITH CHECK ADD  CONSTRAINT [SalesEstimateSettings_FK_BatchId] FOREIGN KEY([FK_BatchId])
REFERENCES [dbo].[Batch] ([Id])
GO
ALTER TABLE [dbo].[SalesEstimateSettings] CHECK CONSTRAINT [SalesEstimateSettings_FK_BatchId]
GO
ALTER TABLE [dbo].[SalesEstimateSettings]  WITH CHECK ADD  CONSTRAINT [SalesEstimateSettings_PartyAccountId] FOREIGN KEY([PartyAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[SalesEstimateSettings] CHECK CONSTRAINT [SalesEstimateSettings_PartyAccountId]
GO
ALTER TABLE [dbo].[SalesEstimateSettings]  WITH CHECK ADD  CONSTRAINT [SalesEstimateSettings_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[SalesEstimateSettings] CHECK CONSTRAINT [SalesEstimateSettings_ProductId]
GO
ALTER TABLE [dbo].[SalesEstimateSettings]  WITH CHECK ADD  CONSTRAINT [SalesEstimateSettings_SalesInvId] FOREIGN KEY([SalesInvId])
REFERENCES [dbo].[SalesEstimateMaster] ([SalesInvId])
GO
ALTER TABLE [dbo].[SalesEstimateSettings] CHECK CONSTRAINT [SalesEstimateSettings_SalesInvId]
GO
ALTER TABLE [dbo].[SalesEstimateSettings]  WITH CHECK ADD  CONSTRAINT [SalesEstimateSettings_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[SalesEstimateSettings] CHECK CONSTRAINT [SalesEstimateSettings_UnitId]
GO
ALTER TABLE [dbo].[SalesOrderMaster]  WITH CHECK ADD  CONSTRAINT [SalesOrderMaster_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[SalesOrderMaster] CHECK CONSTRAINT [SalesOrderMaster_Branch_Id]
GO
ALTER TABLE [dbo].[SalesOrderMaster]  WITH CHECK ADD  CONSTRAINT [SalesOrderMaster_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[SalesOrderMaster] CHECK CONSTRAINT [SalesOrderMaster_CustomerId]
GO
ALTER TABLE [dbo].[SalesOrderMaster]  WITH CHECK ADD  CONSTRAINT [SalesOrderMaster_JEid] FOREIGN KEY([JEid])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[SalesOrderMaster] CHECK CONSTRAINT [SalesOrderMaster_JEid]
GO
ALTER TABLE [dbo].[SalesOrderMaster]  WITH CHECK ADD  CONSTRAINT [SalesOrderMaster_PriceCategoryId] FOREIGN KEY([PriceCategoryId])
REFERENCES [dbo].[PriceCategory] ([PCategoryId])
GO
ALTER TABLE [dbo].[SalesOrderMaster] CHECK CONSTRAINT [SalesOrderMaster_PriceCategoryId]
GO
ALTER TABLE [dbo].[SalesOrderMaster]  WITH CHECK ADD  CONSTRAINT [SalesOrderMaster_RouteId] FOREIGN KEY([RouteId])
REFERENCES [dbo].[Root] ([RootId])
GO
ALTER TABLE [dbo].[SalesOrderMaster] CHECK CONSTRAINT [SalesOrderMaster_RouteId]
GO
ALTER TABLE [dbo].[SalesOrderMaster]  WITH CHECK ADD  CONSTRAINT [SalesOrderMaster_SalesAccountId] FOREIGN KEY([SalesAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[SalesOrderMaster] CHECK CONSTRAINT [SalesOrderMaster_SalesAccountId]
GO
ALTER TABLE [dbo].[SalesOrderMaster]  WITH CHECK ADD  CONSTRAINT [SalesOrderMaster_SalesManId] FOREIGN KEY([SalesManId])
REFERENCES [dbo].[EmployeeDetails] ([EmpID])
GO
ALTER TABLE [dbo].[SalesOrderMaster] CHECK CONSTRAINT [SalesOrderMaster_SalesManId]
GO
ALTER TABLE [dbo].[SalesOrderSettings]  WITH CHECK ADD  CONSTRAINT [SalesOrderSettings_FK_BatchId] FOREIGN KEY([FK_BatchId])
REFERENCES [dbo].[Batch] ([Id])
GO
ALTER TABLE [dbo].[SalesOrderSettings] CHECK CONSTRAINT [SalesOrderSettings_FK_BatchId]
GO
ALTER TABLE [dbo].[SalesOrderSettings]  WITH CHECK ADD  CONSTRAINT [SalesOrderSettings_PartyAccountId] FOREIGN KEY([PartyAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[SalesOrderSettings] CHECK CONSTRAINT [SalesOrderSettings_PartyAccountId]
GO
ALTER TABLE [dbo].[SalesOrderSettings]  WITH CHECK ADD  CONSTRAINT [SalesOrderSettings_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[SalesOrderSettings] CHECK CONSTRAINT [SalesOrderSettings_ProductId]
GO
ALTER TABLE [dbo].[SalesOrderSettings]  WITH CHECK ADD  CONSTRAINT [SalesOrderSettings_SalesOrderMasterId] FOREIGN KEY([SalesOrderMasterId])
REFERENCES [dbo].[SalesOrderMaster] ([SalesOrderMasterId])
GO
ALTER TABLE [dbo].[SalesOrderSettings] CHECK CONSTRAINT [SalesOrderSettings_SalesOrderMasterId]
GO
ALTER TABLE [dbo].[SalesOrderSettings]  WITH CHECK ADD  CONSTRAINT [SalesOrderSettings_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[SalesOrderSettings] CHECK CONSTRAINT [SalesOrderSettings_UnitId]
GO
ALTER TABLE [dbo].[SalesRtnMaster]  WITH CHECK ADD  CONSTRAINT [SalesRtnMaster_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[SalesRtnMaster] CHECK CONSTRAINT [SalesRtnMaster_Branch_Id]
GO
ALTER TABLE [dbo].[SalesRtnMaster]  WITH CHECK ADD  CONSTRAINT [SalesRtnMaster_JeId] FOREIGN KEY([JeId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[SalesRtnMaster] CHECK CONSTRAINT [SalesRtnMaster_JeId]
GO
ALTER TABLE [dbo].[SalesRtnMaster]  WITH CHECK ADD  CONSTRAINT [SalesRtnMaster_PartyAccountId] FOREIGN KEY([PartyAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[SalesRtnMaster] CHECK CONSTRAINT [SalesRtnMaster_PartyAccountId]
GO
ALTER TABLE [dbo].[SalesRtnMaster]  WITH CHECK ADD  CONSTRAINT [SalesRtnMaster_PriceCategoryId] FOREIGN KEY([PriceCategoryId])
REFERENCES [dbo].[PriceCategory] ([PCategoryId])
GO
ALTER TABLE [dbo].[SalesRtnMaster] CHECK CONSTRAINT [SalesRtnMaster_PriceCategoryId]
GO
ALTER TABLE [dbo].[SalesRtnMaster]  WITH CHECK ADD  CONSTRAINT [SalesRtnMaster_RouteId] FOREIGN KEY([RouteId])
REFERENCES [dbo].[Root] ([RootId])
GO
ALTER TABLE [dbo].[SalesRtnMaster] CHECK CONSTRAINT [SalesRtnMaster_RouteId]
GO
ALTER TABLE [dbo].[SalesRtnMaster]  WITH CHECK ADD  CONSTRAINT [SalesRtnMaster_SlsRtnAccountId] FOREIGN KEY([SlsRtnAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[SalesRtnMaster] CHECK CONSTRAINT [SalesRtnMaster_SlsRtnAccountId]
GO
ALTER TABLE [dbo].[SalesRtnMaster]  WITH CHECK ADD  CONSTRAINT [SalesRtnMaster_StaffId] FOREIGN KEY([StaffId])
REFERENCES [dbo].[EmployeeDetails] ([EmpID])
GO
ALTER TABLE [dbo].[SalesRtnMaster] CHECK CONSTRAINT [SalesRtnMaster_StaffId]
GO
ALTER TABLE [dbo].[SalesRtnMaster]  WITH CHECK ADD  CONSTRAINT [SalesRtnMaster_VehId] FOREIGN KEY([VehId])
REFERENCES [dbo].[Vehicle] ([VanId])
GO
ALTER TABLE [dbo].[SalesRtnMaster] CHECK CONSTRAINT [SalesRtnMaster_VehId]
GO
ALTER TABLE [dbo].[SelectedFYear]  WITH CHECK ADD  CONSTRAINT [SelectedFYear_FyearId] FOREIGN KEY([FyearId])
REFERENCES [dbo].[FinancialYear] ([FyearId])
GO
ALTER TABLE [dbo].[SelectedFYear] CHECK CONSTRAINT [SelectedFYear_FyearId]
GO
ALTER TABLE [dbo].[Self]  WITH CHECK ADD  CONSTRAINT [Self_GodownId] FOREIGN KEY([GodownId])
REFERENCES [dbo].[Godown] ([GodwnId])
GO
ALTER TABLE [dbo].[Self] CHECK CONSTRAINT [Self_GodownId]
GO
ALTER TABLE [dbo].[SlryPrcsSettings]  WITH CHECK ADD  CONSTRAINT [SlryPrcsSettings_EmpID] FOREIGN KEY([EmpID])
REFERENCES [dbo].[EmployeeDetails] ([EmpID])
GO
ALTER TABLE [dbo].[SlryPrcsSettings] CHECK CONSTRAINT [SlryPrcsSettings_EmpID]
GO
ALTER TABLE [dbo].[SlryPrcsSettings]  WITH CHECK ADD  CONSTRAINT [SlryPrcsSettings_SprcsMasterID] FOREIGN KEY([SprcsMasterID])
REFERENCES [dbo].[SalaryProcessMaster] ([SPrcsMasterID])
GO
ALTER TABLE [dbo].[SlryPrcsSettings] CHECK CONSTRAINT [SlryPrcsSettings_SprcsMasterID]
GO
ALTER TABLE [dbo].[SlsRtnSettings]  WITH CHECK ADD  CONSTRAINT [SlsRtnSettings_FK_BatchId] FOREIGN KEY([FK_BatchId])
REFERENCES [dbo].[Batch] ([Id])
GO
ALTER TABLE [dbo].[SlsRtnSettings] CHECK CONSTRAINT [SlsRtnSettings_FK_BatchId]
GO
ALTER TABLE [dbo].[SlsRtnSettings]  WITH CHECK ADD  CONSTRAINT [SlsRtnSettings_PartyAccountId] FOREIGN KEY([PartyAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[SlsRtnSettings] CHECK CONSTRAINT [SlsRtnSettings_PartyAccountId]
GO
ALTER TABLE [dbo].[SlsRtnSettings]  WITH CHECK ADD  CONSTRAINT [SlsRtnSettings_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[SlsRtnSettings] CHECK CONSTRAINT [SlsRtnSettings_ProductId]
GO
ALTER TABLE [dbo].[SlsRtnSettings]  WITH CHECK ADD  CONSTRAINT [SlsRtnSettings_SlsRtnId] FOREIGN KEY([SlsRtnId])
REFERENCES [dbo].[SalesRtnMaster] ([SlsRtrnId])
GO
ALTER TABLE [dbo].[SlsRtnSettings] CHECK CONSTRAINT [SlsRtnSettings_SlsRtnId]
GO
ALTER TABLE [dbo].[SlsRtnSettings]  WITH CHECK ADD  CONSTRAINT [SlsRtnSettings_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[SlsRtnSettings] CHECK CONSTRAINT [SlsRtnSettings_UnitId]
GO
ALTER TABLE [dbo].[StockAdjusterMaster]  WITH CHECK ADD  CONSTRAINT [StockAdjusterMaster_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[StockAdjusterMaster] CHECK CONSTRAINT [StockAdjusterMaster_Branch_Id]
GO
ALTER TABLE [dbo].[StockAdjusterMaster]  WITH CHECK ADD  CONSTRAINT [StockAdjusterMaster_ProBrand] FOREIGN KEY([ProBrand])
REFERENCES [dbo].[Brand] ([BrandId])
GO
ALTER TABLE [dbo].[StockAdjusterMaster] CHECK CONSTRAINT [StockAdjusterMaster_ProBrand]
GO
ALTER TABLE [dbo].[StockAdjusterMaster]  WITH CHECK ADD  CONSTRAINT [StockAdjusterMaster_ProtGroup] FOREIGN KEY([ProtGroup])
REFERENCES [dbo].[ProductGroup] ([PGroupId])
GO
ALTER TABLE [dbo].[StockAdjusterMaster] CHECK CONSTRAINT [StockAdjusterMaster_ProtGroup]
GO
ALTER TABLE [dbo].[StockAdjustSettings]  WITH CHECK ADD  CONSTRAINT [StockAdjustSettings_FK_BatchId] FOREIGN KEY([FK_BatchId])
REFERENCES [dbo].[Batch] ([Id])
GO
ALTER TABLE [dbo].[StockAdjustSettings] CHECK CONSTRAINT [StockAdjustSettings_FK_BatchId]
GO
ALTER TABLE [dbo].[StockAdjustSettings]  WITH CHECK ADD  CONSTRAINT [StockAdjustSettings_productId] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[StockAdjustSettings] CHECK CONSTRAINT [StockAdjustSettings_productId]
GO
ALTER TABLE [dbo].[StockAdjustSettings]  WITH CHECK ADD  CONSTRAINT [StockAdjustSettings_stockAdjusterMasterId] FOREIGN KEY([stockAdjusterMasterId])
REFERENCES [dbo].[StockAdjusterMaster] ([stockAdjusterMasterId])
GO
ALTER TABLE [dbo].[StockAdjustSettings] CHECK CONSTRAINT [StockAdjustSettings_stockAdjusterMasterId]
GO
ALTER TABLE [dbo].[StockAdjustSettings]  WITH CHECK ADD  CONSTRAINT [StockAdjustSettings_unitId] FOREIGN KEY([unitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[StockAdjustSettings] CHECK CONSTRAINT [StockAdjustSettings_unitId]
GO
ALTER TABLE [dbo].[StockTranLog]  WITH CHECK ADD  CONSTRAINT [StockTranLog_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[StockTranLog] CHECK CONSTRAINT [StockTranLog_AccountId]
GO
ALTER TABLE [dbo].[StockTranLog]  WITH CHECK ADD  CONSTRAINT [StockTranLog_Batch_Id] FOREIGN KEY([FK_BatchId])
REFERENCES [dbo].[Batch] ([Id])
GO
ALTER TABLE [dbo].[StockTranLog] CHECK CONSTRAINT [StockTranLog_Batch_Id]
GO
ALTER TABLE [dbo].[StockTranLog]  WITH CHECK ADD  CONSTRAINT [StockTranLog_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[StockTranLog] CHECK CONSTRAINT [StockTranLog_Branch_Id]
GO
ALTER TABLE [dbo].[StockTranLog]  WITH CHECK ADD  CONSTRAINT [StockTranLog_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[StockTranLog] CHECK CONSTRAINT [StockTranLog_ProductId]
GO
ALTER TABLE [dbo].[StockTranLog]  WITH CHECK ADD  CONSTRAINT [StockTranLog_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[StockTranLog] CHECK CONSTRAINT [StockTranLog_UnitId]
GO
ALTER TABLE [dbo].[StockTranLog]  WITH CHECK ADD  CONSTRAINT [StockTranLog_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[StockTranLog] CHECK CONSTRAINT [StockTranLog_UserId]
GO
ALTER TABLE [dbo].[StockTranSettings]  WITH CHECK ADD  CONSTRAINT [StockTranSettings_Batch_Id] FOREIGN KEY([FK_BatchId])
REFERENCES [dbo].[Batch] ([Id])
GO
ALTER TABLE [dbo].[StockTranSettings] CHECK CONSTRAINT [StockTranSettings_Batch_Id]
GO
ALTER TABLE [dbo].[StockTranSettings]  WITH CHECK ADD  CONSTRAINT [StockTranSettings_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[StockTranSettings] CHECK CONSTRAINT [StockTranSettings_ProductId]
GO
ALTER TABLE [dbo].[StockTranSettings]  WITH CHECK ADD  CONSTRAINT [StockTranSettings_StockTransferId] FOREIGN KEY([StockTransferId])
REFERENCES [dbo].[StockTransferMaster] ([StockTransferId])
GO
ALTER TABLE [dbo].[StockTranSettings] CHECK CONSTRAINT [StockTranSettings_StockTransferId]
GO
ALTER TABLE [dbo].[StockTranSettings]  WITH CHECK ADD  CONSTRAINT [StockTranSettings_UnitId] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([UnitId])
GO
ALTER TABLE [dbo].[StockTranSettings] CHECK CONSTRAINT [StockTranSettings_UnitId]
GO
ALTER TABLE [dbo].[StockTransferMaster]  WITH CHECK ADD  CONSTRAINT [StockTransferMaster_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[StockTransferMaster] CHECK CONSTRAINT [StockTransferMaster_Branch_Id]
GO
ALTER TABLE [dbo].[StockTransferMaster]  WITH CHECK ADD  CONSTRAINT [StockTransferMaster_FromGodownId] FOREIGN KEY([FromGodownId])
REFERENCES [dbo].[Godown] ([GodwnId])
GO
ALTER TABLE [dbo].[StockTransferMaster] CHECK CONSTRAINT [StockTransferMaster_FromGodownId]
GO
ALTER TABLE [dbo].[StockTransferMaster]  WITH CHECK ADD  CONSTRAINT [StockTransferMaster_ToGodownId] FOREIGN KEY([ToGodownId])
REFERENCES [dbo].[Godown] ([GodwnId])
GO
ALTER TABLE [dbo].[StockTransferMaster] CHECK CONSTRAINT [StockTransferMaster_ToGodownId]
GO
ALTER TABLE [dbo].[StockTransferMaster]  WITH CHECK ADD  CONSTRAINT [StockTransferMaster_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[StockTransferMaster] CHECK CONSTRAINT [StockTransferMaster_UserId]
GO
ALTER TABLE [dbo].[TransactionLog]  WITH CHECK ADD  CONSTRAINT [TransactionLog_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[TransactionLog] CHECK CONSTRAINT [TransactionLog_AccountId]
GO
ALTER TABLE [dbo].[TransactionLog]  WITH CHECK ADD  CONSTRAINT [TransactionLog_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[TransactionLog] CHECK CONSTRAINT [TransactionLog_Branch_Id]
GO
ALTER TABLE [dbo].[TransactionLog]  WITH CHECK ADD  CONSTRAINT [TransactionLog_OppAccountId] FOREIGN KEY([OppAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[TransactionLog] CHECK CONSTRAINT [TransactionLog_OppAccountId]
GO
ALTER TABLE [dbo].[TransactionLog]  WITH CHECK ADD  CONSTRAINT [TransactionLog_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[TransactionLog] CHECK CONSTRAINT [TransactionLog_UserID]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [User_PrevilageId] FOREIGN KEY([PrevilageId])
REFERENCES [dbo].[UserPrevilegeHdr] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [User_PrevilageId]
GO
ALTER TABLE [dbo].[UserPrevilegeDtl]  WITH CHECK ADD  CONSTRAINT [UserPrevilegeDtl_MenuDetails_Id] FOREIGN KEY([FK_MenuDetailsId])
REFERENCES [dbo].[MenuDetails] ([Id])
GO
ALTER TABLE [dbo].[UserPrevilegeDtl] CHECK CONSTRAINT [UserPrevilegeDtl_MenuDetails_Id]
GO
ALTER TABLE [dbo].[UserPrevilegeDtl]  WITH CHECK ADD  CONSTRAINT [UserPrevilegeDtl_UserPrevilegeHdr_Id] FOREIGN KEY([FK_UserPrevilegeHdrId])
REFERENCES [dbo].[UserPrevilegeHdr] ([Id])
GO
ALTER TABLE [dbo].[UserPrevilegeDtl] CHECK CONSTRAINT [UserPrevilegeDtl_UserPrevilegeHdr_Id]
GO
ALTER TABLE [dbo].[VchrTranLog]  WITH CHECK ADD  CONSTRAINT [VchrTranLog_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[VchrTranLog] CHECK CONSTRAINT [VchrTranLog_Branch_Id]
GO
ALTER TABLE [dbo].[VchrTranLog]  WITH CHECK ADD  CONSTRAINT [VchrTranLog_FromAccountId] FOREIGN KEY([FromAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[VchrTranLog] CHECK CONSTRAINT [VchrTranLog_FromAccountId]
GO
ALTER TABLE [dbo].[VchrTranLog]  WITH CHECK ADD  CONSTRAINT [VchrTranLog_SalesManId] FOREIGN KEY([SalesManId])
REFERENCES [dbo].[EmployeeDetails] ([EmpID])
GO
ALTER TABLE [dbo].[VchrTranLog] CHECK CONSTRAINT [VchrTranLog_SalesManId]
GO
ALTER TABLE [dbo].[VchrTranLog]  WITH CHECK ADD  CONSTRAINT [VchrTranLog_ToAccountId] FOREIGN KEY([ToAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[VchrTranLog] CHECK CONSTRAINT [VchrTranLog_ToAccountId]
GO
ALTER TABLE [dbo].[VchrTranLog]  WITH CHECK ADD  CONSTRAINT [VchrTranLog_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[VchrTranLog] CHECK CONSTRAINT [VchrTranLog_UserId]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [Vehicle_Branch_Id] FOREIGN KEY([FK_BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [Vehicle_Branch_Id]
GO
/****** Object:  StoredProcedure [dbo].[spBalanceSheetBalance]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spBalanceSheetBalance]
(
@AccountType nvarchar (10) = 'C',
@AccountId int=null,
@BranchId int=null,
@Date datetime='2021-07-30'
)
As
Begin
if @AccountType = 'C'
Select SUM(tlg.CreditAmount-tlg.DebitAmount) as LedgerAmount 
from TransactionLog tlg
where (tlg.AccountId=@AccountId or @AccountId is null) and
(tlg.FK_BranchId=@BranchId or @BranchId is null) and
tlg.VchrDate <= @Date
else
Select SUM(tlg.DebitAmount-tlg.CreditAmount) as LedgerAmount 
from TransactionLog tlg
where (tlg.AccountId=@AccountId or @AccountId is null) and
(tlg.FK_BranchId=@BranchId or @BranchId is null) and
tlg.VchrDate <= @Date
END

GO
/****** Object:  StoredProcedure [dbo].[spBalanceSheetOppeningStock]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spBalanceSheetOppeningStock]
(
@Validation nvarchar(50)='',
@BranchId int=null,
@Date datetime='2021-07-14'
)
As
Begin
create table #productrates
 (
  Rates decimal,
  ProductId int,
  Stock decimal
  )
  if @Validation='Selling Price'
  insert into #productrates select pt.SellingPrice as Rates,pt.ProductId,pt.OpeningStock as Stock
  From Product pt

  else if @Validation='Average Selling Price'
   insert into #productrates select isnull(AVG(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,pt.OpeningStock as Stock
  From Product pt
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
  left join SalesEstimateMaster sem on ses.SalesInvId=sem.SalesInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  ses.Date <@Date
  group by pt.ProductId,pt.SellingPrice,pt.OpeningStock
  order by pt.ProductId

  else if @Validation='Highest Selling Price'
   insert into #productrates select isnull(MAX(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,pt.OpeningStock as Stock
  From Product pt
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
 left join SalesEstimateMaster sem on ses.SalesInvId=sem.SalesInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  ses.Date <@Date
  group by pt.ProductId,pt.SellingPrice,pt.OpeningStock
  order by pt.ProductId

  else if @Validation='Last Selling Price'
  Begin
  create table #productlastprice
 (
  Rates decimal,
  ProductId int,
  )
    insert into #productlastprice select Top 1 ses.ProRate as Rates, pt.ProductId
  From Product pt
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
  left join SalesEstimateMaster sem on ses.SalesInvId=sem.SalesInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  sem.SalesDate <@Date
  group by pt.ProductId,ses.ProRate,ses.Date
  order by ses.Date desc

   insert into #productrates select isnull(ses.Rates,pt.SellingPrice) as Rates,pt.ProductId,pt.OpeningStock as Stock
  From Product pt
  left join #productlastprice ses on pt.ProductId = ses.ProductId
  end
  else if @Validation='Purchase Price'
  insert into #productrates select pt.PurchasePrice as Rates,pt.ProductId,pt.OpeningStock as Stock
  From Product pt

  else if @Validation='MRP'
  insert into #productrates select pt.MRP as Rates,pt.ProductId,pt.OpeningStock as Stock
  From Product pt  

  else if @Validation='Average Purchase Price'
   insert into #productrates select isnull(AVG(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,pt.OpeningStock as Stock
  From Product pt
 left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
 left join SalesEstimateMaster sem on ses.SalesInvId=sem.SalesInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  ses.Date <@Date
  group by pt.ProductId,pt.SellingPrice,pt.OpeningStock
  order by pt.ProductId

  else if @Validation='Highest Purchase Price'
   insert into #productrates select isnull(MAX(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,pt.OpeningStock as Stock
  From Product pt
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
 left join SalesEstimateMaster sem on ses.SalesInvId=sem.SalesInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  ses.Date <@Date
  group by pt.ProductId,pt.SellingPrice,pt.OpeningStock
  order by pt.ProductId

  else if @Validation='Last Purchase Price'
  Begin
  create table #productlastpricepurchase
 (
  Rates decimal,
  ProductId int,
  )
    insert into #productlastpricepurchase select Top 1 ses.ProRate as Rates, pt.ProductId
  From Product pt
  left join PurInvSettings ses on pt.ProductId = ses.ProId
  left join PurInvMaster sem on ses.PurInvId=sem.PurInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  sem.PurDate <@Date
  group by pt.ProductId,ses.ProRate,ses.Date
  order by ses.Date desc

   insert into #productrates select isnull(ses.Rates,pt.SellingPrice) as Rates,pt.ProductId,pt.OpeningStock as Stock
  From Product pt
  left join #productlastpricepurchase ses on pt.ProductId = ses.ProductId
  end
  select SUM(#productrates.Rates * #productrates.Stock) as OpeningStock
  from #productrates
END
GO
/****** Object:  StoredProcedure [dbo].[spBarcodeExist]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spBarcodeExist]
(
@Barcode nvarchar(20)
)

 AS 
 BEGIN 

 Select Count(Barcode) as brCount from ProductBarcode Where Barcode=@Barcode

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spCashClosePOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spCashClosePOS]
(
@FromDate datetime, @ToDate datetime,@UserId int=0
)
As
Begin
if @UserId=0

Select sem.SequenceNo as [Sequence], sem.Terminal,ur.UserName as [User], min(sem.SalesDate) as StartDate, Max(sem.SalesDate) as EndDate,SUM(isnull(sem.CashPayment,0)) as CashTotal,
SUM(isnull(sem.CardPayment,0)) as CardTotal, sum(isnull(sem.Credit,0)) as CreditTotal, sum(isnull(sem.ReturnAmount,0))  as ReturnTotal , 
Sum(isnull(sem.GrandDiscount,0)) as TotalAmount, Sum(isnull(sem.TotalTaxAmount,0)) as TaxAmount,Sum(isnull(sem.CashPayment,0)+isnull(sem.CardPayment,0)) as  PaymentTotal
From SalesEstimateMaster sem
left join [User] ur on sem.JEId = ur.UserId
Where sem.SalesDate between @FromDate and @ToDate
Group by sem.SequenceNo,sem.Terminal,ur.UserName
order by sem.SequenceNo

else

Select sem.SequenceNo as [Sequence], sem.Terminal,ur.UserName as [User],min(sem.SalesDate) as StartDate, Max(sem.SalesDate) as EndDate,SUM(isnull(sem.CashPayment,0)) as CashTotal,
SUM(isnull(sem.CardPayment,0)) as CardTotal, sum(isnull(sem.Credit,0)) as CreditTotal, sum(isnull(sem.ReturnAmount,0))  as ReturnTotal , 
Sum(isnull(sem.GrandDiscount,0)) as TotalAmount, Sum(isnull(sem.TotalTaxAmount,0)) as TaxAmount,Sum(isnull(sem.CashPayment,0)+isnull(sem.CardPayment,0)) as  PaymentTotal
From SalesEstimateMaster sem
left join [User] ur on sem.JEId = ur.UserId
Where sem.SalesDate between @FromDate and @ToDate and sem.JEId = @UserId
Group by sem.SequenceNo,sem.Terminal,ur.UserName
order by sem.SequenceNo
End
GO
/****** Object:  StoredProcedure [dbo].[spCashFlow]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spCashFlow]
(
@FromDate datetime, @ToDate datetime,@CashAccountId int
)
As
Begin
WITH tempDebitCredit AS (
	SELECT tl.TranId as id, tl.VchrDate,tl.VchrType,tl.VchrNo,act.AccountName,
	tl.DebitAmount,tl.CreditAmount,tl.DebitAmount - tl.CreditAmount 'diff'
	FROM TransactionLog tl
	Left Join Account act on tl.OppAccountId =act.AccountId
	Where tl.AccountId=@CashAccountId and tl.VchrDate between @FromDate and @ToDate
	Group by tl.TranId,tl.VchrDate,tl.VchrType,tl.VchrNo,act.AccountName,tl.DebitAmount,tl.CreditAmount
)
select  a.id,a.VchrDate,a.VchrType,a.VchrNo,a.AccountName,a.DebitAmount,a.CreditAmount,SUM(b.diff) 'Balance'
FROM   tempDebitCredit a,
       tempDebitCredit b
WHERE b.id <= a.id
GROUP BY a.VchrDate,a.VchrType,a.VchrNo,a.AccountName,a.DebitAmount, a.CreditAmount,a.id
order by a.id
End
GO
/****** Object:  StoredProcedure [dbo].[spCashPayment]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spCashPayment]
(
@FromDate datetime, 
@ToDate datetime,
@Terminal nvarchar(25),
@SequenceNo int,
@BranchId int =null
)
As
Begin
create table #OppAccount
 (
  accountId int,
  netValue decimal(24,4)
  )
insert into #OppAccount Select tl.OppAccountId as accountId,SUM(tl.CreditAmount) as netValue
From TransactionLog tl
Left Join Account act on tl.AccountId = act.AccountId and tl.OppAccountId <> act.AccountId
left Join AccountGroup agp on act.AGroupId= agp.AGroupId
Where (tl.FK_BranchId = @BranchId or @BranchId is null) and
tl.VchrDate between @FromDate and @ToDate and 
tl.Terminal = @Terminal and 
tl.SequenceNo=@SequenceNo 
and agp.GroupName ='CASH IN HAND' 
and tl.CreditAmount > 0
group by tl.OppAccountId

select att.AccountName,netValue as Amount From #OppAccount oat
inner join Account att on oat.accountId = att.AccountId

drop table #OppAccount
End
GO
/****** Object:  StoredProcedure [dbo].[spCashReceipt]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spCashReceipt]
(
@FromDate datetime, 
@ToDate datetime,
@Terminal nvarchar(25),
@SequenceNo int,
@BranchId int =null
)
As
Begin
create table #OppAccount
 (
  accountId int,
  netValue decimal(24,4)
  )
insert into #OppAccount Select tl.OppAccountId as accountId,SUM(tl.DebitAmount) as netValue
From TransactionLog tl
Left Join Account act on tl.AccountId = act.AccountId and tl.OppAccountId <> act.AccountId
left Join AccountGroup agp on act.AGroupId= agp.AGroupId
Where (tl.FK_BranchId = @BranchId or @BranchId is null) and
tl.VchrDate between @FromDate and @ToDate and 
tl.Terminal = @Terminal and 
tl.SequenceNo=@SequenceNo and
agp.GroupName ='CASH IN HAND' 
group by tl.OppAccountId

select att.AccountName,netValue as Amount From #OppAccount oat
inner join Account att on oat.accountId = att.AccountId

drop table #OppAccount
END

GO
/****** Object:  StoredProcedure [dbo].[spCustomerHistory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spCustomerHistory]
(
@NumberOfDecimalPlaces tinyint,
@CustomerId int=null,
@BranchId int=null
)
As
Begin
Select row_number() over( order by  ses.Barcode) as SL,  ses.Barcode,pt.ProductName as [Product Name],pb.ProductBarcodeName as [Size Name], STR(sum(ses.ProQty),CAST(sum(ses.ProQty) / 10 AS int) + @NumberOfDecimalPlaces + 2,
        COALESCE(@NumberOfDecimalPlaces,2)) as Quantity,convert(varchar(10), cast(MAX(ses.Date) as date), 103) as [Last Date]
from SalesEstimateSettings ses
Left Join SalesEstimateMaster sem on ses.SalesInvId= sem.SalesInvId
Left Join ProductBarcode pb on ses.Barcode=pb.Barcode
Left Join Product pt on ses.ProductId= pt.ProductId
Where sem.PartyAccountId=@CustomerId and
sem.FK_BranchId=@BranchId
Group By ses.Barcode,pb.ProductBarcodeName,pt.ProductName
End
GO
/****** Object:  StoredProcedure [dbo].[spCustomerItemSalesReportPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spCustomerItemSalesReportPOS]
(
@FromDate datetime, 
@ToDate datetime,
@AccountId int,
@BranchId int =null
)
As
Begin
Select sem.SalesDate as InvoiceDate, sem.SalesInvNo as InvoiceNo,pt.ProductId, pt.ProductName as ProductName,ses.Barcode as Barcode,
ses.ProQty as Quantity,ut.UnitName as Unitname,ses.DiscAmnt as DiscountAmount, ses.GSTAMOUNT as TaxAmount, ses.NetAmnt as NetAmount
From SalesEstimateSettings ses
inner Join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
Left join Product pt on ses.ProductId = pt.ProductId
Left join Unit ut on ses.UnitId = ut.UnitId
Where sem.PartyAccountId = @AccountId and 
(sem.FK_BranchId = @BranchId or @BranchId is null) and
sem.SalesDate between @FromDate and @ToDate
order by sem.SalesDate, pt.ProductId
End
GO
/****** Object:  StoredProcedure [dbo].[spCustomerLedgerReportPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spCustomerLedgerReportPOS]
(
@FromDate datetime, @ToDate datetime,@AccountId int
)
As
Begin
WITH tempDebitCredit AS (
SELECT '0' as id, @FromDate as 'VchrDate','Opening Balance' as VchrType,'1' as VchrNo,'Opening Balance' as AccountName,sum(isnull(tl.DebitAmount,0)) as DebitAmount ,sum(isnull(tl.CreditAmount,0)) as CreditAmount,sum(isnull(tl.DebitAmount,0)) - sum(isnull( tl.CreditAmount,0)) as diff
	FROM TransactionLog tl
	Left Join Account act on tl.OppAccountId =act.AccountId
	Where tl.AccountId=@AccountId and tl.VchrDate < @FromDate
	union
	SELECT tl.TranId as id, tl.VchrDate,tl.VchrType,tl.VchrNo,act.AccountName,tl.DebitAmount,tl.CreditAmount,tl.DebitAmount - tl.CreditAmount 'diff'
	FROM TransactionLog tl
	Left Join Account act on tl.OppAccountId =act.AccountId
	Where tl.AccountId=@AccountId and tl.VchrDate between @FromDate and @ToDate
	Group by tl.TranId,tl.VchrDate,tl.VchrType,tl.VchrNo,act.AccountName,tl.DebitAmount,tl.CreditAmount
	
)
select  a.id,a.VchrDate,a.VchrType,a.VchrNo,a.AccountName,a.DebitAmount,a.CreditAmount,SUM(b.diff) 'Balance'
FROM   tempDebitCredit a,
       tempDebitCredit b
WHERE b.id <= a.id
GROUP BY a.VchrDate,a.VchrType,a.VchrNo,a.AccountName,a.DebitAmount, a.CreditAmount,a.id
order by a.id
End
GO
/****** Object:  StoredProcedure [dbo].[spCustomerReportPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spCustomerReportPOS]
(
@CustomerId int=null,
@BranchId int = null

)
As
Begin
Select act.AccountName as CustomerName,act.AddressOne as Address, act.MobileNo as PhoneNumber,
act.DebitLimit as DebitLimit,Sum(isnull(tl.DebitAmount,0)-isnull(tl.CreditAmount,0)) as CurrentBalance
From Account act
left join TransactionLog tl on tl.AccountId = act.AccountId
where (act.AccountId = @CustomerId or @CustomerId is null) and 
(tl.FK_BranchId = @BranchId or @BranchId is null)
group by act.AccountName, act.AddressOne, act.MobileNo, act.DebitLimit
order by act.AccountName
End
GO
/****** Object:  StoredProcedure [dbo].[spCustomerSalesReportPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spCustomerSalesReportPOS]
(
@FromDate datetime, @ToDate datetime,@AccountId int
)
As
Begin 
Select SalesDate as InvoiceDate, SalesInvNo as InvoiceNo,TaxableAmount as Taxable,TotalTaxAmount as Tax, 
isnull(CashPayment,0)+isnull(CardPayment,0) as Payment, ReturnAmount as [Return], GrandDiscount as Net, 
isnull(GrandDiscount,0)-(isnull(CashPayment,0)+isnull(CardPayment,0)) as Balance
From SalesEstimateMaster 
Where PartyAccountId=@AccountId and SalesDate between @FromDate and @ToDate
order by SalesDate
End
GO
/****** Object:  StoredProcedure [dbo].[spDatabaseReset]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDatabaseReset]
(
@ResetType int=1,
@Terminal nvarchar=null
)
AS 
 BEGIN 
 if @ResetType = 1
 Begin
declare @sql nvarchar(max) = (
    select 
        'alter table ' + quotename(schema_name(schema_id)) + '.' +
        quotename(object_name(parent_object_id)) +
        ' drop constraint '+quotename(name) + ';'
    from sys.foreign_keys
    for xml path('')
);
exec sp_executesql @sql;

declare @command varchar(15)  
set @command = 'drop table ?'  
exec sp_msforeachtable @command
end
else if @ResetType = 2
begin 
Delete From GdwnStckTranLog Where VchrType <> 'Opening Entry'
Delete From StockTranLog Where VchrType <> 'Opening Entry'
Delete From TransactionLog where VchrType <> 'Opening Voucher'
Delete From VchrTranLog where VchrType <> 'Opening Voucher'
Delete From POSHoldSettings
Delete From POSHoldMaster
Delete From SalesEstimateSettings
Delete From SalesEstimateMaster
Delete From SlsRtnSettings
Delete From SalesRtnMaster
Delete From PurInvSettings
Delete From PurInvMaster
Delete From PurRtnInvSttings
Delete From PurRtnInvMaster
Delete From SalesOrderSettings
Delete From SalesOrderMaster
Delete From ManufacturingExp
Delete From ManufactureDtls
Delete From ManufactureHdr
Delete From RawmaterialMapping
Delete From ReceiptAndPayment
Delete From SlryPrcsSettings
Delete From SalaryProcessMaster
Delete From AttenSettings
Delete From AttenMaster
Delete From StockAdjustSettings
Delete From StockAdjusterMaster
Delete From StockTranSettings
Delete From StockTransferMaster
update MasterValue set Description='1' where MasterType='SN' and
(Name = @Terminal or @Terminal is null )
end
else if @ResetType = 3
begin
Delete From GdwnStckTranLog Where VchrType <> 'Opening Entry'
Delete From StockTranLog Where VchrType <> 'Opening Entry'
Delete From TransactionLog
Delete From VchrTranLog 
Delete From POSHoldSettings
Delete From POSHoldMaster
Delete From SalesEstimateSettings
Delete From SalesEstimateMaster
Delete From SlsRtnSettings
Delete From SalesRtnMaster
Delete From PurInvSettings
Delete From PurInvMaster
Delete From PurRtnInvSttings
Delete From PurRtnInvMaster
Delete From SalesOrderSettings
Delete From SalesOrderMaster
Delete From ManufacturingExp
Delete From ManufactureDtls
Delete From ManufactureHdr
Delete From RawmaterialMapping
Delete From ReceiptAndPayment
Delete From SlryPrcsSettings
Delete From SalaryProcessMaster
Delete From AttenSettings
Delete From AttenMaster
Delete From StockAdjustSettings
Delete From StockAdjusterMaster
Delete From StockTranSettings
Delete From StockTransferMaster
delete from Account where IsBuiltIn = 0 and AGroupId in(21,22)
update MasterValue set Description='1' where MasterType='SN' and
(Name = @Terminal or @Terminal is null )
end
else if @ResetType = 4
begin
Delete From GdwnStckTranLog Where VchrType = 'Opening Entry'
Delete From StockTranLog Where VchrType = 'Opening Entry'
Update Product set OpeningStock=0 
end
END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteAccount]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteAccount]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Account where AccountId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteAccountGroup]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteAccountGroup]
(
@Id int
)

 AS 
 BEGIN 

 Delete from AccountGroup where AGroupId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteAttenMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteAttenMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from AttenMaster where AttendanceMasterId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteAttenSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteAttenSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from AttenSettings where AttendanceMasterId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteBarcodeDTL]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteBarcodeDTL]
(
@Id int
)

 AS 
 BEGIN 

 Delete from BarcodeDTL where FK_BarcodeHDR= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteBarcodeHDR]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteBarcodeHDR]
(
@Id int
)

 AS 
 BEGIN 

 Delete from BarcodeHDR where Id= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteBarcodeScale]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteBarcodeScale]
(
@Id int
)

 AS 
 BEGIN 

 Delete from BarcodeScale where Id= @Id

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteBatch]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteBatch]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Batch where Id= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteBranch]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteBranch]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Branch where Id= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteBrand]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteBrand]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Brand where BrandId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteChequeDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteChequeDetails]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ChequeDetails where CheqDtlsId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteCostCenter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteCostCenter]
(
@Id int
)

 AS 
 BEGIN 

 Delete from CostCenter where CostCenterID= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteCulenderPlanning]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteCulenderPlanning]
(
@Id int
)

 AS 
 BEGIN 

 Delete from CulenderPlanning where CPlaningId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteCurrencyInfo]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteCurrencyInfo]
(
@Id int
)

 AS 
 BEGIN 

 Delete from CurrencyInfo where CurrencyID= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteCustomQuery]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteCustomQuery]
(
@Id int
)

 AS 
 BEGIN 

 Delete from CustomQuery where id= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteDebitCreditCardMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteDebitCreditCardMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from DebitCreditCardMaster where CardId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteEmployeeDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteEmployeeDetails]
(
@Id int
)

 AS 
 BEGIN 

 Delete from EmployeeDetails where EmpID= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteFinancialYear]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteFinancialYear]
(
@Id int
)

 AS 
 BEGIN 

 Delete from FinancialYear where FyearId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteGdwnStckTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteGdwnStckTranLog]
(
@Id int
)

 AS 
 BEGIN 

 Delete from GdwnStckTranLog where GSTranId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteGodown]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteGodown]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Godown where GodwnId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteInstitute]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteInstitute]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Institute where InstituteId= @Id

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteItemNote]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteItemNote]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ItemNote where Id= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteKOTOrderMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteKOTOrderMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from KOTOrderMaster where KOTMasterId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteKOTOrderSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteKOTOrderSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from KOTOrderSettings where FK_KOTMasterId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteKOTPrinter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteKOTPrinter]
(
@Id int
)

 AS 
 BEGIN 

 Delete from KOTPrinter where Id= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteLogged]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteLogged]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Logged where UserId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteManufactureDtls]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteManufactureDtls]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ManufactureDtls where FK_ManufactureHdrId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteManufactureHdr]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteManufactureHdr]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ManufactureHdr where Id= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteManufacturingExp]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteManufacturingExp]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ManufacturingExp where FK_ManufactureHdrId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteMasterValue]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteMasterValue]
(
@Id int
)

 AS 
 BEGIN 

 Delete from MasterValue where Id= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteMenuDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteMenuDetails]
(
@Id int
)

 AS 
 BEGIN 

 Delete from MenuDetails where Id= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteModules]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteModules]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Modules where Id= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeletePOSHoldMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeletePOSHoldMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from POSHoldMaster where Id= @Id

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spDeletePOSHoldSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeletePOSHoldSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from POSHoldSettings where SalesInvId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeletePrevilegeCard]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeletePrevilegeCard]
(
@Id int
)

 AS 
 BEGIN 

 Delete from PrevilegeCard where PrevilageCardId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeletePriceCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeletePriceCategory]
(
@Id int
)

 AS 
 BEGIN 

 Delete from PriceCategory where PCategoryId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeletePrintDesignMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeletePrintDesignMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from PrintDesignMaster where printMasterId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeletePrintDesignSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeletePrintDesignSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from PrintDesignSettings where printMasterId= @Id

 
 END
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteProCombMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteProCombMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ProCombMaster where ProCombMasterId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteProCombSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteProCombSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ProCombSettings where ProCombMasterId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteProduct]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteProduct]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Product where ProductId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteProductBarcode]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteProductBarcode]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ProductBarcode where Id= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteProductBarcodeScale]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteProductBarcodeScale]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ProductBarcodeScale where Id= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteProductGroup]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteProductGroup]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ProductGroup where PGroupId= @Id

 
 END

GO
/****** Object:  StoredProcedure [dbo].[spDeleteProductPriceCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteProductPriceCategory]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ProductPriceCategory where ProPrceCatId= @Id

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteProductPrinter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteProductPrinter]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ProductPrinter where Id= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteProductUnit]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteProductUnit]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ProductUnit where ProUnitId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeletePurInvMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeletePurInvMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from PurInvMaster where PurInvId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeletePurInvSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeletePurInvSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from PurInvSettings where PurInvId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeletePurRtnInvMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeletePurRtnInvMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from PurRtnInvMaster where PurRtnInvId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeletePurRtnInvSttings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeletePurRtnInvSttings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from PurRtnInvSttings where PurRtnInvId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteRawmaterialMapping]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteRawmaterialMapping]
(
@Id int
)

 AS 
 BEGIN 

 Delete from RawmaterialMapping where FK_ManufactureHdrId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteReceiptAndPayment]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteReceiptAndPayment]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ReceiptAndPayment where Id= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteReminderTB]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteReminderTB]
(
@Id int
)

 AS 
 BEGIN 

 Delete from ReminderTB where ReminderId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteRestaurantTable]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteRestaurantTable]
(
@Id int
)

 AS 
 BEGIN 

 Delete from RestaurantTable where TableId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteRoot]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteRoot]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Root where RootId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteSalaryProcessMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteSalaryProcessMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from SalaryProcessMaster where SPrcsMasterID= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteSalesCounter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteSalesCounter]
(
@Id int
)

 AS 
 BEGIN 

 Delete from SalesCounter where SalesCounterId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteSalesEstimateMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteSalesEstimateMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from SalesEstimateMaster where SalesInvId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteSalesEstimateSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteSalesEstimateSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from SalesEstimateSettings where SalesSettingsId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteSalesOrderMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteSalesOrderMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from SalesOrderMaster where SalesOrderMasterId= @Id

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteSalesOrderSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteSalesOrderSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from SalesOrderSettings where SalesOrderSettingsId= @Id

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteSalesRtnMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteSalesRtnMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from SalesRtnMaster where SlsRtrnId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteSelectedFYear]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteSelectedFYear]
(
@Id int
)

 AS 
 BEGIN 

 Delete from SelectedFYear where SlectedFY= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteSelf]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteSelf]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Self where SelfId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Settings where Id= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteSlryPrcsSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteSlryPrcsSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from SlryPrcsSettings where SprcsMasterID= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteSlsRtnSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteSlsRtnSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from SlsRtnSettings where SlsRtnId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteStockAdjusterMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteStockAdjusterMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from StockAdjusterMaster where stockAdjusterMasterId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteStockAdjustSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteStockAdjustSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from StockAdjustSettings where stockAdjusterMasterId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteStockTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteStockTranLog]
(
@Id int
)

 AS 
 BEGIN 

 Delete from StockTranLog where STranId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteStockTranSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteStockTranSettings]
(
@Id int
)

 AS 
 BEGIN 

 Delete from StockTranSettings where StockTransferId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteStockTransferMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteStockTransferMaster]
(
@Id int
)

 AS 
 BEGIN 

 Delete from StockTransferMaster where StockTransferId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteTaxCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteTaxCategory]
(
@Id int
)

 AS 
 BEGIN 

 Delete from TaxCategory where TCategoryId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteTransactionLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteTransactionLog]
(
@Id int
)

 AS 
 BEGIN 

 Delete from TransactionLog where TranId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteTranslator]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteTranslator]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Translator where Id= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteUnit]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteUnit]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Unit where UnitId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteUser]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteUser]
(
@Id int
)

 AS 
 BEGIN 

 Delete from [User] where UserId= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteUserPrevilegeDtl]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteUserPrevilegeDtl]
(
@Id int
)

 AS 
 BEGIN 

 Delete from UserPrevilegeDtl where Id= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteUserPrevilegeHdr]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteUserPrevilegeHdr]
(
@Id int
)

 AS 
 BEGIN 

 Delete from UserPrevilegeHdr where Id= @Id

 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spDeleteVchrTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spDeleteVchrTranLog]
(
@Id int
)

 AS 
 BEGIN 

 Delete from VchrTranLog where VchrTranId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spDeleteVehicle]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spDeleteVehicle]
(
@Id int
)

 AS 
 BEGIN 

 Delete from Vehicle where VanId= @Id

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetAccount]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetAccount]
(
@AccountId int=NULL, @AGroupId int=NULL, @RootId int=NULL, @PCategoryId int=NULL, @AccountName nvarchar(200)=NULL, @OpeningBalance decimal(18,4)=NULL, @DebitOrCridt nvarchar(5)=NULL, @AddressOne nvarchar(50)=NULL, @AddressTwo nvarchar(50)=NULL, @AddressThree nvarchar(50)=NULL, @PhoneOne nvarchar(20)=NULL, @MobileNo nvarchar(20)=NULL, @FaxNo nvarchar(20)=NULL, @TinNo nvarchar(20)=NULL, @CstNo nvarchar(20)=NULL, @EMailOne nvarchar(50)=NULL, @Remarks nvarchar(400)=NULL, @BankName nvarchar(50)=NULL, @BankAddress nvarchar(50)=NULL, @AccountNo nvarchar(50)=NULL, @isActive bit=NULL, @IsBuiltIn bit=NULL, @DebitLimit decimal(24,4)=NULL, @IsLimitApplicable bit=NULL, @AddressFour nvarchar(50)=NULL, @FieldOne nvarchar(50)=NULL, @FieldTwo nvarchar(50)=NULL, @FieldThree nvarchar(50)=NULL, @FieldFour nvarchar(50)=NULL 
)

 AS 
 BEGIN 

 Select 
AccountId, 
AGroupId, 
RootId, 
PCategoryId, 
AccountName, 
OpeningBalance, 
DebitOrCridt, 
AddressOne, 
AddressTwo, 
AddressThree, 
PhoneOne, 
MobileNo, 
FaxNo, 
TinNo, 
CstNo, 
EMailOne, 
Remarks, 
BankName, 
BankAddress, 
AccountNo, 
isActive, 
IsBuiltIn, 
DebitLimit, 
IsLimitApplicable, 
AddressFour, 
FieldOne, 
FieldTwo, 
FieldThree, 
FieldFour
 from Account where 
(AccountId = @AccountId or @AccountId is null ) And 
(AGroupId = @AGroupId or @AGroupId is null ) And 
(RootId = @RootId or @RootId is null ) And 
(PCategoryId = @PCategoryId or @PCategoryId is null ) And 
(AccountName = @AccountName or @AccountName is null ) And 
(OpeningBalance = @OpeningBalance or @OpeningBalance is null ) And 
(DebitOrCridt = @DebitOrCridt or @DebitOrCridt is null ) And 
(AddressOne = @AddressOne or @AddressOne is null ) And 
(AddressTwo = @AddressTwo or @AddressTwo is null ) And 
(AddressThree = @AddressThree or @AddressThree is null ) And 
(PhoneOne = @PhoneOne or @PhoneOne is null ) And 
(MobileNo = @MobileNo or @MobileNo is null ) And 
(FaxNo = @FaxNo or @FaxNo is null ) And 
(TinNo = @TinNo or @TinNo is null ) And 
(CstNo = @CstNo or @CstNo is null ) And 
(EMailOne = @EMailOne or @EMailOne is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(BankName = @BankName or @BankName is null ) And 
(BankAddress = @BankAddress or @BankAddress is null ) And 
(AccountNo = @AccountNo or @AccountNo is null ) And 
(isActive = @isActive or @isActive is null ) And 
(IsBuiltIn = @IsBuiltIn or @IsBuiltIn is null ) And 
(DebitLimit = @DebitLimit or @DebitLimit is null ) And 
(IsLimitApplicable = @IsLimitApplicable or @IsLimitApplicable is null ) And 
(AddressFour = @AddressFour or @AddressFour is null ) And 
(FieldOne = @FieldOne or @FieldOne is null ) And 
(FieldTwo = @FieldTwo or @FieldTwo is null ) And 
(FieldThree = @FieldThree or @FieldThree is null ) And 
(FieldFour = @FieldFour or @FieldFour is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetAccountBalance]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetAccountBalance]
(
@AccountId int = null,
@VchrDate nvarchar(100)
)
As
Begin
Select SUM(DebitAmount-CreditAmount) as Balance 
From TransactionLog 
Where (AccountId = @AccountId or @AccountId is null) 
and VchrDate <= @VchrDate 
End
GO
/****** Object:  StoredProcedure [dbo].[spGetAccountGroup]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetAccountGroup]
(
@AGroupId int=NULL, @GroupName nvarchar(50)=NULL, @Under nvarchar(50)=NULL, @Remarks nvarchar(400)=NULL, @isBuilIn bit=NULL 
)

 AS 
 BEGIN 

 Select 
AGroupId, 
GroupName, 
Under, 
Remarks, 
isBuilIn
 from AccountGroup where 
(AGroupId = @AGroupId or @AGroupId is null ) And 
(GroupName = @GroupName or @GroupName is null ) And 
(Under = @Under or @Under is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(isBuilIn = @isBuilIn or @isBuilIn is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetAttenMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetAttenMaster]
(
@AttendanceMasterId int=NULL, @AtteInvNo int=NULL, @AtteDate datetime=NULL, @Remarks nvarchar(400)=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
AttendanceMasterId, 
AtteInvNo, 
AtteDate, 
Remarks, 
FK_BranchId
 from AttenMaster where 
(AttendanceMasterId = @AttendanceMasterId or @AttendanceMasterId is null ) And 
(AtteInvNo = @AtteInvNo or @AtteInvNo is null ) And 
(AtteDate = @AtteDate or @AtteDate is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetAttenSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetAttenSettings]
(
@AttenSettingsId int=NULL, @AttendanceMasterId int=NULL, @EmpID int=NULL, @Morning bit=NULL, @Evening bit=NULL, @AttenType decimal(18,1)=NULL, @EmpName nvarchar(50)=NULL, @AtteDate datetime=NULL 
)

 AS 
 BEGIN 

 Select 
AttenSettingsId, 
AttendanceMasterId, 
EmpID, 
Morning, 
Evening, 
AttenType, 
EmpName, 
AtteDate
 from AttenSettings where 
(AttenSettingsId = @AttenSettingsId or @AttenSettingsId is null ) And 
(AttendanceMasterId = @AttendanceMasterId or @AttendanceMasterId is null ) And 
(EmpID = @EmpID or @EmpID is null ) And 
(Morning = @Morning or @Morning is null ) And 
(Evening = @Evening or @Evening is null ) And 
(AttenType = @AttenType or @AttenType is null ) And 
(EmpName = @EmpName or @EmpName is null ) And 
(AtteDate = @AtteDate or @AtteDate is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetBarcodeDTL]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetBarcodeDTL]
(
@id int=NULL, @Name nvarchar(100)=NULL, @Text nvarchar(100)=NULL, @Value nvarchar(100)=NULL, @isIncluded bit=NULL, @Position_x int=NULL, @Position_y int=NULL, @Width int=NULL, @Height int=NULL, @Alignment nvarchar(50)=NULL, @RotationPoint nvarchar(50)=NULL, @fontFamily nvarchar(50)=NULL, @fontSize int=NULL, @fStyle nvarchar(50)=NULL, @Format nvarchar(50)=NULL, @bAlignment nvarchar(50)=NULL, @FK_BarcodeHDR int=NULL, @Type nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
id, 
Name, 
Text, 
Value, 
isIncluded, 
Position_x, 
Position_y, 
Width, 
Height, 
Alignment, 
RotationPoint, 
fontFamily, 
fontSize, 
fStyle, 
Format, 
bAlignment, 
FK_BarcodeHDR, 
Type
 from BarcodeDTL where 
(id = @id or @id is null ) And 
(Name = @Name or @Name is null ) And 
(Text = @Text or @Text is null ) And 
(Value = @Value or @Value is null ) And 
(isIncluded = @isIncluded or @isIncluded is null ) And 
(Position_x = @Position_x or @Position_x is null ) And 
(Position_y = @Position_y or @Position_y is null ) And 
(Width = @Width or @Width is null ) And 
(Height = @Height or @Height is null ) And 
(Alignment = @Alignment or @Alignment is null ) And 
(RotationPoint = @RotationPoint or @RotationPoint is null ) And 
(fontFamily = @fontFamily or @fontFamily is null ) And 
(fontSize = @fontSize or @fontSize is null ) And 
(fStyle = @fStyle or @fStyle is null ) And 
(Format = @Format or @Format is null ) And 
(bAlignment = @bAlignment or @bAlignment is null ) And 
(FK_BarcodeHDR = @FK_BarcodeHDR or @FK_BarcodeHDR is null ) And 
(Type = @Type or @Type is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetBarcodeHDR]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetBarcodeHDR]
(
@Id int=NULL, @name nvarchar(50)=NULL, @FirstMargin int=NULL, @TopMargin int=NULL, @RightMargin int=NULL, @BottomMargin int=NULL, @Width int=NULL, @Height int=NULL, @Accross int=NULL, @Down int=NULL, @TotalPerPage int=NULL, @PrinterName nvarchar(50)=NULL, @PaperName nvarchar(50)=NULL, @PSWidth int=NULL, @PSHieght int=NULL, @isBorderIncluded bit=NULL, @isCuttingEdge bit=NULL, @BarcodeField int=NULL, @isBarcode bit=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
name, 
FirstMargin, 
TopMargin, 
RightMargin, 
BottomMargin, 
Width, 
Height, 
Accross, 
Down, 
TotalPerPage, 
PrinterName, 
PaperName, 
PSWidth, 
PSHieght, 
isBorderIncluded, 
isCuttingEdge, 
BarcodeField, 
isBarcode
 from BarcodeHDR where 
(Id = @Id or @Id is null ) And 
(name = @name or @name is null ) And 
(FirstMargin = @FirstMargin or @FirstMargin is null ) And 
(TopMargin = @TopMargin or @TopMargin is null ) And 
(RightMargin = @RightMargin or @RightMargin is null ) And 
(BottomMargin = @BottomMargin or @BottomMargin is null ) And 
(Width = @Width or @Width is null ) And 
(Height = @Height or @Height is null ) And 
(Accross = @Accross or @Accross is null ) And 
(Down = @Down or @Down is null ) And 
(TotalPerPage = @TotalPerPage or @TotalPerPage is null ) And 
(PrinterName = @PrinterName or @PrinterName is null ) And 
(PaperName = @PaperName or @PaperName is null ) And 
(PSWidth = @PSWidth or @PSWidth is null ) And 
(PSHieght = @PSHieght or @PSHieght is null ) And 
(isBorderIncluded = @isBorderIncluded or @isBorderIncluded is null ) And 
(isCuttingEdge = @isCuttingEdge or @isCuttingEdge is null ) And 
(BarcodeField = @BarcodeField or @BarcodeField is null ) And 
(isBarcode = @isBarcode or @isBarcode is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetBarcodeScale]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetBarcodeScale]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @IPAddress nvarchar(100)=NULL, @Remarks nvarchar(400)=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Name, 
IPAddress, 
Remarks
 from BarcodeScale where 
(Id = @Id or @Id is null ) And 
(Name = @Name or @Name is null ) And 
(IPAddress = @IPAddress or @IPAddress is null ) And 
(Remarks = @Remarks or @Remarks is null ) 

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spGetBatch]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetBatch]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @FK_PurInvId int=NULL, @Remarks nvarchar(100)=NULL, @FK_BranchId int=NULL, @RefNo nvarchar(100)=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Name, 
FK_PurInvId, 
Remarks, 
FK_BranchId, 
RefNo
 from Batch where 
(Id = @Id or @Id is null ) And 
(Name = @Name or @Name is null ) And 
(FK_PurInvId = @FK_PurInvId or @FK_PurInvId is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(RefNo = @RefNo or @RefNo is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetBranch]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetBranch]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @Description nvarchar(500)=NULL, @Address nvarchar(400)=NULL, @City nvarchar(100)=NULL, @State nvarchar(100)=NULL, @Country nvarchar(100)=NULL, @PostalCode nvarchar(100)=NULL, @PhoneNumber nvarchar(100)=NULL, @MobileNumber nvarchar(100)=NULL, @ContactPerson nvarchar(100)=NULL, @Status nvarchar(100)=NULL, @Active bit=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Name, 
Description, 
Address, 
City, 
State, 
Country, 
PostalCode, 
PhoneNumber, 
MobileNumber, 
ContactPerson, 
Status, 
Active
 from Branch where 
(Id = @Id or @Id is null ) And 
(Name = @Name or @Name is null ) And 
(Description = @Description or @Description is null ) And 
(Address = @Address or @Address is null ) And 
(City = @City or @City is null ) And 
(State = @State or @State is null ) And 
(Country = @Country or @Country is null ) And 
(PostalCode = @PostalCode or @PostalCode is null ) And 
(PhoneNumber = @PhoneNumber or @PhoneNumber is null ) And 
(MobileNumber = @MobileNumber or @MobileNumber is null ) And 
(ContactPerson = @ContactPerson or @ContactPerson is null ) And 
(Status = @Status or @Status is null ) And 
(Active = @Active or @Active is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetBrand]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetBrand]
(
@BrandId int=NULL, @BrandName nvarchar(50)=NULL, @Description nvarchar(400)=NULL 
)

 AS 
 BEGIN 

 Select 
BrandId, 
BrandName, 
Description
 from Brand where 
(BrandId = @BrandId or @BrandId is null ) And 
(BrandName = @BrandName or @BrandName is null ) And 
(Description = @Description or @Description is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetChequeDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetChequeDetails]
(
@CheqDtlsId int=NULL, @VchrId int=NULL, @VchrType nvarchar(50)=NULL, @AccountId int=NULL, @BankAcctId int=NULL, @Amount decimal(24,4)=NULL, @Bank nvarchar(200)=NULL, @Branch nvarchar(500)=NULL, @CheqNo nvarchar(25)=NULL, @CheqDate nvarchar(20)=NULL, @Status nvarchar(10)=NULL, @RefNo nvarchar(20)=NULL, @Remarks nvarchar(500)=NULL, @TranDate datetime=NULL, @JeId int=NULL, @Type nvarchar(50)=NULL, @CollectedDate datetime=NULL, @Date datetime=NULL, @ChqRtnAmount decimal(24,4)=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
CheqDtlsId, 
VchrId, 
VchrType, 
AccountId, 
BankAcctId, 
Amount, 
Bank, 
Branch, 
CheqNo, 
CheqDate, 
Status, 
RefNo, 
Remarks, 
TranDate, 
JeId, 
Type, 
CollectedDate, 
Date, 
ChqRtnAmount, 
FK_BranchId
 from ChequeDetails where 
(CheqDtlsId = @CheqDtlsId or @CheqDtlsId is null ) And 
(VchrId = @VchrId or @VchrId is null ) And 
(VchrType = @VchrType or @VchrType is null ) And 
(AccountId = @AccountId or @AccountId is null ) And 
(BankAcctId = @BankAcctId or @BankAcctId is null ) And 
(Amount = @Amount or @Amount is null ) And 
(Bank = @Bank or @Bank is null ) And 
(Branch = @Branch or @Branch is null ) And 
(CheqNo = @CheqNo or @CheqNo is null ) And 
(CheqDate = @CheqDate or @CheqDate is null ) And 
(Status = @Status or @Status is null ) And 
(RefNo = @RefNo or @RefNo is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(TranDate = @TranDate or @TranDate is null ) And 
(JeId = @JeId or @JeId is null ) And 
(Type = @Type or @Type is null ) And 
(CollectedDate = @CollectedDate or @CollectedDate is null ) And 
(Date = @Date or @Date is null ) And 
(ChqRtnAmount = @ChqRtnAmount or @ChqRtnAmount is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetCostCenter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetCostCenter]
(
@CostCenterID int=NULL, @CostCenterName nvarchar(50)=NULL, @Description nvarchar(400)=NULL 
)

 AS 
 BEGIN 

 Select 
CostCenterID, 
CostCenterName, 
Description
 from CostCenter where 
(CostCenterID = @CostCenterID or @CostCenterID is null ) And 
(CostCenterName = @CostCenterName or @CostCenterName is null ) And 
(Description = @Description or @Description is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetCulenderPlanning]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetCulenderPlanning]
(
@CPlaningId int=NULL, @CplanninDate datetime=NULL, @Description image=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
CPlaningId, 
CplanninDate, 
Description, 
FK_BranchId
 from CulenderPlanning where 
(CPlaningId = @CPlaningId or @CPlaningId is null ) And 
(CplanninDate = @CplanninDate or @CplanninDate is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetCurrencyInfo]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetCurrencyInfo]
(
@CurrencyID int=NULL, @CurrencyCode nvarchar(10)=NULL, @IsCurrencyNameFeminine bit=NULL, @EnglishCurrencyName nvarchar(100)=NULL, @EnglishPluralCurrencyName nvarchar(100)=NULL, @EnglishCurrencyPartName nvarchar(100)=NULL, @EnglishPluralCurrencyPartName nvarchar(100)=NULL, @Arabic1CurrencyName nvarchar(100)=NULL, @Arabic2CurrencyName nvarchar(100)=NULL, @Arabic310CurrencyName nvarchar(100)=NULL, @Arabic1199CurrencyName nvarchar(100)=NULL, @Arabic1CurrencyPartName nvarchar(100)=NULL, @Arabic2CurrencyPartName nvarchar(100)=NULL, @Arabic310CurrencyPartName nvarchar(100)=NULL, @Arabic1199CurrencyPartName nvarchar(100)=NULL, @PartPrecision int=NULL, @IsCurrencyPartNameFeminine bit=NULL 
)

 AS 
 BEGIN 

 Select 
CurrencyID, 
CurrencyCode, 
IsCurrencyNameFeminine, 
EnglishCurrencyName, 
EnglishPluralCurrencyName, 
EnglishCurrencyPartName, 
EnglishPluralCurrencyPartName, 
Arabic1CurrencyName, 
Arabic2CurrencyName, 
Arabic310CurrencyName, 
Arabic1199CurrencyName, 
Arabic1CurrencyPartName, 
Arabic2CurrencyPartName, 
Arabic310CurrencyPartName, 
Arabic1199CurrencyPartName, 
PartPrecision, 
IsCurrencyPartNameFeminine
 from CurrencyInfo where 
(CurrencyID = @CurrencyID or @CurrencyID is null ) And 
(CurrencyCode = @CurrencyCode or @CurrencyCode is null ) And 
(IsCurrencyNameFeminine = @IsCurrencyNameFeminine or @IsCurrencyNameFeminine is null ) And 
(EnglishCurrencyName = @EnglishCurrencyName or @EnglishCurrencyName is null ) And 
(EnglishPluralCurrencyName = @EnglishPluralCurrencyName or @EnglishPluralCurrencyName is null ) And 
(EnglishCurrencyPartName = @EnglishCurrencyPartName or @EnglishCurrencyPartName is null ) And 
(EnglishPluralCurrencyPartName = @EnglishPluralCurrencyPartName or @EnglishPluralCurrencyPartName is null ) And 
(Arabic1CurrencyName = @Arabic1CurrencyName or @Arabic1CurrencyName is null ) And 
(Arabic2CurrencyName = @Arabic2CurrencyName or @Arabic2CurrencyName is null ) And 
(Arabic310CurrencyName = @Arabic310CurrencyName or @Arabic310CurrencyName is null ) And 
(Arabic1199CurrencyName = @Arabic1199CurrencyName or @Arabic1199CurrencyName is null ) And 
(Arabic1CurrencyPartName = @Arabic1CurrencyPartName or @Arabic1CurrencyPartName is null ) And 
(Arabic2CurrencyPartName = @Arabic2CurrencyPartName or @Arabic2CurrencyPartName is null ) And 
(Arabic310CurrencyPartName = @Arabic310CurrencyPartName or @Arabic310CurrencyPartName is null ) And 
(Arabic1199CurrencyPartName = @Arabic1199CurrencyPartName or @Arabic1199CurrencyPartName is null ) And 
(PartPrecision = @PartPrecision or @PartPrecision is null ) And 
(IsCurrencyPartNameFeminine = @IsCurrencyPartNameFeminine or @IsCurrencyPartNameFeminine is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetCustomQuery]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetCustomQuery]
(
@id int=NULL, @CustmQryName nvarchar(100)=NULL, @Query nvarchar(1000)=NULL 
)

 AS 
 BEGIN 

 Select 
id, 
CustmQryName, 
Query
 from CustomQuery where 
(id = @id or @id is null ) And 
(CustmQryName = @CustmQryName or @CustmQryName is null ) And 
(Query = @Query or @Query is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetDebitCreditCardMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetDebitCreditCardMaster]
(
@CardId int=NULL, @CardName nvarchar(50)=NULL, @BankComm decimal(24,4)=NULL, @CardComm decimal(24,4)=NULL, @Remarks nvarchar(400)=NULL 
)

 AS 
 BEGIN 

 Select 
CardId, 
CardName, 
BankComm, 
CardComm, 
Remarks
 from DebitCreditCardMaster where 
(CardId = @CardId or @CardId is null ) And 
(CardName = @CardName or @CardName is null ) And 
(BankComm = @BankComm or @BankComm is null ) And 
(CardComm = @CardComm or @CardComm is null ) And 
(Remarks = @Remarks or @Remarks is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetEmployeeDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetEmployeeDetails]
(
@EmpID int=NULL, @EmployeeID int=NULL, @EmpName nvarchar(400)=NULL, @AddressOne nvarchar(400)=NULL, @AddressTwo nvarchar(400)=NULL, @AddressThree nvarchar(400)=NULL, @PhNumber nvarchar(20)=NULL, @EmailId nvarchar(50)=NULL, @Designation nvarchar(50)=NULL, @Salary decimal(24,4)=NULL, @CasualLeave decimal(24,4)=NULL, @Gender nvarchar(10)=NULL, @Qualification nvarchar(400)=NULL, @ParentName nvarchar(50)=NULL, @Relation nvarchar(50)=NULL, @ParentAddress nvarchar(400)=NULL, @ParentPhNo nvarchar(20)=NULL, @Remarks nvarchar(400)=NULL, @isActive bit=NULL, @AdharNo nvarchar(400)=NULL, @SalaryAccntId int=NULL, @SalaryAdvAccntId int=NULL, @SalaryPayableId int=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
EmpID, 
EmployeeID, 
EmpName, 
AddressOne, 
AddressTwo, 
AddressThree, 
PhNumber, 
EmailId, 
Designation, 
Salary, 
CasualLeave, 
Gender, 
Qualification, 
ParentName, 
Relation, 
ParentAddress, 
ParentPhNo, 
Remarks, 
isActive, 
AdharNo, 
SalaryAccntId, 
SalaryAdvAccntId, 
SalaryPayableId, 
FK_BranchId
 from EmployeeDetails where 
(EmpID = @EmpID or @EmpID is null ) And 
(EmployeeID = @EmployeeID or @EmployeeID is null ) And 
(EmpName = @EmpName or @EmpName is null ) And 
(AddressOne = @AddressOne or @AddressOne is null ) And 
(AddressTwo = @AddressTwo or @AddressTwo is null ) And 
(AddressThree = @AddressThree or @AddressThree is null ) And 
(PhNumber = @PhNumber or @PhNumber is null ) And 
(EmailId = @EmailId or @EmailId is null ) And 
(Designation = @Designation or @Designation is null ) And 
(Salary = @Salary or @Salary is null ) And 
(CasualLeave = @CasualLeave or @CasualLeave is null ) And 
(Gender = @Gender or @Gender is null ) And 
(Qualification = @Qualification or @Qualification is null ) And 
(ParentName = @ParentName or @ParentName is null ) And 
(Relation = @Relation or @Relation is null ) And 
(ParentAddress = @ParentAddress or @ParentAddress is null ) And 
(ParentPhNo = @ParentPhNo or @ParentPhNo is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(isActive = @isActive or @isActive is null ) And 
(AdharNo = @AdharNo or @AdharNo is null ) And 
(SalaryAccntId = @SalaryAccntId or @SalaryAccntId is null ) And 
(SalaryAdvAccntId = @SalaryAdvAccntId or @SalaryAdvAccntId is null ) And 
(SalaryPayableId = @SalaryPayableId or @SalaryPayableId is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetFavoriteProducts]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetFavoriteProducts]
As
Begin
declare @PriceCategoryId int = null
declare @IsFavorite bit ='True'
select @PriceCategoryId = min(PCategoryId) from PriceCategory 
select pct.ProductId,ProductName,image,pct.Barcode,ppc.SPrice as Rate
From Product pct
left join ProductPriceCategory ppc on pct.Barcode = ppc.Barcode
Where 
pct.IsFavorite = @IsFavorite and
ppc.PCategoryId = @PriceCategoryId
order by ListOrder desc
End

GO
/****** Object:  StoredProcedure [dbo].[spGetFinancialYear]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetFinancialYear]
(
@FyearId int=NULL, @FyearName nvarchar(100)=NULL, @DateFrom nvarchar(50)=NULL, @DateTo nvarchar(50)=NULL, @Old bit=NULL, @Closed bit=NULL 
)

 AS 
 BEGIN 

 Select 
FyearId, 
FyearName, 
DateFrom, 
DateTo, 
Old, 
Closed
 from FinancialYear where 
(FyearId = @FyearId or @FyearId is null ) And 
(FyearName = @FyearName or @FyearName is null ) And 
(DateFrom = @DateFrom or @DateFrom is null ) And 
(DateTo = @DateTo or @DateTo is null ) And 
(Old = @Old or @Old is null ) And 
(Closed = @Closed or @Closed is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetGdwnStckTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetGdwnStckTranLog]
(
@GSTranId int=NULL, @GodwnId int=NULL, @GodownName nvarchar(100)=NULL, @ProductId int=NULL, @ProductName nvarchar(100)=NULL, @VchrId int=NULL, @VchrNo int=NULL, @VchrDate datetime=NULL, @VchrType nvarchar(100)=NULL, @InQuantity decimal(24,4)=NULL, @OutQuantity decimal(24,4)=NULL, @UnitId int=NULL, @Narration nvarchar(400)=NULL, @UserId int=NULL, @TranDate datetime=NULL, @FK_BatchId int=NULL, @FK_BranchId int=NULL, @Barcode nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
GSTranId, 
GodwnId, 
GodownName, 
ProductId, 
ProductName, 
VchrId, 
VchrNo, 
VchrDate, 
VchrType, 
InQuantity, 
OutQuantity, 
UnitId, 
Narration, 
UserId, 
TranDate, 
FK_BatchId, 
FK_BranchId, 
Barcode
 from GdwnStckTranLog where 
(GSTranId = @GSTranId or @GSTranId is null ) And 
(GodwnId = @GodwnId or @GodwnId is null ) And 
(GodownName = @GodownName or @GodownName is null ) And 
(ProductId = @ProductId or @ProductId is null ) And 
(ProductName = @ProductName or @ProductName is null ) And 
(VchrId = @VchrId or @VchrId is null ) And 
(VchrNo = @VchrNo or @VchrNo is null ) And 
(VchrDate = @VchrDate or @VchrDate is null ) And 
(VchrType = @VchrType or @VchrType is null ) And 
(InQuantity = @InQuantity or @InQuantity is null ) And 
(OutQuantity = @OutQuantity or @OutQuantity is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(Narration = @Narration or @Narration is null ) And 
(UserId = @UserId or @UserId is null ) And 
(TranDate = @TranDate or @TranDate is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(Barcode = @Barcode or @Barcode is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetGodown]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetGodown]
(
@GodwnId int=NULL, @GodownName nvarchar(50)=NULL, @AddressOne nvarchar(50)=NULL, @AddressTwo nvarchar(50)=NULL, @AddressThree nvarchar(50)=NULL, @Phone nvarchar(50)=NULL, @Description nvarchar(400)=NULL 
)

 AS 
 BEGIN 

 Select 
GodwnId, 
GodownName, 
AddressOne, 
AddressTwo, 
AddressThree, 
Phone, 
Description
 from Godown where 
(GodwnId = @GodwnId or @GodwnId is null ) And 
(GodownName = @GodownName or @GodownName is null ) And 
(AddressOne = @AddressOne or @AddressOne is null ) And 
(AddressTwo = @AddressTwo or @AddressTwo is null ) And 
(AddressThree = @AddressThree or @AddressThree is null ) And 
(Phone = @Phone or @Phone is null ) And 
(Description = @Description or @Description is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetGroupWiseStockValue]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetGroupWiseStockValue]
(
@Date datetime=null
)
AS 
 BEGIN 

 create table #proGroup
 (
  PGroup nvarchar(200),
  StockValue decimal
  )

 insert into #proGroup Select pg.PGroupName, isnull(sum(stl.InQuantity-stl.OutQuantity),0) * isnull(pd.SellingPrice,0) as StockValue
 From StockTranLog stl
 Join Product pd on stl.ProductId= pd.ProductId
 Join ProductGroup pg on pd.PGroupId= pg.PGroupId
 where stl.TranDate <= @Date
 Group by pg.PGroupName,pd.SellingPrice
 
 select distinct PGroup, sum(StockValue) From #proGroup
 Group by PGroup
 drop table #proGroup
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spGetInstitute]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetInstitute]
(
@InstituteId int=NULL, @InstituteName nvarchar(100)=NULL, @Place nvarchar(50)=NULL, @Address nvarchar(250)=NULL, @Country nvarchar(50)=NULL, @Pincod nvarchar(50)=NULL, @PhoneNo nvarchar(50)=NULL, @MobNo nvarchar(50)=NULL, @FaxNo nvarchar(50)=NULL, @website nvarchar(50)=NULL, @EmailId nvarchar(50)=NULL, @Sector nvarchar(50)=NULL, @Logo image=NULL, @LogoPath nvarchar(100)=NULL, @Productserial nvarchar(50)=NULL, @GSTNo nvarchar(50)=NULL, @State nvarchar(50)=NULL, @StateCode int=NULL, @BankName nvarchar(50)=NULL, @BankBranch nvarchar(50)=NULL, @IFSC nvarchar(50)=NULL, @AccNo nvarchar(50)=NULL, @LocalName nvarchar(100)=NULL, @CompanyId nvarchar(20)=NULL 
)

 AS 
 BEGIN 

 Select 
InstituteId, 
InstituteName, 
Place, 
Address, 
Country, 
Pincod, 
PhoneNo, 
MobNo, 
FaxNo, 
website, 
EmailId, 
Sector, 
Logo, 
LogoPath, 
Productserial, 
GSTNo, 
State, 
StateCode, 
BankName, 
BankBranch, 
IFSC, 
AccNo, 
LocalName, 
CompanyId
 from Institute where 
(InstituteId = @InstituteId or @InstituteId is null ) And 
(InstituteName = @InstituteName or @InstituteName is null ) And 
(Place = @Place or @Place is null ) And 
(Address = @Address or @Address is null ) And 
(Country = @Country or @Country is null ) And 
(Pincod = @Pincod or @Pincod is null ) And 
(PhoneNo = @PhoneNo or @PhoneNo is null ) And 
(MobNo = @MobNo or @MobNo is null ) And 
(FaxNo = @FaxNo or @FaxNo is null ) And 
(website = @website or @website is null ) And 
(EmailId = @EmailId or @EmailId is null ) And 
(Sector = @Sector or @Sector is null ) And 
(LogoPath = @LogoPath or @LogoPath is null ) And 
(Productserial = @Productserial or @Productserial is null ) And 
(GSTNo = @GSTNo or @GSTNo is null ) And 
(State = @State or @State is null ) And 
(StateCode = @StateCode or @StateCode is null ) And 
(BankName = @BankName or @BankName is null ) And 
(BankBranch = @BankBranch or @BankBranch is null ) And 
(IFSC = @IFSC or @IFSC is null ) And 
(AccNo = @AccNo or @AccNo is null ) And 
(LocalName = @LocalName or @LocalName is null ) And 
(CompanyId = @CompanyId or @CompanyId is null ) 

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spGetItemNote]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetItemNote]
(
@Id int=NULL, @Note nvarchar(200)=NULL, @IsMain bit=NULL, @FieldOne nvarchar(100)=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Note, 
IsMain, 
FieldOne
 from ItemNote where 
(Id = @Id or @Id is null ) And 
(Note = @Note or @Note is null ) And 
(IsMain = @IsMain or @IsMain is null ) And 
(FieldOne = @FieldOne or @FieldOne is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetKOTOrderMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetKOTOrderMaster]
(
@KOTMasterId int=NULL, @TockenNo int=NULL, @DateAndTime datetime=NULL, @CookingTime decimal(24,4)=NULL, @Terminal nvarchar(100)=NULL, @SerialNo int=NULL, @FK_FYearId int=NULL, @FK_CustomerId int=NULL, @FK_UserId int=NULL, @FK_StaffId int=NULL, @TableId int=NULL, @Remarks nvarchar(400)=NULL, @Notes nvarchar(600)=NULL, @FK_KOTPrinterId int=NULL, @OrderType nvarchar(100)=NULL, @TotalQuantity decimal(24,4)=NULL, @TotalAmount decimal(24,4)=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
KOTMasterId, 
TockenNo, 
DateAndTime, 
CookingTime, 
Terminal, 
SerialNo, 
FK_FYearId, 
FK_CustomerId, 
FK_UserId, 
FK_StaffId, 
TableId, 
Remarks, 
Notes, 
FK_KOTPrinterId, 
OrderType, 
TotalQuantity, 
TotalAmount, 
FK_BranchId
 from KOTOrderMaster where 
(KOTMasterId = @KOTMasterId or @KOTMasterId is null ) And 
(TockenNo = @TockenNo or @TockenNo is null ) And 
(DateAndTime = @DateAndTime or @DateAndTime is null ) And 
(CookingTime = @CookingTime or @CookingTime is null ) And 
(Terminal = @Terminal or @Terminal is null ) And 
(SerialNo = @SerialNo or @SerialNo is null ) And 
(FK_FYearId = @FK_FYearId or @FK_FYearId is null ) And 
(FK_CustomerId = @FK_CustomerId or @FK_CustomerId is null ) And 
(FK_UserId = @FK_UserId or @FK_UserId is null ) And 
(FK_StaffId = @FK_StaffId or @FK_StaffId is null ) And 
(TableId = @TableId or @TableId is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(Notes = @Notes or @Notes is null ) And 
(FK_KOTPrinterId = @FK_KOTPrinterId or @FK_KOTPrinterId is null ) And 
(OrderType = @OrderType or @OrderType is null ) And 
(TotalQuantity = @TotalQuantity or @TotalQuantity is null ) And 
(TotalAmount = @TotalAmount or @TotalAmount is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetKOTOrderSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetKOTOrderSettings]
(
@KOTSettingsId int=NULL, @FK_KOTMasterId int=NULL, @ProductId int=NULL, @Quantity decimal(24,4)=NULL, @UnitId int=NULL, @Rate decimal(24,4)=NULL, @NETAmount decimal(24,4)=NULL, @Barcode nvarchar(100)=NULL, @ItemNote nvarchar(500)=NULL, @ProPriority int=NULL 
)

 AS 
 BEGIN 

 Select 
KOTSettingsId, 
FK_KOTMasterId, 
ProductId, 
Quantity, 
UnitId, 
Rate, 
NETAmount, 
Barcode, 
ItemNote, 
ProPriority
 from KOTOrderSettings where 
(KOTSettingsId = @KOTSettingsId or @KOTSettingsId is null ) And 
(FK_KOTMasterId = @FK_KOTMasterId or @FK_KOTMasterId is null ) And 
(ProductId = @ProductId or @ProductId is null ) And 
(Quantity = @Quantity or @Quantity is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(Rate = @Rate or @Rate is null ) And 
(NETAmount = @NETAmount or @NETAmount is null ) And 
(Barcode = @Barcode or @Barcode is null ) And 
(ItemNote = @ItemNote or @ItemNote is null ) And 
(ProPriority = @ProPriority or @ProPriority is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetKOTPrinter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetKOTPrinter]
(
@Id int=NULL, @KOTName nvarchar(100)=NULL, @PrinterName nvarchar(100)=NULL, @ConnectionType nvarchar(100)=NULL, @IPaddress nvarchar(100)=NULL, @Mode nvarchar(100)=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
KOTName, 
PrinterName, 
ConnectionType, 
IPaddress, 
Mode, 
FK_BranchId
 from KOTPrinter where 
(Id = @Id or @Id is null ) And 
(KOTName = @KOTName or @KOTName is null ) And 
(PrinterName = @PrinterName or @PrinterName is null ) And 
(ConnectionType = @ConnectionType or @ConnectionType is null ) And 
(IPaddress = @IPaddress or @IPaddress is null ) And 
(Mode = @Mode or @Mode is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetLastSalesInvoiceNo]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetLastSalesInvoiceNo]
(
@InvoiceType nvarchar(50) = null,
@BranchId int = null,
@FinYearId int =null
)

 AS 
 BEGIN 

 Select isnull(max(SalesInvNo),0)+1 as ProductCode from SalesEstimateMaster Where  
 (InvType = @InvoiceType or @InvoiceType is null) and
 (FK_BranchId = @BranchId or @BranchId is null) And
 (FYearID = @FinYearId or @FinYearId is null)

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spGetListOfDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetListOfDetails]
(
@ListDetails nvarchar(50)=NULL 
)

 AS 
 BEGIN 

 Select 
ListDetails
 from ListOfDetails where 
(ListDetails = @ListDetails or @ListDetails is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetLogged]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetLogged]
(
@UserId int=NULL 
)

 AS 
 BEGIN 

 Select 
UserId
 from Logged where 
(UserId = @UserId or @UserId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetManufactureDtls]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetManufactureDtls]
(
@Id int=NULL, @FK_ManufactureHdrId int=NULL, @FK_ProductId int=NULL, @Quantity decimal(24,4)=NULL, @FK_UnitId int=NULL, @PurchasePrice decimal(24,4)=NULL, @SalesePrice decimal(24,4)=NULL, @RawCost decimal(24,4)=NULL, @Expenses decimal(24,4)=NULL, @Profit decimal(24,4)=NULL, @FinalAmount decimal(24,4)=NULL, @NewPurchaseRate decimal(24,4)=NULL, @NewSalesRate decimal(24,4)=NULL, @FK_BatchId int=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
FK_ManufactureHdrId, 
FK_ProductId, 
Quantity, 
FK_UnitId, 
PurchasePrice, 
SalesePrice, 
RawCost, 
Expenses, 
Profit, 
FinalAmount, 
NewPurchaseRate, 
NewSalesRate, 
FK_BatchId
 from ManufactureDtls where 
(Id = @Id or @Id is null ) And 
(FK_ManufactureHdrId = @FK_ManufactureHdrId or @FK_ManufactureHdrId is null ) And 
(FK_ProductId = @FK_ProductId or @FK_ProductId is null ) And 
(Quantity = @Quantity or @Quantity is null ) And 
(FK_UnitId = @FK_UnitId or @FK_UnitId is null ) And 
(PurchasePrice = @PurchasePrice or @PurchasePrice is null ) And 
(SalesePrice = @SalesePrice or @SalesePrice is null ) And 
(RawCost = @RawCost or @RawCost is null ) And 
(Expenses = @Expenses or @Expenses is null ) And 
(Profit = @Profit or @Profit is null ) And 
(FinalAmount = @FinalAmount or @FinalAmount is null ) And 
(NewPurchaseRate = @NewPurchaseRate or @NewPurchaseRate is null ) And 
(NewSalesRate = @NewSalesRate or @NewSalesRate is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetManufactureHdr]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetManufactureHdr]
(
@Id int=NULL, @InvNo int=NULL, @Date datetime=NULL, @FK_GodownId int=NULL, @Remarks nvarchar(400)=NULL, @FK_UserId int=NULL, @EntryDate datetime=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
InvNo, 
Date, 
FK_GodownId, 
Remarks, 
FK_UserId, 
EntryDate, 
FK_BranchId
 from ManufactureHdr where 
(Id = @Id or @Id is null ) And 
(InvNo = @InvNo or @InvNo is null ) And 
(Date = @Date or @Date is null ) And 
(FK_GodownId = @FK_GodownId or @FK_GodownId is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(FK_UserId = @FK_UserId or @FK_UserId is null ) And 
(EntryDate = @EntryDate or @EntryDate is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetManufacturingExp]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetManufacturingExp]
(
@Id int=NULL, @FK_ExpenseAccountId int=NULL, @FK_PaymentAccountId int=NULL, @Date datetime=NULL, @Amount decimal(24,4)=NULL, @FK_ManufactureHdrId int=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
FK_ExpenseAccountId, 
FK_PaymentAccountId, 
Date, 
Amount, 
FK_ManufactureHdrId
 from ManufacturingExp where 
(Id = @Id or @Id is null ) And 
(FK_ExpenseAccountId = @FK_ExpenseAccountId or @FK_ExpenseAccountId is null ) And 
(FK_PaymentAccountId = @FK_PaymentAccountId or @FK_PaymentAccountId is null ) And 
(Date = @Date or @Date is null ) And 
(Amount = @Amount or @Amount is null ) And 
(FK_ManufactureHdrId = @FK_ManufactureHdrId or @FK_ManufactureHdrId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetMasterValue]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetMasterValue]
(
@Id int=NULL, @Name nvarchar(50)=NULL, @Description nvarchar(100)=NULL, @MasterType nvarchar(50)=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Name, 
Description, 
MasterType
 from MasterValue where 
(Id = @Id or @Id is null ) And 
(Name = @Name or @Name is null ) And 
(Description = @Description or @Description is null ) And 
(MasterType = @MasterType or @MasterType is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetMenuDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetMenuDetails]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @MenuCaption nvarchar(100)=NULL, @ParentId int=NULL, @AssemblyName nvarchar(100)=NULL, @ClassName nvarchar(100)=NULL, @IsForm bit=NULL, @Icon image=NULL, @FK_ModuleId int=NULL, @IsDialog bit=NULL, @Status bit=NULL, @Position int=NULL, @IsCrudMenu bit=NULL, @MenuUrl nvarchar(50)=NULL, @IsFavorite bit=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Name, 
MenuCaption, 
ParentId, 
AssemblyName, 
ClassName, 
IsForm, 
Icon, 
FK_ModuleId, 
IsDialog, 
Status, 
Position, 
IsCrudMenu, 
MenuUrl, 
IsFavorite
 from MenuDetails where 
(Id = @Id or @Id is null ) And 
(Name = @Name or @Name is null ) And 
(MenuCaption = @MenuCaption or @MenuCaption is null ) And 
(ParentId = @ParentId or @ParentId is null ) And 
(AssemblyName = @AssemblyName or @AssemblyName is null ) And 
(ClassName = @ClassName or @ClassName is null ) And 
(IsForm = @IsForm or @IsForm is null ) And 
(FK_ModuleId = @FK_ModuleId or @FK_ModuleId is null ) And 
(IsDialog = @IsDialog or @IsDialog is null ) And 
(Status = @Status or @Status is null ) And 
(Position = @Position or @Position is null ) And 
(IsCrudMenu = @IsCrudMenu or @IsCrudMenu is null ) And 
(MenuUrl = @MenuUrl or @MenuUrl is null ) And 
(IsFavorite = @IsFavorite or @IsFavorite is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetModules]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetModules]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @Description nvarchar(1000)=NULL, @Status bit=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Name, 
Description, 
Status
 from Modules where 
(Id = @Id or @Id is null ) And 
(Name = @Name or @Name is null ) And 
(Description = @Description or @Description is null ) And 
(Status = @Status or @Status is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetPaymentReportForPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetPaymentReportForPOS]
(
@FromDate datetime, 
@ToDate datetime,
@BranchId int = null

)
As
Begin
Select SalesDate as [Date],CntrInvNo as ReceiptNo,ac.AccountName as Party,CashPayment as Cash, CardPayment as [Card], OtherPayment as [Online],
Credit, ReturnAmount as [Return], GrandDiscount as Total
From SalesEstimateMaster sem
Left join Account ac on sem.PartyAccountId = ac.AccountId
Where (sem.FK_BranchId = @BranchId or @BranchId is null) and
SalesDate between @FromDate and @ToDate
End
GO
/****** Object:  StoredProcedure [dbo].[spGetPOSHoldMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetPOSHoldMaster]
(
@Id int=NULL, @SalesInvNo int=NULL, @InvType nvarchar(50)=NULL, @SalesDate datetime=NULL, @SalesAccountId int=NULL, @PartyAccountId int=NULL, @RouteId int=NULL, @VehId int=NULL, @StafId int=NULL, @PriceCategoryId int=NULL, @AditionalAmount decimal(18,4)=NULL, @Frieght decimal(18,4)=NULL, @TotalDiscount decimal(18,4)=NULL, @BillDiscount decimal(18,4)=NULL, @GrandDiscount decimal(18,4)=NULL, @JEId int=NULL, @EntryDate datetime=NULL, @Remarks nvarchar(50)=NULL, @IGSTAmount decimal(18,4)=NULL, @SGSTAmount decimal(18,4)=NULL, @CGSTAmount decimal(18,4)=NULL, @TaxableAmount decimal(18,4)=NULL, @FYearID int=NULL, @FloodCess decimal(18,4)=NULL, @TotalTaxAmount decimal(18,4)=NULL, @FK_BranchId int=NULL, @CntrInvNo nvarchar(10)=NULL, @SequenceNo int=NULL, @CashPayment decimal(24,8)=NULL, @CardPayment decimal(24,8)=NULL, @Credit decimal(24,8)=NULL, @FielOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @Terminal nvarchar(25)=NULL, @IsClosed bit=NULL, @FreeAmount decimal(24,4)=NULL, @ReturnAmount decimal(24,4)=NULL, @OtherPayment decimal(24,4)=NULL, @CashAccountId int=NULL, @CardAccountId int=NULL, @OnlineAccountId int=NULL, @IsSettiled bit=NULL, @TotalPayment decimal(24,8)=NULL, @VoucherNumber nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
SalesInvNo, 
InvType, 
SalesDate, 
SalesAccountId, 
PartyAccountId, 
RouteId, 
VehId, 
StafId, 
PriceCategoryId, 
AditionalAmount, 
Frieght, 
TotalDiscount, 
BillDiscount, 
GrandDiscount, 
JEId, 
EntryDate, 
Remarks, 
IGSTAmount, 
SGSTAmount, 
CGSTAmount, 
TaxableAmount, 
FYearID, 
FloodCess, 
TotalTaxAmount, 
FK_BranchId, 
CntrInvNo, 
SequenceNo, 
CashPayment, 
CardPayment, 
Credit, 
FielOne, 
FieldTwo, 
FieldThree, 
FieldFour, 
Terminal, 
IsClosed, 
FreeAmount, 
ReturnAmount, 
OtherPayment, 
CashAccountId, 
CardAccountId, 
OnlineAccountId, 
IsSettiled, 
TotalPayment, 
VoucherNumber
 from POSHoldMaster where 
(Id = @Id or @Id is null ) And 
(SalesInvNo = @SalesInvNo or @SalesInvNo is null ) And 
(InvType = @InvType or @InvType is null ) And 
(SalesDate = @SalesDate or @SalesDate is null ) And 
(SalesAccountId = @SalesAccountId or @SalesAccountId is null ) And 
(PartyAccountId = @PartyAccountId or @PartyAccountId is null ) And 
(RouteId = @RouteId or @RouteId is null ) And 
(VehId = @VehId or @VehId is null ) And 
(StafId = @StafId or @StafId is null ) And 
(PriceCategoryId = @PriceCategoryId or @PriceCategoryId is null ) And 
(AditionalAmount = @AditionalAmount or @AditionalAmount is null ) And 
(Frieght = @Frieght or @Frieght is null ) And 
(TotalDiscount = @TotalDiscount or @TotalDiscount is null ) And 
(BillDiscount = @BillDiscount or @BillDiscount is null ) And 
(GrandDiscount = @GrandDiscount or @GrandDiscount is null ) And 
(JEId = @JEId or @JEId is null ) And 
(EntryDate = @EntryDate or @EntryDate is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(IGSTAmount = @IGSTAmount or @IGSTAmount is null ) And 
(SGSTAmount = @SGSTAmount or @SGSTAmount is null ) And 
(CGSTAmount = @CGSTAmount or @CGSTAmount is null ) And 
(TaxableAmount = @TaxableAmount or @TaxableAmount is null ) And 
(FYearID = @FYearID or @FYearID is null ) And 
(FloodCess = @FloodCess or @FloodCess is null ) And 
(TotalTaxAmount = @TotalTaxAmount or @TotalTaxAmount is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(CntrInvNo = @CntrInvNo or @CntrInvNo is null ) And 
(SequenceNo = @SequenceNo or @SequenceNo is null ) And 
(CashPayment = @CashPayment or @CashPayment is null ) And 
(CardPayment = @CardPayment or @CardPayment is null ) And 
(Credit = @Credit or @Credit is null ) And 
(FielOne = @FielOne or @FielOne is null ) And 
(FieldTwo = @FieldTwo or @FieldTwo is null ) And 
(FieldThree = @FieldThree or @FieldThree is null ) And 
(FieldFour = @FieldFour or @FieldFour is null ) And 
(Terminal = @Terminal or @Terminal is null ) And 
(IsClosed = @IsClosed or @IsClosed is null ) And 
(FreeAmount = @FreeAmount or @FreeAmount is null ) And 
(ReturnAmount = @ReturnAmount or @ReturnAmount is null ) And 
(OtherPayment = @OtherPayment or @OtherPayment is null ) And 
(CashAccountId = @CashAccountId or @CashAccountId is null ) And 
(CardAccountId = @CardAccountId or @CardAccountId is null ) And 
(OnlineAccountId = @OnlineAccountId or @OnlineAccountId is null ) And 
(IsSettiled = @IsSettiled or @IsSettiled is null ) And 
(TotalPayment = @TotalPayment or @TotalPayment is null ) And 
(VoucherNumber = @VoucherNumber or @VoucherNumber is null ) 

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spGetPOSHoldSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetPOSHoldSettings]
(
@Id int=NULL, @SalesInvId int=NULL, @PartyAccountId int=NULL, @ProductId int=NULL, @ProQty decimal(18,4)=NULL, @UnitId int=NULL, @ProRate decimal(18,4)=NULL, @ProDiscont decimal(18,4)=NULL, @DiscAmnt decimal(18,4)=NULL, @GrossAmnt decimal(18,4)=NULL, @NetAmnt decimal(18,4)=NULL, @Date datetime=NULL, @HSN nvarchar(20)=NULL, @GST decimal(18,4)=NULL, @GSTAMOUNT decimal(18,4)=NULL, @SGST decimal(18,4)=NULL, @SGSTAmount decimal(18,4)=NULL, @CGST decimal(18,4)=NULL, @CGSTAmount decimal(18,4)=NULL, @IGST decimal(18,4)=NULL, @IGSTAMOUNT decimal(18,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(20)=NULL, @ItemCalory decimal(18,4)=NULL, @EmployeeID int=NULL, @EmployeeCommision decimal(18,4)=NULL, @AgentId int=NULL, @AgentCommission decimal(18,4)=NULL, @ProductName nvarchar(50)=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
SalesInvId, 
PartyAccountId, 
ProductId, 
ProQty, 
UnitId, 
ProRate, 
ProDiscont, 
DiscAmnt, 
GrossAmnt, 
NetAmnt, 
Date, 
HSN, 
GST, 
GSTAMOUNT, 
SGST, 
SGSTAmount, 
CGST, 
CGSTAmount, 
IGST, 
IGSTAMOUNT, 
FK_BatchId, 
Barcode, 
ItemCalory, 
EmployeeID, 
EmployeeCommision, 
AgentId, 
AgentCommission, 
ProductName
 from POSHoldSettings where 
(Id = @Id or @Id is null ) And 
(SalesInvId = @SalesInvId or @SalesInvId is null ) And 
(PartyAccountId = @PartyAccountId or @PartyAccountId is null ) And 
(ProductId = @ProductId or @ProductId is null ) And 
(ProQty = @ProQty or @ProQty is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(ProRate = @ProRate or @ProRate is null ) And 
(ProDiscont = @ProDiscont or @ProDiscont is null ) And 
(DiscAmnt = @DiscAmnt or @DiscAmnt is null ) And 
(GrossAmnt = @GrossAmnt or @GrossAmnt is null ) And 
(NetAmnt = @NetAmnt or @NetAmnt is null ) And 
(Date = @Date or @Date is null ) And 
(HSN = @HSN or @HSN is null ) And 
(GST = @GST or @GST is null ) And 
(GSTAMOUNT = @GSTAMOUNT or @GSTAMOUNT is null ) And 
(SGST = @SGST or @SGST is null ) And 
(SGSTAmount = @SGSTAmount or @SGSTAmount is null ) And 
(CGST = @CGST or @CGST is null ) And 
(CGSTAmount = @CGSTAmount or @CGSTAmount is null ) And 
(IGST = @IGST or @IGST is null ) And 
(IGSTAMOUNT = @IGSTAMOUNT or @IGSTAMOUNT is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) And 
(Barcode = @Barcode or @Barcode is null ) And 
(ItemCalory = @ItemCalory or @ItemCalory is null ) And 
(EmployeeID = @EmployeeID or @EmployeeID is null ) And 
(EmployeeCommision = @EmployeeCommision or @EmployeeCommision is null ) And 
(AgentId = @AgentId or @AgentId is null ) And 
(AgentCommission = @AgentCommission or @AgentCommission is null ) And 
(ProductName = @ProductName or @ProductName is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetPrevilegeCard]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetPrevilegeCard]
(
@PrevilageCardId int=NULL, @ConsumerCode nvarchar(50)=NULL, @CustomerId int=NULL, @PriceCategoryId int=NULL, @NomineeAccountId int=NULL, @ExpairyDate datetime=NULL, @RenewalDate datetime=NULL, @DOBofCustomer datetime=NULL, @Remarks nvarchar(400)=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
PrevilageCardId, 
ConsumerCode, 
CustomerId, 
PriceCategoryId, 
NomineeAccountId, 
ExpairyDate, 
RenewalDate, 
DOBofCustomer, 
Remarks, 
FK_BranchId
 from PrevilegeCard where 
(PrevilageCardId = @PrevilageCardId or @PrevilageCardId is null ) And 
(ConsumerCode = @ConsumerCode or @ConsumerCode is null ) And 
(CustomerId = @CustomerId or @CustomerId is null ) And 
(PriceCategoryId = @PriceCategoryId or @PriceCategoryId is null ) And 
(NomineeAccountId = @NomineeAccountId or @NomineeAccountId is null ) And 
(ExpairyDate = @ExpairyDate or @ExpairyDate is null ) And 
(RenewalDate = @RenewalDate or @RenewalDate is null ) And 
(DOBofCustomer = @DOBofCustomer or @DOBofCustomer is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetPriceCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetPriceCategory]
(
@PCategoryId int=NULL, @PCategoryName nvarchar(50)=NULL, @Percentage decimal(24,4)=NULL, @SDiscountPercentage decimal(24,4)=NULL 
)

 AS 
 BEGIN 

 Select 
PCategoryId, 
PCategoryName, 
Percentage, 
SDiscountPercentage
 from PriceCategory where 
(PCategoryId = @PCategoryId or @PCategoryId is null ) And 
(PCategoryName = @PCategoryName or @PCategoryName is null ) And 
(Percentage = @Percentage or @Percentage is null ) And 
(SDiscountPercentage = @SDiscountPercentage or @SDiscountPercentage is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetPrintDesignMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetPrintDesignMaster]
(
@printMasterId int=NULL, @DesignName nvarchar(50)=NULL, @VochrType nvarchar(50)=NULL, @isDefault bit=NULL, @isLandScape bit=NULL, @PageHieght int=NULL, @PageWidth int=NULL, @LinesPerPage int=NULL, @SpaceBtwnLines int=NULL, @BackgroundImgPath nvarchar(300)=NULL, @PrinterName nvarchar(50)=NULL, @PrintType nvarchar(50)=NULL, @upScroll int=NULL, @downScroll int=NULL, @Riverse nvarchar(10)=NULL, @Bold_On nvarchar(10)=NULL, @Bold_Off nvarchar(10)=NULL, @Pitch nvarchar(10)=NULL, @FormType nvarchar(50)=NULL, @SpaceInLines int=NULL 
)

 AS 
 BEGIN 

 Select 
printMasterId, 
DesignName, 
VochrType, 
isDefault, 
isLandScape, 
PageHieght, 
PageWidth, 
LinesPerPage, 
SpaceBtwnLines, 
BackgroundImgPath, 
PrinterName, 
PrintType, 
upScroll, 
downScroll, 
Riverse, 
Bold_On, 
Bold_Off, 
Pitch, 
FormType, 
SpaceInLines
 from PrintDesignMaster where 
(printMasterId = @printMasterId or @printMasterId is null ) And 
(DesignName = @DesignName or @DesignName is null ) And 
(VochrType = @VochrType or @VochrType is null ) And 
(isDefault = @isDefault or @isDefault is null ) And 
(isLandScape = @isLandScape or @isLandScape is null ) And 
(PageHieght = @PageHieght or @PageHieght is null ) And 
(PageWidth = @PageWidth or @PageWidth is null ) And 
(LinesPerPage = @LinesPerPage or @LinesPerPage is null ) And 
(SpaceBtwnLines = @SpaceBtwnLines or @SpaceBtwnLines is null ) And 
(BackgroundImgPath = @BackgroundImgPath or @BackgroundImgPath is null ) And 
(PrinterName = @PrinterName or @PrinterName is null ) And 
(PrintType = @PrintType or @PrintType is null ) And 
(upScroll = @upScroll or @upScroll is null ) And 
(downScroll = @downScroll or @downScroll is null ) And 
(Riverse = @Riverse or @Riverse is null ) And 
(Bold_On = @Bold_On or @Bold_On is null ) And 
(Bold_Off = @Bold_Off or @Bold_Off is null ) And 
(Pitch = @Pitch or @Pitch is null ) And 
(FormType = @FormType or @FormType is null ) And 
(SpaceInLines = @SpaceInLines or @SpaceInLines is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetPrintDesignSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetPrintDesignSettings]
(
@PrintDesignSettingsID int=NULL, @printMasterId int=NULL, @ControleName nvarchar(50)=NULL, @Type nvarchar(20)=NULL, @FeildText nvarchar(500)=NULL, @X int=NULL, @Y int=NULL, @FontSize int=NULL, @Font nvarchar(50)=NULL, @isBold bit=NULL, @width int=NULL, @Alaingmnet nvarchar(10)=NULL, @Position nvarchar(25)=NULL, @TabIndex int=NULL, @iIndex int=NULL, @Height int=NULL, @Spec nvarchar(10)=NULL 
)

 AS 
 BEGIN 

 Select 
PrintDesignSettingsID, 
printMasterId, 
ControleName, 
Type, 
FeildText, 
X, 
Y, 
FontSize, 
Font, 
isBold, 
width, 
Alaingmnet, 
Position, 
TabIndex, 
iIndex, 
Height, 
Spec
 from PrintDesignSettings where 
(PrintDesignSettingsID = @PrintDesignSettingsID or @PrintDesignSettingsID is null ) And 
(printMasterId = @printMasterId or @printMasterId is null ) And 
(ControleName = @ControleName or @ControleName is null ) And 
(Type = @Type or @Type is null ) And 
(FeildText = @FeildText or @FeildText is null ) And 
(X = @X or @X is null ) And 
(Y = @Y or @Y is null ) And 
(FontSize = @FontSize or @FontSize is null ) And 
(Font = @Font or @Font is null ) And 
(isBold = @isBold or @isBold is null ) And 
(width = @width or @width is null ) And 
(Alaingmnet = @Alaingmnet or @Alaingmnet is null ) And 
(Position = @Position or @Position is null ) And 
(TabIndex = @TabIndex or @TabIndex is null ) And 
(iIndex = @iIndex or @iIndex is null ) And 
(Height = @Height or @Height is null ) And 
(Spec = @Spec or @Spec is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetProCombMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetProCombMaster]
(
@ProCombMasterId int=NULL, @CombProName nvarchar(50)=NULL, @ProCombVchrNo int=NULL, @Date datetime=NULL, @ProQty decimal(24,4)=NULL, @UnitId int=NULL, @ProSaleRate decimal(24,4)=NULL, @ProPurRate decimal(24,4)=NULL, @MRP decimal(24,4)=NULL, @CombType nvarchar(50)=NULL, @Barcode nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
ProCombMasterId, 
CombProName, 
ProCombVchrNo, 
Date, 
ProQty, 
UnitId, 
ProSaleRate, 
ProPurRate, 
MRP, 
CombType, 
Barcode
 from ProCombMaster where 
(ProCombMasterId = @ProCombMasterId or @ProCombMasterId is null ) And 
(CombProName = @CombProName or @CombProName is null ) And 
(ProCombVchrNo = @ProCombVchrNo or @ProCombVchrNo is null ) And 
(Date = @Date or @Date is null ) And 
(ProQty = @ProQty or @ProQty is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(ProSaleRate = @ProSaleRate or @ProSaleRate is null ) And 
(ProPurRate = @ProPurRate or @ProPurRate is null ) And 
(MRP = @MRP or @MRP is null ) And 
(CombType = @CombType or @CombType is null ) And 
(Barcode = @Barcode or @Barcode is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetProCombSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetProCombSettings]
(
@ProCombSettingsId int=NULL, @ProCombMasterId int=NULL, @ProductId int=NULL, @ProQty decimal(24,4)=NULL, @UnitId int=NULL, @SaleRate decimal(24,4)=NULL, @PurRate decimal(24,4)=NULL, @MRP decimal(24,4)=NULL, @Barcode nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
ProCombSettingsId, 
ProCombMasterId, 
ProductId, 
ProQty, 
UnitId, 
SaleRate, 
PurRate, 
MRP, 
Barcode
 from ProCombSettings where 
(ProCombSettingsId = @ProCombSettingsId or @ProCombSettingsId is null ) And 
(ProCombMasterId = @ProCombMasterId or @ProCombMasterId is null ) And 
(ProductId = @ProductId or @ProductId is null ) And 
(ProQty = @ProQty or @ProQty is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(SaleRate = @SaleRate or @SaleRate is null ) And 
(PurRate = @PurRate or @PurRate is null ) And 
(MRP = @MRP or @MRP is null ) And 
(Barcode = @Barcode or @Barcode is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetProduct]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetProduct]
(
@ProductId int=NULL, @ProductCode int=NULL, @ProductName nvarchar(120)=NULL, @Specification nvarchar(50)=NULL, @PGroupId int=NULL, @UnitId int=NULL, @BrandId int=NULL, @TCategoryId int=NULL, @HSNNo nvarchar(10)=NULL, @PurchasePrice decimal(24,4)=NULL, @SellingPrice decimal(24,4)=NULL, @MRP decimal(24,4)=NULL, @OpeningStock decimal(24,4)=NULL, @OppStockDate datetime=NULL, @ReOrderStock decimal(24,4)=NULL, @AccountId int=NULL, @GodwnId int=NULL, @SelfId int=NULL, @PurchaseDis decimal(24,4)=NULL, @SaleDis decimal(24,4)=NULL, @AliasCode nvarchar(50)=NULL, @AliasName nvarchar(120)=NULL, @Remarks nvarchar(500)=NULL, @LocalName nvarchar(500)=NULL, @PurchaseUnit int=NULL, @SalesUnit int=NULL, @ReportUnit int=NULL, @image image=NULL, @filePath nvarchar(400)=NULL, @description nvarchar(5)=NULL, @ProductType nvarchar(400)=NULL, @isActive bit=NULL, @isInclusive bit=NULL, @Barcode nvarchar(20)=NULL, @IsExpairing bit=NULL, @ExpairingDays decimal(24,4)=NULL, @MinimumMargin decimal(24,4)=NULL, @NormalMargin decimal(24,4)=NULL, @RetailMargin decimal(24,4)=NULL, @WholesaleMargin decimal(24,4)=NULL, @MRPMargin decimal(24,4)=NULL, @IsMarginBasedPercentage bit=NULL, @LoyalityPont int=NULL, @AgentCommision decimal(24,4)=NULL, @SalesmanCommision decimal(24,4)=NULL, @IsCommisionBasedPercentage bit=NULL, @FieldOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @FieldFive nvarchar(100)=NULL, @IsScale bit=NULL, @ListOrder int=NULL, @IsAllowMultiple bit=NULL, @isRateBasedWeight bit=NULL, @PriorityStatus nvarchar(25)=NULL, @CookingTime int=NULL, @IsFavorite bit=NULL, @RestaurantItemType nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
ProductId, 
ProductCode, 
ProductName, 
Specification, 
PGroupId, 
UnitId, 
BrandId, 
TCategoryId, 
HSNNo, 
PurchasePrice, 
SellingPrice, 
MRP, 
OpeningStock, 
OppStockDate, 
ReOrderStock, 
AccountId, 
GodwnId, 
SelfId, 
PurchaseDis, 
SaleDis, 
AliasCode, 
AliasName, 
Remarks, 
LocalName, 
PurchaseUnit, 
SalesUnit, 
ReportUnit, 
image, 
filePath, 
description, 
ProductType, 
isActive, 
isInclusive, 
Barcode, 
IsExpairing, 
ExpairingDays, 
MinimumMargin, 
NormalMargin, 
RetailMargin, 
WholesaleMargin, 
MRPMargin, 
IsMarginBasedPercentage, 
LoyalityPont, 
AgentCommision, 
SalesmanCommision, 
IsCommisionBasedPercentage, 
FieldOne, 
FieldTwo, 
FieldThree, 
FieldFour, 
FieldFive, 
IsScale, 
ListOrder, 
IsAllowMultiple, 
isRateBasedWeight, 
PriorityStatus, 
CookingTime, 
IsFavorite, 
RestaurantItemType
 from Product where 
(ProductId = @ProductId or @ProductId is null ) And 
(ProductCode = @ProductCode or @ProductCode is null ) And 
(ProductName = @ProductName or @ProductName is null ) And 
(Specification = @Specification or @Specification is null ) And 
(PGroupId = @PGroupId or @PGroupId is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(BrandId = @BrandId or @BrandId is null ) And 
(TCategoryId = @TCategoryId or @TCategoryId is null ) And 
(HSNNo = @HSNNo or @HSNNo is null ) And 
(PurchasePrice = @PurchasePrice or @PurchasePrice is null ) And 
(SellingPrice = @SellingPrice or @SellingPrice is null ) And 
(MRP = @MRP or @MRP is null ) And 
(OpeningStock = @OpeningStock or @OpeningStock is null ) And 
(OppStockDate = @OppStockDate or @OppStockDate is null ) And 
(ReOrderStock = @ReOrderStock or @ReOrderStock is null ) And 
(AccountId = @AccountId or @AccountId is null ) And 
(GodwnId = @GodwnId or @GodwnId is null ) And 
(SelfId = @SelfId or @SelfId is null ) And 
(PurchaseDis = @PurchaseDis or @PurchaseDis is null ) And 
(SaleDis = @SaleDis or @SaleDis is null ) And 
(AliasCode = @AliasCode or @AliasCode is null ) And 
(AliasName = @AliasName or @AliasName is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(LocalName = @LocalName or @LocalName is null ) And 
(PurchaseUnit = @PurchaseUnit or @PurchaseUnit is null ) And 
(SalesUnit = @SalesUnit or @SalesUnit is null ) And 
(ReportUnit = @ReportUnit or @ReportUnit is null ) And 
(filePath = @filePath or @filePath is null ) And 
(description = @description or @description is null ) And 
(ProductType = @ProductType or @ProductType is null ) And 
(isActive = @isActive or @isActive is null ) And 
(isInclusive = @isInclusive or @isInclusive is null ) And 
(Barcode = @Barcode or @Barcode is null ) And 
(IsExpairing = @IsExpairing or @IsExpairing is null ) And 
(ExpairingDays = @ExpairingDays or @ExpairingDays is null ) And 
(MinimumMargin = @MinimumMargin or @MinimumMargin is null ) And 
(NormalMargin = @NormalMargin or @NormalMargin is null ) And 
(RetailMargin = @RetailMargin or @RetailMargin is null ) And 
(WholesaleMargin = @WholesaleMargin or @WholesaleMargin is null ) And 
(MRPMargin = @MRPMargin or @MRPMargin is null ) And 
(IsMarginBasedPercentage = @IsMarginBasedPercentage or @IsMarginBasedPercentage is null ) And 
(LoyalityPont = @LoyalityPont or @LoyalityPont is null ) And 
(AgentCommision = @AgentCommision or @AgentCommision is null ) And 
(SalesmanCommision = @SalesmanCommision or @SalesmanCommision is null ) And 
(IsCommisionBasedPercentage = @IsCommisionBasedPercentage or @IsCommisionBasedPercentage is null ) And 
(FieldOne = @FieldOne or @FieldOne is null ) And 
(FieldTwo = @FieldTwo or @FieldTwo is null ) And 
(FieldThree = @FieldThree or @FieldThree is null ) And 
(FieldFour = @FieldFour or @FieldFour is null ) And 
(FieldFive = @FieldFive or @FieldFive is null ) And 
(IsScale = @IsScale or @IsScale is null ) And 
(ListOrder = @ListOrder or @ListOrder is null ) And 
(IsAllowMultiple = @IsAllowMultiple or @IsAllowMultiple is null ) And 
(isRateBasedWeight = @isRateBasedWeight or @isRateBasedWeight is null ) And 
(PriorityStatus = @PriorityStatus or @PriorityStatus is null ) And 
(CookingTime = @CookingTime or @CookingTime is null ) And 
(IsFavorite = @IsFavorite or @IsFavorite is null ) And 
(RestaurantItemType = @RestaurantItemType or @RestaurantItemType is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetProductBarcode]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetProductBarcode]
(
@Id int=NULL, @Barcode nvarchar(100)=NULL, @BarcodeType nvarchar(100)=NULL, @ExpairyDate datetime=NULL, @ManufDate datetime=NULL, @FK_ProductId int=NULL, @FK_UnitId int=NULL, @FK_BatchId int=NULL, @FK_BranchId int=NULL, @Remarks nvarchar(100)=NULL, @PurchasePrice decimal(24,4)=NULL, @SellingPrice decimal(24,4)=NULL, @MRP decimal(24,4)=NULL, @ProductCost decimal(24,4)=NULL, @FieldOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @MinimuPrice decimal(24,4)=NULL, @RetailPrice decimal(24,4)=NULL, @WholesalePrice decimal(24,4)=NULL, @ProductBarcodeName nvarchar(50)=NULL, @LocalName nvarchar(50)=NULL, @image image=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Barcode, 
BarcodeType, 
ExpairyDate, 
ManufDate, 
FK_ProductId, 
FK_UnitId, 
FK_BatchId, 
FK_BranchId, 
Remarks, 
PurchasePrice, 
SellingPrice, 
MRP, 
ProductCost, 
FieldOne, 
FieldTwo, 
FieldThree, 
FieldFour, 
MinimuPrice, 
RetailPrice, 
WholesalePrice, 
ProductBarcodeName, 
LocalName, 
image
 from ProductBarcode where 
(Id = @Id or @Id is null ) And 
(Barcode = @Barcode or @Barcode is null ) And 
(BarcodeType = @BarcodeType or @BarcodeType is null ) And 
(ExpairyDate = @ExpairyDate or @ExpairyDate is null ) And 
(ManufDate = @ManufDate or @ManufDate is null ) And 
(FK_ProductId = @FK_ProductId or @FK_ProductId is null ) And 
(FK_UnitId = @FK_UnitId or @FK_UnitId is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(PurchasePrice = @PurchasePrice or @PurchasePrice is null ) And 
(SellingPrice = @SellingPrice or @SellingPrice is null ) And 
(MRP = @MRP or @MRP is null ) And 
(ProductCost = @ProductCost or @ProductCost is null ) And 
(FieldOne = @FieldOne or @FieldOne is null ) And 
(FieldTwo = @FieldTwo or @FieldTwo is null ) And 
(FieldThree = @FieldThree or @FieldThree is null ) And 
(FieldFour = @FieldFour or @FieldFour is null ) And 
(MinimuPrice = @MinimuPrice or @MinimuPrice is null ) And 
(RetailPrice = @RetailPrice or @RetailPrice is null ) And 
(WholesalePrice = @WholesalePrice or @WholesalePrice is null ) And 
(ProductBarcodeName = @ProductBarcodeName or @ProductBarcodeName is null ) And 
(LocalName = @LocalName or @LocalName is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetProductBarcodeScale]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetProductBarcodeScale]
(
@Id int=NULL, @FK_ProductId int=NULL, @FK_BarcodeScale int=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
FK_ProductId, 
FK_BarcodeScale
 from ProductBarcodeScale where 
(Id = @Id or @Id is null ) And 
(FK_ProductId = @FK_ProductId or @FK_ProductId is null ) And 
(FK_BarcodeScale = @FK_BarcodeScale or @FK_BarcodeScale is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetProductGroup]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetProductGroup]
(
@PGroupId int=NULL, @PGroupName nvarchar(50)=NULL, @Under nvarchar(50)=NULL, @SalesInsentive decimal(24,4)=NULL, @SalesDis decimal(24,4)=NULL, @PurchDis decimal(24,4)=NULL, @Remarks nvarchar(1000)=NULL, @ListOrder int=NULL, @isShowInGrid bit=NULL 
)

 AS 
 BEGIN 

 Select 
PGroupId, 
PGroupName, 
Under, 
SalesInsentive, 
SalesDis, 
PurchDis, 
Remarks, 
ListOrder, 
isShowInGrid
 from ProductGroup where 
(PGroupId = @PGroupId or @PGroupId is null ) And 
(PGroupName = @PGroupName or @PGroupName is null ) And 
(Under = @Under or @Under is null ) And 
(SalesInsentive = @SalesInsentive or @SalesInsentive is null ) And 
(SalesDis = @SalesDis or @SalesDis is null ) And 
(PurchDis = @PurchDis or @PurchDis is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(ListOrder = @ListOrder or @ListOrder is null ) And 
(isShowInGrid = @isShowInGrid or @isShowInGrid is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetProductLastProductCode]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetProductLastProductCode]

 AS 
 BEGIN 

 Select isnull(max(ProductCode),0)+1 as ProductCode from Product 

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spGetProductList]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetProductList]

 AS 
 BEGIN 

 SELECT TOP (1000) [ProductId]
      ,[ProductCode]
      ,[ProductName]
      ,[Specification]
      ,[PGroupId]
      ,[UnitId]
      ,[BrandId]
      ,[TCategoryId]
      ,[HSNNo]
      ,[PurchasePrice]
      ,[SellingPrice]
      ,[MRP]
      ,[OpeningStock]
      ,[OppStockDate]
      ,[ReOrderStock]
      ,[AccountId]
      ,[GodwnId]
      ,[SelfId]
      ,[PurchaseDis]
      ,[SaleDis]
      ,[AliasCode]
      ,[AliasName]
      ,[Remarks]
      ,[LocalName]
      ,[PurchaseUnit]
      ,[SalesUnit]
      ,[ReportUnit]
      ,[image]
      ,[filePath]
      ,[description]
      ,[ProductType]
      ,[isActive]
      ,[isInclusive]
      ,[Barcode]
      ,[IsExpairing]
      ,[ExpairingDays]
      ,[MinimumMargin]
      ,[NormalMargin]
      ,[RetailMargin]
      ,[WholesaleMargin]
      ,[MRPMargin]
      ,[IsMarginBasedPercentage]
      ,[LoyalityPont]
      ,[AgentCommision]
      ,[SalesmanCommision]
      ,[IsCommisionBasedPercentage]
      ,[FieldOne]
      ,[FieldTwo]
      ,[FieldThree]
      ,[FieldFour]
      ,[FieldFive]
      ,[IsScale]
      ,[ListOrder]
      ,[IsAllowMultiple]
  FROM [dbo].[Product]

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spGetProductPriceCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetProductPriceCategory]
(
@ProPrceCatId int=NULL, @ProductId int=NULL, @PCategoryId int=NULL, @UnitId int=NULL, @PGroupId int=NULL, @SPrice decimal(24,4)=NULL, @SPDisc decimal(24,4)=NULL, @PPrice decimal(24,4)=NULL, @PDisc decimal(24,4)=NULL, @Barcode nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
ProPrceCatId, 
ProductId, 
PCategoryId, 
UnitId, 
PGroupId, 
SPrice, 
SPDisc, 
PPrice, 
PDisc, 
Barcode
 from ProductPriceCategory where 
(ProPrceCatId = @ProPrceCatId or @ProPrceCatId is null ) And 
(ProductId = @ProductId or @ProductId is null ) And 
(PCategoryId = @PCategoryId or @PCategoryId is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(PGroupId = @PGroupId or @PGroupId is null ) And 
(SPrice = @SPrice or @SPrice is null ) And 
(SPDisc = @SPDisc or @SPDisc is null ) And 
(PPrice = @PPrice or @PPrice is null ) And 
(PDisc = @PDisc or @PDisc is null ) And 
(Barcode = @Barcode or @Barcode is null ) 

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spGetProductPrinter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetProductPrinter]
(
@Id int=NULL, @FK_KOTPrinterId int=NULL, @FK_ProductId int=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
FK_KOTPrinterId, 
FK_ProductId, 
FK_BranchId
 from ProductPrinter where 
(Id = @Id or @Id is null ) And 
(FK_KOTPrinterId = @FK_KOTPrinterId or @FK_KOTPrinterId is null ) And 
(FK_ProductId = @FK_ProductId or @FK_ProductId is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetProductUnit]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetProductUnit]
(
@ProUnitId int=NULL, @ProductId int=NULL, @UnitId int=NULL, @UnitType nvarchar(100)=NULL, @Barcode nvarchar(20)=NULL, @UnitQty decimal(24,4)=NULL, @ProductUnitName nvarchar(100)=NULL, @IsInclusive bit=NULL, @OpeningStock decimal(24,4)=NULL, @SalesPrice decimal(24,4)=NULL 
)

 AS 
 BEGIN 

 Select 
ProUnitId, 
ProductId, 
UnitId, 
UnitType, 
Barcode, 
UnitQty, 
ProductUnitName, 
IsInclusive, 
OpeningStock, 
SalesPrice
 from ProductUnit where 
(ProUnitId = @ProUnitId or @ProUnitId is null ) And 
(ProductId = @ProductId or @ProductId is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(UnitType = @UnitType or @UnitType is null ) And 
(Barcode = @Barcode or @Barcode is null ) And 
(UnitQty = @UnitQty or @UnitQty is null ) And 
(ProductUnitName = @ProductUnitName or @ProductUnitName is null ) And 
(IsInclusive = @IsInclusive or @IsInclusive is null ) And 
(OpeningStock = @OpeningStock or @OpeningStock is null ) And 
(SalesPrice = @SalesPrice or @SalesPrice is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetProductWithBarcode]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetProductWithBarcode]
(
@Barcode nvarchar(25)=NULL
)

 AS 
 BEGIN 
 declare @ProId int
 Select @ProId = FK_ProductId from ProductBarcode where 
(Barcode = @Barcode or @Barcode is null ) 
set @ProId = ISNULL(@ProId,0)
IF @ProId > 0
Begin
select * From Product Where ProductId = @ProId
 END
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spGetPurInvMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetPurInvMaster]
(
@PurInvId int=NULL, @PurInvNo int=NULL, @InvType nvarchar(50)=NULL, @PurDate datetime=NULL, @PurActId int=NULL, @PartyAccId int=NULL, @PriceCategoryId int=NULL, @AdditionalAmnt decimal(24,4)=NULL, @Frieght decimal(24,4)=NULL, @TotalDiscount decimal(24,4)=NULL, @BillDisc decimal(24,4)=NULL, @TotalTax decimal(24,4)=NULL, @TotalAmnt decimal(24,4)=NULL, @JEId int=NULL, @EntryDate datetime=NULL, @Remarks nvarchar(250)=NULL, @IGSTAmount decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @RefInvNo nvarchar(50)=NULL, @Taxable decimal(24,4)=NULL, @FK_BranchId int=NULL, @CashPayment decimal(24,4)=NULL, @CardPayment decimal(24,4)=NULL, @Credit decimal(24,4)=NULL, @FreeAmount decimal(24,4)=NULL, @ReturnAmount decimal(24,4)=NULL, @IsClosed bit=NULL, @FielOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @CashAccountId int=NULL, @CardAccountId int=NULL, @OnlineAccountId int=NULL, @IsSettiled bit=NULL, @TotalPayment decimal(24,8)=NULL, @VoucherNumber nvarchar(25)=NULL, @SequenceNo int=NULL, @Terminal nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
PurInvId, 
PurInvNo, 
InvType, 
PurDate, 
PurActId, 
PartyAccId, 
PriceCategoryId, 
AdditionalAmnt, 
Frieght, 
TotalDiscount, 
BillDisc, 
TotalTax, 
TotalAmnt, 
JEId, 
EntryDate, 
Remarks, 
IGSTAmount, 
SGSTAmount, 
CGSTAmount, 
RefInvNo, 
Taxable, 
FK_BranchId, 
CashPayment, 
CardPayment, 
Credit, 
FreeAmount, 
ReturnAmount, 
IsClosed, 
FielOne, 
FieldTwo, 
FieldThree, 
FieldFour, 
CashAccountId, 
CardAccountId, 
OnlineAccountId, 
IsSettiled, 
TotalPayment, 
VoucherNumber, 
SequenceNo, 
Terminal
 from PurInvMaster where 
(PurInvId = @PurInvId or @PurInvId is null ) And 
(PurInvNo = @PurInvNo or @PurInvNo is null ) And 
(InvType = @InvType or @InvType is null ) And 
(PurDate = @PurDate or @PurDate is null ) And 
(PurActId = @PurActId or @PurActId is null ) And 
(PartyAccId = @PartyAccId or @PartyAccId is null ) And 
(PriceCategoryId = @PriceCategoryId or @PriceCategoryId is null ) And 
(AdditionalAmnt = @AdditionalAmnt or @AdditionalAmnt is null ) And 
(Frieght = @Frieght or @Frieght is null ) And 
(TotalDiscount = @TotalDiscount or @TotalDiscount is null ) And 
(BillDisc = @BillDisc or @BillDisc is null ) And 
(TotalTax = @TotalTax or @TotalTax is null ) And 
(TotalAmnt = @TotalAmnt or @TotalAmnt is null ) And 
(JEId = @JEId or @JEId is null ) And 
(EntryDate = @EntryDate or @EntryDate is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(IGSTAmount = @IGSTAmount or @IGSTAmount is null ) And 
(SGSTAmount = @SGSTAmount or @SGSTAmount is null ) And 
(CGSTAmount = @CGSTAmount or @CGSTAmount is null ) And 
(RefInvNo = @RefInvNo or @RefInvNo is null ) And 
(Taxable = @Taxable or @Taxable is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(CashPayment = @CashPayment or @CashPayment is null ) And 
(CardPayment = @CardPayment or @CardPayment is null ) And 
(Credit = @Credit or @Credit is null ) And 
(FreeAmount = @FreeAmount or @FreeAmount is null ) And 
(ReturnAmount = @ReturnAmount or @ReturnAmount is null ) And 
(IsClosed = @IsClosed or @IsClosed is null ) And 
(FielOne = @FielOne or @FielOne is null ) And 
(FieldTwo = @FieldTwo or @FieldTwo is null ) And 
(FieldThree = @FieldThree or @FieldThree is null ) And 
(FieldFour = @FieldFour or @FieldFour is null ) And 
(CashAccountId = @CashAccountId or @CashAccountId is null ) And 
(CardAccountId = @CardAccountId or @CardAccountId is null ) And 
(OnlineAccountId = @OnlineAccountId or @OnlineAccountId is null ) And 
(IsSettiled = @IsSettiled or @IsSettiled is null ) And 
(TotalPayment = @TotalPayment or @TotalPayment is null ) And 
(VoucherNumber = @VoucherNumber or @VoucherNumber is null ) And 
(SequenceNo = @SequenceNo or @SequenceNo is null ) And 
(Terminal = @Terminal or @Terminal is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetPurInvSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetPurInvSettings]
(
@PurSettingsId int=NULL, @PurInvId int=NULL, @PartyAccId int=NULL, @ProId int=NULL, @ProQty decimal(24,4)=NULL, @UnitId int=NULL, @ProRate decimal(24,4)=NULL, @Disc decimal(24,4)=NULL, @DiscAmnt decimal(24,4)=NULL, @Gross decimal(24,4)=NULL, @NetAmnt decimal(24,4)=NULL, @Date datetime=NULL, @HSN nvarchar(20)=NULL, @GST decimal(24,4)=NULL, @GSTAMOUNT decimal(24,4)=NULL, @SGST decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGST decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @IGST decimal(24,4)=NULL, @IGSTAMOUNT decimal(24,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(100)=NULL 
)

 AS 
 BEGIN 

 Select 
PurSettingsId, 
PurInvId, 
PartyAccId, 
ProId, 
ProQty, 
UnitId, 
ProRate, 
Disc, 
DiscAmnt, 
Gross, 
NetAmnt, 
Date, 
HSN, 
GST, 
GSTAMOUNT, 
SGST, 
SGSTAmount, 
CGST, 
CGSTAmount, 
IGST, 
IGSTAMOUNT, 
FK_BatchId, 
Barcode
 from PurInvSettings where 
(PurSettingsId = @PurSettingsId or @PurSettingsId is null ) And 
(PurInvId = @PurInvId or @PurInvId is null ) And 
(PartyAccId = @PartyAccId or @PartyAccId is null ) And 
(ProId = @ProId or @ProId is null ) And 
(ProQty = @ProQty or @ProQty is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(ProRate = @ProRate or @ProRate is null ) And 
(Disc = @Disc or @Disc is null ) And 
(DiscAmnt = @DiscAmnt or @DiscAmnt is null ) And 
(Gross = @Gross or @Gross is null ) And 
(NetAmnt = @NetAmnt or @NetAmnt is null ) And 
(Date = @Date or @Date is null ) And 
(HSN = @HSN or @HSN is null ) And 
(GST = @GST or @GST is null ) And 
(GSTAMOUNT = @GSTAMOUNT or @GSTAMOUNT is null ) And 
(SGST = @SGST or @SGST is null ) And 
(SGSTAmount = @SGSTAmount or @SGSTAmount is null ) And 
(CGST = @CGST or @CGST is null ) And 
(CGSTAmount = @CGSTAmount or @CGSTAmount is null ) And 
(IGST = @IGST or @IGST is null ) And 
(IGSTAMOUNT = @IGSTAMOUNT or @IGSTAMOUNT is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) And 
(Barcode = @Barcode or @Barcode is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetPurRtnInvMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetPurRtnInvMaster]
(
@PurRtnInvId int=NULL, @PurRtnInvNo int=NULL, @PurRtnDate datetime=NULL, @PurRtnAcId int=NULL, @PartyAcId int=NULL, @PriceCategoryId int=NULL, @AdditionalAmnt decimal(24,4)=NULL, @Frieght decimal(24,4)=NULL, @TotalDiscount decimal(24,4)=NULL, @BillDisc decimal(24,4)=NULL, @TotalTax decimal(24,4)=NULL, @TotalAmnt decimal(24,4)=NULL, @JEId int=NULL, @EntryDate datetime=NULL, @Remarks nvarchar(400)=NULL, @IGSTAmount decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @Taxable decimal(24,4)=NULL, @FK_BranchId int=NULL, @SequenceNo int=NULL, @Terminal nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
PurRtnInvId, 
PurRtnInvNo, 
PurRtnDate, 
PurRtnAcId, 
PartyAcId, 
PriceCategoryId, 
AdditionalAmnt, 
Frieght, 
TotalDiscount, 
BillDisc, 
TotalTax, 
TotalAmnt, 
JEId, 
EntryDate, 
Remarks, 
IGSTAmount, 
SGSTAmount, 
CGSTAmount, 
Taxable, 
FK_BranchId, 
SequenceNo, 
Terminal
 from PurRtnInvMaster where 
(PurRtnInvId = @PurRtnInvId or @PurRtnInvId is null ) And 
(PurRtnInvNo = @PurRtnInvNo or @PurRtnInvNo is null ) And 
(PurRtnDate = @PurRtnDate or @PurRtnDate is null ) And 
(PurRtnAcId = @PurRtnAcId or @PurRtnAcId is null ) And 
(PartyAcId = @PartyAcId or @PartyAcId is null ) And 
(PriceCategoryId = @PriceCategoryId or @PriceCategoryId is null ) And 
(AdditionalAmnt = @AdditionalAmnt or @AdditionalAmnt is null ) And 
(Frieght = @Frieght or @Frieght is null ) And 
(TotalDiscount = @TotalDiscount or @TotalDiscount is null ) And 
(BillDisc = @BillDisc or @BillDisc is null ) And 
(TotalTax = @TotalTax or @TotalTax is null ) And 
(TotalAmnt = @TotalAmnt or @TotalAmnt is null ) And 
(JEId = @JEId or @JEId is null ) And 
(EntryDate = @EntryDate or @EntryDate is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(IGSTAmount = @IGSTAmount or @IGSTAmount is null ) And 
(SGSTAmount = @SGSTAmount or @SGSTAmount is null ) And 
(CGSTAmount = @CGSTAmount or @CGSTAmount is null ) And 
(Taxable = @Taxable or @Taxable is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(SequenceNo = @SequenceNo or @SequenceNo is null ) And 
(Terminal = @Terminal or @Terminal is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetPurRtnInvSttings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetPurRtnInvSttings]
(
@PurRtnSettId int=NULL, @PurRtnInvId int=NULL, @ProId int=NULL, @ProQty decimal(24,4)=NULL, @UnitId int=NULL, @ProRate decimal(24,4)=NULL, @Disc decimal(24,4)=NULL, @DiscAmnt decimal(24,4)=NULL, @Gross decimal(24,4)=NULL, @Tax decimal(24,4)=NULL, @TaxAmnt decimal(24,4)=NULL, @NetAmnt decimal(24,4)=NULL, @PartyAccId int=NULL, @Date datetime=NULL, @HSN nvarchar(20)=NULL, @GST decimal(24,4)=NULL, @GSTAMOUNT decimal(24,4)=NULL, @SGST decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGST decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @IGST decimal(24,4)=NULL, @IGSTAMOUNT decimal(24,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(100)=NULL 
)

 AS 
 BEGIN 

 Select 
PurRtnSettId, 
PurRtnInvId, 
ProId, 
ProQty, 
UnitId, 
ProRate, 
Disc, 
DiscAmnt, 
Gross, 
Tax, 
TaxAmnt, 
NetAmnt, 
PartyAccId, 
Date, 
HSN, 
GST, 
GSTAMOUNT, 
SGST, 
SGSTAmount, 
CGST, 
CGSTAmount, 
IGST, 
IGSTAMOUNT, 
FK_BatchId, 
Barcode
 from PurRtnInvSttings where 
(PurRtnSettId = @PurRtnSettId or @PurRtnSettId is null ) And 
(PurRtnInvId = @PurRtnInvId or @PurRtnInvId is null ) And 
(ProId = @ProId or @ProId is null ) And 
(ProQty = @ProQty or @ProQty is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(ProRate = @ProRate or @ProRate is null ) And 
(Disc = @Disc or @Disc is null ) And 
(DiscAmnt = @DiscAmnt or @DiscAmnt is null ) And 
(Gross = @Gross or @Gross is null ) And 
(Tax = @Tax or @Tax is null ) And 
(TaxAmnt = @TaxAmnt or @TaxAmnt is null ) And 
(NetAmnt = @NetAmnt or @NetAmnt is null ) And 
(PartyAccId = @PartyAccId or @PartyAccId is null ) And 
(Date = @Date or @Date is null ) And 
(HSN = @HSN or @HSN is null ) And 
(GST = @GST or @GST is null ) And 
(GSTAMOUNT = @GSTAMOUNT or @GSTAMOUNT is null ) And 
(SGST = @SGST or @SGST is null ) And 
(SGSTAmount = @SGSTAmount or @SGSTAmount is null ) And 
(CGST = @CGST or @CGST is null ) And 
(CGSTAmount = @CGSTAmount or @CGSTAmount is null ) And 
(IGST = @IGST or @IGST is null ) And 
(IGSTAMOUNT = @IGSTAMOUNT or @IGSTAMOUNT is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) And 
(Barcode = @Barcode or @Barcode is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetRateFormat]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetRateFormat]
(
@Id int=NULL, @Name int=NULL, @Value nvarchar(1)=NULL, @Alternative nvarchar(1)=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Name, 
Value, 
Alternative
 from RateFormat where 
(Id = @Id or @Id is null ) And 
(Name = @Name or @Name is null ) And 
(Value = @Value or @Value is null ) And 
(Alternative = @Alternative or @Alternative is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetRawmaterialMapping]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetRawmaterialMapping]
(
@Id int=NULL, @FK_ProductId int=NULL, @Quantity decimal(24,4)=NULL, @FK_UnitId int=NULL, @Rate decimal(24,4)=NULL, @NetAmount decimal(24,4)=NULL, @FK_ManufactureHdrId int=NULL, @FK_BatchId int=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
FK_ProductId, 
Quantity, 
FK_UnitId, 
Rate, 
NetAmount, 
FK_ManufactureHdrId, 
FK_BatchId
 from RawmaterialMapping where 
(Id = @Id or @Id is null ) And 
(FK_ProductId = @FK_ProductId or @FK_ProductId is null ) And 
(Quantity = @Quantity or @Quantity is null ) And 
(FK_UnitId = @FK_UnitId or @FK_UnitId is null ) And 
(Rate = @Rate or @Rate is null ) And 
(NetAmount = @NetAmount or @NetAmount is null ) And 
(FK_ManufactureHdrId = @FK_ManufactureHdrId or @FK_ManufactureHdrId is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetReceiptAndPayment]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetReceiptAndPayment]
(
@Id int=NULL, @IsSettiled bit=NULL, @CntrInvNo nvarchar(15)=NULL, @SalesDate datetime=NULL, @GrandDiscount decimal(24,4)=NULL, @TotalPayment decimal(24,4)=NULL, @Credit decimal(24,4)=NULL, @Payment decimal(24,4)=NULL, @Balance decimal(24,4)=NULL, @VchrNo nvarchar(15)=NULL, @InvoiceId int=NULL, @VchrId int=NULL, @InvoiceType nvarchar(20)=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
IsSettiled, 
CntrInvNo, 
SalesDate, 
GrandDiscount, 
TotalPayment, 
Credit, 
Payment, 
Balance, 
VchrNo, 
InvoiceId, 
VchrId, 
InvoiceType
 from ReceiptAndPayment where 
(Id = @Id or @Id is null ) And 
(IsSettiled = @IsSettiled or @IsSettiled is null ) And 
(CntrInvNo = @CntrInvNo or @CntrInvNo is null ) And 
(SalesDate = @SalesDate or @SalesDate is null ) And 
(GrandDiscount = @GrandDiscount or @GrandDiscount is null ) And 
(TotalPayment = @TotalPayment or @TotalPayment is null ) And 
(Credit = @Credit or @Credit is null ) And 
(Payment = @Payment or @Payment is null ) And 
(Balance = @Balance or @Balance is null ) And 
(VchrNo = @VchrNo or @VchrNo is null ) And 
(InvoiceId = @InvoiceId or @InvoiceId is null ) And 
(VchrId = @VchrId or @VchrId is null ) And 
(InvoiceType = @InvoiceType or @InvoiceType is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetReminderTB]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetReminderTB]
(
@ReminderId int=NULL, @RemainderHead nvarchar(50)=NULL, @Remainder nvarchar(400)=NULL, @CurrentDate nvarchar(20)=NULL, @ReminderDate nvarchar(20)=NULL, @UserId int=NULL, @TargetUserId int=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
ReminderId, 
RemainderHead, 
Remainder, 
CurrentDate, 
ReminderDate, 
UserId, 
TargetUserId, 
FK_BranchId
 from ReminderTB where 
(ReminderId = @ReminderId or @ReminderId is null ) And 
(RemainderHead = @RemainderHead or @RemainderHead is null ) And 
(Remainder = @Remainder or @Remainder is null ) And 
(CurrentDate = @CurrentDate or @CurrentDate is null ) And 
(ReminderDate = @ReminderDate or @ReminderDate is null ) And 
(UserId = @UserId or @UserId is null ) And 
(TargetUserId = @TargetUserId or @TargetUserId is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetRestaurantTable]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetRestaurantTable]
(
@TableId int=NULL, @TableName nvarchar(100)=NULL, @ChairCount int=NULL, @FloorName nvarchar(100)=NULL, @XPosition int=NULL, @YPostion int=NULL, @Image image=NULL, @FK_EmplyeeId int=NULL 
)

 AS 
 BEGIN 

 Select 
TableId, 
TableName, 
ChairCount, 
FloorName, 
XPosition, 
YPostion, 
Image, 
FK_EmplyeeId
 from RestaurantTable where 
(TableId = @TableId or @TableId is null ) And 
(TableName = @TableName or @TableName is null ) And 
(ChairCount = @ChairCount or @ChairCount is null ) And 
(FloorName = @FloorName or @FloorName is null ) And 
(XPosition = @XPosition or @XPosition is null ) And 
(YPostion = @YPostion or @YPostion is null ) And 
(FK_EmplyeeId = @FK_EmplyeeId or @FK_EmplyeeId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetRoot]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetRoot]
(
@RootId int=NULL, @RootName nvarchar(50)=NULL, @WeekDay nvarchar(20)=NULL, @SalesManId int=NULL, @DriverId int=NULL, @VehicleId int=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
RootId, 
RootName, 
WeekDay, 
SalesManId, 
DriverId, 
VehicleId, 
FK_BranchId
 from Root where 
(RootId = @RootId or @RootId is null ) And 
(RootName = @RootName or @RootName is null ) And 
(WeekDay = @WeekDay or @WeekDay is null ) And 
(SalesManId = @SalesManId or @SalesManId is null ) And 
(DriverId = @DriverId or @DriverId is null ) And 
(VehicleId = @VehicleId or @VehicleId is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetSalaryProcessMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSalaryProcessMaster]
(
@SPrcsMasterID int=NULL, @SPrcsVchrNo int=NULL, @SPrcsDate datetime=NULL, @SlryPrd nvarchar(50)=NULL, @DateFrom datetime=NULL, @DateTo datetime=NULL, @StaffType nvarchar(50)=NULL, @Description nvarchar(250)=NULL, @CashOrBankAcctId int=NULL, @CashOrBankAcctName nvarchar(50)=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
SPrcsMasterID, 
SPrcsVchrNo, 
SPrcsDate, 
SlryPrd, 
DateFrom, 
DateTo, 
StaffType, 
Description, 
CashOrBankAcctId, 
CashOrBankAcctName, 
FK_BranchId
 from SalaryProcessMaster where 
(SPrcsMasterID = @SPrcsMasterID or @SPrcsMasterID is null ) And 
(SPrcsVchrNo = @SPrcsVchrNo or @SPrcsVchrNo is null ) And 
(SPrcsDate = @SPrcsDate or @SPrcsDate is null ) And 
(SlryPrd = @SlryPrd or @SlryPrd is null ) And 
(DateFrom = @DateFrom or @DateFrom is null ) And 
(DateTo = @DateTo or @DateTo is null ) And 
(StaffType = @StaffType or @StaffType is null ) And 
(Description = @Description or @Description is null ) And 
(CashOrBankAcctId = @CashOrBankAcctId or @CashOrBankAcctId is null ) And 
(CashOrBankAcctName = @CashOrBankAcctName or @CashOrBankAcctName is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetSalesCounter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSalesCounter]
(
@SalesCounterId int=NULL, @SalesCounterName nvarchar(50)=NULL, @CashAccountId int=NULL, @Remarks nvarchar(400)=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
SalesCounterId, 
SalesCounterName, 
CashAccountId, 
Remarks, 
FK_BranchId
 from SalesCounter where 
(SalesCounterId = @SalesCounterId or @SalesCounterId is null ) And 
(SalesCounterName = @SalesCounterName or @SalesCounterName is null ) And 
(CashAccountId = @CashAccountId or @CashAccountId is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetSalesEstimateMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSalesEstimateMaster]
(
@SalesInvId int=NULL, @SalesInvNo int=NULL, @InvType nvarchar(50)=NULL, @SalesDate datetime=NULL, @SalesAccountId int=NULL, @PartyAccountId int=NULL, @RouteId int=NULL, @VehId int=NULL, @StafId int=NULL, @PriceCategoryId int=NULL, @AditionalAmount decimal(24,4)=NULL, @Frieght decimal(24,4)=NULL, @TotalDiscount decimal(24,4)=NULL, @BillDiscount decimal(24,4)=NULL, @GrandDiscount decimal(24,4)=NULL, @JEId int=NULL, @EntryDate datetime=NULL, @Remarks nvarchar(50)=NULL, @IGSTAmount decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @TaxableAmount decimal(24,4)=NULL, @FYearID int=NULL, @FloodCess decimal(24,4)=NULL, @TotalTaxAmount decimal(24,4)=NULL, @FK_BranchId int=NULL, @CntrInvNo nvarchar(10)=NULL, @SequenceNo int=NULL, @CashPayment decimal(24,8)=NULL, @CardPayment decimal(24,8)=NULL, @Credit decimal(24,8)=NULL, @FielOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @Terminal nvarchar(25)=NULL, @IsClosed bit=NULL, @FreeAmount decimal(24,4)=NULL, @ReturnAmount decimal(24,4)=NULL, @OtherPayment decimal(24,4)=NULL, @CashAccountId int=NULL, @CardAccountId int=NULL, @OnlineAccountId int=NULL, @IsSettiled bit=NULL, @TotalPayment decimal(24,8)=NULL, @VoucherNumber nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
SalesInvId, 
SalesInvNo, 
InvType, 
SalesDate, 
SalesAccountId, 
PartyAccountId, 
RouteId, 
VehId, 
StafId, 
PriceCategoryId, 
AditionalAmount, 
Frieght, 
TotalDiscount, 
BillDiscount, 
GrandDiscount, 
JEId, 
EntryDate, 
Remarks, 
IGSTAmount, 
SGSTAmount, 
CGSTAmount, 
TaxableAmount, 
FYearID, 
FloodCess, 
TotalTaxAmount, 
FK_BranchId, 
CntrInvNo, 
SequenceNo, 
CashPayment, 
CardPayment, 
Credit, 
FielOne, 
FieldTwo, 
FieldThree, 
FieldFour, 
Terminal, 
IsClosed, 
FreeAmount, 
ReturnAmount, 
OtherPayment, 
CashAccountId, 
CardAccountId, 
OnlineAccountId, 
IsSettiled, 
TotalPayment, 
VoucherNumber
 from SalesEstimateMaster where 
(SalesInvId = @SalesInvId or @SalesInvId is null ) And 
(SalesInvNo = @SalesInvNo or @SalesInvNo is null ) And 
(InvType = @InvType or @InvType is null ) And 
(SalesDate = @SalesDate or @SalesDate is null ) And 
(SalesAccountId = @SalesAccountId or @SalesAccountId is null ) And 
(PartyAccountId = @PartyAccountId or @PartyAccountId is null ) And 
(RouteId = @RouteId or @RouteId is null ) And 
(VehId = @VehId or @VehId is null ) And 
(StafId = @StafId or @StafId is null ) And 
(PriceCategoryId = @PriceCategoryId or @PriceCategoryId is null ) And 
(AditionalAmount = @AditionalAmount or @AditionalAmount is null ) And 
(Frieght = @Frieght or @Frieght is null ) And 
(TotalDiscount = @TotalDiscount or @TotalDiscount is null ) And 
(BillDiscount = @BillDiscount or @BillDiscount is null ) And 
(GrandDiscount = @GrandDiscount or @GrandDiscount is null ) And 
(JEId = @JEId or @JEId is null ) And 
(EntryDate = @EntryDate or @EntryDate is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(IGSTAmount = @IGSTAmount or @IGSTAmount is null ) And 
(SGSTAmount = @SGSTAmount or @SGSTAmount is null ) And 
(CGSTAmount = @CGSTAmount or @CGSTAmount is null ) And 
(TaxableAmount = @TaxableAmount or @TaxableAmount is null ) And 
(FYearID = @FYearID or @FYearID is null ) And 
(FloodCess = @FloodCess or @FloodCess is null ) And 
(TotalTaxAmount = @TotalTaxAmount or @TotalTaxAmount is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(CntrInvNo = @CntrInvNo or @CntrInvNo is null ) And 
(SequenceNo = @SequenceNo or @SequenceNo is null ) And 
(CashPayment = @CashPayment or @CashPayment is null ) And 
(CardPayment = @CardPayment or @CardPayment is null ) And 
(Credit = @Credit or @Credit is null ) And 
(FielOne = @FielOne or @FielOne is null ) And 
(FieldTwo = @FieldTwo or @FieldTwo is null ) And 
(FieldThree = @FieldThree or @FieldThree is null ) And 
(FieldFour = @FieldFour or @FieldFour is null ) And 
(Terminal = @Terminal or @Terminal is null ) And 
(IsClosed = @IsClosed or @IsClosed is null ) And 
(FreeAmount = @FreeAmount or @FreeAmount is null ) And 
(ReturnAmount = @ReturnAmount or @ReturnAmount is null ) And 
(OtherPayment = @OtherPayment or @OtherPayment is null ) And 
(CashAccountId = @CashAccountId or @CashAccountId is null ) And 
(CardAccountId = @CardAccountId or @CardAccountId is null ) And 
(OnlineAccountId = @OnlineAccountId or @OnlineAccountId is null ) And 
(IsSettiled = @IsSettiled or @IsSettiled is null ) And 
(TotalPayment = @TotalPayment or @TotalPayment is null ) And 
(VoucherNumber = @VoucherNumber or @VoucherNumber is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetSalesEstimateSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSalesEstimateSettings]
(
@SalesSettingsId int=NULL, @SalesInvId int=NULL, @PartyAccountId int=NULL, @ProductId int=NULL, @ProQty decimal(18,4)=NULL, @UnitId int=NULL, @ProRate decimal(18,4)=NULL, @ProDiscont decimal(18,4)=NULL, @DiscAmnt decimal(18,4)=NULL, @GrossAmnt decimal(18,4)=NULL, @NetAmnt decimal(18,4)=NULL, @Date datetime=NULL, @HSN nvarchar(20)=NULL, @GST decimal(18,4)=NULL, @GSTAMOUNT decimal(18,4)=NULL, @SGST decimal(18,4)=NULL, @SGSTAmount decimal(18,4)=NULL, @CGST decimal(18,4)=NULL, @CGSTAmount decimal(18,4)=NULL, @IGST decimal(18,4)=NULL, @IGSTAMOUNT decimal(18,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(20)=NULL, @ItemCalory decimal(18,4)=NULL, @EmployeeID int=NULL, @EmployeeCommision decimal(18,4)=NULL, @AgentId int=NULL, @AgentCommission decimal(18,4)=NULL, @ProductName nvarchar(50)=NULL 
)

 AS 
 BEGIN 

 Select 
SalesSettingsId, 
SalesInvId, 
PartyAccountId, 
ProductId, 
ProQty, 
UnitId, 
ProRate, 
ProDiscont, 
DiscAmnt, 
GrossAmnt, 
NetAmnt, 
Date, 
HSN, 
GST, 
GSTAMOUNT, 
SGST, 
SGSTAmount, 
CGST, 
CGSTAmount, 
IGST, 
IGSTAMOUNT, 
FK_BatchId, 
Barcode, 
ItemCalory, 
EmployeeID, 
EmployeeCommision, 
AgentId, 
AgentCommission, 
ProductName
 from SalesEstimateSettings where 
(SalesSettingsId = @SalesSettingsId or @SalesSettingsId is null ) And 
(SalesInvId = @SalesInvId or @SalesInvId is null ) And 
(PartyAccountId = @PartyAccountId or @PartyAccountId is null ) And 
(ProductId = @ProductId or @ProductId is null ) And 
(ProQty = @ProQty or @ProQty is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(ProRate = @ProRate or @ProRate is null ) And 
(ProDiscont = @ProDiscont or @ProDiscont is null ) And 
(DiscAmnt = @DiscAmnt or @DiscAmnt is null ) And 
(GrossAmnt = @GrossAmnt or @GrossAmnt is null ) And 
(NetAmnt = @NetAmnt or @NetAmnt is null ) And 
(Date = @Date or @Date is null ) And 
(HSN = @HSN or @HSN is null ) And 
(GST = @GST or @GST is null ) And 
(GSTAMOUNT = @GSTAMOUNT or @GSTAMOUNT is null ) And 
(SGST = @SGST or @SGST is null ) And 
(SGSTAmount = @SGSTAmount or @SGSTAmount is null ) And 
(CGST = @CGST or @CGST is null ) And 
(CGSTAmount = @CGSTAmount or @CGSTAmount is null ) And 
(IGST = @IGST or @IGST is null ) And 
(IGSTAMOUNT = @IGSTAMOUNT or @IGSTAMOUNT is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) And 
(Barcode = @Barcode or @Barcode is null ) And 
(ItemCalory = @ItemCalory or @ItemCalory is null ) And 
(EmployeeID = @EmployeeID or @EmployeeID is null ) And 
(EmployeeCommision = @EmployeeCommision or @EmployeeCommision is null ) And 
(AgentId = @AgentId or @AgentId is null ) And 
(AgentCommission = @AgentCommission or @AgentCommission is null ) And 
(ProductName = @ProductName or @ProductName is null ) 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetSalesOrderMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSalesOrderMaster]
(
@SalesOrderMasterId int=NULL, @VchrNo int=NULL, @CustomerId int=NULL, @SalesAccountId int=NULL, @SalesManId int=NULL, @RouteId int=NULL, @PriceCategoryId int=NULL, @Remarks nvarchar(400)=NULL, @NetAmount decimal(24,4)=NULL, @AdditionalAmnt decimal(24,4)=NULL, @Freight decimal(24,4)=NULL, @BillDiscount decimal(24,4)=NULL, @AddDiscount decimal(24,4)=NULL, @GrandTotal decimal(24,4)=NULL, @InvType nvarchar(50)=NULL, @JEid int=NULL, @EntryDate datetime=NULL, @InvDate datetime=NULL, @Status bit=NULL, @Taxable decimal(24,4)=NULL, @TotalTax decimal(24,4)=NULL, @FK_BranchId int=NULL, @TargetDate datetime=NULL, @AdvanceAmount decimal(24,4)=NULL, @FieldOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @FieldFive nvarchar(100)=NULL 
)

 AS 
 BEGIN 

 Select 
SalesOrderMasterId, 
VchrNo, 
CustomerId, 
SalesAccountId, 
SalesManId, 
RouteId, 
PriceCategoryId, 
Remarks, 
NetAmount, 
AdditionalAmnt, 
Freight, 
BillDiscount, 
AddDiscount, 
GrandTotal, 
InvType, 
JEid, 
EntryDate, 
InvDate, 
Status, 
Taxable, 
TotalTax, 
FK_BranchId, 
TargetDate, 
AdvanceAmount, 
FieldOne, 
FieldTwo, 
FieldThree, 
FieldFour, 
FieldFive
 from SalesOrderMaster where 
(SalesOrderMasterId = @SalesOrderMasterId or @SalesOrderMasterId is null ) And 
(VchrNo = @VchrNo or @VchrNo is null ) And 
(CustomerId = @CustomerId or @CustomerId is null ) And 
(SalesAccountId = @SalesAccountId or @SalesAccountId is null ) And 
(SalesManId = @SalesManId or @SalesManId is null ) And 
(RouteId = @RouteId or @RouteId is null ) And 
(PriceCategoryId = @PriceCategoryId or @PriceCategoryId is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(NetAmount = @NetAmount or @NetAmount is null ) And 
(AdditionalAmnt = @AdditionalAmnt or @AdditionalAmnt is null ) And 
(Freight = @Freight or @Freight is null ) And 
(BillDiscount = @BillDiscount or @BillDiscount is null ) And 
(AddDiscount = @AddDiscount or @AddDiscount is null ) And 
(GrandTotal = @GrandTotal or @GrandTotal is null ) And 
(InvType = @InvType or @InvType is null ) And 
(JEid = @JEid or @JEid is null ) And 
(EntryDate = @EntryDate or @EntryDate is null ) And 
(InvDate = @InvDate or @InvDate is null ) And 
(Status = @Status or @Status is null ) And 
(Taxable = @Taxable or @Taxable is null ) And 
(TotalTax = @TotalTax or @TotalTax is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(TargetDate = @TargetDate or @TargetDate is null ) And 
(AdvanceAmount = @AdvanceAmount or @AdvanceAmount is null ) And 
(FieldOne = @FieldOne or @FieldOne is null ) And 
(FieldTwo = @FieldTwo or @FieldTwo is null ) And 
(FieldThree = @FieldThree or @FieldThree is null ) And 
(FieldFour = @FieldFour or @FieldFour is null ) And 
(FieldFive = @FieldFive or @FieldFive is null ) 

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spGetSalesOrderSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSalesOrderSettings]
(
@SalesOrderSettingsId int=NULL, @SalesOrderMasterId int=NULL, @PartyAccountId int=NULL, @ProductId int=NULL, @ProQty decimal(24,4)=NULL, @UnitId int=NULL, @ProRate decimal(24,4)=NULL, @ProDiscont decimal(24,4)=NULL, @DiscAmnt decimal(24,4)=NULL, @GrossAmnt decimal(24,4)=NULL, @NetAmnt decimal(24,4)=NULL, @Taxable decimal(24,4)=NULL, @Tax decimal(24,4)=NULL, @TaxAmount decimal(24,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(100)=NULL 
)

 AS 
 BEGIN 

 Select 
SalesOrderSettingsId, 
SalesOrderMasterId, 
PartyAccountId, 
ProductId, 
ProQty, 
UnitId, 
ProRate, 
ProDiscont, 
DiscAmnt, 
GrossAmnt, 
NetAmnt, 
Taxable, 
Tax, 
TaxAmount, 
FK_BatchId, 
Barcode
 from SalesOrderSettings where 
(SalesOrderSettingsId = @SalesOrderSettingsId or @SalesOrderSettingsId is null ) And 
(SalesOrderMasterId = @SalesOrderMasterId or @SalesOrderMasterId is null ) And 
(PartyAccountId = @PartyAccountId or @PartyAccountId is null ) And 
(ProductId = @ProductId or @ProductId is null ) And 
(ProQty = @ProQty or @ProQty is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(ProRate = @ProRate or @ProRate is null ) And 
(ProDiscont = @ProDiscont or @ProDiscont is null ) And 
(DiscAmnt = @DiscAmnt or @DiscAmnt is null ) And 
(GrossAmnt = @GrossAmnt or @GrossAmnt is null ) And 
(NetAmnt = @NetAmnt or @NetAmnt is null ) And 
(Taxable = @Taxable or @Taxable is null ) And 
(Tax = @Tax or @Tax is null ) And 
(TaxAmount = @TaxAmount or @TaxAmount is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) And 
(Barcode = @Barcode or @Barcode is null ) 

 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spGetSalesRtnMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSalesRtnMaster]
(
@SlsRtrnId int=NULL, @SlRtnInvNo int=NULL, @SlsRtnDate datetime=NULL, @SlsRtnAccountId int=NULL, @PartyAccountId int=NULL, @RouteId int=NULL, @VehId int=NULL, @StaffId int=NULL, @PriceCategoryId int=NULL, @AditionalAmnt decimal(24,4)=NULL, @Frieght decimal(24,4)=NULL, @TotalDiscount decimal(24,4)=NULL, @BillDiscount decimal(24,4)=NULL, @TotalAmount decimal(24,4)=NULL, @JeId int=NULL, @EntryDate datetime=NULL, @Remarks nvarchar(400)=NULL, @IGSTAmount decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @FYearID int=NULL, @TotalTax decimal(24,4)=NULL, @Taxable decimal(24,4)=NULL, @FK_BranchId int=NULL, @SequenceNo int=NULL, @Terminal nvarchar(25)=NULL, @RefInvNo nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
SlsRtrnId, 
SlRtnInvNo, 
SlsRtnDate, 
SlsRtnAccountId, 
PartyAccountId, 
RouteId, 
VehId, 
StaffId, 
PriceCategoryId, 
AditionalAmnt, 
Frieght, 
TotalDiscount, 
BillDiscount, 
TotalAmount, 
JeId, 
EntryDate, 
Remarks, 
IGSTAmount, 
SGSTAmount, 
CGSTAmount, 
FYearID, 
TotalTax, 
Taxable, 
FK_BranchId, 
SequenceNo, 
Terminal, 
RefInvNo
 from SalesRtnMaster where 
(SlsRtrnId = @SlsRtrnId or @SlsRtrnId is null ) And 
(SlRtnInvNo = @SlRtnInvNo or @SlRtnInvNo is null ) And 
(SlsRtnDate = @SlsRtnDate or @SlsRtnDate is null ) And 
(SlsRtnAccountId = @SlsRtnAccountId or @SlsRtnAccountId is null ) And 
(PartyAccountId = @PartyAccountId or @PartyAccountId is null ) And 
(RouteId = @RouteId or @RouteId is null ) And 
(VehId = @VehId or @VehId is null ) And 
(StaffId = @StaffId or @StaffId is null ) And 
(PriceCategoryId = @PriceCategoryId or @PriceCategoryId is null ) And 
(AditionalAmnt = @AditionalAmnt or @AditionalAmnt is null ) And 
(Frieght = @Frieght or @Frieght is null ) And 
(TotalDiscount = @TotalDiscount or @TotalDiscount is null ) And 
(BillDiscount = @BillDiscount or @BillDiscount is null ) And 
(TotalAmount = @TotalAmount or @TotalAmount is null ) And 
(JeId = @JeId or @JeId is null ) And 
(EntryDate = @EntryDate or @EntryDate is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(IGSTAmount = @IGSTAmount or @IGSTAmount is null ) And 
(SGSTAmount = @SGSTAmount or @SGSTAmount is null ) And 
(CGSTAmount = @CGSTAmount or @CGSTAmount is null ) And 
(FYearID = @FYearID or @FYearID is null ) And 
(TotalTax = @TotalTax or @TotalTax is null ) And 
(Taxable = @Taxable or @Taxable is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(SequenceNo = @SequenceNo or @SequenceNo is null ) And 
(Terminal = @Terminal or @Terminal is null ) And 
(RefInvNo = @RefInvNo or @RefInvNo is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetSelectedFYear]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSelectedFYear]
(
@SlectedFY int=NULL, @FyearId int=NULL 
)

 AS 
 BEGIN 

 Select 
SlectedFY, 
FyearId
 from SelectedFYear where 
(SlectedFY = @SlectedFY or @SlectedFY is null ) And 
(FyearId = @FyearId or @FyearId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetSelf]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSelf]
(
@SelfId int=NULL, @GodownId int=NULL, @SelfName nvarchar(50)=NULL 
)

 AS 
 BEGIN 

 Select 
SelfId, 
GodownId, 
SelfName
 from Self where 
(SelfId = @SelfId or @SelfId is null ) And 
(GodownId = @GodownId or @GodownId is null ) And 
(SelfName = @SelfName or @SelfName is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSettings]
(
@Id int=NULL, @Name nvarchar(25)=NULL, @Caption nvarchar(100)=NULL, @Remarks nvarchar(100)=NULL, @BitValue bit=NULL, @StringValue nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Name, 
Caption, 
Remarks, 
BitValue, 
StringValue
 from Settings where 
(Id = @Id or @Id is null ) And 
(Name = @Name or @Name is null ) And 
(Caption = @Caption or @Caption is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(BitValue = @BitValue or @BitValue is null ) And 
(StringValue = @StringValue or @StringValue is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetSlryPrcsSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSlryPrcsSettings]
(
@SPrcsStngsID int=NULL, @SprcsMasterID int=NULL, @EmpID int=NULL, @EmpName nvarchar(50)=NULL, @LeavsTaken decimal(24,4)=NULL, @CasualLeave decimal(24,4)=NULL, @BasicSalary decimal(24,4)=NULL, @LeaveDiduction decimal(24,4)=NULL, @Incentive decimal(24,4)=NULL, @OtherAmnt decimal(24,4)=NULL, @SalaryCut decimal(24,4)=NULL, @OtherDiduction decimal(24,4)=NULL, @NetSalary decimal(24,4)=NULL, @SlryAdvance decimal(24,4)=NULL, @PaidAmnt decimal(24,4)=NULL, @Balance decimal(24,4)=NULL, @Remarks nvarchar(250)=NULL 
)

 AS 
 BEGIN 

 Select 
SPrcsStngsID, 
SprcsMasterID, 
EmpID, 
EmpName, 
LeavsTaken, 
CasualLeave, 
BasicSalary, 
LeaveDiduction, 
Incentive, 
OtherAmnt, 
SalaryCut, 
OtherDiduction, 
NetSalary, 
SlryAdvance, 
PaidAmnt, 
Balance, 
Remarks
 from SlryPrcsSettings where 
(SPrcsStngsID = @SPrcsStngsID or @SPrcsStngsID is null ) And 
(SprcsMasterID = @SprcsMasterID or @SprcsMasterID is null ) And 
(EmpID = @EmpID or @EmpID is null ) And 
(EmpName = @EmpName or @EmpName is null ) And 
(LeavsTaken = @LeavsTaken or @LeavsTaken is null ) And 
(CasualLeave = @CasualLeave or @CasualLeave is null ) And 
(BasicSalary = @BasicSalary or @BasicSalary is null ) And 
(LeaveDiduction = @LeaveDiduction or @LeaveDiduction is null ) And 
(Incentive = @Incentive or @Incentive is null ) And 
(OtherAmnt = @OtherAmnt or @OtherAmnt is null ) And 
(SalaryCut = @SalaryCut or @SalaryCut is null ) And 
(OtherDiduction = @OtherDiduction or @OtherDiduction is null ) And 
(NetSalary = @NetSalary or @NetSalary is null ) And 
(SlryAdvance = @SlryAdvance or @SlryAdvance is null ) And 
(PaidAmnt = @PaidAmnt or @PaidAmnt is null ) And 
(Balance = @Balance or @Balance is null ) And 
(Remarks = @Remarks or @Remarks is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetSlsRtnSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetSlsRtnSettings]
(
@SlsrtnSettingsID int=NULL, @SlsRtnId int=NULL, @ProductId int=NULL, @ProQty decimal(24,4)=NULL, @UnitId int=NULL, @ProRate decimal(24,4)=NULL, @ProDis decimal(24,4)=NULL, @DiscAmnt decimal(24,4)=NULL, @GrossAmnt decimal(24,4)=NULL, @isDamaged bit=NULL, @NetAmnt decimal(24,4)=NULL, @PartyAccountId int=NULL, @Date datetime=NULL, @HSN int=NULL, @GST decimal(24,4)=NULL, @GSTAMOUNT decimal(24,4)=NULL, @SGST decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGST decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @IGST decimal(24,4)=NULL, @IGSTAMOUNT decimal(24,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(100)=NULL 
)

 AS 
 BEGIN 

 Select 
SlsrtnSettingsID, 
SlsRtnId, 
ProductId, 
ProQty, 
UnitId, 
ProRate, 
ProDis, 
DiscAmnt, 
GrossAmnt, 
isDamaged, 
NetAmnt, 
PartyAccountId, 
Date, 
HSN, 
GST, 
GSTAMOUNT, 
SGST, 
SGSTAmount, 
CGST, 
CGSTAmount, 
IGST, 
IGSTAMOUNT, 
FK_BatchId, 
Barcode
 from SlsRtnSettings where 
(SlsrtnSettingsID = @SlsrtnSettingsID or @SlsrtnSettingsID is null ) And 
(SlsRtnId = @SlsRtnId or @SlsRtnId is null ) And 
(ProductId = @ProductId or @ProductId is null ) And 
(ProQty = @ProQty or @ProQty is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(ProRate = @ProRate or @ProRate is null ) And 
(ProDis = @ProDis or @ProDis is null ) And 
(DiscAmnt = @DiscAmnt or @DiscAmnt is null ) And 
(GrossAmnt = @GrossAmnt or @GrossAmnt is null ) And 
(isDamaged = @isDamaged or @isDamaged is null ) And 
(NetAmnt = @NetAmnt or @NetAmnt is null ) And 
(PartyAccountId = @PartyAccountId or @PartyAccountId is null ) And 
(Date = @Date or @Date is null ) And 
(HSN = @HSN or @HSN is null ) And 
(GST = @GST or @GST is null ) And 
(GSTAMOUNT = @GSTAMOUNT or @GSTAMOUNT is null ) And 
(SGST = @SGST or @SGST is null ) And 
(SGSTAmount = @SGSTAmount or @SGSTAmount is null ) And 
(CGST = @CGST or @CGST is null ) And 
(CGSTAmount = @CGSTAmount or @CGSTAmount is null ) And 
(IGST = @IGST or @IGST is null ) And 
(IGSTAMOUNT = @IGSTAMOUNT or @IGSTAMOUNT is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) And 
(Barcode = @Barcode or @Barcode is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetStockAdjusterMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetStockAdjusterMaster]
(
@stockAdjusterMasterId int=NULL, @StockAdjusterMasterCode int=NULL, @Date datetime=NULL, @Remarks nvarchar(400)=NULL, @ProtGroup int=NULL, @ProBrand int=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
stockAdjusterMasterId, 
StockAdjusterMasterCode, 
Date, 
Remarks, 
ProtGroup, 
ProBrand, 
FK_BranchId
 from StockAdjusterMaster where 
(stockAdjusterMasterId = @stockAdjusterMasterId or @stockAdjusterMasterId is null ) And 
(StockAdjusterMasterCode = @StockAdjusterMasterCode or @StockAdjusterMasterCode is null ) And 
(Date = @Date or @Date is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(ProtGroup = @ProtGroup or @ProtGroup is null ) And 
(ProBrand = @ProBrand or @ProBrand is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetStockAdjustSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetStockAdjustSettings]
(
@stockAdjustSettingsId int=NULL, @stockAdjusterMasterId int=NULL, @productId int=NULL, @currSystemStock decimal(24,4)=NULL, @curActualStock decimal(24,4)=NULL, @unitId int=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(100)=NULL 
)

 AS 
 BEGIN 

 Select 
stockAdjustSettingsId, 
stockAdjusterMasterId, 
productId, 
currSystemStock, 
curActualStock, 
unitId, 
FK_BatchId, 
Barcode
 from StockAdjustSettings where 
(stockAdjustSettingsId = @stockAdjustSettingsId or @stockAdjustSettingsId is null ) And 
(stockAdjusterMasterId = @stockAdjusterMasterId or @stockAdjusterMasterId is null ) And 
(productId = @productId or @productId is null ) And 
(currSystemStock = @currSystemStock or @currSystemStock is null ) And 
(curActualStock = @curActualStock or @curActualStock is null ) And 
(unitId = @unitId or @unitId is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) And 
(Barcode = @Barcode or @Barcode is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetStockTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetStockTranLog]
(
@STranId int=NULL, @ProductId int=NULL, @VchrId int=NULL, @VchrNo int=NULL, @VchrDate datetime=NULL, @VchrType nvarchar(50)=NULL, @VchrFormType nvarchar(50)=NULL, @AccountId int=NULL, @InQuantity decimal(24,4)=NULL, @OutQuantity decimal(24,4)=NULL, @UnitId int=NULL, @Narration nvarchar(400)=NULL, @UserId int=NULL, @TranDate datetime=NULL, @FK_BatchId int=NULL, @FK_BranchId int=NULL, @Barcode nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
STranId, 
ProductId, 
VchrId, 
VchrNo, 
VchrDate, 
VchrType, 
VchrFormType, 
AccountId, 
InQuantity, 
OutQuantity, 
UnitId, 
Narration, 
UserId, 
TranDate, 
FK_BatchId, 
FK_BranchId, 
Barcode
 from StockTranLog where 
(STranId = @STranId or @STranId is null ) And 
(ProductId = @ProductId or @ProductId is null ) And 
(VchrId = @VchrId or @VchrId is null ) And 
(VchrNo = @VchrNo or @VchrNo is null ) And 
(VchrDate = @VchrDate or @VchrDate is null ) And 
(VchrType = @VchrType or @VchrType is null ) And 
(VchrFormType = @VchrFormType or @VchrFormType is null ) And 
(AccountId = @AccountId or @AccountId is null ) And 
(InQuantity = @InQuantity or @InQuantity is null ) And 
(OutQuantity = @OutQuantity or @OutQuantity is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(Narration = @Narration or @Narration is null ) And 
(UserId = @UserId or @UserId is null ) And 
(TranDate = @TranDate or @TranDate is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(Barcode = @Barcode or @Barcode is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetStockTranSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetStockTranSettings]
(
@StockTranSettingsId int=NULL, @StockTransferId int=NULL, @ProductId int=NULL, @ProductName nvarchar(50)=NULL, @ProQuantity decimal(24,4)=NULL, @UnitId int=NULL, @CompanyName nvarchar(50)=NULL, @CompanyPlace nvarchar(50)=NULL, @ProRate decimal(24,4)=NULL, @Total decimal(24,4)=NULL, @FK_BatchId int=NULL 
)

 AS 
 BEGIN 

 Select 
StockTranSettingsId, 
StockTransferId, 
ProductId, 
ProductName, 
ProQuantity, 
UnitId, 
CompanyName, 
CompanyPlace, 
ProRate, 
Total, 
FK_BatchId
 from StockTranSettings where 
(StockTranSettingsId = @StockTranSettingsId or @StockTranSettingsId is null ) And 
(StockTransferId = @StockTransferId or @StockTransferId is null ) And 
(ProductId = @ProductId or @ProductId is null ) And 
(ProductName = @ProductName or @ProductName is null ) And 
(ProQuantity = @ProQuantity or @ProQuantity is null ) And 
(UnitId = @UnitId or @UnitId is null ) And 
(CompanyName = @CompanyName or @CompanyName is null ) And 
(CompanyPlace = @CompanyPlace or @CompanyPlace is null ) And 
(ProRate = @ProRate or @ProRate is null ) And 
(Total = @Total or @Total is null ) And 
(FK_BatchId = @FK_BatchId or @FK_BatchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetStockTransferMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetStockTransferMaster]
(
@StockTransferId int=NULL, @StockTransferCode int=NULL, @TransferDate datetime=NULL, @FromGodownId int=NULL, @FromGodownName nvarchar(400)=NULL, @ToGodownId int=NULL, @ToGodownName nvarchar(400)=NULL, @Remarks nvarchar(400)=NULL, @UserId int=NULL, @EntryDate datetime=NULL, @CompanyName nvarchar(50)=NULL, @CompanyPlace nvarchar(50)=NULL, @TotalAmnt decimal(24,4)=NULL, @TotalQty decimal(24,4)=NULL, @TranType nvarchar(50)=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
StockTransferId, 
StockTransferCode, 
TransferDate, 
FromGodownId, 
FromGodownName, 
ToGodownId, 
ToGodownName, 
Remarks, 
UserId, 
EntryDate, 
CompanyName, 
CompanyPlace, 
TotalAmnt, 
TotalQty, 
TranType, 
FK_BranchId
 from StockTransferMaster where 
(StockTransferId = @StockTransferId or @StockTransferId is null ) And 
(StockTransferCode = @StockTransferCode or @StockTransferCode is null ) And 
(TransferDate = @TransferDate or @TransferDate is null ) And 
(FromGodownId = @FromGodownId or @FromGodownId is null ) And 
(FromGodownName = @FromGodownName or @FromGodownName is null ) And 
(ToGodownId = @ToGodownId or @ToGodownId is null ) And 
(ToGodownName = @ToGodownName or @ToGodownName is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(UserId = @UserId or @UserId is null ) And 
(EntryDate = @EntryDate or @EntryDate is null ) And 
(CompanyName = @CompanyName or @CompanyName is null ) And 
(CompanyPlace = @CompanyPlace or @CompanyPlace is null ) And 
(TotalAmnt = @TotalAmnt or @TotalAmnt is null ) And 
(TotalQty = @TotalQty or @TotalQty is null ) And 
(TranType = @TranType or @TranType is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetStockValueForChart]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetStockValueForChart]
(
@BranchId int=null,
@Date datetime=null
)
AS 
 BEGIN 
select isnull(sum(stl.InQuantity-stl.OutQuantity),0) * isnull(pd.SellingPrice,0) as StockValue 
 From StockTranLog stl 
 left Join Product pd on stl.ProductId = pd.ProductId
 where (stl.VchrDate<=@Date or @Date is null) and 
 (stl.FK_BranchId=@BranchId or @BranchId is null)
 group by pd.SellingPrice
  END 
  
GO
/****** Object:  StoredProcedure [dbo].[spGetTaxCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetTaxCategory]
(
@TCategoryId int=NULL, @TCategoryName nvarchar(20)=NULL, @SalesVat decimal(24,4)=NULL, @PurVat decimal(24,4)=NULL, @SaleCst decimal(24,4)=NULL, @PurCst decimal(24,4)=NULL, @SaleCess decimal(24,4)=NULL, @PurCess decimal(24,4)=NULL, @SalesXDuty decimal(24,4)=NULL, @PurXDuty decimal(24,4)=NULL 
)

 AS 
 BEGIN 

 Select 
TCategoryId, 
TCategoryName, 
SalesVat, 
PurVat, 
SaleCst, 
PurCst, 
SaleCess, 
PurCess, 
SalesXDuty, 
PurXDuty
 from TaxCategory where 
(TCategoryId = @TCategoryId or @TCategoryId is null ) And 
(TCategoryName = @TCategoryName or @TCategoryName is null ) And 
(SalesVat = @SalesVat or @SalesVat is null ) And 
(PurVat = @PurVat or @PurVat is null ) And 
(SaleCst = @SaleCst or @SaleCst is null ) And 
(PurCst = @PurCst or @PurCst is null ) And 
(SaleCess = @SaleCess or @SaleCess is null ) And 
(PurCess = @PurCess or @PurCess is null ) And 
(SalesXDuty = @SalesXDuty or @SalesXDuty is null ) And 
(PurXDuty = @PurXDuty or @PurXDuty is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetTaxForCloseCash]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetTaxForCloseCash]
(
@SequenceNo int null, 
@Terminal nvarchar(50) null,
@BranchId int = null
)
As
Begin
Select tc.TCategoryName as TaxCategory, sum(isnull(ses.GSTAMOUNT,0)) as Tax, sum(isnull(ses.NetAmnt,0)-isnull(ses.GSTAMOUNT,0)) as Net
From SalesEstimateSettings ses
inner join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
left join Product pd on ses.ProductId = pd.ProductId
left join TaxCategory tc on pd.TCategoryId = tc.TCategoryId
where (sem.FK_BranchId = @BranchId or @BranchId is null) and
sem.SequenceNo = @SequenceNo and 
sem.Terminal = @Terminal
Group by tc.TCategoryName
end
GO
/****** Object:  StoredProcedure [dbo].[spGetTransactionLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetTransactionLog]
(
@TranId int=NULL, @VchrId int=NULL, @VchrNo int=NULL, @VchrType nvarchar(50)=NULL, @VchrDate datetime=NULL, @AccountId int=NULL, @OppAccountId int=NULL, @DebitAmount decimal(24,4)=NULL, @CreditAmount decimal(24,4)=NULL, @Narration nvarchar(400)=NULL, @UserID int=NULL, @TranDate datetime=NULL, @Status nvarchar(10)=NULL, @RefNo nvarchar(20)=NULL, @VchrName nvarchar(50)=NULL, @FK_BranchId int=NULL, @SequenceNo int=NULL, @Terminal nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
TranId, 
VchrId, 
VchrNo, 
VchrType, 
VchrDate, 
AccountId, 
OppAccountId, 
DebitAmount, 
CreditAmount, 
Narration, 
UserID, 
TranDate, 
Status, 
RefNo, 
VchrName, 
FK_BranchId, 
SequenceNo, 
Terminal
 from TransactionLog where 
(TranId = @TranId or @TranId is null ) And 
(VchrId = @VchrId or @VchrId is null ) And 
(VchrNo = @VchrNo or @VchrNo is null ) And 
(VchrType = @VchrType or @VchrType is null ) And 
(VchrDate = @VchrDate or @VchrDate is null ) And 
(AccountId = @AccountId or @AccountId is null ) And 
(OppAccountId = @OppAccountId or @OppAccountId is null ) And 
(DebitAmount = @DebitAmount or @DebitAmount is null ) And 
(CreditAmount = @CreditAmount or @CreditAmount is null ) And 
(Narration = @Narration or @Narration is null ) And 
(UserID = @UserID or @UserID is null ) And 
(TranDate = @TranDate or @TranDate is null ) And 
(Status = @Status or @Status is null ) And 
(RefNo = @RefNo or @RefNo is null ) And 
(VchrName = @VchrName or @VchrName is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(SequenceNo = @SequenceNo or @SequenceNo is null ) And 
(Terminal = @Terminal or @Terminal is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetTransactionReportForPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetTransactionReportForPOS]
(
@FromDate datetime, 
@ToDate datetime,
@BranchId int = null
)
As
Begin
Select SalesDate as [Date],CntrInvNo as ReceiptNo,ac.AccountName as Account,isnull(CashPayment,0)+isnull(CardPayment,0)+isnull(OtherPayment,0) as Payment, 
Credit, ReturnAmount as [Return], GrandDiscount as Total
From SalesEstimateMaster sem
Left join Account ac on sem.PartyAccountId = ac.AccountId
Where (sem.FK_BranchId = @BranchId or @BranchId is null) and
SalesDate between @FromDate and @ToDate
End
GO
/****** Object:  StoredProcedure [dbo].[spGetTranslator]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetTranslator]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @Value nvarchar(100)=NULL, @Form nvarchar(100)=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Name, 
Value, 
Form
 from Translator where 
(Id = @Id or @Id is null ) And 
(Name = @Name or @Name is null ) And 
(Value = @Value or @Value is null ) And 
(Form = @Form or @Form is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetUnit]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetUnit]
(
@UnitId int=NULL, @UnitName nvarchar(10)=NULL, @UnitNo int=NULL 
)

 AS 
 BEGIN 

 Select 
UnitId, 
UnitName, 
UnitNo
 from Unit where 
(UnitId = @UnitId or @UnitId is null ) And 
(UnitName = @UnitName or @UnitName is null ) And 
(UnitNo = @UnitNo or @UnitNo is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetUser]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetUser]
(
@UserId int=NULL, @PrevilageId int=NULL, @UserName nvarchar(50)=NULL, @PassWord nvarchar(50)=NULL 
)

 AS 
 BEGIN 

 Select 
UserId, 
PrevilageId, 
UserName, 
PassWord
 from [User] where 
(UserId = @UserId or @UserId is null ) And 
(PrevilageId = @PrevilageId or @PrevilageId is null ) And 
(UserName = @UserName or @UserName is null ) And 
([PassWord] = @PassWord or @PassWord is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetUserPrevilegeDtl]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetUserPrevilegeDtl]
(
@Id int=NULL, @FK_UserPrevilegeHdrId int=NULL, @FK_MenuDetailsId int=NULL, @Visible bit=NULL, @Enable bit=NULL, @SaveData bit=NULL, @EditData bit=NULL, @DeleteData bit=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
FK_UserPrevilegeHdrId, 
FK_MenuDetailsId, 
Visible, 
Enable, 
SaveData, 
EditData, 
DeleteData
 from UserPrevilegeDtl where 
(Id = @Id or @Id is null ) And 
(FK_UserPrevilegeHdrId = @FK_UserPrevilegeHdrId or @FK_UserPrevilegeHdrId is null ) And 
(FK_MenuDetailsId = @FK_MenuDetailsId or @FK_MenuDetailsId is null ) And 
(Visible = @Visible or @Visible is null ) And 
(Enable = @Enable or @Enable is null ) And 
(SaveData = @SaveData or @SaveData is null ) And 
(EditData = @EditData or @EditData is null ) And 
(DeleteData = @DeleteData or @DeleteData is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetUserPrevilegeHdr]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetUserPrevilegeHdr]
(
@Id int=NULL, @Name nvarchar(100)=NULL 
)

 AS 
 BEGIN 

 Select 
Id, 
Name
 from UserPrevilegeHdr where 
(Id = @Id or @Id is null ) And 
(Name = @Name or @Name is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetValueForChart]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetValueForChart]
(
@GroupId int,@Date datetime
)
AS 
 BEGIN 

Select sum(tl.DebitAmount-tl.CreditAmount) as Balance 
 From TransactionLog tl
 Left join Account acc on tl.AccountId=acc.AccountId
 Where acc.AGroupId=@GroupId and tl.VchrDate <= @Date
 
 END
 
GO
/****** Object:  StoredProcedure [dbo].[spGetValuesForExpenses]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetValuesForExpenses]
(
@BranchId int=null,@FromDate datetime,@ToDate datetime
)
AS 
 BEGIN 
 if @BranchId=null

 Select distinct (tl.VchrDate) as Date, sum(DebitAmount) as Amount
 From TransactionLog  tl
 Left Join Account acc on tl.AccountId = acc.AccountId
 Left Join AccountGroup ag on acc.AGroupId=ag.AGroupId
 where tl.FK_BranchId=@BranchId and tl.VchrType='Payment Voucher' and ag.GroupName='INDIRECT EXPENSES' and tl.VchrDate between @FromDate and @ToDate
 group by tl.VchrDate
 else
Select distinct (tl.VchrDate) as Date, sum(DebitAmount) as Amount
 From TransactionLog  tl
 Left Join Account acc on tl.AccountId = acc.AccountId
 Left Join AccountGroup ag on acc.AGroupId=ag.AGroupId
 where  tl.VchrType='Payment Voucher' and ag.GroupName='INDIRECT EXPENSES' and tl.VchrDate between @FromDate and @ToDate
 group by tl.VchrDate
  END
  
GO
/****** Object:  StoredProcedure [dbo].[spGetValuesForGroupWiseSales]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetValuesForGroupWiseSales]
(
@FromDate datetime=null,@ToDate datetime=null
)
AS 
 BEGIN 
 Select pg.PGroupName, sum(ses.NetAmnt) as NetAmount
 From SalesEstimateSettings ses
 Join Product pct on ses.ProductId= pct.ProductId
 Join ProductGroup pg on pct.PGroupId= pg.PGroupId
 where ses.Date between @FromDate and @ToDate
 Group by pg.PGroupName
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spGetValuesForIncome]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetValuesForIncome]
(
@BranchId int=null,@FromDate datetime,@ToDate datetime
)
AS 
 BEGIN 
 if @BranchId=null

 Select distinct (tl.VchrDate) as Date, sum(CreditAmount) as Amount
 From TransactionLog  tl
 Left Join Account acc on tl.AccountId = acc.AccountId
 Left Join AccountGroup ag on acc.AGroupId=ag.AGroupId
 where tl.FK_BranchId=@BranchId and tl.VchrType='Receipt Voucher' and ag.GroupName='INDIRECT INCOME' and tl.VchrDate between @FromDate and @ToDate
 group by tl.VchrDate
 else
Select distinct (tl.VchrDate) as Date, sum(CreditAmount) as Amount
 From TransactionLog  tl
 Left Join Account acc on tl.AccountId = acc.AccountId
 Left Join AccountGroup ag on acc.AGroupId=ag.AGroupId
 where  tl.VchrType='Receipt Voucher' and ag.GroupName='INDIRECT INCOME' and tl.VchrDate between @FromDate and @ToDate
 group by tl.VchrDate
  END
  
GO
/****** Object:  StoredProcedure [dbo].[spGetValuesForSalesAndPurchase]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetValuesForSalesAndPurchase]
(
@FromDate datetime=null,
@ToDate datetime=null,
@BranchId int = null
)
AS 
 BEGIN 
   Select CONCAT(DAY(tl.VchrDate),'-',MONTH(tl.VchrDate),'-',YEAR(tl.VchrDate)) AS Date, sum(tl.CreditAmount) as Sales, sum(tl.DebitAmount) as Purchase
  From TransactionLog tl
   where Status <>'Sub' and VchrType='Sales Voucher' or VchrType='Purchase Voucher' and
   (tl.FK_BranchId = @BranchId or @BranchId is null) and
   VchrDate between @FromDate and @ToDate 
  GROUP BY DAY(tl.VchrDate), MONTH(tl.VchrDate) , YEAR(tl.VchrDate)
  Order BY  YEAR(tl.VchrDate),MONTH(tl.VchrDate) ,DAY(tl.VchrDate)
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spGetVchrTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spGetVchrTranLog]
(
@VchrTranId int=NULL, @VchrNo int=NULL, @VchrDate datetime=NULL, @VchrAmount decimal(24,4)=NULL, @VchrType nvarchar(50)=NULL, @FromAccountId int=NULL, @ToAccountId int=NULL, @Narration nvarchar(400)=NULL, @SalesManId int=NULL, @UserId int=NULL, @TranDate datetime=NULL, @VchrName nvarchar(50)=NULL, @FK_BranchId int=NULL, @SequenceNo int=NULL, @Terminal nvarchar(25)=NULL 
)

 AS 
 BEGIN 

 Select 
VchrTranId, 
VchrNo, 
VchrDate, 
VchrAmount, 
VchrType, 
FromAccountId, 
ToAccountId, 
Narration, 
SalesManId, 
UserId, 
TranDate, 
VchrName, 
FK_BranchId, 
SequenceNo, 
Terminal
 from VchrTranLog where 
(VchrTranId = @VchrTranId or @VchrTranId is null ) And 
(VchrNo = @VchrNo or @VchrNo is null ) And 
(VchrDate = @VchrDate or @VchrDate is null ) And 
(VchrAmount = @VchrAmount or @VchrAmount is null ) And 
(VchrType = @VchrType or @VchrType is null ) And 
(FromAccountId = @FromAccountId or @FromAccountId is null ) And 
(ToAccountId = @ToAccountId or @ToAccountId is null ) And 
(Narration = @Narration or @Narration is null ) And 
(SalesManId = @SalesManId or @SalesManId is null ) And 
(UserId = @UserId or @UserId is null ) And 
(TranDate = @TranDate or @TranDate is null ) And 
(VchrName = @VchrName or @VchrName is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) And 
(SequenceNo = @SequenceNo or @SequenceNo is null ) And 
(Terminal = @Terminal or @Terminal is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spGetVehicle]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spGetVehicle]
(
@VanId int=NULL, @VehName nvarchar(50)=NULL, @Manufacturer nvarchar(20)=NULL, @Model nvarchar(20)=NULL, @YearOfModel nvarchar(20)=NULL, @RegNo nvarchar(20)=NULL, @Ownership nvarchar(50)=NULL, @RegisterdOwner nvarchar(50)=NULL, @EngineNo nvarchar(50)=NULL, @ChasisNo nvarchar(50)=NULL, @InsurenceDate datetime=NULL, @TaxDate datetime=NULL, @PolusionDate datetime=NULL, @BColor nvarchar(20)=NULL, @LoanEMIDate datetime=NULL, @LoanEMIAmount nvarchar(200)=NULL, @Remarks nvarchar(400)=NULL, @FK_BranchId int=NULL 
)

 AS 
 BEGIN 

 Select 
VanId, 
VehName, 
Manufacturer, 
Model, 
YearOfModel, 
RegNo, 
Ownership, 
RegisterdOwner, 
EngineNo, 
ChasisNo, 
InsurenceDate, 
TaxDate, 
PolusionDate, 
BColor, 
LoanEMIDate, 
LoanEMIAmount, 
Remarks, 
FK_BranchId
 from Vehicle where 
(VanId = @VanId or @VanId is null ) And 
(VehName = @VehName or @VehName is null ) And 
(Manufacturer = @Manufacturer or @Manufacturer is null ) And 
(Model = @Model or @Model is null ) And 
(YearOfModel = @YearOfModel or @YearOfModel is null ) And 
(RegNo = @RegNo or @RegNo is null ) And 
(Ownership = @Ownership or @Ownership is null ) And 
(RegisterdOwner = @RegisterdOwner or @RegisterdOwner is null ) And 
(EngineNo = @EngineNo or @EngineNo is null ) And 
(ChasisNo = @ChasisNo or @ChasisNo is null ) And 
(InsurenceDate = @InsurenceDate or @InsurenceDate is null ) And 
(TaxDate = @TaxDate or @TaxDate is null ) And 
(PolusionDate = @PolusionDate or @PolusionDate is null ) And 
(BColor = @BColor or @BColor is null ) And 
(LoanEMIDate = @LoanEMIDate or @LoanEMIDate is null ) And 
(LoanEMIAmount = @LoanEMIAmount or @LoanEMIAmount is null ) And 
(Remarks = @Remarks or @Remarks is null ) And 
(FK_BranchId = @FK_BranchId or @FK_BranchId is null ) 

 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spImportproduct]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spImportproduct]
(
@ProductCode int,
@ProductName nvarchar (100)='',
@Specification nvarchar (100)='',
@GroupName nvarchar (100)='GENERAL',
@UnitName nvarchar (100)='PCs',
@BrandName nvarchar(100)='GENERAL',
@PurchasePrice decimal(24,4)=0,
@SellingPrice decimal(24,4)=0,
@MRP decimal(24,4)=0,
@OpeningStock decimal(24,4)=0,
@ProductType nvarchar (100)='Finished Good',
@Barcode nvarchar(50)='',
@LocalName nvarchar(100)='',
@TCategoryName nvarchar(100)='NA',
@isInclusive bit =TRUE,
@UserId int = 1,
@BranchId int = 1,
--Add Unit-1
@FUnitName nvarchar (100)='',
@FProductName nvarchar (100)='',
@FQuantity int=0,
@FRate decimal(24,4)=0,
@FBarcode nvarchar(50)='',
@FLocalName nvarchar(100)='',
--Add Unit-2
@SUnitName nvarchar (100)='',
@SProductName nvarchar (100)='',
@SQuantity int=0,
@SRate decimal(24,4)=0,
@SBarcode nvarchar(50)='',
@SLocalName nvarchar(100)='',
--Additional Details
@ItemCalory nvarchar(5) = '',
@errMessage VARCHAR(MAX)='' OUTPUT,
@return int=null OUTPUT
)
 AS 
 BEGIN 

 BEGIN TRY
   BEGIN TRANSACTION pro_import

    IF @ProductName <> ''
 BEGIN
 declare @PId int
 select @Pid = ProductId From product Where ProductName = @ProductName
 set @Pid=isnull(@Pid,0)
 IF @PId = 0
 Begin
 declare @ProBarcodeId int 
 select @ProBarcodeId =  Id From ProductBarcode Where Barcode = @Barcode
 set @ProBarcodeId = ISNULL(@ProBarcodeId,0)
 IF @ProBarcodeId = 0
 Begin
 declare @GroupId int
 declare @BrandId int
 declare @GodownId int
 declare @SelfId int
 declare @UnitId int
 declare @TCategoryId int
 declare @PdctCode int
 declare @proCode int
 declare @SupplierId int
 declare @IsActive bit 
 declare @OppStockDate datetime 
 declare @IsAllowMultiple bit 
 declare @IsCommisionBasedPercentage bit
 declare @IsExpairing bit
 declare @IsMarginBasedPercentage bit 
 declare @IsScale bit 
 declare @ListOrder int 
 declare @UnitQty int
 declare @UnitType nvarchar(10)
 declare @ProductId int
 declare @OppVchrType nvarchar (25)
 declare @OppNarration nvarchar (25)
 declare @PCategoryId int
 declare @BarcodeType nvarchar (25)
 declare @ZeroVal int
 set @ZeroVal = 0
 set @SupplierId = 7
 select @GroupId = PGroupId  From ProductGroup Where PGroupName =@GroupName
 set @GroupId = ISNULL(@GroupId,0)
 iF @GroupId = 0
 Begin
  Select @GroupId =  PGroupId  From ProductGroup Where PGroupName = 'GENERAL'
  End
 select @BrandId = BrandId From Brand Where BrandName = @BrandName
 set @BrandId = ISNULL(@BrandId,0)
 if @BrandId = 0
 Begin
 Select @BrandId = BrandId From Brand Where BrandName = 'GENERAL'
 End
 select @TCategoryId = TCategoryId From TaxCategory Where TCategoryName = @TCategoryName
 set @TCategoryId = isnull(@TCategoryId,0)
 if @TCategoryId = 0
 Begin
 Select @TCategoryId = TCategoryId From TaxCategory Where TCategoryName = 'NA'
 End
 select @UnitId = UnitId From Unit Where UnitName = @UnitName
 set @UnitId = isnull(@UnitId,0)
 if @UnitId = 0
 Begin
 Select @UnitId = UnitId From Unit Where UnitName = 'PCs'
 End
 select @GodownId = GodwnId From Godown Where GodownName = 'PRIMARY'
 select @SelfId = SelfId From Self Where SelfName='PRIMARY - 1' 
 select top 1  @PdctCode = ProductCode from Product order by ProductCode desc
 set @PdctCode = ISNULL(@PdctCode,0)
 set @ProCode = @PdctCode + 1
 set @IsActive = 1
 set @OppStockDate = Getdate()
 set @IsAllowMultiple = 0
 set @IsCommisionBasedPercentage = 0
 set @IsExpairing = 0
 set @IsMarginBasedPercentage = 0
 set @IsScale =0
 set @ListOrder = @ProCode
 set @UnitQty = 1
 set @UnitType = 'Base Unit'
 set @OppVchrType = 'Opening Entry'
 set @OppNarration = 'Opening Stock Entry'
 set @BarcodeType = 'Base Barcode'
   insert into Product (ProductName,ProductCode,PGroupId,BrandId,UnitId,TCategoryId,
   GodwnId,SelfId,PurchasePrice,SellingPrice,MRP,isActive,isInclusive,IsScale,IsAllowMultiple,
   OpeningStock,OppStockDate,IsCommisionBasedPercentage,IsExpairing,IsMarginBasedPercentage,
   ListOrder,Barcode,LocalName,ProductType,Specification,FieldTwo)
   Values (@ProductName,@ProCode,@GroupId,@BrandId,@UnitId,@TCategoryId,
   @GodownId,@SelfId,@PurchasePrice,@SellingPrice,@MRP,@IsActive,@isInclusive,@IsScale,@IsAllowMultiple,
   @OpeningStock,@OppStockDate,@IsCommisionBasedPercentage,@IsExpairing,@IsMarginBasedPercentage,
   @ListOrder,@Barcode,@LocalName,@ProductType,@Specification,@ItemCalory)

   set @ProductId = SCOPE_IDENTITY()
   insert into ProductUnit (UnitId,ProductId,ProductUnitName,Barcode,IsInclusive,OpeningStock,SalesPrice,UnitQty,UnitType)
   Values (@UnitId,@ProductId,@ProductName,@Barcode,@isInclusive,@OpeningStock,@SellingPrice,@UnitQty,@UnitType)
   
   insert into ProductBarcode (FK_BranchId,FK_ProductId,FK_UnitId,Barcode,BarcodeType,PurchasePrice,ProductCost,SellingPrice,MRP,MinimuPrice,RetailPrice,WholesalePrice,ProductBarcodeName,LocalName)
   values (@BranchId,@ProductId,@UnitId,@Barcode,@BarcodeType,@PurchasePrice,@PurchasePrice,@SellingPrice,@MRP,@SellingPrice,@SellingPrice,@SellingPrice,@ProductName,@LocalName)
   
   DECLARE MyCursor_service CURSOR
		FOR
		   select PCategoryId From PriceCategory 
		   OPEN MyCursor_service
		FETCH NEXT FROM MyCursor_service INTO @PCategoryId
		WHILE @@FETCH_STATUS = 0
		BEGIN
		insert into ProductPriceCategory (ProductId,PCategoryId,PDisc,SPDisc,PGroupId,PPrice,SPrice,UnitId,Barcode)
		Values (@ProductId,@PCategoryId,0,0,@GroupId,@PurchasePrice,@SellingPrice,@UnitId,@Barcode)
		FETCH NEXT FROM MyCursor_service INTO @PCategoryId
		END
		close MyCursor_service
		DEALLOCATE MyCursor_service
  IF @OpeningStock >0
   begin
   insert into StockTranLog(ProductId,VchrId,VchrNo,VchrDate,VchrType,VchrFormType,AccountId,InQuantity,OutQuantity,UnitId,Narration,
   UserId,TranDate,FK_BranchId,Barcode)
   Values(@ProductId,@ProductId,@ProCode,@OppStockDate,@OppVchrType,@OppVchrType,@SupplierId,@OpeningStock,@ZeroVal,@UnitId,@OppNarration,
   @UserId,@OppStockDate,@BranchId,@Barcode)

   insert into GdwnStckTranLog (GodwnId,ProductId,VchrId,VchrNo,VchrDate,VchrType,InQuantity,OutQuantity,UnitId,UserId,Narration,TranDate,FK_BranchId,Barcode)
   Values (@GodownId,@ProductId,@ProductId,@ProCode,@OppStockDate,@OppVchrType,@OpeningStock,@ZeroVal,@UnitId,@UserId,@OppNarration,@OppStockDate,@BranchId,@Barcode)
   END
   --end
  if @FUnitName <> ''
  Begin
  declare @FUnitId int
  select @FUnitId = UnitId From Unit Where UnitName = @FUnitName
 set @FUnitId = isnull(@FUnitId,0)
 if @FUnitId = 0
 Begin
 insert into Unit (UnitName) Values (@FUnitName)
 set @FUnitId = SCOPE_IDENTITY()
 End
 --Start
 if @FBarcode = ''
 Begin
 set @FBarcode = CONCAT(@Barcode,@FQuantity)
 End
 if @FRate = 0
 Begin
 set @FRate = @SellingPrice * @FQuantity
 END
 --End
  insert into ProductUnit (UnitId,ProductId,ProductUnitName,Barcode,IsInclusive,OpeningStock,SalesPrice,UnitQty,UnitType)
   Values (@FUnitId,@ProductId,CONCAT(@ProductName,@FUnitName),@FBarcode,@isInclusive,0,@FRate,@FQuantity,'2 Unit')

   insert into ProductBarcode (FK_BranchId,FK_ProductId,FK_UnitId,Barcode,BarcodeType,PurchasePrice,ProductCost,SellingPrice,MRP,MinimuPrice,RetailPrice,WholesalePrice,ProductBarcodeName,LocalName)
   values (@BranchId,@ProductId,@FUnitId,@FBarcode,'Unit Barcode',0,0,@FRate,@FRate,@FRate,@FRate,@FRate,CONCAT(@ProductName,@FUnitName),@FLocalName)

   DECLARE MyCursor_service CURSOR
		FOR
		   select PCategoryId From PriceCategory 
		   OPEN MyCursor_service
		FETCH NEXT FROM MyCursor_service INTO @PCategoryId
		WHILE @@FETCH_STATUS = 0
		BEGIN
		insert into ProductPriceCategory (ProductId,PCategoryId,PDisc,SPDisc,PGroupId,PPrice,SPrice,UnitId,Barcode)
		Values (@ProductId,@PCategoryId,0,0,@GroupId,0,@FRate,@FUnitId,@FBarcode)
		FETCH NEXT FROM MyCursor_service INTO @PCategoryId
		END
		close MyCursor_service
		DEALLOCATE MyCursor_service
  END
  if @SUnitName <> ''
  Begin
  declare @SUnitId int

  select @SUnitId = UnitId From Unit Where UnitName = @SUnitName
 set @SUnitId = isnull(@SUnitId,0)
 if @FUnitId = 0
 Begin
 insert into Unit (UnitName) Values (@SUnitName)
 set @SUnitId = SCOPE_IDENTITY()
 End
  --Start
 if @SBarcode = ''
 Begin
 set @SBarcode = CONCAT(@Barcode,@SQuantity)
 End
 if @SRate = 0
 Begin
 set @SRate = @SellingPrice * @SQuantity
 END
 --End
  insert into ProductUnit (UnitId,ProductId,ProductUnitName,Barcode,IsInclusive,OpeningStock,SalesPrice,UnitQty,UnitType)
   Values (@SUnitId,@ProductId,CONCAT(@ProductName,@SUnitName),@SBarcode,@isInclusive,0,@SRate,@SQuantity,'3 Unit')

   insert into ProductBarcode (FK_BranchId,FK_ProductId,FK_UnitId,Barcode,BarcodeType,PurchasePrice,ProductCost,SellingPrice,MRP,MinimuPrice,RetailPrice,WholesalePrice,ProductBarcodeName,LocalName)
   values (@BranchId,@ProductId,@SUnitId,@SBarcode,'Unit Barcode',0,0,@SRate,@SRate,@SRate,@SRate,@SRate,CONCAT(@ProductName,@SUnitName),@SLocalName)

   DECLARE MyCursor_service CURSOR
		FOR
		   select PCategoryId From PriceCategory 
		   OPEN MyCursor_service
		FETCH NEXT FROM MyCursor_service INTO @PCategoryId
		WHILE @@FETCH_STATUS = 0
		BEGIN
		insert into ProductPriceCategory (ProductId,PCategoryId,PDisc,SPDisc,PGroupId,PPrice,SPrice,UnitId,Barcode)
		Values (@ProductId,@PCategoryId,0,0,@GroupId,0,@SRate,@SUnitId,@SBarcode)
		FETCH NEXT FROM MyCursor_service INTO @PCategoryId
		END
		close MyCursor_service
		DEALLOCATE MyCursor_service

  END
   END
   END
  End
   set @return=@ProductId
	select @return 
   COMMIT TRANSACTION pro_import
   END TRY
   BEGIN CATCH
    ROLLBACK TRANSACTION pro_import;
    SELECT @errMessage = ERROR_MESSAGE()
   END CATCH 
   
 END 
GO
/****** Object:  StoredProcedure [dbo].[spInsertAccount]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertAccount]
(
@AGroupId int=NULL, 
@RootId int=NULL, 
@PCategoryId int=NULL, 
@AccountName nvarchar(200)=NULL, 
@OpeningBalance decimal(18,4)=NULL, 
@DebitOrCridt nvarchar(5)=NULL, 
@AddressOne nvarchar(50)=NULL, 
@AddressTwo nvarchar(50)=NULL, 
@AddressThree nvarchar(50)=NULL, 
@PhoneOne nvarchar(20)=NULL, 
@MobileNo nvarchar(20)=NULL, 
@FaxNo nvarchar(20)=NULL, 
@TinNo nvarchar(20)=NULL, 
@CstNo nvarchar(20)=NULL, 
@EMailOne nvarchar(50)=NULL, 
@Remarks nvarchar(400)=NULL, 
@BankName nvarchar(50)=NULL, 
@BankAddress nvarchar(50)=NULL, 
@AccountNo nvarchar(50)=NULL, 
@isActive bit=NULL, 
@IsBuiltIn bit=NULL, 
@DebitLimit decimal(24,4)=NULL, 
@IsLimitApplicable bit=NULL, 
@AddressFour nvarchar(50)=NULL, 
@FieldOne nvarchar(50)=NULL, 
@FieldTwo nvarchar(50)=NULL, 
@FieldThree nvarchar(50)=NULL, 
@FieldFour nvarchar(50)=NULL

)
 AS 
 BEGIN 
 
 insert into Account(AGroupId, RootId, PCategoryId, AccountName, OpeningBalance, DebitOrCridt, AddressOne, AddressTwo, AddressThree, PhoneOne, MobileNo, FaxNo, TinNo, CstNo, EMailOne, Remarks, BankName, BankAddress, AccountNo, isActive, IsBuiltIn, DebitLimit, IsLimitApplicable, AddressFour, FieldOne, FieldTwo, FieldThree, FieldFour )
 values(@AGroupId, @RootId, @PCategoryId, @AccountName, @OpeningBalance, @DebitOrCridt, @AddressOne, @AddressTwo, @AddressThree, @PhoneOne, @MobileNo, @FaxNo, @TinNo, @CstNo, @EMailOne, @Remarks, @BankName, @BankAddress, @AccountNo, @isActive, @IsBuiltIn, @DebitLimit, @IsLimitApplicable, @AddressFour, @FieldOne, @FieldTwo, @FieldThree, @FieldFour )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertAccountGroup]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertAccountGroup]
(
@GroupName nvarchar(50)=NULL, @Under nvarchar(50)=NULL, @Remarks nvarchar(400)=NULL, @isBuilIn bit=NULL 
)
 AS 
 BEGIN 
 
 insert into AccountGroup(GroupName, Under, Remarks, isBuilIn )
 values(@GroupName, @Under, @Remarks, @isBuilIn )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertAttenMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertAttenMaster]
(
@AtteInvNo int=NULL, @AtteDate datetime=NULL, @Remarks nvarchar(400)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into AttenMaster(AtteInvNo, AtteDate, Remarks, FK_BranchId )
 values(@AtteInvNo, @AtteDate, @Remarks, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertAttenSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertAttenSettings]
(
@AttendanceMasterId int=NULL, @EmpID int=NULL, @Morning bit=NULL, @Evening bit=NULL, @AttenType decimal(18,1)=NULL, @EmpName nvarchar(50)=NULL, @AtteDate datetime=NULL 
)
 AS 
 BEGIN 
 
 insert into AttenSettings(AttendanceMasterId, EmpID, Morning, Evening, AttenType, EmpName, AtteDate )
 values(@AttendanceMasterId, @EmpID, @Morning, @Evening, @AttenType, @EmpName, @AtteDate )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertBarcodeDTL]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertBarcodeDTL]
(
@Name nvarchar(100)=NULL, 
@Text nvarchar(100)=NULL, 
@Value nvarchar(100)=NULL, 
@isIncluded bit=NULL, 
@Position_x int=NULL, 
@Position_y int=NULL, 
@Width int=NULL, 
@Height int=NULL, 
@Alignment nvarchar(50)=NULL, 
@RotationPoint nvarchar(50)=NULL, 
@fontFamily nvarchar(50)=NULL, 
@fontSize int=NULL, 
@fStyle nvarchar(50)=NULL, 
@Format nvarchar(50)=NULL, 
@bAlignment nvarchar(50)=NULL, 
@FK_BarcodeHDR int=NULL, 
@Type nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into BarcodeDTL(Name, Text, Value, isIncluded, Position_x, Position_y, Width, Height, Alignment, RotationPoint, fontFamily, fontSize, fStyle, Format, bAlignment, FK_BarcodeHDR, Type )
 values(@Name, @Text, @Value, @isIncluded, @Position_x, @Position_y, @Width, @Height, @Alignment, @RotationPoint, @fontFamily, @fontSize, @fStyle, @Format, @bAlignment, @FK_BarcodeHDR, @Type )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertBarcodeHDR]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertBarcodeHDR]
(
@name nvarchar(50)=NULL, @FirstMargin int=NULL, @TopMargin int=NULL, @RightMargin int=NULL, @BottomMargin int=NULL, @Width int=NULL, @Height int=NULL, @Accross int=NULL, @Down int=NULL, @TotalPerPage int=NULL, @PrinterName nvarchar(50)=NULL, @PaperName nvarchar(50)=NULL, @PSWidth int=NULL, @PSHieght int=NULL, @isBorderIncluded bit=NULL, @isCuttingEdge bit=NULL, @BarcodeField int=NULL, @isBarcode bit=NULL 
)
 AS 
 BEGIN 
 
 insert into BarcodeHDR(name, FirstMargin, TopMargin, RightMargin, BottomMargin, Width, Height, Accross, Down, TotalPerPage, PrinterName, PaperName, PSWidth, PSHieght, isBorderIncluded, isCuttingEdge, BarcodeField, isBarcode )
 values(@name, @FirstMargin, @TopMargin, @RightMargin, @BottomMargin, @Width, @Height, @Accross, @Down, @TotalPerPage, @PrinterName, @PaperName, @PSWidth, @PSHieght, @isBorderIncluded, @isCuttingEdge, @BarcodeField, @isBarcode )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertBarcodeScale]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertBarcodeScale]
(
@Name nvarchar(100)=NULL, 
@IPAddress nvarchar(100)=NULL, 
@Remarks nvarchar(400)=NULL

)
 AS 
 BEGIN 
 
 insert into BarcodeScale(Name, IPAddress, Remarks )
 values(@Name, @IPAddress, @Remarks )
 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spInsertBatch]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertBatch]
(
@Name nvarchar(100)=NULL, @FK_PurInvId int=NULL, @Remarks nvarchar(100)=NULL, @FK_BranchId int=NULL, @RefNo nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 insert into Batch(Name, FK_PurInvId, Remarks, FK_BranchId, RefNo )
 values(@Name, @FK_PurInvId, @Remarks, @FK_BranchId, @RefNo )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertBranch]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertBranch]
(
@Name nvarchar(100)=NULL, @Description nvarchar(500)=NULL, @Address nvarchar(400)=NULL, @City nvarchar(100)=NULL, @State nvarchar(100)=NULL, @Country nvarchar(100)=NULL, @PostalCode nvarchar(100)=NULL, @PhoneNumber nvarchar(100)=NULL, @MobileNumber nvarchar(100)=NULL, @ContactPerson nvarchar(100)=NULL, @Status nvarchar(100)=NULL, @Active bit=NULL 
)
 AS 
 BEGIN 
 
 insert into Branch(Name, Description, Address, City, State, Country, PostalCode, PhoneNumber, MobileNumber, ContactPerson, Status, Active )
 values(@Name, @Description, @Address, @City, @State, @Country, @PostalCode, @PhoneNumber, @MobileNumber, @ContactPerson, @Status, @Active )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertBrand]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertBrand]
(
@BrandName nvarchar(50)=NULL, @Description nvarchar(400)=NULL 
)
 AS 
 BEGIN 
 
 insert into Brand(BrandName, Description )
 values(@BrandName, @Description )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertChequeDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertChequeDetails]
(
@VchrId int=NULL, @VchrType nvarchar(50)=NULL, @AccountId int=NULL, @BankAcctId int=NULL, @Amount decimal(18,2)=NULL, @Bank nvarchar(200)=NULL, @Branch nvarchar(500)=NULL, @CheqNo nvarchar(25)=NULL, @CheqDate nvarchar(20)=NULL, @Status nvarchar(10)=NULL, @RefNo nvarchar(20)=NULL, @Remarks nvarchar(500)=NULL, @TranDate datetime=NULL, @JeId int=NULL, @Type nvarchar(50)=NULL, @CollectedDate datetime=NULL, @Date datetime=NULL, @ChqRtnAmount decimal(18,2)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into ChequeDetails(VchrId, VchrType, AccountId, BankAcctId, Amount, Bank, Branch, CheqNo, CheqDate, Status, RefNo, Remarks, TranDate, JeId, Type, CollectedDate, Date, ChqRtnAmount, FK_BranchId )
 values(@VchrId, @VchrType, @AccountId, @BankAcctId, @Amount, @Bank, @Branch, @CheqNo, @CheqDate, @Status, @RefNo, @Remarks, @TranDate, @JeId, @Type, @CollectedDate, @Date, @ChqRtnAmount, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertCostCenter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertCostCenter]
(
@CostCenterName nvarchar(50)=NULL, @Description nvarchar(400)=NULL 
)
 AS 
 BEGIN 
 
 insert into CostCenter(CostCenterName, Description )
 values(@CostCenterName, @Description )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertCulenderPlanning]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertCulenderPlanning]
(
@CplanninDate datetime=NULL, @Description image=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into CulenderPlanning(CplanninDate, Description, FK_BranchId )
 values(@CplanninDate, @Description, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertCurrencyInfo]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertCurrencyInfo]
(
@CurrencyCode nvarchar(10)=NULL, 
@IsCurrencyNameFeminine bit=NULL, 
@EnglishCurrencyName nvarchar(100)=NULL, 
@EnglishPluralCurrencyName nvarchar(100)=NULL, 
@EnglishCurrencyPartName nvarchar(100)=NULL, 
@EnglishPluralCurrencyPartName nvarchar(100)=NULL, 
@Arabic1CurrencyName nvarchar(100)=NULL, 
@Arabic2CurrencyName nvarchar(100)=NULL, 
@Arabic310CurrencyName nvarchar(100)=NULL, 
@Arabic1199CurrencyName nvarchar(100)=NULL, 
@Arabic1CurrencyPartName nvarchar(100)=NULL, 
@Arabic2CurrencyPartName nvarchar(100)=NULL, 
@Arabic310CurrencyPartName nvarchar(100)=NULL, 
@Arabic1199CurrencyPartName nvarchar(100)=NULL, 
@PartPrecision int=NULL, 
@IsCurrencyPartNameFeminine bit=NULL

)
 AS 
 BEGIN 
 
 insert into CurrencyInfo(CurrencyCode, IsCurrencyNameFeminine, EnglishCurrencyName, EnglishPluralCurrencyName, EnglishCurrencyPartName, EnglishPluralCurrencyPartName, Arabic1CurrencyName, Arabic2CurrencyName, Arabic310CurrencyName, Arabic1199CurrencyName, Arabic1CurrencyPartName, Arabic2CurrencyPartName, Arabic310CurrencyPartName, Arabic1199CurrencyPartName, PartPrecision, IsCurrencyPartNameFeminine )
 values(@CurrencyCode, @IsCurrencyNameFeminine, @EnglishCurrencyName, @EnglishPluralCurrencyName, @EnglishCurrencyPartName, @EnglishPluralCurrencyPartName, @Arabic1CurrencyName, @Arabic2CurrencyName, @Arabic310CurrencyName, @Arabic1199CurrencyName, @Arabic1CurrencyPartName, @Arabic2CurrencyPartName, @Arabic310CurrencyPartName, @Arabic1199CurrencyPartName, @PartPrecision, @IsCurrencyPartNameFeminine )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertCustomQuery]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertCustomQuery]
(
@CustmQryName nvarchar(100)=NULL, @Query nvarchar(1000)=NULL 
)
 AS 
 BEGIN 
 
 insert into CustomQuery(CustmQryName, Query )
 values(@CustmQryName, @Query )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertDebitCreditCardMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertDebitCreditCardMaster]
(
@CardName nvarchar(50)=NULL, @BankComm decimal(18,2)=NULL, @CardComm decimal(18,2)=NULL, @Remarks nvarchar(400)=NULL 
)
 AS 
 BEGIN 
 
 insert into DebitCreditCardMaster(CardName, BankComm, CardComm, Remarks )
 values(@CardName, @BankComm, @CardComm, @Remarks )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertEmployeeDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertEmployeeDetails]
(
@EmployeeID int=NULL, @EmpName nvarchar(400)=NULL, @AddressOne nvarchar(400)=NULL, @AddressTwo nvarchar(400)=NULL, @AddressThree nvarchar(400)=NULL, @PhNumber nvarchar(20)=NULL, @EmailId nvarchar(50)=NULL, @Designation nvarchar(50)=NULL, @Salary decimal(18,2)=NULL, @CasualLeave decimal(18,2)=NULL, @Gender nvarchar(10)=NULL, @Qualification nvarchar(400)=NULL, @ParentName nvarchar(50)=NULL, @Relation nvarchar(50)=NULL, @ParentAddress nvarchar(400)=NULL, @ParentPhNo nvarchar(20)=NULL, @Remarks nvarchar(400)=NULL, @isActive bit=NULL, @AdharNo nvarchar(400)=NULL, @SalaryAccntId int=NULL, @SalaryAdvAccntId int=NULL, @SalaryPayableId int=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into EmployeeDetails(EmployeeID, EmpName, AddressOne, AddressTwo, AddressThree, PhNumber, EmailId, Designation, Salary, CasualLeave, Gender, Qualification, ParentName, Relation, ParentAddress, ParentPhNo, Remarks, isActive, AdharNo, SalaryAccntId, SalaryAdvAccntId, SalaryPayableId, FK_BranchId )
 values(@EmployeeID, @EmpName, @AddressOne, @AddressTwo, @AddressThree, @PhNumber, @EmailId, @Designation, @Salary, @CasualLeave, @Gender, @Qualification, @ParentName, @Relation, @ParentAddress, @ParentPhNo, @Remarks, @isActive, @AdharNo, @SalaryAccntId, @SalaryAdvAccntId, @SalaryPayableId, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertFinancialYear]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertFinancialYear]
(
@FyearName nvarchar(100)=NULL, @DateFrom nvarchar(50)=NULL, @DateTo nvarchar(50)=NULL, @Old bit=NULL, @Closed bit=NULL 
)
 AS 
 BEGIN 
 
 insert into FinancialYear(FyearName, DateFrom, DateTo, Old, Closed )
 values(@FyearName, @DateFrom, @DateTo, @Old, @Closed )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertGdwnStckTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertGdwnStckTranLog]
(
@GodwnId int=NULL, 
@GodownName nvarchar(100)=NULL, 
@ProductId int=NULL, 
@ProductName nvarchar(100)=NULL, 
@VchrId int=NULL, 
@VchrNo int=NULL, 
@VchrDate datetime=NULL, 
@VchrType nvarchar(100)=NULL, 
@InQuantity decimal(24,4)=NULL, 
@OutQuantity decimal(24,4)=NULL, 
@UnitId int=NULL, 
@Narration nvarchar(400)=NULL, 
@UserId int=NULL, 
@TranDate datetime=NULL, 
@FK_BatchId int=NULL, 
@FK_BranchId int=NULL, 
@Barcode nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into GdwnStckTranLog(GodwnId, GodownName, ProductId, ProductName, VchrId, VchrNo, VchrDate, VchrType, InQuantity, OutQuantity, UnitId, Narration, UserId, TranDate, FK_BatchId, FK_BranchId, Barcode )
 values(@GodwnId, @GodownName, @ProductId, @ProductName, @VchrId, @VchrNo, @VchrDate, @VchrType, @InQuantity, @OutQuantity, @UnitId, @Narration, @UserId, @TranDate, @FK_BatchId, @FK_BranchId, @Barcode )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertGodown]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertGodown]
(
@GodownName nvarchar(50)=NULL, @AddressOne nvarchar(50)=NULL, @AddressTwo nvarchar(50)=NULL, @AddressThree nvarchar(50)=NULL, @Phone nvarchar(50)=NULL, @Description nvarchar(400)=NULL 
)
 AS 
 BEGIN 
 
 insert into Godown(GodownName, AddressOne, AddressTwo, AddressThree, Phone, Description )
 values(@GodownName, @AddressOne, @AddressTwo, @AddressThree, @Phone, @Description )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertInstitute]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertInstitute]
(
@InstituteName nvarchar(100)=NULL, 
@Place nvarchar(50)=NULL, 
@Address nvarchar(250)=NULL, 
@Country nvarchar(50)=NULL, 
@Pincod nvarchar(50)=NULL, 
@PhoneNo nvarchar(50)=NULL, 
@MobNo nvarchar(50)=NULL, 
@FaxNo nvarchar(50)=NULL, 
@website nvarchar(50)=NULL, 
@EmailId nvarchar(50)=NULL, 
@Sector nvarchar(50)=NULL, 
@Logo image=NULL, 
@LogoPath nvarchar(100)=NULL, 
@Productserial nvarchar(50)=NULL, 
@GSTNo nvarchar(50)=NULL, 
@State nvarchar(50)=NULL, 
@StateCode int=NULL, 
@BankName nvarchar(50)=NULL, 
@BankBranch nvarchar(50)=NULL, 
@IFSC nvarchar(50)=NULL, 
@AccNo nvarchar(50)=NULL, 
@LocalName nvarchar(100)=NULL, 
@CompanyId nvarchar(20)=NULL

)
 AS 
 BEGIN 
 
 insert into Institute(InstituteName, Place, Address, Country, Pincod, PhoneNo, MobNo, FaxNo, website, EmailId, Sector, Logo, LogoPath, Productserial, GSTNo, State, StateCode, BankName, BankBranch, IFSC, AccNo, LocalName, CompanyId )
 values(@InstituteName, @Place, @Address, @Country, @Pincod, @PhoneNo, @MobNo, @FaxNo, @website, @EmailId, @Sector, @Logo, @LogoPath, @Productserial, @GSTNo, @State, @StateCode, @BankName, @BankBranch, @IFSC, @AccNo, @LocalName, @CompanyId )
 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spInsertItemNote]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertItemNote]
(
@Note nvarchar(200)=NULL, 
@IsMain bit=NULL, 
@FieldOne nvarchar(100)=NULL

)
 AS 
 BEGIN 
 
 insert into ItemNote(Note, IsMain, FieldOne )
 values(@Note, @IsMain, @FieldOne )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertKOTOrderMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertKOTOrderMaster]
(
@KOTMasterId int=NULL,
@TockenNo int=NULL, 
@DateAndTime datetime=NULL, 
@CookingTime decimal(24,4)=NULL, 
@Terminal nvarchar(100)=NULL, 
@SerialNo int=NULL, 
@FK_FYearId int=NULL, 
@FK_CustomerId int=NULL, 
@FK_UserId int=NULL, 
@FK_StaffId int=NULL, 
@TableId int=NULL, 
@Remarks nvarchar(400)=NULL, 
@Notes nvarchar(600)=NULL, 
@FK_KOTPrinterId int=NULL, 
@OrderType nvarchar(100)=NULL, 
@TotalQuantity decimal(24,4)=NULL, 
@TotalAmount decimal(24,4)=NULL, 
@FK_BranchId int=NULL,
@TransactionType nvarchar(20)='insert',
@errMessage VARCHAR(MAX)='' OUTPUT,
@return int=null OUTPUT,
@type dbo.UT_KOTOrderSettings READONLY
)
 AS 
 BEGIN 
  BEGIN TRY
 BEGIN TRANSACTION KOTOrder
 if @TransactionType ='insert'
Begin
 insert into KOTOrderMaster(TockenNo, DateAndTime, CookingTime, Terminal, SerialNo, FK_FYearId, FK_CustomerId, FK_UserId, FK_StaffId, TableId, Remarks, Notes, FK_KOTPrinterId, OrderType, TotalQuantity, TotalAmount, FK_BranchId )
 values(@TockenNo, @DateAndTime, @CookingTime, @Terminal, @SerialNo, @FK_FYearId, @FK_CustomerId, @FK_UserId, @FK_StaffId, @TableId, @Remarks, @Notes, @FK_KOTPrinterId, @OrderType, @TotalQuantity, @TotalAmount, @FK_BranchId )
 
  set @KOTMasterId = SCOPE_IDENTITY()

   insert into KOTOrderSettings(FK_KOTMasterId, ProductId, Quantity, UnitId, Rate, NETAmount, Barcode, ItemNote )
 Select @KOTMasterId,ProductId, Quantity, UnitId, Rate, NETAmount, Barcode, ItemNote from @type
 set @return=@KOTMasterId
 select @return 
 End

 else
 Begin
 Delete From KOTOrderSettings Where FK_KOTMasterId =@KOTMasterId
  update KOTOrderMaster set TockenNo=@TockenNo, DateAndTime=@DateAndTime, CookingTime=@CookingTime, Terminal=@Terminal, 
  SerialNo=@SerialNo, FK_FYearId=@FK_FYearId, FK_CustomerId=@FK_CustomerId, FK_UserId=@FK_UserId, FK_StaffId=@FK_StaffId, 
  TableId=@TableId, Remarks=@Remarks, Notes=@Notes, FK_KOTPrinterId=@FK_KOTPrinterId, OrderType=@OrderType, TotalQuantity=@TotalQuantity, 
  TotalAmount=@TotalAmount, FK_BranchId=@FK_BranchId  where KOTMasterId=@KOTMasterId
   insert into KOTOrderSettings(FK_KOTMasterId, ProductId, Quantity, UnitId, Rate, NETAmount, Barcode, ItemNote )
 Select @KOTMasterId,ProductId, Quantity, UnitId, Rate, NETAmount, Barcode, ItemNote from @type
 set @return=@KOTMasterId
 select @return 
 end

 COMMIT TRANSACTION KOTOrder
  END TRY
   BEGIN CATCH
    ROLLBACK TRANSACTION KOTOrder;
   SELECT @errMessage = ERROR_MESSAGE()
   END CATCH 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertKOTPrinter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertKOTPrinter]
(
@KOTName nvarchar(100)=NULL, 
@PrinterName nvarchar(100)=NULL, 
@ConnectionType nvarchar(100)=NULL, 
@IPaddress nvarchar(100)=NULL, 
@Mode nvarchar(100)=NULL, 
@FK_BranchId int=NULL

)
 AS 
 BEGIN 
 
 insert into KOTPrinter(KOTName, PrinterName, ConnectionType, IPaddress, Mode, FK_BranchId )
 values(@KOTName, @PrinterName, @ConnectionType, @IPaddress, @Mode, @FK_BranchId )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertLogged]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertLogged]
(
@UserId int
)
 AS 
 BEGIN 
 
 insert into Logged(UserId)
 values(@UserId)
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertManufactureDtls]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertManufactureDtls]
(
@FK_ManufactureHdrId int=NULL, @FK_ProductId int=NULL, @Quantity decimal(24,8)=NULL, @FK_UnitId int=NULL, @PurchasePrice decimal(24,8)=NULL, @SalesePrice decimal(24,8)=NULL, @RawCost decimal(24,8)=NULL, @Expenses decimal(24,8)=NULL, @Profit decimal(24,8)=NULL, @FinalAmount decimal(24,8)=NULL, @NewPurchaseRate decimal(24,8)=NULL, @NewSalesRate decimal(24,8)=NULL, @FK_BatchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into ManufactureDtls(FK_ManufactureHdrId, FK_ProductId, Quantity, FK_UnitId, PurchasePrice, SalesePrice, RawCost, Expenses, Profit, FinalAmount, NewPurchaseRate, NewSalesRate, FK_BatchId )
 values(@FK_ManufactureHdrId, @FK_ProductId, @Quantity, @FK_UnitId, @PurchasePrice, @SalesePrice, @RawCost, @Expenses, @Profit, @FinalAmount, @NewPurchaseRate, @NewSalesRate, @FK_BatchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertManufactureHdr]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertManufactureHdr]
(
@InvNo int=NULL, @Date datetime=NULL, @FK_GodownId int=NULL, @Remarks nvarchar(400)=NULL, @FK_UserId int=NULL, @EntryDate datetime=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into ManufactureHdr(InvNo, Date, FK_GodownId, Remarks, FK_UserId, EntryDate, FK_BranchId )
 values(@InvNo, @Date, @FK_GodownId, @Remarks, @FK_UserId, @EntryDate, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertManufacturingExp]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertManufacturingExp]
(
@FK_ExpenseAccountId int=NULL, @FK_PaymentAccountId int=NULL, @Date datetime=NULL, @Amount decimal(24,8)=NULL, @FK_ManufactureHdrId int=NULL 
)
 AS 
 BEGIN 
 
 insert into ManufacturingExp(FK_ExpenseAccountId, FK_PaymentAccountId, Date, Amount, FK_ManufactureHdrId )
 values(@FK_ExpenseAccountId, @FK_PaymentAccountId, @Date, @Amount, @FK_ManufactureHdrId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertMasterValue]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertMasterValue]
(
@Name nvarchar(50)=NULL, @Description nvarchar(100)=NULL, @MasterType nvarchar(50)=NULL 
)
 AS 
 BEGIN 
 
 insert into MasterValue(Name, Description, MasterType )
 values(@Name, @Description, @MasterType )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertMenuDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertMenuDetails]
(
@Name nvarchar(100)=NULL, 
@MenuCaption nvarchar(100)=NULL, 
@ParentId int=NULL, 
@AssemblyName nvarchar(100)=NULL, 
@ClassName nvarchar(100)=NULL, 
@IsForm bit=NULL, 
@Icon image=NULL, 
@FK_ModuleId int=NULL, 
@IsDialog bit=NULL, 
@Status bit=NULL, 
@Position int=NULL, 
@IsCrudMenu bit=NULL, 
@MenuUrl nvarchar(50)=NULL, 
@IsFavorite bit=NULL

)
 AS 
 BEGIN 
 
 insert into MenuDetails(Name, MenuCaption, ParentId, AssemblyName, ClassName, IsForm, Icon, FK_ModuleId, IsDialog, Status, Position, IsCrudMenu, MenuUrl, IsFavorite )
 values(@Name, @MenuCaption, @ParentId, @AssemblyName, @ClassName, @IsForm, @Icon, @FK_ModuleId, @IsDialog, @Status, @Position, @IsCrudMenu, @MenuUrl, @IsFavorite )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertModules]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertModules]
(
@Name nvarchar(100)=NULL, @Description nvarchar(1000)=NULL, @Status bit=NULL 
)
 AS 
 BEGIN 
 
 insert into Modules(Name, Description, Status )
 values(@Name, @Description, @Status )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertPOSHold]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertPOSHold]
(
@SalesInvNo int=NULL, 
@InvType nvarchar(50)=NULL, 
@SalesDate datetime=NULL, 
@SalesAccountId int=NULL, 
@PartyAccountId int=NULL, 
@RouteId int=NULL, 
@VehId int=NULL, 
@StafId int=NULL, 
@PriceCategoryId int=NULL, 
@AditionalAmount decimal(18,4)=NULL, 
@Frieght decimal(18,4)=NULL, 
@TotalDiscount decimal(18,4)=NULL, 
@BillDiscount decimal(18,4)=NULL, 
@GrandDiscount decimal(18,4)=NULL, 
@JEId int=NULL, 
@EntryDate datetime=NULL, 
@Remarks nvarchar(50)=NULL, 
@IGSTAmount decimal(18,4)=NULL, 
@SGSTAmount decimal(18,4)=NULL, 
@CGSTAmount decimal(18,4)=NULL, 
@TaxableAmount decimal(18,4)=NULL, 
@FYearID int=NULL, 
@FloodCess decimal(18,4)=NULL, 
@TotalTaxAmount decimal(18,4)=NULL, 
@FK_BranchId int=NULL, 
@CntrInvNo nvarchar(10)=NULL, 
@SequenceNo int=NULL, 
@CashPayment decimal(24,8)=NULL, 
@CardPayment decimal(24,8)=NULL, 
@Credit decimal(24,8)=NULL, 
@FielOne nvarchar(100)=NULL, 
@FieldTwo nvarchar(100)=NULL, 
@FieldThree nvarchar(100)=NULL, 
@FieldFour nvarchar(100)=NULL, 
@Terminal nvarchar(25)=NULL, 
@IsClosed bit=NULL, 
@FreeAmount decimal(24,4)=NULL, 
@ReturnAmount decimal(24,4)=NULL,
@OtherPayment decimal(24,4)=NULL,
@CashAccountId int=NULL, 
@CardAccountId int=NULL, 
@OnlineAccountId int=NULL, 
@IsSettiled bit=NULL, 
@TotalPayment decimal(24,8)=NULL, 
@VoucherNumber nvarchar(25)=NULL,
@errMessage VARCHAR(MAX)='' OUTPUT,
@return int=null OUTPUT,
@type dbo.UT_POSHoldSettings READONLY
)
 AS 
 BEGIN 
 declare @SalesInvId int
  BEGIN TRY
 BEGIN TRANSACTION pro_import
 insert into POSHoldMaster(SalesInvNo, InvType, SalesDate, SalesAccountId, PartyAccountId, RouteId, VehId, StafId, PriceCategoryId, AditionalAmount, Frieght, TotalDiscount, BillDiscount, GrandDiscount, JEId, EntryDate, Remarks, IGSTAmount, SGSTAmount, CGSTAmount, TaxableAmount, FYearID, FloodCess, TotalTaxAmount, FK_BranchId, CntrInvNo, SequenceNo, CashPayment, CardPayment, Credit, FielOne, FieldTwo, FieldThree, FieldFour, Terminal, IsClosed, FreeAmount, ReturnAmount, OtherPayment, CashAccountId, CardAccountId, OnlineAccountId, IsSettiled, TotalPayment, VoucherNumber )
 values(@SalesInvNo, @InvType, @SalesDate, @SalesAccountId, @PartyAccountId, @RouteId, @VehId, @StafId, @PriceCategoryId, @AditionalAmount, @Frieght, @TotalDiscount, @BillDiscount, @GrandDiscount, @JEId, @EntryDate, @Remarks, @IGSTAmount, @SGSTAmount, @CGSTAmount, @TaxableAmount, @FYearID, @FloodCess, @TotalTaxAmount, @FK_BranchId, @CntrInvNo, @SequenceNo, @CashPayment, @CardPayment, @Credit, @FielOne, @FieldTwo, @FieldThree, @FieldFour, @Terminal, @IsClosed, @FreeAmount, @ReturnAmount, @OtherPayment, @CashAccountId, @CardAccountId, @OnlineAccountId, @IsSettiled, @TotalPayment, @VoucherNumber )
 
 set @SalesInvId = SCOPE_IDENTITY()

  insert into POSHoldSettings(SalesInvId, PartyAccountId, ProductId, ProQty, UnitId, ProRate, ProDiscont, DiscAmnt, GrossAmnt, NetAmnt, Date, HSN, GST, GSTAMOUNT, SGST, SGSTAmount, CGST, CGSTAmount, IGST, IGSTAMOUNT, FK_BatchId, Barcode, ItemCalory, EmployeeID, EmployeeCommision, AgentId, AgentCommission, ProductName,ItemSerialNumber,FieldOne,FieldTwo,FieldThree,FieldFour )
 select @SalesInvId, PartyAccountId, ProductId, ProQty, UnitId, ProRate, ProDiscont, DiscAmnt, GrossAmnt, NetAmnt, Date, HSN, GST, GSTAMOUNT, SGST, SGSTAmount, CGST, CGSTAmount, IGST, IGSTAMOUNT, FK_BatchId, Barcode, ItemCalory, EmployeeID, EmployeeCommision, AgentId, AgentCommission, ProductName,ItemSerialNumber,FieldOne,FieldTwo,FieldThree,FieldFour  from @type

 set @return=@SalesInvNo
	select @return 
 COMMIT TRANSACTION pro_import
  END TRY
   BEGIN CATCH
    ROLLBACK TRANSACTION pro_import;
   SELECT @errMessage = ERROR_MESSAGE()
   END CATCH 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertPOSHoldMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertPOSHoldMaster]
(
@SalesInvNo int=NULL, @InvType nvarchar(50)=NULL, @SalesDate datetime=NULL, @SalesAccountId int=NULL, @PartyAccountId int=NULL, @RouteId int=NULL, @VehId int=NULL, @StafId int=NULL, @PriceCategoryId int=NULL, @AditionalAmount decimal(18,4)=NULL, @Frieght decimal(18,4)=NULL, @TotalDiscount decimal(18,4)=NULL, @BillDiscount decimal(18,4)=NULL, @GrandDiscount decimal(18,4)=NULL, @JEId int=NULL, @EntryDate datetime=NULL, @Remarks nvarchar(50)=NULL, @IGSTAmount decimal(18,4)=NULL, @SGSTAmount decimal(18,4)=NULL, @CGSTAmount decimal(18,4)=NULL, @TaxableAmount decimal(18,4)=NULL, @FYearID int=NULL, @FloodCess decimal(18,4)=NULL, @TotalTaxAmount decimal(18,4)=NULL, @FK_BranchId int=NULL, @CntrInvNo nvarchar(10)=NULL, @SequenceNo int=NULL, @CashPayment decimal(24,8)=NULL, @CardPayment decimal(24,8)=NULL, @Credit decimal(24,8)=NULL, @FielOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @Terminal nvarchar(25)=NULL, @IsClosed bit=NULL, @FreeAmount decimal(24,4)=NULL, @ReturnAmount decimal(24,4)=NULL 
)
 AS 
 BEGIN 
 
 insert into POSHoldMaster(SalesInvNo, InvType, SalesDate, SalesAccountId, PartyAccountId, RouteId, VehId, StafId, PriceCategoryId, AditionalAmount, Frieght, TotalDiscount, BillDiscount, GrandDiscount, JEId, EntryDate, Remarks, IGSTAmount, SGSTAmount, CGSTAmount, TaxableAmount, FYearID, FloodCess, TotalTaxAmount, FK_BranchId, CntrInvNo, SequenceNo, CashPayment, CardPayment, Credit, FielOne, FieldTwo, FieldThree, FieldFour, Terminal, IsClosed, FreeAmount, ReturnAmount )
 values(@SalesInvNo, @InvType, @SalesDate, @SalesAccountId, @PartyAccountId, @RouteId, @VehId, @StafId, @PriceCategoryId, @AditionalAmount, @Frieght, @TotalDiscount, @BillDiscount, @GrandDiscount, @JEId, @EntryDate, @Remarks, @IGSTAmount, @SGSTAmount, @CGSTAmount, @TaxableAmount, @FYearID, @FloodCess, @TotalTaxAmount, @FK_BranchId, @CntrInvNo, @SequenceNo, @CashPayment, @CardPayment, @Credit, @FielOne, @FieldTwo, @FieldThree, @FieldFour, @Terminal, @IsClosed, @FreeAmount, @ReturnAmount )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertPOSHoldSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertPOSHoldSettings]
(
@SalesInvId int=NULL, @PartyAccountId int=NULL, @ProductId int=NULL, @ProQty decimal(18,4)=NULL, @UnitId int=NULL, @ProRate decimal(18,4)=NULL, @ProDiscont decimal(18,4)=NULL, @DiscAmnt decimal(18,4)=NULL, @GrossAmnt decimal(18,4)=NULL, @NetAmnt decimal(18,4)=NULL, @Date datetime=NULL, @HSN int=NULL, @GST decimal(18,4)=NULL, @GSTAMOUNT decimal(18,4)=NULL, @SGST decimal(18,4)=NULL, @SGSTAmount decimal(18,4)=NULL, @CGST decimal(18,4)=NULL, @CGSTAmount decimal(18,4)=NULL, @IGST decimal(18,4)=NULL, @IGSTAMOUNT decimal(18,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(20)=NULL 
)
 AS 
 BEGIN 
 
 insert into POSHoldSettings(SalesInvId, PartyAccountId, ProductId, ProQty, UnitId, ProRate, ProDiscont, DiscAmnt, GrossAmnt, NetAmnt, Date, HSN, GST, GSTAMOUNT, SGST, SGSTAmount, CGST, CGSTAmount, IGST, IGSTAMOUNT, FK_BatchId, Barcode )
 values(@SalesInvId, @PartyAccountId, @ProductId, @ProQty, @UnitId, @ProRate, @ProDiscont, @DiscAmnt, @GrossAmnt, @NetAmnt, @Date, @HSN, @GST, @GSTAMOUNT, @SGST, @SGSTAmount, @CGST, @CGSTAmount, @IGST, @IGSTAMOUNT, @FK_BatchId, @Barcode )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertPrevilegeCard]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertPrevilegeCard]
(
@ConsumerCode nvarchar(50)=NULL, @CustomerId int=NULL, @PriceCategoryId int=NULL, @NomineeAccountId int=NULL, @ExpairyDate datetime=NULL, @RenewalDate datetime=NULL, @DOBofCustomer datetime=NULL, @Remarks nvarchar(400)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into PrevilegeCard(ConsumerCode, CustomerId, PriceCategoryId, NomineeAccountId, ExpairyDate, RenewalDate, DOBofCustomer, Remarks, FK_BranchId )
 values(@ConsumerCode, @CustomerId, @PriceCategoryId, @NomineeAccountId, @ExpairyDate, @RenewalDate, @DOBofCustomer, @Remarks, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertPriceCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertPriceCategory]
(
@PCategoryName nvarchar(50)=NULL, @Percentage decimal(18,2)=NULL, @SDiscountPercentage decimal(18,2)=NULL 
)
 AS 
 BEGIN 
 
 insert into PriceCategory(PCategoryName, Percentage, SDiscountPercentage )
 values(@PCategoryName, @Percentage, @SDiscountPercentage )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertPrintDesignMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertPrintDesignMaster]
(
@DesignName nvarchar(50)=NULL, 
@VochrType nvarchar(50)=NULL, 
@isDefault bit=NULL, 
@isLandScape bit=NULL, 
@PageHieght int=NULL, 
@PageWidth int=NULL, 
@LinesPerPage int=NULL, 
@SpaceBtwnLines int=NULL, 
@BackgroundImgPath nvarchar(300)=NULL, 
@PrinterName nvarchar(50)=NULL, 
@PrintType nvarchar(50)=NULL, 
@upScroll int=NULL, 
@downScroll int=NULL, 
@Riverse nvarchar(10)=NULL, 
@Bold_On nvarchar(10)=NULL, 
@Bold_Off nvarchar(10)=NULL, 
@Pitch nvarchar(10)=NULL, 
@FormType nvarchar(50)=NULL, 
@SpaceInLines int=NULL

)
 AS 
 BEGIN 
 
 insert into PrintDesignMaster(DesignName, VochrType, isDefault, isLandScape, PageHieght, PageWidth, LinesPerPage, SpaceBtwnLines, BackgroundImgPath, PrinterName, PrintType, upScroll, downScroll, Riverse, Bold_On, Bold_Off, Pitch, FormType, SpaceInLines )
 values(@DesignName, @VochrType, @isDefault, @isLandScape, @PageHieght, @PageWidth, @LinesPerPage, @SpaceBtwnLines, @BackgroundImgPath, @PrinterName, @PrintType, @upScroll, @downScroll, @Riverse, @Bold_On, @Bold_Off, @Pitch, @FormType, @SpaceInLines )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertPrintDesignSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertPrintDesignSettings]
(
@printMasterId int=NULL, 
@ControleName nvarchar(50)=NULL, 
@Type nvarchar(20)=NULL, 
@FeildText nvarchar(500)=NULL, 
@X int=NULL, 
@Y int=NULL, 
@FontSize int=NULL, 
@Font nvarchar(50)=NULL, 
@isBold bit=NULL, 
@width int=NULL, 
@Alaingmnet nvarchar(10)=NULL, 
@Position nvarchar(25)=NULL, 
@TabIndex int=NULL, 
@iIndex int=NULL, 
@Height int=NULL, 
@Spec nvarchar(100)=NULL

)
 AS 
 BEGIN 
 
 insert into PrintDesignSettings(printMasterId, ControleName, Type, FeildText, X, Y, FontSize, Font, isBold, width, Alaingmnet, Position, TabIndex, iIndex, Height, Spec )
 values(@printMasterId, @ControleName, @Type, @FeildText, @X, @Y, @FontSize, @Font, @isBold, @width, @Alaingmnet, @Position, @TabIndex, @iIndex, @Height, @Spec )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertProCombMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertProCombMaster]
(
@CombProName nvarchar(50)=NULL, 
@ProCombVchrNo int=NULL, 
@Date datetime=NULL, 
@ProQty decimal(24,4)=NULL, 
@UnitId int=NULL, 
@ProSaleRate decimal(24,4)=NULL, 
@ProPurRate decimal(24,4)=NULL, 
@MRP decimal(24,4)=NULL, 
@CombType nvarchar(50)=NULL, 
@Barcode nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into ProCombMaster(CombProName, ProCombVchrNo, Date, ProQty, UnitId, ProSaleRate, ProPurRate, MRP, CombType, Barcode )
 values(@CombProName, @ProCombVchrNo, @Date, @ProQty, @UnitId, @ProSaleRate, @ProPurRate, @MRP, @CombType, @Barcode )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertProCombSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertProCombSettings]
(
@ProCombMasterId int=NULL, 
@ProductId int=NULL, 
@ProQty decimal(24,4)=NULL, 
@UnitId int=NULL, 
@SaleRate decimal(24,4)=NULL, 
@PurRate decimal(24,4)=NULL, 
@MRP decimal(24,4)=NULL, 
@Barcode nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into ProCombSettings(ProCombMasterId, ProductId, ProQty, UnitId, SaleRate, PurRate, MRP, Barcode )
 values(@ProCombMasterId, @ProductId, @ProQty, @UnitId, @SaleRate, @PurRate, @MRP, @Barcode )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertProduct]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertProduct]
(
@ProductCode int=NULL, 
@ProductName nvarchar(120)=NULL, 
@Specification nvarchar(50)=NULL, 
@PGroupId int=NULL, 
@UnitId int=NULL, 
@BrandId int=NULL, 
@TCategoryId int=NULL, 
@HSNNo nvarchar(10)=NULL, 
@PurchasePrice decimal(24,4)=NULL, 
@SellingPrice decimal(24,4)=NULL, 
@MRP decimal(24,4)=NULL, 
@OpeningStock decimal(24,4)=NULL, 
@OppStockDate datetime=NULL, 
@ReOrderStock decimal(24,4)=NULL, 
@AccountId int=NULL, 
@GodwnId int=NULL, 
@SelfId int=NULL, 
@PurchaseDis decimal(24,4)=NULL, 
@SaleDis decimal(24,4)=NULL, 
@AliasCode nvarchar(50)=NULL, 
@AliasName nvarchar(120)=NULL, 
@Remarks nvarchar(500)=NULL, 
@LocalName nvarchar(500)=NULL, 
@PurchaseUnit int=NULL, 
@SalesUnit int=NULL, 
@ReportUnit int=NULL, 
@image image=NULL, 
@filePath nvarchar(400)=NULL, 
@description nvarchar(5)=NULL, 
@ProductType nvarchar(400)=NULL, 
@isActive bit=NULL, 
@isInclusive bit=NULL, 
@Barcode nvarchar(20)=NULL, 
@IsExpairing bit=NULL, 
@ExpairingDays decimal(24,4)=NULL, 
@MinimumMargin decimal(24,4)=NULL, 
@NormalMargin decimal(24,4)=NULL, 
@RetailMargin decimal(24,4)=NULL, 
@WholesaleMargin decimal(24,4)=NULL, 
@MRPMargin decimal(24,4)=NULL, 
@IsMarginBasedPercentage bit=NULL, 
@LoyalityPont int=NULL, 
@AgentCommision decimal(24,4)=NULL, 
@SalesmanCommision decimal(24,4)=NULL, 
@IsCommisionBasedPercentage bit=NULL, 
@FieldOne nvarchar(100)=NULL, 
@FieldTwo nvarchar(100)=NULL, 
@FieldThree nvarchar(100)=NULL, 
@FieldFour nvarchar(100)=NULL, 
@FieldFive nvarchar(100)=NULL, 
@IsScale bit=NULL, 
@ListOrder int=NULL, 
@IsAllowMultiple bit=NULL, 
@isRateBasedWeight bit=NULL, 
@PriorityStatus nvarchar(25)=NULL, 
@CookingTime int=NULL, 
@IsFavorite bit=NULL, 
@RestaurantItemType nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into Product(ProductCode, ProductName, Specification, PGroupId, UnitId, BrandId, TCategoryId, HSNNo, PurchasePrice, SellingPrice, MRP, OpeningStock, OppStockDate, ReOrderStock, AccountId, GodwnId, SelfId, PurchaseDis, SaleDis, AliasCode, AliasName, Remarks, LocalName, PurchaseUnit, SalesUnit, ReportUnit, image, filePath, description, ProductType, isActive, isInclusive, Barcode, IsExpairing, ExpairingDays, MinimumMargin, NormalMargin, RetailMargin, WholesaleMargin, MRPMargin, IsMarginBasedPercentage, LoyalityPont, AgentCommision, SalesmanCommision, IsCommisionBasedPercentage, FieldOne, FieldTwo, FieldThree, FieldFour, FieldFive, IsScale, ListOrder, IsAllowMultiple, isRateBasedWeight, PriorityStatus, CookingTime, IsFavorite, RestaurantItemType )
 values(@ProductCode, @ProductName, @Specification, @PGroupId, @UnitId, @BrandId, @TCategoryId, @HSNNo, @PurchasePrice, @SellingPrice, @MRP, @OpeningStock, @OppStockDate, @ReOrderStock, @AccountId, @GodwnId, @SelfId, @PurchaseDis, @SaleDis, @AliasCode, @AliasName, @Remarks, @LocalName, @PurchaseUnit, @SalesUnit, @ReportUnit, @image, @filePath, @description, @ProductType, @isActive, @isInclusive, @Barcode, @IsExpairing, @ExpairingDays, @MinimumMargin, @NormalMargin, @RetailMargin, @WholesaleMargin, @MRPMargin, @IsMarginBasedPercentage, @LoyalityPont, @AgentCommision, @SalesmanCommision, @IsCommisionBasedPercentage, @FieldOne, @FieldTwo, @FieldThree, @FieldFour, @FieldFive, @IsScale, @ListOrder, @IsAllowMultiple, @isRateBasedWeight, @PriorityStatus, @CookingTime, @IsFavorite, @RestaurantItemType )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertProductBarcode]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertProductBarcode]
(
@Barcode nvarchar(100)=NULL, 
@BarcodeType nvarchar(100)=NULL, 
@ExpairyDate datetime=NULL, 
@ManufDate datetime=NULL, 
@FK_ProductId int=NULL, 
@FK_UnitId int=NULL, 
@FK_BatchId int=NULL, 
@FK_BranchId int=NULL, 
@Remarks nvarchar(100)=NULL, 
@PurchasePrice decimal(24,4)=NULL, 
@SellingPrice decimal(24,4)=NULL, 
@MRP decimal(24,4)=NULL, 
@ProductCost decimal(24,4)=NULL, 
@FieldOne nvarchar(100)=NULL, 
@FieldTwo nvarchar(100)=NULL, 
@FieldThree nvarchar(100)=NULL, 
@FieldFour nvarchar(100)=NULL, 
@MinimuPrice decimal(24,4)=NULL, 
@RetailPrice decimal(24,4)=NULL, 
@WholesalePrice decimal(24,4)=NULL, 
@ProductBarcodeName nvarchar(50)=NULL, 
@LocalName nvarchar(50)=NULL, 
@image image=NULL

)
 AS 
 BEGIN 
 
 insert into ProductBarcode(Barcode, BarcodeType, ExpairyDate, ManufDate, FK_ProductId, FK_UnitId, FK_BatchId, FK_BranchId, Remarks, PurchasePrice, SellingPrice, MRP, ProductCost, FieldOne, FieldTwo, FieldThree, FieldFour, MinimuPrice, RetailPrice, WholesalePrice, ProductBarcodeName, LocalName, image )
 values(@Barcode, @BarcodeType, @ExpairyDate, @ManufDate, @FK_ProductId, @FK_UnitId, @FK_BatchId, @FK_BranchId, @Remarks, @PurchasePrice, @SellingPrice, @MRP, @ProductCost, @FieldOne, @FieldTwo, @FieldThree, @FieldFour, @MinimuPrice, @RetailPrice, @WholesalePrice, @ProductBarcodeName, @LocalName, @image )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertProductBarcodeScale]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertProductBarcodeScale]
(
@FK_ProductId int=NULL, 
@FK_BarcodeScale int=NULL

)
 AS 
 BEGIN 
 
 insert into ProductBarcodeScale(FK_ProductId, FK_BarcodeScale )
 values(@FK_ProductId, @FK_BarcodeScale )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertProductGroup]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertProductGroup]
(
@PGroupName nvarchar(50)=NULL, 
@Under nvarchar(50)=NULL, 
@SalesInsentive decimal(24,4)=NULL, 
@SalesDis decimal(24,4)=NULL, 
@PurchDis decimal(24,4)=NULL, 
@Remarks nvarchar(1000)=NULL, 
@ListOrder int=NULL, 
@isShowInGrid bit=NULL

)
 AS 
 BEGIN 
 
 insert into ProductGroup(PGroupName, Under, SalesInsentive, SalesDis, PurchDis, Remarks, ListOrder, isShowInGrid )
 values(@PGroupName, @Under, @SalesInsentive, @SalesDis, @PurchDis, @Remarks, @ListOrder, @isShowInGrid )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertProductPriceCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertProductPriceCategory]
(
@ProductId int=NULL, 
@PCategoryId int=NULL, 
@UnitId int=NULL, 
@PGroupId int=NULL, 
@SPrice decimal(24,4)=NULL, 
@SPDisc decimal(24,4)=NULL, 
@PPrice decimal(24,4)=NULL, 
@PDisc decimal(24,4)=NULL, 
@Barcode nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into ProductPriceCategory(ProductId, PCategoryId, UnitId, PGroupId, SPrice, SPDisc, PPrice, PDisc, Barcode )
 values(@ProductId, @PCategoryId, @UnitId, @PGroupId, @SPrice, @SPDisc, @PPrice, @PDisc, @Barcode )
 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spInsertProductPrinter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertProductPrinter]
(
@FK_KOTPrinterId int=NULL, 
@FK_ProductId int=NULL, 
@FK_BranchId int=NULL

)
 AS 
 BEGIN 
 
 insert into ProductPrinter(FK_KOTPrinterId, FK_ProductId, FK_BranchId )
 values(@FK_KOTPrinterId, @FK_ProductId, @FK_BranchId )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertProductUnit]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertProductUnit]
(
@ProductId int=NULL, @UnitId int=NULL, @UnitType nvarchar(100)=NULL, @Barcode nvarchar(20)=NULL, @UnitQty decimal(24,8)=NULL, @ProductUnitName nvarchar(100)=NULL, @IsInclusive bit=NULL, @OpeningStock decimal(24,8)=NULL, @SalesPrice decimal(24,8)=NULL 
)
 AS 
 BEGIN 
 
 insert into ProductUnit(ProductId, UnitId, UnitType, Barcode, UnitQty, ProductUnitName, IsInclusive, OpeningStock, SalesPrice )
 values(@ProductId, @UnitId, @UnitType, @Barcode, @UnitQty, @ProductUnitName, @IsInclusive, @OpeningStock, @SalesPrice )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertPurInvMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertPurInvMaster]
(
@PurInvNo int=NULL, 
@InvType nvarchar(50)=NULL, 
@PurDate datetime=NULL, 
@PurActId int=NULL, 
@PartyAccId int=NULL, 
@PriceCategoryId int=NULL, 
@AdditionalAmnt decimal(24,4)=NULL, 
@Frieght decimal(24,4)=NULL, 
@TotalDiscount decimal(24,4)=NULL, 
@BillDisc decimal(24,4)=NULL, 
@TotalTax decimal(24,4)=NULL, 
@TotalAmnt decimal(24,4)=NULL, 
@JEId int=NULL, 
@EntryDate datetime=NULL, 
@Remarks nvarchar(250)=NULL, 
@IGSTAmount decimal(24,4)=NULL, 
@SGSTAmount decimal(24,4)=NULL, 
@CGSTAmount decimal(24,4)=NULL, 
@RefInvNo nvarchar(50)=NULL, 
@Taxable decimal(24,4)=NULL, 
@FK_BranchId int=NULL, 
@CashPayment decimal(24,4)=NULL, 
@CardPayment decimal(24,4)=NULL, 
@Credit decimal(24,4)=NULL, 
@FreeAmount decimal(24,4)=NULL, 
@ReturnAmount decimal(24,4)=NULL, 
@IsClosed bit=NULL, 
@FielOne nvarchar(100)=NULL, 
@FieldTwo nvarchar(100)=NULL, 
@FieldThree nvarchar(100)=NULL, 
@FieldFour nvarchar(100)=NULL, 
@CashAccountId int=NULL, 
@CardAccountId int=NULL, 
@OnlineAccountId int=NULL, 
@IsSettiled bit=NULL, 
@TotalPayment decimal(24,8)=NULL, 
@VoucherNumber nvarchar(25)=NULL, 
@SequenceNo int=NULL, 
@Terminal nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into PurInvMaster(PurInvNo, InvType, PurDate, PurActId, PartyAccId, PriceCategoryId, AdditionalAmnt, Frieght, TotalDiscount, BillDisc, TotalTax, TotalAmnt, JEId, EntryDate, Remarks, IGSTAmount, SGSTAmount, CGSTAmount, RefInvNo, Taxable, FK_BranchId, CashPayment, CardPayment, Credit, FreeAmount, ReturnAmount, IsClosed, FielOne, FieldTwo, FieldThree, FieldFour, CashAccountId, CardAccountId, OnlineAccountId, IsSettiled, TotalPayment, VoucherNumber, SequenceNo, Terminal )
 values(@PurInvNo, @InvType, @PurDate, @PurActId, @PartyAccId, @PriceCategoryId, @AdditionalAmnt, @Frieght, @TotalDiscount, @BillDisc, @TotalTax, @TotalAmnt, @JEId, @EntryDate, @Remarks, @IGSTAmount, @SGSTAmount, @CGSTAmount, @RefInvNo, @Taxable, @FK_BranchId, @CashPayment, @CardPayment, @Credit, @FreeAmount, @ReturnAmount, @IsClosed, @FielOne, @FieldTwo, @FieldThree, @FieldFour, @CashAccountId, @CardAccountId, @OnlineAccountId, @IsSettiled, @TotalPayment, @VoucherNumber, @SequenceNo, @Terminal )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertPurInvSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertPurInvSettings]
(
@PurInvId int=NULL, @PartyAccId int=NULL, @ProId int=NULL, @ProQty decimal(18,4)=NULL, @UnitId int=NULL, @ProRate decimal(18,4)=NULL, @Disc decimal(18,4)=NULL, @DiscAmnt decimal(18,4)=NULL, @Gross decimal(18,4)=NULL, @NetAmnt decimal(18,4)=NULL, @Date datetime=NULL, @HSN int=NULL, @GST decimal(18,4)=NULL, @GSTAMOUNT decimal(18,4)=NULL, @SGST decimal(18,4)=NULL, @SGSTAmount decimal(18,4)=NULL, @CGST decimal(18,4)=NULL, @CGSTAmount decimal(18,4)=NULL, @IGST decimal(18,4)=NULL, @IGSTAMOUNT decimal(18,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 insert into PurInvSettings(PurInvId, PartyAccId, ProId, ProQty, UnitId, ProRate, Disc, DiscAmnt, Gross, NetAmnt, Date, HSN, GST, GSTAMOUNT, SGST, SGSTAmount, CGST, CGSTAmount, IGST, IGSTAMOUNT, FK_BatchId, Barcode )
 values(@PurInvId, @PartyAccId, @ProId, @ProQty, @UnitId, @ProRate, @Disc, @DiscAmnt, @Gross, @NetAmnt, @Date, @HSN, @GST, @GSTAMOUNT, @SGST, @SGSTAmount, @CGST, @CGSTAmount, @IGST, @IGSTAMOUNT, @FK_BatchId, @Barcode )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertPurRtnInvMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertPurRtnInvMaster]
(
@PurRtnInvNo int=NULL, 
@PurRtnDate datetime=NULL, 
@PurRtnAcId int=NULL, 
@PartyAcId int=NULL, 
@PriceCategoryId int=NULL, 
@AdditionalAmnt decimal(24,4)=NULL, 
@Frieght decimal(24,4)=NULL, 
@TotalDiscount decimal(24,4)=NULL, 
@BillDisc decimal(24,4)=NULL, 
@TotalTax decimal(24,4)=NULL, 
@TotalAmnt decimal(24,4)=NULL, 
@JEId int=NULL, 
@EntryDate datetime=NULL, 
@Remarks nvarchar(400)=NULL, 
@IGSTAmount decimal(24,4)=NULL, 
@SGSTAmount decimal(24,4)=NULL, 
@CGSTAmount decimal(24,4)=NULL, 
@Taxable decimal(24,4)=NULL, 
@FK_BranchId int=NULL, 
@SequenceNo int=NULL, 
@Terminal nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into PurRtnInvMaster(PurRtnInvNo, PurRtnDate, PurRtnAcId, PartyAcId, PriceCategoryId, AdditionalAmnt, Frieght, TotalDiscount, BillDisc, TotalTax, TotalAmnt, JEId, EntryDate, Remarks, IGSTAmount, SGSTAmount, CGSTAmount, Taxable, FK_BranchId, SequenceNo, Terminal )
 values(@PurRtnInvNo, @PurRtnDate, @PurRtnAcId, @PartyAcId, @PriceCategoryId, @AdditionalAmnt, @Frieght, @TotalDiscount, @BillDisc, @TotalTax, @TotalAmnt, @JEId, @EntryDate, @Remarks, @IGSTAmount, @SGSTAmount, @CGSTAmount, @Taxable, @FK_BranchId, @SequenceNo, @Terminal )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertPurRtnInvSttings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertPurRtnInvSttings]
(
@PurRtnInvId int=NULL, 
@ProId int=NULL, 
@ProQty decimal(24,4)=NULL, 
@UnitId int=NULL, 
@ProRate decimal(24,4)=NULL, 
@Disc decimal(24,4)=NULL, 
@DiscAmnt decimal(24,4)=NULL, 
@Gross decimal(24,4)=NULL, 
@Tax decimal(24,4)=NULL, 
@TaxAmnt decimal(24,4)=NULL, 
@NetAmnt decimal(24,4)=NULL, 
@PartyAccId int=NULL, 
@Date datetime=NULL, 
@HSN nvarchar(20)=NULL, 
@GST decimal(24,4)=NULL, 
@GSTAMOUNT decimal(24,4)=NULL, 
@SGST decimal(24,4)=NULL, 
@SGSTAmount decimal(24,4)=NULL, 
@CGST decimal(24,4)=NULL, 
@CGSTAmount decimal(24,4)=NULL, 
@IGST decimal(24,4)=NULL, 
@IGSTAMOUNT decimal(24,4)=NULL, 
@FK_BatchId int=NULL, 
@Barcode nvarchar(100)=NULL

)
 AS 
 BEGIN 
 
 insert into PurRtnInvSttings(PurRtnInvId, ProId, ProQty, UnitId, ProRate, Disc, DiscAmnt, Gross, Tax, TaxAmnt, NetAmnt, PartyAccId, Date, HSN, GST, GSTAMOUNT, SGST, SGSTAmount, CGST, CGSTAmount, IGST, IGSTAMOUNT, FK_BatchId, Barcode )
 values(@PurRtnInvId, @ProId, @ProQty, @UnitId, @ProRate, @Disc, @DiscAmnt, @Gross, @Tax, @TaxAmnt, @NetAmnt, @PartyAccId, @Date, @HSN, @GST, @GSTAMOUNT, @SGST, @SGSTAmount, @CGST, @CGSTAmount, @IGST, @IGSTAMOUNT, @FK_BatchId, @Barcode )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertRateFormat]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertRateFormat]
(
@Name int=NULL, 
@Value nvarchar(1)=NULL, 
@Alternative nvarchar(1)=NULL

)
 AS 
 BEGIN 
 
 insert into RateFormat(Name, Value, Alternative )
 values(@Name, @Value, @Alternative )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertRawmaterialMapping]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertRawmaterialMapping]
(
@FK_ProductId int=NULL, @Quantity decimal(24,8)=NULL, @FK_UnitId int=NULL, @Rate decimal(24,8)=NULL, @NetAmount decimal(24,8)=NULL, @FK_ManufactureHdrId int=NULL, @FK_BatchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into RawmaterialMapping(FK_ProductId, Quantity, FK_UnitId, Rate, NetAmount, FK_ManufactureHdrId, FK_BatchId )
 values(@FK_ProductId, @Quantity, @FK_UnitId, @Rate, @NetAmount, @FK_ManufactureHdrId, @FK_BatchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertReceiptAndPayment]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertReceiptAndPayment]
(
@IsSettiled bit=NULL, 
@CntrInvNo nvarchar(15)=NULL, 
@SalesDate datetime=NULL, 
@GrandDiscount decimal(24,4)=NULL, 
@TotalPayment decimal(24,4)=NULL, 
@Credit decimal(24,4)=NULL, 
@Payment decimal(24,4)=NULL, 
@Balance decimal(24,4)=NULL, 
@VchrNo nvarchar(15)=NULL, 
@InvoiceId int=NULL, 
@VchrId int=NULL, 
@InvoiceType nvarchar(20)=NULL

)
 AS 
 BEGIN 
 
 insert into ReceiptAndPayment(IsSettiled, CntrInvNo, SalesDate, GrandDiscount, TotalPayment, Credit, Payment, Balance, VchrNo, InvoiceId, VchrId, InvoiceType )
 values(@IsSettiled, @CntrInvNo, @SalesDate, @GrandDiscount, @TotalPayment, @Credit, @Payment, @Balance, @VchrNo, @InvoiceId, @VchrId, @InvoiceType )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertReminderTB]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertReminderTB]
(
@RemainderHead nvarchar(50)=NULL, @Remainder nvarchar(400)=NULL, @CurrentDate nvarchar(20)=NULL, @ReminderDate nvarchar(20)=NULL, @UserId int=NULL, @TargetUserId int=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into ReminderTB(RemainderHead, Remainder, CurrentDate, ReminderDate, UserId, TargetUserId, FK_BranchId )
 values(@RemainderHead, @Remainder, @CurrentDate, @ReminderDate, @UserId, @TargetUserId, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertRestaurantTable]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertRestaurantTable]
(
@TableName nvarchar(100)=NULL, 
@ChairCount int=NULL, 
@FloorName nvarchar(100)=NULL, 
@XPosition int=NULL, 
@YPostion int=NULL, 
@Image image=NULL, 
@FK_EmplyeeId int=NULL

)
 AS 
 BEGIN 
 
 insert into RestaurantTable(TableName, ChairCount, FloorName, XPosition, YPostion, Image, FK_EmplyeeId )
 values(@TableName, @ChairCount, @FloorName, @XPosition, @YPostion, @Image, @FK_EmplyeeId )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertRoot]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertRoot]
(
@RootName nvarchar(50)=NULL, @WeekDay nvarchar(20)=NULL, @SalesManId int=NULL, @DriverId int=NULL, @VehicleId int=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into Root(RootName, WeekDay, SalesManId, DriverId, VehicleId, FK_BranchId )
 values(@RootName, @WeekDay, @SalesManId, @DriverId, @VehicleId, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertSalaryProcessMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertSalaryProcessMaster]
(
@SPrcsVchrNo int=NULL, @SPrcsDate datetime=NULL, @SlryPrd nvarchar(50)=NULL, @DateFrom datetime=NULL, @DateTo datetime=NULL, @StaffType nvarchar(50)=NULL, @Description nvarchar(250)=NULL, @CashOrBankAcctId int=NULL, @CashOrBankAcctName nvarchar(50)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into SalaryProcessMaster(SPrcsVchrNo, SPrcsDate, SlryPrd, DateFrom, DateTo, StaffType, Description, CashOrBankAcctId, CashOrBankAcctName, FK_BranchId )
 values(@SPrcsVchrNo, @SPrcsDate, @SlryPrd, @DateFrom, @DateTo, @StaffType, @Description, @CashOrBankAcctId, @CashOrBankAcctName, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertSalesCounter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertSalesCounter]
(
@SalesCounterName nvarchar(50)=NULL, @CashAccountId int=NULL, @Remarks nvarchar(400)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into SalesCounter(SalesCounterName, CashAccountId, Remarks, FK_BranchId )
 values(@SalesCounterName, @CashAccountId, @Remarks, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertSalesEstimateMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertSalesEstimateMaster]
(
@SalesInvNo int=NULL, 
@InvType nvarchar(50)=NULL, 
@SalesDate datetime=NULL, 
@SalesAccountId int=NULL, 
@PartyAccountId int=NULL, 
@RouteId int=NULL, 
@VehId int=NULL, 
@StafId int=NULL, 
@PriceCategoryId int=NULL, 
@AditionalAmount decimal(24,4)=NULL, 
@Frieght decimal(24,4)=NULL, 
@TotalDiscount decimal(24,4)=NULL, 
@BillDiscount decimal(24,4)=NULL, 
@GrandDiscount decimal(24,4)=NULL, 
@JEId int=NULL, 
@EntryDate datetime=NULL, 
@Remarks nvarchar(50)=NULL, 
@IGSTAmount decimal(24,4)=NULL, 
@SGSTAmount decimal(24,4)=NULL, 
@CGSTAmount decimal(24,4)=NULL, 
@TaxableAmount decimal(24,4)=NULL, 
@FYearID int=NULL, 
@FloodCess decimal(24,4)=NULL, 
@TotalTaxAmount decimal(24,4)=NULL, 
@FK_BranchId int=NULL, 
@CntrInvNo nvarchar(10)=NULL, 
@SequenceNo int=NULL, 
@CashPayment decimal(24,8)=NULL, 
@CardPayment decimal(24,8)=NULL, 
@Credit decimal(24,8)=NULL, 
@FielOne nvarchar(100)=NULL, 
@FieldTwo nvarchar(100)=NULL, 
@FieldThree nvarchar(100)=NULL, 
@FieldFour nvarchar(100)=NULL, 
@Terminal nvarchar(25)=NULL, 
@IsClosed bit=NULL, 
@FreeAmount decimal(24,4)=NULL, 
@ReturnAmount decimal(24,4)=NULL, 
@OtherPayment decimal(24,4)=NULL, 
@CashAccountId int=NULL, 
@CardAccountId int=NULL, 
@OnlineAccountId int=NULL, 
@IsSettiled bit=NULL, 
@TotalPayment decimal(24,8)=NULL, 
@VoucherNumber nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into SalesEstimateMaster(SalesInvNo, InvType, SalesDate, SalesAccountId, PartyAccountId, RouteId, VehId, StafId, PriceCategoryId, AditionalAmount, Frieght, TotalDiscount, BillDiscount, GrandDiscount, JEId, EntryDate, Remarks, IGSTAmount, SGSTAmount, CGSTAmount, TaxableAmount, FYearID, FloodCess, TotalTaxAmount, FK_BranchId, CntrInvNo, SequenceNo, CashPayment, CardPayment, Credit, FielOne, FieldTwo, FieldThree, FieldFour, Terminal, IsClosed, FreeAmount, ReturnAmount, OtherPayment, CashAccountId, CardAccountId, OnlineAccountId, IsSettiled, TotalPayment, VoucherNumber )
 values(@SalesInvNo, @InvType, @SalesDate, @SalesAccountId, @PartyAccountId, @RouteId, @VehId, @StafId, @PriceCategoryId, @AditionalAmount, @Frieght, @TotalDiscount, @BillDiscount, @GrandDiscount, @JEId, @EntryDate, @Remarks, @IGSTAmount, @SGSTAmount, @CGSTAmount, @TaxableAmount, @FYearID, @FloodCess, @TotalTaxAmount, @FK_BranchId, @CntrInvNo, @SequenceNo, @CashPayment, @CardPayment, @Credit, @FielOne, @FieldTwo, @FieldThree, @FieldFour, @Terminal, @IsClosed, @FreeAmount, @ReturnAmount, @OtherPayment, @CashAccountId, @CardAccountId, @OnlineAccountId, @IsSettiled, @TotalPayment, @VoucherNumber )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertSalesEstimateSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertSalesEstimateSettings]
(
@SalesInvId int=NULL, 
@PartyAccountId int=NULL, 
@ProductId int=NULL, 
@ProQty decimal(18,4)=NULL, 
@UnitId int=NULL, 
@ProRate decimal(18,4)=NULL, 
@ProDiscont decimal(18,4)=NULL, 
@DiscAmnt decimal(18,4)=NULL, 
@GrossAmnt decimal(18,4)=NULL, 
@NetAmnt decimal(18,4)=NULL, 
@Date datetime=NULL, 
@HSN nvarchar(20)=NULL, 
@GST decimal(18,4)=NULL, 
@GSTAMOUNT decimal(18,4)=NULL, 
@SGST decimal(18,4)=NULL, 
@SGSTAmount decimal(18,4)=NULL, 
@CGST decimal(18,4)=NULL, 
@CGSTAmount decimal(18,4)=NULL, 
@IGST decimal(18,4)=NULL, 
@IGSTAMOUNT decimal(18,4)=NULL, 
@FK_BatchId int=NULL, 
@Barcode nvarchar(20)=NULL, 
@ItemCalory decimal(18,4)=NULL, 
@EmployeeID int=NULL, 
@EmployeeCommision decimal(18,4)=NULL, 
@AgentId int=NULL, 
@AgentCommission decimal(18,4)=NULL, 
@ProductName nvarchar(50)=NULL

)
 AS 
 BEGIN 
 
 insert into SalesEstimateSettings(SalesInvId, PartyAccountId, ProductId, ProQty, UnitId, ProRate, ProDiscont, DiscAmnt, GrossAmnt, NetAmnt, Date, HSN, GST, GSTAMOUNT, SGST, SGSTAmount, CGST, CGSTAmount, IGST, IGSTAMOUNT, FK_BatchId, Barcode, ItemCalory, EmployeeID, EmployeeCommision, AgentId, AgentCommission, ProductName )
 values(@SalesInvId, @PartyAccountId, @ProductId, @ProQty, @UnitId, @ProRate, @ProDiscont, @DiscAmnt, @GrossAmnt, @NetAmnt, @Date, @HSN, @GST, @GSTAMOUNT, @SGST, @SGSTAmount, @CGST, @CGSTAmount, @IGST, @IGSTAMOUNT, @FK_BatchId, @Barcode, @ItemCalory, @EmployeeID, @EmployeeCommision, @AgentId, @AgentCommission, @ProductName )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertSalesOrderMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertSalesOrderMaster]
(
@VchrNo int=NULL, 
@CustomerId int=NULL, 
@SalesAccountId int=NULL, 
@SalesManId int=NULL, 
@RouteId int=NULL, 
@PriceCategoryId int=NULL, 
@Remarks nvarchar(400)=NULL, 
@NetAmount decimal(24,4)=NULL, 
@AdditionalAmnt decimal(24,4)=NULL, 
@Freight decimal(24,4)=NULL, 
@BillDiscount decimal(24,4)=NULL, 
@AddDiscount decimal(24,4)=NULL, 
@GrandTotal decimal(24,4)=NULL, 
@InvType nvarchar(50)=NULL, 
@JEid int=NULL, 
@EntryDate datetime=NULL, 
@InvDate datetime=NULL, 
@Status bit=NULL, 
@Taxable decimal(24,4)=NULL, 
@TotalTax decimal(24,4)=NULL, 
@FK_BranchId int=NULL, 
@TargetDate datetime=NULL, 
@AdvanceAmount decimal(24,4)=NULL, 
@FieldOne nvarchar(100)=NULL, 
@FieldTwo nvarchar(100)=NULL, 
@FieldThree nvarchar(100)=NULL, 
@FieldFour nvarchar(100)=NULL, 
@FieldFive nvarchar(100)=NULL

)
 AS 
 BEGIN 
 
 insert into SalesOrderMaster(VchrNo, CustomerId, SalesAccountId, SalesManId, RouteId, PriceCategoryId, Remarks, NetAmount, AdditionalAmnt, Freight, BillDiscount, AddDiscount, GrandTotal, InvType, JEid, EntryDate, InvDate, Status, Taxable, TotalTax, FK_BranchId, TargetDate, AdvanceAmount, FieldOne, FieldTwo, FieldThree, FieldFour, FieldFive )
 values(@VchrNo, @CustomerId, @SalesAccountId, @SalesManId, @RouteId, @PriceCategoryId, @Remarks, @NetAmount, @AdditionalAmnt, @Freight, @BillDiscount, @AddDiscount, @GrandTotal, @InvType, @JEid, @EntryDate, @InvDate, @Status, @Taxable, @TotalTax, @FK_BranchId, @TargetDate, @AdvanceAmount, @FieldOne, @FieldTwo, @FieldThree, @FieldFour, @FieldFive )
 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spInsertSalesOrderSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertSalesOrderSettings]
(
@SalesOrderMasterId int=NULL, 
@PartyAccountId int=NULL, 
@ProductId int=NULL, 
@ProQty decimal(24,4)=NULL, 
@UnitId int=NULL, 
@ProRate decimal(24,4)=NULL, 
@ProDiscont decimal(24,4)=NULL, 
@DiscAmnt decimal(24,4)=NULL, 
@GrossAmnt decimal(24,4)=NULL, 
@NetAmnt decimal(24,4)=NULL, 
@Taxable decimal(24,4)=NULL, 
@Tax decimal(24,4)=NULL, 
@TaxAmount decimal(24,4)=NULL, 
@FK_BatchId int=NULL, 
@Barcode nvarchar(100)=NULL

)
 AS 
 BEGIN 
 
 insert into SalesOrderSettings(SalesOrderMasterId, PartyAccountId, ProductId, ProQty, UnitId, ProRate, ProDiscont, DiscAmnt, GrossAmnt, NetAmnt, Taxable, Tax, TaxAmount, FK_BatchId, Barcode )
 values(@SalesOrderMasterId, @PartyAccountId, @ProductId, @ProQty, @UnitId, @ProRate, @ProDiscont, @DiscAmnt, @GrossAmnt, @NetAmnt, @Taxable, @Tax, @TaxAmount, @FK_BatchId, @Barcode )
 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spInsertSalesRtnMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertSalesRtnMaster]
(
@SlRtnInvNo int=NULL, 
@SlsRtnDate datetime=NULL, 
@SlsRtnAccountId int=NULL, 
@PartyAccountId int=NULL, 
@RouteId int=NULL, 
@VehId int=NULL, 
@StaffId int=NULL, 
@PriceCategoryId int=NULL, 
@AditionalAmnt decimal(24,4)=NULL, 
@Frieght decimal(24,4)=NULL, 
@TotalDiscount decimal(24,4)=NULL, 
@BillDiscount decimal(24,4)=NULL, 
@TotalAmount decimal(24,4)=NULL, 
@JeId int=NULL, 
@EntryDate datetime=NULL, 
@Remarks nvarchar(400)=NULL, 
@IGSTAmount decimal(24,4)=NULL, 
@SGSTAmount decimal(24,4)=NULL, 
@CGSTAmount decimal(24,4)=NULL, 
@FYearID int=NULL, 
@TotalTax decimal(24,4)=NULL, 
@Taxable decimal(24,4)=NULL, 
@FK_BranchId int=NULL, 
@SequenceNo int=NULL, 
@Terminal nvarchar(25)=NULL, 
@RefInvNo nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into SalesRtnMaster(SlRtnInvNo, SlsRtnDate, SlsRtnAccountId, PartyAccountId, RouteId, VehId, StaffId, PriceCategoryId, AditionalAmnt, Frieght, TotalDiscount, BillDiscount, TotalAmount, JeId, EntryDate, Remarks, IGSTAmount, SGSTAmount, CGSTAmount, FYearID, TotalTax, Taxable, FK_BranchId, SequenceNo, Terminal, RefInvNo )
 values(@SlRtnInvNo, @SlsRtnDate, @SlsRtnAccountId, @PartyAccountId, @RouteId, @VehId, @StaffId, @PriceCategoryId, @AditionalAmnt, @Frieght, @TotalDiscount, @BillDiscount, @TotalAmount, @JeId, @EntryDate, @Remarks, @IGSTAmount, @SGSTAmount, @CGSTAmount, @FYearID, @TotalTax, @Taxable, @FK_BranchId, @SequenceNo, @Terminal, @RefInvNo )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertSelectedFYear]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertSelectedFYear]
(
@FyearId int=NULL 
)
 AS 
 BEGIN 
 
 insert into SelectedFYear(FyearId )
 values(@FyearId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertSelf]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertSelf]
(
@GodownId int=NULL, @SelfName nvarchar(50)=NULL 
)
 AS 
 BEGIN 
 
 insert into Self(GodownId, SelfName )
 values(@GodownId, @SelfName )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertSettings]
(
@Name nvarchar(25)=NULL, @Caption nvarchar(100)=NULL, @Remarks nvarchar(100)=NULL, @BitValue bit=NULL, @StringValue nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 insert into Settings(Name, Caption, Remarks, BitValue, StringValue )
 values(@Name, @Caption, @Remarks, @BitValue, @StringValue )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertSlryPrcsSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertSlryPrcsSettings]
(
@SprcsMasterID int=NULL, @EmpID int=NULL, @EmpName nvarchar(50)=NULL, @LeavsTaken decimal(18,2)=NULL, @CasualLeave decimal(18,2)=NULL, @BasicSalary decimal(18,2)=NULL, @LeaveDiduction decimal(18,2)=NULL, @Incentive decimal(18,2)=NULL, @OtherAmnt decimal(18,2)=NULL, @SalaryCut decimal(18,2)=NULL, @OtherDiduction decimal(18,2)=NULL, @NetSalary decimal(18,2)=NULL, @SlryAdvance decimal(18,2)=NULL, @PaidAmnt decimal(18,2)=NULL, @Balance decimal(18,2)=NULL, @Remarks nvarchar(250)=NULL 
)
 AS 
 BEGIN 
 
 insert into SlryPrcsSettings(SprcsMasterID, EmpID, EmpName, LeavsTaken, CasualLeave, BasicSalary, LeaveDiduction, Incentive, OtherAmnt, SalaryCut, OtherDiduction, NetSalary, SlryAdvance, PaidAmnt, Balance, Remarks )
 values(@SprcsMasterID, @EmpID, @EmpName, @LeavsTaken, @CasualLeave, @BasicSalary, @LeaveDiduction, @Incentive, @OtherAmnt, @SalaryCut, @OtherDiduction, @NetSalary, @SlryAdvance, @PaidAmnt, @Balance, @Remarks )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertSlsRtnSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertSlsRtnSettings]
(
@SlsRtnId int=NULL, 
@ProductId int=NULL, 
@ProQty decimal(24,4)=NULL, 
@UnitId int=NULL, 
@ProRate decimal(24,4)=NULL, 
@ProDis decimal(24,4)=NULL, 
@DiscAmnt decimal(24,4)=NULL, 
@GrossAmnt decimal(24,4)=NULL, 
@isDamaged bit=NULL, 
@NetAmnt decimal(24,4)=NULL, 
@PartyAccountId int=NULL, 
@Date datetime=NULL, 
@HSN int=NULL, 
@GST decimal(24,4)=NULL, 
@GSTAMOUNT decimal(24,4)=NULL, 
@SGST decimal(24,4)=NULL, 
@SGSTAmount decimal(24,4)=NULL, 
@CGST decimal(24,4)=NULL, 
@CGSTAmount decimal(24,4)=NULL, 
@IGST decimal(24,4)=NULL, 
@IGSTAMOUNT decimal(24,4)=NULL, 
@FK_BatchId int=NULL, 
@Barcode nvarchar(100)=NULL

)
 AS 
 BEGIN 
 
 insert into SlsRtnSettings(SlsRtnId, ProductId, ProQty, UnitId, ProRate, ProDis, DiscAmnt, GrossAmnt, isDamaged, NetAmnt, PartyAccountId, Date, HSN, GST, GSTAMOUNT, SGST, SGSTAmount, CGST, CGSTAmount, IGST, IGSTAMOUNT, FK_BatchId, Barcode )
 values(@SlsRtnId, @ProductId, @ProQty, @UnitId, @ProRate, @ProDis, @DiscAmnt, @GrossAmnt, @isDamaged, @NetAmnt, @PartyAccountId, @Date, @HSN, @GST, @GSTAMOUNT, @SGST, @SGSTAmount, @CGST, @CGSTAmount, @IGST, @IGSTAMOUNT, @FK_BatchId, @Barcode )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertStockAdjusterMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertStockAdjusterMaster]
(
@StockAdjusterMasterCode int=NULL, @Date datetime=NULL, @Remarks nvarchar(400)=NULL, @ProtGroup int=NULL, @ProBrand int=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into StockAdjusterMaster(StockAdjusterMasterCode, Date, Remarks, ProtGroup, ProBrand, FK_BranchId )
 values(@StockAdjusterMasterCode, @Date, @Remarks, @ProtGroup, @ProBrand, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertStockAdjustSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertStockAdjustSettings]
(
@stockAdjusterMasterId int=NULL, @productId int=NULL, @currSystemStock decimal(18,2)=NULL, @curActualStock decimal(18,2)=NULL, @unitId int=NULL, @FK_BatchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into StockAdjustSettings(stockAdjusterMasterId, productId, currSystemStock, curActualStock, unitId, FK_BatchId )
 values(@stockAdjusterMasterId, @productId, @currSystemStock, @curActualStock, @unitId, @FK_BatchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertStockTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertStockTranLog]
(
@ProductId int=NULL, 
@VchrId int=NULL, 
@VchrNo int=NULL, 
@VchrDate datetime=NULL, 
@VchrType nvarchar(50)=NULL, 
@VchrFormType nvarchar(50)=NULL, 
@AccountId int=NULL, 
@InQuantity decimal(24,4)=NULL, 
@OutQuantity decimal(24,4)=NULL, 
@UnitId int=NULL, 
@Narration nvarchar(400)=NULL, 
@UserId int=NULL, 
@TranDate datetime=NULL, 
@FK_BatchId int=NULL, 
@FK_BranchId int=NULL, 
@Barcode nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into StockTranLog(ProductId, VchrId, VchrNo, VchrDate, VchrType, VchrFormType, AccountId, InQuantity, OutQuantity, UnitId, Narration, UserId, TranDate, FK_BatchId, FK_BranchId, Barcode )
 values(@ProductId, @VchrId, @VchrNo, @VchrDate, @VchrType, @VchrFormType, @AccountId, @InQuantity, @OutQuantity, @UnitId, @Narration, @UserId, @TranDate, @FK_BatchId, @FK_BranchId, @Barcode )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertStockTranSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertStockTranSettings]
(
@StockTransferId int=NULL, @ProductId int=NULL, @ProductName nvarchar(50)=NULL, @ProQuantity decimal(18,2)=NULL, @UnitId int=NULL, @CompanyName nvarchar(50)=NULL, @CompanyPlace nvarchar(50)=NULL, @ProRate decimal(18,2)=NULL, @Total decimal(18,2)=NULL, @FK_BatchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into StockTranSettings(StockTransferId, ProductId, ProductName, ProQuantity, UnitId, CompanyName, CompanyPlace, ProRate, Total, FK_BatchId )
 values(@StockTransferId, @ProductId, @ProductName, @ProQuantity, @UnitId, @CompanyName, @CompanyPlace, @ProRate, @Total, @FK_BatchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertStockTransferMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertStockTransferMaster]
(
@StockTransferCode int=NULL, @TransferDate datetime=NULL, @FromGodownId int=NULL, @FromGodownName nvarchar(400)=NULL, @ToGodownId int=NULL, @ToGodownName nvarchar(400)=NULL, @Remarks nvarchar(400)=NULL, @UserId int=NULL, @EntryDate datetime=NULL, @CompanyName nvarchar(50)=NULL, @CompanyPlace nvarchar(50)=NULL, @TotalAmnt decimal(18,2)=NULL, @TotalQty decimal(18,2)=NULL, @TranType nvarchar(50)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 insert into StockTransferMaster(StockTransferCode, TransferDate, FromGodownId, FromGodownName, ToGodownId, ToGodownName, Remarks, UserId, EntryDate, CompanyName, CompanyPlace, TotalAmnt, TotalQty, TranType, FK_BranchId )
 values(@StockTransferCode, @TransferDate, @FromGodownId, @FromGodownName, @ToGodownId, @ToGodownName, @Remarks, @UserId, @EntryDate, @CompanyName, @CompanyPlace, @TotalAmnt, @TotalQty, @TranType, @FK_BranchId )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertTaxCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertTaxCategory]
(
@TCategoryName nvarchar(20)=NULL, @SalesVat decimal(18,2)=NULL, @PurVat decimal(18,2)=NULL, @SaleCst decimal(18,2)=NULL, @PurCst decimal(18,2)=NULL, @SaleCess decimal(18,2)=NULL, @PurCess decimal(18,2)=NULL, @SalesXDuty decimal(18,2)=NULL, @PurXDuty decimal(18,2)=NULL 
)
 AS 
 BEGIN 
 
 insert into TaxCategory(TCategoryName, SalesVat, PurVat, SaleCst, PurCst, SaleCess, PurCess, SalesXDuty, PurXDuty )
 values(@TCategoryName, @SalesVat, @PurVat, @SaleCst, @PurCst, @SaleCess, @PurCess, @SalesXDuty, @PurXDuty )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertTransactionLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertTransactionLog]
(
@VchrId int=NULL, 
@VchrNo int=NULL, 
@VchrType nvarchar(50)=NULL, 
@VchrDate datetime=NULL, 
@AccountId int=NULL, 
@OppAccountId int=NULL, 
@DebitAmount decimal(24,4)=NULL, 
@CreditAmount decimal(24,4)=NULL, 
@Narration nvarchar(400)=NULL, 
@UserID int=NULL, 
@TranDate datetime=NULL, 
@Status nvarchar(10)=NULL, 
@RefNo nvarchar(20)=NULL, 
@VchrName nvarchar(50)=NULL, 
@FK_BranchId int=NULL, 
@SequenceNo int=NULL, 
@Terminal nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into TransactionLog(VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VchrId, @VchrNo, @VchrType, @VchrDate, @AccountId, @OppAccountId, @DebitAmount, @CreditAmount, @Narration, @UserID, @TranDate, @Status, @RefNo, @VchrName, @FK_BranchId, @SequenceNo, @Terminal )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertTranslator]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertTranslator]
(
@Name nvarchar(100)=NULL, @Value nvarchar(100)=NULL, @Form nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 insert into Translator(Name, Value, Form )
 values(@Name, @Value, @Form )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertUnit]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertUnit]
(
@UnitName nvarchar(10)=NULL, 
@UnitNo int=NULL

)
 AS 
 BEGIN 
 
 insert into Unit(UnitName, UnitNo )
 values(@UnitName, @UnitNo )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertUser]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertUser]
(
@PrevilageId int=NULL, @UserName nvarchar(50)=NULL, @PassWord nvarchar(50)=NULL 
)
 AS 
 BEGIN 
 
 insert into [User](PrevilageId, UserName, PassWord )
 values(@PrevilageId, @UserName, @PassWord )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertUserPrevilegeDtl]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertUserPrevilegeDtl]
(
@FK_UserPrevilegeHdrId int=NULL, 
@FK_MenuDetailsId int=NULL, 
@Visible bit=NULL, 
@Enable bit=NULL, 
@SaveData bit=NULL, 
@EditData bit=NULL, 
@DeleteData bit=NULL

)
 AS 
 BEGIN 
 
 insert into UserPrevilegeDtl(FK_UserPrevilegeHdrId, FK_MenuDetailsId, Visible, Enable, SaveData, EditData, DeleteData )
 values(@FK_UserPrevilegeHdrId, @FK_MenuDetailsId, @Visible, @Enable, @SaveData, @EditData, @DeleteData )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertUserPrevilegeHdr]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertUserPrevilegeHdr]
(
@Name nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 insert into UserPrevilegeHdr(Name )
 values(@Name )
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spInsertVchrTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spInsertVchrTranLog]
(
@VchrNo int=NULL, 
@VchrDate datetime=NULL, 
@VchrAmount decimal(24,4)=NULL, 
@VchrType nvarchar(50)=NULL, 
@FromAccountId int=NULL, 
@ToAccountId int=NULL, 
@Narration nvarchar(400)=NULL, 
@SalesManId int=NULL, 
@UserId int=NULL, 
@TranDate datetime=NULL, 
@VchrName nvarchar(50)=NULL, 
@FK_BranchId int=NULL, 
@SequenceNo int=NULL, 
@Terminal nvarchar(25)=NULL

)
 AS 
 BEGIN 
 
 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VchrNo, @VchrDate, @VchrAmount, @VchrType, @FromAccountId, @ToAccountId, @Narration, @SalesManId, @UserId, @TranDate, @VchrName, @FK_BranchId, @SequenceNo, @Terminal )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spInsertVehicle]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spInsertVehicle]
(
@VehName nvarchar(50)=NULL, 
@Manufacturer nvarchar(20)=NULL, 
@Model nvarchar(20)=NULL, 
@YearOfModel nvarchar(20)=NULL, 
@RegNo nvarchar(20)=NULL, 
@Ownership nvarchar(50)=NULL, 
@RegisterdOwner nvarchar(50)=NULL, 
@EngineNo nvarchar(50)=NULL, 
@ChasisNo nvarchar(50)=NULL, 
@InsurenceDate datetime=NULL, 
@TaxDate datetime=NULL, 
@PolusionDate datetime=NULL, 
@BColor nvarchar(20)=NULL, 
@LoanEMIDate datetime=NULL, 
@LoanEMIAmount nvarchar(200)=NULL, 
@Remarks nvarchar(400)=NULL, 
@FK_BranchId int=NULL

)
 AS 
 BEGIN 
 
 insert into Vehicle(VehName, Manufacturer, Model, YearOfModel, RegNo, Ownership, RegisterdOwner, EngineNo, ChasisNo, InsurenceDate, TaxDate, PolusionDate, BColor, LoanEMIDate, LoanEMIAmount, Remarks, FK_BranchId )
 values(@VehName, @Manufacturer, @Model, @YearOfModel, @RegNo, @Ownership, @RegisterdOwner, @EngineNo, @ChasisNo, @InsurenceDate, @TaxDate, @PolusionDate, @BColor, @LoanEMIDate, @LoanEMIAmount, @Remarks, @FK_BranchId )
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spMultiSizePatch]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 Create Procedure [dbo].[spMultiSizePatch]
 (
 @errMessage VARCHAR(MAX)='' OUTPUT,
@return int=null OUTPUT
)
 AS 
 BEGIN 
  BEGIN TRY
 BEGIN TRANSACTION MultSizePatch
  Update ppc
set ppc.Barcode = ptu.Barcode
From ProductPriceCategory ppc
Left Join ProductUnit ptu on (ppc.ProductId = ptu.ProductId and ppc.UnitId = ptu.UnitId)
where ppc.Barcode is null or ppc.Barcode = ''

  Update stl
set stl.Barcode = pt.Barcode
From StockTranLog stl
Left Join Product pt on stl.ProductId = pt.ProductId
where stl.Barcode is null or stl.Barcode = ''
  Update gstl
set gstl.Barcode = pt.Barcode
From GdwnStckTranLog gstl
Left Join Product pt on gstl.ProductId = pt.ProductId
where gstl.Barcode is null or gstl.Barcode = ''

  COMMIT TRANSACTION MultSizePatch
  END TRY
   BEGIN CATCH
    ROLLBACK TRANSACTION MultSizePatch;
   SELECT @errMessage = ERROR_MESSAGE()
   END CATCH 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spOpeningCash]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spOpeningCash]
(
@Date datetime,
@Terminal nvarchar(25),
@SequenceNo int,
@BranchId int =null 
)
As
Begin
select SUM(tl.DebitAmount)-sum(tl.CreditAmount) as OpeningCash
From TransactionLog tl
Left Join Account act on tl.AccountId = act.AccountId and tl.OppAccountId <> act.AccountId
left Join AccountGroup agp on act.AGroupId= agp.AGroupId
Where (tl.FK_BranchId = @BranchId or @BranchId is null) and
tl.VchrDate < @Date and 
tl.Terminal = @Terminal and tl.SequenceNo=@SequenceNo and
agp.GroupName ='CASH IN HAND'
End
GO
/****** Object:  StoredProcedure [dbo].[spPaymentOrReceiptCash]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spPaymentOrReceiptCash]
(
@FromDate datetime, 
@ToDate datetime,
@Terminal nvarchar(25),
@SequenceNo int,
@Type int,
@BranchId int =null 
)
As
Begin
if @Type = 0
select SUM(tl.DebitAmount) as CashReceived
From TransactionLog tl
Left Join Account act on tl.AccountId = act.AccountId and tl.OppAccountId <> act.AccountId
left Join AccountGroup agp on act.AGroupId= agp.AGroupId
Where (tl.FK_BranchId = @BranchId or @BranchId is null) and
tl.VchrDate between @FromDate and @ToDate and 
tl.Terminal = @Terminal and tl.SequenceNo=@SequenceNo and
agp.GroupName ='CASH IN HAND'
else
select SUM(tl.CreditAmount) as CashReceived
From TransactionLog tl
Left Join Account act on tl.AccountId = act.AccountId and tl.OppAccountId <> act.AccountId
left Join AccountGroup agp on act.AGroupId= agp.AGroupId
Where (tl.FK_BranchId = @BranchId or @BranchId is null) and
tl.VchrDate between @FromDate and @ToDate and 
tl.Terminal = @Terminal and tl.SequenceNo=@SequenceNo and
agp.GroupName ='CASH IN HAND'
End
GO
/****** Object:  StoredProcedure [dbo].[spPendingKOT]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spPendingKOT]
(
@FromDate datetime=null,
@ToDate datetime=null,
@Terminal nvarchar(25)=null,
@SequenceNo int=null,
@BranchId int =null
)
As
Begin
Select act.AccountName, kom.DateAndTime, kom.Terminal, kom.SerialNo,kom.TotalAmount,kom.TockenNo
From KOTOrderMaster kom
Left Join Account act on kom.FK_CustomerId=act.AccountId
where kom.Remarks='KOT'and
kom.DateAndTime >= @FromDate and kom.DateAndTime<= @ToDate and
(kom.Terminal=@Terminal or  @Terminal is null)  and 
(kom.SerialNo=@SequenceNo or @SequenceNo is null)and
(kom.FK_BranchId = @BranchId or @BranchId is null)
End
GO
/****** Object:  StoredProcedure [dbo].[spProCategoryForCloseCash]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProCategoryForCloseCash]
(
@FromDate datetime, 
@ToDate datetime,
@SequenceNo int, 
@Terminal nvarchar(25),
@BranchId int = null
)
As
Begin
Select (case when pg.PGroupName=pg.PGroupName then PGroupName end) as CategoryName,
sum(case when pg.PGroupName=pg.PGroupName then ses.ProQty end) as Qty,
sum((case when pg.PGroupName=pg.PGroupName then ses.GrossAmnt end)-(case when pg.PGroupName=pg.PGroupName then ses.DiscAmnt end)) as Taxable,
sum(case when pg.PGroupName=pg.PGroupName then ses.GSTAMOUNT end) as Vat,
sum(case when pg.PGroupName=pg.PGroupName then ses.NetAmnt end) as Net
From SalesEstimateSettings ses
inner  Join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
left join Product pt on ses.ProductId = pt.ProductId
left join ProductGroup pg on pt.PGroupId = pg.PGroupId
where (sem.FK_BranchId = @BranchId or @BranchId is null) and
sem.SequenceNo=@SequenceNo and sem.Terminal=@Terminal and sem.SalesDate between @FromDate and @ToDate
Group by pg.PGroupName
End
GO
/****** Object:  StoredProcedure [dbo].[spProductBarcodePatch]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 Create Procedure [dbo].[spProductBarcodePatch]
 (
 @errMessage VARCHAR(MAX)='' OUTPUT,
@return int=null OUTPUT
)
 AS 
 BEGIN 
  BEGIN TRY
 BEGIN TRANSACTION ProductBarcodePatch
  Update pb
set pb.SellingPrice = ppc.SPrice, pb.PurchasePrice = ppc.PPrice
From ProductBarcode pb
Left Join ProductPriceCategory ppc on pb.Barcode = ppc.Barcode

  COMMIT TRANSACTION ProductBarcodePatch
  END TRY
   BEGIN CATCH
    ROLLBACK TRANSACTION ProductBarcodePatch;
   SELECT @errMessage = ERROR_MESSAGE()
   END CATCH 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spProductDifferencePOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProductDifferencePOS]
(
@ProductId int = null,
@BranchId int =null
)
As
Begin
Select pt.Barcode,pt.ProductCode,pt.ProductName, sum(isnull(stl.InQuantity,0)) as [In],sum(isnull(stl.OutQuantity,0)) as [Out],sum(isnull(stl.InQuantity,0)) - Sum(isnull(stl.OutQuantity,0)) as [Difference]
From StockTranLog stl
Left Join Product pt on stl.ProductId = pt.ProductId
 where (stl.FK_BranchId = @BranchId or @BranchId is null) and
 (stl.ProductId = @ProductId or @ProductId is null)
 group by pt.Barcode,pt.ProductCode,pt.ProductName
 order by pt.ProductName

End
GO
/****** Object:  StoredProcedure [dbo].[spProductFlowReport]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProductFlowReport]
(
@FromDate datetime,
@ToDate datetime,
@Barcode nvarchar(25)=null,
@ProductId int = null,
@BranchId int =null
)
As
Begin
create table #OppStock
 (
 ProductId int,
 Barcode nvarchar(25),
  Oppening decimal(18,4)
  )
  create table #PurStock
 (
 ProductId int,
 Barcode nvarchar(25),
  Purchase decimal(18,4)
  )
  create table #PReturnStock
 (
 ProductId int,
 Barcode nvarchar(25),
  PurchaseReturn decimal(18,4)
  )
  create table #SalesStock
 (
 ProductId int,
 Barcode nvarchar(25),
  Sales decimal(18,4)
  )
  create table #SReturnStock
 (
 ProductId int,
 Barcode nvarchar(25),
  SalesReturn decimal(18,4)
  )
  create table #ClosingStock
 (
 ProductId int,
 Barcode nvarchar(25),
  Closing decimal(18,4)
  )

  insert into #OppStock Select stl.ProductId,stl.Barcode, isnull(sum(stl.InQuantity-stl.OutQuantity),0)  as Oppening
 From StockTranLog stl
 where stl.VchrDate < @FromDate and 
 (stl.Barcode = @Barcode or @Barcode is null) and
 (stl.ProductId = @ProductId or @ProductId is null) and 
 (stl.FK_BranchId = @BranchId or @BranchId is null)
Group by stl.Barcode, stl.ProductId

  insert into #OppStock Select stl.ProductId,stl.Barcode, isnull(sum(stl.InQuantity-stl.OutQuantity),0)  as Oppening
 From StockTranLog stl
 where stl.VchrDate between @FromDate and @ToDate and
 stl.VchrType = 'Opening Entry' and
 (stl.Barcode = @Barcode or @Barcode is null) and
 (stl.ProductId = @ProductId or @ProductId is null) and 
 (stl.FK_BranchId = @BranchId or @BranchId is null)
Group by stl.Barcode, stl.ProductId

  insert into #PurStock Select stl.ProductId,stl.Barcode, isnull(sum(stl.InQuantity-stl.OutQuantity),0)  as Purchase
 From StockTranLog stl
 where stl.VchrType = 'Purchase Voucher' and
 stl.VchrDate between @FromDate and @ToDate and
 (stl.Barcode = @Barcode or @Barcode is null) and
 (stl.ProductId = @ProductId or @ProductId is null) and 
 (stl.FK_BranchId = @BranchId or @BranchId is null)
Group by stl.Barcode, stl.ProductId

   insert into #PReturnStock Select stl.ProductId,stl.Barcode, isnull(sum(stl.InQuantity-stl.OutQuantity),0)  as PurchaseReturn
 From StockTranLog stl
 where stl.VchrType = 'Purchase Return Voucher' and
 stl.VchrDate between @FromDate and @ToDate and
 (stl.Barcode = @Barcode or @Barcode is null) and
 (stl.ProductId = @ProductId or @ProductId is null) and 
 (stl.FK_BranchId = @BranchId or @BranchId is null)
Group by stl.Barcode, stl.ProductId

 insert into #SalesStock Select stl.ProductId,stl.Barcode, isnull(sum(stl.InQuantity-stl.OutQuantity),0)  as Sales
 From StockTranLog stl
 where stl.VchrType = 'Sales Voucher' and
 stl.VchrDate between @FromDate and @ToDate and
 (stl.Barcode = @Barcode or @Barcode is null) and
 (stl.ProductId = @ProductId or @ProductId is null) and 
 (stl.FK_BranchId = @BranchId or @BranchId is null)
Group by stl.Barcode, stl.ProductId

 insert into #SReturnStock Select stl.ProductId,stl.Barcode, isnull(sum(stl.InQuantity-stl.OutQuantity),0)  as SalesReturn
 From StockTranLog stl
 where stl.VchrType = 'Sales Return Voucher' and
 stl.VchrDate between @FromDate and @ToDate and
 (stl.Barcode = @Barcode or @Barcode is null) and
 (stl.ProductId = @ProductId or @ProductId is null) and 
 (stl.FK_BranchId = @BranchId or @BranchId is null)
 Group by stl.Barcode, stl.ProductId

  insert into #ClosingStock Select stl.ProductId,stl.Barcode, isnull(sum(stl.InQuantity-stl.OutQuantity),0)  as Closing
 From StockTranLog stl
 where  stl.VchrDate < @ToDate and
 (stl.Barcode = @Barcode or @Barcode is null) and
 (stl.ProductId = @ProductId or @ProductId is null) and 
 (stl.FK_BranchId = @BranchId or @BranchId is null)
 Group by stl.Barcode,stl.ProductId

 Select stl.Barcode,pb.ProductBarcodeName,pg.PGroupName,bd.BrandName,Convert(Double PRECISION, isnull(os.Oppening ,0)) as Openingstock,Convert(Double PRECISION,isnull(ps.Purchase,0)) as PurTotal,Convert(Double PRECISION,abs(isnull(prs.PurchaseReturn,0))) as PurRtnTotal,
 Convert(Double PRECISION,abs(isnull(ss.Sales,0))) as SalesTotal,Convert(Double PRECISION,isnull(srs.SalesReturn,0)) as SalesRtnTotal, Convert(Double PRECISION,isnull(cs.Closing,0)) as Clostock,ut.UnitName, Convert(Double PRECISION,pb.SellingPrice) as Rate, Convert(Double PRECISION,isnull(cs.Closing,0) * pb.SellingPrice) as [Stock Value]
 From StockTranLog stl
 Left Join ProductBarcode pb on stl.Barcode = pb.Barcode
 Left Join #OppStock os on stl.Barcode = os.Barcode
 Left join #PurStock ps on stl.Barcode = ps.Barcode
 Left join #PReturnStock prs on stl.Barcode = prs.Barcode
 Left join #SalesStock ss on stl.Barcode = ss.Barcode
 Left join #SReturnStock srs on stl.Barcode = srs.Barcode
 Left join #ClosingStock cs on stl.Barcode = cs.Barcode
 Left Join Unit ut on stl.UnitId = ut.UnitId
 Left Join Product pct on stl.ProductId = pct.ProductId
 Left Join ProductGroup pg on pct.PGroupId = pg.PGroupId
 Left Join Brand bd on pct.BrandId = bd.BrandId
 where (pb.Barcode = @Barcode or @Barcode is null) and
 (pb.FK_ProductId = @ProductId or @ProductId is null) and 
 (pb.FK_BranchId = @BranchId or @BranchId is null)
 Group by stl.Barcode,pb.ProductBarcodeName,os.Oppening,ps.Purchase,prs.PurchaseReturn,ss.Sales,srs.SalesReturn,cs.Closing,ut.UnitName,pb.SellingPrice,pg.PGroupName,bd.BrandName
 drop table #OppStock
 drop table #PurStock
 drop table #PReturnStock
 drop table #SalesStock
 drop table #SReturnStock
 drop table #ClosingStock
End
GO
/****** Object:  StoredProcedure [dbo].[spProductGroupSales]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProductGroupSales]
(
@FromDate datetime, 
@ToDate datetime,
@ProGroupId int = null,
@UserId int = null
)
As
Begin
Select pg.PGroupName as CategoryName,
sum(ses.ProQty) as Qty,
sum(ses.GrossAmnt) as Gross,
sum(ses.GrossAmnt-ses.DiscAmnt) as Taxable,
sum(ses.GSTAMOUNT) as Vat,
sum(ses.NetAmnt) as Net
From SalesEstimateSettings ses
left join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
left join Product pt on ses.ProductId = pt.ProductId
left join ProductGroup pg on pt.PGroupId = pg.PGroupId
left join [User] ur on sem.JEId = ur.UserId
where SalesDate between @FromDate and @ToDate and 
(pg.PGroupId = @ProGroupId or @ProGroupId is null) and
(ur.UserId = @UserId or @UserId is null)
Group by pg.PGroupName


End
GO
/****** Object:  StoredProcedure [dbo].[spProductLoadForPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProductLoadForPOS]
(
@ProductId int = null,
@PGroupId int=null,
@IsFavorite bit = null
)
As
Begin
declare @PriceCategoryId int = null
select @PriceCategoryId = min(PCategoryId) from PriceCategory 
select pct.ProductId,ProductName,image,pct.Barcode,ppc.SPrice as Rate
From Product pct
left join ProductPriceCategory ppc on pct.Barcode = ppc.Barcode
Where (pct.ProductId = @ProductId or @ProductId is null) and
(pct.PGroupId = @PGroupId or @PGroupId is null) and
(pct.IsFavorite = @IsFavorite or @IsFavorite is null) and
ppc.PCategoryId = @PriceCategoryId
order by ListOrder desc
End
GO
/****** Object:  StoredProcedure [dbo].[spProductOpeningStockReport]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProductOpeningStockReport]
(
@ProGroupId int = null
)
As
Begin
Select pt.Barcode,pt.ProductName,pt.OpeningStock, ut.UnitName,pt.PurchasePrice, pt.OpeningStock * pt.PurchasePrice as Total
From Product pt
inner Join Unit ut on pt.UnitId = ut.UnitId
Where (pt.PGroupId= @ProGroupId or @ProGroupId is null)

End
GO
/****** Object:  StoredProcedure [dbo].[spProductProfitSalePOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProductProfitSalePOS]
(
@FromDate datetime, @ToDate datetime
)
As
Begin
Select pt.Barcode, pt.ProductName, ses.ProQty,pt.PurchasePrice, ses.ProRate, ((isnull(ses.ProQty,0)* isnull(ses.ProRate,0))-(ISNULL( ses.ProQty,0)* ISNULL( pt.PurchasePrice,0))) as Profit
From SalesEstimateSettings ses
Left join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
Left join Product pt on ses.ProductId = pt.ProductId
Where sem.SalesDate between @FromDate and @ToDate
group by pt.Barcode, pt.ProductName, ses.ProQty, pt.PurchasePrice,ses.ProRate
End
GO
/****** Object:  StoredProcedure [dbo].[spProductPurchasePrice]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProductPurchasePrice]
(
@ProductId int
)
As
Begin
 declare @HighestPurPrice decimal(24,4)
 select @HighestPurPrice = MAX(PurInvSettings.ProRate) From PurInvSettings Where ProId = @ProductId
declare @AveragePurPrice decimal(24,4)
 select @AveragePurPrice = AVG(PurInvSettings.ProRate) From PurInvSettings Where ProId = @ProductId
 declare @LastPurPrice decimal(24,4)
 select Top 1  @LastPurPrice = PurInvSettings.ProRate From PurInvSettings Where ProId = @ProductId order by [Date] desc

 select isnull(@HighestPurPrice,0) as Highest,isnull(@AveragePurPrice,0) as Average,isnull(@LastPurPrice,0) as [Last]
End
GO
/****** Object:  StoredProcedure [dbo].[spProductSaleCloseCash]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProductSaleCloseCash]
(
@SequenceNo int = null,
@BranchId int =null
)
As
Begin
Select pt.Barcode as Barcode, pt.ProductName as ProductName,pg.PGroupName as GroupName,sum(iif(ses.ProQty > 0 ,ses.ProQty,0)) as Quantity,
sum(iif(ses.GrossAmnt > 0, ses.GrossAmnt,0)) as Gross,sum(ses.DiscAmnt) as Discount,abs(sum(iif(ses.ProQty<0 ,ses.ProQty,0))) as RefundQty,
abs(sum(iif(ses.GrossAmnt<0 ,ses.GrossAmnt,0))) as RefundTotal,((sum(iif(ses.GrossAmnt > 0, ses.GrossAmnt,0)))+ abs(sum(iif(ses.GrossAmnt < 0, ses.GrossAmnt,0))))/((sum(iif(ses.ProQty > 0, ses.ProQty,0)))+abs(sum(iif(ses.ProQty < 0, ses.ProQty,0)))) as SalesRate,
sum(ses.NetAmnt-ses.GSTAMOUNT) as Taxable,sum(ses.GSTAMOUNT) as Tax, sum(ses.NetAmnt) as NetAmount
From SalesEstimateSettings ses
left join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
inner join Product pt on ses.ProductId=pt.ProductId
left join ProductGroup pg on pt.PGroupId = pg.PGroupId
where(sem.FK_BranchId = @BranchId or @BranchId is null) and
(sem.SequenceNo=@SequenceNo or @SequenceNo is null)
Group by pt.Barcode,pt.ProductName,pg.PGroupName
order by pt.ProductName
End
GO
/****** Object:  StoredProcedure [dbo].[spProductStockReportPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProductStockReportPOS]
(
@ProductGroupId int =null,
@BranchId int =null
)
As
Begin

Select pg.PGroupName,pt.ProductName,pt.Barcode,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock,
pt.PurchasePrice as BuyingPrice,pt.SellingPrice as SalePrice,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) * pt.PurchasePrice as
TotalBuyingValue, SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) * pt.SellingPrice as TotalSaleValue
From Product pt
left join StockTranLog stl on stl.ProductId = pt.ProductId
left join ProductGroup pg on pt.PGroupId = pg.PGroupId
where (stl.FK_BranchId = @BranchId or @BranchId is null) and
(pg.PGroupId = @ProductGroupId or @ProductGroupId is null)
group by pt.ProductName, pt.Barcode,pt.PurchasePrice,pt.SellingPrice,pg.PGroupName
order by pg.PGroupName
End
GO
/****** Object:  StoredProcedure [dbo].[spProductUnitWiseStock]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProductUnitWiseStock]
(
@BranchId int= null,
@ProductId int= null,
@UnitId int= null,
@Barcode nvarchar(25)=null,
@GodwnId int = null
)
AS 
 BEGIN 
 declare @UnitCount decimal(18,4)
 select @UnitCount = UnitQty From ProductUnit Where ProductId = @ProductId and UnitId = @UnitId
  Select isnull(nullif(SUM(InQuantity-OutQuantity),0)/@UnitCount , 0) as Stock 
 From GdwnStckTranLog Where
 (GodwnId=@GodwnId or @GodwnId is null) and
 (Barcode= @Barcode or @Barcode is null)and
 (ProductId= @ProductId or @ProductId is null)and 
 VchrDate <= GETDATE() and FK_BranchId = @BranchId
 End
 
GO
/****** Object:  StoredProcedure [dbo].[spProfitAndLossAccountBalance]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProfitAndLossAccountBalance]
(
@Type int=0,
@AccountType nvarchar (10) = 'C',
@GroupName nvarchar(50)=null,
@BranchId int=null,
@FromDate datetime='2021-04-01',
@ToDate datetime='2021-07-18',
@Under nvarchar(50)=null
)
As
Begin
if @Type = 0
if @AccountType = 'C'
Select SUM(tlg.CreditAmount-tlg.DebitAmount) as LedgerAmount 
from TransactionLog tlg
Join Account acc on tlg.AccountId=acc.AccountId 
Join AccountGroup agp on acc.AGroupId=agp.AGroupId  
where (agp.GroupName=@GroupName or @GroupName is null) and
(agp.Under=@Under or @Under is null) and
(tlg.FK_BranchId=@BranchId or @BranchId is null) and
tlg.VchrDate between @FromDate and @ToDate
else
Select SUM(tlg.DebitAmount-tlg.CreditAmount) as LedgerAmount 
from TransactionLog tlg
Join Account acc on tlg.AccountId=acc.AccountId 
Join AccountGroup agp on acc.AGroupId=agp.AGroupId  
where (agp.GroupName=@GroupName or @GroupName is null) and
(agp.Under=@Under or @Under is null) and
(tlg.FK_BranchId=@BranchId or @BranchId is null) and
tlg.VchrDate between @FromDate and @ToDate

else
if @AccountType = 'C'
Select SUM(tlg.CreditAmount-tlg.DebitAmount) as LedgerAmount 
from TransactionLog tlg
Join Account acc on tlg.AccountId=acc.AccountId 
Join AccountGroup agp on acc.AGroupId=agp.AGroupId  
where (agp.GroupName=@GroupName or @GroupName is null) and
(agp.Under=@Under or @Under is null) and
(tlg.FK_BranchId=@BranchId or @BranchId is null) and
tlg.VchrDate <= @ToDate
else
Select SUM(tlg.DebitAmount-tlg.CreditAmount) as LedgerAmount 
from TransactionLog tlg
Join Account acc on tlg.AccountId=acc.AccountId 
Join AccountGroup agp on acc.AGroupId=agp.AGroupId  
where (agp.GroupName=@GroupName or @GroupName is null) and
(agp.Under=@Under or @Under is null) and
(tlg.FK_BranchId=@BranchId or @BranchId is null) and
tlg.VchrDate <= @ToDate
END
GO
/****** Object:  StoredProcedure [dbo].[spProfitAndLossClosingStock]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProfitAndLossClosingStock]
(
@Validation nvarchar(50)='',
@BranchId int=null,
@Date datetime='2021-07-14'
)
As
Begin
create table #productrates
 (
  Rates decimal,
  ProductId int,
  Stock decimal
  )
  if @Validation='Selling Price'
  insert into #productrates select pt.SellingPrice as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date
  group by pt.ProductId,pt.SellingPrice

  else if @Validation='Average Selling Price'
   insert into #productrates select isnull(AVG(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date and ses.Date <=@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Highest Selling Price'
   insert into #productrates select isnull(MAX(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date and ses.Date <=@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Last Selling Price'
  Begin
  create table #productlastprice
 (
  Rates decimal,
  ProductId int,
  )
    insert into #productlastprice select Top 1 ses.ProRate as Rates, pt.ProductId
  From Product pt
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
  left join SalesEstimateMaster sem on ses.SalesInvId=sem.SalesInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  sem.SalesDate <=@Date
  group by pt.ProductId,ses.ProRate,ses.Date
  order by ses.Date desc

   insert into #productrates select isnull(ses.Rates,pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join #productlastprice ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date
   group by pt.ProductId,ses.Rates,pt.SellingPrice
  end
  else if @Validation='Purchase Price'
  insert into #productrates select pt.PurchasePrice as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date
  group by pt.ProductId,pt.PurchasePrice

  else if @Validation='MRP'
  insert into #productrates select pt.MRP as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date 
  group by pt.ProductId,pt.MRP

  else if @Validation='Average Purchase Price'
   insert into #productrates select isnull(AVG(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join PurInvSettings ses on pt.ProductId = ses.ProId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date and ses.Date<=@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Highest Purchase Price'
   insert into #productrates select isnull(MAX(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join PurInvSettings ses on pt.ProductId = ses.ProId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
 stl.VchrDate <=@Date and ses.Date<=@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Last Purchase Price'
  Begin
  create table #productlastpricepurchase
 (
  Rates decimal,
  ProductId int,
  )
    insert into #productlastpricepurchase select Top 1 ses.ProRate as Rates, pt.ProductId
  From Product pt
  left join PurInvSettings ses on pt.ProductId = ses.ProId
  left join PurInvMaster sem on ses.PurInvId=sem.PurInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  sem.PurDate <=@Date
  group by pt.ProductId,ses.ProRate,ses.Date
  order by ses.Date desc

   insert into #productrates select isnull(ses.Rates,pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join #productlastpricepurchase ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date
   group by pt.ProductId,ses.Rates,pt.SellingPrice
  end
  select SUM(#productrates.Rates * #productrates.Stock) as OpeningStock
  from #productrates
END
GO
/****** Object:  StoredProcedure [dbo].[spProfitAndLossOppeningStock]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProfitAndLossOppeningStock]
(
@Validation nvarchar(50)='',
@BranchId int=null,
@Date datetime='2021-07-14'
)
As
Begin
create table #productrates
 (
  Rates decimal,
  ProductId int,
  Stock decimal
  )
  if @Validation='Selling Price'
  insert into #productrates select pt.SellingPrice as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <@Date
  group by pt.ProductId,pt.SellingPrice

  else if @Validation='Average Selling Price'
   insert into #productrates select isnull(AVG(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <@Date and ses.Date <@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Highest Selling Price'
   insert into #productrates select isnull(MAX(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <@Date and ses.Date <@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Last Selling Price'
  Begin
  create table #productlastprice
 (
  Rates decimal,
  ProductId int,
  )
    insert into #productlastprice select Top 1 ses.ProRate as Rates, pt.ProductId
  From Product pt
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
  left join SalesEstimateMaster sem on ses.SalesInvId=sem.SalesInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  sem.SalesDate <@Date
  group by pt.ProductId,ses.ProRate,ses.Date
  order by ses.Date desc

   insert into #productrates select isnull(ses.Rates,pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join #productlastprice ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <@Date
   group by pt.ProductId,ses.Rates,pt.SellingPrice
  end
  else if @Validation='Purchase Price'
  insert into #productrates select pt.PurchasePrice as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <@Date
  group by pt.ProductId,pt.PurchasePrice

  else if @Validation='MRP'
  insert into #productrates select pt.MRP as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <@Date 
  group by pt.ProductId,pt.MRP

  else if @Validation='Average Purchase Price'
   insert into #productrates select isnull(AVG(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join PurInvSettings ses on pt.ProductId = ses.ProId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <@Date and ses.Date<@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Highest Purchase Price'
   insert into #productrates select isnull(MAX(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join PurInvSettings ses on pt.ProductId = ses.ProId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
 stl.VchrDate <@Date and ses.Date<@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Last Purchase Price'
  Begin
  create table #productlastpricepurchase
 (
  Rates decimal,
  ProductId int,
  )
    insert into #productlastpricepurchase select Top 1 ses.ProRate as Rates, pt.ProductId
  From Product pt
  left join PurInvSettings ses on pt.ProductId = ses.ProId
  left join PurInvMaster sem on ses.PurInvId=sem.PurInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  sem.PurDate <@Date
  group by pt.ProductId,ses.ProRate,ses.Date
  order by ses.Date desc

   insert into #productrates select isnull(ses.Rates,pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join #productlastpricepurchase ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <@Date
   group by pt.ProductId,ses.Rates,pt.SellingPrice
  end
  select SUM(#productrates.Rates * #productrates.Stock) as OpeningStock
  from #productrates
END
GO
/****** Object:  StoredProcedure [dbo].[spProwisePurchasePO]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spProwisePurchasePO]
(
@FromDate datetime, 
@ToDate datetime,
@SupplierId int = null,
@BranchId int =null
)
As
Begin
Select pim.PurDate, pim.PurInvNo,pim.RefInvNo,act.AccountName as Supplier,pt.ProductName,pt.ProductCode,pis.Barcode,pis.ProQty,
ut.UnitName as Unit,pis.ProRate,pis.Gross,pis.DiscAmnt,pis.NetAmnt-pis.GSTAMOUNT as Taxable, pis.GSTAMOUNT,pis.NetAmnt
From PurInvSettings pis
Inner Join PurInvMaster pim on pis.PurInvId = pim.PurInvId
Left join Account act on pis.PartyAccId = act.AccountId
Left join Product pt on pis.ProId = pt.ProductId
Left join Unit ut on pis.UnitId = ut.UnitId
Where (pim.PartyAccId=@SupplierId or @SupplierId is null) and
(pim.FK_BranchId = @BranchId or @BranchId is null)and 
pim.PurDate between @FromDate and  @ToDate
order by pim.PurDate,pt.ProductName
End
GO
/****** Object:  StoredProcedure [dbo].[spSaleByProductPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spSaleByProductPOS]
(
@FromDate datetime, @ToDate datetime,@ProductId int=null,@ProductGroupId int = null
)
As
Begin
Select pt.Barcode as Barcode, pt.ProductName as ProductName,pg.PGroupName as GroupName,sum(iif(ses.ProQty > 0 ,ses.ProQty,0)) as Quantity,
sum(iif(ses.GrossAmnt > 0, ses.GrossAmnt,0)) as Gross,sum(ses.DiscAmnt) as Discount,abs(sum(iif(ses.ProQty<0 ,ses.ProQty,0))) as RefundQty,
abs(sum(iif(ses.GrossAmnt<0 ,ses.GrossAmnt,0))) as RefundTotal,((sum(iif(ses.GrossAmnt > 0, ses.GrossAmnt,0)))+ abs(sum(iif(ses.GrossAmnt < 0, ses.GrossAmnt,0))))/((sum(iif(ses.ProQty > 0, ses.ProQty,0)))+abs(sum(iif(ses.ProQty < 0, ses.ProQty,0)))) as SalesRate,
sum(ses.NetAmnt-ses.GSTAMOUNT) as Taxable,sum(ses.GSTAMOUNT) as Tax, sum(ses.NetAmnt) as NetAmount
From SalesEstimateSettings ses
left join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
inner join Product pt on ses.ProductId=pt.ProductId
left join ProductGroup pg on pt.PGroupId = pg.PGroupId
where sem.SalesDate between @FromDate and @ToDate and 
(pt.PGroupId=@ProductGroupId or @ProductGroupId is null)and 
(pt.ProductId = @ProductId or @ProductId is null)
Group by pt.Barcode,pt.ProductName,pg.PGroupName
order by pt.ProductName
End
GO
/****** Object:  StoredProcedure [dbo].[spSalesInvoice]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spSalesInvoice]
(
@SalesInvId int = null,
@SalesInvNo int=NULL, 
@InvType nvarchar(50)=NULL, 
@SalesDate datetime=NULL, 
@SalesAccountId int=NULL, 
@PartyAccountId int=NULL, 
@RouteId int=NULL, 
@VehId int=NULL, 
@StafId int=NULL, 
@PriceCategoryId int=NULL, 
@AditionalAmount decimal(18,4)=NULL, 
@Frieght decimal(18,4)=NULL, 
@TotalDiscount decimal(18,4)=NULL, 
@BillDiscount decimal(18,4)=NULL, 
@GrandDiscount decimal(18,4)=NULL, 
@JEId int=NULL, 
@EntryDate datetime=NULL, 
@Remarks nvarchar(50)=NULL, 
@IGSTAmount decimal(18,4)=NULL, 
@SGSTAmount decimal(18,4)=NULL, 
@CGSTAmount decimal(18,4)=NULL, 
@TaxableAmount decimal(18,4)=NULL, 
@FYearID int=NULL, 
@FloodCess decimal(18,4)=NULL, 
@TotalTaxAmount decimal(18,4)=NULL, 
@FK_BranchId int=NULL, 
@CntrInvNo nvarchar(10)=NULL, 
@SequenceNo int=NULL, 
@CashPayment decimal(24,8)=NULL, 
@CardPayment decimal(24,8)=NULL, 
@Credit decimal(24,8)=NULL, 
@FielOne nvarchar(100)=NULL, 
@FieldTwo nvarchar(100)=NULL, 
@FieldThree nvarchar(100)=NULL, 
@FieldFour nvarchar(100)=NULL, 
@Terminal nvarchar(25)=NULL, 
@IsClosed bit=NULL, 
@FreeAmount decimal(24,4)=NULL, 
@ReturnAmount decimal(24,4)=NULL,
@OtherPayment decimal(24,4)=NULL,
@CashAccountId int=NULL, 
@CardAccountId int=NULL, 
@OnlineAccountId int=NULL, 
@IsSettiled bit=NULL, 
@TotalPayment decimal(24,8)=NULL, 
@VoucherNumber nvarchar(25)=NULL,
@IsReturn int=null,
@TransactionType nvarchar(20)='insert',
@GodownId int = null,
@GodownName nvarchar(50),
@errMessage VARCHAR(MAX)='' OUTPUT,
@return int=null OUTPUT,
@type dbo.UT_SalesSettings READONLY
)
 AS 
 BEGIN 
  BEGIN TRY
 BEGIN TRANSACTION inser_SalesInvoice

 declare @taxPayableId int 
 declare @CashVoucherNo int
 declare @VoucherInvIdCashReceipt int
 declare @VoucherInvIdCashPayment int
 declare @CardVoucherNo int
 declare @VoucherInvIdCardReceipt int
 declare @VoucherInvIdCardPayment int
 declare @OnlineVoucherNo int
declare @VoucherInvIdOnlineReceipt int
declare @VoucherInvIdOnlinePayment int

if @TransactionType ='insert'
Begin
 --Sales Invoice number finding
 select @SalesInvNo = isnull(MAX(SalesInvNo),0)+1 from SalesEstimateMaster Where InvType =@InvType and FK_BranchId = @FK_BranchId
 set @CntrInvNo= CONCAT(@CntrInvNo , @SalesInvNo)

 --Inserting Master Table
 insert into SalesEstimateMaster(SalesInvNo, InvType, SalesDate, SalesAccountId, PartyAccountId, RouteId, VehId, StafId, PriceCategoryId, AditionalAmount, Frieght, TotalDiscount, BillDiscount, GrandDiscount, JEId, EntryDate, Remarks, IGSTAmount, SGSTAmount, CGSTAmount, TaxableAmount, FYearID, FloodCess, TotalTaxAmount, FK_BranchId, CntrInvNo, SequenceNo, CashPayment, CardPayment, Credit, FielOne, FieldTwo, FieldThree, FieldFour, Terminal, IsClosed, FreeAmount, ReturnAmount, OtherPayment, CashAccountId, CardAccountId, OnlineAccountId, IsSettiled, TotalPayment, VoucherNumber )
 values(@SalesInvNo, @InvType, @SalesDate, @SalesAccountId, @PartyAccountId, @RouteId, @VehId, @StafId, @PriceCategoryId, @AditionalAmount, @Frieght, @TotalDiscount, @BillDiscount, @GrandDiscount, @JEId, @EntryDate, @Remarks, @IGSTAmount, @SGSTAmount, @CGSTAmount, @TaxableAmount, @FYearID, @FloodCess, @TotalTaxAmount, @FK_BranchId, @CntrInvNo, @SequenceNo, @CashPayment, @CardPayment, @Credit, @FielOne, @FieldTwo, @FieldThree, @FieldFour, @Terminal, @IsClosed, @FreeAmount, @ReturnAmount, @OtherPayment, @CashAccountId, @CardAccountId, @OnlineAccountId, @IsSettiled, @TotalPayment, @VoucherNumber )
 --Sales Invoice Id finding
 set @SalesInvId = SCOPE_IDENTITY()
 --Inserting Details Table
  insert into SalesEstimateSettings(SalesInvId, PartyAccountId, ProductId, ProQty, UnitId, ProRate, ProDiscont, DiscAmnt, GrossAmnt, NetAmnt, Date, HSN, GST, GSTAMOUNT, SGST, SGSTAmount, CGST, CGSTAmount, IGST, IGSTAMOUNT, FK_BatchId, Barcode,ItemCalory, EmployeeID, EmployeeCommision, AgentId, AgentCommission, ProductName )
 select @SalesInvId, PartyAccountId, ProductId, ProQty, UnitId, ProRate, ProDiscont, DiscAmnt, GrossAmnt, NetAmnt, Date, HSN, GST, GSTAMOUNT, SGST, SGSTAmount, CGST, CGSTAmount, IGST, IGSTAMOUNT, FK_BatchId, Barcode,ItemCalory, EmployeeID, EmployeeCommision, AgentId, AgentCommission, ProductName from @type
 --Inserting TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @SalesAccountId, @PartyAccountId, 0, @GrandDiscount, 'Sales Voucher', @JEId, @EntryDate, 'Main', @SalesInvNo, @InvType, @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @PartyAccountId, @SalesAccountId,@GrandDiscount, 0,  'Sales Voucher', @JEId, @EntryDate, 'Sub', @SalesInvNo, @InvType, @FK_BranchId, @SequenceNo, @Terminal )

 if @TotalTaxAmount <> 0
 Begin
 --Tax Payable Posting
 -- Tax Payable AccountId Finding
 select @taxPayableId= AccountId From Account Where AccountName ='TAX PAYABLE'
 --Tax Payable Account Posting
 --Inserting Tax Payable TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @SalesAccountId, @taxPayableId, @TotalTaxAmount, 0, 'Tax Entry', @JEId, @EntryDate, 'Main', @SalesInvNo, 'TAX PAYABLE', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Tax Payable TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @taxPayableId, @SalesAccountId, 0,@TotalTaxAmount,  'Tax Entry', @JEId, @EntryDate, 'Sub', @SalesInvNo, 'TAX PAYABLE', @FK_BranchId, @SequenceNo, @Terminal )
 End
 if @FloodCess <> 0
 Begin
 --Flood Tax Payable Account Posting
 --Inserting Flood  Tax Payable TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @SalesAccountId, @taxPayableId, @FloodCess, 0, 'Cess Entry', @JEId, @EntryDate, 'Main', @SalesInvNo, 'TAX PAYABLE', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Flood Tax Payable TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @taxPayableId, @SalesAccountId, 0,@FloodCess,  'Cess Entry', @JEId, @EntryDate, 'Sub', @SalesInvNo, 'TAX PAYABLE', @FK_BranchId, @SequenceNo, @Terminal )
 End

 --Cash Receipt(Cash Payment in the case of Return)
 
 if @CashPayment <> 0
 Begin
 if @IsReturn = 0
 Begin
 --Voucher Number finding
 select @CashVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Cash Receipt'
 --Insert into Voucher Table Card
 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@CashVoucherNo, @SalesDate, @CashPayment, 'Receipt Voucher', @PartyAccountId, @CashAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Cash Receipt', @FK_BranchId, @SequenceNo, @Terminal )
 
 set @VoucherInvIdCashReceipt = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCashReceipt,@CashVoucherNo, 'Receipt Voucher', @SalesDate, @PartyAccountId, @CashAccountId,0, @CashPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Cash Receipt', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCashReceipt,@CashVoucherNo, 'Receipt Voucher', @SalesDate,@CashAccountId, @PartyAccountId,@CashPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Cash Receipt', @FK_BranchId, @SequenceNo, @Terminal )
 end
 else
 begin
  --Voucher Number finding
 select @CashVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Payment Voucher' and VchrName='Cash Payment'
 --Insert into Voucher Table
 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@CashVoucherNo, @SalesDate, @CashPayment, 'Payment Voucher', @PartyAccountId, @CashAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Cash Payment', @FK_BranchId, @SequenceNo, @Terminal )
 
 set @VoucherInvIdCashPayment = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCashPayment,@CashVoucherNo, 'Payment Voucher', @SalesDate,@CashAccountId, @PartyAccountId, 0, @CashPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Cash Payment', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCashPayment,@CashVoucherNo, 'Payment Voucher', @SalesDate,@PartyAccountId,@CashAccountId, @CashPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Cash Payment', @FK_BranchId, @SequenceNo, @Terminal )
 End
 End

 if @CardPayment <> 0
 Begin
 
 if @IsReturn = 0

 Begin
 --Voucher Number finding
 select @CardVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Bank Receipt'
 --Insert into Voucher Table-Card
 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@CardVoucherNo, @SalesDate, @CardPayment, 'Receipt Voucher', @PartyAccountId, @CardAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )
 
 set @VoucherInvIdCardReceipt = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCardReceipt,@CardVoucherNo, 'Receipt Voucher', @SalesDate, @PartyAccountId, @CardAccountId,0, @CardPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCardReceipt,@CardVoucherNo, 'Receipt Voucher', @SalesDate,@CashAccountId, @PartyAccountId,@CardPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )
 end
 else
 begin
  --Voucher Number finding
 select @CardVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Payment Voucher' and VchrName='Bank Payment'
 --Insert into Voucher Table
 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@CardVoucherNo, @SalesDate, @CardPayment, 'Payment Voucher', @PartyAccountId, @CardAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )
 
 set @VoucherInvIdCardPayment = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCardPayment,@CardVoucherNo, 'Payment Voucher', @SalesDate,@CardAccountId, @PartyAccountId, 0, @CardPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCardPayment,@CardVoucherNo, 'Payment Voucher', @SalesDate,@PartyAccountId,@CardAccountId, @CardPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )
 End
 End

 if @OtherPayment <> 0
 Begin
 
 if @IsReturn = 0

 Begin
 --Voucher Number finding
 select @OnlineVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Bank Receipt'
 --Insert into Voucher Table-Card

 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@OnlineVoucherNo, @SalesDate, @OtherPayment, 'Receipt Voucher', @PartyAccountId, @OnlineAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )

 set @VoucherInvIdOnlineReceipt = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdOnlineReceipt,@OnlineVoucherNo, 'Receipt Voucher', @SalesDate, @PartyAccountId, @OnlineAccountId,0, @OtherPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdOnlineReceipt,@OnlineVoucherNo, 'Receipt Voucher', @SalesDate,@OnlineAccountId, @PartyAccountId,@OtherPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )
 end
 else
 begin
  --Voucher Number finding
 select @OnlineVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Payment Voucher' and VchrName='Bank Payment'
 --Insert into Voucher Table
 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@OnlineVoucherNo, @SalesDate, @OtherPayment, 'Payment Voucher', @PartyAccountId, @OnlineAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )
 
 set @VoucherInvIdOnlinePayment = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdOnlinePayment,@OnlineVoucherNo, 'Payment Voucher', @SalesDate,@OnlineAccountId, @PartyAccountId, 0, @OtherPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdOnlinePayment,@OnlineVoucherNo, 'Payment Voucher', @SalesDate,@PartyAccountId,@OnlineAccountId, @OtherPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )
 End
 End
 set @return=@SalesInvId
 select @return 
END
else
Begin
--Delete From Tables
Delete from GdwnStckTranLog where VchrId=@SalesInvId and VchrType='Sales Voucher' and FK_BranchId=@FK_BranchId
Delete From StockTranLog Where VchrId=@SalesInvId and VchrType ='Sales Voucher' and VchrFormType = @InvType and FK_BranchId=@FK_BranchId
delete from TransactionLog where VchrId=@SalesInvId and VchrNo=@SalesInvNo and VchrType='Sales Voucher' and FK_BranchId=@FK_BranchId
Delete From SalesEstimateSettings Where SalesInvId =@SalesInvId
delete from TransactionLog where Narration=CONCAT('POS',@SalesInvId)  and FK_BranchId=@FK_BranchId
Delete From VchrTranLog Where Narration =CONCAT('POS',@SalesInvId)  and FK_BranchId=@FK_BranchId
Delete From SalesEstimateSettings Where SalesInvId =@SalesInvId

--Update Sales Master
update SalesEstimateMaster set SalesInvNo=@SalesInvNo, InvType=@InvType, SalesDate=@SalesDate, SalesAccountId=@SalesAccountId, PartyAccountId=@PartyAccountId, RouteId=@RouteId, 
VehId=@VehId, StafId=@StafId, PriceCategoryId=@PriceCategoryId, AditionalAmount=@AditionalAmount, Frieght=@Frieght, TotalDiscount=@TotalDiscount, BillDiscount=@BillDiscount, 
GrandDiscount=@GrandDiscount, JEId=@JEId, EntryDate=@EntryDate, Remarks=@Remarks, IGSTAmount=@IGSTAmount, SGSTAmount=@SGSTAmount, CGSTAmount=@CGSTAmount, TaxableAmount=@TaxableAmount, 
FYearID=@FYearID, FloodCess=@FloodCess, TotalTaxAmount=@TotalTaxAmount, FK_BranchId=@FK_BranchId, CntrInvNo=@CntrInvNo, SequenceNo=@SequenceNo, CashPayment=@CashPayment, 
CardPayment=@CardPayment, Credit=@Credit, FielOne=@FielOne, FieldTwo=@FieldTwo, FieldThree=@FieldThree, FieldFour=@FieldFour, Terminal=@Terminal, IsClosed=@IsClosed, 
FreeAmount=@FreeAmount, ReturnAmount=@ReturnAmount, OtherPayment=@OtherPayment, CashAccountId=@CashAccountId, CardAccountId=@CardAccountId, OnlineAccountId=@OnlineAccountId, 
IsSettiled=@IsSettiled, TotalPayment=@TotalPayment, VoucherNumber=@VoucherNumber  where SalesInvId=@SalesInvId

 --Inserting Details Table
  insert into SalesEstimateSettings(SalesInvId, PartyAccountId, ProductId, ProQty, UnitId, ProRate, ProDiscont, DiscAmnt, GrossAmnt, NetAmnt, Date, HSN, GST, GSTAMOUNT, SGST, SGSTAmount, CGST, CGSTAmount, IGST, IGSTAMOUNT, FK_BatchId, Barcode,ItemCalory, EmployeeID, EmployeeCommision, AgentId, AgentCommission, ProductName )
 select @SalesInvId, PartyAccountId, ProductId, ProQty, UnitId, ProRate, ProDiscont, DiscAmnt, GrossAmnt, NetAmnt, Date, HSN, GST, GSTAMOUNT, SGST, SGSTAmount, CGST, CGSTAmount, IGST, IGSTAMOUNT, FK_BatchId, Barcode,ItemCalory, EmployeeID, EmployeeCommision, AgentId, AgentCommission, ProductName from @type
 --Inserting TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @SalesAccountId, @PartyAccountId, 0, @GrandDiscount, 'Sales Voucher', @JEId, @EntryDate, 'Main', @SalesInvNo, @InvType, @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @PartyAccountId, @SalesAccountId,@GrandDiscount, 0,  'Sales Voucher', @JEId, @EntryDate, 'Sub', @SalesInvNo, @InvType, @FK_BranchId, @SequenceNo, @Terminal )

 if @TotalTaxAmount <> 0
 Begin
 --Tax Payable Posting
 -- Tax Payable AccountId Finding
 select @taxPayableId= AccountId From Account Where AccountName ='TAX PAYABLE'
 --Tax Payable Account Posting
 --Inserting Tax Payable TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @SalesAccountId, @taxPayableId, @TotalTaxAmount, 0, 'Tax Entry', @JEId, @EntryDate, 'Main', @SalesInvNo, 'TAX PAYABLE', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Tax Payable TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @taxPayableId, @SalesAccountId, 0,@TotalTaxAmount,  'Tax Entry', @JEId, @EntryDate, 'Sub', @SalesInvNo, 'TAX PAYABLE', @FK_BranchId, @SequenceNo, @Terminal )
 End
 if @FloodCess <> 0
 Begin
 --Flood Tax Payable Account Posting
 --Inserting Flood  Tax Payable TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @SalesAccountId, @taxPayableId, @FloodCess, 0, 'Cess Entry', @JEId, @EntryDate, 'Main', @SalesInvNo, 'TAX PAYABLE', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Flood Tax Payable TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@SalesInvId,@SalesInvNo, 'Sales Voucher', @SalesDate, @taxPayableId, @SalesAccountId, 0,@FloodCess,  'Cess Entry', @JEId, @EntryDate, 'Sub', @SalesInvNo, 'TAX PAYABLE', @FK_BranchId, @SequenceNo, @Terminal )
 End

 --Cash Receipt(Cash Payment in the case of Return)
 
 if @CashPayment <> 0
 Begin
 if @IsReturn = 0
 Begin
 --Voucher Number finding
 select @CashVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Cash Receipt'
 --Insert into Voucher Table Card
 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@CashVoucherNo, @SalesDate, @CashPayment, 'Receipt Voucher', @PartyAccountId, @CashAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Cash Receipt', @FK_BranchId, @SequenceNo, @Terminal )
 set @VoucherInvIdCashReceipt = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCashReceipt,@CashVoucherNo, 'Receipt Voucher', @SalesDate, @PartyAccountId, @CashAccountId,0, @CashPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Cash Receipt', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCashReceipt,@CashVoucherNo, 'Receipt Voucher', @SalesDate,@CashAccountId, @PartyAccountId,@CashPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Cash Receipt', @FK_BranchId, @SequenceNo, @Terminal )
 end
 else
 begin
  --Voucher Number finding
 select @CashVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Payment Voucher' and VchrName='Cash Payment'
 --Insert into Voucher Table
 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@CashVoucherNo, @SalesDate, @CashPayment, 'Payment Voucher', @PartyAccountId, @CashAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Cash Payment', @FK_BranchId, @SequenceNo, @Terminal )
 set @VoucherInvIdCashPayment = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCashPayment,@CashVoucherNo, 'Payment Voucher', @SalesDate,@CashAccountId, @PartyAccountId, 0, @CashPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Cash Payment', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCashPayment,@CashVoucherNo, 'Payment Voucher', @SalesDate,@PartyAccountId,@CashAccountId, @CashPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Cash Payment', @FK_BranchId, @SequenceNo, @Terminal )
 End
 End

 if @CardPayment <> 0
 Begin
 if @IsReturn = 0

 Begin
 --Voucher Number finding
 select @CardVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Bank Receipt'
 --Insert into Voucher Table-Card

 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@CardVoucherNo, @SalesDate, @CardPayment, 'Receipt Voucher', @PartyAccountId, @CardAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )
 set @VoucherInvIdCardReceipt = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCardReceipt,@CardVoucherNo, 'Receipt Voucher', @SalesDate, @PartyAccountId, @CardAccountId,0, @CardPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCardReceipt,@CardVoucherNo, 'Receipt Voucher', @SalesDate,@CashAccountId, @PartyAccountId,@CardPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )
 end
 else
 begin
  --Voucher Number finding
 select @CardVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Payment Voucher' and VchrName='Bank Payment'
 --Insert into Voucher Table
 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@CardVoucherNo, @SalesDate, @CardPayment, 'Payment Voucher', @PartyAccountId, @CardAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )
 set @VoucherInvIdCardPayment = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCardPayment,@CardVoucherNo, 'Payment Voucher', @SalesDate,@CardAccountId, @PartyAccountId, 0, @CardPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdCardPayment,@CardVoucherNo, 'Payment Voucher', @SalesDate,@PartyAccountId,@CardAccountId, @CardPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )
 End
 End

 if @OtherPayment <> 0
 Begin
 if @IsReturn = 0

 Begin
 --Voucher Number finding
 select @OnlineVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Bank Receipt'
 --Insert into Voucher Table-Card

 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@OnlineVoucherNo, @SalesDate, @OtherPayment, 'Receipt Voucher', @PartyAccountId, @OnlineAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )
 set @VoucherInvIdOnlineReceipt = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdOnlineReceipt,@OnlineVoucherNo, 'Receipt Voucher', @SalesDate, @PartyAccountId, @OnlineAccountId,0, @OtherPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdOnlineReceipt,@OnlineVoucherNo, 'Receipt Voucher', @SalesDate,@OnlineAccountId, @PartyAccountId,@OtherPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Bank Receipt', @FK_BranchId, @SequenceNo, @Terminal )
 end
 else
 begin
  --Voucher Number finding
 select @OnlineVoucherNo = isnull(max(VchrNo),0)+1 from VchrTranLog where VchrType='Payment Voucher' and VchrName='Bank Payment'
 --Insert into Voucher Table
 insert into VchrTranLog(VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@OnlineVoucherNo, @SalesDate, @OtherPayment, 'Payment Voucher', @PartyAccountId, @OnlineAccountId, CONCAT('POS' , @SalesInvId), @StafId, @JEId, @EntryDate, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )
 set @VoucherInvIdOnlinePayment = SCOPE_IDENTITY()
  --Inserting Cash receipt TransactionLog Main
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdOnlinePayment,@OnlineVoucherNo, 'Payment Voucher', @SalesDate,@OnlineAccountId, @PartyAccountId, 0, @OtherPayment,  CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Main', @SalesInvNo, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )

 --Inserting Cash receipt TransactionLog Sub
  insert into TransactionLog(VchrId,VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal )
 values(@VoucherInvIdOnlinePayment,@OnlineVoucherNo, 'Payment Voucher', @SalesDate,@PartyAccountId,@OnlineAccountId, @OtherPayment, 0,   CONCAT('POS' , @SalesInvId), @JEId, @EntryDate, 'Sub', @SalesInvNo, 'Bank Payment', @FK_BranchId, @SequenceNo, @Terminal )
 End
 End
set @return=@SalesInvId
select @return 
END
 COMMIT TRANSACTION inser_SalesInvoice
  END TRY
   BEGIN CATCH
    ROLLBACK TRANSACTION inser_SalesInvoice;
    SELECT @errMessage = ERROR_MESSAGE()
   END CATCH 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spSalesReturnitems]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spSalesReturnitems]
(
@FromDate datetime, 
@ToDate datetime,
@Terminal nvarchar(25),
@SequenceNo int,
@BranchId int =null
)
As
Begin
Select pt.ProductName,SUM(stg.ProQty) as Quantity, SUM(stg.NetAmnt) as NetAmount
From SalesEstimateSettings stg
inner Join SalesEstimateMaster sem on stg.SalesInvId = sem.SalesInvId
inner Join Product pt on stg.ProductId = pt.ProductId
Where (sem.FK_BranchId = @BranchId or @BranchId is null) and
sem.SalesDate between @FromDate and
@ToDate and Terminal=@Terminal and 
SequenceNo=@SequenceNo and ProQty < 0
Group by pt.ProductName
End
GO
/****** Object:  StoredProcedure [dbo].[spSalesSummaryReportPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spSalesSummaryReportPOS]
(
@FromDate datetime, 
@ToDate datetime,
@BranchId int = null
)
As
Begin
Select CONCAT(DAY(sem.SalesDate),'-',MONTH(sem.SalesDate),'-',YEAR(sem.SalesDate)) AS Date,SUM(sem.TaxableAmount) as Taxable ,SUM(sem.TotalTaxAmount) as Tax,SUM(sem.ReturnAmount) as [Return],SUM(sem.GrandDiscount) as Net
From SalesEstimateMaster sem
Where (sem.FK_BranchId = @BranchId or @BranchId is null) and
sem.SalesDate between @FromDate and @ToDate
GROUP BY DAY(sem.SalesDate), MONTH(sem.SalesDate) , YEAR(sem.SalesDate)
Order BY  YEAR(sem.SalesDate),MONTH(sem.SalesDate) ,DAY(sem.SalesDate)
End
GO
/****** Object:  StoredProcedure [dbo].[spSalesTaxReportPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spSalesTaxReportPOS]
(
@FromDate datetime
, @ToDate datetime,
@BranchId int = null
)
As
Begin
create table #tbTaxValues
 (
  EstimateId int,
  TaxValue decimal
  )
  insert into #tbTaxValues  select SalesInvId as EstimateId ,sum(isnull(GSTAMOUNT,0)) as TaxValue
  From SalesEstimateSettings 
  Where Date between @FromDate and @ToDate and GSTAMOUNT<0
  Group by SalesInvId
Select SalesDate as [Date],CntrInvNo as ReceiptNo,ac.AccountName as Account,TaxableAmount as Taxable, 
TotalTaxAmount as Tax, ReturnAmount as [Return],ABS(tb.TaxValue) as TaxOnReturn, GrandDiscount as Net
From SalesEstimateMaster sem
Left join Account ac on sem.PartyAccountId = ac.AccountId
LEFT join #tbTaxValues tb on sem.SalesInvId=tb.EstimateId
Where (sem.FK_BranchId = @BranchId or @BranchId is null) and
SalesDate between @FromDate and @ToDate
End
GO
/****** Object:  StoredProcedure [dbo].[spStockAdjustProductLoading]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spStockAdjustProductLoading]
(
@ProGroupId int=null,@BrandId int=null
)
 AS 
 BEGIN 
 
Select row_number() over( order by PR.ProductId) as SL, PR.ProductId , PR.ProductCode, PR.ProductName,ISNULL(SUM(strl.InQuantity + strl.OutQuantity),0) As currSystemStock,
 PR.UnitId,UT.UnitName,ISNULL(SUM(strl.InQuantity + strl.OutQuantity),0) As curActualStock,0 as Excess, 0 as Short,PR.SellingPrice AS Rate, 0 As TotalAmount
 From dbo.Product PR
 Left JOIN dbo.Unit UT on PR.UnitId = UT.UnitId
 Left JOIN dbo.StockTranLog strl on strl.ProductId=PR.ProductId
 Where   ( PR.PGroupId = @ProGroupId or @ProGroupId is null)
 and (PR.BrandId = @BrandId or @BrandId is null)

 group by PR.ProductId , PR.ProductCode, PR.ProductName,PR.SellingPrice,UT.UnitName,PR.UnitId
 order by PR.ProductCode
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spStockAndProfitPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spStockAndProfitPOS]
(
@ProductId int =null,
@BranchId int = null
)
As
Begin
Select pt.ProductName,pt.Barcode,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock,
pt.PurchasePrice as BuyingPrice,pt.SellingPrice as SalePrice,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) * pt.PurchasePrice as
TotalBuyingValue, SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) * pt.SellingPrice as TotalSaleValue, SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) * pt.SellingPrice
- SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) * pt.PurchasePrice as Profit
From Product pt
left join StockTranLog stl on stl.ProductId = pt.ProductId
where (pt.ProductId = @ProductId or @ProductId is null) and
(stl.FK_BranchId = @BranchId or @BranchId is null)
group by pt.ProductName, pt.Barcode,pt.PurchasePrice,pt.SellingPrice
End
GO
/****** Object:  StoredProcedure [dbo].[spStockReportNormal]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spStockReportNormal]
(
@Validation nvarchar(50)='',
@Date datetime='2021-07-14',
@ProductGroupId int =null,
@ProductId int=null,
@BranchId int=null,
@BrandId int =null,
@SupplierId int = null,
@TaxCategoryId int = null,
@Type nvarchar(50)=null
)
As
Begin
declare @SValue nvarchar(20)=''
create table #productstock
 (
  SL nvarchar(6),
  Barcode nvarchar(20),
  ProductName nvarchar(100),
  Stock nvarchar(15),
  UnitName nvarchar(5),
  Rates nvarchar(15),
  GrossValue nvarchar(15),
  SaleDis nvarchar(15),
  DisAmount nvarchar(15),
  StockValue nvarchar(15),
  ReOrderStock nvarchar(15),
  Specification nvarchar(100),
  HSNNo nvarchar(100),
  SalesVat nvarchar(100),
  PGroupName nvarchar(100),
  BrandName nvarchar(100),
  AccountName nvarchar(100),
  GodownName nvarchar(100),
  SelfName nvarchar(100)
  )
create table #productrates
 (
  Rates decimal,
  ProductId int,
  Stock decimal
  )
  if @Validation='Selling Price'
  insert into #productrates select pt.SellingPrice as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date
  group by pt.ProductId,pt.SellingPrice

  else if @Validation='Average Selling Price'
   insert into #productrates select isnull(AVG(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date and ses.Date <=@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Highest Selling Price'
   insert into #productrates select isnull(MAX(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date and ses.Date <=@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Last Selling Price'
  Begin
  create table #productlastprice
 (
  Rates decimal,
  ProductId int,
  )
    insert into #productlastprice select Top 1 ses.ProRate as Rates, pt.ProductId
  From Product pt
  left join SalesEstimateSettings ses on pt.ProductId = ses.ProductId
  left join SalesEstimateMaster sem on ses.SalesInvId=sem.SalesInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  sem.SalesDate <=@Date
  group by pt.ProductId,ses.ProRate,ses.Date
  order by ses.Date desc

   insert into #productrates select isnull(ses.Rates,pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join #productlastprice ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date
   group by pt.ProductId,ses.Rates,pt.SellingPrice
  end
  else if @Validation='Purchase Price'
  insert into #productrates select pt.PurchasePrice as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date
  group by pt.ProductId,pt.PurchasePrice

  else if @Validation='MRP'
  insert into #productrates select pt.MRP as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date 
  group by pt.ProductId,pt.MRP

  else if @Validation='Average Purchase Price'
   insert into #productrates select isnull(AVG(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join PurInvSettings ses on pt.ProductId = ses.ProId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date and ses.Date<=@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Highest Purchase Price'
   insert into #productrates select isnull(MAX(ses.ProRate),pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join PurInvSettings ses on pt.ProductId = ses.ProId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
 stl.VchrDate <=@Date and ses.Date<=@Date
  group by pt.ProductId,pt.SellingPrice
  order by pt.ProductId

  else if @Validation='Last Purchase Price'
  Begin
  create table #productlastpricepurchase
 (
  Rates decimal,
  ProductId int,
  )
    insert into #productlastpricepurchase select Top 1 ses.ProRate as Rates, pt.ProductId
  From Product pt
  left join PurInvSettings ses on pt.ProductId = ses.ProId
  left join PurInvMaster sem on ses.PurInvId=sem.PurInvId
  where (sem.FK_BranchId=@BranchId or @BranchId is null) and
  sem.PurDate <=@Date
  group by pt.ProductId,ses.ProRate,ses.Date
  order by ses.Date desc

   insert into #productrates select isnull(ses.Rates,pt.SellingPrice) as Rates,pt.ProductId,SUM(isnull(stl.InQuantity,0)-isnull(stl.OutQuantity,0)) as Stock
  From Product pt
  left join StockTranLog stl on pt.ProductId = stl.ProductId
  left join #productlastpricepurchase ses on pt.ProductId = ses.ProductId
  where (stl.FK_BranchId=@BranchId or @BranchId is null) and
  stl.VchrDate <=@Date
   group by pt.ProductId,ses.Rates,pt.SellingPrice
  end
insert into #productstock Select row_number() over(order by pt.ProductId) as SL,pt.Barcode, pt.ProductName,isnull(prs.Stock,0) as Stock,ut.UnitName,isnull(prs.Rates,0),
prs.Stock * prs.Rates as GrossValue, Format(pt.SaleDis,'N'),Format((prs.Stock *(prs.Rates * iif(pt.SaleDis > 0, pt.SaleDis/100,0)) ),'N') as [Dis Amount],
(prs.Stock * prs.Rates)-(prs.Rates * iif(pt.SaleDis > 0, pt.SaleDis/100,0)) as [Stock Value],
Format(pt.ReOrderStock,'N'),pt.Specification,pt.HSNNo,Format(tct.SalesVat,'N'),pg.PGroupName,bd.BrandName,act.AccountName,gd.GodownName,sf.SelfName
From Product pt
left join ProductGroup pg on pt.PGroupId = pg.PGroupId
left join #productrates prs on pt.ProductId = prs.ProductId
left join Unit ut on pt.UnitId = ut.UnitId
left join TaxCategory tct on pt.TCategoryId = tct.TCategoryId
left join Brand bd on pt.BrandId = bd.BrandId
left join Account act on pt.AccountId = act.AccountId
left join Godown gd on pt.GodwnId = gd.GodwnId
left join [Self] sf on pt.SelfId = sf.SelfId
where prs.Stock <> 0 and
(pt.PGroupId = @ProductGroupId or @ProductGroupId is null) and
(pt.TCategoryId=@TaxCategoryId or @TaxCategoryId is null) and
(pt.ProductId=@ProductId or @ProductId is null) and
(pt.AccountId=@SupplierId or @SupplierId is null) and
(pt.BrandId=@BrandId or @BrandId is null) and
(pt.ProductType=@Type or @Type is null)
group by pt.ProductId,pt.Barcode, pt.ProductName,pt.Barcode,ut.UnitName,prs.Rates,pt.SaleDis,prs.Stock,
pt.ReOrderStock,pt.Specification,pt.HSNNo,tct.SalesVat,pg.PGroupName,bd.BrandName,act.AccountName,gd.GodownName,sf.SelfName
order by pt.ProductId

insert into #productstock select @SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue
insert into #productstock select @SValue,@SValue,'Total',sum(pr.Stock) as stock,@SValue,@SValue,sum(stock*Rates) as GrossValue,
Format(sum(pr.Stock *(pr.Rates * iif(pt.SaleDis > 0, pt.SaleDis/100,0))),'N') as DiscAmount,@SValue,(sum(stock*Rates))-(sum(pr.Stock *(pr.Rates * iif(pt.SaleDis > 0, pt.SaleDis/100,0)))) as StockValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue,@SValue
From Product pt
left join #productrates pr on pt.ProductId = pr.ProductId
where (pt.PGroupId = @ProductGroupId or @ProductGroupId is null) and
(pt.TCategoryId=@TaxCategoryId or @TaxCategoryId is null) and
(pt.ProductId=@ProductId or @ProductId is null) and
(pt.AccountId=@SupplierId or @SupplierId is null) and
(pt.BrandId=@BrandId or @BrandId is null) and
(pt.ProductType=@Type or @Type is null)
 select * from #productstock
drop table #productrates
drop table #productstock
END
GO
/****** Object:  StoredProcedure [dbo].[spSupplierItemPurchaseReportPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spSupplierItemPurchaseReportPOS]
(
@FromDate datetime, @ToDate datetime,@AccountId int
)
As
Begin
Select pim.PurDate,pim.PurInvNo,pim.RefInvNo,pt.ProductName,pt.Barcode,pis.ProQty,ut.UnitName,pis.ProRate,pis.Gross,pis.DiscAmnt,
pis.NetAmnt-pis.GSTAMOUNT as Taxable, pis.GSTAMOUNT,pis.NetAmnt
From PurInvSettings pis
inner Join PurInvMaster pim on pis.PurInvId = pim.PurInvId
Left Join Product pt on pis.ProId = pt.ProductId
Left Join Unit ut on pis.UnitId = ut.UnitId
Where pim.PartyAccId=@AccountId and pim.PurDate between @FromDate and @ToDate
order by pim.PurDate
End
GO
/****** Object:  StoredProcedure [dbo].[spSupplierPurchase]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spSupplierPurchase]
(
@FromDate datetime, 
@ToDate datetime,
@Terminal nvarchar(25),
@SequenceNo int,
@BranchId int =null 
)
As
Begin
Select act.AccountName as SupplareName,SUM(TotalAmnt) as Amount
From PurInvMaster pim
Left Join Account act on pim.PartyAccId = act.AccountId
Where (pim.FK_BranchId = @BranchId or @BranchId is null) and
pim.PurDate between @FromDate and @ToDate and 
pim.Terminal = @Terminal and 
SequenceNo=@SequenceNo
group by act.AccountName
End
GO
/****** Object:  StoredProcedure [dbo].[spSupplierPurchaseReportPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spSupplierPurchaseReportPOS]
(
@FromDate datetime, 
@ToDate datetime,
@AccountId int=null,
@BranchId int =null
)
As
Begin 
Select convert(nvarchar,PurDate,103) as InvoiceDate,act.AccountName as SupplierName,act.TinNo as TaxId, RefInvNo as InvoiceNo,Taxable as Taxable,TotalTax as Tax, 
isnull(CashPayment,0)+isnull(CardPayment,0) as Payment, ReturnAmount as [Return], TotalAmnt as Net, 
isnull(TotalAmnt,0)-(isnull(CashPayment,0)+isnull(CardPayment,0)) as Balance
From PurInvMaster pm
inner join Account act on pm.PartyAccId=act.AccountId
Where (pm.FK_BranchId = @BranchId or @BranchId is null) and
(PartyAccId=@AccountId or @AccountId is null) and 
PurDate between @FromDate and @ToDate
order by PurDate
End
GO
/****** Object:  StoredProcedure [dbo].[spSupplierReportPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spSupplierReportPOS]
(
@SupplierId int=null,
@BranchId int =null
)
As
Begin
Select act.AccountName as SupplierName,act.TinNo as TaxId,act.AddressOne as [Address], act.MobileNo as PhoneNumber,
act.DebitLimit as CreditLimit,Sum(isnull(tl.DebitAmount,0)-isnull(tl.CreditAmount,0)) as CurrentBalance
From Account act
left join TransactionLog tl on tl.AccountId = act.AccountId
where (act.AccountId = @SupplierId or @SupplierId is null) and
(tl.FK_BranchId = @BranchId or @BranchId is null)
group by act.AccountName, act.AddressOne, act.MobileNo, act.DebitLimit,act.TinNo
order by act.AccountName
End
GO
/****** Object:  StoredProcedure [dbo].[spTaxCategoryReportPOS]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spTaxCategoryReportPOS]
(
@FromDate datetime, 
@ToDate datetime,
@BranchId int = null
)
As
Begin
Select tg.TCategoryName as TaxName,pg.PGroupName as GroupName,pt.ProductName as Product,sum(ses.ProQty) as Quantity,SUM(isnull(ses.NetAmnt,0)-isnull(ses.GSTAMOUNT,0))
as Taxable,SUM(ses.GSTAMOUNT) as Tax,SUM(ses.NetAmnt) as Net
From SalesEstimateSettings ses
Left Join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
Left join Product pt on ses.ProductId=pt.ProductId
left join ProductGroup pg on pt.PGroupId = pg.PGroupId
Left join TaxCategory tg on pt.TCategoryId = tg.TCategoryId
Where (sem.FK_BranchId = @BranchId or @BranchId is null) and
sem.SalesDate between @FromDate and @ToDate
Group by tg.TCategoryName,pg.PGroupName,pt.ProductName
END
GO
/****** Object:  StoredProcedure [dbo].[spTopCreditors]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spTopCreditors]
(
@Date datetime
)
AS 
 BEGIN 


 Select top 10 (sum(tl.CreditAmount-tl.DebitAmount)) as CreditorsBalance, act.AccountName as CreditorsName From TransactionLog tl
Join Account act on tl.AccountId = act.AccountId
Join AccountGroup ag on act.AGroupId = ag.AGroupId
Where ag.GroupName='SUPPLIERS/SUNDRY CREDITORS'
and tl.VchrDate <= @Date
group by act.AccountName
order by CreditorsBalance desc
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spTopDebitors]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spTopDebitors]
(
@Date datetime
)
AS 
 BEGIN 


 Select Top 10 (sum(tl.DebitAmount-tl.CreditAmount)) as DebtorsBalance, act.AccountName as DebtorsName 
 From TransactionLog tl
Left Join Account act on tl.AccountId = act.AccountId
Left Join AccountGroup ag on act.AGroupId = ag.AGroupId
Where  ag.GroupName='CUSTOMER/SUNDRY DEBTORS'
and tl.VchrDate <= @Date
Group by act.AccountName
order by DebtorsBalance desc
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spTopProductSold]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spTopProductSold]
(
@FromDate datetime,
@ToDate datetime
)
AS 
 BEGIN 


 Select top 10 ( isnull(sum(stl.OutQuantity-stl.InQuantity),0) * isnull(pd.SellingPrice,0)) as NetAmount, isnull(sum(stl.OutQuantity-stl.InQuantity),0) as SoldQuantity,pd.ProductName as ProductName
 From StockTranLog stl
 Join Product pd on stl.ProductId= pd.ProductId
 where stl.TranDate between @FromDate and @ToDate
 and VchrType = 'Sales Voucher'
 Group by pd.ProductName,pd.SellingPrice
 order by NetAmount desc
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spTopProductStockReport]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spTopProductStockReport]
(
@Date datetime=null
)
AS 
 BEGIN 

 create table #Product
 (
  ProductName nvarchar(200),
  StockValue decimal(24,4),
  Quantity decimal(24,4),
  )

 insert into #Product Select pd.ProductName, isnull(sum(stl.InQuantity-stl.OutQuantity),0) * isnull(pd.SellingPrice,0) as StockValue, isnull(sum(stl.InQuantity-stl.OutQuantity),0) as Quantity
 From StockTranLog stl
 Join Product pd on stl.ProductId= pd.ProductId
 where stl.TranDate <= @Date
 Group by pd.ProductName,pd.SellingPrice
 Order by StockValue
 
 select Top 10 ( StockValue) as ProStockValue ,ProductName ,Quantity as ProductQuantity
 From #Product
 order by StockValue desc
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateAccount]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateAccount]
(
@AccountId int=NULL, @AGroupId int=NULL, @RootId int=NULL, @PCategoryId int=NULL, @AccountName nvarchar(200)=NULL, @OpeningBalance decimal(18,4)=NULL, @DebitOrCridt nvarchar(5)=NULL, @AddressOne nvarchar(50)=NULL, @AddressTwo nvarchar(50)=NULL, @AddressThree nvarchar(50)=NULL, @PhoneOne nvarchar(20)=NULL, @MobileNo nvarchar(20)=NULL, @FaxNo nvarchar(20)=NULL, @TinNo nvarchar(20)=NULL, @CstNo nvarchar(20)=NULL, @EMailOne nvarchar(50)=NULL, @Remarks nvarchar(400)=NULL, @BankName nvarchar(50)=NULL, @BankAddress nvarchar(50)=NULL, @AccountNo nvarchar(50)=NULL, @isActive bit=NULL, @IsBuiltIn bit=NULL, @DebitLimit decimal(24,4)=NULL, @IsLimitApplicable bit=NULL, @AddressFour nvarchar(50)=NULL, @FieldOne nvarchar(50)=NULL, @FieldTwo nvarchar(50)=NULL, @FieldThree nvarchar(50)=NULL, @FieldFour nvarchar(50)=NULL 
)
 AS 
 BEGIN 
 
 update Account set AGroupId=@AGroupId, RootId=@RootId, PCategoryId=@PCategoryId, AccountName=@AccountName, OpeningBalance=@OpeningBalance, DebitOrCridt=@DebitOrCridt, AddressOne=@AddressOne, AddressTwo=@AddressTwo, AddressThree=@AddressThree, PhoneOne=@PhoneOne, MobileNo=@MobileNo, FaxNo=@FaxNo, TinNo=@TinNo, CstNo=@CstNo, EMailOne=@EMailOne, Remarks=@Remarks, BankName=@BankName, BankAddress=@BankAddress, AccountNo=@AccountNo, isActive=@isActive, IsBuiltIn=@IsBuiltIn, DebitLimit=@DebitLimit, IsLimitApplicable=@IsLimitApplicable, AddressFour=@AddressFour, FieldOne=@FieldOne, FieldTwo=@FieldTwo, FieldThree=@FieldThree, FieldFour=@FieldFour  where AccountId=@AccountId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateAccountGroup]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateAccountGroup]
(
@AGroupId int=NULL, @GroupName nvarchar(50)=NULL, @Under nvarchar(50)=NULL, @Remarks nvarchar(400)=NULL, @isBuilIn bit=NULL 
)
 AS 
 BEGIN 
 
 update AccountGroup set GroupName=@GroupName, Under=@Under, Remarks=@Remarks, isBuilIn=@isBuilIn  where AGroupId=@AGroupId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateAttenMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateAttenMaster]
(
@AttendanceMasterId int=NULL, @AtteInvNo int=NULL, @AtteDate datetime=NULL, @Remarks nvarchar(400)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update AttenMaster set AtteInvNo=@AtteInvNo, AtteDate=@AtteDate, Remarks=@Remarks, FK_BranchId=@FK_BranchId  where AttendanceMasterId=@AttendanceMasterId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateAttenSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateAttenSettings]
(
@AttenSettingsId int=NULL, @AttendanceMasterId int=NULL, @EmpID int=NULL, @Morning bit=NULL, @Evening bit=NULL, @AttenType decimal(18,1)=NULL, @EmpName nvarchar(50)=NULL, @AtteDate datetime=NULL 
)
 AS 
 BEGIN 
 
 update AttenSettings set EmpID=@EmpID, Morning=@Morning, Evening=@Evening, AttenType=@AttenType, EmpName=@EmpName, AtteDate=@AtteDate  where AttendanceMasterId=AttendanceMasterId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateBarcodeDTL]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateBarcodeDTL]
(
@id int=NULL, @Name nvarchar(100)=NULL, @Text nvarchar(100)=NULL, @Value nvarchar(100)=NULL, @isIncluded bit=NULL, @Position_x int=NULL, @Position_y int=NULL, @Width int=NULL, @Height int=NULL, @Alignment nvarchar(50)=NULL, @RotationPoint nvarchar(50)=NULL, @fontFamily nvarchar(50)=NULL, @fontSize int=NULL, @fStyle nvarchar(50)=NULL, @Format nvarchar(50)=NULL, @bAlignment nvarchar(50)=NULL, @FK_BarcodeHDR int=NULL, @Type nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update BarcodeDTL set Name=@Name, Text=@Text, Value=@Value, isIncluded=@isIncluded, Position_x=@Position_x, Position_y=@Position_y, Width=@Width, Height=@Height, Alignment=@Alignment, RotationPoint=@RotationPoint, fontFamily=@fontFamily, fontSize=@fontSize, fStyle=@fStyle, Format=@Format, bAlignment=@bAlignment, FK_BarcodeHDR=@FK_BarcodeHDR, Type=@Type  where id=@id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateBarcodeHDR]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateBarcodeHDR]
(
@Id int=NULL, @name nvarchar(50)=NULL, @FirstMargin int=NULL, @TopMargin int=NULL, @RightMargin int=NULL, @BottomMargin int=NULL, @Width int=NULL, @Height int=NULL, @Accross int=NULL, @Down int=NULL, @TotalPerPage int=NULL, @PrinterName nvarchar(50)=NULL, @PaperName nvarchar(50)=NULL, @PSWidth int=NULL, @PSHieght int=NULL, @isBorderIncluded bit=NULL, @isCuttingEdge bit=NULL, @BarcodeField int=NULL, @isBarcode bit=NULL 
)
 AS 
 BEGIN 
 
 update BarcodeHDR set name=@name, FirstMargin=@FirstMargin, TopMargin=@TopMargin, RightMargin=@RightMargin, BottomMargin=@BottomMargin, Width=@Width, Height=@Height, Accross=@Accross, Down=@Down, TotalPerPage=@TotalPerPage, PrinterName=@PrinterName, PaperName=@PaperName, PSWidth=@PSWidth, PSHieght=@PSHieght, isBorderIncluded=@isBorderIncluded, isCuttingEdge=@isCuttingEdge, BarcodeField=@BarcodeField, isBarcode=@isBarcode  where Id=@Id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateBarcodeScale]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateBarcodeScale]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @IPAddress nvarchar(100)=NULL, @Remarks nvarchar(400)=NULL 
)
 AS 
 BEGIN 
 
 update BarcodeScale set Name=@Name, IPAddress=@IPAddress, Remarks=@Remarks  where Id=@Id
 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateBatch]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateBatch]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @FK_PurInvId int=NULL, @Remarks nvarchar(100)=NULL, @FK_BranchId int=NULL, @RefNo nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 update Batch set Name=@Name, FK_PurInvId=@FK_PurInvId, Remarks=@Remarks, FK_BranchId=@FK_BranchId, RefNo=@RefNo  where Id=@Id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateBranch]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateBranch]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @Description nvarchar(500)=NULL, @Address nvarchar(400)=NULL, @City nvarchar(100)=NULL, @State nvarchar(100)=NULL, @Country nvarchar(100)=NULL, @PostalCode nvarchar(100)=NULL, @PhoneNumber nvarchar(100)=NULL, @MobileNumber nvarchar(100)=NULL, @ContactPerson nvarchar(100)=NULL, @Status nvarchar(100)=NULL, @Active bit=NULL 
)
 AS 
 BEGIN 
 
 update Branch set Name=@Name, Description=@Description, Address=@Address, City=@City, State=@State, Country=@Country, PostalCode=@PostalCode, PhoneNumber=@PhoneNumber, MobileNumber=@MobileNumber, ContactPerson=@ContactPerson, Status=@Status, Active=@Active  where Id=@Id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateBrand]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateBrand]
(
@BrandId int=NULL, @BrandName nvarchar(50)=NULL, @Description nvarchar(400)=NULL 
)
 AS 
 BEGIN 
 
 update Brand set BrandName=@BrandName, Description=@Description  where BrandId=@BrandId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateChequeDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateChequeDetails]
(
@CheqDtlsId int=NULL, @VchrId int=NULL, @VchrType nvarchar(50)=NULL, @AccountId int=NULL, @BankAcctId int=NULL, @Amount decimal(18,2)=NULL, @Bank nvarchar(200)=NULL, @Branch nvarchar(500)=NULL, @CheqNo nvarchar(25)=NULL, @CheqDate nvarchar(20)=NULL, @Status nvarchar(10)=NULL, @RefNo nvarchar(20)=NULL, @Remarks nvarchar(500)=NULL, @TranDate datetime=NULL, @JeId int=NULL, @Type nvarchar(50)=NULL, @CollectedDate datetime=NULL, @Date datetime=NULL, @ChqRtnAmount decimal(18,2)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update ChequeDetails set VchrId=@VchrId, VchrType=@VchrType, AccountId=@AccountId, BankAcctId=@BankAcctId, Amount=@Amount, Bank=@Bank, Branch=@Branch, CheqNo=@CheqNo, CheqDate=@CheqDate, Status=@Status, RefNo=@RefNo, Remarks=@Remarks, TranDate=@TranDate, JeId=@JeId, Type=@Type, CollectedDate=@CollectedDate, Date=@Date, ChqRtnAmount=@ChqRtnAmount, FK_BranchId=@FK_BranchId  where CheqDtlsId=@CheqDtlsId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateCostCenter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateCostCenter]
(
@CostCenterID int=NULL, @CostCenterName nvarchar(50)=NULL, @Description nvarchar(400)=NULL 
)
 AS 
 BEGIN 
 
 update CostCenter set CostCenterName=@CostCenterName, Description=@Description  where CostCenterID=@CostCenterID
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateCulenderPlanning]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateCulenderPlanning]
(
@CPlaningId int=NULL, @CplanninDate datetime=NULL, @Description image=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update CulenderPlanning set CplanninDate=@CplanninDate, Description=@Description, FK_BranchId=@FK_BranchId  where CPlaningId=@CPlaningId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateCurrencyInfo]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateCurrencyInfo]
(
@CurrencyID int=NULL, @CurrencyCode nvarchar(10)=NULL, @IsCurrencyNameFeminine bit=NULL, @EnglishCurrencyName nvarchar(100)=NULL, @EnglishPluralCurrencyName nvarchar(100)=NULL, @EnglishCurrencyPartName nvarchar(100)=NULL, @EnglishPluralCurrencyPartName nvarchar(100)=NULL, @Arabic1CurrencyName nvarchar(100)=NULL, @Arabic2CurrencyName nvarchar(100)=NULL, @Arabic310CurrencyName nvarchar(100)=NULL, @Arabic1199CurrencyName nvarchar(100)=NULL, @Arabic1CurrencyPartName nvarchar(100)=NULL, @Arabic2CurrencyPartName nvarchar(100)=NULL, @Arabic310CurrencyPartName nvarchar(100)=NULL, @Arabic1199CurrencyPartName nvarchar(100)=NULL, @PartPrecision int=NULL, @IsCurrencyPartNameFeminine bit=NULL 
)
 AS 
 BEGIN 
 
 update CurrencyInfo set CurrencyCode=@CurrencyCode, IsCurrencyNameFeminine=@IsCurrencyNameFeminine, EnglishCurrencyName=@EnglishCurrencyName, EnglishPluralCurrencyName=@EnglishPluralCurrencyName, EnglishCurrencyPartName=@EnglishCurrencyPartName, EnglishPluralCurrencyPartName=@EnglishPluralCurrencyPartName, Arabic1CurrencyName=@Arabic1CurrencyName, Arabic2CurrencyName=@Arabic2CurrencyName, Arabic310CurrencyName=@Arabic310CurrencyName, Arabic1199CurrencyName=@Arabic1199CurrencyName, Arabic1CurrencyPartName=@Arabic1CurrencyPartName, Arabic2CurrencyPartName=@Arabic2CurrencyPartName, Arabic310CurrencyPartName=@Arabic310CurrencyPartName, Arabic1199CurrencyPartName=@Arabic1199CurrencyPartName, PartPrecision=@PartPrecision, IsCurrencyPartNameFeminine=@IsCurrencyPartNameFeminine  where CurrencyID=@CurrencyID
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateCustomQuery]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateCustomQuery]
(
@id int=NULL, @CustmQryName nvarchar(100)=NULL, @Query nvarchar(1000)=NULL 
)
 AS 
 BEGIN 
 
 update CustomQuery set CustmQryName=@CustmQryName, Query=@Query  where id=@id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateDebitCreditCardMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateDebitCreditCardMaster]
(
@CardId int=NULL, @CardName nvarchar(50)=NULL, @BankComm decimal(18,2)=NULL, @CardComm decimal(18,2)=NULL, @Remarks nvarchar(400)=NULL 
)
 AS 
 BEGIN 
 
 update DebitCreditCardMaster set CardName=@CardName, BankComm=@BankComm, CardComm=@CardComm, Remarks=@Remarks  where CardId=@CardId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateEmployeeDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateEmployeeDetails]
(
@EmpID int=NULL, @EmployeeID int=NULL, @EmpName nvarchar(400)=NULL, @AddressOne nvarchar(400)=NULL, @AddressTwo nvarchar(400)=NULL, @AddressThree nvarchar(400)=NULL, @PhNumber nvarchar(20)=NULL, @EmailId nvarchar(50)=NULL, @Designation nvarchar(50)=NULL, @Salary decimal(18,2)=NULL, @CasualLeave decimal(18,2)=NULL, @Gender nvarchar(10)=NULL, @Qualification nvarchar(400)=NULL, @ParentName nvarchar(50)=NULL, @Relation nvarchar(50)=NULL, @ParentAddress nvarchar(400)=NULL, @ParentPhNo nvarchar(20)=NULL, @Remarks nvarchar(400)=NULL, @isActive bit=NULL, @AdharNo nvarchar(400)=NULL, @SalaryAccntId int=NULL, @SalaryAdvAccntId int=NULL, @SalaryPayableId int=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update EmployeeDetails set EmployeeID=@EmployeeID, EmpName=@EmpName, AddressOne=@AddressOne, AddressTwo=@AddressTwo, AddressThree=@AddressThree, PhNumber=@PhNumber, EmailId=@EmailId, Designation=@Designation, Salary=@Salary, CasualLeave=@CasualLeave, Gender=@Gender, Qualification=@Qualification, ParentName=@ParentName, Relation=@Relation, ParentAddress=@ParentAddress, ParentPhNo=@ParentPhNo, Remarks=@Remarks, isActive=@isActive, AdharNo=@AdharNo, SalaryAccntId=@SalaryAccntId, SalaryAdvAccntId=@SalaryAdvAccntId, SalaryPayableId=@SalaryPayableId, FK_BranchId=@FK_BranchId  where EmpID=@EmpID
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateFinancialYear]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateFinancialYear]
(
@FyearId int=NULL, @FyearName nvarchar(100)=NULL, @DateFrom nvarchar(50)=NULL, @DateTo nvarchar(50)=NULL, @Old bit=NULL, @Closed bit=NULL 
)
 AS 
 BEGIN 
 
 update FinancialYear set FyearName=@FyearName, DateFrom=@DateFrom, DateTo=@DateTo, Old=@Old, Closed=@Closed  where FyearId=@FyearId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateGdwnStckTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateGdwnStckTranLog]
(
@GSTranId int=NULL, @GodwnId int=NULL, @GodownName nvarchar(100)=NULL, @ProductId int=NULL, @ProductName nvarchar(100)=NULL, @VchrId int=NULL, @VchrNo int=NULL, @VchrDate datetime=NULL, @VchrType nvarchar(100)=NULL, @InQuantity decimal(24,4)=NULL, @OutQuantity decimal(24,4)=NULL, @UnitId int=NULL, @Narration nvarchar(400)=NULL, @UserId int=NULL, @TranDate datetime=NULL, @FK_BatchId int=NULL, @FK_BranchId int=NULL, @Barcode nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update GdwnStckTranLog set GodwnId=@GodwnId, GodownName=@GodownName, ProductId=@ProductId, ProductName=@ProductName, VchrId=@VchrId, VchrNo=@VchrNo, VchrDate=@VchrDate, VchrType=@VchrType, InQuantity=@InQuantity, OutQuantity=@OutQuantity, UnitId=@UnitId, Narration=@Narration, UserId=@UserId, TranDate=@TranDate, FK_BatchId=@FK_BatchId, FK_BranchId=@FK_BranchId, Barcode=@Barcode  where GSTranId=@GSTranId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateGodown]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateGodown]
(
@GodwnId int=NULL, @GodownName nvarchar(50)=NULL, @AddressOne nvarchar(50)=NULL, @AddressTwo nvarchar(50)=NULL, @AddressThree nvarchar(50)=NULL, @Phone nvarchar(50)=NULL, @Description nvarchar(400)=NULL 
)
 AS 
 BEGIN 
 
 update Godown set GodownName=@GodownName, AddressOne=@AddressOne, AddressTwo=@AddressTwo, AddressThree=@AddressThree, Phone=@Phone, Description=@Description  where GodwnId=@GodwnId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateInstitute]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateInstitute]
(
@InstituteId int=NULL, @InstituteName nvarchar(100)=NULL, @Place nvarchar(50)=NULL, @Address nvarchar(250)=NULL, @Country nvarchar(50)=NULL, @Pincod nvarchar(50)=NULL, @PhoneNo nvarchar(50)=NULL, @MobNo nvarchar(50)=NULL, @FaxNo nvarchar(50)=NULL, @website nvarchar(50)=NULL, @EmailId nvarchar(50)=NULL, @Sector nvarchar(50)=NULL, @Logo image=NULL, @LogoPath nvarchar(100)=NULL, @Productserial nvarchar(50)=NULL, @GSTNo nvarchar(50)=NULL, @State nvarchar(50)=NULL, @StateCode int=NULL, @BankName nvarchar(50)=NULL, @BankBranch nvarchar(50)=NULL, @IFSC nvarchar(50)=NULL, @AccNo nvarchar(50)=NULL, @LocalName nvarchar(100)=NULL, @CompanyId nvarchar(20)=NULL 
)
 AS 
 BEGIN 
 
 update Institute set InstituteName=@InstituteName, Place=@Place, Address=@Address, Country=@Country, Pincod=@Pincod, PhoneNo=@PhoneNo, MobNo=@MobNo, FaxNo=@FaxNo, website=@website, EmailId=@EmailId, Sector=@Sector, Logo=@Logo, LogoPath=@LogoPath, Productserial=@Productserial, GSTNo=@GSTNo, State=@State, StateCode=@StateCode, BankName=@BankName, BankBranch=@BankBranch, IFSC=@IFSC, AccNo=@AccNo, LocalName=@LocalName, CompanyId=@CompanyId  where InstituteId=@InstituteId
 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateItemNote]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateItemNote]
(
@Id int=NULL, @Note nvarchar(200)=NULL, @IsMain bit=NULL, @FieldOne nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 update ItemNote set Note=@Note, IsMain=@IsMain, FieldOne=@FieldOne  where Id=@Id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateKOTPrinter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateKOTPrinter]
(
@Id int=NULL, @KOTName nvarchar(100)=NULL, @PrinterName nvarchar(100)=NULL, @ConnectionType nvarchar(100)=NULL, @IPaddress nvarchar(100)=NULL, @Mode nvarchar(100)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update KOTPrinter set KOTName=@KOTName, PrinterName=@PrinterName, ConnectionType=@ConnectionType, IPaddress=@IPaddress, Mode=@Mode, FK_BranchId=@FK_BranchId  where Id=@Id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateManufactureDtls]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateManufactureDtls]
(
@Id int=NULL, @FK_ManufactureHdrId int=NULL, @FK_ProductId int=NULL, @Quantity decimal(24,8)=NULL, @FK_UnitId int=NULL, @PurchasePrice decimal(24,8)=NULL, @SalesePrice decimal(24,8)=NULL, @RawCost decimal(24,8)=NULL, @Expenses decimal(24,8)=NULL, @Profit decimal(24,8)=NULL, @FinalAmount decimal(24,8)=NULL, @NewPurchaseRate decimal(24,8)=NULL, @NewSalesRate decimal(24,8)=NULL, @FK_BatchId int=NULL 
)
 AS 
 BEGIN 
 
 update ManufactureDtls set FK_ManufactureHdrId=@FK_ManufactureHdrId, FK_ProductId=@FK_ProductId, Quantity=@Quantity, FK_UnitId=@FK_UnitId, PurchasePrice=@PurchasePrice, SalesePrice=@SalesePrice, RawCost=@RawCost, Expenses=@Expenses, Profit=@Profit, FinalAmount=@FinalAmount, NewPurchaseRate=@NewPurchaseRate, NewSalesRate=@NewSalesRate, FK_BatchId=@FK_BatchId  where Id=@Id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateManufactureHdr]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateManufactureHdr]
(
@Id int=NULL, @InvNo int=NULL, @Date datetime=NULL, @FK_GodownId int=NULL, @Remarks nvarchar(400)=NULL, @FK_UserId int=NULL, @EntryDate datetime=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update ManufactureHdr set InvNo=@InvNo, Date=@Date, FK_GodownId=@FK_GodownId, Remarks=@Remarks, FK_UserId=@FK_UserId, EntryDate=@EntryDate, FK_BranchId=@FK_BranchId  where Id=@Id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateManufacturingExp]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateManufacturingExp]
(
@Id int=NULL, @FK_ExpenseAccountId int=NULL, @FK_PaymentAccountId int=NULL, @Date datetime=NULL, @Amount decimal(24,8)=NULL, @FK_ManufactureHdrId int=NULL 
)
 AS 
 BEGIN 
 
 update ManufacturingExp set FK_ExpenseAccountId=@FK_ExpenseAccountId, FK_PaymentAccountId=@FK_PaymentAccountId, Date=@Date, Amount=@Amount, FK_ManufactureHdrId=@FK_ManufactureHdrId  where Id=@Id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateMasterValue]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateMasterValue]
(
@Id int=NULL, @Name nvarchar(50)=NULL, @Description nvarchar(100)=NULL, @MasterType nvarchar(50)=NULL 
)
 AS 
 BEGIN 
 
 update MasterValue set Name=@Name, Description=@Description, MasterType=@MasterType  where Id=@Id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateMenuDetails]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateMenuDetails]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @MenuCaption nvarchar(100)=NULL, @ParentId int=NULL, @AssemblyName nvarchar(100)=NULL, @ClassName nvarchar(100)=NULL, @IsForm bit=NULL, @Icon image=NULL, @FK_ModuleId int=NULL, @IsDialog bit=NULL, @Status bit=NULL, @Position int=NULL, @IsCrudMenu bit=NULL, @MenuUrl nvarchar(50)=NULL, @IsFavorite bit=NULL 
)
 AS 
 BEGIN 
 
 update MenuDetails set Name=@Name, MenuCaption=@MenuCaption, ParentId=@ParentId, AssemblyName=@AssemblyName, ClassName=@ClassName, IsForm=@IsForm, Icon=@Icon, FK_ModuleId=@FK_ModuleId, IsDialog=@IsDialog, Status=@Status, Position=@Position, IsCrudMenu=@IsCrudMenu, MenuUrl=@MenuUrl, IsFavorite=@IsFavorite  where Id=@Id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateModules]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateModules]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @Description nvarchar(1000)=NULL, @Status bit=NULL 
)
 AS 
 BEGIN 
 
 update Modules set Name=@Name, Description=@Description, Status=@Status  where Id=@Id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdatePOSHoldMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdatePOSHoldMaster]
(
@Id int=NULL, @SalesInvNo int=NULL, @InvType nvarchar(50)=NULL, @SalesDate datetime=NULL, @SalesAccountId int=NULL, @PartyAccountId int=NULL, @RouteId int=NULL, @VehId int=NULL, @StafId int=NULL, @PriceCategoryId int=NULL, @AditionalAmount decimal(18,4)=NULL, @Frieght decimal(18,4)=NULL, @TotalDiscount decimal(18,4)=NULL, @BillDiscount decimal(18,4)=NULL, @GrandDiscount decimal(18,4)=NULL, @JEId int=NULL, @EntryDate datetime=NULL, @Remarks nvarchar(50)=NULL, @IGSTAmount decimal(18,4)=NULL, @SGSTAmount decimal(18,4)=NULL, @CGSTAmount decimal(18,4)=NULL, @TaxableAmount decimal(18,4)=NULL, @FYearID int=NULL, @FloodCess decimal(18,4)=NULL, @TotalTaxAmount decimal(18,4)=NULL, @FK_BranchId int=NULL, @CntrInvNo nvarchar(10)=NULL, @SequenceNo int=NULL, @CashPayment decimal(24,8)=NULL, @CardPayment decimal(24,8)=NULL, @Credit decimal(24,8)=NULL, @FielOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @Terminal nvarchar(25)=NULL, @IsClosed bit=NULL, @FreeAmount decimal(24,4)=NULL, @ReturnAmount decimal(24,4)=NULL, @OtherPayment decimal(24,4)=NULL, @CashAccountId int=NULL, @CardAccountId int=NULL, @OnlineAccountId int=NULL, @IsSettiled bit=NULL, @TotalPayment decimal(24,8)=NULL, @VoucherNumber nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update POSHoldMaster set SalesInvNo=@SalesInvNo, InvType=@InvType, SalesDate=@SalesDate, SalesAccountId=@SalesAccountId, PartyAccountId=@PartyAccountId, RouteId=@RouteId, VehId=@VehId, StafId=@StafId, PriceCategoryId=@PriceCategoryId, AditionalAmount=@AditionalAmount, Frieght=@Frieght, TotalDiscount=@TotalDiscount, BillDiscount=@BillDiscount, GrandDiscount=@GrandDiscount, JEId=@JEId, EntryDate=@EntryDate, Remarks=@Remarks, IGSTAmount=@IGSTAmount, SGSTAmount=@SGSTAmount, CGSTAmount=@CGSTAmount, TaxableAmount=@TaxableAmount, FYearID=@FYearID, FloodCess=@FloodCess, TotalTaxAmount=@TotalTaxAmount, FK_BranchId=@FK_BranchId, CntrInvNo=@CntrInvNo, SequenceNo=@SequenceNo, CashPayment=@CashPayment, CardPayment=@CardPayment, Credit=@Credit, FielOne=@FielOne, FieldTwo=@FieldTwo, FieldThree=@FieldThree, FieldFour=@FieldFour, Terminal=@Terminal, IsClosed=@IsClosed, FreeAmount=@FreeAmount, ReturnAmount=@ReturnAmount, OtherPayment=@OtherPayment, CashAccountId=@CashAccountId, CardAccountId=@CardAccountId, OnlineAccountId=@OnlineAccountId, IsSettiled=@IsSettiled, TotalPayment=@TotalPayment, VoucherNumber=@VoucherNumber  where Id=@Id
 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spUpdatePOSHoldSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdatePOSHoldSettings]
(
@Id int=NULL, @SalesInvId int=NULL, @PartyAccountId int=NULL, @ProductId int=NULL, @ProQty decimal(18,4)=NULL, @UnitId int=NULL, @ProRate decimal(18,4)=NULL, @ProDiscont decimal(18,4)=NULL, @DiscAmnt decimal(18,4)=NULL, @GrossAmnt decimal(18,4)=NULL, @NetAmnt decimal(18,4)=NULL, @Date datetime=NULL, @HSN int=NULL, @GST decimal(18,4)=NULL, @GSTAMOUNT decimal(18,4)=NULL, @SGST decimal(18,4)=NULL, @SGSTAmount decimal(18,4)=NULL, @CGST decimal(18,4)=NULL, @CGSTAmount decimal(18,4)=NULL, @IGST decimal(18,4)=NULL, @IGSTAMOUNT decimal(18,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(20)=NULL 
)
 AS 
 BEGIN 
 
 update POSHoldSettings set SalesInvId=@SalesInvId, PartyAccountId=@PartyAccountId, ProductId=@ProductId, ProQty=@ProQty, UnitId=@UnitId, ProRate=@ProRate, ProDiscont=@ProDiscont, DiscAmnt=@DiscAmnt, GrossAmnt=@GrossAmnt, NetAmnt=@NetAmnt, Date=@Date, HSN=@HSN, GST=@GST, GSTAMOUNT=@GSTAMOUNT, SGST=@SGST, SGSTAmount=@SGSTAmount, CGST=@CGST, CGSTAmount=@CGSTAmount, IGST=@IGST, IGSTAMOUNT=@IGSTAMOUNT, FK_BatchId=@FK_BatchId, Barcode=@Barcode  where Id=@Id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdatePrevilegeCard]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdatePrevilegeCard]
(
@PrevilageCardId int=NULL, @ConsumerCode nvarchar(50)=NULL, @CustomerId int=NULL, @PriceCategoryId int=NULL, @NomineeAccountId int=NULL, @ExpairyDate datetime=NULL, @RenewalDate datetime=NULL, @DOBofCustomer datetime=NULL, @Remarks nvarchar(400)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update PrevilegeCard set ConsumerCode=@ConsumerCode, CustomerId=@CustomerId, PriceCategoryId=@PriceCategoryId, NomineeAccountId=@NomineeAccountId, ExpairyDate=@ExpairyDate, RenewalDate=@RenewalDate, DOBofCustomer=@DOBofCustomer, Remarks=@Remarks, FK_BranchId=@FK_BranchId  where PrevilageCardId=@PrevilageCardId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdatePriceCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdatePriceCategory]
(
@PCategoryId int=NULL, @PCategoryName nvarchar(50)=NULL, @Percentage decimal(18,2)=NULL, @SDiscountPercentage decimal(18,2)=NULL 
)
 AS 
 BEGIN 
 
 update PriceCategory set PCategoryName=@PCategoryName, Percentage=@Percentage, SDiscountPercentage=@SDiscountPercentage  where PCategoryId=@PCategoryId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdatePrintDesignMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdatePrintDesignMaster]
(
@printMasterId int=NULL, @DesignName nvarchar(50)=NULL, @VochrType nvarchar(50)=NULL, @isDefault bit=NULL, @isLandScape bit=NULL, @PageHieght int=NULL, @PageWidth int=NULL, @LinesPerPage int=NULL, @SpaceBtwnLines int=NULL, @BackgroundImgPath nvarchar(300)=NULL, @PrinterName nvarchar(50)=NULL, @PrintType nvarchar(50)=NULL, @upScroll int=NULL, @downScroll int=NULL, @Riverse nvarchar(10)=NULL, @Bold_On nvarchar(10)=NULL, @Bold_Off nvarchar(10)=NULL, @Pitch nvarchar(10)=NULL, @FormType nvarchar(50)=NULL, @SpaceInLines int=NULL 
)
 AS 
 BEGIN 
 
 update PrintDesignMaster set DesignName=@DesignName, VochrType=@VochrType, isDefault=@isDefault, isLandScape=@isLandScape, PageHieght=@PageHieght, PageWidth=@PageWidth, LinesPerPage=@LinesPerPage, SpaceBtwnLines=@SpaceBtwnLines, BackgroundImgPath=@BackgroundImgPath, PrinterName=@PrinterName, PrintType=@PrintType, upScroll=@upScroll, downScroll=@downScroll, Riverse=@Riverse, Bold_On=@Bold_On, Bold_Off=@Bold_Off, Pitch=@Pitch, FormType=@FormType, SpaceInLines=@SpaceInLines  where printMasterId=@printMasterId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdatePrintDesignSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdatePrintDesignSettings]
(
@PrintDesignSettingsID int=NULL, @printMasterId int=NULL, @ControleName nvarchar(50)=NULL, @Type nvarchar(20)=NULL, @FeildText nvarchar(500)=NULL, @X int=NULL, @Y int=NULL, @FontSize int=NULL, @Font nvarchar(50)=NULL, @isBold bit=NULL, @width int=NULL, @Alaingmnet nvarchar(10)=NULL, @Position nvarchar(25)=NULL, @TabIndex int=NULL, @iIndex int=NULL, @Height int=NULL, @Spec nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 update PrintDesignSettings set printMasterId=@printMasterId, ControleName=@ControleName, Type=@Type, FeildText=@FeildText, X=@X, Y=@Y, FontSize=@FontSize, Font=@Font, isBold=@isBold, width=@width, Alaingmnet=@Alaingmnet, Position=@Position, TabIndex=@TabIndex, iIndex=@iIndex, Height=@Height, Spec=@Spec  where PrintDesignSettingsID=@PrintDesignSettingsID
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateProCombMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateProCombMaster]
(
@ProCombMasterId int=NULL, @CombProName nvarchar(50)=NULL, @ProCombVchrNo int=NULL, @Date datetime=NULL, @ProQty decimal(24,4)=NULL, @UnitId int=NULL, @ProSaleRate decimal(24,4)=NULL, @ProPurRate decimal(24,4)=NULL, @MRP decimal(24,4)=NULL, @CombType nvarchar(50)=NULL, @Barcode nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update ProCombMaster set CombProName=@CombProName, ProCombVchrNo=@ProCombVchrNo, Date=@Date, ProQty=@ProQty, UnitId=@UnitId, ProSaleRate=@ProSaleRate, ProPurRate=@ProPurRate, MRP=@MRP, CombType=@CombType, Barcode=@Barcode  where ProCombMasterId=@ProCombMasterId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateProCombSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateProCombSettings]
(
@ProCombSettingsId int=NULL, @ProCombMasterId int=NULL, @ProductId int=NULL, @ProQty decimal(24,4)=NULL, @UnitId int=NULL, @SaleRate decimal(24,4)=NULL, @PurRate decimal(24,4)=NULL, @MRP decimal(24,4)=NULL, @Barcode nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update ProCombSettings set ProCombMasterId=@ProCombMasterId, ProductId=@ProductId, ProQty=@ProQty, UnitId=@UnitId, SaleRate=@SaleRate, PurRate=@PurRate, MRP=@MRP, Barcode=@Barcode  where ProCombSettingsId=@ProCombSettingsId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateProduct]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateProduct]
(
@ProductId int=NULL, @ProductCode int=NULL, @ProductName nvarchar(120)=NULL, @Specification nvarchar(50)=NULL, @PGroupId int=NULL, @UnitId int=NULL, @BrandId int=NULL, @TCategoryId int=NULL, @HSNNo nvarchar(10)=NULL, @PurchasePrice decimal(24,4)=NULL, @SellingPrice decimal(24,4)=NULL, @MRP decimal(24,4)=NULL, @OpeningStock decimal(24,4)=NULL, @OppStockDate datetime=NULL, @ReOrderStock decimal(24,4)=NULL, @AccountId int=NULL, @GodwnId int=NULL, @SelfId int=NULL, @PurchaseDis decimal(24,4)=NULL, @SaleDis decimal(24,4)=NULL, @AliasCode nvarchar(50)=NULL, @AliasName nvarchar(120)=NULL, @Remarks nvarchar(500)=NULL, @LocalName nvarchar(500)=NULL, @PurchaseUnit int=NULL, @SalesUnit int=NULL, @ReportUnit int=NULL, @image image=NULL, @filePath nvarchar(400)=NULL, @description nvarchar(5)=NULL, @ProductType nvarchar(400)=NULL, @isActive bit=NULL, @isInclusive bit=NULL, @Barcode nvarchar(20)=NULL, @IsExpairing bit=NULL, @ExpairingDays decimal(24,4)=NULL, @MinimumMargin decimal(24,4)=NULL, @NormalMargin decimal(24,4)=NULL, @RetailMargin decimal(24,4)=NULL, @WholesaleMargin decimal(24,4)=NULL, @MRPMargin decimal(24,4)=NULL, @IsMarginBasedPercentage bit=NULL, @LoyalityPont int=NULL, @AgentCommision decimal(24,4)=NULL, @SalesmanCommision decimal(24,4)=NULL, @IsCommisionBasedPercentage bit=NULL, @FieldOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @FieldFive nvarchar(100)=NULL, @IsScale bit=NULL, @ListOrder int=NULL, @IsAllowMultiple bit=NULL, @isRateBasedWeight bit=NULL, @PriorityStatus nvarchar(25)=NULL, @CookingTime int=NULL, @IsFavorite bit=NULL, @RestaurantItemType nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update Product set ProductCode=@ProductCode, ProductName=@ProductName, Specification=@Specification, PGroupId=@PGroupId, UnitId=@UnitId, BrandId=@BrandId, TCategoryId=@TCategoryId, HSNNo=@HSNNo, PurchasePrice=@PurchasePrice, SellingPrice=@SellingPrice, MRP=@MRP, OpeningStock=@OpeningStock, OppStockDate=@OppStockDate, ReOrderStock=@ReOrderStock, AccountId=@AccountId, GodwnId=@GodwnId, SelfId=@SelfId, PurchaseDis=@PurchaseDis, SaleDis=@SaleDis, AliasCode=@AliasCode, AliasName=@AliasName, Remarks=@Remarks, LocalName=@LocalName, PurchaseUnit=@PurchaseUnit, SalesUnit=@SalesUnit, ReportUnit=@ReportUnit, image=@image, filePath=@filePath, description=@description, ProductType=@ProductType, isActive=@isActive, isInclusive=@isInclusive, Barcode=@Barcode, IsExpairing=@IsExpairing, ExpairingDays=@ExpairingDays, MinimumMargin=@MinimumMargin, NormalMargin=@NormalMargin, RetailMargin=@RetailMargin, WholesaleMargin=@WholesaleMargin, MRPMargin=@MRPMargin, IsMarginBasedPercentage=@IsMarginBasedPercentage, LoyalityPont=@LoyalityPont, AgentCommision=@AgentCommision, SalesmanCommision=@SalesmanCommision, IsCommisionBasedPercentage=@IsCommisionBasedPercentage, FieldOne=@FieldOne, FieldTwo=@FieldTwo, FieldThree=@FieldThree, FieldFour=@FieldFour, FieldFive=@FieldFive, IsScale=@IsScale, ListOrder=@ListOrder, IsAllowMultiple=@IsAllowMultiple, isRateBasedWeight=@isRateBasedWeight, PriorityStatus=@PriorityStatus, CookingTime=@CookingTime, IsFavorite=@IsFavorite, RestaurantItemType=@RestaurantItemType  where ProductId=@ProductId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateProductBarcode]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateProductBarcode]
(
@Id int=NULL, @Barcode nvarchar(100)=NULL, @BarcodeType nvarchar(100)=NULL, @ExpairyDate datetime=NULL, @ManufDate datetime=NULL, @FK_ProductId int=NULL, @FK_UnitId int=NULL, @FK_BatchId int=NULL, @FK_BranchId int=NULL, @Remarks nvarchar(100)=NULL, @PurchasePrice decimal(24,4)=NULL, @SellingPrice decimal(24,4)=NULL, @MRP decimal(24,4)=NULL, @ProductCost decimal(24,4)=NULL, @FieldOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @MinimuPrice decimal(24,4)=NULL, @RetailPrice decimal(24,4)=NULL, @WholesalePrice decimal(24,4)=NULL, @ProductBarcodeName nvarchar(50)=NULL, @LocalName nvarchar(50)=NULL, @image image=NULL 
)
 AS 
 BEGIN 
 
 update ProductBarcode set Barcode=@Barcode, BarcodeType=@BarcodeType, ExpairyDate=@ExpairyDate, ManufDate=@ManufDate, FK_ProductId=@FK_ProductId, FK_UnitId=@FK_UnitId, FK_BatchId=@FK_BatchId, FK_BranchId=@FK_BranchId, Remarks=@Remarks, PurchasePrice=@PurchasePrice, SellingPrice=@SellingPrice, MRP=@MRP, ProductCost=@ProductCost, FieldOne=@FieldOne, FieldTwo=@FieldTwo, FieldThree=@FieldThree, FieldFour=@FieldFour, MinimuPrice=@MinimuPrice, RetailPrice=@RetailPrice, WholesalePrice=@WholesalePrice, ProductBarcodeName=@ProductBarcodeName, LocalName=@LocalName, image=@image  where Id=@Id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateProductBarcodeScale]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateProductBarcodeScale]
(
@Id int=NULL, @FK_ProductId int=NULL, @FK_BarcodeScale int=NULL 
)
 AS 
 BEGIN 
 
 update ProductBarcodeScale set FK_ProductId=@FK_ProductId, FK_BarcodeScale=@FK_BarcodeScale  where Id=@Id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateProductGroup]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateProductGroup]
(
@PGroupId int=NULL, @PGroupName nvarchar(50)=NULL, @Under nvarchar(50)=NULL, @SalesInsentive decimal(24,4)=NULL, @SalesDis decimal(24,4)=NULL, @PurchDis decimal(24,4)=NULL, @Remarks nvarchar(1000)=NULL, @ListOrder int=NULL, @isShowInGrid bit=NULL 
)
 AS 
 BEGIN 
 
 update ProductGroup set PGroupName=@PGroupName, Under=@Under, SalesInsentive=@SalesInsentive, SalesDis=@SalesDis, PurchDis=@PurchDis, Remarks=@Remarks, ListOrder=@ListOrder, isShowInGrid=@isShowInGrid  where PGroupId=@PGroupId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateProductPriceCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateProductPriceCategory]
(
@ProPrceCatId int=NULL, @ProductId int=NULL, @PCategoryId int=NULL, @UnitId int=NULL, @PGroupId int=NULL, @SPrice decimal(24,4)=NULL, @SPDisc decimal(24,4)=NULL, @PPrice decimal(24,4)=NULL, @PDisc decimal(24,4)=NULL, @Barcode nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update ProductPriceCategory set ProductId=@ProductId, PCategoryId=@PCategoryId, UnitId=@UnitId, PGroupId=@PGroupId, SPrice=@SPrice, SPDisc=@SPDisc, PPrice=@PPrice, PDisc=@PDisc, Barcode=@Barcode  where ProPrceCatId=@ProPrceCatId
 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateProductPrinter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateProductPrinter]
(
@Id int=NULL, @FK_KOTPrinterId int=NULL, @FK_ProductId int=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update ProductPrinter set FK_KOTPrinterId=@FK_KOTPrinterId, FK_ProductId=@FK_ProductId, FK_BranchId=@FK_BranchId  where Id=@Id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateProductUnit]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateProductUnit]
(
@ProUnitId int=NULL, @ProductId int=NULL, @UnitId int=NULL, @UnitType nvarchar(100)=NULL, @Barcode nvarchar(20)=NULL, @UnitQty decimal(24,8)=NULL, @ProductUnitName nvarchar(100)=NULL, @IsInclusive bit=NULL, @OpeningStock decimal(24,8)=NULL, @SalesPrice decimal(24,8)=NULL 
)
 AS 
 BEGIN 
 
 update ProductUnit set ProductId=@ProductId, UnitId=@UnitId, UnitType=@UnitType, Barcode=@Barcode, UnitQty=@UnitQty, ProductUnitName=@ProductUnitName, IsInclusive=@IsInclusive, OpeningStock=@OpeningStock, SalesPrice=@SalesPrice  where ProUnitId=@ProUnitId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdatePurInvMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdatePurInvMaster]
(
@PurInvId int=NULL, @PurInvNo int=NULL, @InvType nvarchar(50)=NULL, @PurDate datetime=NULL, @PurActId int=NULL, @PartyAccId int=NULL, @PriceCategoryId int=NULL, @AdditionalAmnt decimal(24,4)=NULL, @Frieght decimal(24,4)=NULL, @TotalDiscount decimal(24,4)=NULL, @BillDisc decimal(24,4)=NULL, @TotalTax decimal(24,4)=NULL, @TotalAmnt decimal(24,4)=NULL, @JEId int=NULL, @EntryDate datetime=NULL, @Remarks nvarchar(250)=NULL, @IGSTAmount decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @RefInvNo nvarchar(50)=NULL, @Taxable decimal(24,4)=NULL, @FK_BranchId int=NULL, @CashPayment decimal(24,4)=NULL, @CardPayment decimal(24,4)=NULL, @Credit decimal(24,4)=NULL, @FreeAmount decimal(24,4)=NULL, @ReturnAmount decimal(24,4)=NULL, @IsClosed bit=NULL, @FielOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @CashAccountId int=NULL, @CardAccountId int=NULL, @OnlineAccountId int=NULL, @IsSettiled bit=NULL, @TotalPayment decimal(24,8)=NULL, @VoucherNumber nvarchar(25)=NULL, @SequenceNo int=NULL, @Terminal nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update PurInvMaster set PurInvNo=@PurInvNo, InvType=@InvType, PurDate=@PurDate, PurActId=@PurActId, PartyAccId=@PartyAccId, PriceCategoryId=@PriceCategoryId, AdditionalAmnt=@AdditionalAmnt, Frieght=@Frieght, TotalDiscount=@TotalDiscount, BillDisc=@BillDisc, TotalTax=@TotalTax, TotalAmnt=@TotalAmnt, JEId=@JEId, EntryDate=@EntryDate, Remarks=@Remarks, IGSTAmount=@IGSTAmount, SGSTAmount=@SGSTAmount, CGSTAmount=@CGSTAmount, RefInvNo=@RefInvNo, Taxable=@Taxable, FK_BranchId=@FK_BranchId, CashPayment=@CashPayment, CardPayment=@CardPayment, Credit=@Credit, FreeAmount=@FreeAmount, ReturnAmount=@ReturnAmount, IsClosed=@IsClosed, FielOne=@FielOne, FieldTwo=@FieldTwo, FieldThree=@FieldThree, FieldFour=@FieldFour, CashAccountId=@CashAccountId, CardAccountId=@CardAccountId, OnlineAccountId=@OnlineAccountId, IsSettiled=@IsSettiled, TotalPayment=@TotalPayment, VoucherNumber=@VoucherNumber, SequenceNo=@SequenceNo, Terminal=@Terminal  where PurInvId=@PurInvId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdatePurInvSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdatePurInvSettings]
(
@PurSettingsId int=NULL, @PurInvId int=NULL, @PartyAccId int=NULL, @ProId int=NULL, @ProQty decimal(18,4)=NULL, @UnitId int=NULL, @ProRate decimal(18,4)=NULL, @Disc decimal(18,4)=NULL, @DiscAmnt decimal(18,4)=NULL, @Gross decimal(18,4)=NULL, @NetAmnt decimal(18,4)=NULL, @Date datetime=NULL, @HSN int=NULL, @GST decimal(18,4)=NULL, @GSTAMOUNT decimal(18,4)=NULL, @SGST decimal(18,4)=NULL, @SGSTAmount decimal(18,4)=NULL, @CGST decimal(18,4)=NULL, @CGSTAmount decimal(18,4)=NULL, @IGST decimal(18,4)=NULL, @IGSTAMOUNT decimal(18,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 update PurInvSettings set PurInvId=@PurInvId, PartyAccId=@PartyAccId, ProId=@ProId, ProQty=@ProQty, UnitId=@UnitId, ProRate=@ProRate, Disc=@Disc, DiscAmnt=@DiscAmnt, Gross=@Gross, NetAmnt=@NetAmnt, Date=@Date, HSN=@HSN, GST=@GST, GSTAMOUNT=@GSTAMOUNT, SGST=@SGST, SGSTAmount=@SGSTAmount, CGST=@CGST, CGSTAmount=@CGSTAmount, IGST=@IGST, IGSTAMOUNT=@IGSTAMOUNT, FK_BatchId=@FK_BatchId, Barcode=@Barcode  where PurSettingsId=@PurSettingsId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdatePurRtnInvMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdatePurRtnInvMaster]
(
@PurRtnInvId int=NULL, @PurRtnInvNo int=NULL, @PurRtnDate datetime=NULL, @PurRtnAcId int=NULL, @PartyAcId int=NULL, @PriceCategoryId int=NULL, @AdditionalAmnt decimal(24,4)=NULL, @Frieght decimal(24,4)=NULL, @TotalDiscount decimal(24,4)=NULL, @BillDisc decimal(24,4)=NULL, @TotalTax decimal(24,4)=NULL, @TotalAmnt decimal(24,4)=NULL, @JEId int=NULL, @EntryDate datetime=NULL, @Remarks nvarchar(400)=NULL, @IGSTAmount decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @Taxable decimal(24,4)=NULL, @FK_BranchId int=NULL, @SequenceNo int=NULL, @Terminal nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update PurRtnInvMaster set PurRtnInvNo=@PurRtnInvNo, PurRtnDate=@PurRtnDate, PurRtnAcId=@PurRtnAcId, PartyAcId=@PartyAcId, PriceCategoryId=@PriceCategoryId, AdditionalAmnt=@AdditionalAmnt, Frieght=@Frieght, TotalDiscount=@TotalDiscount, BillDisc=@BillDisc, TotalTax=@TotalTax, TotalAmnt=@TotalAmnt, JEId=@JEId, EntryDate=@EntryDate, Remarks=@Remarks, IGSTAmount=@IGSTAmount, SGSTAmount=@SGSTAmount, CGSTAmount=@CGSTAmount, Taxable=@Taxable, FK_BranchId=@FK_BranchId, SequenceNo=@SequenceNo, Terminal=@Terminal  where PurRtnInvId=@PurRtnInvId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdatePurRtnInvSttings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdatePurRtnInvSttings]
(
@PurRtnSettId int=NULL, @PurRtnInvId int=NULL, @ProId int=NULL, @ProQty decimal(24,4)=NULL, @UnitId int=NULL, @ProRate decimal(24,4)=NULL, @Disc decimal(24,4)=NULL, @DiscAmnt decimal(24,4)=NULL, @Gross decimal(24,4)=NULL, @Tax decimal(24,4)=NULL, @TaxAmnt decimal(24,4)=NULL, @NetAmnt decimal(24,4)=NULL, @PartyAccId int=NULL, @Date datetime=NULL, @HSN nvarchar(20)=NULL, @GST decimal(24,4)=NULL, @GSTAMOUNT decimal(24,4)=NULL, @SGST decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGST decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @IGST decimal(24,4)=NULL, @IGSTAMOUNT decimal(24,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 update PurRtnInvSttings set PurRtnInvId=@PurRtnInvId, ProId=@ProId, ProQty=@ProQty, UnitId=@UnitId, ProRate=@ProRate, Disc=@Disc, DiscAmnt=@DiscAmnt, Gross=@Gross, Tax=@Tax, TaxAmnt=@TaxAmnt, NetAmnt=@NetAmnt, PartyAccId=@PartyAccId, Date=@Date, HSN=@HSN, GST=@GST, GSTAMOUNT=@GSTAMOUNT, SGST=@SGST, SGSTAmount=@SGSTAmount, CGST=@CGST, CGSTAmount=@CGSTAmount, IGST=@IGST, IGSTAMOUNT=@IGSTAMOUNT, FK_BatchId=@FK_BatchId, Barcode=@Barcode  where PurRtnSettId=@PurRtnSettId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateRateFormat]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateRateFormat]
(
@Id int=NULL, @Name int=NULL, @Value nvarchar(1)=NULL, @Alternative nvarchar(1)=NULL 
)
 AS 
 BEGIN 
 
 update RateFormat set Name=@Name, Value=@Value, Alternative=@Alternative  where Id=@Id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateRawmaterialMapping]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateRawmaterialMapping]
(
@Id int=NULL, @FK_ProductId int=NULL, @Quantity decimal(24,8)=NULL, @FK_UnitId int=NULL, @Rate decimal(24,8)=NULL, @NetAmount decimal(24,8)=NULL, @FK_ManufactureHdrId int=NULL, @FK_BatchId int=NULL 
)
 AS 
 BEGIN 
 
 update RawmaterialMapping set FK_ProductId=@FK_ProductId, Quantity=@Quantity, FK_UnitId=@FK_UnitId, Rate=@Rate, NetAmount=@NetAmount, FK_ManufactureHdrId=@FK_ManufactureHdrId, FK_BatchId=@FK_BatchId  where Id=@Id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateReceiptAndPayment]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateReceiptAndPayment]
(
@Id int=NULL, @IsSettiled bit=NULL, @CntrInvNo nvarchar(15)=NULL, @SalesDate datetime=NULL, @GrandDiscount decimal(24,4)=NULL, @TotalPayment decimal(24,4)=NULL, @Credit decimal(24,4)=NULL, @Payment decimal(24,4)=NULL, @Balance decimal(24,4)=NULL, @VchrNo nvarchar(15)=NULL, @InvoiceId int=NULL, @VchrId int=NULL, @InvoiceType nvarchar(20)=NULL 
)
 AS 
 BEGIN 
 
 update ReceiptAndPayment set IsSettiled=@IsSettiled, CntrInvNo=@CntrInvNo, SalesDate=@SalesDate, GrandDiscount=@GrandDiscount, TotalPayment=@TotalPayment, Credit=@Credit, Payment=@Payment, Balance=@Balance, VchrNo=@VchrNo, InvoiceId=@InvoiceId, VchrId=@VchrId, InvoiceType=@InvoiceType  where Id=@Id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateReminderTB]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateReminderTB]
(
@ReminderId int=NULL, @RemainderHead nvarchar(50)=NULL, @Remainder nvarchar(400)=NULL, @CurrentDate nvarchar(20)=NULL, @ReminderDate nvarchar(20)=NULL, @UserId int=NULL, @TargetUserId int=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update ReminderTB set RemainderHead=@RemainderHead, Remainder=@Remainder, CurrentDate=@CurrentDate, ReminderDate=@ReminderDate, UserId=@UserId, TargetUserId=@TargetUserId, FK_BranchId=@FK_BranchId  where ReminderId=@ReminderId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateRestaurantTable]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateRestaurantTable]
(
@TableId int=NULL, @TableName nvarchar(100)=NULL, @ChairCount int=NULL, @FloorName nvarchar(100)=NULL, @XPosition int=NULL, @YPostion int=NULL, @Image image=NULL, @FK_EmplyeeId int=NULL 
)
 AS 
 BEGIN 
 
 update RestaurantTable set TableName=@TableName, ChairCount=@ChairCount, FloorName=@FloorName, XPosition=@XPosition, YPostion=@YPostion, Image=@Image, FK_EmplyeeId=@FK_EmplyeeId  where TableId=@TableId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateRoot]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateRoot]
(
@RootId int=NULL, @RootName nvarchar(50)=NULL, @WeekDay nvarchar(20)=NULL, @SalesManId int=NULL, @DriverId int=NULL, @VehicleId int=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update Root set RootName=@RootName, WeekDay=@WeekDay, SalesManId=@SalesManId, DriverId=@DriverId, VehicleId=@VehicleId, FK_BranchId=@FK_BranchId  where RootId=@RootId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateSalaryProcessMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateSalaryProcessMaster]
(
@SPrcsMasterID int=NULL, @SPrcsVchrNo int=NULL, @SPrcsDate datetime=NULL, @SlryPrd nvarchar(50)=NULL, @DateFrom datetime=NULL, @DateTo datetime=NULL, @StaffType nvarchar(50)=NULL, @Description nvarchar(250)=NULL, @CashOrBankAcctId int=NULL, @CashOrBankAcctName nvarchar(50)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update SalaryProcessMaster set SPrcsVchrNo=@SPrcsVchrNo, SPrcsDate=@SPrcsDate, SlryPrd=@SlryPrd, DateFrom=@DateFrom, DateTo=@DateTo, StaffType=@StaffType, Description=@Description, CashOrBankAcctId=@CashOrBankAcctId, CashOrBankAcctName=@CashOrBankAcctName, FK_BranchId=@FK_BranchId  where SPrcsMasterID=@SPrcsMasterID
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateSalesCounter]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateSalesCounter]
(
@SalesCounterId int=NULL, @SalesCounterName nvarchar(50)=NULL, @CashAccountId int=NULL, @Remarks nvarchar(400)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update SalesCounter set SalesCounterName=@SalesCounterName, CashAccountId=@CashAccountId, Remarks=@Remarks, FK_BranchId=@FK_BranchId  where SalesCounterId=@SalesCounterId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateSalesEstimateMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateSalesEstimateMaster]
(
@SalesInvId int=NULL, @SalesInvNo int=NULL, @InvType nvarchar(50)=NULL, @SalesDate datetime=NULL, @SalesAccountId int=NULL, @PartyAccountId int=NULL, @RouteId int=NULL, @VehId int=NULL, @StafId int=NULL, @PriceCategoryId int=NULL, @AditionalAmount decimal(24,4)=NULL, @Frieght decimal(24,4)=NULL, @TotalDiscount decimal(24,4)=NULL, @BillDiscount decimal(24,4)=NULL, @GrandDiscount decimal(24,4)=NULL, @JEId int=NULL, @EntryDate datetime=NULL, @Remarks nvarchar(50)=NULL, @IGSTAmount decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @TaxableAmount decimal(24,4)=NULL, @FYearID int=NULL, @FloodCess decimal(24,4)=NULL, @TotalTaxAmount decimal(24,4)=NULL, @FK_BranchId int=NULL, @CntrInvNo nvarchar(10)=NULL, @SequenceNo int=NULL, @CashPayment decimal(24,8)=NULL, @CardPayment decimal(24,8)=NULL, @Credit decimal(24,8)=NULL, @FielOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @Terminal nvarchar(25)=NULL, @IsClosed bit=NULL, @FreeAmount decimal(24,4)=NULL, @ReturnAmount decimal(24,4)=NULL, @OtherPayment decimal(24,4)=NULL, @CashAccountId int=NULL, @CardAccountId int=NULL, @OnlineAccountId int=NULL, @IsSettiled bit=NULL, @TotalPayment decimal(24,8)=NULL, @VoucherNumber nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update SalesEstimateMaster set SalesInvNo=@SalesInvNo, InvType=@InvType, SalesDate=@SalesDate, SalesAccountId=@SalesAccountId, PartyAccountId=@PartyAccountId, RouteId=@RouteId, VehId=@VehId, StafId=@StafId, PriceCategoryId=@PriceCategoryId, AditionalAmount=@AditionalAmount, Frieght=@Frieght, TotalDiscount=@TotalDiscount, BillDiscount=@BillDiscount, GrandDiscount=@GrandDiscount, JEId=@JEId, EntryDate=@EntryDate, Remarks=@Remarks, IGSTAmount=@IGSTAmount, SGSTAmount=@SGSTAmount, CGSTAmount=@CGSTAmount, TaxableAmount=@TaxableAmount, FYearID=@FYearID, FloodCess=@FloodCess, TotalTaxAmount=@TotalTaxAmount, FK_BranchId=@FK_BranchId, CntrInvNo=@CntrInvNo, SequenceNo=@SequenceNo, CashPayment=@CashPayment, CardPayment=@CardPayment, Credit=@Credit, FielOne=@FielOne, FieldTwo=@FieldTwo, FieldThree=@FieldThree, FieldFour=@FieldFour, Terminal=@Terminal, IsClosed=@IsClosed, FreeAmount=@FreeAmount, ReturnAmount=@ReturnAmount, OtherPayment=@OtherPayment, CashAccountId=@CashAccountId, CardAccountId=@CardAccountId, OnlineAccountId=@OnlineAccountId, IsSettiled=@IsSettiled, TotalPayment=@TotalPayment, VoucherNumber=@VoucherNumber  where SalesInvId=@SalesInvId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateSalesEstimateSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateSalesEstimateSettings]
(
@SalesSettingsId int=NULL, @SalesInvId int=NULL, @PartyAccountId int=NULL, @ProductId int=NULL, @ProQty decimal(18,4)=NULL, @UnitId int=NULL, @ProRate decimal(18,4)=NULL, @ProDiscont decimal(18,4)=NULL, @DiscAmnt decimal(18,4)=NULL, @GrossAmnt decimal(18,4)=NULL, @NetAmnt decimal(18,4)=NULL, @Date datetime=NULL, @HSN nvarchar(20)=NULL, @GST decimal(18,4)=NULL, @GSTAMOUNT decimal(18,4)=NULL, @SGST decimal(18,4)=NULL, @SGSTAmount decimal(18,4)=NULL, @CGST decimal(18,4)=NULL, @CGSTAmount decimal(18,4)=NULL, @IGST decimal(18,4)=NULL, @IGSTAMOUNT decimal(18,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(20)=NULL, @ItemCalory decimal(18,4)=NULL, @EmployeeID int=NULL, @EmployeeCommision decimal(18,4)=NULL, @AgentId int=NULL, @AgentCommission decimal(18,4)=NULL, @ProductName nvarchar(50)=NULL 
)
 AS 
 BEGIN 
 
 update SalesEstimateSettings set SalesInvId=@SalesInvId, PartyAccountId=@PartyAccountId, ProductId=@ProductId, ProQty=@ProQty, UnitId=@UnitId, ProRate=@ProRate, ProDiscont=@ProDiscont, DiscAmnt=@DiscAmnt, GrossAmnt=@GrossAmnt, NetAmnt=@NetAmnt, Date=@Date, HSN=@HSN, GST=@GST, GSTAMOUNT=@GSTAMOUNT, SGST=@SGST, SGSTAmount=@SGSTAmount, CGST=@CGST, CGSTAmount=@CGSTAmount, IGST=@IGST, IGSTAMOUNT=@IGSTAMOUNT, FK_BatchId=@FK_BatchId, Barcode=@Barcode, ItemCalory=@ItemCalory, EmployeeID=@EmployeeID, EmployeeCommision=@EmployeeCommision, AgentId=@AgentId, AgentCommission=@AgentCommission, ProductName=@ProductName  where SalesSettingsId=@SalesSettingsId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateSalesOrderMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateSalesOrderMaster]
(
@SalesOrderMasterId int=NULL, @VchrNo int=NULL, @CustomerId int=NULL, @SalesAccountId int=NULL, @SalesManId int=NULL, @RouteId int=NULL, @PriceCategoryId int=NULL, @Remarks nvarchar(400)=NULL, @NetAmount decimal(24,4)=NULL, @AdditionalAmnt decimal(24,4)=NULL, @Freight decimal(24,4)=NULL, @BillDiscount decimal(24,4)=NULL, @AddDiscount decimal(24,4)=NULL, @GrandTotal decimal(24,4)=NULL, @InvType nvarchar(50)=NULL, @JEid int=NULL, @EntryDate datetime=NULL, @InvDate datetime=NULL, @Status bit=NULL, @Taxable decimal(24,4)=NULL, @TotalTax decimal(24,4)=NULL, @FK_BranchId int=NULL, @TargetDate datetime=NULL, @AdvanceAmount decimal(24,4)=NULL, @FieldOne nvarchar(100)=NULL, @FieldTwo nvarchar(100)=NULL, @FieldThree nvarchar(100)=NULL, @FieldFour nvarchar(100)=NULL, @FieldFive nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 update SalesOrderMaster set VchrNo=@VchrNo, CustomerId=@CustomerId, SalesAccountId=@SalesAccountId, SalesManId=@SalesManId, RouteId=@RouteId, PriceCategoryId=@PriceCategoryId, Remarks=@Remarks, NetAmount=@NetAmount, AdditionalAmnt=@AdditionalAmnt, Freight=@Freight, BillDiscount=@BillDiscount, AddDiscount=@AddDiscount, GrandTotal=@GrandTotal, InvType=@InvType, JEid=@JEid, EntryDate=@EntryDate, InvDate=@InvDate, Status=@Status, Taxable=@Taxable, TotalTax=@TotalTax, FK_BranchId=@FK_BranchId, TargetDate=@TargetDate, AdvanceAmount=@AdvanceAmount, FieldOne=@FieldOne, FieldTwo=@FieldTwo, FieldThree=@FieldThree, FieldFour=@FieldFour, FieldFive=@FieldFive  where SalesOrderMasterId=@SalesOrderMasterId
 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateSalesOrderSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateSalesOrderSettings]
(
@SalesOrderSettingsId int=NULL, @SalesOrderMasterId int=NULL, @PartyAccountId int=NULL, @ProductId int=NULL, @ProQty decimal(24,4)=NULL, @UnitId int=NULL, @ProRate decimal(24,4)=NULL, @ProDiscont decimal(24,4)=NULL, @DiscAmnt decimal(24,4)=NULL, @GrossAmnt decimal(24,4)=NULL, @NetAmnt decimal(24,4)=NULL, @Taxable decimal(24,4)=NULL, @Tax decimal(24,4)=NULL, @TaxAmount decimal(24,4)=NULL, @FK_BatchId int=NULL, @Barcode nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 update SalesOrderSettings set SalesOrderMasterId=@SalesOrderMasterId, PartyAccountId=@PartyAccountId, ProductId=@ProductId, ProQty=@ProQty, UnitId=@UnitId, ProRate=@ProRate, ProDiscont=@ProDiscont, DiscAmnt=@DiscAmnt, GrossAmnt=@GrossAmnt, NetAmnt=@NetAmnt, Taxable=@Taxable, Tax=@Tax, TaxAmount=@TaxAmount, FK_BatchId=@FK_BatchId, Barcode=@Barcode  where SalesOrderSettingsId=@SalesOrderSettingsId
 
 END 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateSalesRtnMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateSalesRtnMaster]
(
@SlsRtrnId int=NULL, @SlRtnInvNo int=NULL, @SlsRtnDate datetime=NULL, @SlsRtnAccountId int=NULL, @PartyAccountId int=NULL, @RouteId int=NULL, @VehId int=NULL, @StaffId int=NULL, @PriceCategoryId int=NULL, @AditionalAmnt decimal(24,4)=NULL, @Frieght decimal(24,4)=NULL, @TotalDiscount decimal(24,4)=NULL, @BillDiscount decimal(24,4)=NULL, @TotalAmount decimal(24,4)=NULL, @JeId int=NULL, @EntryDate datetime=NULL, @Remarks nvarchar(400)=NULL, @IGSTAmount decimal(24,4)=NULL, @SGSTAmount decimal(24,4)=NULL, @CGSTAmount decimal(24,4)=NULL, @FYearID int=NULL, @TotalTax decimal(24,4)=NULL, @Taxable decimal(24,4)=NULL, @FK_BranchId int=NULL, @SequenceNo int=NULL, @Terminal nvarchar(25)=NULL, @RefInvNo nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update SalesRtnMaster set SlRtnInvNo=@SlRtnInvNo, SlsRtnDate=@SlsRtnDate, SlsRtnAccountId=@SlsRtnAccountId, PartyAccountId=@PartyAccountId, RouteId=@RouteId, VehId=@VehId, StaffId=@StaffId, PriceCategoryId=@PriceCategoryId, AditionalAmnt=@AditionalAmnt, Frieght=@Frieght, TotalDiscount=@TotalDiscount, BillDiscount=@BillDiscount, TotalAmount=@TotalAmount, JeId=@JeId, EntryDate=@EntryDate, Remarks=@Remarks, IGSTAmount=@IGSTAmount, SGSTAmount=@SGSTAmount, CGSTAmount=@CGSTAmount, FYearID=@FYearID, TotalTax=@TotalTax, Taxable=@Taxable, FK_BranchId=@FK_BranchId, SequenceNo=@SequenceNo, Terminal=@Terminal, RefInvNo=@RefInvNo  where SlsRtrnId=@SlsRtrnId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateSelectedFYear]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateSelectedFYear]
(
@SlectedFY int=NULL, @FyearId int=NULL 
)
 AS 
 BEGIN 
 
 update SelectedFYear set FyearId=@FyearId  where SlectedFY=@SlectedFY
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateSelf]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateSelf]
(
@SelfId int=NULL, @GodownId int=NULL, @SelfName nvarchar(50)=NULL 
)
 AS 
 BEGIN 
 
 update Self set GodownId=@GodownId, SelfName=@SelfName  where SelfId=@SelfId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateSettings]
(
@Id int=NULL, @Name nvarchar(25)=NULL, @Caption nvarchar(100)=NULL, @Remarks nvarchar(100)=NULL, @BitValue bit=NULL, @StringValue nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update Settings set Name=@Name, Caption=@Caption, Remarks=@Remarks, BitValue=@BitValue, StringValue=@StringValue  where Id=@Id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateSlryPrcsSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateSlryPrcsSettings]
(
@SPrcsStngsID int=NULL, @SprcsMasterID int=NULL, @EmpID int=NULL, @EmpName nvarchar(50)=NULL, @LeavsTaken decimal(18,2)=NULL, @CasualLeave decimal(18,2)=NULL, @BasicSalary decimal(18,2)=NULL, @LeaveDiduction decimal(18,2)=NULL, @Incentive decimal(18,2)=NULL, @OtherAmnt decimal(18,2)=NULL, @SalaryCut decimal(18,2)=NULL, @OtherDiduction decimal(18,2)=NULL, @NetSalary decimal(18,2)=NULL, @SlryAdvance decimal(18,2)=NULL, @PaidAmnt decimal(18,2)=NULL, @Balance decimal(18,2)=NULL, @Remarks nvarchar(250)=NULL 
)
 AS 
 BEGIN 
 
 update SlryPrcsSettings set SprcsMasterID=@SprcsMasterID, EmpID=@EmpID, EmpName=@EmpName, LeavsTaken=@LeavsTaken, CasualLeave=@CasualLeave, BasicSalary=@BasicSalary, LeaveDiduction=@LeaveDiduction, Incentive=@Incentive, OtherAmnt=@OtherAmnt, SalaryCut=@SalaryCut, OtherDiduction=@OtherDiduction, NetSalary=@NetSalary, SlryAdvance=@SlryAdvance, PaidAmnt=@PaidAmnt, Balance=@Balance, Remarks=@Remarks  where SPrcsStngsID=@SPrcsStngsID
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateSlsRtnSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateSlsRtnSettings]
(
@SlsrtnSettingsID int=NULL, @SlsRtnId int=NULL, @ProductId int=NULL, @ProQty decimal(18,2)=NULL, @UnitId int=NULL, @ProRate decimal(18,2)=NULL, @ProDis decimal(18,2)=NULL, @DiscAmnt decimal(18,2)=NULL, @GrossAmnt decimal(18,2)=NULL, @isDamaged bit=NULL, @NetAmnt decimal(18,2)=NULL, @PartyAccountId int=NULL, @Date datetime=NULL, @HSN int=NULL, @GST decimal(18,2)=NULL, @GSTAMOUNT decimal(18,2)=NULL, @SGST decimal(18,2)=NULL, @SGSTAmount decimal(18,2)=NULL, @CGST decimal(18,2)=NULL, @CGSTAmount decimal(18,2)=NULL, @IGST decimal(18,2)=NULL, @IGSTAMOUNT decimal(18,2)=NULL, @FK_BatchId int=NULL 
)
 AS 
 BEGIN 
 
 update SlsRtnSettings set SlsRtnId=@SlsRtnId, ProductId=@ProductId, ProQty=@ProQty, UnitId=@UnitId, ProRate=@ProRate, ProDis=@ProDis, DiscAmnt=@DiscAmnt, GrossAmnt=@GrossAmnt, isDamaged=@isDamaged, NetAmnt=@NetAmnt, PartyAccountId=@PartyAccountId, Date=@Date, HSN=@HSN, GST=@GST, GSTAMOUNT=@GSTAMOUNT, SGST=@SGST, SGSTAmount=@SGSTAmount, CGST=@CGST, CGSTAmount=@CGSTAmount, IGST=@IGST, IGSTAMOUNT=@IGSTAMOUNT, FK_BatchId=@FK_BatchId  where SlsrtnSettingsID=@SlsrtnSettingsID
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateStockAdjusterMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateStockAdjusterMaster]
(
@stockAdjusterMasterId int=NULL, @StockAdjusterMasterCode int=NULL, @Date datetime=NULL, @Remarks nvarchar(400)=NULL, @ProtGroup int=NULL, @ProBrand int=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update StockAdjusterMaster set StockAdjusterMasterCode=@StockAdjusterMasterCode, Date=@Date, Remarks=@Remarks, ProtGroup=@ProtGroup, ProBrand=@ProBrand, FK_BranchId=@FK_BranchId  where stockAdjusterMasterId=@stockAdjusterMasterId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateStockAdjustSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateStockAdjustSettings]
(
@stockAdjustSettingsId int=NULL, @stockAdjusterMasterId int=NULL, @productId int=NULL, @currSystemStock decimal(18,2)=NULL, @curActualStock decimal(18,2)=NULL, @unitId int=NULL, @FK_BatchId int=NULL 
)
 AS 
 BEGIN 
 
 update StockAdjustSettings set stockAdjusterMasterId=@stockAdjusterMasterId, productId=@productId, currSystemStock=@currSystemStock, curActualStock=@curActualStock, unitId=@unitId, FK_BatchId=@FK_BatchId  where stockAdjustSettingsId=@stockAdjustSettingsId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateStockTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateStockTranLog]
(
@STranId int=NULL, @ProductId int=NULL, @VchrId int=NULL, @VchrNo int=NULL, @VchrDate datetime=NULL, @VchrType nvarchar(50)=NULL, @VchrFormType nvarchar(50)=NULL, @AccountId int=NULL, @InQuantity decimal(24,4)=NULL, @OutQuantity decimal(24,4)=NULL, @UnitId int=NULL, @Narration nvarchar(400)=NULL, @UserId int=NULL, @TranDate datetime=NULL, @FK_BatchId int=NULL, @FK_BranchId int=NULL, @Barcode nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update StockTranLog set ProductId=@ProductId, VchrId=@VchrId, VchrNo=@VchrNo, VchrDate=@VchrDate, VchrType=@VchrType, VchrFormType=@VchrFormType, AccountId=@AccountId, InQuantity=@InQuantity, OutQuantity=@OutQuantity, UnitId=@UnitId, Narration=@Narration, UserId=@UserId, TranDate=@TranDate, FK_BatchId=@FK_BatchId, FK_BranchId=@FK_BranchId, Barcode=@Barcode  where STranId=@STranId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateStockTranSettings]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateStockTranSettings]
(
@StockTranSettingsId int=NULL, @StockTransferId int=NULL, @ProductId int=NULL, @ProductName nvarchar(50)=NULL, @ProQuantity decimal(18,2)=NULL, @UnitId int=NULL, @CompanyName nvarchar(50)=NULL, @CompanyPlace nvarchar(50)=NULL, @ProRate decimal(18,2)=NULL, @Total decimal(18,2)=NULL, @FK_BatchId int=NULL 
)
 AS 
 BEGIN 
 
 update StockTranSettings set StockTransferId=@StockTransferId, ProductId=@ProductId, ProductName=@ProductName, ProQuantity=@ProQuantity, UnitId=@UnitId, CompanyName=@CompanyName, CompanyPlace=@CompanyPlace, ProRate=@ProRate, Total=@Total, FK_BatchId=@FK_BatchId  where StockTranSettingsId=@StockTranSettingsId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateStockTransferMaster]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateStockTransferMaster]
(
@StockTransferId int=NULL, @StockTransferCode int=NULL, @TransferDate datetime=NULL, @FromGodownId int=NULL, @FromGodownName nvarchar(400)=NULL, @ToGodownId int=NULL, @ToGodownName nvarchar(400)=NULL, @Remarks nvarchar(400)=NULL, @UserId int=NULL, @EntryDate datetime=NULL, @CompanyName nvarchar(50)=NULL, @CompanyPlace nvarchar(50)=NULL, @TotalAmnt decimal(18,2)=NULL, @TotalQty decimal(18,2)=NULL, @TranType nvarchar(50)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update StockTransferMaster set StockTransferCode=@StockTransferCode, TransferDate=@TransferDate, FromGodownId=@FromGodownId, FromGodownName=@FromGodownName, ToGodownId=@ToGodownId, ToGodownName=@ToGodownName, Remarks=@Remarks, UserId=@UserId, EntryDate=@EntryDate, CompanyName=@CompanyName, CompanyPlace=@CompanyPlace, TotalAmnt=@TotalAmnt, TotalQty=@TotalQty, TranType=@TranType, FK_BranchId=@FK_BranchId  where StockTransferId=@StockTransferId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateTaxCategory]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateTaxCategory]
(
@TCategoryId int=NULL, @TCategoryName nvarchar(20)=NULL, @SalesVat decimal(18,2)=NULL, @PurVat decimal(18,2)=NULL, @SaleCst decimal(18,2)=NULL, @PurCst decimal(18,2)=NULL, @SaleCess decimal(18,2)=NULL, @PurCess decimal(18,2)=NULL, @SalesXDuty decimal(18,2)=NULL, @PurXDuty decimal(18,2)=NULL 
)
 AS 
 BEGIN 
 
 update TaxCategory set TCategoryName=@TCategoryName, SalesVat=@SalesVat, PurVat=@PurVat, SaleCst=@SaleCst, PurCst=@PurCst, SaleCess=@SaleCess, PurCess=@PurCess, SalesXDuty=@SalesXDuty, PurXDuty=@PurXDuty  where TCategoryId=@TCategoryId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateTransactionLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateTransactionLog]
(
@TranId int=NULL, @VchrId int=NULL, @VchrNo int=NULL, @VchrType nvarchar(50)=NULL, @VchrDate datetime=NULL, @AccountId int=NULL, @OppAccountId int=NULL, @DebitAmount decimal(24,4)=NULL, @CreditAmount decimal(24,4)=NULL, @Narration nvarchar(400)=NULL, @UserID int=NULL, @TranDate datetime=NULL, @Status nvarchar(10)=NULL, @RefNo nvarchar(20)=NULL, @VchrName nvarchar(50)=NULL, @FK_BranchId int=NULL, @SequenceNo int=NULL, @Terminal nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update TransactionLog set VchrId=@VchrId, VchrNo=@VchrNo, VchrType=@VchrType, VchrDate=@VchrDate, AccountId=@AccountId, OppAccountId=@OppAccountId, DebitAmount=@DebitAmount, CreditAmount=@CreditAmount, Narration=@Narration, UserID=@UserID, TranDate=@TranDate, Status=@Status, RefNo=@RefNo, VchrName=@VchrName, FK_BranchId=@FK_BranchId, SequenceNo=@SequenceNo, Terminal=@Terminal  where TranId=@TranId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateTranslator]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateTranslator]
(
@Id int=NULL, @Name nvarchar(100)=NULL, @Value nvarchar(100)=NULL, @Form nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 update Translator set Name=@Name, Value=@Value, Form=@Form  where Id=@Id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateUnit]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateUnit]
(
@UnitId int=NULL, @UnitName nvarchar(10)=NULL, @UnitNo int=NULL 
)
 AS 
 BEGIN 
 
 update Unit set UnitName=@UnitName, UnitNo=@UnitNo  where UnitId=@UnitId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateUser]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateUser]
(
@UserId int=NULL, @PrevilageId int=NULL, @UserName nvarchar(50)=NULL, @PassWord nvarchar(50)=NULL 
)
 AS 
 BEGIN 
 
 update [User] set PrevilageId=@PrevilageId, UserName=@UserName, PassWord=@PassWord  where UserId=@UserId
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateUserPrevilegeDtl]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateUserPrevilegeDtl]
(
@Id int=NULL, @FK_UserPrevilegeHdrId int=NULL, @FK_MenuDetailsId int=NULL, @Visible bit=NULL, @Enable bit=NULL, @SaveData bit=NULL, @EditData bit=NULL, @DeleteData bit=NULL 
)
 AS 
 BEGIN 
 
 update UserPrevilegeDtl set FK_UserPrevilegeHdrId=@FK_UserPrevilegeHdrId, FK_MenuDetailsId=@FK_MenuDetailsId, Visible=@Visible, Enable=@Enable, SaveData=@SaveData, EditData=@EditData, DeleteData=@DeleteData  where Id=@Id
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateUserPrevilegeHdr]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateUserPrevilegeHdr]
(
@Id int=NULL, @Name nvarchar(100)=NULL 
)
 AS 
 BEGIN 
 
 update UserPrevilegeHdr set Name=@Name  where Id=@Id
 
 END 
 
GO
/****** Object:  StoredProcedure [dbo].[spUpdateVchrTranLog]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUpdateVchrTranLog]
(
@VchrTranId int=NULL, @VchrNo int=NULL, @VchrDate datetime=NULL, @VchrAmount decimal(24,4)=NULL, @VchrType nvarchar(50)=NULL, @FromAccountId int=NULL, @ToAccountId int=NULL, @Narration nvarchar(400)=NULL, @SalesManId int=NULL, @UserId int=NULL, @TranDate datetime=NULL, @VchrName nvarchar(50)=NULL, @FK_BranchId int=NULL, @SequenceNo int=NULL, @Terminal nvarchar(25)=NULL 
)
 AS 
 BEGIN 
 
 update VchrTranLog set VchrNo=@VchrNo, VchrDate=@VchrDate, VchrAmount=@VchrAmount, VchrType=@VchrType, FromAccountId=@FromAccountId, ToAccountId=@ToAccountId, Narration=@Narration, SalesManId=@SalesManId, UserId=@UserId, TranDate=@TranDate, VchrName=@VchrName, FK_BranchId=@FK_BranchId, SequenceNo=@SequenceNo, Terminal=@Terminal  where VchrTranId=@VchrTranId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUpdateVehicle]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
Create Procedure [dbo].[spUpdateVehicle]
(
@VanId int=NULL, @VehName nvarchar(50)=NULL, @Manufacturer nvarchar(20)=NULL, @Model nvarchar(20)=NULL, @YearOfModel nvarchar(20)=NULL, @RegNo nvarchar(20)=NULL, @Ownership nvarchar(50)=NULL, @RegisterdOwner nvarchar(50)=NULL, @EngineNo nvarchar(50)=NULL, @ChasisNo nvarchar(50)=NULL, @InsurenceDate datetime=NULL, @TaxDate datetime=NULL, @PolusionDate datetime=NULL, @BColor nvarchar(20)=NULL, @LoanEMIDate datetime=NULL, @LoanEMIAmount nvarchar(200)=NULL, @Remarks nvarchar(400)=NULL, @FK_BranchId int=NULL 
)
 AS 
 BEGIN 
 
 update Vehicle set VehName=@VehName, Manufacturer=@Manufacturer, Model=@Model, YearOfModel=@YearOfModel, RegNo=@RegNo, Ownership=@Ownership, RegisterdOwner=@RegisterdOwner, EngineNo=@EngineNo, ChasisNo=@ChasisNo, InsurenceDate=@InsurenceDate, TaxDate=@TaxDate, PolusionDate=@PolusionDate, BColor=@BColor, LoanEMIDate=@LoanEMIDate, LoanEMIAmount=@LoanEMIAmount, Remarks=@Remarks, FK_BranchId=@FK_BranchId  where VanId=@VanId
 
 END 

GO
/****** Object:  StoredProcedure [dbo].[spUserWiseSale]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spUserWiseSale]
(
@FromDate datetime,
@ToDate datetime,
@BranchId int =null
)
As
Begin
select usr.UserName,SUM(sem.GrandDiscount) as SalesAmount
From SalesEstimateMaster sem
Left Join [User] usr on sem.JEId = usr.UserId
Where (sem.FK_BranchId = @BranchId or @BranchId is null) and
sem.SalesDate between @FromDate and @ToDate 
Group by usr.UserName order by usr.UserName
End
GO
/****** Object:  StoredProcedure [dbo].[spVatReport]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spVatReport]
(
@FromDate datetime, 
@ToDate datetime,
@BranchId int = null
)
As
Begin

declare @TotalTaxSale decimal(18,4)
select @TotalTaxSale = sum(NetAmnt) 
from SalesEstimateSettings ses
Left Join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
 where (sem.FK_BranchId = @BranchId or @BranchId is null) and
[Date] between @FromDate and @ToDate and GST > 0

declare @TotalSale decimal(18,4)
select @TotalSale = sum(NetAmnt) 
from SalesEstimateSettings ses
Left Join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
where (sem.FK_BranchId = @BranchId or @BranchId is null) and
[Date] between @FromDate and @ToDate

declare @TotalZTaxSale decimal(18,4)
select @TotalZTaxSale = sum(NetAmnt)  
from dbo.SalesEstimateSettings ses
Left Join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
where (sem.FK_BranchId = @BranchId or @BranchId is null) and
[Date] between @FromDate and @ToDate and GST = 0

declare @TotalSalesVat decimal(18,4)
select @TotalSalesVat = sum(GSTAMOUNT) 
from dbo.SalesEstimateSettings ses
Left Join SalesEstimateMaster sem on ses.SalesInvId = sem.SalesInvId
where (sem.FK_BranchId = @BranchId or @BranchId is null) and
[Date] between @FromDate and @ToDate

declare @TotalTaxPurchase decimal(18,4)
select @TotalTaxPurchase = sum(NetAmnt) 
from dbo.PurInvSettings pis
Left join PurInvMaster pim on pis.PurInvId = pim.PurInvId
where (pim.FK_BranchId = @BranchId or @BranchId is null) and
[Date] between @FromDate and @ToDate and GST > 0

declare @TotalPurchase decimal(18,4)
select @TotalPurchase = sum(NetAmnt) 
from dbo.PurInvSettings pis
Left join PurInvMaster pim on pis.PurInvId = pim.PurInvId
where (pim.FK_BranchId = @BranchId or @BranchId is null) and
[Date] between @FromDate and @ToDate

declare @TotalZTaxPurchase decimal(18,4)
select @TotalZTaxPurchase = sum(NetAmnt) 
from dbo.PurInvSettings pis
Left join PurInvMaster pim on pis.PurInvId = pim.PurInvId
where (pim.FK_BranchId = @BranchId or @BranchId is null) and
[Date] between @FromDate and @ToDate and GST = 0

declare @TotalPurchaseVat decimal(18,4)
select @TotalPurchaseVat= sum(GSTAMOUNT) 
from dbo.PurInvSettings pis
Left join PurInvMaster pim on pis.PurInvId = pim.PurInvId
where (pim.FK_BranchId = @BranchId or @BranchId is null) and
[Date] between @FromDate and @ToDate and GST > 0

declare @VatDifference decimal(18,4)
set @VatDifference = @TotalSalesVat- @TotalPurchaseVat

select @TotalSale as TotalSalesAmount,@TotalTaxSale as TotalVatSale, @TotalSalesVat as TotalSalesVat,@TotalPurchase as TotalPurchaseAmount,
@TotalTaxPurchase as TotalVatPurchase, @TotalZTaxSale as ZeroVatSales, @TotalZTaxPurchase as ZeroVatPurchase, @VatDifference as VatDifference, @TotalPurchaseVat as TotalPurchaseVat
End
GO
/****** Object:  StoredProcedure [dbo].[spVoidKOTReports]    Script Date: 3/29/2022 7:15:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[spVoidKOTReports]
(
@FromDate datetime=null,
@ToDate datetime=null,
@Terminal nvarchar(25)=null,
@SequenceNo int=null,
@BranchId int =null
)
As
Begin
Select row_number() over( order by  kos.Barcode) as SL,coalesce(pb.ProductBarcodeName,pt.ProductName) as ProductName,
kos.Barcode ,sum(kos.Quantity) as Quantity,sum(kos.NETAmount) as Total
from KOTOrderSettings kos
left Join KOTOrderMaster kom on kos.FK_KOTMasterId=kom.KOTMasterId
left join ProductBarcode pb on kos.Barcode= pb.Barcode
left join Product pt on kos.ProductId = pt.ProductId
where kom.Remarks='Cancel KOT'and
kom.DateAndTime >= @FromDate and kom.DateAndTime<= @ToDate and
(kom.Terminal=@Terminal or  @Terminal is null)  and 
(kom.SerialNo=@SequenceNo or @SequenceNo is null) and
(kom.FK_BranchId = @BranchId or @BranchId is null)
Group by kos.Barcode,pb.ProductBarcodeName,pt.ProductName
End
GO

