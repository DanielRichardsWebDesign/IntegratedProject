CREATE TABLE [dbo].[PatientSurveyAnswers]
(
	[PatientSurveyAnswerID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[PatientID] INT NOT NULL REFERENCES Patients (PatientID),
	[QuestionID] INT NOT NULL REFERENCES Questions (QuestionID),
	[QuestionAnswerID] INT NOT NULL REFERENCES QuestionAnswers (QuestionAnswerID),
	[PatientSurveyAnswer] VARCHAR NOT NULL,
	[TimeStarted] DATETIME NOT NULL,
	[TimeSubmitted] DATETIME NOT NULL
)