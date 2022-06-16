CREATE PROCEDURE [dbo].[spUser_GetAll]

AS

begin
	Select Id, FirstName, LastName
	from dbo.[User];
end