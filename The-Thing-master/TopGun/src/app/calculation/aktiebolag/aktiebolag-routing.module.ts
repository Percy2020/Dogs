import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AktiebolagComponent } from './aktiebolag.component';

const routes: Routes = [{ path: '', component: AktiebolagComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AktiebolagRoutingModule { }
