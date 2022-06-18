CREATE TABLE TOURNAMENT(
[Key] VARCHAR(6) NOT NULL UNIQUE,
[Name] VARCHAR(30) NOT NULL ,
[InitialDate] DATE NOT NULL ,
[InitialHour] TIME NOT NULL,
[FinalDate] DATE NOT NULL ,
[FinalHour] TIME NOT NULL,
Budget float NOT NULL,
Rules VARCHAR(1000),
PRIMARY KEY([Key]));

CREATE TABLE COUNTRY(
[Name] VARCHAR(30) NOT NULL UNIQUE,
Photo varchar(MAX) not null,
PRIMARY KEY ([Name]));

create Table [USER](
Username varchar(30) not null,
[Password] varchar(256) not null,
Email varchar(30) unique,
TeamsName varchar(30) not null,
TeamsLogo varchar(MAX) not null,
CountryName varchar(30) not null,
PrivateLeagueName varchar(30),
PRIMARY KEY (Email),
FOREIGN KEY (CountryName) REFERENCES Country([Name])); 

CREATE TABLE RACE(
[Name] VARCHAR(30) NOT NULL,
Country VARCHAR(30) NOT NULL,
[State] int NOT NULL,
[TrackName] VARCHAR(30) NOT NULL,
[InitialDate] DATE NOT NULL ,
[InitialHour] TIME NOT NULL,
[FinalDate] DATE NOT NULL ,
[FinalHour] TIME NOT NULL,
TournamentKey VARCHAR(6) NOT NULL,
PRIMARY KEY ([Name], TournamentKey),
FOREIGN KEY (TournamentKey) REFERENCES Tournament ([Key]),
FOREIGN KEY (Country) REFERENCES Country ([Name]));

Create Table [PUBLICLEAGUE](
UserEmail varchar(30),
TournamentKey varchar(6),
Primary KEY (UserEmail,TournamentKey),
FOREIGN KEY (TournamentKey) REFERENCES Tournament ([Key]),
FOREIGN KEY (UserEmail) REFERENCES dbo.[User](Email));


Create Table [PRIVATELEAGUE](
OwnerEmail varchar(30),
TournamentKey varchar(6),
PrivateLeagueKey varchar(6),
[Name] varchar(30),
maxUser int,
Primary KEY ([Name]),
FOREIGN KEY (TournamentKey) REFERENCES Tournament ([Key]),
FOREIGN KEY (OwnerEmail) REFERENCES dbo.[User](Email));

Create Table REALTEAMS(
[Name] varchar(30) not null,
Price float not null,
Photo varchar(MAX) not null,
Logo varchar(MAX) not null,
PRIMARY KEY ([Name])
);

Create Table SubteamPoints(
TournamentKey varchar(6),
SubTeamId int,
points int
PRIMARY KEY (TournamentKey,SubTeamId),
FOREIGN KEY (TournamentKey) REFERENCES Tournament ([Key]));

Create Table SUBTEAMS(
ID int NOT NULL,
[Name] varchar(30) not null,
UserEmail varchar(30) not null,
RealTeamsName varchar(30) not null,
CreationDate Date,
CreationHour Time,
PRIMARY KEY (ID),
FOREIGN KEY (UserEmail) REFERENCES [USER](Email),
FOREIGN KEY (RealTeamsName) REFERENCES REALTEAMS([Name]));

Create Table PILOT(
ID varchar(11) NOT NULL,
Firstname varchar(30) not null,
Lastname varchar(30) not null,
Price float not null,
HotstreakClassification int,
HotstreakRace int,
Photo varchar(MAX) not null,
CountryName varchar(30) not null,
RealTeamsName varchar(30),
PRIMARY KEY (ID),
FOREIGN KEY (CountryName) REFERENCES Country([Name]),
FOREIGN KEY (RealTeamsName) REFERENCES REALTEAMS([Name]));

Create Table HAS_PILOT(
SubTeamsID int NOT NULL,
PilotID varchar(11) NOT NULL,
dummyData int,
PRIMARY KEY (SubTeamsID , PilotID),
FOREIGN KEY (SubTeamsID) REFERENCES SUBTEAMS(id),
FOREIGN KEY (PilotID) REFERENCES PILOT(id)
);

Create Table PilotRace(
PilotID varchar(11) NOT NULL,
[Name] VARCHAR(30) NOT NULL,
TournamentKey VARCHAR(6) not null,
points int,
PRIMARY KEY ([Name], TournamentKey, PilotID),
FOREIGN KEY (PilotID) REFERENCES PILOT(id),
FOREIGN KEY ([Name], TournamentKey) REFERENCES RACE([Name],TournamentKey)
);

Create Table RealTeamRace(
RealTeamName varchar(30) NOT NULL,
[Name] VARCHAR(30) NOT NULL,
TournamentKey VARCHAR(6) not null,
points int,
PRIMARY KEY (RealTeamName, TournamentKey, [Name]),
FOREIGN KEY (RealTeamName) REFERENCES RealTeams([Name]),
FOREIGN KEY ([Name], TournamentKey) REFERENCES RACE([Name],TournamentKey)
);

