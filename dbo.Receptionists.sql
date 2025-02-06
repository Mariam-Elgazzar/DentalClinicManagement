CREATE TABLE [dbo].[Receptionists] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [Email]    NVARCHAR (MAX) NOT NULL,
    [Phone]    NVARCHAR (MAX) NULL,
    [Password] VARCHAR(50) NOT NULL,
    [IsAdmin]  BIT            NOT NULL,
    CONSTRAINT [PK_Receptionists] PRIMARY KEY CLUSTERED ([Id] ASC)
);

