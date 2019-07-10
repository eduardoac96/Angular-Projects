import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs'; 
import { AuthService } from '../shared/services/auth.service';
@Component({
  selector: 'app-head',
  templateUrl: './head.component.html',
  styleUrls: ['./head.component.scss']
})
export class HeadComponent implements OnInit, OnDestroy{

  private authenticationSubscription: Subscription;
  public isAuthenticated = this.authService.isAuthenticated();
  public userDisplayName: string;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
      this.setUserDisplayName();

      this.authenticationSubscription = this.authService.onAuthenticationChanged
        .subscribe(isAuthenticated => {
              this.isAuthenticated = isAuthenticated;
              this.setUserDisplayName();
        });

  }

  ngOnDestroy(): void {
    if(this.authenticationSubscription){
      this.authenticationSubscription.unsubscribe();
    }
  }


  private setUserDisplayName(){
    let displayName = '';
    if(this.authService.isAuthenticated()){
      displayName = this.authService.loggedUser.displayName ?
      `Hello ${this.authService.loggedUser.displayName}!` :
      `Hello ${this.authService.loggedUser.username}!`;
    }

    this.userDisplayName = displayName;
  }

}
