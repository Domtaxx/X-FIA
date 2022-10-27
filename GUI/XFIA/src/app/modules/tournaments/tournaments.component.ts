import { Component, OnInit } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { FormControl,FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { saveData,deleteKey, getData } from 'src/app/functions/browserDataInfo';
import { localStorageNames } from 'src/app/const/localStorageNames';
@Component({
  selector: 'app-tournaments',
  templateUrl: './tournaments.component.html',
  styleUrls: ['./tournaments.component.css']
})
export class TournamentsComponent implements OnInit {


  loggedEmail:string;



  
  constructor() { 
    this.loggedEmail=getData(localStorageNames.email);
  }

  ngOnInit(): void {
  }
 
  logOut(){
    deleteKey(localStorageNames.email)
    
  }
 

}
