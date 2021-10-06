USE [UnaPintaDB_En]
GO

INSERT INTO [dbo].[Roles]
           ([Description]
           ,[CreatedAt]
           ,[LastUpdatedAt]
           ,[Name]
           ,[NormalizedName]
           ,[ConcurrencyStamp])
     VALUES
           (NUll
           ,'2021-10-02 17:54:52.6252287'
           ,'2021-10-02 17:54:52.6252287'
           ,'donante'
           ,'DONANTE'
           ,'71b1eef1-ca5e-4684-9aef-385b96260420'),
		   
		   (NUll
           ,'2021-10-02 17:54:52.6252287'
           ,'2021-10-02 17:54:52.6252287'
           ,'solicitante'
           ,'SOLICITANTE'
           ,'69843acb-7a88-45ff-99e7-0c6b8fe635f6')
GO
