CREATE TABLE [dbo].[Quotes]
(
	[Id] INT NOT NULL IDENTITY , 
    [UserId] NVARCHAR(36) NOT NULL, 
    [Amount] MONEY NOT NULL, 
    CONSTRAINT [PK_Quotes] PRIMARY KEY ([Id])
)
