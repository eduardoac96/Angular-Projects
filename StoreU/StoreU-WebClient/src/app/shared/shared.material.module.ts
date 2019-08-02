import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule, MatCheckboxModule, MatProgressSpinnerModule, MatFormFieldModule, MatSnackBarModule, 
       MatInputModule,MatIconModule, MatSlideToggleModule, MatDatepickerModule, MatNativeDateModule,MatButtonToggleModule} from '@angular/material';
import { MatPasswordStrengthModule } from '@angular-material-extensions/password-strength';

@NgModule({
  declarations: [],
  imports: [
    CommonModule, 
    MatButtonModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonToggleModule,
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
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatButtonToggleModule,
    MatSnackBarModule,
    MatInputModule,
    MatPasswordStrengthModule,
    MatSlideToggleModule

  ]
})
export class SharedMaterialModule { }
