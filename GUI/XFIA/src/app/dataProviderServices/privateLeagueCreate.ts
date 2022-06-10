import { Injectable } from '@angular/core';
import { NetworkService } from '../services/network.service';
import { SweetAlertService } from '../services/sweet-alert.service';
import { appSettings } from '../const/appSettings';
import { alertMessages } from '../const/messages';
import { checkRegex } from '../functions/regex';
import { localStorageNames } from '../const/localStorageNames';
import { alertMessage, privateLeagueCreate, privateLeagueInfo, privateLeagueJoinMember } from '../interface/interfaces';
import { getData } from '../functions/browserDataInfo';
import { privateLeagueCreateError, privateLeagueJoinError } from '../errorCodeHandler/errorHandler';
import { RouterServiceService } from '../services/router-service.service';
@Injectable({
  providedIn: 'root'
})
export class privateLeagueCreateService {
    inputRegex=/^(([a-zA-Z]*[0-9]*)*[a-zA-Z]+([a-zA-Z]*[0-9]*)*[0-9]+([a-zA-Z]*[0-9]*)*)*(([a-zA-Z]*[0-9]*)*[0-9]+([a-zA-Z]*[0-9]*)*[a-zA-Z]+([a-zA-Z]*[0-9]*)*)*$/;
    constructor(private backend:NetworkService,private swal:SweetAlertService,private router:RouterServiceService){
        
    }

    createLeague(name:string,maxUser:number,sucessCallback:(info:any)=>void,faillureCallback:(errorMessage:alertMessage)=>void){
        const params:privateLeagueCreate={
            name:name,
            maxUser:maxUser,
            ownerEmail:getData(localStorageNames.email)

        }
        console.log(params)
        this.backend.post_request(appSettings.privateCreatePrivateLeagueRoute,params).subscribe(
            (sucess:any)=>{
                console.log('Exito crear');
                console.log(sucess);
                sucessCallback(sucess)
            },
            (error:any)=>{
                console.log(error)
                const message:alertMessage=privateLeagueCreateError(error.error)
                faillureCallback(
                    message
                )

            }
        )
    }

    joinLeague(){
        this.swal.inputTextSwal(alertMessages.privateLeagueCreateHeader,alertMessages.privateAcceptCreate,
            (data:string)=>{
                this.handleRequest(data);
            }
            
        )
    }
    private handleRequest(input:string){
        console.log(input);
        let member={
            userEmail:getData(localStorageNames.email),
            privateLeagueKey: input
        }
        
        if(checkRegex(this.inputRegex,input) &&input.length==12){
        
                
            this.backend.post_request(appSettings.privateLeagueJoinRoute,member).subscribe(

                (success)=>{
                    this.swal.showSuccess(alertMessages.successHeader,alertMessages.privateLeagueJoinBody)
                    this.router.redirect('privateLeague/ranking')
                    
                },
                (error)=>{
                    const message=privateLeagueJoinError(error.error);

                    this.swal.showError(message.header,message.body)
                }
            )
            
        
           
           
        }
        else{
            console.log('Fracaso');
            this.swal.showError(alertMessages.privateLeagueFormatErrorHeader,alertMessages.privateLeagueFormatErrorBody)
        }

    }
    





}
