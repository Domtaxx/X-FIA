import { Injectable } from '@angular/core';
import { NetworkService } from '../services/network.service';
import { SweetAlertService } from '../services/sweet-alert.service';
import { appSettings } from '../const/appSettings';
import { alertMessages } from '../const/messages';
import { checkRegex } from '../functions/regex';

@Injectable({
  providedIn: 'root'
})
export class privateLeagueCreateService {
    inputRegex='^([a-zA-Z]*[0-9]*[a-zA-Z]+[a-zA-Z]*[0-9]*[0-9]+[a-zA-Z]*[0-9]*)*([a-zA-Z]*[0-9]*[0-9]+[a-zA-Z]*[0-9]*[a-zA-Z]+[a-zA-Z]*[0-9]*)*';
    constructor(private backend:NetworkService,private swal:SweetAlertService){

    }

    createLeague(){
        this.swal.inputTextSwal(alertMessages.privateLeagueCreateHeader,alertMessages.privateAcceptCreate,
            (data:string)=>{
                this.handleRequest(data);
            }
            
        )
    }
    private handleRequest(input:string){
        console.log(input);
        if(checkRegex(this.inputRegex,input)){
            console.log("Exito")
        }
        else{
            console.log('Fracaso');
        }

    }
    





}
