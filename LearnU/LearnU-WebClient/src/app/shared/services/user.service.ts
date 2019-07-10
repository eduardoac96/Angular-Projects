import { MatSnackBar } from '@angular/material';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService } from './base.service';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { UserDisplay } from '../../models/user-display.model';
import { Paginated } from '../../models/paginated.model';
import { UserMaintenance } from '../../models/user-maintenance.model';
import { map} from 'rxjs/operators';


@Injectable()
export class UserService extends BaseService {
    constructor(
        private http: HttpClient,
        private authService: AuthService,
        private snackBar: MatSnackBar
    ){
        super();
    }


    public getUsers(): Observable<UserDisplay[]> {
        return this.http.get<UserDisplay[]>(`${this.apiUrl}user`);
    }

    public getUsersPaginated(
        sortField: string,
        sortDirection: string,
        maxRecordsPerPage: number,
        pageIndex: number
    ): Observable<Paginated<UserDisplay>>{
        let params = new HttpParams();
        params = params.append('sortField', sortField);
        params = params.append('sortDirection', sortDirection);
        params = params.append('maxRecordsPerPage', String(maxRecordsPerPage));
        params = params.append('pageIndex', String(pageIndex));
    
        return this.http.get<Paginated<UserDisplay>>(`${this.apiUrl}user/paginated`, { params });
    }

    public register(userToRegister: UserMaintenance): Observable<boolean> {
        return this.http.post<UserDisplay>(`${this.apiUrl}user/register`, userToRegister)
        .pipe(map((registeredUser: UserDisplay) => {
          const isValidResponse = registeredUser && registeredUser.userId;
          if (isValidResponse) {
            this.authService.setUser(registeredUser);
          } else {
            this.snackBar.open('Invalid credentials', 'Ok', {
              duration: 4000,
            });
          }
          return !!isValidResponse;
        }));
      }
}