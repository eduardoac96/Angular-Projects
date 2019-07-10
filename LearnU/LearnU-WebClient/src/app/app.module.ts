import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { } from 'automapper-ts';
 import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatCheckboxModule, MatRadioModule, MatSnackBarModule, MatProgressSpinnerModule, MatSortModule, MatTableModule, MatSidenavModule, MatFormFieldModule, MatInputModule } from '@angular/material';
  
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module'; 
import { AuthService } from './shared/services/auth.service';
import { UserService } from './shared/services/user.service';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

// AoT requires an exported function for factories
export const createTranslateLoader = (http: HttpClient) => {
  /* for development
  return new TranslateHttpLoader(
      http,
      '/start-javascript/sb-admin-material/master/dist/assets/i18n/',
      '.json'
  );*/
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
};


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatButtonModule,
    MatCheckboxModule,
    MatRadioModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatSortModule,
    MatTableModule,
    MatSidenavModule,
    MatFormFieldModule,
    MatInputModule,
    TranslateModule.forRoot({
      loader: {
          provide: TranslateLoader,
          useFactory: createTranslateLoader,
          deps: [HttpClient]
      }
  })

  ],
  providers: [AuthService, UserService],
  bootstrap: [AppComponent]
})
export class AppModule {

  constructor() {
    automapper.createMap('UserRegistrationForm', 'UserMaintenance');
    automapper.createMap('LoginForm', 'UserLogin');
  }
}
