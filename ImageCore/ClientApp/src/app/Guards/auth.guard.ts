import {Inject, Injectable} from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  private token: string
  private url: string;

  constructor(@Inject("TOKEN") token: string,@Inject("REDIRECTION_URL") redirectUrl: string) {
    this.token = token;
    this.url = redirectUrl;
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean  {

    if(this.token === "NotValid")
    {
      window.location.href = this.url;
      return false;
    }
    else {
      return true;
    }
  }

}
