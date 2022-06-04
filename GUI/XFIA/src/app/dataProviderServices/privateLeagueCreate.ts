import { Injectable } from '@angular/core';
import { NetworkService } from '../services/network.service';
import { SweetAlertService } from '../services/sweet-alert.service';
import { appSettings } from '../const/appSettings';
import { alertMessages } from '../const/messages';
import { checkRegex } from '../functions/regex';
import { localStorageNames } from '../const/localStorageNames';
import { privateLeagueJoinMember } from '../interface/interfaces';
import { getData } from '../functions/browserDataInfo';
@Injectable({
  providedIn: 'root'
})
export class privateLeagueCreateService {
    inputRegex=/^(([a-zA-Z]*[0-9]*)*[a-zA-Z]+([a-zA-Z]*[0-9]*)*[0-9]+([a-zA-Z]*[0-9]*)*)*(([a-zA-Z]*[0-9]*)*[0-9]+([a-zA-Z]*[0-9]*)*[a-zA-Z]+([a-zA-Z]*[0-9]*)*)*$/;
    constructor(private backend:NetworkService,private swal:SweetAlertService){
        
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
        let member:privateLeagueJoinMember={
            email:getData(localStorageNames.email),
            code:input
        }
        if(checkRegex(this.inputRegex,input) &&input.length==12){
        
                /*
            this.backend.post_request(appSettings.privateLeagueJoinRoute,member).subscribe(

                (success)=>{
                    this.swal.showSuccess(alertMessages.successHeader,alertMessages.privateLeagueCreatedBody);
                    
                },
                (error)=>{}
            )
            */
            this.swal.showSuccess(alertMessages.successHeader,alertMessages.privateLeagueCreatedBody);
        
           
           
        }
        else{
            console.log('Fracaso');
            this.swal.showError(alertMessages.privateLeagueFormatErrorHeader,alertMessages.privateLeagueFormatErrorBody)
        }

    }
    





}
