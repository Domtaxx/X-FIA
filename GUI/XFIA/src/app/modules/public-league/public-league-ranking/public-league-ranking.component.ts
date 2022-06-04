import { Component, OnInit } from '@angular/core';
import{leagueMemberInterface} from 'src/app/interface/interfaces'
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { publicLeagueRankingService } from 'src/app/dataProviderServices/publicLeagueRanking';
import { privateLeagueCreateService } from 'src/app/dataProviderServices/privateLeagueCreate';

@Component({
  selector: 'app-public-league-ranking',
  templateUrl: './public-league-ranking.component.html',
  styleUrls: ['./public-league-ranking.component.css']
})
export class PublicLeagueRankingComponent implements OnInit {
  tableDataSource:leagueMemberInterface[]=[];
  displayedColumns=['Posicion','Usuario','Escuderia','Equipo','Puntaje'];
  page!:number;
  playersTeam:leagueMemberInterface[]=[];
  constructor(private dataManagement:publicLeagueRankingService,private swal:SweetAlertService,private privateLeagueService:privateLeagueCreateService) { }

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
    for(var i=0;i<2;i++){
      this.playersTeam.push(object)
    }
    //this.getData();
    
  }
  getData(){
    this.getRankingInfo();
    this.getPlayersTeamInfo();
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
  getPlayersTeamInfo(){
    this.dataManagement.getPlayersTeam(
      (playerTeams:leagueMemberInterface[])=>{
          this.playersTeam=playerTeams;
          this.playersTeam=[...this.playersTeam];
      },
      ()=>{
        this.playersTeam=[];
        this.playersTeam=[...this.playersTeam];
      }

    );
  }
  joinPrivateLeague(){
    this.privateLeagueService.joinLeague()
  }

}
