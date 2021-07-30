import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EnskildfirmaComponent } from './enskildfirma.component';

const routes: Routes = [{ path: '', component: EnskildfirmaComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EnskildfirmaRoutingModule { }
