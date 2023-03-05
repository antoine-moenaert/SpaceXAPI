import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LaunchesComponent } from './components/launches/launches.component';
import { LaunchesxComponent } from './components/launchesx/launchesx.component';


const routes: Routes = [
  {
    path: 'launches',
    component: LaunchesComponent
  },
  {
    path: 'launchesX',
    component: LaunchesxComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
