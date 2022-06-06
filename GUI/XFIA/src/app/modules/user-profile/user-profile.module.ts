import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewProfileComponent } from './view-profile/view-profile.component';
import { UserProfileRoutingModule } from './user-profile-routing.module';
import {MatTabsModule} from '@angular/material/tabs';

@NgModule({
  declarations: [
    ViewProfileComponent
  ],
  imports: [
    CommonModule,
    UserProfileRoutingModule,
    MatTabsModule
  ]
})
export class UserProfileModule { }
