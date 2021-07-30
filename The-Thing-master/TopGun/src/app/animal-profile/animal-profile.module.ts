import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AnimalProfileRoutingModule } from './animal-profile-routing.module';
import { AnimalProfileComponent } from './animal-profile.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [AnimalProfileComponent],
  imports: [
    CommonModule,
    AnimalProfileRoutingModule,
    FormsModule
  ]
})
export class AnimalProfileModule { }
