import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './_helpers/auth.guard';
import { AuthorListComponent } from './components/author-list/author-list.component';
import {EmployeeListComponent} from './components/employee-list/employee-list.component';
import {MediaTypeListComponent} from './components/media-type-list/media-type-list.component';
import {PublisherListComponent} from './components/publisher-list/publisher-list.component';
import {MediaListComponent} from './components/media-list/media-list.component';
import {UserListComponent} from './components/user-list/user-list.component';
import {SetupComponent} from './components/setup/setup.component';
import {LibraryComponent} from './components/library/library.component';
import {HistoryComponent} from './components/history/history.component';

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'author', component: AuthorListComponent, canActivate: [AuthGuard] },
    { path: 'employee', component: EmployeeListComponent, canActivate: [AuthGuard] },   
    { path: 'mediatype', component: MediaTypeListComponent, canActivate: [AuthGuard] }, 
    { path: 'publisher', component: PublisherListComponent, canActivate: [AuthGuard] }, 
    { path: 'media', component: MediaListComponent, canActivate: [AuthGuard] }, 
    { path: 'user', component: UserListComponent, canActivate: [AuthGuard] },    
    { path: 'setup', component: SetupComponent, canActivate: [AuthGuard] },    
    { path: 'library', component: LibraryComponent, canActivate: [AuthGuard] },    
    { path: 'history', component: HistoryComponent, canActivate: [AuthGuard] },    

    { path: 'login', component: LoginComponent },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
