  ------------------------------------------------
-- Drops old DB if it exists
------------------------------------------------
USE [master]
GO
IF EXISTS (SELECT * FROM sysdatabases WHERE name='Flockbuster')
BEGIN
	ALTER DATABASE [Flockbuster] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP database Flockbuster
END
GO

------------------------------------------------
-- New DB
------------------------------------------------
CREATE DATABASE Flockbuster
GO
------------------------------------------------
-- Tables
------------------------------------------------
USE Flockbuster

CREATE TABLE Movies (
	[ID] INT IDENTITY PRIMARY KEY,
	[Title] NVARCHAR(255) NOT NULL,
	[Age Rating] INT NOT NULL,
	[TTW] INT NOT NULL,
	[Release date] DATE NOT NULL,
	[Price] INT NOT NULL,
	[Img] VARCHAR(255) DEFAULT '/pics/Movies/template.png'
)
CREATE TABLE Users (
	[ID] INT IDENTITY PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
	[Age] INT NOT NULL,
    [E-Mail] NVARCHAR(255) NOT NULL,
	[Password] VARBINARY(64) NOT NULL,
	[Balance] INT DEFAULT 0,
	[Admin] BIT DEFAULT 0,
	[Img] VARCHAR(255) DEFAULT '/pics/Users/template.png'
)
CREATE TABLE UserBorrowedMovies (
	[ID] INT IDENTITY PRIMARY KEY,
	[Movie ID] INT NOT NULL,
	[User ID] INT NOT NULL,
	[IsBorrowed] BIT DEFAULT 0,
	[WasBorrowed] BIT DEFAULT 0,
	[Return Date] DATE NOT NULL,
	FOREIGN KEY ([Movie ID]) REFERENCES Movies(ID),
	FOREIGN KEY ([User ID]) REFERENCES Users(ID)
)
CREATE TABLE Genres (
	[ID] INT IDENTITY PRIMARY KEY,
	[Genre] NVARCHAR(255) NOT NULL
)
CREATE TABLE MovieGenreRelation (
	[Movie ID] INT,
	[Genre ID] INT,
	FOREIGN KEY ([Movie ID]) REFERENCES Movies(ID),
	FOREIGN KEY ([Genre ID]) REFERENCES Genres(ID)
)
GO
------------------------------------------------
-- View
------------------------------------------------
CREATE VIEW GetMovieID AS
SELECT ID FROM (
	SELECT *, ROW_NUMBER() OVER (ORDER BY ID DESC) AS rn
	FROM Movies
) AS subquery
WHERE rn = 1;
GO
------------------------------------------------
-- Prodcedures
------------------------------------------------
CREATE PROCEDURE CreateGenre
AS
Begin
	INSERT INTO Genres (Genre) VALUES 
	('Comedy'),
	('Horror'),
	('Action'),
	('Fantasy'),
	('Drama'),
	('Romance'),
	('Science Fiction'),
	('Thriller'),
	('Western'),
	('Animation'),
	('Adventure'),
	('Mystery'),
	('Crime'),
	('Historical'),
	('Musical'),
	('Documentary'),
	('Historical'),
	('Noir'),
	('Art'),
	('Melodrama')
END
GO
CREATE PROCEDURE CreateUser
(@Name NVARCHAR(255), @Age INT, @Mail NVARCHAR(255), @Password NVARCHAR(50))
AS
Begin
	DECLARE @HashedPassword VARBINARY(64) = HASHBYTES('SHA2_256', @Password);
	INSERT INTO [Users]
	([Name], [Age], [E-Mail], [Password]) 
	VALUES 
	(@Name, @Age, @Mail, @HashedPassword)
END
GO
CREATE PROCEDURE CreateMovie
(@Title NVARCHAR(255),@AgeRating INT,@TTW INT,@ReleaseDate DATE, @Price INT)
AS
Begin
	INSERT INTO Movies
	([Title], [Age Rating], [TTW], [Release date], [Price]) 
	VALUES 
	(@Title, @AgeRating, @TTW, @ReleaseDate, @Price)
