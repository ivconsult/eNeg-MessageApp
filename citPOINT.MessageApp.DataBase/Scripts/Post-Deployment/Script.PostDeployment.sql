/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*-----------------------------------------------
	[NegotiationPhase] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[NegotiationPhase])

BEGIN

	BEGIN TRANSACTION;
	INSERT INTO [dbo].[NegotiationPhase]([NegotiationPhaseID], [NegotiationPhaseName] ,[Deleted] ,[DeletedBy] ,[DeletedOn] )
	SELECT N'13830846-07AB-4966-8550-247E2F185821', N'Preparation', 0, Null, GETDATE() UNION ALL
	SELECT N'13830846-07AB-4966-8550-247E2F185822', N'Agenda Setting', 0, Null,GETDATE() UNION ALL
	SELECT N'13830846-07AB-4966-8550-247E2F185823', N'Discussion', 0, Null, GETDATE()
	COMMIT;
	RAISERROR (N'[dbo].[NegotiationPhase]: Insert Batch: 1.....Done!', 3, 1) WITH NOWAIT;
End
GO

/*-----------------------------------------------
	[MessageType] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[MessageType])

BEGIN

	BEGIN TRANSACTION;
	INSERT INTO [dbo].[MessageType]([MessageTypeID],[MessageTypeName] ,[Deleted] ,[DeletedBy] ,[DeletedOn] )
	SELECT N'969D11BF-5504-4C30-92A2-A8BF1D241F89', 'Greeting',0,Null,GETDATE() UNION ALL
	SELECT N'969D11BF-5504-4C30-92A2-A8BF1D241F88', 'Introduction',0,Null,GETDATE() UNION ALL
	SELECT N'969D11BF-5504-4C30-92A2-A8BF1D241F87', 'Requesting Agenda',0,Null,GETDATE() UNION ALL
	SELECT N'969D11BF-5504-4C30-92A2-A8BF1D241F86', 'Offering Agenda',0,Null,GETDATE() UNION ALL
	SELECT N'969D11BF-5504-4C30-92A2-A8BF1D241F85', 'Offering',0,Null,GETDATE() 
	COMMIT;
	RAISERROR (N'[dbo].[MessageType]: Insert Batch: 1.....Done!', 5, 1) WITH NOWAIT;
End
GO

/*-----------------------------------------------
	[NegPhaseMessages] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[NegPhaseMessages])

BEGIN

	BEGIN TRANSACTION;
	INSERT INTO [dbo].[NegPhaseMessages]([NegPhaseMessagesID], [MessageTypeID] , [NegotiationPhaseID], [MessageContent] ,[Deleted] ,[DeletedBy] ,[DeletedOn] )
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7430', N'969D11BF-5504-4C30-92A2-A8BF1D241F89', N'13830846-07AB-4966-8550-247E2F185821', N'Preparation Greeting', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7431', N'969D11BF-5504-4C30-92A2-A8BF1D241F88', N'13830846-07AB-4966-8550-247E2F185821', N'Preparation Introduction', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7432', N'969D11BF-5504-4C30-92A2-A8BF1D241F87', N'13830846-07AB-4966-8550-247E2F185821', N'Preparation Requesting Agenda', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7433', N'969D11BF-5504-4C30-92A2-A8BF1D241F86', N'13830846-07AB-4966-8550-247E2F185821', N'Preparation Offering Agenda', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7434', N'969D11BF-5504-4C30-92A2-A8BF1D241F85', N'13830846-07AB-4966-8550-247E2F185821', N'Preparation Offering', 0, Null, GETDATE() UNION ALL
	/*------------------------------------------------------------------*/			  
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7435', N'969D11BF-5504-4C30-92A2-A8BF1D241F89', N'13830846-07AB-4966-8550-247E2F185822', N'Agenda Setting Greeting', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7436', N'969D11BF-5504-4C30-92A2-A8BF1D241F88', N'13830846-07AB-4966-8550-247E2F185822', N'Agenda Setting Introduction', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7437', N'969D11BF-5504-4C30-92A2-A8BF1D241F87', N'13830846-07AB-4966-8550-247E2F185822', N'Agenda Setting Requesting Agenda', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7438', N'969D11BF-5504-4C30-92A2-A8BF1D241F86', N'13830846-07AB-4966-8550-247E2F185822', N'Agenda Setting Offering Agenda', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7439', N'969D11BF-5504-4C30-92A2-A8BF1D241F85', N'13830846-07AB-4966-8550-247E2F185822', N'Agenda Setting Offering', 0, Null, GETDATE() UNION ALL
	/*-------------------------------------------------------------------*/			 	
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7440', N'969D11BF-5504-4C30-92A2-A8BF1D241F89', N'13830846-07AB-4966-8550-247E2F185823', N'Discussion Greeting', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7441', N'969D11BF-5504-4C30-92A2-A8BF1D241F88', N'13830846-07AB-4966-8550-247E2F185823', N'Discussion Introduction', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7442', N'969D11BF-5504-4C30-92A2-A8BF1D241F87', N'13830846-07AB-4966-8550-247E2F185823', N'Discussion Requesting Agenda', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7443', N'969D11BF-5504-4C30-92A2-A8BF1D241F86', N'13830846-07AB-4966-8550-247E2F185823', N'Discussion Offering Agenda', 0, Null, GETDATE() UNION ALL
	SELECT N'C4AD4459-FF1E-4210-BAA1-60173A2C7444', N'969D11BF-5504-4C30-92A2-A8BF1D241F85', N'13830846-07AB-4966-8550-247E2F185823', N'Discussion Offering', 0, Null, GETDATE()
	COMMIT;
	RAISERROR (N'[dbo].[NegPhaseMessages]: Insert Batch: 1.....Done!', 15, 1) WITH NOWAIT;
End
GO



IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'MessageAppUser')
DROP USER [MessageAppUser]
GO

/****** Object:  Login [MessageAppUser]    Script Date: 08/25/2010 10:31:45 ******/
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'MessageAppUser')
DROP LOGIN [MessageAppUser]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [MessageAppUser]    Script Date: 08/25/2010 10:31:45 ******/
CREATE LOGIN [MessageAppUser] WITH PASSWORD='MessageAppUserPassword', DEFAULT_DATABASE=[MessageApp], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

 
CREATE USER [MessageAppUser] FOR LOGIN [MessageAppUser] 
GO


EXEC sp_addrolemember N'db_owner', N'MessageAppUser'
go
