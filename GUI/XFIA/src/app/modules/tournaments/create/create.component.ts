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
    tournamentName:new FormControl('',Validators.required),
    initialDate:new FormControl('',Validators.required),
    finalDate: new FormControl('',Validators.required),
    initialTime: new FormControl('',Validators.required),
    finalTime:new FormControl('',Validators.required),
    budget: new FormControl('',Validators.required),
    rules:new FormControl('',Validators.required)
  });
  constructor(private backend:NetworkService) { }

  ngOnInit(): void {
    this.tournamentForm.get('tournamentName')?.valueChanges.subscribe(selectedValue=>{
      console.log(selectedValue)
    })
  }
  submit(){
    var value=this.tournamentForm.get('tournamentName')?.value
    console.log("imprimi")
    console.log(value)
    this.backend.get_request('',{}).subscribe((user)=>{
      console.log(user)
    })
  }

}
