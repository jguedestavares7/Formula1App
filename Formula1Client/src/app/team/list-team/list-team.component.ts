import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Team } from '../team.model';
import { TeamService } from '../team.service';
import {MatPaginator} from '@angular/material/paginator';

@Component({
  selector: 'app-list-team',
  templateUrl: './list-team.component.html',
  styleUrls: ['./list-team.component.css']
})
export class ListTeamComponent implements OnInit, OnDestroy {

  columnsToDisplay: string[] = ['name', 'carName', 'engine', 'director'];
  titles: Record<string, string> = {
    name: "Team",
    carName: "Car",
    engine: "Engine",
    director: "Team director"
  }
  teams: MatTableDataSource<Team> = new MatTableDataSource<Team>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private teamService: TeamService, private router: Router){
  }

  ngOnInit(): void {
    this.loadTeams();
  }

  loadTeams() {
    this.teamService.getAllTeams()
      .subscribe({
        next: (teams) => {
          this.teams = new MatTableDataSource(teams);
          this.teams.paginator = this.paginator;
        },
        error: (response) => {
          console.log(response);
        }
      });
  }

  editTeam(team: any){
    this.router.navigate(['team/create'], {
      queryParams: {
        id: team.id
      }
    })
  }

  filterTeam(event: Event){
    this.teams.filter = (event.target as HTMLInputElement)?.value.trim().toLowerCase();
  }

  ngOnDestroy(): void {
    
  }
}
