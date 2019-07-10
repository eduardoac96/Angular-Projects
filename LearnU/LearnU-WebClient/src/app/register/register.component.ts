import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/services/user.service';
import { Router } from '@angular/router';
import { FormGroup, AbstractControl, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { UserMaintenance } from 'src/app/models/user-maintenance.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup;
  private registerSubscription: Subscription;



  static passwordMatchValidator(control: AbstractControl){
    const password = control.get('password').value;
    const confirmPassword = control.get('confirmPassword').value;
    return password === confirmPassword ? null : { 'passwordMatch': true };
  }


  constructor(
    private router: Router,
    private userService: UserService
  ) { }

  ngOnInit() {
      this.registerForm = new FormGroup({
        firstName: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
        lastName: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
        username: new FormControl(null, [Validators.required, Validators.maxLength(30)]),
        password: new FormControl(null, [Validators.required, Validators.maxLength(30)]),
        confirmPassword: new FormControl(null, [Validators.required])
      }, {
         validators: [RegisterComponent.passwordMatchValidator]
      });
  }

  ngOnDestroy(){
    if(this.registerSubscription){
      this.registerSubscription.unsubscribe();
    }
  }

  public back(){
    this.router.navigateByUrl('/login');
  }

  public save(){
    const userToRegister: UserMaintenance = automapper.map('UserRegistrationForm', 'UserMaintenance', this.registerForm.value);
    this.registerSubscription = this.userService.register(userToRegister).subscribe(response => {
      if (response) {
        this.router.navigate(['/about']);
      }
    });
  }

}
