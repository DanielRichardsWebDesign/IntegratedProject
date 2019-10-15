CREATE TABLE [dbo].[Conditions]
(
	[ConditionID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[PatientID] INT NOT NULL REFERENCES Patients (PatientID),
	[Condition] VARCHAR (50) NOT NULL,
	[ConditionNotes] VARCHAR (255) NOT NULL
)