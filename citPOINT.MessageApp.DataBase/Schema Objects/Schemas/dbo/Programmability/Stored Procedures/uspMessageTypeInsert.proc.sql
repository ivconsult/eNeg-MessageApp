CREATE PROCEDURE [dbo].[uspMessageTypeInsert]
	@MessageTypeID uniqueidentifier,
    @MessageTypeName nvarchar(100),
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[MessageType] ([MessageTypeID], [MessageTypeName], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @MessageTypeID, @MessageTypeName, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [MessageTypeID], [MessageTypeName], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[MessageType]
	WHERE  [MessageTypeID] = @MessageTypeID
	-- End Return Select <- do not remove
               
	COMMIT