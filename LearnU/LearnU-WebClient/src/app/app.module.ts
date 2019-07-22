import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { } from 'automapper-ts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module';
import { AuthService } from './shared/services/auth.service';
import { UserService } from './shared/services/user.service';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { MatSnackBarModule, MatProgressSpinnerModule } from '@angular/material';
import { AddAuthorizationHeaderInterceptor } from './shared/helpers/add-authorization-header-interceptor';
import { AddAcceptHeaderInterceptor } from './shared/helpers/add-accept-header-interceptor';
import { HandleHttpErrorInterceptor } from './shared/helpers/handle-http-error-interceptor';
import { ForgotComponent } from './forgot/forgot.component';
import { LoaderComponent } from './loader/loader.component';

// AoT requires an exported function for factories
export const createTranslateLoader = (http: HttpClient) => {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
};


@NgModule({
  declarations: [
    AppComponent,
    LoaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSnackBarModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: createTranslateLoader,
        deps: [HttpClient]
      }
    })

  ],
  providers: [AuthService, UserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AddAuthorizationHeaderInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AddAcceptHeaderInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HandleHttpErrorInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {

  constructor() {
    automapper.createMap('registerForm', 'UserMaintenance');
    automapper.createMap('LoginForm', 'UserLogin');
  }
}
