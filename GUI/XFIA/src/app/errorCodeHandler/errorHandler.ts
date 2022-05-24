import { alertMessage } from "../interface/interfaces";
import { alertMessages } from "../const/messages";
export function userRegisterMessage(errorCode:any):alertMessage|undefined{
    var header:string;
    var body:string;
    switch(errorCode){
        case 1:
            header=alertMessages.rejected;
            body=alertMessages.accountAlreadyExists;
            break;
        case 2:
            header=alertMessages.rejected;
            body=alertMessages.teamAlreadyExists;
            break
        case 3:
            header=alertMessages.rejected;
            body=alertMessages.repeatedSubName;
            break;
        default: 
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