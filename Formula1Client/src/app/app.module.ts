import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { TeamModule } from './team/team.module';
import { DriverModule } from './driver/driver.module';
import { RaceModule } from './race/race.module';

import { SharedModule } from './shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlertMessageComponent } from './alert-message/alert-message.component';
import { MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';

import { WelcomePageComponent } from './welcome/welcome-page/welcome-page.component';
@NgModule({
  declarations: [
    AppComponent,
    AlertMessageComponent,
    WelcomePageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    BrowserAnimationsModule,
    TeamModule,
    DriverModule,
    RaceModule,
    MatNativeDateModule
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'pt-PT'}    //set the locale to Portugal
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
