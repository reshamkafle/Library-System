import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import {Media} from '../_models/media';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MediaService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  baseURL: string =`${environment.apiUrl}/api/media`;

  constructor(private http: HttpClient) {
  }

  getMediaList(): Observable<Media[]>
  {
    return this.http.get<Media[]>(this.baseURL)
  }

  getMedia(id: string): Observable<Media>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.get<Media>(url);
  }
  addMedia(media: Media): Observable<Media> {
    return this.http.post<Media>(this.baseURL,media, this.httpOptions);   
  }
  deleteMedia(id: string): Observable<Media>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.delete<Media>(url);
  }

  editMedia(id: string, media: Media): Observable<Media>
  {
    const url = this.baseURL+`/${id}`;
    return this.http.put<Media>(url,media, this.httpOptions);
  }
}
