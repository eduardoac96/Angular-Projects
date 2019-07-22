import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPasswordStrengthComponent } from '@angular-material-extensions/password-strength';
import { Subscription } from 'rxjs';
import { NgForm } from '@angular/forms';
import { UserChangePassword } from 'src/app/models/user/user-changePassword.model';
import { AuthService } from 'src/app/shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-reset',
  templateUrl: './forgot-reset.component.html',
  styleUrls: ['./forgot-reset.component.scss']
})
export class ForgotResetComponent implements OnInit {
  showDetails: boolean;

  @ViewChild('passwordComponentWithConfirmation', {static: true})
  passwordComponentWithConfirmation: MatPasswordStrengthComponent;

  private forgotSubscription: Subscription;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  ngOnDestroy(){
    if(this.forgotSubscription){
      this.forgotSubscription.unsubscribe();
    }
  }

  onStrengthChanged(strength: number) {
    console.log('password strength = ', strength);
  }

  public reset(forgotForm: NgForm): void{
    if(!forgotForm.valid){
      return;
    }

    const model: UserChangePassword  = automapper.map('ForgotForm', 'UserChangePassword', forgotForm.value);

    this.forgotSubscription = this.authService.setPassword(model).subscribe(response => {
        if(response){
          this.router.navigate(['/login']);
        }
    }); 
  }


}
