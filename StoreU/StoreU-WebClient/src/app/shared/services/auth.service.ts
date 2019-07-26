
import { Subject, Observable, throwError } from 'rxjs';
import { MatSnackBar } from '@angular/material';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { UserDisplay } from 'src/app/models/user/user-display.model';
import { UserLogin } from 'src/app/models/user/user-login.model';
import { UserChangePassword } from 'src/app/models/user/user-changePassword.model';

export class AuthService extends BaseService {

    constructor(
        private http: HttpClient,
        private snackBar: MatSnackBar
    ) {
        super();
    }

    public onAuthenticationChanged = new Subject<boolean>();
    private userKey = 'loggedUser';

    get loggedUser(): UserDisplay {
        return this.isAuthenticated() ? JSON.parse(localStorage[this.userKey]) : null;

    }


    public isAuthenticated(): boolean {
        const currentStorage = localStorage[this.userKey];

        if (currentStorage) {
            const parsedUser: UserDisplay = JSON.parse(currentStorage);
            return !!(parsedUser && parsedUser.userId);
        }

        return false;
    }

    public setUser(user: UserDisplay): void {
        if (!user.antiqueSince) {
            const now = new Date();
            now.setSeconds(now.getSeconds() + 30);
            user.antiqueSince = now.toISOString();
        }
        localStorage[this.userKey] = JSON.stringify(user);

        this.onAuthenticationChanged.next(this.isAuthenticated());
    }


    public logout(): void {
        localStorage.removeItem(this.userKey);

        this.onAuthenticationChanged.next(this.isAuthenticated());
    }

    public login(userToLogin: UserLogin): Observable<boolean> {
        let params = new HttpParams();
        params = params.append('username', userToLogin.username);
        params = params.append('password', userToLogin.password);

        return this.http.get<UserDisplay>(`${this.apiUrl}user/authenticate`, { params })
            .pipe(map((loggedUser: UserDisplay) => {
                const isValidResponse = loggedUser && loggedUser.userId;
                if (isValidResponse) {
                    this.setUser(loggedUser);
                } else {
                    this.snackBar.open('Invalid credentials', 'Ok', {
                        duration: 4000,
                    });
                }
                return !!isValidResponse;
            })


            );
    }

    public generateCode(email: string): Observable<string> {
        let params = new HttpParams();
        params = params.append('email', email);

        return this.http.get<string>(`${this.apiUrl}user/generateCode`, { params })
            .pipe(map((message: string) => {
                return message;
            })
            );
    }
  
    public setPassword(model: UserChangePassword): Observable<string> {
 
        if(model.userToken == undefined || model.userToken == null || model.userToken === '')
        {
            this.snackBar.open('The user token is invalid', 'OK', {
                duration: 4000,
            });

            return;
        }


 
        return this.http.post(`${this.apiUrl}user/setPassword`, model)
            .pipe(map((message: string) => {
                return message;
            })
            );
    }


}