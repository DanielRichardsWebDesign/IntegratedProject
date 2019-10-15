CREATE TABLE [dbo].[DoctorPatientMatches]
(
	[MatchID] INT IDENTITY (1,1) NOT NULL,
	[DoctorID] INT NOT NULL REFERENCES Doctors(DoctorID),
	[DoctorFName] VARCHAR (50) NOT NULL,
	[DoctorSName] VARCHAR (50) NOT NULL,
	[PatientID] INT NOT NULL REFERENCES Patients(PatientID),
	[PatientFName] VARCHAR (50) NOT NULL,
	[PatientSName] VARCHAR (50) NOT NULL,
	PRIMARY KEY (MatchID)
)