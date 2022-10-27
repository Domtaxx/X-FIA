import { Component } from '@angular/core';
import { RouterServiceService } from './services/router-service.service';
import { deleteKey } from './functions/browserDataInfo';
import { localStorageNames } from './const/localStorageNames';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'XFIA';
  constructor(private router:RouterServiceService){
    //deleteKey(localStorageNames.email)
  }
  
  redirectMain(){
    this.router.redirect('tournaments')
  }
}
