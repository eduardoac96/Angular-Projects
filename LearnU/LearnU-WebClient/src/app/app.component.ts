import { Component, HostListener } from '@angular/core'; 
import { Subscription } from 'rxjs'; 
import { AuthService } from './shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  private authenticationSubscription: Subscription;
  public isSideNavOpened: boolean;
  public authentication = this.authService.isAuthenticated();


  constructor(private authService: AuthService){

  }
  
  @HostListener('window:resize', ['$event']) onResize(event?) {
    this.isSideNavOpened = window.innerWidth > 768;
  }

  ngOnInit(): void {
    this.onResize();
    this.authenticationSubscription = this.authService.onAuthenticationChanged
      .subscribe(isAuthenticated => {
        this.authentication = isAuthenticated;
      });
  }

  ngOnDestroy(): void {
    if (this.authenticationSubscription) {
      this.authenticationSubscription.unsubscribe();
    }
  }

}
