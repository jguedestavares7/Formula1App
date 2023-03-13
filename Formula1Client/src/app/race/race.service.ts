import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { API_URL, API_REQ_HEADER } from '../comunicationConstants';
import { Race } from './race.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RaceService {

  private apiUrl = API_URL + "Race/";

  constructor(private http: HttpClient) { }

  getAllRaces(): Observable<Race[]> {
    return this.http.get<Race[]>(this.apiUrl, {headers: API_REQ_HEADER});
  }

  getRace(id: number): Observable<Race> {
    return this.http.get<Race>(this.apiUrl + id, {headers: API_REQ_HEADER});
  }

  addRace(race: Race): Observable<Race> {
    return this.http.post<Race>(this.apiUrl, race, {headers: API_REQ_HEADER});
  }

  editRace(id: number, race: Race): Observable<Race> {
    return this.http.put<Race>(this.apiUrl + id, race, {headers: API_REQ_HEADER});
  }

  deleteRace(id: number): Observable<Boolean> {
    return this.http.delete<Boolean>(this.apiUrl + id, {headers: API_REQ_HEADER});
  }
}
