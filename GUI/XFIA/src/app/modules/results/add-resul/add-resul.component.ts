import { Component, OnInit } from '@angular/core';
import { raceInterface, tournamentInterface } from 'src/app/interface/interfaces';
import { UntypedFormControl, Validators } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { ResultsService } from 'src/app/dataProviderServices/results.service';
import { fileValidations } from 'src/app/validations/fileValidation';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { appSettings } from 'src/app/const/appSettings';
import { alertMessages } from 'src/app/const/messages';
@Component({
  selector: 'app-add-resul',
  templateUrl: './add-resul.component.html',
  styleUrls: ['./add-resul.component.css']
})
export class AddResulComponent implements OnInit {
  tournaments:tournamentInterface[];
  races:raceInterface[];
  File:File;
  resultForm=new UntypedFormGroup(
    {
      tournament:new UntypedFormControl('',[Validators.required]),
      race:new UntypedFormControl('',[Validators.required]),
      file:new UntypedFormControl('',[Validators.required])
    }
  )
  constructor(private dataProvider:ResultsService,private swal:SweetAlertService) { }
  
  ngOnInit(): void {
    this.resultForm.controls['tournament'].valueChanges.subscribe(
      ()=>{
        this.tournamentUpdate();
      }
    )
    this.getTournaments();
  }
  getTournaments(){
    this.dataProvider.getTournaments(
      (tournaments:tournamentInterface[])=>{
        this.tournaments=tournaments;
        this.tournaments=[...this.tournaments];
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
    this.dataProvider.uploadResults(tournamentKey,race,this.File)
  }
  onFileUploaded(fileEvent:any){
    const file: File = fileEvent.target.files[0];
    this.File=file;
    console.log(file)
    if(!fileValidations.checkData(file)){
      this.swal.showError(alertMessages.rejectedImageFileHeader,alertMessages.rejectedCSVFileBody)
    }
    ;


  }

}
