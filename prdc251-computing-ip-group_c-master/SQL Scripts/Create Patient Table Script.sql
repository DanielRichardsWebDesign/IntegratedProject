CREATE TABLE [dbo].[Patients]
(
	[PatientID] INT IDENTITY (1,1) NOT NULL,
	[PatientFName] VARCHAR (50) NOT NULL,
	[PatientSName] VARCHAR (50) NOT NULL,
	[PatientEmail] VARCHAR (50) NOT NULL,
	[PatientLoginCode] VARCHAR (6) NOT NULL,
	[PatientPass] VARCHAR (256) NOT NULL,
	PRIMARY KEY (PatientID)
)