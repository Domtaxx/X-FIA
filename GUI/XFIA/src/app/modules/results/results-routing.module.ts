import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddResulComponent } from './add-resul/add-resul.component';

const routes: Routes = [
{path:'add',component:AddResulComponent},
{path:' ',redirectTo:'/add'}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ResultsRoutingModule { }