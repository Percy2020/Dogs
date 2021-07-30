import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainViewRoutingModule } from './main-view-routing.module';
import { MainViewComponent } from './main-view.component';
import { MenuModule } from '../menu/menu.module';


@NgModule({
  declarations: [MainViewComponent],
  entryComponents: [MainViewComponent],
  exports: [MainViewComponent],
  imports: [
    CommonModule,
    MainViewRoutingModule,
    MenuModule
  ]
})
export class MainViewModule { }
