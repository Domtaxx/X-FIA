import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewProfileComponent } from './view-profile/view-profile.component';
import { UserProfileRoutingModule } from './user-profile-routing.module';
import {MatTabsModule} from '@angular/material/tabs';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { EditProfileInfoComponent } from './edit-profile-info/edit-profile-info.component';
import { EditProfileTeamComponent } from './edit-profile-team/edit-profile-team.component';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import {MatStepperModule} from '@angular/material/stepper';
import { FormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    ViewProfileComponent,
    EditProfileComponent,
    EditProfileInfoComponent,
    EditProfileTeamComponent
  ],
  imports: [
    CommonModule,
    UserProfileRoutingModule,
    MatTabsModule,
    MatIconModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatStepperModule,
    FormsModule
  ]
})
export class UserProfileModule { }
