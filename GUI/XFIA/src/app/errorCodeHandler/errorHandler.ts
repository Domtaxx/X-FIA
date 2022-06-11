import { alertMessage } from "../interface/interfaces";
import { alertMessages } from "../const/messages";
/*
    input: an integer number
    output: alertMessage interface that includes a header and a body message
    description: takes an error conde of register message and returns a interface representing
    the message to be shown due to the error
*/
export function userRegisterMessage(errorCode:any):alertMessage|undefined{
    var header:string;
    var body:string;
    switch(errorCode){
        case 1: //the email its already being used by another account
            header=alertMessages.rejected;
            body=alertMessages.accountAlreadyExists;
            break;
        case 2: //the team name its already registered
            header=alertMessages.rejected;
            body=alertMessages.teamAlreadyExists;
            break
        case 3: //the subteam names are the same
            header=alertMessages.rejected;
            body=alertMessages.repeatedSubName;
            break;
        default: // other error
            header="";
            body="";
            break;

    }
    const message:alertMessage={
        header:header,
        body:body
    }
    return message;
}

export function privateLeagueLeaveError(errorCode:any){
    var header:string=alertMessages.rejected;
    var body:string='';

    switch(errorCode){
        case 5:
            body=alertMessages.privateLeagueUserNotRegisterBody;
            break
        case 12:
            body=alertMessages.privateLeagueUserDoesntBelongToAnyPrivateLeague;
            break
        case 10:
            body=alertMessages.privateLeagueOwnerCantLeaveThePrivateLeague;
            break
        
        
    }
    const message:alertMessage={
        header:header,
        body:body
    }
    return message;
    
}


export function privateLeagueCreateError(errorCode:any){
    var header:string=alertMessages.rejected;
    var body:string='';
    switch(errorCode){
        case 5:
            body='El Usuario presentado no esta en la base de datos';
            break;
        case 3:
            body='El usuario ya se encuentra en una liga privada, para crear otra debe salir primero de la existente';
            break;
        case 1:
            body='El nombre de la liga privada seleccionado ya ha sido utilizado';
            break;

            

    }
    const message:alertMessage={
        header:header,
        body:body
    }
    return message;
}



export function privateLeagueRankingError(errorCode:any){
    var header:string=alertMessages.rejected;
    var body:string='';
    switch(errorCode){
        case 5:
            body=alertMessages.privateLeagueUserNotLogRanking;
            break;
        case 12:
            body=alertMessages.privateLeagueUserNotOnPrivateLeague;
            break;
     

            

    }
    const message:alertMessage={
        header:header,
        body:body
    }
    return message;
}

export function privateLeagueJoinError(errorCode:any){
    var header:string=alertMessages.rejected;
    var body:string='';
    switch(errorCode){
        case 5:
            body=alertMessages.privateLeagueUserNotLogRanking;
            break;
        case 3:
            body=alertMessages.privateLeagueUserOnALeague;
            break;
        case 7:
            body=alertMessages.privateLeagueDoesntExist;
            break
     

            

    }
    const message:alertMessage={
        header:header,
        body:body
    }
    return message;
}