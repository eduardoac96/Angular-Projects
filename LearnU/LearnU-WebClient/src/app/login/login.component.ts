import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router'; 
import { NgForm } from "@angular/forms";
import { AuthService } from '../shared/services/auth.service';
import { UserLogin } from '../models/user-login.model';
 

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {

  private loginSubscription: Subscription;

  constructor(private router: Router,
    private authService: AuthService) { }

  ngOnInit() {
  }

  ngOnDestroy() {
    if (this.loginSubscription) {
      this.loginSubscription.unsubscribe();
    }
  }

  public login(loginForm: NgForm): void {
    if (!loginForm.valid) {
      return;
    }

    const userToLogin: UserLogin = automapper.map('LoginForm', 'UserLogin', loginForm.value);
    this.loginSubscription = this.authService.login(userToLogin).subscribe(response => {
      if (response) {
        this.router.navigate(['/dashboard']);
      }
    });
  }

  public register(): void {
    this.router.navigate(['/register']);
  }

}