Alter table [USER]
add constraint PrivateLeague_User_key
FOREIGN KEY (PrivateLeagueName) REFERENCES [PRIVATELEAGUE]([Name]);

Alter table SubteamPoints
add constraint SubteamKey
FOREIGN KEY (SubTeamId) REFERENCES SUBTEAMS(ID);



INSERT INTO COUNTRY
VALUES ('FRANCIA','C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Bandera Francia.png');
INSERT INTO COUNTRY
VALUES ('ESPAÑA','C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Bandera_de_España.png');
INSERT INTO COUNTRY
VALUES ('PORTUGAL','C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Bandera Portugal.png');
INSERT INTO COUNTRY
VALUES ('ARABIA SAUDI','C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Bandera arabia saudi.jpg');


INSERT INTO TOURNAMENT
VALUES ('QWE123','GP de ARABIA SAUDI',  '2022-03-25', '00:00:00', '2022-03-27', '9:00:00',1000, 'Ver reglas');
INSERT INTO TOURNAMENT
VALUES ('QWE125','GP de Francia',  '2022-10-25', '00:00:00', '2022-12-27', '9:00:00',23, 'Ver reglas');

INSERT INTO TOURNAMENT
VALUES ('QWE126','GP de Francia',  '2030-10-25', '00:00:00', '2030-12-27', '9:00:00',23, 'Ver reglas');

INSERT INTO Race
VALUES ('Street Circuit', 'ARABIA SAUDI',0,'Jeddah', '2022-03-26', '17:00:00', '2022-03-27', '4:00:00', 'QWE123');

INSERT INTO dbo.[User]
VALUES('Briwag','3f21a8490cef2bfb60a9702e9d2ddb7a805c9bd1a263557dfd51a7d0e9dfa93e','briwag88@hotmail.com','Los tornados locos','C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\briwag88@hotmail.com.png','FRANCIA',null)

Insert into PUBLICLEAGUE
Values('briwag88@hotmail.com','QWE123')

Insert into PUBLICLEAGUE
Values('briwag88@hotmail.com','QWE125')
Insert into PUBLICLEAGUE
Values('briwag88@hotmail.com','QWE126')

Insert into REALTEAMS
Values('Redbull', 6, 'C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Redbull carro.png','C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Red-Bull-Logo.png')
Insert into REALTEAMS
Values('Mclaren', 5, 'C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Mclaren carro.png','C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\McLaren-Logo.png')
Insert into REALTEAMS
Values('Alpine', 7, 'C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Alpine carro.png','C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Alpine Logo.png')

Insert into RealTeamRace values ('Redbull', 'Street Circuit', 'QWE123', 5);
Insert into RealTeamRace values ('Mclaren', 'Street Circuit', 'QWE123', 6);
Insert into RealTeamRace values ('Alpine', 'Street Circuit', 'QWE123', 4);

insert into SUBTEAMS
Values(1,'Equipo Supermega Corredor', 'briwag88@hotmail.com', 'Redbull','2022-03-25','00:00:00')

insert into SUBTEAMS
Values(2,'Equipo Malos Corredores', 'briwag88@hotmail.com', 'Alpine','2022-03-25','00:00:00')

insert into SUBTEAMS
Values(3,'Equipo Supermega Corredor', 'briwag88@hotmail.com', 'Redbull','2022-10-25','00:00:00')

insert into SUBTEAMS
Values(4,'Equipo Malos Corredores', 'briwag88@hotmail.com', 'Alpine','2022-10-25','00:00:00')

insert into SUBTEAMS
Values(5,'Equipo Supermega Corredor', 'briwag88@hotmail.com', 'Redbull','2030-10-25','00:00:00')

insert into SUBTEAMS
Values(6,'Equipo Malos Corredores', 'briwag88@hotmail.com', 'Alpine','2030-10-25','00:00:00')

Insert into PILOT
Values('XFIA-P-1077','Fernando', 'Alonso', 2,0,0,'C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Piloto Fernando Alonso Alpine.png', 'FRANCIA','Alpine')

Insert into PILOT
Values('XFIA-P-1081','Sebastian', 'Vettel', 5,0,0,'C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Piloto Sebastian vettel Redbull.png','FRANCIA','Redbull')

Insert into PILOT
Values('XFIA-P-1099','Daniel', 'Ricciardo', 2,0,0,'C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Piloto Daniel Ricciardo Mclaren.png','FRANCIA','Mclaren')

Insert into PILOT
Values('XFIA-P-1043','Sergio', 'Perez', 7,0,0,'C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Piloto Sergio Perez redbull.png','FRANCIA','Redbull')

Insert into PILOT
Values('XFIA-P-1111','Lando', 'Norris', 2,0,0,'C:\Users\briwa\Documents\Github\X-FIA\REST-API-XFIA\bin\Debug\net6.0\Files\Images\Piloto Lando Norris McLaren.png','FRANCIA','Mclaren')