END
GO
CREATE PROCEDURE CreateGenreConnection
(@Genre_ID INT)
AS
BEGIN
	INSERT INTO MovieGenreRelation
	([Movie ID], [Genre ID]) 
	VALUES 
	((SELECT * FROM GetMovieID), @Genre_ID)
END
GO
-- Create
CREATE PROCEDURE GetAllUsers
AS
Begin
	SELECT * FROM [Users]
END
GO
CREATE PROCEDURE GetAllMovies
AS
Begin
	SELECT * FROM [Movies]
END
GO
CREATE PROCEDURE GetAllBorrowedMovies
AS
Begin
	SELECT * FROM [UserBorrowedMovies]
END
GO
CREATE PROCEDURE GetAllGenres
AS
Begin
	SELECT * FROM [Genres]
END
GO
-- Get alls
CREATE PROCEDURE GetMovieGenre
(@Movie_ID INT)
AS
BEGIN
	SELECT 
		Genres.Genre
	FROM 
		MovieGenreRelation
	JOIN Genres ON MovieGenreRelation.[Genre ID] = Genres.ID
	JOIN Movies ON MovieGenreRelation.[Movie ID] = Movies.ID
	WHERE MovieGenreRelation.[Movie ID] = @Movie_ID;
END
GO
-- Join
CREATE PROCEDURE DeleteMovie
(@MovieID INT)
AS
BEGIN
	DELETE FROM MovieGenreRelation WHERE [Movie ID] = @MovieID
	DELETE FROM UserBorrowedMovies WHERE [Movie ID] = @MovieID
	DELETE FROM Movies WHERE ID = @MovieID
END
GO
CREATE PROCEDURE DeleteUser
(@UserID INT)
AS
BEGIN
	DELETE FROM UserBorrowedMovies WHERE [User ID] = @UserID
	DELETE FROM Users WHERE ID = @UserID
END
GO
-- Delete
CREATE PROCEDURE Admins
(@UserId INT, @Admin BIT)
AS
BEGIN
	UPDATE Users
	SET [Admin] = @Admin
	WHERE ID = @UserId
END
GO
-- Admins
CREATE PROCEDURE GetBorrowedMoviesById
(@UserId INT)
AS
Begin
	SELECT * FROM [UserBorrowedMovies] WHERE (WasBorrowed = 1 OR IsBorrowed = 1) AND [User ID] = @UserId
END
GO
CREATE PROCEDURE GetGenreById
(@GenreId INT)
AS
Begin
	SELECT * FROM Genres WHERE ID = @GenreId
END
GO
CREATE PROCEDURE GetMovieById
(@MovieId INT)
AS
Begin
	SELECT * FROM Movies WHERE ID = @MovieId
END
GO
CREATE PROCEDURE GetUserById
(@UserId INT)
AS
Begin
	SELECT * FROM Users WHERE ID = @UserId
END
GO
-- Get By Id
CREATE PROCEDURE GetUserByMail
(@Mail NVARCHAR(255))
AS
BEGIN
	SELECT * FROM Users WHERE [E-Mail] = @Mail
END
GO
CREATE PROCEDURE GetMovieByTitle
(@Title NVARCHAR(255))
AS
BEGIN
	SELECT ID FROM Movies WHERE [Title] = @Title
END
GO
-- Get By
CREATE PROCEDURE GetLogin
(@Mail NVARCHAR(255), @Password NVARCHAR(50))
AS
Begin
	DECLARE @StoredHash VARBINARY(64) =
		(SELECT [Password] FROM Users WHERE [E-Mail] = @Mail)
	DECLARE @EnteredHash VARBINARY(64) = HASHBYTES('SHA2_256', @Password);
	
	IF @EnteredHash = @StoredHash
		SELECT * FROM Users WHERE [E-Mail] = @Mail
END
GO
CREATE PROCEDURE VerifyUser
(@UserId INT, @Password NVARCHAR(50))
AS
BEGIN
    DECLARE @StoredHashedPassword VARBINARY(64);
        SELECT @StoredHashedPassword = [Password]
    FROM [Users]
    WHERE [ID] = @UserId;
    IF HASHBYTES('SHA2_256', @Password) = @StoredHashedPassword
    BEGIN
        SELECT 'Edit successful' AS Result;
    END
    ELSE
    BEGIN
        SELECT 'Edit failed' AS Result;
    END
