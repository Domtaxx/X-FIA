import { Injectable } from '@angular/core';
import { leagueMemberInterface } from '../interface/interfaces';
import { NetworkService } from '../services/network.service';
import { appSettings } from '../const/appSettings';
import { localStorageNames } from '../const/localStorageNames';
import { getData } from '../functions/browserDataInfo';
@Injectable({
  providedIn: 'root'
})
export class publicLeagueRankingService {
    constructor(private backend:NetworkService){}


    public getMembers(pageNumber:number,ElementPerPage:number,sucessCallback:(member:leagueMemberInterface[])=>void,faillureCallback:(member:leagueMemberInterface[])=>void){
        
        const params={
            page:pageNumber,
            amountByPage:ElementPerPage

        }
        this.backend.get_request(appSettings.publicLeagueRankingRoute,params).subscribe(
            (success:leagueMemberInterface[])=>{
              console.log(success)
              sucessCallback(success)
            },
            (error)=>{
              //faillureCallback([])
              this.getMembers(pageNumber,ElementPerPage,sucessCallback,faillureCallback);
            
            }
          )
     
    }
    public getPlayersTeam(sucessCallback:(member:leagueMemberInterface[])=>void,faillureCallback:()=>void){
        
      const user=getData(localStorageNames.email);
      //const user='briwag88@hotmail.com'
        this.backend.get_request(appSettings.publicLeaguePlayerTeamsRoute,{userEmail:user}).subscribe(
            (success)=>{
              console.log('player team')
              sucessCallback(success)
  
            },
            ()=>{
              this.getPlayersTeam(sucessCallback,faillureCallback)
            
            }
          )
     
    }
    public getMaxPage(itemPerPage:number,callback:(pageNumber:number)=>void){
      this.backend.get_request('PublicLeague/MaxPages',{amountByPage:itemPerPage}).subscribe(
        (sucess:any)=>{
          callback(sucess)
          console.log(sucess)
        },
        ()=>{
          
          this.getMaxPage(itemPerPage,callback)
        }
      )
    }

    public getUserAmount(callback:(users:number)=>void){
      this.backend.get_request(appSettings.publicLeagueUserAmount,{}).subscribe(
        (sucess:number)=>{
          callback(sucess);
        },
        ()=>{
         this.getUserAmount(callback)
        }
      )
    }
    


}
