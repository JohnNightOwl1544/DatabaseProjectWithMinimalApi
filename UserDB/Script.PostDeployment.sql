if not exists (select 1 from dbo.[User])
begin
	insert into dbo.[User] (FirstName, LastName)
	values ('John', 'Jovence'),
	('Jackston', 'Timberlake'),
	('Mary', 'Jones'),
	('Sue', 'Smith')
end