import { NgModule } from '@angular/core';
import { } from 'automapper-ts';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component'; 
import { SharedModule } from './shared/shared.module'; 
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { MatPasswordStrengthModule } from '@angular-material-extensions/password-strength';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [ 
    AppRoutingModule,
    SharedModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatPasswordStrengthModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { 
  constructor() {
    automapper.createMap('registerForm', 'UserMaintenance');
    automapper.createMap('LoginForm', 'UserLogin');
  }

}
