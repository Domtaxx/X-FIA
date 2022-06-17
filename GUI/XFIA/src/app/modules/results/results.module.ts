import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddResulComponent } from './add-resul/add-resul.component';
import { ResultsRoutingModule } from './results-routing.module';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import {MatIconModule} from '@angular/material/icon';
@NgModule({
  declarations: [
    AddResulComponent
  ],
  imports: [
    CommonModule,
    ResultsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule
  ]
})
export class ResultsModule { }
