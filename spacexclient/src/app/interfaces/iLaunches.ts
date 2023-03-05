import { IRocket } from './iRocket';
import { ILocation } from './iLocation';

export interface ILaunches {
  id: number,
  flightNumber: number;
  missionName: string;
  locationId: number;
  rocketId: number;
  rocket?: IRocket;
  location?: ILocation
}