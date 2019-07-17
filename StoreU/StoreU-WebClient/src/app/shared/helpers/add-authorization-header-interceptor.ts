import { Injectable } from "@angular/core";
import { HttpRequest, HttpInterceptor, HttpHandler, HttpEvent } from "@angular/common/http"; 
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AddAuthorizationHeaderInterceptor implements HttpInterceptor {
  // This interceptor will add the bearer to the request header, i.e. an access token so the requests have access to the API
  constructor(private authService: AuthService, private router: Router) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.authService.isAuthenticated()) {
      // Getting the token expiration date
      const expiration = new Date(this.authService.loggedUser.tokenExpiration);
      const now = new Date();

      // Checking if the token is expired
      if (now.getTime() >= expiration.getTime()) {
        this.authService.logout();
        this.router.navigateByUrl('/login');
      } else {
        request = request.clone({
          setHeaders: {
            Authorization: `bearer ${this.authService.loggedUser.token}`
          }
        });
      }
    }
    return next.handle(request);
  }
}
