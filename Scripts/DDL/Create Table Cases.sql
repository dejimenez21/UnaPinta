USE [UnaPintaDB_En]
GO

/****** Object:  Table [dbo].[Cases]    Script Date: 10/10/2021 7:26:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cases](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DonorId] [bigint] NOT NULL,
	[RequestId] [bigint] NOT NULL,
	[StatusId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[LastUpdatedAt] [datetime2](7) NOT NULL,
	[DeletedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Cases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cases]  WITH CHECK ADD  CONSTRAINT [FK_Cases_CaseStatus_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[CaseStatus] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Cases] CHECK CONSTRAINT [FK_Cases_CaseStatus_StatusId]
GO

ALTER TABLE [dbo].[Cases]  WITH CHECK ADD  CONSTRAINT [FK_Cases_Requests_RequestId] FOREIGN KEY([RequestId])
REFERENCES [dbo].[Requests] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Cases] CHECK CONSTRAINT [FK_Cases_Requests_RequestId]
GO

ALTER TABLE [dbo].[Cases]  WITH CHECK ADD  CONSTRAINT [FK_Cases_Users_DonorId] FOREIGN KEY([DonorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Cases] CHECK CONSTRAINT [FK_Cases_Users_DonorId]
GO

