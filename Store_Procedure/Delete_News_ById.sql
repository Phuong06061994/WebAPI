
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Delete_News_ById
@Id int
AS
BEGIN
	
	SET NOCOUNT ON;

   delete from News where NewsId = @Id ;
   
END
