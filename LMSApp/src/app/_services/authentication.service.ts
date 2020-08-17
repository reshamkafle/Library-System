import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { User } from '../_models/user';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;
    private loggedIn = new BehaviorSubject<boolean>(false);
    private admin = new BehaviorSubject<boolean>(false);

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    get isAdmin(){      
        if(this.currentUserSubject!= null)
        {
            if(this.currentUserSubject.value.isAdmin == true){
                this.admin.next(true);
            }else{
                this.admin.next(false);
            }
        }
        return this.admin.asObservable();
    }


    get isLoggedIn() {
        if(this.currentUserSubject!= null)
        {
            this.loggedIn.next(true);
        }
        return this.loggedIn.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<any>(`${environment.apiUrl}/users/authenticate`, { username, password })
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
                this.loggedIn.next(true);

                if(this.currentUserSubject.value.isAdmin == true){
                    this.admin.next(true);
                }else{
                    this.admin.next(false);
                }

                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
        this.loggedIn.next(false);
        this.admin.next(false);

    }
}