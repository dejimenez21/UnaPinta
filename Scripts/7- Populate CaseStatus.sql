
INSERT INTO [dbo].[CaseStatus]
           ([Id]
		   ,[Description]
           ,[CreatedAt]
           ,[LastUpdatedAt]
           ,[DeletedAt])
     VALUES
           (1
		   ,'En Proceso'
           ,getdate()
           ,getdate()
           ,null),
		   (2
		   ,'Finalizado'
           ,getdate()
           ,getdate()
           ,null),
		   (3
		   ,'Cancelado'
           ,getdate()
           ,getdate()
           ,null)
GO


