import { Component, OnInit, Input } from '@angular/core';
import { DialogService } from 'src/app/Service/dialog.service';
import { ConfirmComponent } from 'src/app/dialog/confirm/confirm.component';


@Component({
  selector: 'app-firma',
  templateUrl: './firma.component.html',
  styleUrls: ['./firma.component.css']
})
export class FirmaComponent implements OnInit {

  @Input() index: string;

  constructor(public dialog: DialogService) { }

  public test2: any;

  ngOnInit(): void {



  }

  public ngOnDestroy() {

  }


  test() {

    const ref = this.dialog.open(ConfirmComponent, {
      data: {
        title: 'Påminnelse',
        message: 'Du har varit inaktiv, om 5 minuter loggas du ut!',
        firstButton: 'Fortsätt var inloggad',
        secondButton: 'Logga ut' },
    });

    ref.afterClosed.subscribe(result => {
      console.log('----------------');
      console.log('Dialog closed', result);
    });
  }

}
