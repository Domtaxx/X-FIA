import { Component, OnInit } from '@angular/core';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { FormGroup,FormControl, Validators } from '@angular/forms';
import { pilotInterface } from 'src/app/interface/interfaces';

@Component({
  selector: 'app-register-team',
  templateUrl: './register-team.component.html',
  styleUrls: ['./register-team.component.css']
})
export class RegisterTeamComponent implements OnInit {
  memberIndex:number;
  memberArray:string[];
  inputHintArray:string[];
  selectedPilots:Map<number,pilotInterface>;
  availablePilots:pilotInterface[];
  teamForm= new FormGroup({
    pilot1:new FormControl('',[Validators.required]),
    pilot2:new FormControl('',[Validators.required]),
    pilot3:new FormControl('',[Validators.required]),
    pilot4:new FormControl('',[Validators.required]),
    pilot5:new FormControl('',[Validators.required]),
    car: new FormControl('',Validators.required)

  })
  constructor(swal:SweetAlertService) { 
    this.memberArray=[
      'Piloto 1',
      'Piloto 2',
      'Piloto 3',
      'Piloto 4',
      'Piloto 5',
      'Auto'
    ]
    this.inputHintArray=[
      'Corredor',
      'Corredor',
      'Corredor',
      'Corredor',
      'Corredor',
      'Auto'
    ]
    this.memberIndex=0;
    this.selectedPilots=new Map()
    this.availablePilots=[];
    

  }

  ngOnInit(): void {
    
  }
  indexUp(){
    this.memberIndex++;
    if(this.memberIndex>=this.memberArray.length){
      this.memberIndex=0;
    }
  }
  indexDown(){
    this.memberIndex--;
    if(this.memberIndex<0){
      this.memberIndex=this.memberArray.length-1;
    }
  }
  getCurrentName(){
    
  }

}
