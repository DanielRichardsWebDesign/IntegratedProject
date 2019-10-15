CREATE TABLE [dbo].[Rules]
(
    [RuleID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    [QuestionID] INT NOT NULL REFERENCES Questions (QuestionID),
    [RuleParam] INT NOT NULL
)
