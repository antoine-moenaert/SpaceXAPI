import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IlaunchesX } from '../interfaces/iLaunchesX';

@Injectable({
  providedIn: 'root'
})
export class LaunchesxService {

  private base_url = "https://api.spacexdata.com/v3/launches"
  constructor(private http: HttpClient) { }

  getLaunchesXAll() : Observable<IlaunchesX[]>{
    return this.http.get<IlaunchesX[]>(this.base_url);
  }

  getLaunchesX(offset:number, limit:number, sort:string, order:string) : Observable<IlaunchesX[]>{
    return this.http.get<IlaunchesX[]>(`${this.base_url}?offset=${offset}&limit=${limit}&sort=${sort}&order=${order}`);
  }
}