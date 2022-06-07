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
    var header:string='';
    var body:string='';
    const message:alertMessage={
        header:header,
        body:body
    }
    return message;
    
}