import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { bookCreateComponent } from './Pages/book/create/book-create.component';
import { BookListComponent } from './Pages/book/get/book-list.component';
import { bookEditComponent } from './Pages/book/edit/book-edit.component';

const routes: Routes = [

  { path: '', component: BookListComponent, title: 'Home page' },
  { path: 'book', component: BookListComponent, title: 'book' },
  { path: 'book/create', component: bookCreateComponent, title: 'Create book' },
  { path: 'book/:id', component: bookEditComponent, title: 'Edit book' },
  { path: '**', component: BookListComponent, title: 'book' }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
