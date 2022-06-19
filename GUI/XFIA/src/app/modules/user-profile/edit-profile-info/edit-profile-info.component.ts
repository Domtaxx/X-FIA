import { Component, OnInit } from '@angular/core';
import { UntypedFormGroup,UntypedFormControl } from '@angular/forms';
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
  selector: 'app-edit-profile-info',
  templateUrl: './edit-profile-info.component.html',
  styleUrls: ['./edit-profile-info.component.css']
})
export class EditProfileInfoComponent implements OnInit {

  userRegisterForm=new UntypedFormGroup({
    userName:new UntypedFormControl('',[Validators.required,Validators.maxLength(30)]),
    teamName:new UntypedFormControl('',[Validators.required,Validators.maxLength(30)]),
    countryName:new UntypedFormControl('',[Validators.required]),
    image:new UntypedFormControl('',[Validators.required])
  });
 
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
    this.image=''
    this.imageFile=null;
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
    this.fileUploaded=false;
    this.getCountries()
     this.image="";
    this.imageFile=undefined;
  }
  setInitialData(userName:string,TeamName:string,TeamLogo:string){
    this.userRegisterForm.controls['userName'].setValue(userName);
    this.userRegisterForm.controls['teamName'].setValue(TeamName);
    //this.userRegisterForm.controls['image'].setValue(TeamLogo);
    this.setImage(TeamLogo);
    this.fileUploaded=true;

  }


}
