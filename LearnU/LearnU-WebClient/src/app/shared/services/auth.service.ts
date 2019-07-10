import { BaseService } from './base.service';
import { Subject, Observable } from 'rxjs';
import { UserDisplay } from '../../models/user-display.model';
 import { MatSnackBar } from '@angular/material';
import { UserLogin } from '../../models/user-login.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';

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

        debugger
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
            }));
    }

}