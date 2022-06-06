import { Component, OnInit } from '@angular/core';
import { pilotInterface } from 'src/app/interface/interfaces';
import { carInterface } from 'src/app/interface/interfaces';
@Component({
  selector: 'app-view-profile',
  templateUrl: './view-profile.component.html',
  styleUrls: ['./view-profile.component.css']
})
export class ViewProfileComponent implements OnInit {
  pilots1!:pilotInterface[];
  pilots2!:pilotInterface[];
  car1!:carInterface;
  car2!:carInterface;
  constructor() { }

  ngOnInit(): void {
  }

}
