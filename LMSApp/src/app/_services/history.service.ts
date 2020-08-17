import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import {History} from '../_models/history';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  checkoutBaseURL: string =`${environment.apiUrl}/api/checkout`;
  reservationBaseURL: string =`${environment.apiUrl}/api/reservation`;

  constructor(private http: HttpClient) {
  }

  getCheckoutHistory(): Observable<History[]>
  {
    return this.http.get<History[]>(this.checkoutBaseURL)
  }

  getReservationHistory(): Observable<History[]>
  {
    return this.http.get<History[]>(this.reservationBaseURL)
  }
}
