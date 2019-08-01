import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { NgForm } from '@angular/forms';
import swal from 'sweetalert2';


@Component({
  selector: 'app-forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.scss']
})
export class ForgotComponent implements OnInit {

  private forgotSubscription: Subscription;

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit() {
  }

  ngOnDestroy(){
      if(this.forgotSubscription){
        this.forgotSubscription.unsubscribe();
      }
  }

  public sendEmail(forgotForm: NgForm){

    debugger

      if(!forgotForm.valid){
        return;
      }

    let email: string = forgotForm.value.username;

    this.forgotSubscription = this.authService.generateCode(email).subscribe(response => {
      if (response) {
 
        swal.fire("Message",`Favor de revisar las instrucciones enviadas al correo  ${email}`, "success").then(() => {
          debugger    
          
          this.router.navigate(['/login'],  { state: { response } });
        });
 
      }
    });

  }

  public back(): void {
    this.router.navigate(['/back']);
  }
 
}
