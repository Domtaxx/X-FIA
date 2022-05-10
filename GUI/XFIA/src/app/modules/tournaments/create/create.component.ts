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
  rules='rules';


  constructor(private backend:NetworkService,private swal:SweetAlertService) { }
  
  ngOnInit(): void {
    
  }
  submit(){
    if(!this.tournamentForm.valid){
      this.swal.showError("Errores en los campos",'Alguno de los campos indicados no cumple con las reglas, por favor revisar el texto bajo los cuadros')
      return
    }
    var iDate=this.tournamentForm.get(this.initialDate)?.value
    var fDate=this.tournamentForm.get(this.finalDate)?.value
    if(iDate>fDate){
      
      this.swal.showError("Fechas Invalidas","La fecha final debe ser mayor o igual a la fecha inicial")
      return
    }
    else if(iDate==fDate){
      var initialTime=this.tournamentForm.get(this.initialTime)?.value;
      var finalTime=this.tournamentForm.get(this.finalTime)?.value;
      if(initialTime>=finalTime){
        this.swal.showError('Errores en las fecha',"Si la competencia inicia y termina el mismo dia, la hora inicial debe ser menor a la fecha final")
        return
      }
      
    }
    
    var tournamentName=this.tournamentForm.controls[this.tournamentName].value
    var initialDate=this.tournamentForm.controls[this.initialDate].value
    var finalDate=this.tournamentForm.controls[this.finalDate].value
    var iTime=this.tournamentForm.controls[this.initialTime].value
    var fTime=this.tournamentForm.controls[this.finalTime].value
    var budget=this.tournamentForm.controls[this.budget].value
    var rules=this.tournamentForm.controls[this.rules].value
    var httpParams={
      "nombreCm": tournamentName,
      "fechaDeInicio": initialDate,
      "horaDeInicio": iTime,
      "fechaDeFin": finalDate,
      "horaDeFin": fTime,
      "descripcionDeReglas": rules,
    }
    this.backend.post_request('admin/Campeonato',httpParams).subscribe((result)=>{
      this.swal.showSuccess("Se ingreso exitosamente",'Se ha agregado el campeonato de forma exitosa')
    },(error)=>{

      this.swal.showError('Se han presentado errores','No ha sido posible agregar el campeonato, recuerde que no puede coincidir dos campeonatos de forma simultanéa ')
      console.log(error)
    })
    
    
  }


}
