import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-league',
  templateUrl: './create-league.component.html',
  styleUrls: ['./create-league.component.css']
})
export class CreateLeagueComponent implements OnInit {

  memberNumber!:number[];
  constructor() { 
    this.startMemberNumber();
  }

  ngOnInit(): void {
    
  }

  startMemberNumber(){
    this.memberNumber=[];
    for(var i=0;i<20;i++){
      this.memberNumber.push(i+1);
    }
  }

}
