import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { IlaunchesX } from 'src/app/interfaces/iLaunchesX';
import { LaunchesxService } from 'src/app/services/launchesx.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-launchesx',
  templateUrl: './launchesx.component.html',
  styleUrls: ['./launchesx.component.css']
})
export class LaunchesxComponent implements OnInit, OnDestroy {
  private launchesSbc : Subscription;
  private rocketsSbc : Subscription;
  private locationsSbc : Subscription;
  private updateSbc : Subscription;
  private createSbc : Subscription;
  private countSbc : Subscription;
  private deleteSbc : Subscription;
  launchesX : IlaunchesX[];
  sortOrder : string = "asc";
  sortHeader :  string = "flightNumber"
  count : number;
  page :  number = 0;
  limit : number = 2;
  selectedId : number = 0;
  searchText : string = '';

  constructor(private launchesxSvc: LaunchesxService, private router: Router) { }

  ngOnInit() {
    this.launchesSbc = this.launchesxSvc.getLaunchesXAll().subscribe(launchesX => {
      this.count = launchesX.length;
    })
    this.launchesSbc = this.launchesxSvc.getLaunchesX(this.page*this.limit, this.limit, this.sortHeader, this.sortOrder).subscribe(launchesX => {
      this.launchesX = launchesX;
    })
  }

  ngOnDestroy(): void {
    this.launchesSbc.unsubscribe;
  }

  countLaunches():void{
  }

  changeSorting(sortHeader: string){
    this.sortHeader = sortHeader;
    this.page = 0;
    if(this.sortOrder == "asc"){
      this.sortOrder = "desc";
    }
    else {
      this.sortOrder = "asc";
    }
    this.ngOnInit();
  }

  previousPage(event: MouseEvent){
    if(this.page > 0)
      this.page--;
    this.ngOnInit();
  }

  nextPage(event: MouseEvent){
    if(this.page * this.limit < this.count)
      this.page++;
    this.ngOnInit();
  }
}
