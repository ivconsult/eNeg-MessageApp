CREATE PROCEDURE [dbo].[uspNegPhaseMessagesSelect]
	@NegPhaseMessagesID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NegPhaseMessagesID], [MessageTypeID], [NegotiationPhaseID], [MessageContent], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[NegPhaseMessages] 
	WHERE  ([NegPhaseMessagesID] = @NegPhaseMessagesID OR @NegPhaseMessagesID IS NULL) 

	COMMIT