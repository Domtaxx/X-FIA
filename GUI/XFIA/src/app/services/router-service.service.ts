import { Injectable } from '@angular/core';
import { Router,ActivatedRoute } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class RouterServiceService {

  constructor(private router:Router) { }

  redirect(route:String){
    var navigateInfo:String[]=[];
    navigateInfo.push(route);
    this.router.navigate(navigateInfo);
  }
}
