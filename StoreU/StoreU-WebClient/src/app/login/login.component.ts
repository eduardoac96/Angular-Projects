import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { Subscription } from 'rxjs';
import { NgForm } from '@angular/forms';
import { UserLogin } from '../models/user/user-login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  private loginSubscription: Subscription;
  
  constructor(private router: Router, private authService: AuthService) { }

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
        if(response){
          this.router.navigate(['/dashboard']);
        }
    });
  }


  public register(): void {
    this.router.navigate(['/register']);
  }



}
