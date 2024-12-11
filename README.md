# VUE_NET_MSSQL

## Project Description

Since I am developing on a Mac and unable to install SQL Server, I used Docker to install Azure SQL Edge in this project. The main focus of this project is to practice setting up a basic front-end using Vue 3 with Vite, and connecting it to a back-end built with .NET Core to interact with an MSSQL database. Additionally, Husky and lint-staged are integrated with ESLint and Prettier to automatically format the code and review commit messages.

## Tech Stack

- **Vue 3** with **Vite**
- **Azure SQL Edge** (via Docker)
- **.NET Core**

## Installation Steps

### 1. Clone the Project
git clone <your-repo-url>  
cd VUE_NET_MSSQL

### 2. Install dependencies for the root project:

yarn install

### 3. Back-End Setup:
#### (1)Navigate to the Vue_Net_MSSQL/API/WebApplication1/WebApplication1 directory, create a .env file with the following content: DB_PASSWORD={YourPassword}
#### (2)Start the back-end with: dotnet watch run
#### (3)After the back-end starts successfully, you can access the Swagger UI at https://localhost:7242/swagger/index.html where you will see three APIs:
- #### GET: /api/TodoApp/GetNotes
- #### POST: /api/TodoApp/AddNotes
- #### DELETE: /api/TodoApp/DeleteNotes/{id}

### 4. Front-End Setup:
 #### (1)Navigate to the Vue_Net_MSSQL/UI/todoapp directory and install the dependencies:
 yarn install
 #### (2)Run the front-end app:
 yarn start dev
 #### (3)After the front-end starts:
 You can view the Todo List app at http://localhost:5173/ .

## Automatic Code Formatting with Prettier and ESLint
This project is configured to automatically format code using ESLint and Prettier whenever a commit is made. The configuration is set in the package.json under the lint-staged section:

```bash
"lint-staged": {
  "*.js": [
    "eslint --fix",
    "prettier --write"
  ],
  "*.ts": [
    "eslint --fix",
    "prettier --write"
  ],
  "*.vue": [
    "eslint --fix",
    "prettier --write"
  ]
}
```

#### This ensures that your code will be automatically linted and formatted before committing. The rules applied include:
#### ESLint: Ensures code quality and style.
#### Prettier: Automatically formats code to follow a consistent style.
#### You do not need to manually format or lint your code before committing. Husky and lint-staged will handle this for you!
