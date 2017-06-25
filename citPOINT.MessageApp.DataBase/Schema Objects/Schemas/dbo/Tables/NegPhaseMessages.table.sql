CREATE TABLE [dbo].[NegPhaseMessages]
(
	NegPhaseMessagesID uniqueidentifier primary key, 
	MessageTypeID uniqueidentifier references MessageType(MessageTypeID), 
	NegotiationPhaseID uniqueidentifier references NegotiationPhase(NegotiationPhaseID), 
	MessageContent ntext,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn datetime
)
