import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrivateLeagueRankingComponent } from './private-league-ranking/private-league-ranking.component';
import { PrivateLeagueRoutingModule } from './private-league-routing.module';


@NgModule({
  declarations: [
    PrivateLeagueRankingComponent
  ],
  imports: [
    CommonModule,
    PrivateLeagueRoutingModule
  ]
})
export class PrivateLeagueModule { }
