import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertMessageComponent } from 'src/app/alert-message/alert-message.component';
import { Driver } from 'src/app/driver/driver.model';
import { DriverService } from 'src/app/driver/driver.service';
import { Race } from '../race.model';
import { RaceService } from '../race.service';

@Component({
  selector: 'app-create-race',
  templateUrl: './create-race.component.html',
  styleUrls: ['./create-race.component.css']
})
export class CreateRaceComponent implements OnInit, OnDestroy {
  //This component is used to create and to edit
  isEdit: boolean = false;
  idEditRace?: number = undefined;

  drivers: Array<Driver> = [];

  raceForm = new FormGroup({
    name: new FormControl("", [Validators.required]),
    country: new FormControl("", [Validators.required]),
    numberLaps: new FormControl(0, [Validators.required]),
    date: new FormControl("", [Validators.required]),
    winnerDriver: new FormControl({}, [])
  });

  constructor(private raceService: RaceService, private router: Router, private activRoute: ActivatedRoute, private formBuilder: FormBuilder, private dialog: MatDialog, private driverService: DriverService){
    this.getDrivers();
  }

  ngOnInit(): void {
    this.isEdition();
  }

  isEdition() {
    var id = this.activRoute.snapshot.queryParamMap.get('id')
    if(!!id){
      this.isEdit = true;
      this.idEditRace = +id;
      this.getRace(+id);
    }
  }

  getRace(id: number) {
    this.raceService.getRace(id)
      .subscribe({
        next: (race: Race) => {
          this.raceForm.controls.name.setValue(race.name);
          this.raceForm.controls.country.setValue(race.country);
          this.raceForm.controls.numberLaps.setValue(race.numberLaps);
          this.raceForm.controls.date.setValue(race.date);
          
          var findDriver = this.drivers.find(t => t.id == ((race.winnerDriver) ? race.winnerDriver.id : -1));
          if(findDriver){
            this.raceForm.controls.winnerDriver.setValue(findDriver);
          }
          
          this.raceForm.updateValueAndValidity();         

        },
        error: (response) => {
          console.log(response);
          this.redirectToRaceList()
        }
      });
  }

  getDrivers() {
    this.driverService.getAllDrivers()
      .subscribe({
        next: (drivers) => {
          this.drivers = drivers;
        },
        error: (response) => {
          console.log(response);
        }
    });
  }

  editOrSaveForm() {
    if(this.raceForm.valid){
      const formData = this.raceForm.value;

      const datePipe = new DatePipe('en-US');
      var formatDate = datePipe.transform(formData.date, 'yyyyMMddHHmmss');

      const newEditRace = new Race(
        formData.name!,
        formData.country!,
        formData.numberLaps!,
        formatDate!,
        (formData.winnerDriver! as Driver).id,
      )

      if(this.isEdit && this.idEditRace){

        this.raceService.editRace(this.idEditRace, newEditRace)
          .subscribe({
            next: (race) => {
              this.redirectToRaceList();
            },
            error: (response) => {
              console.log(response);
              this.redirectToRaceList();
            }
          });
      } else {
        this.raceService.addRace(newEditRace)
          .subscribe({
            next: (race) => {
              this.redirectToRaceList();
            },
            error: (response) => {
              console.log(response);
              this.redirectToRaceList();
            }
          });
      }
      
    }

  }

  delete() {
    if(this.isEdit && this.idEditRace){

      const dialogRef = this.dialog.open(AlertMessageComponent, {
        width: '500px',
        height: '200px',
        data: {
          title: 'Delete the race?',
          message: 'Are you sure you want to delete the race? The race can not be recovered once deleted!'
        }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result && this.idEditRace) {
          this.raceService.deleteRace(this.idEditRace)
            .subscribe({
              next: (response) => {
                this.redirectToRaceList();
              },
              error: (response) => {
                console.log(response);
                this.redirectToRaceList();
              }
            });
        }
      });
    }
  }

  redirectToRaceList() {
    this.router.navigate(['race/list']);
  }

  ngOnDestroy(): void {
    
  }
}
