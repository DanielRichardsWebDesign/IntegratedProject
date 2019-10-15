CREATE TABLE [dbo].[Survey]
(
	[SurveyID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[DoctorID] INT NOT NULL REFERENCES Doctor (DoctorID),
	[PatientID] INT NOT NULL REFERENCES Patients (PatientID),
	[SurveyTitle] VARCHAR (50) NOT NULL,
	[SurveyDesc] VARCHAR (255) NOT NULL,
	[SurveyFreq] VARCHAR (20) NOT NULL,
	[SurveyQuestionAmount] INT NOT NULL,
	[SurveyStatus] VARCHAR (20)
)