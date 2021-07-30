import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MainViewComponent } from './main-view.component';
import { AuthGuardService } from '../Service/auth-guard.service';

const routes: Routes = [
   { path: '', component: MainViewComponent,
   children: [
     // tslint:disable-next-line: max-line-length
     { path: 'aktiebolag', loadChildren: () => import('../calculation/aktiebolag/aktiebolag.module').then(m => m.AktiebolagModule), canActivate: [AuthGuardService] },
     // tslint:disable-next-line: max-line-length
     { path: 'enskildfirma', loadChildren: () => import('../calculation/enskildfirma/enskildfirma.module').then(m => m.EnskildfirmaModule), canActivate: [AuthGuardService] },
     // tslint:disable-next-line: max-line-length
     { path: 'userSettings', loadChildren: () => import('../user-settings/user-settings.module').then(m => m.UserSettingsModule), canActivate: [AuthGuardService] },
     // tslint:disable-next-line: max-line-length
     { path: 'animalOverview', loadChildren: () => import('../animal-overview/animal-overview.module').then(m => m.AnimalOverviewModule), canActivate: [AuthGuardService] },
     // tslint:disable-next-line: max-line-length
     { path: 'animalProfile', loadChildren: () => import('../animal-profile/animal-profile.module').then(m => m.AnimalProfileModule), canActivate: [AuthGuardService] },
   ] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainViewRoutingModule { }
