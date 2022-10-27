import { Component, OnInit } from '@angular/core';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { UntypedFormGroup,UntypedFormControl, Validators } from '@angular/forms';
import { carInterface, pilotInterface } from 'src/app/interface/interfaces';
import { NetworkService } from 'src/app/services/network.service';
import { appSettings } from 'src/app/const/appSettings';

import { budgedCalc } from 'src/app/calc/budgetCalc';
import { appColor } from 'src/app/const/appSettings';
import { pilotsDoesntMatch } from 'src/app/validations/customFieldValidations';
import{overBudget} from 'src/app/validations/customFieldValidations'
import { totalBudget } from 'src/app/interface/interfaces';
import { MatPaginatorDefaultOptions } from '@angular/material/paginator';
@Component({
  selector: 'app-edit-profile-team',
  templateUrl: './edit-profile-team.component.html',
  styleUrls: ['./edit-profile-team.component.css']
})
export class EditProfileTeamComponent implements OnInit {

  memberIndex:number;
  memberArray:string[];
  inputHintArray:string[];
  selectedPilots:Map<number,pilotInterface>;
  selectedCar?:carInterface;
  availablePilots:pilotInterface[];
  availableCars:carInterface[];
  currentImage?:string;
  currentName?:string;
  currentPrice?:number;
  currentCountryImage?:string;
  currentTeamImage?:string;
  outBudget:boolean;
  cost:totalBudget={
    budget:0,
    leftBudget:0
  };
  pilot1Index=0;
  pilot2Index=1;
  pilot3Index=2;
  pilot4Index=3;
  pilot5Index=4;
  carIndex=5;
  imagePath=appSettings.imageGet;
  teamForm= new UntypedFormGroup({
    teamName:new UntypedFormControl('Equipo',[Validators.required]),
    pilot1:new UntypedFormControl('',[Validators.required]),
    pilot2:new UntypedFormControl('',[Validators.required]),
    pilot3:new UntypedFormControl('',[Validators.required]),
    pilot4:new UntypedFormControl('',[Validators.required]),
    pilot5:new UntypedFormControl('',[Validators.required]),
    car: new UntypedFormControl('',Validators.required)

  },
  {validators:[pilotsDoesntMatch(5),overBudget(this.cost)]}
  )
  
  
  constructor(private swal:SweetAlertService,private backend:NetworkService) { 
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
    this.availableCars=[];
    this.currentImage=appSettings.defaultPilotPhotoRoute;
    this.currentCountryImage=appSettings.defaultCountryPhoto;
    this.currentTeamImage=appSettings.defaultTeamPhoto;
    
    
    this.currentName=appSettings.defaultPilotName;
    this.currentPrice=0;
    this.outBudget=false;
    
    

  }
    /*
    input:none
    output:none
    description: function called when the view starts, ask the network module to get data required to show
    */
  ngOnInit(): void {
    //this.resetData()
    
  }
  /*
  input:none
  ouput:none
  description: makes the shift right between the team members
  */
  indexUp(){
    this.memberIndex++;
    if(this.memberIndex>=this.memberArray.length){
      this.memberIndex=0;
    }
    this.updateMetadata()
  }
  /*
  input:none
  output:none
  description: makes the shift left beetween the team members
  */
  indexDown(){
    this.memberIndex--;
    if(this.memberIndex<0){
      this.memberIndex=this.memberArray.length-1;
    }
    this.updateMetadata()
  }
  /*
  input:none
  output:none
  description: update the information(name, price, photo) of the current member
  */
  updateMetadata(){
    this.currentImage=appSettings.defaultPilotPhotoRoute;
    this.currentCountryImage=appSettings.defaultCountryPhoto;
    this.currentTeamImage=appSettings.defaultTeamPhoto;
    this.currentPrice=0;
    if(this.memberIndex<this.carIndex){
      this.currentName=appSettings.defaultPilotName;
      if(this.selectedPilots.get(this.memberIndex)!=undefined){
        var currentPilot=this.selectedPilots.get(this.memberIndex);
        this.currentImage= currentPilot?.Photo;
        this.currentName=currentPilot?.Firstname+ " "+ currentPilot?.Lastname;
        this.currentPrice=currentPilot?.Price;
        this.currentCountryImage=currentPilot?.CountryNameNavigation.Photo;
        this.currentTeamImage=currentPilot?.RealTeamsNameNavigation.Logo
        console.log(this.currentCountryImage);
        console.log(this.currentTeamImage)
      }
    }
    else{
      this.currentName=appSettings.defaultCarName;
      if(this.selectedCar!=undefined){
        this.currentImage=this.selectedCar.Photo;
        this.currentName=this.selectedCar.Name;
        this.currentPrice=this.selectedCar.Price;
      }
    }
    
    
  }
  /*
  input:none
  output:none
  description:ask the network module to get the data of the available piltos
  */
  getRunners(){
    this.backend.get_request(appSettings.everyPilotRoute,{}).subscribe(
      (sucess:pilotInterface[])=>{
        this.availablePilots=sucess;
        console.log(sucess)
        this.availablePilots=[...this.availablePilots]
      }
    )
  }
   /*
  input:none
  output:none
  description:ask the network module to get the data of the available cars
  */
  getCars(){
    this.backend.get_request(appSettings.carsRoute,{}).subscribe(
      (sucess:carInterface[])=>{
        this.availableCars=sucess;
        this.availableCars=[...this.availableCars]

      }
    )
  }
   /*
  input:none
  output:none
  description:ask the network module to get the data of budget of the tournament
  */
  getBudget(){
    this.backend.get_request(appSettings.currentBudgetRoute,{}).subscribe(
      (sucess)=>{
        this.cost.budget=sucess;
        this.cost.leftBudget=sucess;
        this.updateBudget();
        
      }
    )

  }
  /*
  input: value of the input gotten
  output: none
  description: get the value when the member input changes, and call the method to update the metadata
  */
  onChangedInput(value:any){
   if(this.memberIndex<this.memberArray.length-1){
     
      var chosenPilot=this.availablePilots.filter((item)=> item.Id==value)[0]
      this.selectedPilots.set(this.memberIndex,chosenPilot)
   }
   else{
     this.selectedCar=this.availableCars.filter(item=>item.Name==value)[0]
   }
   
   this.updateMetadata();
   this.updateBudget()
   
  }
  /*
  input:none
  output:none
  description: updates the budget given the current team members
  */
  updateBudget(){
   var currentValue=budgedCalc.calculateTeamCost(this.selectedPilots,this.selectedCar);
   var leftBudget=this.cost?.budget-currentValue;
   console.log('budget')
   console.log(this.cost.budget)
   console.log(leftBudget)
   this.outBudget=leftBudget<0;
   this.cost.leftBudget=leftBudget;
  }
  /*
  input:none
  output: boolean 
  description: verifies if the form has the overBudget error
  */
  hasBudgetError():boolean{
   return this.teamForm.hasError('overBudget')
    
  }
  /*
  input:none
  output: boolean 
  description: verifies if the form has the hasRepitedPilots error
  */
  hasRepitedPilots():boolean{
    return this.teamForm.hasError('repitedPilot')
  }
  /*
  input:none
  output: boolean 
  description: verifies if the form has the hasEmptyPilots error
  */
  hasEmptyPilots():boolean{
    for(var i=1;i<=5;i++){
      var errorRequired=this.teamForm.get('pilot'+i)?.hasError('required');
      if(errorRequired){
        return true
      }
    }
    return false;
  }
   /*
  input:none
  output: boolean 
  description: verifies if the form has the hasEmptyCar error
  */
  hasEmptyCar():boolean{
    return this.teamForm.controls['car'].hasError('required')
  }

  resetData(){
    this.memberIndex=0;
    this.getBudget()
    this.getCars()
    this.getRunners()
    this.updateMetadata();
    this.updateBudget();
  }
  setInitial(pilots:pilotInterface[],car:carInterface,teamName:string){
    this.selectedPilots=new Map();
    for(var i=0;i<pilots.length;i++){
      this.selectedPilots.set(i,pilots[i])
      this.teamForm.controls['pilot'+(i+1)].setValue(pilots[i].Id)
    }
    this.selectedCar=car;
    this.teamForm.controls['car'].setValue(car.Name);
    this.teamForm.controls['teamName'].setValue(teamName)
    this.resetData()
    console.log('Pilotos Seleccionados')
    console.log(this.selectedPilots)
    console.log('Selected Car')
    console.log(this.selectedCar)

  }

}
