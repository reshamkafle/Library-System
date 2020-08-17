import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import {Author} from '../_models/author';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  baseURL: string =`${environment.apiUrl}/api/author`;

  constructor(private http: HttpClient) {
  }

  getAuthorList(): Observable<Author[]>
  {
    return this.http.get<Author[]>(this.baseURL)
  }

  getAuthor(id: string): Observable<Author>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.get<Author>(url);
  }
  addAuthor(author: Author): Observable<Author> {

    return this.http.post<Author>(this.baseURL,author, this.httpOptions);   
  }
  deleteAuthor(id: string): Observable<Author>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.delete<Author>(url);
  }

  editAuthor(id: string, author: Author): Observable<Author>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.put<Author>(url,author, this.httpOptions);
  }
}
