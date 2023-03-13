import { NgModule } from '@angular/core';
import { ListTeamComponent } from './list-team/list-team.component';
import { CreateTeamComponent } from './create-team/create-team.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'team/list', component: ListTeamComponent},
  {path: 'team/create', component: CreateTeamComponent},
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class TeamRoutingModule { }
