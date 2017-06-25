CREATE PROCEDURE [dbo].[uspNegPhaseMessagesUpdate]
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

	UPDATE [dbo].[NegPhaseMessages]
	SET    [NegPhaseMessagesID] = @NegPhaseMessagesID, [MessageTypeID] = @MessageTypeID, [NegotiationPhaseID] = @NegotiationPhaseID, [MessageContent] = @MessageContent, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [NegPhaseMessagesID] = @NegPhaseMessagesID
	
	-- Begin Return Select <- do not remove
	SELECT [NegPhaseMessagesID], [MessageTypeID], [NegotiationPhaseID], [MessageContent], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegPhaseMessages]
	WHERE  [NegPhaseMessagesID] = @NegPhaseMessagesID	
	-- End Return Select <- do not remove

	COMMIT TRAN