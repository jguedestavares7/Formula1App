import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import {MatPaginator} from '@angular/material/paginator';
import { Driver } from '../driver.model';
import { DriverService } from '../driver.service';

@Component({
  selector: 'app-list-driver',
  templateUrl: './list-driver.component.html',
  styleUrls: ['./list-driver.component.css']
})
export class ListDriverComponent implements OnInit, OnDestroy {
  columnsToDisplay: string[] = ['name', 'number', 'abbreviation', 'nationality', 'birthday', 'teamName'];
  titles: Record<string, string> = {
    name: "Team",
    number: "Number",
    abbreviation: "Engine",
    nationality: "Team director",
    birthday: "Birthday",
    teamName: "Team"
  }

  drivers: MatTableDataSource<Driver> = new MatTableDataSource<Driver>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private driverService: DriverService, private router: Router){

  }

  ngOnInit(): void {
    this.loadDrivers();
  }

  loadDrivers() {
    this.driverService.getAllDrivers()
      .subscribe({
        next: (drivers) => {
          this.drivers = new MatTableDataSource(drivers);
          this.drivers.paginator = this.paginator;
        },
        error: (response) => {
          console.log(response);
        }
    });
  }

  editDriver(driver: any){
    this.router.navigate(['driver/create'], {
      queryParams: {
        id: driver.id
      }
    })
  }

  filterDriver(event: Event){
    this.drivers.filter = (event.target as HTMLInputElement)?.value.trim().toLowerCase();
  }

  ngOnDestroy(): void {
    
  }
}
