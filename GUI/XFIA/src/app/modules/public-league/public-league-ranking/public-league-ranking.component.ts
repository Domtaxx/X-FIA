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
  pageNumber!:number;
  elementPerPage!:number;
  maxPage!:number;
  userAmount!:number;
  playersTeam:leagueMemberInterface[]=[];
  constructor(private dataManagement:publicLeagueRankingService,private swal:SweetAlertService,private privateLeagueService:privateLeagueCreateService) {
    this.pageNumber=0;
    this.elementPerPage=11;
    this.maxPage=5;
    this.userAmount=0;
   }

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
    //this.getRankingInfo();
    //this.getPlayersTeamInfo();
    //this.getMaxPage();
    //this.getUserAmount();
  }

  getRankingInfo(){
    this.dataManagement.getMembers(this.pageNumber,this.elementPerPage,
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
  getMaxPage(){
    this.dataManagement.getMaxPage(this.elementPerPage,
      (maxPage:number)=>{
        this.maxPage=maxPage;
      }
      )
  }
  getUserAmount(){
    this.dataManagement.getUserAmount(
      (users:number)=>{
        this.userAmount=users;
      }
    );
  }
  joinPrivateLeague(){
    this.privateLeagueService.joinLeague()
  }

  rightPage(){
    if(this.pageNumber+1<this.maxPage){
      this.pageNumber+=1;
      this.getData();
    }

  }
  leftPage(){
    if(this.pageNumber-1>=0){
      this.pageNumber-=1;
      this.getData();
    }

  }


}
