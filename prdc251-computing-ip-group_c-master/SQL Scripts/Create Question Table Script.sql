CREATE TABLE [dbo].[Questions]
(
	[QuestionID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[SurveyID] INT NOT NULL REFERENCES Survey (SurveyID),
	[Question] VARCHAR(max) NOT NULL,
	[QuestionType] VARCHAR(50) NOT NULL 
)