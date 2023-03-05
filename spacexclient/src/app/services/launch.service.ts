import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ILaunches } from '../interfaces/iLaunches';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LaunchService {

  constructor(private http: HttpClient) { }

  private base_url: string = environment.base_url + "/launches";

  getLaunches(searchText:string, page:number, sortHeader:string, length:number, sortOrder:string): Observable<ILaunches[]>
  {
    return this.http.get<ILaunches[]>(`${this.base_url}?missionName=${searchText}&page=${page}&header=${sortHeader}&length=${length}&dir=${sortOrder}`)
  }

  countLaunches(): Observable<number>
  {
    return this.http.get<number>(`${this.base_url}/count`)
  }

  updateLaunch(newLaunch: ILaunches){
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.put(`${this.base_url}`, newLaunch, {headers})
  }

  createLaunch(newLaunch: ILaunches){
    let headers = new HttpHeaders();
    //delete newLaunch.id;
    headers = headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.post(`${this.base_url}`, newLaunch, {headers})
  }

  deleteLaunch(id: number){
    return this.http.delete(`${this.base_url}/${id}`)
  }
}
