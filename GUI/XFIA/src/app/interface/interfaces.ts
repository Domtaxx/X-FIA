
export interface tournamentInterface{
    nombreCm:string;
    llave:string
    
}
export interface pilotInterface{
    Id:number;
    Firstname:string;
    Lastname:string;
    Photo:string;
    Price:number;
    CountryNameNavigation:pilotContry
    RealTeamsNameNavigation:pilotTeam

}
interface pilotContry{
    Name:string;
    Photo:string;
}
interface pilotTeam{
    Name:string;
    Logo:string;
}
export interface carInterface{
    Name:string;
    Price:number;
    Photo:string;

}
export interface  totalBudget{
    budget:number;
    leftBudget:number;
}
export interface country{
    name:string;
}
export interface userRegisterInterface{
    Username:string;
    Password:string;
    Email:string;
    TeamsName:string;
    TeamsLogo?:File;
    CountryName:string;
    NameSubteam1:string;
    Car1:string;
    pilot1Subteam1:number;
    pilot2Subteam1:number;
    pilot3Subteam1:number;
    pilot4Subteam1:number;
    pilot5Subteam1:number;
    NameSubteam2:string;
    Car2:string;
    pilot1Subteam2:number;
    pilot2Subteam2:number;
    pilot3Subteam2:number;
    pilot4Subteam2:number;
    pilot5Subteam2:number;




}

