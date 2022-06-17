import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddResulComponent } from './add-resul/add-resul.component';
import { ResultsRoutingModule } from './results-routing.module';


@NgModule({
  declarations: [
    AddResulComponent
  ],
  imports: [
    CommonModule,
    ResultsRoutingModule
  ]
})
export class ResultsModule { }
