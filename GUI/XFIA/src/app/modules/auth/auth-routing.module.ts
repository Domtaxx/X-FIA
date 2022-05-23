import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FullRegisterComponent } from './full-register/full-register.component';
import { UserRegisterComponent } from './user-register/user-register.component';

const routes: Routes = [
{
  path:'register',component:FullRegisterComponent,
},
{path:'',redirectTo:'/register'}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
