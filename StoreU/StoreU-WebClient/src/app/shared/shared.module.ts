import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { LayoutModule } from '@angular/cdk/layout';
import { SharedMaterialModule } from './shared.material.module'; 
import { LoaderComponent } from '../loader/loader.component';
import { FormsModule } from '@angular/forms';
import { AuthService } from './services/auth.service';
import { FlexLayoutModule } from '@angular/flex-layout';

import { HttpClientModule } from '@angular/common/http';
 @NgModule({
  declarations: [LoaderComponent],
  imports: [
    CommonModule,
    LayoutModule,
    SharedMaterialModule, 
    HttpClientModule,
    FormsModule,
    FlexLayoutModule.withConfig({addFlexToParent: false})
  ],
  exports: [FormsModule],
  providers: [AuthService ]
})
export class SharedModule { }
