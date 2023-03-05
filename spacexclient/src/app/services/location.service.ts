import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ILocation } from '../interfaces/iLocation';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  private base_url = environment.base_url + "/locations";

  constructor(private http : HttpClient) { }

  getLocations() : Observable<ILocation[]> {
    return this.http.get<ILocation[]>(this.base_url);
  }
}
