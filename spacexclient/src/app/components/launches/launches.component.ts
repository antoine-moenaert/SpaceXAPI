import { Component, OnInit, OnDestroy } from '@angular/core';
import { ILaunches } from 'src/app/interfaces/iLaunches';
import { Router } from '@angular/router';
import { LaunchService } from 'src/app/services/launch.service';
import { Subscription, Observable } from 'rxjs';
import { ChangeDetectorRef } from '@angular/core';
import { IRocket } from 'src/app/interfaces/iRocket';
import { ILocation } from 'src/app/interfaces/iLocation';
import { RocketService } from 'src/app/services/rocket.service';
import { LocationService } from 'src/app/services/location.service';

@Component({
  selector: 'app-launches',
  templateUrl: './launches.component.html',
  styleUrls: ['./launches.component.css']
})
export class LaunchesComponent implements OnInit, OnDestroy {
  private launchesSbc : Subscription;
  private rocketsSbc : Subscription;
  private locationsSbc : Subscription;
  private updateSbc : Subscription;
  private createSbc : Subscription;
  private countSbc : Subscription;
  private deleteSbc : Subscription;
  launches : ILaunches[];
  rockets : IRocket[];
  locations : ILocation[];
  newLaunch : ILaunches = {
    id: 0,
    flightNumber: 0,
    missionName: "",
    locationId: 0,
    rocketId: 0,
  };
  sortOrder : string = "asc";
  sortHeader :  string = "flightNumber"
  count : number;
  page :  number = 0;
  length : number = 2;
  selectedId : number = 0;
  searchText : string = '';
  error : boolean = false;

  constructor(private launchSvc: LaunchService, private rocketSvc: RocketService, private locationSvc: LocationService, private router: Router) { }

  ngOnInit() {
    this.countSbc = this.launchSvc.countLaunches().subscribe(
      count => {this.count = count}
    )
    this.launchesSbc = this.launchSvc.getLaunches(this.searchText, this.page, this.sortHeader, this.length, this.sortOrder).subscribe(launches => {
      this.launches = launches;
      let test = launches[0].rocket.name;
      console.log(launches)
    })
    this.rocketsSbc = this.rocketSvc.getRockets().subscribe(rockets =>
      this.rockets = rockets
    )
    this.locationsSbc = this.locationSvc.getLocations().subscribe(locations =>
      this.locations = locations
    )
  }

  ngOnDestroy(): void {
    this.countSbc.unsubscribe;
    this.launchesSbc.unsubscribe;
    this.rocketsSbc.unsubscribe;
    this.locationsSbc.unsubscribe;
    if(this.updateSbc)
      this.updateSbc.unsubscribe;
    if(this.createSbc)
      this.createSbc.unsubscribe;
    if(this.deleteSbc)
      this.deleteSbc.unsubscribe;
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

  editLaunch(event: MouseEvent, id: number){
    this.selectedId = id;
    this.ngOnInit();
  }

  updateLaunch(event: MouseEvent, launch: ILaunches){
    launch.flightNumber = Number(launch.flightNumber);
    launch.rocketId = Number(launch.rocketId);
    launch.locationId = Number(launch.locationId);
    this.updateSbc = this.launchSvc.updateLaunch(launch).subscribe({next: () => this.ngOnInit()});
    this.selectedId = 0;
  }

  addLaunch(event: MouseEvent){
    this.selectedId = -1;
  }

  createLaunch(event:MouseEvent, launch: ILaunches){
    launch.flightNumber = Number(launch.flightNumber);
    launch.rocketId = Number(launch.rocketId);
    launch.locationId = Number(launch.locationId);
    this.createSbc = this.launchSvc.createLaunch(launch).subscribe(
      () => {
        this.error = false;
        this.ngOnInit();
        this.selectedId = 0;
      },
      () => {
        this.error=true
      });
    
  }

  cancelLaunch(event: MouseEvent){
    this.selectedId = 0;
  }

  deleteLaunch(event: MouseEvent, id: number){
  this.deleteSbc = this.launchSvc.deleteLaunch(id).subscribe({next: () => {
    this.countSbc = this.launchSvc.countLaunches().subscribe({next: count => {
      this.count = count;
      if(this.page*this.length <= this.count)
        this.page--;
      this.ngOnInit();
      }})
  }});
  }

  previousPage(event: MouseEvent){
    if(this.page > 0)
      this.page--;
    this.ngOnInit();
  }

  nextPage(event: MouseEvent){
    if(this.page * this.length < this.count)
      this.page++;
    this.ngOnInit();
  }

  searchLaunches(){
    this.page = 0;
    this.ngOnInit();
  }
}