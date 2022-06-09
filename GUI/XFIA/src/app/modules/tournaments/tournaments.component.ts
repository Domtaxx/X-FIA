import { Component, OnInit } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { FormControl,FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { saveData } from 'src/app/functions/browserDataInfo';
import { localStorageNames } from 'src/app/const/localStorageNames';
@Component({
  selector: 'app-tournaments',
  templateUrl: './tournaments.component.html',
  styleUrls: ['./tournaments.component.css']
})
export class TournamentsComponent implements OnInit {



  LoginForm:FormGroup=new UntypedFormGroup({
    userEmail:new UntypedFormControl('',[Validators.required,Validators.maxLength(30)])

  })
  
  constructor() { }

  ngOnInit(): void {
  }
  log(){
    const email=this.LoginForm.controls['userEmail'].value;
    console.log(email)
    saveData(localStorageNames.email,email)
  }
 

}
