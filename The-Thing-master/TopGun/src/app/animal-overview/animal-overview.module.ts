import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AnimalOverviewRoutingModule } from './animal-overview-routing.module';
import { AnimalOverviewComponent } from './animal-overview.component';


@NgModule({
  declarations: [AnimalOverviewComponent],
  imports: [
    CommonModule,
    AnimalOverviewRoutingModule
  ]
})
export class AnimalOverviewModule { }
