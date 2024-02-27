import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../../Services/book.service';

function validInteger(control: any) {
    const value = control.value;
    if (value === null || value === '') {
        return null; // Allow empty input
    }
    const isValid = !isNaN(value) && Number(value) === parseFloat(value);
    return isValid ? null : { invalidIntege: true };
}

@Component({
  selector: 'app-book-edit',
  templateUrl: './book-edit.component.html',
})
export class bookEditComponent implements OnInit{
  
  bookId !: any;
  book!: any;
  bookForm!: FormGroup;
  isLoading: boolean = false;
  loadingTitle: string = 'Loading ...';
  errors: any;

  constructor(private activatedRoute: ActivatedRoute,
              private bookService: BookService,
              private formBuilder: FormBuilder,
    private router: Router) {
    
    this.bookForm = this.formBuilder.group({
          title: ['', Validators.required],
          author: [],
          price: ['', [Validators.required, validInteger]],
        });
  }
  ngOnInit(): void {
    this.bookId = this.activatedRoute.snapshot.paramMap.get('id')
    this.bookService.getbook(this.bookId).subscribe(
      (res: any) => {
        this.book = res;
        this.bookForm.patchValue(this.book);

      }
    );
  }

  

  updatebook() {
    this.isLoading = true;
    this.loadingTitle = "Creating a new book ....";
    if (this.bookForm.valid) { 
      
    this.bookService.updatebook(this.bookForm.value, this.bookId).subscribe({
        next: (res: any) => {
            this.isLoading = false;
            this.router.navigate(['/book']);
        },
        error: (err: any) => {
            this.errors = err.error.errors;
            console.log('err', this.errors);
            console.log('Name', this.errors.Name)
        }
    });
      
      
    }
    else {
      this.isLoading = false;
      this.bookForm.markAllAsTouched();
    }
  }

}
