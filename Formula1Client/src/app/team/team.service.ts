import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { API_URL, API_REQ_HEADER } from '../comunicationConstants';
import { Team } from './team.model';
import { Observable } from 'rxjs';
import { Driver } from '../driver/driver.model';
import { Race } from '../race/race.model';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  private apiUrl = API_URL + "Team/";

  constructor(private http: HttpClient) { }

  getAllTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(this.apiUrl, {headers: API_REQ_HEADER});
  }

  getTeam(id: number): Observable<Team> {
    return this.http.get<Team>(this.apiUrl + id, {headers: API_REQ_HEADER});
  }

  getTeamOfDriver(id: number): Observable<Team> {
    return this.http.get<Team>(this.apiUrl + "driver/" + id, {headers: API_REQ_HEADER});
  }

  getDriversOfTeam(id: number): Observable<Driver[]> {
    return this.http.get<Driver[]>(this.apiUrl + id + "/drivers", {headers: API_REQ_HEADER});
  }

  getRacesOfTeam(id: number): Observable<Race[]> {
    return this.http.get<Race[]>(this.apiUrl + id + "/races", {headers: API_REQ_HEADER});
  }

  addTeam(team: Team): Observable<Team> {
    return this.http.post<Team>(this.apiUrl, team, {headers: API_REQ_HEADER});
  }

  editTeam(id: number, team: Team): Observable<Team> {
    return this.http.put<Team>(this.apiUrl + id, team, {headers: API_REQ_HEADER});
  }

  deleteTeam(id: number): Observable<Boolean> {
    return this.http.delete<Boolean>(this.apiUrl + id, {headers: API_REQ_HEADER});
  }
}
