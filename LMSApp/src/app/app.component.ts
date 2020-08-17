import { Component } from '@angular/core';
import { Observable } from 'rxjs';

import { Router } from '@angular/router';

import { AuthenticationService } from './_services/authentication.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'LMSApp';

  currentUser: User;
  isLoggedIn$: Observable<boolean>;
  isAdmin$: Observable<boolean>;


    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
        this.isLoggedIn$ = this.authenticationService.isLoggedIn;
        this.isAdmin$ = this.authenticationService.isAdmin;
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}
