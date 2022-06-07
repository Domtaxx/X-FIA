import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrivateLeagueRankingComponent } from './private-league-ranking/private-league-ranking.component';
import { PrivateLeagueRoutingModule } from './private-league-routing.module';
import {MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';

@NgModule({
  declarations: [
    PrivateLeagueRankingComponent
  ],
  imports: [
    CommonModule,
    PrivateLeagueRoutingModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ]
})
export class PrivateLeagueModule { }
