import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl,Validators} from '@angular/forms';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { NetworkService } from 'src/app/services/network.service';
import { tournamentInterface } from 'src/app/interface/interfaces';
import { appSettings } from 'src/app/const/appSettings';
import { dateValidations } from 'src/app/validations/dateValidations';
import { alertMessages } from 'src/app/const/messages';
@Component({
  selector: 'app-create-race',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateRaceComponent implements OnInit {

  constructor(private backend:NetworkService,private swal:SweetAlertService) { }
  countries:string[]=[]
  tournaments:tournamentInterface[]=[]
  raceForm=new FormGroup({
    raceName:new FormControl('',[Validators.required,Validators.maxLength(30)]),
    tournamentName:new FormControl('',Validators.required),
    streetName:new FormControl('',[Validators.required,Validators.maxLength(30)]),
    countryName:new FormControl('',Validators.required),
    initialDate:new FormControl('',Validators.required),
    finalDate: new FormControl('',Validators.required),
    initialTime: new FormControl('',Validators.required),
    finalTime:new FormControl('',Validators.required)
  });
  raceName='raceName';
  tournamentName='tournamentName';
  streetName='streetName';
  countryName='countryName';
  initialDate='initialDate';
  finalDate='finalDate';
  initialTime='initialTime';
  finalTime='finalTime';
  errorCodes={
    duplicatedName:1,
    outboundDate:2,
    simultaneousRaces:3
  }
  /*
  input: NA
  output:NA
  This function is called as an angular event once the page elements have been loaded.
  In this case, it is in charge of obtaining the values ​​for the country input and the championship input.
  */
  ngOnInit(): void {
    this.backend.get_request(appSettings.tournamentRoute,{}).subscribe((response)=>{//request to get tournaments
      
      
      for(var i=0;i<response.length;i++){
        console.log(response[i])
        console.log(response[i].nombreCm)
        var tourn={nombreCm: response[i].name,
          llave:response[i].key
        }
        this.tournaments.push(tourn)//updates input values
      }
      this.tournaments = [...this.tournaments]
    },(error)=>{
    })
    this.backend.get_request(appSettings.countryRoute,{}).subscribe(//request to update countries
      (response)=>{
  
          for(var i=0;i<response.length;i++){
            var countryName=response[i].name
            this.countries.push(countryName)
          }
          this.countries=[...this.countries]//updaties countries arrya
      }
    )
  }
  /*
  input: NA
  output:NA
  This method is responsible for submitting the race, for this it verifies some errors
  as in empty spaces or dates
  */
  submit(){
    if(!this.validations)return;//if the validation conditions are not meet, end the method
    var raceName=this.raceForm.controls[this.raceName].value;
    var tournamentName=this.raceForm.controls[this.tournamentName].value;
    var streenName=this.raceForm.controls[this.streetName].value;
    var countryName=this.raceForm.controls[this.countryName].value;
    var iDate=this.raceForm.controls[this.initialDate].value;
    var fDate=this.raceForm.controls[this.finalDate].value;
    var iTime=this.raceForm.controls[this.initialTime].value;
    var fTime=this.raceForm.controls[this.finalTime].value;

    var httpParams={
      nombre: raceName,
      pais: countryName,
      estado: 0,
      nombreDePista: streenName,
      fechaDeInicio: iDate,
      horaDeInicio: iTime,
      fechaDeFin: fDate,
      horaDeFin: fTime,
      campeonatoKey: tournamentName
    }
   
    
    this.backend.post_request(appSettings.adminRaceRoute,httpParams).subscribe(//races post
      (result)=>{ //caso de exito
        this.raceForm.reset()
        this.swal.showSuccess(alertMessages.successHeader,
        alertMessages.raceSuccess )
        
      },
      (error)=>{
        var errorCode=error.error;
        if(errorCode==this.errorCodes.duplicatedName){//race name already exists
          this.swal.showError(alertMessages.rejected,alertMessages.duplicatedRaceName)
        }
        else if(errorCode==this.errorCodes.outboundDate){//race dates outbounds of the tournament dates
          this.swal.showError(alertMessages.rejected,alertMessages.outsideBoundsRace)
         
        }
        else if(errorCode==this.errorCodes.simultaneousRaces){//there are races in the dates already
            this.swal.showError(alertMessages.rejected,alertMessages.simultaneousRace)
        }
        else{//server error
          this.swal.showError(alertMessages.rejected,alertMessages.serverInterrupt)
        }
        
      }
    )
    
  }
  validations():boolean{
    
    if(!this.raceForm.valid){//empty input, max length, min value and other input values validation
      this.showError(alertMessages.invalidFieldsHeader,alertMessages.invalidFieldsBody)
      return false
    }
    var initialDate=this.raceForm.get(this.initialDate)?.value
    var finalDate=this.raceForm.get(this.finalDate)?.value
    if(dateValidations.inThePast(initialDate)|| dateValidations.inThePast(finalDate)){//Verifica que las fechas indicadas no esten en el pasado
      this.showError(alertMessages.rejectedDateHeader,alertMessages.inThePast)
      return false
    }
    if(!dateValidations.continousDate(initialDate,finalDate)){// compares the start date with the end date, in such a way that it is consistent
      
      this.showError(alertMessages.rejectedDateHeader,alertMessages.invalidDateElapse)
      return false;
    }
    var initialTime=this.raceForm.get(this.initialTime)?.value;
    var finalTime=this.raceForm.get(this.finalTime)?.value;
    if(!dateValidations.continousTime(initialDate,finalDate,initialTime,finalTime)){//if both initial and final date are on the same date, checks the initial and final time

      this.showError(alertMessages.rejectedDateHeader,alertMessages.invalidTimeElapsed)
      return false
    }
    
    return true
  }
  showError(header:string,body:string){
    this.swal.showError(header,body)
  }
  

}
