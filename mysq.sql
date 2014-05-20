DROP TABLE Reservations;
DROP TABLE Users;
DROP TABLE Busses;
DROP TABLE Passwords;
DROP TABLE ComCenters;


CREATE TABLE Passwords (
id int AUTO_INCREMENT primary key,
password char(255) not null,
created int not null
);

CREATE TABLE Users (
mobile int primary key,
passwordId int not null,
foreign key (passwordId) references Passwords(ID),
admin bit not null,
name varchar(80) not null,
email varchar(255) not null
);

CREATE TABLE ComCenters (
name varchar(60) primary key not null,
address varchar(120) not null,
contactName varchar(80) not null,
contactPhone int not null
);

CREATE TABLE Busses (
regNo varchar(7) primary key not null,
comCenter varchar(60) not null,
foreign key (comCenter) references ComCenters(name)
);

CREATE TABLE Reservations(
id int AUTO_INCREMENT primary key not null,
username int not null,
foreign key (username) references Users(mobile),
bus varchar(7) not null,
foreign key (bus) references Busses(regNo),
fromDate datetime not null,
toDate datetime not null
);
-- Test data --

-- ComCenters -- 
INSERT INTO ComCenters VALUES ('Lokalcenter Marselis','Marselis Boulevard 94 A, 8000 Aarhus C','Rigmor',24784167);
INSERT INTO ComCenters VALUES ('Sundheds- og Kulturcenter Frederiksbjerg & Langenæs','Ankersgade 21, 8000 Aarhus C','Rigmor',24784167);
INSERT INTO ComCenters VALUES ('Lokalcenter Dalgas','Dalgas Avenue 54, 8000 Aarhus C','Rigmor',24784167);
INSERT INTO ComCenters VALUES ('Lokalcenter Rosenvang','Rosenvangs Allé 76, 8260 Viby J','Rigmor',24784167);

-- Busses
INSERT INTO Busses VALUES ('AB12345','Lokalcenter Marselis');
INSERT INTO Busses VALUES ('BA12345','Sundheds- og Kulturcenter Frederiksbjerg & Langenæs');
INSERT INTO Busses VALUES ('AC12345','Lokalcenter Dalgas');
INSERT INTO Busses VALUES ('CA12345','Lokalcenter Rosenvang');

-- Passwords -- REMEMBER TO REFACTOR FOR HASHES.
INSERT INTO Passwords (password, created) VALUES ('123456',1400484521);
INSERT INTO Passwords (password, created) VALUES ('1234567',1400484521);

-- Users --
INSERT INTO Users VALUES (20662541,1,1,'Stefan Weibel','Stefan.Weibel@gmail.com');
