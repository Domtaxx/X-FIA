import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TournamentsRoutingModule } from './tournaments-routing.module';
import { TournamentsComponent } from './tournaments.component';
import { CreateComponent } from './create/create.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
@NgModule({
  declarations: [
    TournamentsComponent,
    CreateComponent
  ],
  imports: [
    CommonModule,
    TournamentsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SweetAlert2Module
  ]
})
export class TournamentsModule { }
