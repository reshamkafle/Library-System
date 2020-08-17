import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';

import {ErrorInterceptor } from './_helpers/error.interceptor';
import { JwtInterceptor} from './_helpers/jwt.interceptor';
import { AuthorListComponent } from './components/author-list/author-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatDialogModule} from '@angular/material/dialog';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { MediaTypeListComponent } from './components/media-type-list/media-type-list.component';
import { PublisherListComponent } from './components/publisher-list/publisher-list.component';
import { MediaListComponent } from './components/media-list/media-list.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { SetupComponent } from './components/setup/setup.component';
import { LibraryComponent } from './components/library/library.component';
import { HistoryComponent } from './components/history/history.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    AuthorListComponent,
    EmployeeListComponent,
    MediaTypeListComponent,
    PublisherListComponent,
    MediaListComponent,
    UserListComponent,
    SetupComponent,
    LibraryComponent,
    HistoryComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatDialogModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
