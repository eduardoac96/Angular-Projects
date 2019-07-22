import { NgModule } from "@angular/core";
import { MatInputModule, MatCheckboxModule, MatSelectModule, MatButtonModule, MatNativeDateModule, MatDatepickerModule, MatSnackBarModule, MatProgressSpinnerModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RegisterComponent } from './register.component';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { RegisterRoutingModule } from './register-routing.module';
import { AuthService } from '../shared/services/auth.service';
import { UserService } from '../shared/services/user.service';
import { Glob } from 'glob';
import { GlobalErrorHandler } from '../shared/helpers/global-error-handler';
import { AddAuthorizationHeaderInterceptor } from '../shared/helpers/add-authorization-header-interceptor';
import { AddAcceptHeaderInterceptor } from '../shared/helpers/add-accept-header-interceptor';
import { HandleHttpErrorInterceptor } from '../shared/helpers/handle-http-error-interceptor';

@NgModule({
    imports: [
        CommonModule,
        RegisterRoutingModule,
        MatInputModule,
        MatCheckboxModule,
        MatSelectModule,
        MatSnackBarModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        MatProgressSpinnerModule,
        MatButtonModule,
        MatNativeDateModule,
        MatDatepickerModule,
        FlexLayoutModule.withConfig({ addFlexToParent: false })

    ],
    declarations: [RegisterComponent],
    providers: [AuthService, UserService, GlobalErrorHandler,

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

})

export class RegisterModule { }