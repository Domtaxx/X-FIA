import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Route, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { isLogged } from '../validations/loginValidation';
import { RouterServiceService } from '../services/router-service.service';
@Injectable({
  providedIn: 'root'
})
export class LoginGuardGuard implements CanActivate, CanActivateChild {
  constructor(private router:RouterServiceService){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const logCondition=isLogged();
      if(!logCondition){
        this.router.redirect('auth/login')
      }
      return logCondition;
      //return true;
  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const logCondition=isLogged();
    if(!logCondition){
      this.router.redirect('auth/login')
    }
    return logCondition;
    //return true;
  }
  
}
