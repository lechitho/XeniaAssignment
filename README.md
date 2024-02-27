# XeniaAssignment
book management
This repository contains a complete solution for managing book management, consisting of a .Net Core API (BookManagemnt), and an Angular frontend application. This system allows you to perform various operations on book data, such as creating, updating, deleting, and viewing Book details.

## Backend

### Book Management Service - Book Management API

Book Management Service is a .Net Core API that handles Book management. It follows best practices and design principles to ensure code quality and maintainability.

#### Features

- **Book Data**:
  - Book are identified by an `id` and have attributes including `Title`, `Author`, `published date`, and `price`.
  
- **Endpoints**:
  - The API provides the following endpoints:
    - `GET /books`: Retrieve a list of all books.
    - `GET /books/{id}`: Retrieve a book by their ID.
    - `POST /books`: Create a new book.
    - `PUT /books/{id}`: Update an existing book by their ID.
    - `DELETE /books/{id}`: Delete an book by their ID.
    
- **Design Principles**:
  - Use Clean Architecure
  - Apply CQRS pattern using MediatR
  - Follow Domain-Driven Design (DDD)
  - Apply SOLID, KISS, YAGNI principles for clean and maintainable code.
  
- **Data Storage**:
  - book data is stored in a JSON file.

- **Caching**:
  - Use .NET InMemmory Cache, apply DI to be able to replace by other Cache like Redis, ...

- **Logging**:
  - User Serilog to log console and file, be able to extend to write to other platforms like DataDog, ...
  
- **Dependency Injection**:
  - Apply for Services, Repositories, ... be able to replace, change the implementation or technical easily
  
- **Configuration**:
  - Configuration options, such as the file path of the book repository, are abstracted using configuration settings.
  
- **Unit Tests**:
  - Use xUnit to write Unit Test (for Business (Application and Domain layers)) and Integration Test (for API, ...)

## Frontend - Angular hiring process management Application

The front end is an Angular application that allows users to manage book.

#### Features

- **API Integration**:
  - The application integrates with the book Management API to perform various operations.
  - Use HttpClient with Observable

## How to Run
To run easily, we can use Visual Studio and Angular CLI to serve/build
(or we can run/deploy each service/app by dotnet CLI and Angular CLI)
1. Clone Repo
2. Use Visual Studio to open the solution file **Xenia.sln**
3. Run the project using Visual Studio
4. Run the Angular app by open **FrontEnd** and run / build by angular cli
