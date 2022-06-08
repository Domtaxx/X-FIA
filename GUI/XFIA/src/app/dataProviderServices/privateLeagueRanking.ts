import { Injectable } from '@angular/core';
import { leagueMemberInterface } from '../interface/interfaces';
import { NetworkService } from '../services/network.service';
import { appSettings } from '../const/appSettings';
import { localStorageNames } from '../const/localStorageNames';
import { getData } from '../functions/browserDataInfo';
import { privateLeagueInfo } from '../interface/interfaces';
import { alertMessage } from '../interface/interfaces';
import { privateLeagueLeaveError } from '../errorCodeHandler/errorHandler';
@Injectable({
  providedIn: 'root'
})
export class privateLeagueRankingService {
    constructor(private backend:NetworkService){}


    public getMembers(sucessCallback:(member:leagueMemberInterface[])=>void,faillureCallback:(member:leagueMemberInterface[])=>void){
      const email=getData(localStorageNames.email);
      //const email='briwag88@hotmail.com'
      this.backend.get_request(appSettings.privateLeagueRankingRoute,{userEmail:email}).subscribe(
          (success:leagueMemberInterface[])=>{
            console.log(success)
            sucessCallback(success)
          },
          (error)=>{
            this.getMembers(sucessCallback,faillureCallback);
          
          }
        )
     
    }
    public getPrivateLeagueInfo(sucessCallback:(key:privateLeagueInfo)=>void){
      const email=getData(localStorageNames.email);
      //const email='briwag88@hotmail.com'
      this.backend.get_request(appSettings.privateLeagueKeyRoute,{userEmail:email}).subscribe(
        (sucess:privateLeagueInfo)=>{
          console.log(sucess)
          sucessCallback(sucess);
        },
        ()=>{
          this.getPrivateLeagueInfo(sucessCallback)
        }
      )
    }
    public leaveLeague(sucessCallback:()=>void,faillureCallback:(message:alertMessage)=>void){
      const email=getData(localStorageNames.email);
      this.backend.delete_request(appSettings.privateLeagueLeaveRoute,{userEmail:email}).subscribe(
        ()=>{
          sucessCallback();
        },
        (code:any)=>{
          const message=privateLeagueLeaveError(code)
          faillureCallback(message);
        }
      )
    }
    
  


}
