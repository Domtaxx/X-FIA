import { Component, OnInit } from '@angular/core';
import { UntypedFormGroup} from '@angular/forms';
import { UntypedFormControl } from '@angular/forms';
import { FormControl,FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';

import { alertMessages } from 'src/app/const/messages';
import { privateLeagueCreateService } from 'src/app/dataProviderServices/privateLeagueCreate';
import { alertMessage, privateLeagueInfo } from 'src/app/interface/interfaces';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
@Component({
  selector: 'app-create-league',
  templateUrl: './create-league.component.html',
  styleUrls: ['./create-league.component.css']
})
export class CreateLeagueComponent implements OnInit {


  privateLeagueForm=new UntypedFormGroup({
    name:new UntypedFormControl('',[Validators.required,Validators.maxLength(30)]),
    participants:new UntypedFormControl('',[Validators.required])
  });
  memberNumber!:number[];
  constructor(private dataProvider:privateLeagueCreateService,private swal:SweetAlertService) { 
    this.startMemberNumber();
  }

  ngOnInit(): void {
    
  }

  startMemberNumber(){
    this.memberNumber=[];
    for(var i=0;i<20;i++){
      this.memberNumber.push(i+1);
    }
  }

  createLeague(){
    if(!this.privateLeagueForm.valid){
      this.swal.showError(alertMessages.rejected,alertMessages.invalidFieldsBody)
      return;
    }
    const name=this.privateLeagueForm.controls['name'].value;
    const participants=this.privateLeagueForm.controls['participants'].value;
    this.dataProvider.createLeague(name,participants,
      (info:any)=>{
        this.callSucess(info)
      },
      (message:alertMessage)=>{
        this.callMistake(message)
      }
        
      )
  }
  callSucess(key:string){
    this.swal.showSuccess(alertMessages.successHeader,alertMessages.privateLeagueCreatedBody+' '+key);
  }
  callMistake(message:alertMessage){
    this.swal.showError(message.header,message.body);
  }

}
