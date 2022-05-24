import { Component, OnInit, ViewChild } from '@angular/core';
import { UserRegisterComponent } from '../user-register/user-register.component';
import { RegisterTeamComponent } from '../register-team/register-team.component';
import { MatStepper } from '@angular/material/stepper';
import { userRegisterRequest } from 'src/app/request/requestBuilder';
import { userRegisterInterface } from 'src/app/interface/interfaces';
import { NetworkService } from 'src/app/services/network.service';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { appSettings } from 'src/app/const/appSettings';
import { userRegisterMessage } from 'src/app/errorCodeHandler/errorHandler';
import { alertMessage } from 'src/app/interface/interfaces';
import { alertMessages } from 'src/app/const/messages';
import { temporaryAllocator } from '@angular/compiler/src/render3/view/util';
@Component({
  selector: 'app-full-register',
  templateUrl: './full-register.component.html',
  styleUrls: ['./full-register.component.css']
})
export class FullRegisterComponent implements OnInit {
  @ViewChild('userInfo') userForm!:UserRegisterComponent;
  @ViewChild('team1') team1!:RegisterTeamComponent;
  @ViewChild('team2') team2!:RegisterTeamComponent;
  @ViewChild('stepper')stepper!:MatStepper;
  constructor(private swal:SweetAlertService,private backend:NetworkService) { }

  ngOnInit(): void {
  }
  submit(){
    if(!this.team2.teamForm.valid){
      this.showTeamError(this.team2)
      return;
    }
    this.makeRequest();
    

  }
  
  goBack(stepper: MatStepper){
    
    stepper.previous();
}

  goForward(stepper: MatStepper){
    const currentIndex=stepper.selectedIndex
    stepper.next();
    if(currentIndex==stepper.selectedIndex){
      if(currentIndex==1){
        this.showTeamError(this.team1)
      }
      else{
        this.showCantAdvanceMessage()
      }
      
    }
  }
  makeRequest(){
    const requestBody:userRegisterInterface=userRegisterRequest(this.userForm,this.team1,this.team2)
    this.backend.post_request_multipart(appSettings.userRegisterRoute,requestBody).subscribe(
      (sucess)=>{this.handleSucess(sucess)},
      (error)=>{this.handleMistake(error)}
    )
  }
  handleSucess(result:any){
    this.swal.showSuccess(alertMessages.successHeader,alertMessages.allowedTeamCreation)
    this.stepper.reset();
  }
  handleMistake(result:any){
    var message=userRegisterMessage(result.error);
    if(message?.header!=undefined && message.body!=undefined){
      this.swal.showError(message?.header,message?.body)
    }

   
    

  }
  showCantAdvanceMessage(){
    this.swal.showError(alertMessages.rejectedTeamTabHeader,alertMessages.rejectedTeamTabBody);
  }
  showTeamError(team:RegisterTeamComponent){
    
    if(team.hasEmptyPilots()){
      this.swal.showError(alertMessages.rejectedTeamTabHeader,alertMessages.emptyPilots)
      return
    }
    if(team.hasEmptyCar()){
      this.swal.showError(alertMessages.rejectedTeamTabHeader,alertMessages.emptyCar)
      return
    }
    if(team.hasRepitedPilots()){
      this.swal.showError(alertMessages.rejectedTeamTabHeader,alertMessages.reapetedPilots)
      return;
    }
    if(team.hasBudgetError()){
      this.swal.showError(alertMessages.rejectedTeamTabHeader,alertMessages.outBoundBudget)
      return;
    }

  }


}
