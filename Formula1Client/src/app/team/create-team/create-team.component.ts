import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertMessageComponent } from 'src/app/alert-message/alert-message.component';
import { Team } from '../team.model';
import { TeamService } from '../team.service';

@Component({
  selector: 'app-create-team',
  templateUrl: './create-team.component.html',
  styleUrls: ['./create-team.component.css']
})
export class CreateTeamComponent implements OnInit, OnDestroy {
  
  //This component is used to create and to edit
  isEdit: boolean = false;
  idEditTeam?: number = undefined;

  teamForm = new FormGroup({
    name: new FormControl("", [Validators.required]),
    carName: new FormControl("", [Validators.required]),
    engine: new FormControl("", [Validators.required]),
    director: new FormControl("", [Validators.required])
  });

  constructor(private teamService: TeamService, private router: Router, private activRoute: ActivatedRoute, private formBuilder: FormBuilder, private dialog: MatDialog){

  }

  ngOnInit(): void {
    this.isEdition();
  }

  isEdition() {
    var id = this.activRoute.snapshot.queryParamMap.get('id')
    if(!!id){
      this.isEdit = true;
      this.idEditTeam = +id;
      this.getTeam(+id);
    }
  }

  getTeam(id: number) {
    this.teamService.getTeam(id)
      .subscribe({
        next: (team: Team) => {
          this.teamForm.controls.name.setValue(team.name);
          this.teamForm.controls.carName.setValue(team.carName);
          this.teamForm.controls.engine.setValue(team.engine);
          this.teamForm.controls.director.setValue(team.director);

          this.teamForm.updateValueAndValidity();
        },
        error: (response) => {
          console.log(response);
          this.redirectToTeamList();
        }
      });
  }

  editOrSaveForm() {    
    if(this.teamForm.valid) {
      const formData = this.teamForm.value;

      if(this.isEdit && this.idEditTeam){
        const editTeam = new Team(
          formData.name!,
          formData.carName!,
          formData.engine!,
          formData.director!,
          []
        );
        this.teamService.editTeam(this.idEditTeam, editTeam)
        .subscribe({
          next: (team) => {
            this.redirectToTeamList();
          },
          error: (response) => {
            console.log(response);
            this.redirectToTeamList();
          }
        });
        
      } else {
        const newTeam = new Team(
          formData.name!,
          formData.carName!,
          formData.engine!,
          formData.director!,
          [],
          undefined
        );
        
        this.teamService.addTeam(newTeam)
        .subscribe({
          next: (team) => {
            this.redirectToTeamList();
          },
          error: (response) => {
            console.log(response);
            this.redirectToTeamList();
          }
        });
      }
    }
  }

  redirectToTeamList() {
    this.router.navigate(['team/list']);
  }

  delete() {
    if(this.isEdit && this.idEditTeam){

      const dialogRef = this.dialog.open(AlertMessageComponent, {
        width: '500px',
        height: '200px',
        data: {
          title: 'Delete the team?',
          message: 'Are you sure you want to delete the team? The team can not be recovered once deleted!'
        }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result && this.idEditTeam) {
          this.teamService.deleteTeam(this.idEditTeam)
            .subscribe({
              next: (response) => {
                this.redirectToTeamList();
              },
              error: (response) => {
                console.log(response);
                this.redirectToTeamList();
              }
            });
        }
      });
    }
  }

  ngOnDestroy(): void {
    
  }

}
