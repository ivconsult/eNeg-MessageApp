CREATE FUNCTION [dbo].[GetMappedKey]
(
	@TableName NVARCHAR(50),
	@OLdKey UNIQUEIDENTIFIER	
)
RETURNS UNIQUEIDENTIFIER
AS
BEGIN	
	RETURN (SELECT Newkey
			FROM Mappingtbl
		    WHERE OldKey = @OLdKey and TableName = @TableName)
END