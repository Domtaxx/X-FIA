import { Injectable } from '@angular/core';
import { NetworkService } from '../services/network.service';
import { SweetAlertService } from '../services/sweet-alert.service';
import { appSettings } from '../const/appSettings';
import { alertMessages } from '../const/messages';
import { localStorageNames } from '../const/localStorageNames';
import { userInterface } from '../interface/interfaces';
import { getData } from '../functions/browserDataInfo';
@Injectable({
  providedIn: 'root'
})
export class userProfileProviderService {

    constructor(private backend:NetworkService,private swal:SweetAlertService){}
    

    getProfileData(email:string,callback:(user:userInterface)=>void){
        this.backend.get_request(appSettings.profileUserGet,{userEmail:email}).subscribe(
            (sucess:userInterface)=>{
                console.log(sucess)
                callback(sucess);
            },
            (error)=>{
                console.log(error)
            }
        )
    }




}
