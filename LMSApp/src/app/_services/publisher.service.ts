import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import {Publisher} from '../_models/publisher';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PublisherService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  baseURL: string =`${environment.apiUrl}/api/publisher`;

  constructor(private http: HttpClient) {
  }

  getPublisherList(): Observable<Publisher[]>
  {
    return this.http.get<Publisher[]>(this.baseURL)
  }

  getPublisher(id: string): Observable<Publisher>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.get<Publisher>(url);
  }
  addPublisher(employee: Publisher): Observable<Publisher> {

    return this.http.post<Publisher>(this.baseURL,employee, this.httpOptions);   
  }
  deletePublisher(id: string): Observable<Publisher>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.delete<Publisher>(url);
  }

  editPublisher(id: string, employee: Publisher): Observable<Publisher>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.put<Publisher>(url,employee, this.httpOptions);
  }
}
