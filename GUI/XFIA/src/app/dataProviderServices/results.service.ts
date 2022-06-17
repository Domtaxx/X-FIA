import { Injectable } from '@angular/core';
import { NetworkService } from '../services/network.service';
import { raceInterface, tournamentInterface } from '../interface/interfaces';
import { appSettings } from '../const/appSettings';

@Injectable({
  providedIn: 'root'
})
export class ResultsService {

  constructor(private backend:NetworkService) { }

  getTournaments(callback:(tournaments:tournamentInterface[])=>void){
    this.backend.get_request(appSettings.allTournamentsRoute,{}).subscribe(
      (sucess:any)=>{
        callback(sucess);
      }
    )

  }

  getRaces(tournamentKey,callback:(races:raceInterface[])=>void){
    this.backend.get_request(appSettings.tournamentRaces,{tournamentKey:tournamentKey}).subscribe(
      (sucess:any)=>{
        callback(sucess)
      }
    )
  }
}
