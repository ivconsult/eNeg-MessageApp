CREATE TABLE [dbo].[MessageType]
(
	MessageTypeID uniqueidentifier primary key, 
	MessageTypeName nvarchar(100) not null,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn datetime
)
