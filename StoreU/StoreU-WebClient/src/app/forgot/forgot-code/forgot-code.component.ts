import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/shared/services/auth.service';
import { NgForm } from '@angular/forms';
import { Router,ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-forgot-code',
  templateUrl: './forgot-code.component.html',
  styleUrls: ['./forgot-code.component.scss']
})
export class ForgotCodeComponent implements OnInit {

  private validateCodeSubscription: Subscription;
  private UserToken: string;
  

  constructor(private authService: AuthService, private router: Router, private activatedRoute: ActivatedRoute) {
    
  this.userToken = this.activatedRoute.snapshot.paramMap.get("userToken")

   }

  ngOnInit() {
  }
userToken
  ngOnDestroy() {
    if (this.validateCodeSubscription) {
      this.validateCodeSubscription.unsubscribe();
    }
  }

  public validateCode(codeForm: NgForm): void {
    if (!codeForm.valid) {
      return;
    }

    let codeNumber: number = codeForm.value.code;
    // let email: string = codeForm.value.email;

    let email: string = 'eduardo96_@live.com';

    this.validateCodeSubscription = this.authService.validateCode(email, codeNumber).subscribe(response => {
      if (response) {
        this.router.navigate(['/forgot/reset']);
      }
    });
  }

}
