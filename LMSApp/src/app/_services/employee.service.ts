import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import {User} from '../_models/user';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  baseURL: string =`${environment.apiUrl}/api/employee`;

  constructor(private http: HttpClient) {
  }

  getUserList(): Observable<User[]>
  {
    return this.http.get<User[]>(this.baseURL)
  }
  addUser(employee: User): Observable<User> {

    return this.http.post<User>(this.baseURL,employee, this.httpOptions);   
  }

}
