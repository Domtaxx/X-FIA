import { Component } from '@angular/core';
import { RouterServiceService } from './services/router-service.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'XFIA';
  constructor(private router:RouterServiceService){

  }
  
  redirectMain(){
    this.router.redirect('tournaments')
  }
}
