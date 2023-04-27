USE [BooksDB]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 4/27/2023 11:21:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[BookTitle] [nvarchar](100) NOT NULL,
	[AuthorName] [nvarchar](100) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[ShelfID] [int] NOT NULL,
	[RackID] [int] NOT NULL,
	[IsAvialalbe] [bit] NOT NULL,
	[DeleteFlag] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Racks]    Script Date: 4/27/2023 11:21:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Racks](
	[code] [int] NOT NULL,
	[RackID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shelfs]    Script Date: 4/27/2023 11:21:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shelfs](
	[code] [int] NOT NULL,
	[ShelfID] [int] NOT NULL,
	[RackID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddBook]    Script Date: 4/27/2023 11:21:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 create procedure [dbo].[AddBook]  
 
@bookTitle nvarchar(122) ,
@AuthorName nvarchar(122) ,
@Price nvarchar(122) ,
@ShelfID nvarchar(122) ,
@RackID nvarchar(122)  ,
@Availability bit ,
@DeleteFlag bit  



  as

insert into Books values 
(@bookTitle,@AuthorName,@Price,@ShelfID,@RackID,@Availability,@DeleteFlag)
GO
/****** Object:  StoredProcedure [dbo].[DeleteBook]    Script Date: 4/27/2023 11:21:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create procedure [dbo].[DeleteBook]  
  @bookID nvarchar(122)  
   
  as

update   books  set DeleteFlag=1
  where BookId=@bookID

GO
/****** Object:  StoredProcedure [dbo].[GetAllRacks]    Script Date: 4/27/2023 11:21:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[GetAllRacks]
as 
select * from Racks
GO
/****** Object:  StoredProcedure [dbo].[GetAllShelves]    Script Date: 4/27/2023 11:21:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetAllShelves]
as 
select * from Shelfs

GO
/****** Object:  StoredProcedure [dbo].[UpdateBook]    Script Date: 4/27/2023 11:21:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE  procedure [dbo].[UpdateBook]  
  @bookID nvarchar(122) ,
  @bookTitle nvarchar(122) ,
  @AuthorName nvarchar(122) ,
  @Availability nvarchar(122),
  @Price nvarchar(122) ,
  @ShelfID nvarchar(122) ,
  @RackID nvarchar(122)  
  as

  update books set BookTitle=@bookTitle,AuthorName=@AuthorName,IsAvialalbe=@Availability,Price=@Price,ShelfID=@ShelfID ,RackID=@RackID
  where BookId=@bookID
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 --drop procedure SearchBooks
 CREATE procedure [dbo].[SearchBooks]  
  @bookID nvarchar(122)='A',
  @bookTitle nvarchar(122)='A',
  @AuthorName nvarchar(122)='A',
  @Availability nvarchar(122)='A',
  @Price nvarchar(122)='A',
  @ShelfID nvarchar(122)='A',
  @DeleteFlag nvarchar(122)='A',
  @RackID nvarchar(122)='A'
  as 
select * from Books
where DeleteFlag=0

and BookId =case when @bookID!='A' then @bookID else BookId end 
and BookTitle =case when @bookTitle!='A' then @bookTitle else BookTitle end 
and AuthorName =case when @AuthorName!='A' then @AuthorName else AuthorName end 
and IsAvialalbe =case when @Availability!='A' then @Availability else IsAvialalbe end 
and Price =case when @Price!='A' then @Price else Price end 
and ShelfID =case when @ShelfID!='A' then @ShelfID else ShelfID end 
and DeleteFlag =case when @DeleteFlag!='A' then @DeleteFlag else DeleteFlag end 
and RackID =case when @RackID!='A' then @RackID else RackID end 
GO
