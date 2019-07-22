import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { SharedMaterialModule } from '../shared/shared.material.module';
import { ForgotComponent } from './forgot.component';
import { ForgotRoutingModule } from './forgot-routing.module';  
import { ForgotResetComponent } from './forgot-reset/forgot-reset.component';
import { ForgotCodeComponent } from './forgot-code/forgot-code.component';
@NgModule({ 
  imports: [
    CommonModule,
    SharedModule,
    SharedMaterialModule,
    ForgotRoutingModule
  ],
  declarations: [ForgotComponent, ForgotCodeComponent, ForgotResetComponent]
})
export class ForgotModule { }
