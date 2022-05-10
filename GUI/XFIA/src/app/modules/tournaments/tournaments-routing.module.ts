import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TournamentsComponent } from './tournaments.component';
import { CreateComponent } from './create/create.component';
import { CreateRaceComponent } from './races/create/create.component';

const routes: Routes = [{ path: '', component: TournamentsComponent },
{
  path:'create',component:CreateComponent,
},
{
  path:'race/create',component:CreateRaceComponent
}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TournamentsRoutingModule { }
