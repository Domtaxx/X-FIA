import { Component, OnInit } from '@angular/core';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { FormGroup,FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-register-team',
  templateUrl: './register-team.component.html',
  styleUrls: ['./register-team.component.css']
})
export class RegisterTeamComponent implements OnInit {
  
  teamForm= new FormGroup({
    pilot1:new FormControl('',[Validators.required]),
    pilot2:new FormControl('',[Validators.required]),
    pilot3:new FormControl('',[Validators.required]),
    pilot4:new FormControl('',[Validators.required]),
    pilot5:new FormControl('',[Validators.required]),
    car: new FormControl('',Validators.required)

  })
  constructor(swal:SweetAlertService) { }

  ngOnInit(): void {
  }

}
