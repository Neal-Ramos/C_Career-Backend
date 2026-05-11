# C Career Backend

Backend API for the C Career platform built with ASP.NET Core and Clean Architecture principles.

## Description

This project is a backend service that provides APIs for managing the C Career application. It uses ASP.NET Core, Entity Framework Core, and SQL Server for handling data and business logic.

## Prerequisites

Before running the project, make sure you have the following installed:

- .NET SDK 8.0+
- SQL Server
- Git
- Visual Studio 2022 or VS Code (optional)

## How to Run

### 1. Clone the repository

```bash
git clone https://github.com/Neal-Ramos/C_Career-Backend.git
```

### 2. Go to the project folder

```bash
cd C_Career-Backend
```

### 3. Configure the database connection

Update the `appsettings.json` file with your SQL Server connection string.

Example:

```json
"ConnectionStrings": {
  "CCareerDB":"Server=Your_SQL_Server;Database=CCareerDB;Trusted_Connection=True;TrustServerCertificate=True;"
},
"AdminAccount":{// CREDENTIAL FOR ADMIN ACCOUNT
  "Email": "",
  "Password": ""
},
"Supabase": {// SUPABASE CREDENTIALS
  "Url":"",
  "ServiceKey":""
},
"Resend": {// RESEND CREDENTIALS
  "ApiKey": "",
  "FromEmail": ""
},
"JwtSettings": {// JWT CONFIG
  "Secret":"",
  "Issuer":"",
  "Audience":"",
  "ExpiryMinutes": 10080
},
"OAuthClient":{// GOOGLE OAUTH CREDENTIALS
  "Id":"",
  "Secret":""
},
"Groq":{// GROQ AI CREDENTIALS
  "ApiKey":"",
  "AiModel": "",
  "Url": ""
}
```

### 4. Apply database migrations
In Root Folder
```bash
dotnet ef database update --project src/Infrastructure --startup-project src/API
```

### 5. Run the project
In API Folder
```bash
dotnet run
```

The API should now be running locally.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- SQL Server
- Clean Architecture
- MediatR

## Author

Neal Ramos  
https://github.com/Neal-Ramos
