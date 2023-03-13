import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { API_URL, API_REQ_HEADER } from '../comunicationConstants';
import { Driver } from './driver.model';
import { Observable } from 'rxjs';
import { Race } from '../race/race.model';

@Injectable({
  providedIn: 'root'
})
export class DriverService {

  private apiUrl = API_URL + "Driver/";

  constructor(private http: HttpClient) { }

  getAllDrivers(): Observable<Driver[]> {
    return this.http.get<Driver[]>(this.apiUrl, {headers: API_REQ_HEADER});
  }

  getDriver(id: number): Observable<Driver> {
    return this.http.get<Driver>(this.apiUrl + id, {headers: API_REQ_HEADER});
  }

  getRacesOfDriver(id: number): Observable<Race> {
    return this.http.get<Race>(this.apiUrl + id + "/races", {headers: API_REQ_HEADER});
  }

  addDriver(driver: Driver): Observable<Driver> {
    return this.http.post<Driver>(this.apiUrl, driver, {headers: API_REQ_HEADER});
  }

  editDriver(id: number, driver: Driver): Observable<Driver> {
    return this.http.put<Driver>(this.apiUrl + id, driver, {headers: API_REQ_HEADER});
  }

  deleteDriver(id: number): Observable<Boolean> {
    return this.http.delete<Boolean>(this.apiUrl + id, {headers: API_REQ_HEADER});
  }
}
