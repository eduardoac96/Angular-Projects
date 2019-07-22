import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/services/user.service';
import { Router } from '@angular/router';
import { AbstractControl, NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { UserMaintenance } from 'src/app/models/user-maintenance.model';
import { RolesItem } from '../models/roles-item.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit { 
  private registerSubscription: Subscription;

  public date = new Date();

  public minDate = this.date.getFullYear() - 15;
  public maxDate = this.date.getFullYear();

  private rolesItem: RolesItem[];

  public bindRolesItem() {
    this.rolesItem = [
      {
        roleId: 20,
        description: "(Student) Improve my skills and learn a lot"
      },
      {
        roleId: 30,
        description: "(Teacher) Share my knowledge and skills with courses"
      }

    ]
  }

  static passwordMatchValidator(control: AbstractControl) {
    const password = control.get('password').value;
    const confirmPassword = control.get('confirmPassword').value;
    return password === confirmPassword ? null : { 'passwordMatch': true };
  }


  constructor(
    private router: Router,
    private userService: UserService
  ) { }

  ngOnInit() {
    // this.registerForm = new FormGroup({
    //   firstName: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
    //   lastName: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
    //   username: new FormControl(null, [Validators.required, Validators.maxLength(30)]),
    //   password: new FormControl(null, [Validators.required, Validators.maxLength(30)]),
    //   confirmPassword: new FormControl(null, [Validators.required])
    // }, {
    //     validators: [RegisterComponent.passwordMatchValidator]
    //   });

      this.bindRolesItem();
  }

  ngOnDestroy() {
    if (this.registerSubscription) {
      this.registerSubscription.unsubscribe();
    }
  }

  public back() {
    this.router.navigateByUrl('/login');
  }

  public register(registerForm: NgForm) {
    const userToRegister: UserMaintenance = automapper.map('registerForm', 'UserMaintenance', registerForm.value);
     
    this.registerSubscription = this.userService.register(userToRegister).subscribe(response => {
      if (response) {
        this.router.navigate(['/dashboard']);
      }

    });
  }

}
