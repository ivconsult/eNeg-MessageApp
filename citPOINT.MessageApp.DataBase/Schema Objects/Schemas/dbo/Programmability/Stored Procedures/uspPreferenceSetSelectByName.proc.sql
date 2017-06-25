CREATE PROCEDURE [dbo].[uspPreferenceSetSelectByName]
	@PreferenceSetID uniqueidentifier, 
	@PreferenceSetName nvarchar(150),
	@UserID uniqueidentifier
AS
	SELECT COUNT(PreferenceSetName) as NoOfNegotiationRepeat
	FROM PreferenceSet
	WHERE PreferenceSetName = @PreferenceSetName and 
	PreferenceSetID != @PreferenceSetID and 
	Deleted = 0 and [UserID] = @UserID
RETURN 0