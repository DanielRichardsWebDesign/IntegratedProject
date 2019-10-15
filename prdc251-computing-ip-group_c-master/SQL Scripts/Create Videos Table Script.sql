CREATE TABLE [dbo].[Videos]
(
	VideoID INT IDENTITY (1,1) PRIMARY KEY,
	QuestionID INT FOREIGN KEY REFERENCES Questions(QuestionID),
	VideoName VARCHAR(100),
	VideoPath VARCHAR(200),
)