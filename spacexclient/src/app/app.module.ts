import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { LaunchesComponent } from './components/launches/launches.component';
import { LaunchService } from './services/launch.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LaunchComponent } from './components/launch/launch.component';
import { RocketService } from './services/rocket.service';
import { AuthService } from './services/auth.service';
import { TokenInterceptor } from './tokeninterceptor';
import { LaunchesxComponent } from './components/launchesx/launchesx.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    LaunchesComponent,
    LaunchComponent,
    LaunchesxComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    LaunchService,
    RocketService,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
