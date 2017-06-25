CREATE TABLE [dbo].[NegotiationPhase]
(
	NegotiationPhaseID uniqueidentifier primary key, 
	NegotiationPhaseName nvarchar(100)  not null,
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn datetime
)
