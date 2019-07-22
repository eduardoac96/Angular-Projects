import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.scss']
})
export class ForgotComponent implements OnInit, OnDestroy {

  private forgotSubscription: Subscription;

  constructor(private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit() {
  }

  ngOnDestroy() {
    if (this.forgotSubscription) {
      this.forgotSubscription.unsubscribe();
    }
  }


  public forgot(forgotForm: NgForm) {
    if (!forgotForm.valid) {
      return;
    }
  }


}
