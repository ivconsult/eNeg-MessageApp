CREATE PROCEDURE [dbo].[uspNegotiationPhaseInsert]
	@NegotiationPhaseID uniqueidentifier,
    @NegotiationPhaseName nvarchar(100),
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[NegotiationPhase] ([NegotiationPhaseID], [NegotiationPhaseName], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @NegotiationPhaseID, @NegotiationPhaseName, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationPhaseID], [NegotiationPhaseName], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegotiationPhase]
	WHERE  [NegotiationPhaseID] = @NegotiationPhaseID
	-- End Return Select <- do not remove
               
	COMMIT