select * from [SubTeams]
select * from [User]
select * from [Has_pilot]
select * from PILOT
select * from RealTeams
select * from PRIVATELEAGUE
select * from SUBTEAMS
select * from RACE
select * from SubteamPoints
select * from PilotRace

ALTER TABLE SubteamPoints DROP Constraint SubteamKey;
Drop Table SubteamPoints
Drop Table PilotRace
Drop Table RealTeamRace
ALTER TABLE [USER] DROP Constraint PrivateLeague_User_key;
Drop Table PRIVATELEAGUE
Drop Table Has_pilot
Drop Table PUBLICLEAGUE
Drop Table PILOT
Drop Table RACE
DROP TABLE SUBTEAMS
DROP TABLE REALTEAMS
DROP TABLE TOURNAMENT
DROP TABLE [USER]
DROP TABLE COUNTRY

