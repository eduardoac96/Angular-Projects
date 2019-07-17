import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule, MatCheckboxModule, MatProgressSpinnerModule, MatFormFieldModule, MatSnackBarModule, matSnackBarAnimations, MatInputModule} from '@angular/material';

@NgModule({
  declarations: [],
  imports: [
    CommonModule, 
    MatButtonModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatInputModule
  ],
  exports: [
    MatButtonModule, 
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatInputModule
  ]
})
export class SharedMaterialModule { }
