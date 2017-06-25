CREATE PROCEDURE [dbo].[uspMessageTypeDelete]
	@MessageTypeID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[MessageType]
	SET [Deleted]  = 1, DeletedOn = GETDATE()
	WHERE  [MessageTypeID] = @MessageTypeID

	COMMIT