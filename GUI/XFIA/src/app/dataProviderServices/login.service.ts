import { Injectable } from '@angular/core';
import { NetworkService } from '../services/network.service';
import { appSettings } from '../const/appSettings';
import { saveData } from 'src/app/functions/browserDataInfo';
import { localStorageNames } from 'src/app/const/localStorageNames';
import { alertMessage } from '../interface/interfaces';
import { alertMessages } from '../const/messages';
import { sha256 } from 'js-sha256';

@Injectable({
  
  providedIn: 'root'
})
export class LoginService {

  constructor(private backend:NetworkService) { }

  log(email:string,password:string,sucessCallback:()=>void,errorCallback:()=>void){
    this.backend.get_request(appSettings.loginRoute,{
      userEmail:email,
      password:sha256(password)
    }).subscribe(
    ()=>{
      saveData(localStorageNames.email,email);
      sucessCallback();

    },
    ()=>{
      errorCallback();

    })
  }
  successMesagge():alertMessage{
    const message:alertMessage={
      header:alertMessages.successHeader,
      body:alertMessages.sucessLogin
    }
    return message
  }
  errorMesagge():alertMessage{
    const message:alertMessage={
      header:alertMessages.successHeader,
      body:alertMessages.rejectedLogin
    }
    return message
  }
}
