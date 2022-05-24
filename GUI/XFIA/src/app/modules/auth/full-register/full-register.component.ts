import { Component, OnInit, ViewChild } from '@angular/core';
import { UserRegisterComponent } from '../user-register/user-register.component';
import { RegisterTeamComponent } from '../register-team/register-team.component';
import { MatStepper } from '@angular/material/stepper';
import { userRegisterRequest } from 'src/app/request/requestBuilder';
import { userRegisterInterface } from 'src/app/interface/interfaces';
import { NetworkService } from 'src/app/services/network.service';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { appSettings } from 'src/app/const/appSettings';
@Component({
  selector: 'app-full-register',
  templateUrl: './full-register.component.html',
  styleUrls: ['./full-register.component.css']
})
export class FullRegisterComponent implements OnInit {
  @ViewChild('userInfo') userForm!:UserRegisterComponent;
  @ViewChild('team1') team1!:RegisterTeamComponent;
  @ViewChild('team2') team2!:RegisterTeamComponent;
  constructor(private swal:SweetAlertService,private backend:NetworkService) { }

  ngOnInit(): void {
  }
  submit(){
    console.log(this.userForm.userRegisterForm.controls['userName'].value);
    if(!this.team2.teamForm.valid)return;
    this.makeRequest();
    

  }
  
  goBack(stepper: MatStepper){
    stepper.previous();
}

  goForward(stepper: MatStepper){
      stepper.next();
  }
  makeRequest(){
    const requestBody:userRegisterInterface=userRegisterRequest(this.userForm,this.team1,this.team2)
    this.backend.post_request_multipart(appSettings.userRegisterRoute,requestBody).subscribe(
      (sucess)=>{this.handleSucess(sucess)},
      (error)=>{this.handleMistake(error)}
    )
  }
  handleSucess(result:any){
    console.log(result)

  }
  handleMistake(result:any){
    console.log(result)

  }


}
