import { Injectable } from '@angular/core';
import { leagueMemberInterface } from '../interface/interfaces';
import { NetworkService } from '../services/network.service';
import { appSettings } from '../const/appSettings';

@Injectable({
  providedIn: 'root'
})
export class publicLeagueRankingService {
    constructor(private backend:NetworkService){}


    public getMembers(sucessCallback:(member:leagueMemberInterface[])=>void,faillureCallback:(member:leagueMemberInterface[])=>void){
        this.backend.get_request(appSettings.publicLeagueRankingRoute,{}).subscribe(
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


}
