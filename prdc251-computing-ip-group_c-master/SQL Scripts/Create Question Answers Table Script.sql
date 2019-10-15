CREATE TABLE [dbo].[QuestionAnswers]
(
	[QuestionAnswerID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[QuestionID] INT REFERENCES Questions (QuestionID) NOT NULL,
	[QuestionAnswer] VARCHAR(255) NOT NULL
)