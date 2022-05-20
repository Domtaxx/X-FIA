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
PRIMARY KEY ([Name]));

Create Table [USER](
Firstname varchar(30) not null,
Lastname varchar(30) not null,
[Password] varchar(256) not null,
InitialDate DATE NOT NULL ,
Email varchar(30) unique,
TeamsName varchar(30) not null,
TeamsBudget float not null,
TeamsLogo varchar(256) not null,
CountryName varchar(30) not null,
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

Create Table REALTEAMS(
[Name] varchar(30) not null,
Price float not null,
Photo varchar(256) not null,
PRIMARY KEY ([Name]),
);

Create Table SUBTEAMS(
ID int NOT NULL IDENTITY,
[Name] varchar(30) not null,
Budget float NOT NULL,
UserEmail varchar(30) not null,
RealTeamsName varchar(30) not null,
PRIMARY KEY (ID),
FOREIGN KEY (UserEmail) REFERENCES [USER](Email),
FOREIGN KEY (RealTeamsName) REFERENCES REALTEAMS([Name]));

Create Table PILOT(
ID int NOT NULL IDENTITY,
Firstname varchar(30) not null,
Lastname varchar(30) not null,
Price float not null,
Logo varchar(256) not null,
CountryName varchar(30) not null,
SubTeamsID int NOT NULL,
RealTeamsName varchar(30) not null,
PRIMARY KEY (ID),
FOREIGN KEY (CountryName) REFERENCES Country([Name]),
FOREIGN KEY (SubTeamsID) REFERENCES SUBTEAMS(ID),
FOREIGN KEY (RealTeamsName) REFERENCES REALTEAMS([Name]));

Create Table HAS_PILOT(
SubTeamsID int NOT NULL,
PilotID int NOT NULL,
PRIMARY KEY (SubTeamsID , PilotID));


INSERT INTO COUNTRY
VALUES ('FRANCIA');
INSERT INTO COUNTRY
VALUES ('ESPAÑA');
INSERT INTO COUNTRY
VALUES ('PORTUGAL');
INSERT INTO COUNTRY
VALUES ('ARABIA SAUDI');


INSERT INTO TOURNAMENT
VALUES ('QWE123','GP de ARABIA SAUDI',  '2022-03-25', '00:00:00', '2022-03-27', '9:00:00',1000, 'Ver reglas');

INSERT INTO Race
VALUES ('Street Circuit', 'ARABIA SAUDI',0,'Jeddah', '2022-03-26', '17:00:00', '2022-03-27', '4:00:00', 'QWE123');

INSERT INTO dbo.[User]
VALUES('Brian', 'Wagemans','3f21a8490cef2bfb60a9702e9d2ddb7a805c9bd1a263557dfd51a7d0e9dfa93e','briwag88@hotmail.com','FRANCIA')

Insert into PUBLICLEAGUE
Values('briwag88@hotmail.com','QWE123')