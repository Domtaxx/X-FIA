import { Injectable } from '@angular/core';
import { NetworkService } from '../services/network.service';
import { alertMessage, raceInterface, tournamentInterface } from '../interface/interfaces';
import { appSettings } from '../const/appSettings';
import { alertMessages } from '../const/messages';
import { resultsUploadError } from '../errorCodeHandler/errorHandler';
import { SweetAlertService } from '../services/sweet-alert.service';
@Injectable({
  providedIn: 'root'
})
export class ResultsService {

  constructor(private backend:NetworkService,private swal:SweetAlertService) { }

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
  uploadResults(tournamentKey:string,raceName:string,files:File){
    console.log('Estoy en el servicio de envio para resultados')
    console.log(tournamentKey);
    console.log(raceName);
    console.log(files)
    const params={
      tournamentKey:tournamentKey,
      race:raceName,
      file:files
    }
    console.log(params)
    this.backend.post_request_multipart(
      appSettings.publishResults,params
    ).subscribe(
      ()=>{
        this.swal.showSuccess(alertMessages.successHeader,alertMessages.sucessResultsFile);
      },
      (error)=>{
        const message:alertMessage=resultsUploadError(error.error);
        this.swal.showError(message.header,message.body);
      }
    );
  }
}
