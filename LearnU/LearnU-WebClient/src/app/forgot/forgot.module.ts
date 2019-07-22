import { CommonModule } from "@angular/common";
import { MatInputModule, MatButtonModule, MatSnackBarModule, MatCheckboxModule } from '@angular/material';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from '../login/login.component';
import { AuthService } from '../shared/services/auth.service';
import { UserService } from '../shared/services/user.service';
import { GlobalErrorHandler } from '../shared/helpers/global-error-handler';
import { HandleHttpErrorInterceptor } from '../shared/helpers/handle-http-error-interceptor';
import { AddAcceptHeaderInterceptor } from '../shared/helpers/add-accept-header-interceptor';
import { AddAuthorizationHeaderInterceptor } from '../shared/helpers/add-authorization-header-interceptor';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ForgotRoutingModule } from './forgot-routing.module';
import { NgModule } from '@angular/core';
import { ForgotComponent } from './forgot.component';

@NgModule({
    imports: [
        CommonModule,
        ForgotRoutingModule,
        MatInputModule,
        MatCheckboxModule,
        FormsModule, 
        HttpClientModule,
        MatButtonModule,
        MatSnackBarModule,
        FlexLayoutModule.withConfig({addFlexToParent: false})
    ],
    declarations: [ForgotComponent],
    providers: [AuthService,UserService,GlobalErrorHandler, {
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
      }],

})

export class ForgotModule{}