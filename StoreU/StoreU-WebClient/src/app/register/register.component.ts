import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPasswordStrengthComponent } from '@angular-material-extensions/password-strength';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  minDate = new Date(2000, 0, 1);
  maxDate = new Date(2020, 0, 1);

  showDetails: boolean;
   
  @ViewChild('passwordComponentWithConfirmation', { static: true })
  passwordComponentWithConfirmation: MatPasswordStrengthComponent;

  constructor() { }

  ngOnInit() {
  }
  onStrengthChanged(strength: number) {
    console.log('password strength = ', strength);
  }

}
