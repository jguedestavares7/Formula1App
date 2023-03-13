import { NgModule } from '@angular/core';
import { ListRaceComponent } from './list-race/list-race.component';
import { CreateRaceComponent } from './create-race/create-race.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'race/list', component: ListRaceComponent},
  {path: 'race/create', component: CreateRaceComponent},
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class RaceRoutingModule { }
