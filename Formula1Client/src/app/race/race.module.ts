import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RaceRoutingModule } from './race-routing.module';
import { CreateRaceComponent } from './create-race/create-race.component';
import { ListRaceComponent } from './list-race/list-race.component';
import { SharedModule } from '../shared.module';
import { RaceService } from './race.service';



@NgModule({
  declarations: [
    CreateRaceComponent,
    ListRaceComponent
  ],
  imports: [
    CommonModule,
    RaceRoutingModule,
    SharedModule
  ],
  exports: [
    ListRaceComponent,
    CreateRaceComponent
  ],
  providers: [
    RaceService
  ]
})
export class RaceModule { }
