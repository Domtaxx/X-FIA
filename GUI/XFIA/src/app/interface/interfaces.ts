
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

