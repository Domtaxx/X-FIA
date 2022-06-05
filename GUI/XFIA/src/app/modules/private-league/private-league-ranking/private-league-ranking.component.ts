import { Component, OnInit } from '@angular/core';
import{alertMessage, leagueMemberInterface} from 'src/app/interface/interfaces'
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { privateLeagueRankingService } from 'src/app/dataProviderServices/privateLeagueRanking';
import { privateLeagueCreateService } from 'src/app/dataProviderServices/privateLeagueCreate';
import { privateLeagueInfo } from 'src/app/interface/interfaces';

@Component({
  selector: 'app-private-league-ranking',
  templateUrl: './private-league-ranking.component.html',
  styleUrls: ['./private-league-ranking.component.css']
})
export class PrivateLeagueRankingComponent implements OnInit {

  tableDataSource:leagueMemberInterface[]=[];
  displayedColumns=['Posicion','Usuario','Escuderia','Equipo','Puntaje'];
  state!:string;
  leagueKey!:string;
  maxMember!:number;
  leagueInfo!:privateLeagueInfo;
  constructor(private dataManagement:privateLeagueRankingService,private swal:SweetAlertService,private privateLeagueService:privateLeagueCreateService) { }

  ngOnInit(): void {
    const object={
      posicion:1,
      usuario:'Martin',
      escuderia:'Perritos',
      equipo:'Vida',
      puntaje:1000,
      }
      
    for(var i=0;i<100;i++){
      this.tableDataSource.push(object)
    }
    
    this.getData();
    
  }
  getData(){
    //this.getRankingInfo();
    this.leagueInfo={
      key:'asdlfsdf44',
      maxUser:600,
      state: true
    }
    this.getLeagueInfo()
    
  }

  getRankingInfo(){
    this.dataManagement.getMembers(
      (leagueMembers:leagueMemberInterface[])=>{
        this.tableDataSource=leagueMembers;
        this.tableDataSource=[...this.tableDataSource];
      },
      ()=>{
        this.tableDataSource=[];
        this.tableDataSource=[...this.tableDataSource];
      }
    );
    
    
  }
  getLeagueInfo(){
    this.dataManagement.getPrivateLeagueInfo(
      (info:privateLeagueInfo)=>{
        this.leagueInfo=info;
      }
    )
  }

  leavePrivateLeague(){
    this.dataManagement.leaveLeague(
      ()=>{},
      (message:alertMessage)=>{
        this.failureMessage(message)
      }
    )
  }
  sucessMessage(header:string,body:string){
    this.swal.showSuccess(header,body);
  }
  failureMessage(message:alertMessage){
    this.swal.showError(message.header,message.body);
  }
}
