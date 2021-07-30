import { Component, OnInit } from '@angular/core';
import { DialogConfig } from '../dialog-config';
import { DialogRef } from '../dialog-ref';

@Component({
  selector: 'app-example',
  templateUrl: './example.component.html',
  styleUrls: ['./example.component.css']
})
export class ExampleComponent implements OnInit {

  constructor(public config: DialogConfig, public dialog: DialogRef) {}

  ngOnInit(): void {
  }

  onClose() {
    this.dialog.close('some value')
  }

}
