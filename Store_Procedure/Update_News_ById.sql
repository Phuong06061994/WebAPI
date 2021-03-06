USE [DBContext]
GO
/****** Object:  StoredProcedure [dbo].[Update_News]    Script Date: 27/05/2020 8:50:42 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Update_News_ById

@Id int,
@Title varChar(255),
@Theme varChar(255),
@Content text,
@CreatedBy varChar(50)

AS
BEGIN
	
	SET NOCOUNT OFF;

   update News set Title = @Title,Content = @Content, Theme = @Theme, CreatedBy = @CreatedBy
   where NewsId = @Id ;
   
END
