import { Component, OnInit } from '@angular/core';
import{leagueMemberInterface} from 'src/app/interface/interfaces'
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { publicLeagueRankingService } from 'src/app/dataProviderServices/publicLeagueRanking';
import { privateLeagueCreateService } from 'src/app/dataProviderServices/privateLeagueCreate';
import { Router } from '@angular/router';



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
  constructor(private dataManagement:publicLeagueRankingService,private swal:SweetAlertService,private privateLeagueService:privateLeagueCreateService,private router:Router) {
    this.pageNumber=1;
    this.elementPerPage=10;
    this.maxPage=0;
    this.userAmount=0;
    this.getData();
    
   }

  ngOnInit(): void {
    
    
  }
  getData(){
    try{
      this.getRankingInfo();
      this.getMaxPage();
    
    this.getPlayersTeamInfo();
    
    this.getUserAmount();
    }
    catch(e){

    }
    
  }

  getRankingInfo(){
    this.dataManagement.getMembers(this.pageNumber,this.elementPerPage,
      (leagueMembers:leagueMemberInterface[])=>{
        this.tableDataSource=leagueMembers;
    
        console.log('dataSource');
        console.log(this.tableDataSource)
      },
      ()=>{
        this.tableDataSource=[];
 
      }
    );
    
    
  }
  getPlayersTeamInfo(){
    this.dataManagement.getPlayersTeam(
      (playerTeams:leagueMemberInterface[])=>{
          this.playersTeam=playerTeams;
          this.playersTeam=[...this.playersTeam];
          console.log('team')
          console.log(this.playersTeam)
      },
      ()=>{
        this.playersTeam=[];
        this.playersTeam=[...this.playersTeam];
      }

    );
  }
  getMaxPage(){
    this.dataManagement.getMaxPage(this.elementPerPage,
      (maxPages:number)=>{
        this.maxPage=maxPages;
        console.log('max page')
        console.log(this.maxPage)
      
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
    if(this.pageNumber+1<=this.maxPage){
      this.pageNumber+=1;
      this.getData();
    }

  }
  leftPage(){
    if(this.pageNumber-1>=1){
      this.pageNumber-=1;
      this.getData();
    }

  }
  redirect(email:string){
    this.router.navigate(['/profile/view',email])

  }


}
