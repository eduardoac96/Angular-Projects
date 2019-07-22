import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule, MatCheckboxModule, MatProgressSpinnerModule, MatFormFieldModule, MatSnackBarModule, matSnackBarAnimations, MatInputModule,MatIconModule, MatSlideToggleModule} from '@angular/material';
import { MatPasswordStrengthModule } from '@angular-material-extensions/password-strength';

@NgModule({
  declarations: [],
  imports: [
    CommonModule, 
    MatButtonModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatInputModule,
    MatIconModule,
    MatPasswordStrengthModule.forRoot(),
    MatSlideToggleModule
  ],
  exports: [
    MatButtonModule, 
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatInputModule,
    MatPasswordStrengthModule,
    MatSlideToggleModule

  ]
})
export class SharedMaterialModule { }
