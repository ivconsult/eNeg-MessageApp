CREATE PROCEDURE [dbo].[uspNegotiationPhaseDelete]
	@NegotiationPhaseID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegotiationPhase]
	SET [Deleted]  = 1, DeletedOn = GETDATE()
	WHERE  [NegotiationPhaseID] = @NegotiationPhaseID

	COMMIT