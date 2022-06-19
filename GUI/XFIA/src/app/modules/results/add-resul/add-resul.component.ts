import { Component, OnInit } from '@angular/core';
import { raceInterface, tournamentInterface } from 'src/app/interface/interfaces';
import { UntypedFormControl, Validators } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { ResultsService } from 'src/app/dataProviderServices/results.service';
@Component({
  selector: 'app-add-resul',
  templateUrl: './add-resul.component.html',
  styleUrls: ['./add-resul.component.css']
})
export class AddResulComponent implements OnInit {
  tournaments:tournamentInterface[];
  races:raceInterface[];

  resultForm=new UntypedFormGroup(
    {
      tournament:new UntypedFormControl('',[Validators.required]),
      race:new UntypedFormControl('',[Validators.required]),
      file:new UntypedFormControl('',[Validators.required])
    },
    {}
  )
  constructor(private dataProvider:ResultsService) { }
  
  ngOnInit(): void {
    this.resultForm.controls['tournament'].valueChanges.subscribe(
      ()=>{
        this.tournamentUpdate();
      }
    )
  }

  tournamentUpdate(){
    const key=this.resultForm.controls['tournament'].value;
    this.dataProvider.getRaces(key,(races:raceInterface[])=>{
      this.races=races;
      this.races=[...this.races];
    })

  }
  submitResults(){
    const tournamentKey:string=this.resultForm.controls['tournament'].value;
    const race:string=this.resultForm.controls['race'].value;
    const file:File=this.resultForm.controls['file'].value;
    this.dataProvider.uploadResults(tournamentKey,race,file);
  }

}
