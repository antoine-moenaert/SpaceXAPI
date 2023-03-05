import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IRocket } from '../interfaces/iRocket';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RocketService {

  private base_url : string = environment.base_url + "/rockets";
  
  constructor(private http : HttpClient) { }

  getRockets(): Observable<IRocket[]> {
    return this.http.get<IRocket[]>(this.base_url);
  }
}