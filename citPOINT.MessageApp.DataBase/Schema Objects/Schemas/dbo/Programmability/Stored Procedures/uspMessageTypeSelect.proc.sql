CREATE PROCEDURE [dbo].[uspMessageTypeSelect]
	 @MessageTypeID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [MessageTypeID], [MessageTypeName], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[MessageType] 
	WHERE  ([MessageTypeID] = @MessageTypeID OR @MessageTypeID IS NULL) 

	COMMIT