import { NgModule } from '@angular/core';
import { ListDriverComponent } from './list-driver/list-driver.component';
import { CreateDriverComponent } from './create-driver/create-driver.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'driver/list', component: ListDriverComponent},
  {path: 'driver/create', component: CreateDriverComponent},
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class DriverRoutingModule { }
