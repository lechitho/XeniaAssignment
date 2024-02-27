import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { BookService } from '../../../Services/book.service';
import { Inject } from '@angular/core';

function validInteger(control: any) {
  const value = control.value;
  if (value === null || value === '') {
    return null; // Allow empty input
  }
  const isValid = !isNaN(value) && Number(value) === parseFloat(value);
  return isValid ? null : { invalidInteg: true };
}

@Component({
  selector: 'app-book-create',
  templateUrl: './book-create.component.html', 
})
export class bookCreateComponent implements OnInit {
  
  bookForm!: FormGroup;
  isLoading: boolean = false;
  loadingTitle: string = 'Loading ...';
  
  ngOnInit(): void {
    // Add initialization logic here
  }

  // ...

  constructor(@Inject(BookService) private bookService: BookService,
    private formBuilder: FormBuilder,
    private router: Router) { }
  price!: number

  errors : any = []

  public createbook() {
    this.isLoading = true;
    this.loadingTitle = "Creating a new book ....";
    if (this.bookForm.valid) { 
      
      this.bookService.createbook(this.bookForm.value).subscribe({
              next: (res: any) => {
                this.router.navigate(['/book']);
              },
              error: (err: any) => {
                this.errors = err.error.errors;
              }
      });
      
      this.isLoading = false;
    }
    else {
      this.isLoading = false;
      this.bookForm.markAllAsTouched();
    }
    
  }
}
