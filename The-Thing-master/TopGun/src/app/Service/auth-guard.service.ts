import { Injectable } from '@angular/core';

import { AuthenticationService } from './authentication.service';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {

  constructor(private router: Router,
              private authenticationService: AuthenticationService) { }


  canActivate() {
    if (!this.authenticationService.isTokenExpired()) {
      return true;
    }
    this.authenticationService.cleanTokenData();
    this.router.navigate(['/login']);
    return false;
  }

}
