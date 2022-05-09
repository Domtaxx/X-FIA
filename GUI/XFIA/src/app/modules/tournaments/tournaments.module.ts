import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TournamentsRoutingModule } from './tournaments-routing.module';
import { TournamentsComponent } from './tournaments.component';
import { CreateComponent } from './create/create.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { CreateRaceComponent } from './races/create/create.component';
import {MatSelectModule} from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatOptionModule } from '@angular/material/core';
@NgModule({
  declarations: [
    TournamentsComponent,
    CreateComponent,
    CreateRaceComponent
  ],
  imports: [
    CommonModule,
    TournamentsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SweetAlert2Module, 
    MatSelectModule,
    MatFormFieldModule,
    MatOptionModule
  ]
})
export class TournamentsModule { }
