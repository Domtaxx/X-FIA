import { Injectable } from '@angular/core';
import { leagueMemberInterface } from '../interface/interfaces';
import { NetworkService } from '../services/network.service';
import { appSettings } from '../const/appSettings';
import { splitAtPeriod } from '@angular/compiler/src/util';
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
        this.backend.get_request(appSettings.privateLeagueRankingRoute,{}).subscribe(
            (success:leagueMemberInterface[])=>{
              sucessCallback(success)
            },
            (error)=>{
              faillureCallback([])
            
            }
          )
     
    }
    public getPrivateLeagueInfo(sucessCallback:(key:privateLeagueInfo)=>void){
      const email=getData(localStorageNames.email);
      this.backend.get_request(appSettings.privateLeagueKeyRoute,{email:email}).subscribe(
        (sucess:privateLeagueInfo)=>{
          sucessCallback(sucess);
        },
        ()=>{
          sucessCallback({
            key:'',
            maxUser:0,
            state:false,
          });
        }
      )
    }
    public leaveLeague(sucessCallback:()=>void,faillureCallback:(message:alertMessage)=>void){
      const email=getData(localStorageNames.email);
      this.backend.get_request(appSettings.privateLeagueLeaveRoute,{email:email}).subscribe(
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
