USE [DBContext]
GO
/****** Object:  StoredProcedure [dbo].[Get_News_All]    Script Date: 5/29/2020 2:02:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE Get_User_All
AS
BEGIN

	SET NOCOUNT ON;
	SELECT Id , UserName, Email, PhoneNumber, Address FROM AspNetUsers

END
