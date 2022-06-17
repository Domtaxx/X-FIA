import { Component, OnInit } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';

import { RouterServiceService } from 'src/app/services/router-service.service';
import { LoginService } from 'src/app/dataProviderServices/login.service';
import { SweetAlertService } from 'src/app/services/sweet-alert.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private router:RouterServiceService,private login:LoginService,private swal:SweetAlertService) { }
  loginForm= new UntypedFormGroup({
    email:new UntypedFormControl('',[Validators.required,Validators.email]),
    password:new UntypedFormControl('',[Validators.required,Validators.pattern('^(([a-zA-Z]*[0-9]*)*[a-zA-Z]+([a-zA-Z]*[0-9]*)*[0-9]+([a-zA-Z]*[0-9]*)*)*(([a-zA-Z]*[0-9]*)*[0-9]+([a-zA-Z]*[0-9]*)*[a-zA-Z]+([a-zA-Z]*[0-9]*)*)*'),Validators.minLength(8),Validators.maxLength(8)])

  }
  )

  ngOnInit(): void {
  }
  log(){
    if(!this.loginForm.valid)return;
    const email=this.loginForm.controls['email'].value;
    const password=this.loginForm.controls['password'].value;
    this.login.log(email,password,
      ()=>{this.logged()},
      ()=>{this.notLogged()}
      )
    

  }
  logged(){
    this.router.redirect('publicLeague/ranking');
    const message=this.login.successMesagge();
    this.swal.showSuccess(message.header,message.body);

  }
  notLogged(){
    this.loginForm.reset()
    const message=this.login.errorMesagge();
    this.swal.showError(message.header,message.body);

  }

}
