CREATE TABLE CAMPEONATO(
Llave VARCHAR(6) NOT NULL UNIQUE,
Nombre VARCHAR(30) NOT NULL ,
Fecha_de_inicio DATE NOT NULL ,
Hora_de_inicio TIME NOT NULL,
Fecha_de_fin DATE NOT NULL ,
Hora_de_fin TIME NOT NULL,
Presupuesto float NOT NULL,
Descripcion_de_reglas VARCHAR(1000),
PRIMARY KEY(Llave));


CREATE TABLE PAISES(
Nombre VARCHAR(30) NOT NULL UNIQUE,
PRIMARY KEY (Nombre));
 
Create Table [User](
[Firstname] varchar(30) not null,
[Lastname] varchar(30) not null,
[Password] varchar(256) not null,
Email varchar(30) unique,
Country varchar(30) not null,
PRIMARY KEY (Email),
FOREIGN KEY (Country) REFERENCES PAISES(Nombre));

CREATE TABLE CARRERA(
Nombre VARCHAR(30) NOT NULL,
Pais VARCHAR(30) NOT NULL,
Estado int NOT NULL,
Nombre_de_pista VARCHAR(30) NOT NULL,
Fecha_de_inicio DATE NOT NULL,
Hora_de_inicio TIME NOT NULL,
Fecha_de_fin DATE NOT NULL,
Hora_de_fin TIME NOT NULL,
Campeonato_key VARCHAR(6) NOT NULL,
TournamentUserKey int,
PRIMARY KEY (Nombre, Campeonato_key),
FOREIGN KEY (Campeonato_key) REFERENCES CAMPEONATO (Llave),
FOREIGN KEY (Pais) REFERENCES PAISES (Nombre));

Create Table [PublicLeague](
UserEmail varchar(30),
TournamentKey varchar(6),
Primary KEY (UserEmail,TournamentKey),
FOREIGN KEY (TournamentKey) REFERENCES CAMPEONATO (Llave),
FOREIGN KEY (UserEmail) REFERENCES dbo.[User](Email));

INSERT INTO dbo.[User]
VALUES('Brian', 'Wagemans','3f21a8490cef2bfb60a9702e9d2ddb7a805c9bd1a263557dfd51a7d0e9dfa93e','briwag88@hotmail.com','FRANCIA')

INSERT INTO "PAISES"
VALUES ('FRANCIA');
INSERT INTO "PAISES"
VALUES ('ESPAÑA');
INSERT INTO "PAISES"
VALUES ('PORTUGAL');
INSERT INTO "PAISES"
VALUES ('ARABIA SAUDI');


INSERT INTO "CAMPEONATO"
VALUES ('QWE123','GP de ARABIA SAUDI',  '2022-03-25', '00:00:00', '2022-03-27', '9:00:00',1000, 'Ver reglas');

INSERT INTO "CARRERA"
VALUES ('Street Circuit', 'ARABIA SAUDI',0,'Jeddah', '2022-03-26', '17:00:00', '2022-03-27', '4:00:00', 'QWE123');

alter procedure dbo.uspAddToCampeonato
	@Key VARCHAR(6),
	@Name VARCHAR(30),
	@InitialDate VARCHAR(50),
	@InitialTime VARCHAR(50),
	@FinalDate VARCHAR(50),
	@FinalTime VARCHAR(50),
	@Budget float,
	@Rules VARCHAR(1000)
AS
	DECLARE @exitCode as INT
	Select @exitCode = COUNT(*)from dbo.CAMPEONATO where (@FinalDate < Fecha_de_fin AND @FinalDate > Fecha_de_inicio)OR(@InitialDate < Fecha_de_fin AND @InitialDate > Fecha_de_inicio) OR (@InitialDate = Fecha_de_fin AND @InitialTime<= Hora_de_fin) OR (@FinalDate = Fecha_de_inicio AND @FinalTime >= Hora_de_inicio)
	set nocount on
BEGIN
	if @exitCode=0
		begin
			Insert into dbo.CAMPEONATO(Llave,Nombre,Descripcion_de_reglas,Fecha_de_inicio,Hora_de_inicio,Fecha_de_fin,Hora_de_fin,Presupuesto)
			Values (@Key,@Name,@Rules,CONVERT(datetime,@InitialDate),CONVERT(time,@InitialTime),CONVERT(datetime,@FinalDate),CONVERT(time,@FinalTime),@Budget)
		end
	select * from dbo.CAMPEONATO where @Key=Llave
END

select*from Paises


