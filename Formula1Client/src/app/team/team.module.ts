import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListTeamComponent } from './list-team/list-team.component';
import { CreateTeamComponent } from './create-team/create-team.component';
import { TeamRoutingModule } from './team-routing.module';
import { SharedModule } from '../shared.module';
import { TeamService } from './team.service';



@NgModule({
  declarations: [
    ListTeamComponent,
    CreateTeamComponent
  ],
  imports: [
    CommonModule,
    TeamRoutingModule,
    SharedModule
  ],
  exports: [
    ListTeamComponent,
    CreateTeamComponent
  ],
  providers: [
    TeamService
  ]
})
export class TeamModule { }
