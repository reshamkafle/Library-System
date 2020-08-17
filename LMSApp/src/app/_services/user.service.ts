import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import {User} from '../_models/user';
import { Observable } from 'rxjs';
import {ChangePassword} from '../_models/changepassword';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  baseURL: string =`${environment.apiUrl}/users`;

  constructor(private http: HttpClient) {
  }

  getUserList(): Observable<User[]>
  {
    return this.http.get<User[]>(this.baseURL)
  }

  getUser(id: string): Observable<User>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.get<User>(url);
  }
  addUser(employee: User): Observable<User> {

    const url = this.baseURL+`/register`;
    return this.http.post<User>(url,employee, this.httpOptions);   
  }
  deleteUser(id: string): Observable<User>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.delete<User>(url);
  }

  resetPassword(id: string,password: string): Observable<User>
  {
    const url = this.baseURL+`/changepassword`;
     
    let changePassword: ChangePassword = new ChangePassword();
    changePassword.password = password;
    changePassword.id = id;
    
    return this.http.put<User>(url,changePassword, this.httpOptions);
  }

  editUser(id: string, employee: User): Observable<User>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.put<User>(url,employee, this.httpOptions);
  }
}
