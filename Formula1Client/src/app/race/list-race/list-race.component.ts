import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Race } from '../race.model';
import { RaceService } from '../race.service';

@Component({
  selector: 'app-list-race',
  templateUrl: './list-race.component.html',
  styleUrls: ['./list-race.component.css']
})
export class ListRaceComponent implements OnInit, OnDestroy {
  columnsToDisplay: string[] = ['name', 'country', 'numberLaps', 'date', 'winnerDriver'];
  titles: Record<string, string> = {
    name: "Team",
    country: "Country",
    numberLaps: "Number of laps",
    date: "Date",
    winnerDriver: "Winner driver"
  }

  races: MatTableDataSource<Race> = new MatTableDataSource<Race>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private raceService: RaceService, private router: Router){

  }

  ngOnInit(): void {
    this.loadRaces();
  }

  loadRaces() {
    this.raceService.getAllRaces()
      .subscribe({
        next: (races) => {
          this.races = new MatTableDataSource(races);
          this.races.paginator = this.paginator;
        },
        error: (response) => {
          console.log(response);
        }
    });
  }

  editRace(race: any){
    this.router.navigate(['race/create'], {
      queryParams: {
        id: race.id
      }
    })
  }

  filterRace(event: Event){
    this.races.filter = (event.target as HTMLInputElement)?.value.trim().toLowerCase();
  }

  ngOnDestroy(): void {
    
  }
}
