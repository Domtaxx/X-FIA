import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { NetworkService } from 'src/app/services/network.service';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { alertMessages } from 'src/app/const/messages';
import { dateValidations } from 'src/app/validations/dateValidations';
import { invalid } from '@angular/compiler/src/render3/view/util';
import { appSettings } from 'src/app/const/appSettings';
@Component({
  selector: 'app-create-tournament', 
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  tournamentForm=new FormGroup({
    tournamentName:new FormControl('',[Validators.required,Validators.maxLength(30)]),
    initialDate:new FormControl('',Validators.required),
    finalDate: new FormControl('',Validators.required),
    initialTime: new FormControl('',Validators.required),
    finalTime:new FormControl('',Validators.required),
    budget: new FormControl('',[Validators.required,Validators.min(1)]),
    rules:new FormControl('',[Validators.required,Validators.maxLength(1000)])
  });
  tournamentName='tournamentName';
  initialDate='initialDate';
  finalDate='finalDate';
  initialTime='initialTime';
  finalTime='finalTime';
  budget='budget';
  rules='rules'
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
  submit(){
    if(!this.validateInput()){
      return
    }
    var tournamentName=this.tournamentForm.controls[this.tournamentName].value;
    var iDate=this.tournamentForm.controls[this.initialDate].value;
    var fDate=this.tournamentForm.controls[this.finalDate].value;
    var iTime=this.tournamentForm.controls[this.initialTime].value;
    var fTime=this.tournamentForm.controls[this.finalTime].value;
    var budget=this.tournamentForm.controls[this.budget].value;
    var rules=this.tournamentForm.controls[this.rules].value;

    
    var httpParam={
      nombreCm: tournamentName,
      fechaDeInicio: iDate,
      horaDeInicio: iTime,
      fechaDeFin: fDate,
      horaDeFin: fTime,
      descripcionDeReglas: rules
    }
    this.backend.post_request(appSettings.tournamentRoute,httpParam).subscribe( // tournament post request
      (result)=>{//sucess case
        this.tournamentForm.reset()
        this.swal.showSuccess(alertMessages.rejected,alertMessages.sucessTournament+result)

      },
      (error)=>{
        var errorCode=error.error;
        console.log(errorCode)
        if(errorCode==this.errorCode.simultaneousTournament){ //a tournament already exists in those dates
          this.showError(alertMessages.rejected,alertMessages.simultaneousTournaments)
        }
        else{//server error
        this.showError(alertMessages.rejected,alertMessages.serverInterrupt)
        }
      }
      
    )
    
  }
  

  validateInput():boolean{
    if(!this.tournamentForm.valid){
      this.showError(alertMessages.invalidFieldsHeader,alertMessages.invalidFieldsBody)
      return false;
    }
    var initialDate=this.tournamentForm.get(this.initialDate)?.value
    var finalDate=this.tournamentForm.get(this.finalDate)?.value
    if(dateValidations.inThePast(initialDate) || dateValidations.inThePast(finalDate)){ //check that neither of the dates are on the past
      this.showError(alertMessages.rejectedDateHeader,alertMessages.inThePast)
      return false;
    }
    if(!dateValidations.continousDate(initialDate,finalDate)){//check finaldate is after initial date
      console.log(alertMessages.invalidDateElapse)
      this.showError(alertMessages.rejectedDateHeader,alertMessages.invalidDateElapse);
      return false;
    }
    var initialTime=this.tournamentForm.get(this.initialTime)?.value;
    var finalTime=this.tournamentForm.get(this.finalTime)?.value;
    if(!dateValidations.continousTime(initialDate,finalDate,initialTime,finalTime)){//if finaldate and initial date are on the same day, check for the initial and final time
      this.showError(alertMessages.rejectedDateHeader,alertMessages.invalidTimeElapsed);
      return false;
      
    }
   
    
    return true;
  }
  showError(header:string,body:string){
    this.swal.showError(header,body)
  }


}
