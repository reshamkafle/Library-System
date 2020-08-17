import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import {Item} from '../_models/item';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReserveService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  baseURL: string =`${environment.apiUrl}/api/reservation`;

  constructor(private http: HttpClient) {
  }

  reserve(item: Item){
    return this.http.post<Item>(this.baseURL,item, this.httpOptions);   
  }
}
