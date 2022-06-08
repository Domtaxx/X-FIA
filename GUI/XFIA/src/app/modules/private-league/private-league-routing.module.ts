import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PrivateLeagueRankingComponent } from './private-league-ranking/private-league-ranking.component';
import { CreateLeagueComponent } from './create-league/create-league.component';
const routes: Routes = [
{path:'ranking',component:PrivateLeagueRankingComponent},
{path:'create',component:CreateLeagueComponent}
{path:' ',redirectTo:'/ranking'}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrivateLeagueRoutingModule { }