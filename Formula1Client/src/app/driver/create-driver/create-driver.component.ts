import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertMessageComponent } from 'src/app/alert-message/alert-message.component';
import { Team } from 'src/app/team/team.model';
import { TeamService } from 'src/app/team/team.service';
import { Driver } from '../driver.model';
import { DriverService } from '../driver.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-create-driver',
  templateUrl: './create-driver.component.html',
  styleUrls: ['./create-driver.component.css']
})
export class CreateDriverComponent implements OnInit, OnDestroy {
  //This component is used to create and to edit
  isEdit: boolean = false;
  idEditDriver?: number = undefined;

  teams: Array<Team> = [];

  driverForm = new FormGroup({
    name: new FormControl("", [Validators.required]),
    number: new FormControl(0, [Validators.required]),
    abbreviation: new FormControl("", [Validators.required]),
    nationality: new FormControl("", [Validators.required]),
    birthday: new FormControl("", [Validators.required]),
    team: new FormControl({}, [])
  });

  constructor(private driverService: DriverService, private router: Router, private activRoute: ActivatedRoute, private formBuilder: FormBuilder, private dialog: MatDialog, private teamService: TeamService){
    this.getTeams();
  }

  ngOnInit(): void {
    this.isEdition();
  }

  isEdition() {
    var id = this.activRoute.snapshot.queryParamMap.get('id')
    if(!!id){
      this.isEdit = true;
      this.idEditDriver = +id;
      this.getDriver(+id);
    }
  }

  getDriver(id: number) {
    this.driverService.getDriver(id)
      .subscribe({
        next: (driver: Driver) => {
          this.driverForm.controls.name.setValue(driver.name);
          this.driverForm.controls.number.setValue(driver.number);
          this.driverForm.controls.abbreviation.setValue(driver.abbreviation);
          this.driverForm.controls.nationality.setValue(driver.nationality);
          this.driverForm.controls.birthday.setValue(driver.birthday);
          
          var findTeam = this.teams.find(t => t.id == ((driver.team) ? driver.team.id : -1));
          if(findTeam){
            this.driverForm.controls.team.setValue(findTeam);
          }
          
          this.driverForm.updateValueAndValidity();         

        },
        error: (response) => {
          console.log(response);
          this.redirectToDriversList()
        }
      });
  }

  getTeams(){
    this.teamService.getAllTeams()
      .subscribe({
        next: (teams) => {
          this.teams = teams;
        },
        error: (response) => {
          console.log(response);
        }
      });
  }

  editOrSaveForm() {
    if(this.driverForm.valid){
      const formData = this.driverForm.value;

      const datePipe = new DatePipe('en-US');
      var formatDate = datePipe.transform(formData.birthday, 'yyyyMMddHHmmss');

      const newEditDriver = new Driver(
        formData.name!,
        formData.number!,
        formData.abbreviation!,
        formData.nationality!,
        formatDate!,
        (formData.team as Team).id
      )

      if(this.isEdit && this.idEditDriver){

        this.driverService.editDriver(this.idEditDriver, newEditDriver)
          .subscribe({
            next: (driver) => {
              this.redirectToDriversList();
            },
            error: (response) => {
              console.log(response);
              this.redirectToDriversList();
            }
          });        
      } else {

        this.driverService.addDriver(newEditDriver)
          .subscribe({
            next: (driver) => {
              this.redirectToDriversList();
            },
            error: (response) => {
              console.log(response);
              this.redirectToDriversList();
            }
          });
      }
      
    }

  }

  delete() {
    if(this.isEdit && this.idEditDriver){

      const dialogRef = this.dialog.open(AlertMessageComponent, {
        width: '500px',
        height: '200px',
        data: {
          title: 'Delete the driver?',
          message: 'Are you sure you want to delete the driver? The driver can not be recovered once deleted!'
        }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result && this.idEditDriver) {
          this.driverService.deleteDriver(this.idEditDriver)
            .subscribe({
              next: (response) => {
                this.redirectToDriversList();
              },
              error: (response) => {
                console.log(response);
                this.redirectToDriversList();
              }
            });
        }
      });
    }
  }

  redirectToDriversList() {
    this.router.navigate(['driver/list']);
  }

  ngOnDestroy(): void {
    
  }
}
