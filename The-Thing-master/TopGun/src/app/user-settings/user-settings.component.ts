import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../model/user';
import { UserService } from '../Service/user.service';
import { PhoneNumberService } from '../Service/phone-number.service';


@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.css']
})
export class UserSettingsComponent implements OnInit, AfterViewInit {

  @ViewChild('userForm', { static: false }) userForm: NgForm;
  public user: User = User.empty();
  public originalUser: User = User.empty();
  public successMessage: string;
  public showSuccessful = true;

  constructor(private userService: UserService, private phoneNumberService: PhoneNumberService) { }

  ngOnInit(): void {

    this.userService.getCustomerUserProfile();

    this.userService.currentUser.subscribe(userData => {

      if (userData) {
        this.user = userData;
        this.setOriginalProfileData();
        this.formatAllPhoneNumber();
      }

      console.info('user profile');
      console.info(this.user);

    });

  }

  ngAfterViewInit(): void {
    // setup the listeners changing the visibility of the sign and add buttons.
    // tslint:disable-next-line: no-non-null-assertion
    this.userForm.statusChanges!
      .subscribe({
        next: (result) => {

          console.info('form Changes');
          this.checkIfProfileFormDataChanged();
          console.info('form Changes invalid ' +  this.userForm.invalid);
          console.info('form Changes dirty ' +  this.userForm.dirty);
        }

      });

  }

  public formatAllPhoneNumber() {
    this.user.mobilePhoneNumber = this.phoneNumberService.formatPhoneNumber(this.user.mobilePhoneNumber);

    this.user.phoneNumber = this.phoneNumberService.formatPhoneNumber(this.user.phoneNumber);
  }



  formPhoneNumberValidation(typ: number, phoneNumber: string) {

    switch (typ) {

      case 0:

        this.user.phoneNumber = this.phoneNumberService.formatPhoneNumber(this.user.phoneNumber);

        if (this.phoneNumberService.checkIfPhoneNumberValid(typ, phoneNumber)) {
          setTimeout(() => this.userForm.controls.inputPhone.setErrors({ invalid: true }));

        } else {
          this.userForm.controls.inputPhone.setErrors(null);
        }
        this.userForm.controls.inputPhone.markAsTouched();
        this.userForm.controls.inputPhone.markAsDirty();

        break;

      case 1:

        this.user.mobilePhoneNumber = this.phoneNumberService.formatPhoneNumber(this.user.mobilePhoneNumber);

        if (this.phoneNumberService.checkIfPhoneNumberValid(typ, phoneNumber)) {
          setTimeout(() => this.userForm.controls.inputMobilePhone.setErrors({ invalid: true }));
        } else {
          this.userForm.controls.inputMobilePhone.setErrors(null);
        }
        this.userForm.controls.inputMobilePhone.markAsTouched();
        this.userForm.controls.inputMobilePhone.markAsDirty();

        break;

      default:
        console.log('formPhoneNumberValidation error!');
        break;
    }



  }

  /*
* Makes a copy of the original profile data that it uses to validate if a change has occurred and used in a later function
*/
  setOriginalProfileData() {
    this.originalUser.agreeMarketing = this.user.agreeMarketing;
    this.originalUser.email = this.user.email;
    this.originalUser.mobilePhoneNumber = this.user.mobilePhoneNumber;
    this.originalUser.phoneNumber = this.user.phoneNumber;
    this.originalUser.subscribeToEmailNotification = this.user.subscribeToEmailNotification;
  }

  private checkIfProfileFormDataChanged() {


    if (this.userForm.dirty) {

      // tslint:disable-next-line: forin
      for (const item in this.userForm.controls) {

        const currentControl = this.userForm.controls[item];

        if (item === 'inputPhone' &&
          // tslint:disable-next-line: max-line-length
          this.phoneNumberService.resetFormatPhoneNumber(this.originalUser.phoneNumber) === this.phoneNumberService.resetFormatPhoneNumber(currentControl.value)) {
          this.userForm.controls[item].markAsPristine();
        }

        if (item === 'userCheckbox' && this.originalUser.agreeMarketing === currentControl.value) {
          this.userForm.controls[item].markAsPristine();
        }

        if (item === 'checkbox' && this.originalUser.subscribeToEmailNotification === currentControl.value) {
          this.userForm.controls[item].markAsPristine();
        }

        if (item === 'email' && this.originalUser.email === currentControl.value) {
          this.userForm.controls[item].markAsPristine();
        }

        if (item === 'inputMobilePhone' &&
          // tslint:disable-next-line: max-line-length
          this.phoneNumberService.resetFormatPhoneNumber(this.originalUser.mobilePhoneNumber) === this.phoneNumberService.resetFormatPhoneNumber(currentControl.value)) {
          this.userForm.controls[item].markAsPristine();
        }


      }

    }

  }



  saveUser() {

    this.userService.updateCustomerProfile(this.user).subscribe({
      next: (updatedUser: User) => {
        this.successMessage = 'Kontaktuppgifter har sparats';
        this.showSuccessful = false;
        this.userForm.form.markAsPristine();
        // reload the profile from server. We are alredy listening for changes in user
        setTimeout(() => {
          this.showSuccessful = true;
          this.successMessage = '';
        }, 6000);
      }
    });

  }


  public userAddressEmpty() {

    return !this.user || (!this.user.address.attention &&
      !this.user.address.careOf &&
      !this.user.address.city &&
      !this.user.address.country &&
      !this.user.address.street &&
      !this.user.address.zipCode
    );

  }

}
