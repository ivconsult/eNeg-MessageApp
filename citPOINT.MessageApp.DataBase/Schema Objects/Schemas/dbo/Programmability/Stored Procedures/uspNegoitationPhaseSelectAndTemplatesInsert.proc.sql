CREATE PROCEDURE [dbo].[uspNegoitationPhaseSelectAndTemplatesInsert]
	@UserID uniqueidentifier 
AS
	BEGIN TRANSACTION 
	
	declare @NegotiatiobPhasesCount int
	/*----------- Check on the number of rows exist for certain user to check 
		if he is the first time to access this or not  -----------*/
	SET @NegotiatiobPhasesCount = (SELECT COUNT(*)
									FROM [dbo].[NegotiationPhase]
									WHERE [DeletedBy] = @UserID and Deleted = 0)

	IF(@NegotiatiobPhasesCount = 0)

	   /*----------- Insert templates for Negotiation Phase -----------*/
		INSERT INTO [dbo].[NegotiationPhase]
			SELECT NEWID(), [NegotiationPhaseName], 0, @UserID, GETDATE() 
			FROM [dbo].[NegotiationPhase] 	
			WHERE [DeletedBy] is NULL

		/*----------- Insert templates for Message Types -----------*/
		INSERT INTO [dbo].[MessageType] 
			SELECT NEWID(), [MessageTypeName], 0, @UserID, GETDATE() 
			FROM [dbo].[MessageType]
			WHERE [DeletedBy] is NULL

		/*----------- Create Temp table to save last added keys to be used as foreign keys -----------*/
		CREATE TABLE Mappingtbl 
		(
			TableName NVARCHAR(50),
			NewKey UNIQUEIDENTIFIER,
			OldKey UNIQUEIDENTIFIER
		);

		/*----------- Insert keys of both tables -----------*/
		INSERT INTO Mappingtbl
			SELECT 'NegotiationPhase', NewTable.NegotiationPhaseID AS New, dbo.NegotiationPhase.NegotiationPhaseID AS Old
			FROM   dbo.NegotiationPhase INNER JOIN
				   dbo.NegotiationPhase AS NewTable ON dbo.NegotiationPhase.NegotiationPhaseName = NewTable.NegotiationPhaseName
			WHERE  (dbo.NegotiationPhase.DeletedBy IS NULL) AND (NewTable.DeletedBy = @UserID) AND 
				   (NewTable.Deleted = 0) AND (dbo.NegotiationPhase.Deleted = 0)
			Union ALL
			SELECT 'MessageType', NewTable.MessageTypeID AS New, dbo.MessageType.MessageTypeID AS Old
			FROM   dbo.MessageType INNER JOIN
				   dbo.MessageType AS NewTable ON dbo.MessageType.MessageTypeName = NewTable.MessageTypeName
			WHERE  (dbo.MessageType.DeletedBy IS NULL) AND (NewTable.DeletedBy = @UserID) AND 
				   (NewTable.Deleted = 0) AND (dbo.MessageType.Deleted = 0)
			
		/*----------- Insert templates for Negotiation Phase Messages with replacing old key by new one using mapped table-----------*/
		INSERT INTO [dbo].[NegPhaseMessages] 
			SELECT NEWID(), [dbo].GetMappedKey('MessageType',[MessageTypeID]) , [dbo].GetMappedKey('NegotiationPhase',[NegotiationPhaseID]) ,[MessageContent] , 0, @UserID, GETDATE() 
			FROM [dbo].[NegPhaseMessages] 
			WHERE [DeletedBy] is NULL

		DROP TABLE Mappingtbl
	COMMIT