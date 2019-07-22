import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs";

export class AddAcceptHeaderInterceptor implements HttpInterceptor {
    // HTTP Interceptors allow us to intercept an ongoing request and do something with it or its response
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (!request.headers.has('Accept')) {
            request = request.clone({ headers: request.headers.set('Accept', 'application/json') });
        }
        // Allows the next interceptor to be executed
        return next.handle(request);
    }
}
