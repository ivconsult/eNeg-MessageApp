CREATE PROCEDURE [dbo].[uspNegotiationPhaseSelect]
	@NegotiationPhaseID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NegotiationPhaseID], [NegotiationPhaseName], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[NegotiationPhase] 
	WHERE  ([NegotiationPhaseID] = @NegotiationPhaseID OR @NegotiationPhaseID IS NULL) 

	COMMIT