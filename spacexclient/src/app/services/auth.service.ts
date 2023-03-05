import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  clientID: string = '296278154937-vq0v5rm6uv57nj69jrur5dma5okj47a6.apps.googleusercontent.com';
  secret: string = '4qMwC_58DmcBGMi6gsIlu4A3';
  redirectUrl: string = environment.redirectUrl;

  idToken: string;

  constructor(private http: HttpClient) {
    
  }

  onSignIn(googleUser){
    this.idToken = googleUser.getAuthResponse().id_token;
    console.log(this.idToken);
  }
}