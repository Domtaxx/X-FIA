import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { LoginGuardGuard } from './guards/login-guard.guard';

const routes: Routes = [
  {
    path: 'tournaments',
    loadChildren: () => import('./modules/tournaments/tournaments.module').then(m => m.TournamentsModule),
    canActivateChild:[LoginGuardGuard]
  },
  {path:'',redirectTo:'/auth/login',pathMatch:'full',
    canActivateChild:[LoginGuardGuard]
  },
  {path:'auth',
  loadChildren:()=>import('./modules/auth/auth.module').then(m=>m.AuthModule)
  }, 
  {path:'publicLeague',loadChildren:()=>import('./modules/public-league/public-league.module').then(m=>m.PublicLeagueModule)},
  {
    path:'privateLeague',
    loadChildren:()=>import('./modules/private-league/private-league.module').then(m=>m.PrivateLeagueModule),
    canActivateChild:[LoginGuardGuard]
  },
  {
    path:'profile',
    loadChildren:()=>import('./modules/user-profile/user-profile.module').then(m=>m.UserProfileModule),
    canActivateChild:[LoginGuardGuard]
  },
  {
    path:'results',
    loadChildren:()=>import('./modules/results/results.module').then(m=>m.ResultsModule),
    canActivateChild:[LoginGuardGuard]
  }
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes),SweetAlert2Module.forRoot()],
  exports: [RouterModule]
})
export class AppRoutingModule { }
