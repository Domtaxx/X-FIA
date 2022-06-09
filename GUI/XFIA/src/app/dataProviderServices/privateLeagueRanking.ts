import { Injectable } from '@angular/core';
import { leagueMemberInterface } from '../interface/interfaces';
import { NetworkService } from '../services/network.service';
import { appSettings } from '../const/appSettings';
import { localStorageNames } from '../const/localStorageNames';
import { getData } from '../functions/browserDataInfo';
import { privateLeagueInfo } from '../interface/interfaces';
import { alertMessage } from '../interface/interfaces';
import { privateLeagueLeaveError } from '../errorCodeHandler/errorHandler';
import { SweetAlertService } from '../services/sweet-alert.service';
import { RouterServiceService } from '../services/router-service.service';
import { alertMessages } from '../const/messages';
import { privateLeagueRankingError } from '../errorCodeHandler/errorHandler';
@Injectable({
  providedIn: 'root'
})
export class privateLeagueRankingService {
    failed=false;
    constructor(private backend:NetworkService,private swal:SweetAlertService,private router:RouterServiceService){}


    public getMembers(sucessCallback:(member:leagueMemberInterface[])=>void,faillureCallback:(member:leagueMemberInterface[])=>void){
      const email=getData(localStorageNames.email);
      //const email='briwag88@hotmail.com'
      this.backend.get_request(appSettings.privateLeagueRankingRoute,{userEmail:email}).subscribe(
          (success:leagueMemberInterface[])=>{
            console.log(success)
            sucessCallback(success)
          },
          (error)=>{
          const code=error.error;
          if(code==5 || code==12){
            this.noInLeagueError(code)
          }
          else{ 
            this.getMembers(sucessCallback,faillureCallback);
          }
            
          
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
        (error)=>{
          if(this.failed)return;
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
          console.log(code.error)
          const message=privateLeagueLeaveError(code.error)
          faillureCallback(message);
        }
      )
    }

    noInLeagueError(code:any){
      this.failed=true;
      const message=privateLeagueRankingError(code);
      this.swal.acceptSwal(message.header,message.body,alertMessages.confirmButtonText)
      .then(
        (confirm)=>{
          if(confirm.isConfirmed){
            this.router.redirect('publicLeague/ranking')
          }
        }
      );

    }
    
    
  


}
