import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TournamentsRoutingModule } from './tournaments-routing.module';
import { TournamentsComponent } from './tournaments.component';
import { CreateComponent } from './create/create.component';


@NgModule({
  declarations: [
    TournamentsComponent,
    CreateComponent
  ],
  imports: [
    CommonModule,
    TournamentsRoutingModule
  ]
})
export class TournamentsModule { }
