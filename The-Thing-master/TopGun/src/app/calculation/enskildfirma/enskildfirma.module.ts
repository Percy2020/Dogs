import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EnskildfirmaRoutingModule } from './enskildfirma-routing.module';
import { EnskildfirmaComponent } from './enskildfirma.component';
import { FirmaComponent } from './firma/firma.component';


@NgModule({
  declarations: [EnskildfirmaComponent, FirmaComponent],
  imports: [
    CommonModule,
    EnskildfirmaRoutingModule
  ]
})
export class EnskildfirmaModule { }
