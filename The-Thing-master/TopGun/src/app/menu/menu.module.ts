import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MenuRoutingModule } from './menu-routing.module';
import { MenuComponent } from './menu.component';
import { LoginModule } from '../login/login.module';



@NgModule({
  declarations: [
    MenuComponent
  ],
  entryComponents: [MenuComponent],
  exports: [MenuComponent],
  imports: [
    CommonModule,
    MenuRoutingModule,
    LoginModule
  ]
})
export class MenuModule { }
