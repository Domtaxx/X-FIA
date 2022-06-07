import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PublicLeagueRankingComponent } from './public-league-ranking/public-league-ranking.component';

const routes: Routes = [
{path:'ranking',component:PublicLeagueRankingComponent},
{path:' ',redirectTo:'/ranking'}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicLeagueRoutingModule { }