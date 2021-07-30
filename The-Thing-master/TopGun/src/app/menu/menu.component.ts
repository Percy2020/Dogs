import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../Service/user.service';
import { AuthenticationService } from '../Service/authentication.service';


@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  constructor(private router: Router, private userService: UserService, private authenticationService: AuthenticationService) { }

  public pathItem: string[] = ['/mainView/aktiebolag', '/mainView/enskildfirma', '/mainView/userSettings', '/mainView/animalOverview', '/mainView/animalProfile'];
  public userName: string;
  ngOnInit() {


    this.userName = this.authenticationService.getDataFromToken('displayname');

  }

  navigate(path: string) {
    this.router.navigate([path]);
  }



}
