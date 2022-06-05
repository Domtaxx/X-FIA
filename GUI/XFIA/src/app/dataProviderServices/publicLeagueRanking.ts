import { Injectable } from '@angular/core';
import { leagueMemberInterface } from '../interface/interfaces';
import { NetworkService } from '../services/network.service';
import { appSettings } from '../const/appSettings';

@Injectable({
  providedIn: 'root'
})
export class publicLeagueRankingService {
    constructor(private backend:NetworkService){}


    public getMembers(pageNumber:number,ElementPerPage:number,sucessCallback:(member:leagueMemberInterface[])=>void,faillureCallback:(member:leagueMemberInterface[])=>void){
        
        const params={
            page:pageNumber,
            ElementPerPage:ElementPerPage

        }
        this.backend.get_request(appSettings.publicLeagueRankingRoute,params).subscribe(
            (success:leagueMemberInterface[])=>{
              sucessCallback(success)
            },
            (error)=>{
              faillureCallback([])
            
            }
          )
     
    }
    public getPlayersTeam(sucessCallback:(member:leagueMemberInterface[])=>void,faillureCallback:(member:leagueMemberInterface[])=>void){
        this.backend.get_request(appSettings.publicLeaguePlayerTeamsRoute,{}).subscribe(
            (success:leagueMemberInterface[])=>{
              sucessCallback(success)
            },
            (error)=>{
              faillureCallback([])
            
            }
          )
     
    }
    public getMaxPage(itemPerPage:number,callback:(pageNumber:number)=>void){
      this.backend.get_request(appSettings.publicLeagueMaxPage,{amountByPage:itemPerPage}).subscribe(
        (sucess)=>{
          callback(sucess)
        },
        (error)=>{
          callback(0);
        }
      )
    }

    public getUserAmount(callback:(users:number)=>void){
      this.backend.get_request(appSettings.publicLeagueUserAmount,{}).subscribe(
        (sucess:number)=>{
          callback(sucess);
        }
      )
    }
    


}
