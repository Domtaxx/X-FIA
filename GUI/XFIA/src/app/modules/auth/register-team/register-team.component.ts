import { Component, OnInit } from '@angular/core';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';
import { FormGroup,FormControl, Validators } from '@angular/forms';
import { carInterface, pilotInterface } from 'src/app/interface/interfaces';
import { NetworkService } from 'src/app/services/network.service';
import { appSettings } from 'src/app/const/appSettings';
import { ValueConverter } from '@angular/compiler/src/render3/view/template';
import { budgedCalc } from 'src/app/calc/budgetCalc';
import { appColor } from 'src/app/const/appSettings';
import { pilotsDoesntMatch } from 'src/app/validations/customFieldValidations';
import{overBudget} from 'src/app/validations/customFieldValidations'
import { totalBudget } from 'src/app/interface/interfaces';
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
  teamForm= new FormGroup({
    teamName:new FormControl('Equipo',[Validators.required]),
    pilot1:new FormControl('',[Validators.required]),
    pilot2:new FormControl('',[Validators.required]),
    pilot3:new FormControl('',[Validators.required]),
    pilot4:new FormControl('',[Validators.required]),
    pilot5:new FormControl('',[Validators.required]),
    car: new FormControl('',Validators.required)

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

  ngOnInit(): void {
    this.cost.budget=0
    this.cost.leftBudget=0;
    this.getBudget();
    this.getCars();
    this.getRunners();

  }
  indexUp(){
    this.memberIndex++;
    if(this.memberIndex>=this.memberArray.length){
      this.memberIndex=0;
    }
    this.updateMetadata()
  }
  indexDown(){
    this.memberIndex--;
    if(this.memberIndex<0){
      this.memberIndex=this.memberArray.length-1;
    }
    this.updateMetadata()
  }
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

  getRunners(){
    this.backend.get_request(appSettings.everyPilotRoute,{}).subscribe(
      (sucess:pilotInterface[])=>{
        this.availablePilots=sucess;
        console.log(sucess)
        this.availablePilots=[...this.availablePilots]
      }
    )
  }
  getCars(){
    this.backend.get_request(appSettings.carsRoute,{}).subscribe(
      (sucess:carInterface[])=>{
        this.availableCars=sucess;
        this.availableCars=[...this.availableCars]

      }
    )
  }
  getBudget(){
    this.backend.get_request(appSettings.currentBudgetRoute,{}).subscribe(
      (sucess)=>{
        this.cost.budget=sucess;
        this.cost.leftBudget=sucess;
        
      }
    )

  }
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
  updateBudget(){
   var currentValue=budgedCalc.calculateTeamCost(this.selectedPilots,this.selectedCar);
   var leftBudget=this.cost?.budget-currentValue;
   this.outBudget=leftBudget<0;
   this.cost.leftBudget=leftBudget;
  }

}
