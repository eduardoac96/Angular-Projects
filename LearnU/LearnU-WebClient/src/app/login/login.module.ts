import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule, MatCheckboxModule, MatInputModule,MatSnackBarModule, MatProgressSpinnerModule } from '@angular/material';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { UserService } from '../shared/services/user.service';
import { AuthService } from '../shared/services/auth.service';
import { AddAuthorizationHeaderInterceptor } from '../shared/helpers/add-authorization-header-interceptor';
import { HandleHttpErrorInterceptor } from '../shared/helpers/handle-http-error-interceptor';
import { AddAcceptHeaderInterceptor } from '../shared/helpers/add-accept-header-interceptor';
import { GlobalErrorHandler } from '../shared/helpers/global-error-handler';
import { LoaderService } from '../shared/services/loader.service';
import { LoaderInterceptor } from '../shared/helpers/loader.interceptor'; 

@NgModule({
    imports: [
        CommonModule,
        LoginRoutingModule,
        MatInputModule,
        MatCheckboxModule,
        FormsModule,
        HttpClientModule,
        MatButtonModule,
        MatSnackBarModule,
        MatProgressSpinnerModule,
        FlexLayoutModule.withConfig({addFlexToParent: false})
    ],
    declarations: [LoginComponent],
      providers: [AuthService, UserService, GlobalErrorHandler, LoaderService, {
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
      },
      { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true }

    
    ],

})
export class LoginModule {}
