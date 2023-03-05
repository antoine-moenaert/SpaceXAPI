import { Component, OnInit, NgZone } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private authSvc: AuthService, ngZone: NgZone) {
    window['onSignIn'] = (user) => ngZone.run(() => this.onSignIn(user)) 
    console.log(this.authSvc.idToken);
   }

  ngOnInit() {
  }

  onSignIn(googleUser) {
    this.authSvc.idToken = googleUser.getAuthResponse().id_token;
    if(!environment.production)
      console.log(this.authSvc.idToken);
    console.log("You are logged in !");
  }
}