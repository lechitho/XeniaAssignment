import { Component, OnInit } from '@angular/core';
import { BookService, Book } from '../../../Services/book.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
})
export class BookListComponent implements OnInit {

  book!: Book[];
  isLoading: boolean = false;
  loadingTitle!: string;
  constructor(private bookService: BookService) {
  }
  ngOnInit(): void {
    this.getbooks();
  }
  getbooks() {
    this.loadingTitle = "loading book ..."; 
    this.isLoading = true;
    this.bookService.getbooks().subscribe(
      (res: any) => {
        this.book = res;
        this.isLoading = false;
      }
    );
    
  }
  deletebook(bookId: any) {
    this.bookService.deletebook(bookId).subscribe(
      (res: any) => {
        this.getbooks();
      }
    )
  }
}
