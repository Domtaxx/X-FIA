import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrivateLeagueRankingComponent } from './private-league-ranking/private-league-ranking.component';
import { PrivateLeagueRoutingModule } from './private-league-routing.module';
import {MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { CreateLeagueComponent } from './create-league/create-league.component';
import {MatFormFieldModule} from '@angular/material/form-field';
@NgModule({
  declarations: [
    PrivateLeagueRankingComponent,
    CreateLeagueComponent
  ],
  imports: [
    CommonModule,
    PrivateLeagueRoutingModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule
  ]
})
export class PrivateLeagueModule { }
