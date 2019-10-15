CREATE TABLE [dbo].[Doctor]
(
	[DoctorID] INT NOT NULL PRIMARY KEY,
	[DoctorFName] VARCHAR (50) NOT NULL,
	[DoctorSName] VARCHAR (50) NOT NULL,
	[DoctorEmail] VARCHAR (50) NOT NULL,
	[DoctorPass] VARCHAR (256) NOT NULL,	
)