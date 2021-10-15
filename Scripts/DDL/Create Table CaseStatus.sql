USE [UnaPintaDB_En]
GO

/****** Object:  Table [dbo].[CaseStatus]    Script Date: 10/10/2021 7:27:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CaseStatus](
	[Id] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[LastUpdatedAt] [datetime2](7) NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_CaseStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

