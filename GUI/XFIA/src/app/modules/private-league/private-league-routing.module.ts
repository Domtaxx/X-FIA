import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PrivateLeagueRankingComponent } from './private-league-ranking/private-league-ranking.component';
import { CreateLeagueComponent } from './create-league/create-league.component';
const routes: Routes = [
{path:'ranking',component:PrivateLeagueRankingComponent},
{path:' ',redirectTo:'/ranking'},
{path:'create',component:CreateLeagueComponent}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrivateLeagueRoutingModule { }