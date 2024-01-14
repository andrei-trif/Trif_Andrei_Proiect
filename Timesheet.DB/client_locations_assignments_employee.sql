INSERT INTO [dbo].[Employee] ([FirstName] ,[LastName])
     VALUES ('Popescu', 'Vasile')
GO

INSERT INTO [dbo].[Client]([Name])
     VALUES ('ABC SRL')
GO

INSERT INTO [dbo].[Location] ([Clientid], [Name], [StreetName], [SteetNumber], [SteetCode], [Building], [City], [County], [Country])
     VALUES (1, 'Punct de lucru', 'Alexandru Vaida Voevod', '51', '400592', 'United Business Center Tower', 'Cluj', 'CJ', 'RO')
GO

INSERT INTO [dbo].[Location] ([Clientid], [Name] ,[StreetName] ,[SteetNumber], [SteetCode], [Building], [City], [County], [Country])
     VALUES (1, 'Sediu', '21 Decembrie 1989', '77', '400124', 'The Office', 'Cluj', 'CJ', 'RO')
GO

INSERT INTO [dbo].[Assignment] ([EmployeeId], [LocationId], [Hours])
     VALUES (1, 1, 2)
GO

INSERT INTO [dbo].[Assignment] ([EmployeeId], [LocationId], [Hours])
     VALUES (1, 2, 6)
GO
