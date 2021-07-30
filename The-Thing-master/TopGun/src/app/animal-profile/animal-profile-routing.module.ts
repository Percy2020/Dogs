import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AnimalProfileComponent } from './animal-profile.component';

const routes: Routes = [{ path: '', component: AnimalProfileComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AnimalProfileRoutingModule { }