END
GO
-- Get Login
CREATE PROCEDURE UpdateMovie
(@MovieID INT, @title NVARCHAR(255), @AgeRating INT, @TTW INT, @ReleaseDate DATE, @Price INT)
AS
BEGIN
	UPDATE Movies
	SET [Title] = @title, [Age Rating] = @AgeRating, [TTW] = @TTW, [Release date] = @ReleaseDate, Price = @Price
	WHERE ID = @MovieID
END
GO
CREATE PROCEDURE UpdateUser
(@UserID INT, @Name NVARCHAR(255), @Age INT, @Mail NVARCHAR(255))
AS
BEGIN
	UPDATE Users
	SET [Name] = @Name, Age = @Age, [E-Mail] = @Mail
	WHERE ID = @UserID
END
GO
CREATE PROCEDURE UpdateUserBalancePlus
(@UserID INT, @Balance INT)
AS
BEGIN
	DECLARE @CurrentBalance INT = (SELECT [Balance] FROM Users WHERE ID = @UserID);
	DECLARE @NewBalance INT = @CurrentBalance + @Balance
	UPDATE Users
	SET [Balance] = @NewBalance
	WHERE ID = @UserID
END
GO
CREATE PROCEDURE UpdateUserBalanceMinus
(@UserID INT, @Balance INT)
AS
BEGIN
	DECLARE @CurrentBalance INT = (SELECT [Balance] FROM Users WHERE ID = @UserID);
	DECLARE @NewBalance INT = @CurrentBalance - @Balance
	UPDATE Users
	SET [Balance] = @NewBalance
	WHERE ID = @UserID
END
GO
CREATE PROCEDURE UpdateUserPassword
(@UserID INT, @Password NVARCHAR(50))
AS
BEGIN
	DECLARE @HashedPassword VARBINARY(64) = HASHBYTES('SHA2_256', @Password);
	UPDATE Users
	SET [Password] = @HashedPassword
	WHERE ID = @UserID
END
GO
-- Update
CREATE PROCEDURE UploadUserPicture
(@UserID INT, @Img VARCHAR(255))
AS
BEGIN
	UPDATE Users
	SET [Img] = @Img
	WHERE ID = @UserID
END
GO
CREATE PROCEDURE UploadMoviePicture
(@MovieID INT, @Img VARCHAR(255))
AS
BEGIN
	UPDATE Movies
	SET [Img] = @Img
	WHERE ID = @MovieID
END
GO
-- Upload img
CREATE PROCEDURE BorrowMovie
(@MovieID INT, @UserID INT, @ReturnDate DATE)
AS
BEGIN
	INSERT INTO UserBorrowedMovies ([Movie ID], [User ID], [IsBorrowed], [Return Date]) VALUES (@MovieID, @UserID, 1, @ReturnDate)
END
GO
CREATE PROCEDURE ReturnMovie
(@MovieID INT, @UserID INT)
AS
BEGIN
	UPDATE UserBorrowedMovies
	SET IsBorrowed = 0, WasBorrowed = 1
	WHERE [Movie ID] = @MovieID AND [User ID] = @UserID
END
GO
-- Borrow

------------------------------------------------
-- Executable Commands
------------------------------------------------
EXEC CreateGenre
EXEC CreateUser @Name = 'Admin', @age = 999, @Mail = 'Admin@Admin.com', @Password = 'Admin1234!'
EXEC Admins @UserId = 1, @Admin = 1
/*
EXEC CreateUser @Name = 'Test Account', @Age = 18, @Mail = 'Test@test.com', @Password = 'Test1234!'
EXEC CreateMovie @title = 'Test movie', @AgeRating = 18, @TTW = 9000, @ReleaseDate = '2018-04-04', @Price = 75
EXEC CreateGenreConnection @Genre_ID = 1
EXEC CreateGenreConnection @Genre_ID = 3
EXEC CreateGenreConnection @Genre_ID = 6
EXEC UpdateUserBalancePlus @UserID = 2, @Balance = 255
*/