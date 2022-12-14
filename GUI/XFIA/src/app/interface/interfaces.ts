

//interface of messages
export interface alertMessage{
    header:string;
    body:string;
}

//interface the represents the tournament with the information needed
export interface tournamentInterface{
    name:string;
    key:string
    
}
export interface raceInterface{
    name:string;
}
//interface that represents the pilot 
export interface pilotInterface{
    Id:number;
    Firstname:string;
    Lastname:string;
    Photo:string;
    Price:number;
    CountryNameNavigation:pilotContry|null;
    RealTeamsNameNavigation:pilotTeam|null

}
//interface that representest the data navigation of country in the pilot
interface pilotContry{
    Name:string;
    Photo:string;
}
//interface that representest the data navigation of country in the pilot
interface pilotTeam{
    Name:string;
    Logo:string;
}
//interface that representest the car in the api
export interface carInterface{
    Name:string;
    Price:number;
    Photo:string;

}
//interface that representest the budget and left budget of a team
export interface  totalBudget{
    budget:number;
    leftBudget:number;
}
//represents the country
export interface country{
    name:string;
}
//interface that represents the request to add a user
export interface userRegisterInterface{
    Username:string;
    Password:string;
    Email:string;
    TeamsName:string;
    TeamsLogo?:File;
    CountryName:string;
    NameSubteam1:string;
    Car1:string;
    pilot1Subteam1:string;
    pilot2Subteam1:string;
    pilot3Subteam1:string;
    pilot4Subteam1:string;
    pilot5Subteam1:string;
    NameSubteam2:string;
    Car2:string;
    pilot1Subteam2:string;
    pilot2Subteam2:string;
    pilot3Subteam2:string;
    pilot4Subteam2:string;
    pilot5Subteam2:string;




}
export interface profileEditInterface{
    Username:string;
    Email:string;
    TeamsName:string;
    TeamsLogo?:File;
    CountryName:string;
    NameSubteam1:string;
    Car1:string;
    pilot1Subteam1:string;
    pilot2Subteam1:string;
    pilot3Subteam1:string;
    pilot4Subteam1:string;
    pilot5Subteam1:string;
    NameSubteam2:string;
    Car2:string;
    pilot1Subteam2:string;
    pilot2Subteam2:string;
    pilot3Subteam2:string;
    pilot4Subteam2:string;
    pilot5Subteam2:string;
}

export interface leagueMemberInterface{
    Position:number;
    UserName:string;
    TeamName:string;
    SubteamName:string;
    Points:number;
    UserEmail:string;
}

export interface privateLeagueJoinMember{
    email:string|null;
    code:string|null;
}

export interface privateLeagueInfo{
    name:string;
    key:string;
    maxUser:number;
    state:boolean;
}

export interface privateLeagueCreate{
    name:string;
    ownerEmail:string|null;
    maxUser:number;
}
export interface userInterface{
    Username:string;
    TeamsName:string;
    TeamsLogo:string;
    CountryName:string;
    Subteams:subTeam[];
}

interface subTeam{
    Name:string;
    Pilots:pilotInterface[];
    RealTeamsNameNavigation:carInterface;
}