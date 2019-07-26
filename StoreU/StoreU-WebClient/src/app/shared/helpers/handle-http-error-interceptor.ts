import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators'; 
import { GlobalErrorHandler } from './global-error-handler';
import { Router } from '@angular/router'; 
import { AuthService } from '../services/auth.service';
import { ActionResponse } from 'src/app/models/action-response.model';

@Injectable()
export class HandleHttpErrorInterceptor implements HttpInterceptor {
    constructor(
      private globalErrorHandler: GlobalErrorHandler,
      private router: Router,
      private authService: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

         return next.handle(request).pipe(catchError((error: HttpErrorResponse) => {
            if (error && error.error && error.error.hasOwnProperty("friendlyErrorMessage")) {
                this.globalErrorHandler.handleFriendlyError(error.error);
            } else if (error && error.error) {
                const errorToLog = `Http error (unsuccessful reponse). Message: ${error.message}, status code: ${(error).status}, body: ${JSON.stringify(error.error)}`;
                this.globalErrorHandler.handleError(errorToLog);
            } else {
                const errorToLog = `Http error (unsuccessful reponse). Message: ${error.message}, status code: ${(error).status}, body: ${JSON.stringify(error)}`;
                this.globalErrorHandler.handleError(errorToLog);
            }

            // Redirecting to login when unauthorized
            if (error.status === 401 || error.status === 403) {
              const friendlyError: ActionResponse = {
                friendlyErrorMessage: 'The current user is not authorized',
                isSuccessful: false
              };
              this.globalErrorHandler.handleFriendlyError(friendlyError);
              if (error.status === 401) {
                this.authService.logout();
                this.router.navigateByUrl('/login');
              }
              return of(new HttpResponse());
            } else {
              return throwError(error.error);
            }
        }));
    }
}
