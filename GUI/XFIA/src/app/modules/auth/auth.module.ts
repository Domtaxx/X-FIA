import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRegisterComponent } from './user-register/user-register.component';
import { AuthRoutingModule } from './auth-routing.module';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import {MatSelectModule} from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatOptionModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { FullRegisterComponent } from './full-register/full-register.component';
import {MatStepperModule} from '@angular/material/stepper';
import { RegisterTeamComponent } from './register-team/register-team.component';
import {MatButtonModule} from '@angular/material/button';

@NgModule({
  declarations: [
    UserRegisterComponent,
    FullRegisterComponent,
    RegisterTeamComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SweetAlert2Module,
    MatSelectModule,
    MatFormFieldModule,
    MatOptionModule,
    MatIconModule,
    MatInputModule,
    MatStepperModule,
    MatButtonModule
  ]
})
export class AuthModule { }
