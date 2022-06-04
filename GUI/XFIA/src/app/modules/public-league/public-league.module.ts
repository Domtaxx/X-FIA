import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicLeagueRoutingModule } from './public-league-routing.module';
import { PublicLeagueRankingComponent } from './public-league-ranking/public-league-ranking.component';
import {MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';

@NgModule({
  declarations: [
    PublicLeagueRankingComponent
  ],
  imports: [
    CommonModule,
    PublicLeagueRoutingModule, 
    MatTableModule,
    MatButtonModule,
    MatIconModule,
  ]
})
export class PublicLeagueModule { }
