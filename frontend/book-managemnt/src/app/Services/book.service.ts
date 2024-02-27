import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


export interface Book{
  id: string
  title: string
  author: string
  publishedDate: Date
  price: number
}

@Injectable({ providedIn: 'root' })
  
export class BookService {

  private apiUrl = 'https://localhost:7200/api/books';

  constructor(private httpClient: HttpClient) { }
  
  
  createbook = (createdData: Book) => this.httpClient.post<Book>(`${this.apiUrl}`, createdData);

  getbooks = () => this.httpClient.get<Book[]>(`${this.apiUrl}`);

  getbook = (bookId: string) => this.httpClient.get<Book>(`${this.apiUrl}/${bookId}`);

  updatebook = (updateData: Book, bookId: string) => this.httpClient.put<Book>(`${this.apiUrl}/${bookId}`, updateData);

  deletebook = (bookId: string) => this.httpClient.delete(`${this.apiUrl}/${bookId}`);

}