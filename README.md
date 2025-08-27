# BookHub Project

## Table of Contents
- [BookHub Project](#bookhub-project)
  - [Table of Contents](#table-of-contents)
  - [Walkers](#walkers)
  - [Functional Requirements](#functional-requirements)
  - [Project Structure](#project-structure)
  - [Features](#features)
  - [Getting Started](#getting-started)
    - [1. Prerequisites](#1-prerequisites)
  - [Design](#design)
    - [1. Frontend – `bookhub-ui`](#1-frontend--bookhub-ui)
    - [2. Backend – `BookHub.App`](#2-backend--bookhubapp)
  - [Installation](#installation)

## Walkers
Project created as part of a take home assessment for WalkersGlobal Senior Frontend position.

## Functional Requirements
Detailed requirements can be found in the pdf document in project [root](Walkers%20Technical%20Assessment%201.0%20-%20Full-Stack%20Developer.pdf).

## Project Structure

```

Book-Hub/
├── bookhub-ui/        # Frontend (Vue.js)
├── BookHub.App/       # Backend Web API (ASP.NET Core)
├── BookHub.Core/      # Core domain models & business logic
├── BookHub.Tests/     # Unit & integration tests
├── BookHub.sln        # Solution file
├── .gitignore
└── README.md

````

## Features
- Add, update, and manage books  
- Validation on ratings & comments  
- SQLite persistence  
- Unit test coverage  


## Getting Started
### 1. Prerequisites
Make sure you have the following installed:
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [Node.js (LTS)](https://nodejs.org/) (v18+ recommended)  
- [npm](https://www.npmjs.com/) or [yarn](https://yarnpkg.com/)  
- Git  

## Design

BookHub is structured as a full-stack web application with two main components:

### 1. Frontend – `bookhub-ui`
- **Framework:** Vue 3 with Vite + TypeScript
- **Responsibilities:**
  - Provides a user-friendly UI for interacting with the book collection.
  - Uses **Element Plus** for modern, responsive components.
  - Uses **Vue Router** for navigation and **VueUse** for composable utilities.
  - Displays charts using **Chart.js** + `vue-chartjs`.
- **Key Points:**
  - Communicates with the backend API via **Axios**.
  - Includes unit tests with **Vitest** and **Vue Test Utils**.
  - Built and bundled with **Vite** for fast development and optimized production builds.
  
### 2. Backend – `BookHub.App`
- **Framework:** ASP.NET Core Web API (C#)
- **Responsibilities:**
  - Exposes RESTful endpoints for managing books (CRUD operations).
  - Handles validation logic (e.g., max 25 books, ratings between 1–5, disallowed words in comments).
  - Persists data using **Entity Framework Core** with a SQLite database.
- **Key Points:**
  - All business rules are enforced in the backend to ensure data integrity.
  - Provides JSON responses to the frontend via `axios` calls.





## Installation

Running Instructions for Vue 3 + .NET Backend

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/daniellarrusso/book-hub.git
   cd book-hub
   ```
2. **Install Frontend Dependencies and run**:
   ```bash
   cd bookhub-ui
   npm install
   npm run dev
   ```
   → Opens at http://localhost:5173
3. **Install Backend Dependencies and run**:
   ```bash
   cd BookHub.App
   dotnet restore
   dotnet run
   ```
   → Opens at http://localhost:5228
