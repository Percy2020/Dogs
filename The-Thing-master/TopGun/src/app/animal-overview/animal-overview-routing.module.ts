import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AnimalOverviewComponent } from './animal-overview.component';

const routes: Routes = [{ path: '', component: AnimalOverviewComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AnimalOverviewRoutingModule { }