Insert into PilotRace values ('XFIA-P-1077', 'Street Circuit', 'QWE123', 5);
Insert into PilotRace values ('XFIA-P-1081', 'Street Circuit', 'QWE123', 4);
Insert into PilotRace values ('XFIA-P-1099', 'Street Circuit', 'QWE123', 3);
Insert into PilotRace values ('XFIA-P-1043', 'Street Circuit', 'QWE123', 2);
Insert into PilotRace values ('XFIA-P-1111', 'Street Circuit', 'QWE123', 1);

insert into HAS_PILOT
Values(1,'XFIA-P-1099',0)
insert into HAS_PILOT
Values(1,'XFIA-P-1081',0)
insert into HAS_PILOT
Values(1,'XFIA-P-1077',0)
insert into HAS_PILOT
Values(1,'XFIA-P-1043',0)
insert into HAS_PILOT
Values(1,'XFIA-P-1111',0)

insert into HAS_PILOT
Values(2,'XFIA-P-1099',0)
insert into HAS_PILOT
Values(2,'XFIA-P-1081',0)
insert into HAS_PILOT
Values(2,'XFIA-P-1077',0)
insert into HAS_PILOT
Values(2,'XFIA-P-1043',0)
insert into HAS_PILOT
Values(2,'XFIA-P-1111',0)

insert into HAS_PILOT
Values(3,'XFIA-P-1099',0)
insert into HAS_PILOT
Values(3,'XFIA-P-1081',0)
insert into HAS_PILOT
Values(3,'XFIA-P-1077',0)
insert into HAS_PILOT
Values(3,'XFIA-P-1043',0)
insert into HAS_PILOT
Values(3,'XFIA-P-1111',0)

insert into HAS_PILOT
Values(4,'XFIA-P-1099',0)
insert into HAS_PILOT
Values(4,'XFIA-P-1081',0)
insert into HAS_PILOT
Values(4,'XFIA-P-1077',0)
insert into HAS_PILOT
Values(4,'XFIA-P-1043',0)
insert into HAS_PILOT
Values(4,'XFIA-P-1111',0)

insert into HAS_PILOT
Values(5,'XFIA-P-1099',0)
insert into HAS_PILOT
Values(5,'XFIA-P-1081',0)
insert into HAS_PILOT
Values(5,'XFIA-P-1077',0)
insert into HAS_PILOT
Values(5,'XFIA-P-1043',0)
insert into HAS_PILOT
Values(5,'XFIA-P-1111',0)

insert into HAS_PILOT
Values(6,'XFIA-P-1099',0)
insert into HAS_PILOT
Values(6,'XFIA-P-1081',0)
insert into HAS_PILOT
Values(6,'XFIA-P-1077',0)
insert into HAS_PILOT
Values(6,'XFIA-P-1043',0)
insert into HAS_PILOT
Values(6,'XFIA-P-1111',0)

go
Create TRIGGER AfterINSERTUser on [dbo].[USER]
after INSERT 
AS 
BEGIN
SET NOCOUNT on;
select T.[KEY], ROW_NUMBER() over(ORDER BY T.[KEY]) as ROW into TempTable
from [dbo].[TOURNAMENT] as T where T.[InitialDate]> CONVERT(DATE, GETDATE()) OR T.[InitialDate] = CONVERT(DATE, GETDATE()) and T.[InitialHour] > CONVERT(TIME, GETDATE())
DECLARE @COUNTER INT = (SELECT MAX(ROW) FROM TempTable);
DECLARE @ROW INT;
WHILE (@COUNTER != 0)
begin
	SELECT @ROW = ROW
    FROM TempTable
    WHERE ROW = @COUNTER
    ORDER BY ROW DESC

	Insert into [dbo].[PublicLeague]([UserEmail], TournamentKey) values ((select Email from INSERTED), (Select [Key] from TempTable where ROW = @ROW))

	SET @COUNTER = @ROW -1

end
drop table TempTable
END
GO

Create TRIGGER AfterINSERTTournament on [dbo].TOURNAMENT
after INSERT 
AS 
BEGIN
SET NOCOUNT on;
select U.[Email], ROW_NUMBER() over(ORDER BY U.[Email]) as ROW into TempTable
from [dbo].[USER] as U
DECLARE @COUNTER INT = (SELECT MAX(ROW) FROM TempTable);
DECLARE @ROW INT;
WHILE (@COUNTER != 0)
begin
	SELECT @ROW = ROW
    FROM TempTable
    WHERE ROW = @COUNTER
    ORDER BY ROW DESC

	Insert into [dbo].[PublicLeague](TournamentKey, [UserEmail]) values ((select [Key] from INSERTED), (Select Email from TempTable where ROW = @ROW))

	SET @COUNTER = @ROW -1

end
drop table TempTable
END
GO 