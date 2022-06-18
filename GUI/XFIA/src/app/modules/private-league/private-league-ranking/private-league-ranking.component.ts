import { Component, OnInit } from '@angular/core';
import{alertMessage, leagueMemberInterface} from 'src/app/interface/interfaces'
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { privateLeagueRankingService } from 'src/app/dataProviderServices/privateLeagueRanking';
import { privateLeagueCreateService } from 'src/app/dataProviderServices/privateLeagueCreate';
import { privateLeagueInfo } from 'src/app/interface/interfaces';
import { alertMessages } from 'src/app/const/messages';
import { Router } from '@angular/router';
@Component({
  selector: 'app-private-league-ranking',
  templateUrl: './private-league-ranking.component.html',
  styleUrls: ['./private-league-ranking.component.css']
})
export class PrivateLeagueRankingComponent implements OnInit {

  tableDataSource:leagueMemberInterface[]=[];
  displayedColumns=['Posicion','Usuario','Escuderia','Equipo','Puntaje'];
  leagueInfo!:privateLeagueInfo;
  constructor(private dataManagement:privateLeagueRankingService,private swal:SweetAlertService,private privateLeagueService:privateLeagueCreateService,private router:Router) { }

  ngOnInit(): void {
  
    
    this.getData();
    
  }
  getData(){
    this.getRankingInfo();
    this.leagueInfo={
      name:'Liga Privada',
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

    this.swal.optionSwal(alertMessages.privateLeagueLeaveQuestion,alertMessages.privateLeagueLeaveWarning,alertMessages.cancelButtonText,alertMessages.confirmButtonText).then(
      (result)=>{
        if(!result.isConfirmed)return;
        this.dataManagement.leaveLeague(
          ()=>{
            this.sucessMessage(alertMessages.successHeader,alertMessages.privateLeagueLeaveSucessBody)
          },
          (message:alertMessage)=>{
            this.failureMessage(message)
          }
        )
      }
    )
   
  }
  sucessMessage(header:string,body:string){
    this.swal.showSuccess(header,body);
  }
  failureMessage(message:alertMessage){
    this.swal.showError(message.header,message.body);
  }

  redirect(email:string){

    this.router.navigate(['/profile/view',email])
  }
}
