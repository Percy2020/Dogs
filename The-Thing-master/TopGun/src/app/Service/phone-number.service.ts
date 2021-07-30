import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PhoneNumberService {

  constructor() { }

  resetFormatPhoneNumber(tempPhoneNumber: string): string {
    if (tempPhoneNumber) {
      tempPhoneNumber = tempPhoneNumber.replace(/\s/g, '');
      tempPhoneNumber = tempPhoneNumber.replace(/-/g, '');
    }
    return tempPhoneNumber;
  }

  formatPhoneNumber(phoneNumber: string) {
    if (phoneNumber.length > 4) {
      phoneNumber = this.resetFormatPhoneNumber(phoneNumber);
      phoneNumber = this.areaCode(phoneNumber);
      phoneNumber = this.addWhiteSpace(phoneNumber);
    }

    return  phoneNumber;

  }

  checkIfPhoneNumberValid(typ: number, phoneNumber: string): boolean {
    phoneNumber = this.resetFormatPhoneNumber(phoneNumber);
    if (phoneNumber.length > 4) {
    if (typ === 0) {
      return this.phoneNumberRegeX(phoneNumber);
    } else {
      return  this.mobilePhoneNumberRegeX(phoneNumber);
    }
  }else{

    return false;
  }

  }

  phoneNumberRegeX(tempPhoneNumber: string): boolean {
    const regexp = /^(([+]\d{2}[1-9]\d{0,2})|([0]\d{1,3}))((\d{2}(\d{2}){2})|(\d{3}(\d{3})*(\d{2})+))$/;
    if (!regexp.test(tempPhoneNumber) || tempPhoneNumber.length > 11) {
      return true;
    } else {
      return false;
    }
  }

  mobilePhoneNumberRegeX(tempPhoneNumber: string): boolean {
    const regexp = /^(([+]46)\s*(7)|07)[02369]\s*(\d{4})\s*(\d{3})$/;
    if (!regexp.test(tempPhoneNumber)) {
      return true;
    } else {
      return false;
    }
  }


  public addWhiteSpace(tempPhoneNumber: string): string {

    let tempPhone: string = '';
    if (tempPhoneNumber) {

      if (tempPhoneNumber.indexOf('-') !== -1) {

        tempPhone = tempPhoneNumber.substring(tempPhoneNumber.indexOf('-') + 1, tempPhoneNumber.length);

        switch (tempPhone.length) {
          case 5:
            tempPhone = this.insert(tempPhone, 3, ' ');
            break;
          case 6:
            tempPhone = this.insert(tempPhone, 2, ' ');
            tempPhone = this.insert(tempPhone, 5, ' ');
            break;
          case 7:
            tempPhone = this.insert(tempPhone, 3, ' ');
            tempPhone = this.insert(tempPhone, 6, ' ');
            break;
          case 8:
            tempPhone = this.insert(tempPhone, 3, ' ');
            tempPhone = this.insert(tempPhone, 7, ' ');
            break;
          case 9:
            tempPhone = this.insert(tempPhone, 3, ' ');
            tempPhone = this.insert(tempPhone, 7, ' ');
            break;
          default:
            break;
        }
        tempPhoneNumber = this.insert(tempPhoneNumber, tempPhoneNumber.indexOf('-') + 1, tempPhone, true);

      }

    }
    return tempPhoneNumber;
  }

  public areaCode(tempPhoneNumber: string): string {

    if (tempPhoneNumber) {
      // tslint:disable-next-line: max-line-length
      const areaCodes: string[] = ['08', '010', '011', '013', '016', '018', '019', '020', '021', '023', '026', '031', '033', '035', '036', '040', '042', '044', '046', '060', '063', '070', '071', '072', '073', '076', '077', '078', '079', '090', '099'];
      let areaCodePhoneNumber: string = '';

      if (tempPhoneNumber) {
        areaCodePhoneNumber = tempPhoneNumber.substring(0, 3);
      }

      for (const areaCode of areaCodes) {
        if (areaCodePhoneNumber === areaCode) {
          tempPhoneNumber = this.insert(tempPhoneNumber, areaCode.length, '-');
        }

        if (areaCodePhoneNumber.substring(0, 2) === areaCode) {
          tempPhoneNumber = this.insert(tempPhoneNumber, areaCode.length, '-');
        }
      }

      if (tempPhoneNumber.includes('-') === false) {
        tempPhoneNumber = this.insert(tempPhoneNumber, 4, '-');
      }
    }
    return tempPhoneNumber;
  }

  public insert(str: string, index: number, value: string, deleteEverythingAfterAddText: boolean = false) {
    if (deleteEverythingAfterAddText) {
      return str.substr(0, index) + value;
    } else {
      return str.substr(0, index) + value + str.substr(index);
    }
  }

}
