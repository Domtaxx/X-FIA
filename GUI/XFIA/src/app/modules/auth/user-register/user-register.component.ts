import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { NetworkService } from 'src/app/services/network.service';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { alertMessages } from 'src/app/const/messages';
import { appSettings } from 'src/app/const/appSettings';
import { fileValidations } from 'src/app/validations/fileValidation';
import { matchPassword } from 'src/app/validations/customFieldValidations';
import { fileProcessFuncion } from 'src/app/functions/fileprocess';
import { country } from 'src/app/interface/interfaces';
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
  }
  ,{validators:[matchPassword('password','confirmPassword')]});
  showPass1=false;
  showPass2=false;
  fileUploaded=false;
  countries:string[]=[]
  image?:string="";
  imageFile?:File;

 
  errorCode={
    simultaneousTournament:1
  }
  constructor(private swal:SweetAlertService,private backend:NetworkService) { }

  ngOnInit(): void {
    this.resetData()
   
 
  }
  /*
  input: NA
  output: NA
  This funcion in called when the form its summited, checks for input mistakes and makes the http request to add the tournamente
  */
  submit(){}
  /*
  ouput: none
  input:none
  description: Change the visibility for the password 1, if was visible, it hides it and the other way around
  */
  chageVisibilityPass1(){
    this.showPass1=!this.showPass1;
  }
  /*
  ouput: none
  input:none
  description: Change the visibility for the password 2, if was visible, it hides it and the other way around
  */
  chageVisibilityPass2(){
    this.showPass2=!this.showPass2;
  }
  /*
  input: file
  output: none
  description: event called when a file its entered in the file input, it asked the file to be procesed to be
  show in the view
  */
  onFileUploaded(fileEvent:any){
    const file: File = fileEvent.target.files[0];
    this.imageFile=file;
    console.log(file)
    if(!fileValidations.checkImage(file)){
      this.showError(alertMessages.rejectedImageFileHeader,alertMessages.rejectedImageFileBody)
    }
    else this.loadFile(file);


  }
  /*
  input: header message and body message
  output: none
  description: ask the swal module to show the message with the information given
  */
  showError(header:string,body:string){
    this.swal.showError(header,body);

  }
  /*
  input: header message and body message
  output: none
  description: ask the swal module to show the message with the information given
  */
  showSucess(header:string,body:string){
    this.swal.showSuccess(header,body);
  }
  /*
  input:file
  output:none
  description: function called to get the processed file
  */
  loadFile(file:File){
    this.fileUploaded=true;
   
   fileProcessFuncion(file,(value)=>{
     this.setImage(value);
   })

  }
/*
  input:string url
  output:none
  description: sets the image to be shown
  */
  setImage(img:string){
    this.image=img;
    console.log(this.image)
  }
  /*
  input:none
  output:none
  description: resets the image making available to upload again
  */
  resetImage(){
    this.userRegisterForm.controls['image'].reset();
    this.fileUploaded=false;
  }
  /*
  input:none
  output:none
  description: ask the network service to get the countries
  */
  getCountries(){
    this.backend.get_request(appSettings.countryRoute,{}).subscribe(
      (result:country[])=>{
        var countriesNames=[];
        for(var i=0;i<result.length;i++){
          countriesNames.push(result[i].name)
        }
        this.countries=countriesNames;
        this.countries=[...this.countries]
      }
    )
  }

  resetData(){
    this.showPass1=false;
    this.showPass2=false;
    this.fileUploaded=false;
    this.getCountries()
     this.image="";
    this.imageFile=undefined;
  }



}
