import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl,Validators} from '@angular/forms';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { NetworkService } from 'src/app/services/network.service';
import { tournamentInterface } from 'src/app/interface/interfaces';
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
  /*
  input: NA
  output:NA
  This function is called as an angular event once the page elements have been loaded.
  In this case, it is in charge of obtaining the values ​​for the country input and the championship input.
  */
  ngOnInit(): void {
    this.backend.get_request('admin/Campeonato',{}).subscribe((response)=>{//request to get tournaments
      
      
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
      console.log('estoy en el error')
      console.log(error)
    })
    this.backend.get_request('Pais',{}).subscribe(//request to update countries
      (response)=>{
          console.log("paises");
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
   console.log( this.raceForm.controls['initialDate'].value);
    if(!this.raceForm.valid){//empty input, max length, min value and other input values validation
      this.swal.showError("Errores en los campos",'Alguno de los campos indicados no cumple con las reglas, por favor revisar el texto bajo los cuadros')
      return
    }
    var initialDate=this.raceForm.get('initialDate')?.value
    var finalDate=this.raceForm.get('finalDate')?.value
    console.log("imprimi")
    if(initialDate>finalDate){// compares the start date with the end date, in such a way that it is consistent
      
      this.swal.showError("Fechas Invalidas","La fecha final debe ser mayor o igual a la fecha inicial")
      return
    }
    else if(initialDate==finalDate){//if both initial and final date are on the same date, checks the initial and final time
      var initialTime=this.raceForm.get('initialTime')?.value;
      var finalTime=this.raceForm.get('finalTime')?.value;
      if(initialTime>=finalTime){
        this.swal.showError('Errores en las fecha',"Si la competencia inicia y termina el mismo dia, la hora inicial debe ser menor a la fecha final")
        return
      }
      
    }
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); 
    var yyyy = today.getFullYear();

    var currentDate = yyyy + '-' + mm + '-' + dd;
    if(initialDate<currentDate || finalDate<currentDate){//Verifica que las fechas indicadas no esten en el pasado
      this.swal.showError("Error de fechas",'No es posible agregar carreras en el pasado')
      return
    }
    
    var raceName=this.raceForm.controls[this.raceName].value;
    var tournamentName=this.raceForm.controls[this.tournamentName].value;
    var streenName=this.raceForm.controls[this.streetName].value;
    var countryName=this.raceForm.controls[this.countryName].value;
    var iDate=this.raceForm.controls[this.initialDate].value;
    var fDate=this.raceForm.controls[this.finalDate].value;
    var iTime=this.raceForm.controls[this.initialTime].value;
    var fTime=this.raceForm.controls[this.finalTime].value;

    var httpParams={
      "nombre": raceName,
      "pais": countryName,
      "estado": 0,
      "nombreDePista": streenName,
      "fechaDeInicio": iDate,
      "horaDeInicio": iTime,
      "fechaDeFin": fDate,
      "horaDeFin": fTime,
      "campeonatoKey": tournamentName
    }
   
    
    this.backend.post_request('Admin/Carreras',httpParams).subscribe(//races post
      (result)=>{ //caso de exito
        this.raceForm.reset()
        this.swal.showSuccess('Se ha agregado corrrectamente',
        'La carrera ha sido agregada al campeonato seleccionado' )
        
      },
      (error)=>{
        var errorCode=error.error;
        if(errorCode==1){//race name already exists
          this.swal.showError('No se ha podido agregar','el nombre indicado ya ha sido usado para otra carrera en el torneo seleccionado')
        }
        else if(errorCode==2){//race dates outbounds of the tournament dates
          this.swal.showError('No se ha podido agregar','recuerde que las fechas de la carrera deben estar dentro de los limites del campeonato')
         
        }
        else if(errorCode==3){//there are races in the dates already
            this.swal.showError('No se ha podido agregar','Ya existen carreras activas en el periodo indicado')
        }
        else{//server error
          this.swal.showError('No se ha podido agregar','Error con el servidor')
        }
        
      }
    )
    
  }
  

}
