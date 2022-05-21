import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { NetworkService } from 'src/app/services/network.service';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { alertMessages } from 'src/app/const/messages';
import { appSettings } from 'src/app/const/appSettings';
import { fileValidations } from 'src/app/validations/fileValidation';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  userRegisterForm=new FormGroup({
    userName:new FormControl('',[Validators.required,Validators.maxLength(30)]),
    email:new FormControl('',[Validators.required,Validators.email,Validators.maxLength(256)]),
    password:new FormControl('',[Validators.required,Validators.maxLength(8),Validators.minLength(8),Validators.pattern('^([a-zA-Z]*[0-9]*)*')]),
    confirmPassword:new FormControl('',[Validators.required,Validators.maxLength(8),Validators.minLength(8),Validators.pattern('^([a-zA-Z]*[0-9]*)*')]),
    teamName:new FormControl('',[Validators.required,Validators.maxLength(30)]),
    countryName:new FormControl('',[Validators.required]),
    image:new FormControl('',[Validators.required])
  });
  showPass1=false;
  showPass2=false;
  fileUploaded=false;
  countries:string[]=["hola"]

 
  errorCode={
    simultaneousTournament:1
  }
  constructor(private backend:NetworkService,private swal:SweetAlertService) { }

  ngOnInit(): void {
   
 
  }
  /*
  input: NA
  output: NA
  This funcion in called when the form its summited, checks for input mistakes and makes the http request to add the tournamente
  */
  submit(){}

  chageVisibilityPass1(){
    this.showPass1=!this.showPass1;
  }
  chageVisibilityPass2(){
    this.showPass2=!this.showPass2;
  }
  onFileUploaded(fileEvent:any){
    const file: File = fileEvent.target.files[0];
    if(!fileValidations.checkImage(file)){
      this.showError("error","formato de archivo incompatible")
    }


  }
  showError(header:string,body:string){
    this.swal.showError(header,body);

  }



}
