import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { NetworkService } from 'src/app/services/network.service';
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
  constructor(private backend:NetworkService) { }

  ngOnInit(): void {
    this.tournamentForm.get('tournamentName')?.valueChanges.subscribe(selectedValue=>{
      console.log(selectedValue)
    })
  }
  submit(){
    //var valid=this.tournamentForm.get('tournamentName')?.valid
    //this.tournamentForm.get('tournamentName')?.hasError('required')
    //var value=this.tournamentForm.get('tournamentName')?.value
    //Nombre Campeonato
    var requiredNameValidator=this.tournamentForm.get('tournamentName')?.hasError('required');
    var maxLenNameValidator=this.tournamentForm.get('tournamentName')?.hasError('maxLength')
    if(requiredNameValidator){
      alert("El nombre del campeonato no puede estar vacio")
      return
    }
    if(maxLenNameValidator){
      alert('El nombre del campeonato no puede ser mayor a 30 caracteres')
      return
    }
    console.log("imprimi")
   // console.log(value)
    
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
