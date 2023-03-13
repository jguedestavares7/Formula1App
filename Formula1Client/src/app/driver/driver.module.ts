import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DriverRoutingModule } from './driver-routing.module';
import { CreateDriverComponent } from './create-driver/create-driver.component';
import { ListDriverComponent } from './list-driver/list-driver.component';
import { SharedModule } from '../shared.module';
import { DriverService } from './driver.service';


@NgModule({
  declarations: [
    CreateDriverComponent,
    ListDriverComponent
  ],
  imports: [
    CommonModule,
    DriverRoutingModule,
    SharedModule
  ],
  exports: [
    ListDriverComponent,
    CreateDriverComponent 
  ],
  providers: [
    DriverService
  ]
})
export class DriverModule { }
