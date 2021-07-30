import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogComponent } from './dialog.component';
import { InsertionDirective } from './insertion.directive';
import { ExampleComponent } from './example/example.component';
import { ConfirmComponent } from './confirm/confirm.component';



@NgModule({
  declarations: [DialogComponent, InsertionDirective, ExampleComponent, ConfirmComponent],
  entryComponents: [DialogComponent],
  imports: [
    CommonModule
  ]
})
export class DialogModule { }
