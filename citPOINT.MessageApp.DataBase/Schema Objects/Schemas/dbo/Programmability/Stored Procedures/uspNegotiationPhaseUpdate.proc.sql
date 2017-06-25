CREATE PROCEDURE [dbo].[uspNegotiationPhaseUpdate]
	@NegotiationPhaseID uniqueidentifier,
    @NegotiationPhaseName nvarchar(100),
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegotiationPhase]
	SET    [NegotiationPhaseID] = @NegotiationPhaseID, [NegotiationPhaseName] = @NegotiationPhaseName, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [NegotiationPhaseID] = @NegotiationPhaseID
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationPhaseID], [NegotiationPhaseName], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegotiationPhase]
	WHERE  [NegotiationPhaseID] = @NegotiationPhaseID	
	-- End Return Select <- do not remove

	COMMIT TRAN