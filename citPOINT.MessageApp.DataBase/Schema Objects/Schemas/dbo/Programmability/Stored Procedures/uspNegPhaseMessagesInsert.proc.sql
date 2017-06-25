CREATE PROCEDURE [dbo].[uspNegPhaseMessagesInsert]
	@NegPhaseMessagesID uniqueidentifier,
    @MessageTypeID uniqueidentifier,
    @NegotiationPhaseID uniqueidentifier,
    @MessageContent ntext,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[NegPhaseMessages] ([NegPhaseMessagesID], [MessageTypeID], [NegotiationPhaseID], [MessageContent], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @NegPhaseMessagesID, @MessageTypeID, @NegotiationPhaseID, @MessageContent, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [NegPhaseMessagesID], [MessageTypeID], [NegotiationPhaseID], [MessageContent], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegPhaseMessages]
	WHERE  [NegPhaseMessagesID] = @NegPhaseMessagesID
	-- End Return Select <- do not remove
               
	COMMIT