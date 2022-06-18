import { Component, OnInit } from '@angular/core';
import { pilotInterface } from 'src/app/interface/interfaces';
import { carInterface } from 'src/app/interface/interfaces';
import { userInterface } from 'src/app/interface/interfaces';
import { userProfileProviderService } from 'src/app/dataProviderServices/userProfileProvider';
import { appSettings } from 'src/app/const/appSettings';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-view-profile',
  templateUrl: './view-profile.component.html',
  styleUrls: ['./view-profile.component.css']
})
export class ViewProfileComponent implements OnInit {
  username!:string;
  teamName!:string;
  teamLogo!:string;
  subteam1Name!:string;
  subteam2Name!:string;
  pilots1!:pilotInterface[];
  pilots2!:pilotInterface[];
  car1!:carInterface;
  car2!:carInterface;
  imagePath=appSettings.imageGet;
  email:string;
  constructor(private DataManagement:userProfileProviderService,private route:ActivatedRoute) { 
    this.route.params.subscribe(params=>{this.email=params['email']})
  }

  ngOnInit(): void {
    this.getData();
  }
  getData(){
    this.defaultData();
    this.DataManagement.getProfileData(this.email,(user:userInterface)=>{
      this.updateData(user);
    })
  }
  updateData(user:userInterface){
    if(user==null)return
    this.username=user.Username;
    this.teamName=user.TeamsName;
    this.teamLogo=user.TeamsLogo;
    this.subteam1Name=user.Subteams[0].Name;
    this.subteam2Name=user.Subteams[1].Name;
    this.pilots1=user.Subteams[0].Pilots;
    this.pilots2=user.Subteams[1].Pilots;
    this.car1=user.Subteams[0].RealTeamsNameNavigation;
    this.car2=user.Subteams[1].RealTeamsNameNavigation;
    console.log(user.TeamsLogo)
  }

  defaultData(){
    this.username="Username";
    this.teamName="Equipo";
    this.teamLogo="https://thumbs.dreamstime.com/b/grupo-colorido-team-logo-design-symbol-illustration-de-la-gente-124513112.jpg"
    this.subteam1Name='Equipo 1';
    this.subteam2Name="Equipo 2";
    this.pilots1=[];
    this.pilots2=[];
    
  }

}
