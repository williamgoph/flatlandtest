USE [test]

CREATE TABLE [dbo].[entity](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[created] [datetime] NOT NULL DEFAULT(GETDATE()),
	[type] [varchar](60) NULL,
	[content] [text] NULL
)

GO


CREATE PROCEDURE sp_query_GetEntityByType
	@type VARCHAR(60)
AS
BEGIN

SELECT * FROM entity WHERE RTRIM(LTRIM(type))=RTRIM(LTRIM(@type))

END
GO
