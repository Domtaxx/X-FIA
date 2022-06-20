import { Component, OnInit, ViewChild } from '@angular/core';
import { EditProfileInfoComponent } from '../edit-profile-info/edit-profile-info.component';
import { EditProfileTeamComponent } from '../edit-profile-team/edit-profile-team.component';
import { MatStepper } from '@angular/material/stepper';
import { userEditRequest } from 'src/app/request/requestBuilder';
import { profileEditInterface, userInterface, userRegisterInterface } from 'src/app/interface/interfaces';
import { NetworkService } from 'src/app/services/network.service';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { appSettings } from 'src/app/const/appSettings';
import { userRegisterMessage } from 'src/app/errorCodeHandler/errorHandler';
import { alertMessage } from 'src/app/interface/interfaces';
import { alertMessages } from 'src/app/const/messages';
import { userProfileProviderService } from 'src/app/dataProviderServices/userProfileProvider';
import { getData } from 'src/app/functions/browserDataInfo';
import { localStorageNames } from 'src/app/const/localStorageNames';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  @ViewChild('userInfo') userForm!:EditProfileInfoComponent;
  @ViewChild('team1') team1!:EditProfileTeamComponent;
  @ViewChild('team2') team2!:EditProfileTeamComponent;
  @ViewChild('stepper')stepper!:MatStepper;
  constructor(private swal:SweetAlertService,private backend:NetworkService,private dataProvider:userProfileProviderService) {
    dataProvider.getProfileData(getData(localStorageNames.email),
    (user:userInterface)=>{
      
      this.userForm.setInitialData(user.Username,user.TeamsName,appSettings.imageGet+user.TeamsLogo);
      this.team1.setInitial(user.Subteams[0].Pilots,user.Subteams[0].RealTeamsNameNavigation,user.Subteams[0].Name);
      this.team2.setInitial(user.Subteams[1].Pilots,user.Subteams[1].RealTeamsNameNavigation,user.Subteams[1].Name);
    })
   }

  ngOnInit(): void {
  }
  /*
  input:none
  output:none
  description: function called when the userRegister its summited
  */
  submit(){
    if(!this.team2.teamForm.valid){ //verifies if the team2 form fulfull all the validations
      this.showTeamError(this.team2)
      return;
    }
    this.makeRequest();//makes the request to add a user
    

  }
  /*
  input: the mat-stepper of the form stepper
  output: none
  description: tries to go back in the stepper
  */
  goBack(stepper: MatStepper){
    
    stepper.previous();
}
  /*
  input: the mat-stepper of the form stepper
  output: none
  description: tries to go forwar to the next form
  */
  goForward(stepper: MatStepper){
    const currentIndex=stepper.selectedIndex
    stepper.next();
    if(currentIndex==stepper.selectedIndex){//checks if is on the team request
      if(currentIndex==1){
        this.showTeamError(this.team1)//ask to show a error in the form
      }
      else{
        this.showCantAdvanceMessage()
      }
      
    }
  }
  /*
  input: none
  output: none
  description: gets the data and makes the request to the server
  */
  makeRequest(){
    const requestBody:profileEditInterface=userEditRequest(this.userForm,this.team1,this.team2)//get the data to send the request
    console.log('body');
    console.log(requestBody);
    this.backend.post_request_multipart(appSettings.userEditRoute,requestBody).subscribe(
      (sucess)=>{this.handleSucess(sucess)},//sucess case
      (error)=>{this.handleMistake(error)} //error case
    )
  }
  /*
  input:the result of the request
  output:none
  description: handles the sucess case, show the sucess message
  */
  handleSucess(result:any){
    this.swal.showSuccess(alertMessages.successHeader,alertMessages.allowedTeamCreation)
    this.stepper.reset();
    this.team1.resetData()
    this.team2.resetData()
    this.userForm.resetData()
  }
  /*
  input:result of the request
  output:none
  description: handles the mistake case, get the message to 
  the specific error code
  */
  handleMistake(result:any){
    console.log('Error Registro Usuarip')
    console.log(result);

    var message=userRegisterMessage(result.error);
    if(message?.header!=undefined && message.body!=undefined){
      this.swal.showError(message?.header,message?.body)
    }

   
    

  }
  /*
  input: none
  output: none
  description: show the error message when cant go from a step to another
  */
  showCantAdvanceMessage(){
    this.swal.showError(alertMessages.rejectedTeamTabHeader,alertMessages.rejectedTeamTabBody);
  }
  /*
  input: the current team screen
  output: none
  description: shows the error in the team form to advance to the next step
  */
  showTeamError(team:EditProfileTeamComponent){
    
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
