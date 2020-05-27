
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Create_News

@Title varChar(255),
@Theme varChar(255),
@Content text,
@CreatedBy varChar(50),

@id int output
	
AS
BEGIN
	
	SET NOCOUNT ON;

   insert into News(Title, Content, Theme, CreatedBy) values (@Title,@Content,@Theme, @CreatedBy);
   set @id = SCOPE_IDENTITY();
END
GO
