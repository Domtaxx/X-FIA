import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { NetworkService } from 'src/app/services/network.service';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
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
  constructor(private backend:NetworkService,private swal:SweetAlertService) { }

  ngOnInit(): void {
 
  }
  /*
  input: NA
  output: NA
  This funcion in called when the form its summited, checks for input mistakes and makes the http request to add the tournamente
  */
  submit(){
    if(!this.tournamentForm.valid){
      this.swal.showError("Errores en los campos",'Alguno de los campos indicados no cumple con las reglas, por favor revisar el texto bajo los cuadros')
      return
    }
    var initialDate=this.tournamentForm.get(this.initialDate)?.value
    var finalDate=this.tournamentForm.get(this.finalDate)?.value
    console.log("imprimi")
    if(initialDate>finalDate){//check finaldate is after initial date
      
      this.swal.showError("Fechas Invalidas","La fecha final debe ser mayor o igual a la fecha inicial")
      return
    }
    else if(initialDate==finalDate){//if finaldate and initial date are on the same day, check for the initial and final time
      var initialTime=this.tournamentForm.get(this.initialTime)?.value;
      var finalTime=this.tournamentForm.get(this.finalTime)?.value;
      if(initialTime>=finalTime){
        this.swal.showError('Errores en las fecha',"Si la competencia inicia y termina el mismo dia, la hora inicial debe ser menor a la fecha final")
        return
      }
      
    }
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    var currentDate = yyyy + '-' + mm + '-' + dd;
    if(initialDate<currentDate || finalDate<currentDate){ //check that neither of the dates are on the past
      this.swal.showError("Error de fechas",'No es posible agregar carreras en el pasado')
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
      "nombreCm": tournamentName,
      "fechaDeInicio": iDate,
      "horaDeInicio": iTime,
      "fechaDeFin": fDate,
      "horaDeFin": fTime,
      "descripcionDeReglas": rules
    }
    this.backend.post_request('admin/Campeonato',httpParam).subscribe( // tournament post request
      (result)=>{//sucess case
        this.tournamentForm.reset()
        this.swal.showSuccess('Exito','Se ha agregado con éxito, la llave generada es '+result)
        console.log(result)

      },
      (error)=>{
        var errorCode=error.error;
        if(errorCode==1){ //a tournament already exists in those dates
          this.swal.showError('No se ha podido procesar la solicitud','No se ha podido agregar el campeonato, recuerde que no es posible que existan dos campeonatos ocurriendo en fechas simultaneas')
        }
        else{//server error
        this.swal.showError('No se ha podido procesar la solicitud','No se ha podido conectar con el servidor')
        }
      }
      
    )
    
  }


}
