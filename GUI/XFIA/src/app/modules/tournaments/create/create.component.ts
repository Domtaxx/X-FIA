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
  lab={
    Name:'tournamentName',
    dateI:'initialDate',
    dateE:'finalDate',
    timeS:'initilTime',
    timeE:'finalTime',
    budget:'budget',
    rules:'rules'

  }
  constructor(private backend:NetworkService,private swal:SweetAlertService) { }

  ngOnInit(): void {
    this.tournamentForm.get('tournamentName')?.valueChanges.subscribe(selectedValue=>{
      console.log(selectedValue)
    })
  }
  submit(){
    if(!this.tournamentForm.valid){
      this.swal.showError("Errores en los campos",'Alguno de los campos indicados no cumple con las reglas, por favor revisar el texto bajo los cuadros')
      return
    }
    var initialDate=this.tournamentForm.get('initialDate')?.value
    var finalDate=this.tournamentForm.get('finalDate')?.value
    console.log("imprimi")
    if(initialDate>finalDate){
      
      this.swal.showError("Fechas Invalidas","La fecha final debe ser mayor o igual a la fecha inicial")
      return
    }
    else if(initialDate==finalDate){
      var initialTime=this.tournamentForm.get('initialTime')?.value;
      var finalTime=this.tournamentForm.get('finalTime')?.value;
      if(initialTime>=finalTime){
        this.swal.showError('Errores en las fecha',"Si la competencia inicia y termina el mismo dia, la hora inicial debe ser menor a la fecha final")
        return
      }
      
    }
    
  }
  /*
  noValue(event:any){
    var isEmpty=this.tournamentForm.get('tournamentName')?.hasError('required')
    if(isEmpty){
      console.log(event.target.getAttribute('name'))
      //alert("El espacio no puede estar vacio")
    }
    
  }
  */

}
