GO
DROP TABLE Reservations
DROP TABLE Users
DROP TABLE Busses
DROP TABLE Passwords
DROP TABLE ComCenters

GO
CREATE TABLE Passwords (
id int identity primary key,
password char(255) not null,
created int not null
)
CREATE TABLE Users (
mobile int primary key,
passwordId int foreign key references Passwords(ID) not null,
admin bit not null,
name nvarchar(80) not null,
email nvarchar(255) not null
)
CREATE TABLE ComCenters (
name nvarchar(60) primary key not null,
address nvarchar(120) not null,
contactName nvarchar(80) not null,
contactPhone int not null
)
CREATE TABLE Busses (
regNo nvarchar(7) primary key not null,
comCenter nvarchar(60) foreign key references ComCenters(name) not null
)
CREATE TABLE Reservations(
id int identity primary key not null,
username int foreign key references Users(mobile) not null,
bus nvarchar(7) foreign key references Busses(regNo) not null,
fromDate datetime not null,
toDate datetime not null
)

GO
-- Test data --

-- ComCenters -- 
INSERT INTO ComCenters values ('Lokalcenter Marselis','Marselis Boulevard 94 A, 8000 Aarhus C','Rigmor',24784167);
INSERT INTO ComCenters values ('Sundheds- og Kulturcenter Frederiksbjerg & Langenæs','Ankersgade 21, 8000 Aarhus C','Rigmor',24784167);
INSERT INTO ComCenters values ('Lokalcenter Dalgas','Dalgas Avenue 54, 8000 Aarhus C','Rigmor',24784167);
INSERT INTO ComCenters values ('Lokalcenter Rosenvang','Rosenvangs Allé 76, 8260 Viby J','Rigmor',24784167);

-- Busses
INSERT INTO Busses values ('AB12345','Lokalcenter Marselis')
INSERT INTO Busses values ('BA12345','Sundheds- og Kulturcenter Frederiksbjerg & Langenæs')
INSERT INTO Busses values ('AC12345','Lokalcenter Dalgas')
INSERT INTO Busses values ('CA12345','Lokalcenter Rosenvang')

-- Passwords -- REMEMBER TO REFACTOR FOR HASHES.
INSERT INTO Passwords values ('123456',1400484521)
INSERT INTO Passwords values ('1234567',1400484521)
INSERT INTO Passwords values ('blablabla', 1400580496)
INSERT INTO Passwords values ('000000', 1400580496)

-- Users --
INSERT INTO Users values(20662541,1,1,'Stefan Weibel','Stefan.Weibel@gmail.com')
INSERT INTO Users values (88888888, 2, 1, 'Peter fra Leasy', 'peter@leasy.dk')
INSERT INTO Users values (12345678, 3, 0, 'Lars Licens', 'll@dr.dk')

--Reservations--
INSERT INTO Reservations values(20662541, 'AB12345', '2014-05-20 16:00:00', '2014-05-20 19:00:00')
INSERT INTO Reservations values(20662541, 'AB12345', '2014-05-20 23:00:00', '2014-05-20 23:20:00')