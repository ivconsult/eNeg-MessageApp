CREATE PROCEDURE [dbo].[uspNegPhaseMessagesDelete]
	@NegPhaseMessagesID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegPhaseMessages]
	SET [Deleted]  = 1, DeletedOn = GETDATE()
	WHERE  [NegPhaseMessagesID] = @NegPhaseMessagesID

	COMMIT