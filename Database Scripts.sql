
CREATE DATABASE BooksDB;



create table Books   
(
BookId			int IDENTITY(1,1),  
BookTitle     	 nvarchar(100) not null,
AuthorName    	 nvarchar(100) not null,
Price         	 decimal(10,2) not null,
ShelfID       	 int not null,
RackID       	 int not null,
IsAvialalbe   	 bit not null ,
DeleteFlag       bit null ,

)

 create table Shelfs  
(
code int not null,
ShelfID       	 int not null,
RackID       	 int not null, 
 
)


create table Racks  
(
code int not null, 
RackID       	 int not null, 
 
)



 
create procedure GetAllRacks
as 
select * from Racks

 
 --drop procedure SearchBooks
 alter procedure SearchBooks  
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

 
 ---------
 create procedure UpdateBook  
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


   -----------------
 create procedure DeleteBook  
  @bookID nvarchar(122)  
   
  as

update   books  set DeleteFlag=1
  where BookId=@bookID

   


 
 alter procedure AddBook  
 
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
