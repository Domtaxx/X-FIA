import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

const routes: Routes = [
  {
    path: 'tournaments',
    loadChildren: () => import('./modules/tournaments/tournaments.module').then(m => m.TournamentsModule)
  },
  {path:'',redirectTo:'/tournaments',pathMatch:'full'}
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes),SweetAlert2Module.forRoot()],
  exports: [RouterModule]
})
export class AppRoutingModule { }
