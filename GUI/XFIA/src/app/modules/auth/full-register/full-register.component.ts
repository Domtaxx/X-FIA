import { Component, OnInit, ViewChild } from '@angular/core';
import { UserRegisterComponent } from '../user-register/user-register.component';
import { RegisterTeamComponent } from '../register-team/register-team.component';
@Component({
  selector: 'app-full-register',
  templateUrl: './full-register.component.html',
  styleUrls: ['./full-register.component.css']
})
export class FullRegisterComponent implements OnInit {
  @ViewChild('userInfo') userForm!:UserRegisterComponent;
  @ViewChild('team1') team1!:RegisterTeamComponent;
  @ViewChild('team2') team2!:RegisterTeamComponent;
  constructor() { }

  ngOnInit(): void {
  }
  action(){
    console.log(this.userForm.userRegisterForm.controls['userName'].value);

  }


}
