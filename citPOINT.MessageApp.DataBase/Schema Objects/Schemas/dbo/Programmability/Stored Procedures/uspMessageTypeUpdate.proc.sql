CREATE PROCEDURE [dbo].[uspMessageTypeUpdate]
	@MessageTypeID uniqueidentifier,
    @MessageTypeName nvarchar(100),
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[MessageType]
	SET    [MessageTypeID] = @MessageTypeID, [MessageTypeName] = @MessageTypeName, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [MessageTypeID] = @MessageTypeID
	
	-- Begin Return Select <- do not remove
	SELECT [MessageTypeID], [MessageTypeName], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[MessageType]
	WHERE  [MessageTypeID] = @MessageTypeID	
	-- End Return Select <- do not remove

	COMMIT TRAN