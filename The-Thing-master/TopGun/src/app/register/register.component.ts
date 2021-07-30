import { Component, OnInit, ViewChild } from '@angular/core';
import { Register } from '../model/register';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { UserService } from '../Service/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private router: Router, private userService: UserService) { }

  public registerData: Register;
  public password2 = '';
  public showErrorMessage: boolean;
  @ViewChild('registerForm', { static: false }) registerForm: NgForm;

  ngOnInit() {

    this.registerData = new Register();

    this.registerData.socialSecurityNumber = '';
    this.registerData.email = '';
    this.registerData.password = '';
    this.registerData.agreeMarketing = false;
    this.registerData.receiveNotificationsByMail = false;

  }

  public registerNewUser(): void {

    this.userService.addUser(this.registerData).subscribe((res) => {


      console.info('--------register------------');
      console.info(res);
      this.router.navigate(['/login']);


    });

  }

  navigate(): void {
    this.router.navigate(['/login']);
  }

  checkSame(secEmail: string) {
    const secondPassword = secEmail;
    const firstPassword = this.registerData.password;
    if (secondPassword === firstPassword) {
      // stuff for form control
      this.showErrorMessage = true;
      this.registerForm.controls.password2.markAsDirty();
      this.registerForm.form.controls.password2.setErrors(null);
    } else {
      // form control with errors
      this.showErrorMessage = false;
      this.registerForm.form.controls.password2.setErrors({ incorrect: true });
      this.registerForm.controls.password2.markAsPristine();

    }
  }




}
