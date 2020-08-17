import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import {MediaType} from '../_models/mediaType';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MediaTypeService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  baseURL: string =`${environment.apiUrl}/api/mediatype`;

  constructor(private http: HttpClient) {
  }

  getMediaTypeList(): Observable<MediaType[]>
  {
    return this.http.get<MediaType[]>(this.baseURL)
  }

  getMediaType(id: string): Observable<MediaType>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.get<MediaType>(url);
  }
  addMediaType(employee: MediaType): Observable<MediaType> {

    return this.http.post<MediaType>(this.baseURL,employee, this.httpOptions);   
  }
  deleteMediaType(id: string): Observable<MediaType>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.delete<MediaType>(url);
  }

  editMediaType(id: string, employee: MediaType): Observable<MediaType>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.put<MediaType>(url,employee, this.httpOptions);
  }
}
