import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewProfileComponent } from './view-profile/view-profile.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
const routes: Routes = [
{path:'view/:email',component:ViewProfileComponent},
{path:' ',redirectTo:'/view'},
{path:'edit',component:EditProfileComponent}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserProfileRoutingModule { 
    
}