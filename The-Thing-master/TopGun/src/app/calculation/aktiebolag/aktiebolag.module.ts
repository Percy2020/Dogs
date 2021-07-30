import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AktiebolagRoutingModule } from './aktiebolag-routing.module';
import { AktiebolagComponent } from './aktiebolag.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    AktiebolagComponent,
  ],
  imports: [
    CommonModule,
    AktiebolagRoutingModule,
    FormsModule
  ]
})
export class AktiebolagModule { }
