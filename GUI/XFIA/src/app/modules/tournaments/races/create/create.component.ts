import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl,Validators} from '@angular/forms';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { NetworkService } from 'src/app/services/network.service';

@Component({
  selector: 'app-create-race',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateRaceComponent implements OnInit {

  constructor(private backend:NetworkService,private swal:SweetAlertService) { }
  countries=[]
  tournaments=[{nombreCm:'opcion A',llave:'llave A'},{nombreCm:'opcion B',llave:'llave B'}]
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
  ngOnInit(): void {
    this.backend.get_request('admin/Campeonato',{}).subscribe((response)=>{
      
      
      for(var i=0;i<response.length;i++){
        console.log(response[i].nombreCm)
        var tourn={nombreCm: response[i].nombreCm,
          llave:response[i].llave
        }
        this.tournaments.push(tourn)
      }
      this.tournaments = [...this.tournaments]
    },(error)=>{
      console.log('estoy en el error')
      console.log(error)
    })
  }
  submit(){
   console.log( this.raceForm.controls['initialDate'].value);
    if(!this.raceForm.valid){
      this.swal.showError("Errores en los campos",'Alguno de los campos indicados no cumple con las reglas, por favor revisar el texto bajo los cuadros')
      return
    }
    var initialDate=this.raceForm.get('initialDate')?.value
    var finalDate=this.raceForm.get('finalDate')?.value
    console.log("imprimi")
    if(initialDate>finalDate){
      
      this.swal.showError("Fechas Invalidas","La fecha final debe ser mayor o igual a la fecha inicial")
      return
    }
    else if(initialDate==finalDate){
      var initialTime=this.raceForm.get('initialTime')?.value;
      var finalTime=this.raceForm.get('finalTime')?.value;
      if(initialTime>=finalTime){
        this.swal.showError('Errores en las fecha',"Si la competencia inicia y termina el mismo dia, la hora inicial debe ser menor a la fecha final")
        return
      }
      
    }
  }

}
