import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './Pages/Partials/Navbar/navbar.component';
import { bookCreateComponent } from './Pages/book/create/book-create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {  HttpClientModule, HTTP_INTERCEPTORS  } from '@angular/common/http';
import { LoaderComponent } from './Pages/Partials/Loader/loader.component';
import { BookListComponent } from './Pages/book/get/book-list.component';
import { bookEditComponent } from './Pages/book/edit/book-edit.component';
import { NgToastModule } from 'ng-angular-popup'

import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    bookCreateComponent,
    LoaderComponent,
    BookListComponent,
    bookEditComponent,
    bookCreateComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgToastModule,
    BsDatepickerModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
