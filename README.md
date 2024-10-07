### **OMDb API Movie Management Console**

**Description:**

A C# application that provides a complete solution for managing movie information using the OMDb API. 
This project includes a Web API built with ASP.NET Core for retrieving, caching, and persisting movie data in a MySQL database, as well as a C# console application client that consumes the API. It offers full CRUD functionality for movie entries, leveraging memory caching for efficiency and OData for flexible querying.

**Key Features:**

OMDb API Integration: Fetch movie details by title, year, or IMDb ID using OMDb's movie database.
Memory Caching & Persistence: Efficiently cache movie data in memory and persist entries in a MySQL database for quick retrieval and long-term storage.
Flexible OData Queries: Utilize OData for advanced filtering, sorting, and searching on cached movie data.
CRUD Operations: Full create, read, update, and delete operations for movie entries through both the API and console application client.
C# Console Application Client: An interactive console application allowing users to easily manage movie entries through the API.

---

### **Table of Contents**

1. [Description](#description)
2. [Key Features](#key-features)
3. [Technologies Used](#technologies-used)
4. [Prerequisites](#prerequisites)
5. [Getting Started](#getting-started)
   - [API Setup](#api-setup)
   - [Console Application Setup](#console-application-setup)
6. [Usage](#usage)
   - [Running the API](#running-the-api)
   - [Running the Console Application](#running-the-console-application)
7. [Endpoints & Functionality](#endpoints--functionality)
8. [Configuration](#configuration)
9. [Contributing](#contributing)
10. [License](#license)

---

### **Technologies Used**

- **C#**
- **ASP.NET Core Web API** for API creation and handling requests
- **Entity Framework Core** for database management and CRUD operations
- **MySQL** as the persistent data storage
- **Simple Injector** for dependency injection
- **Swagger & Swashbuckle** for API documentation
- **OData** for advanced querying support
- **HttpClient** for handling API requests to OMDb
- **IMemoryCache** for in-memory caching
- **Git & GitHub** for version control

---

### **Prerequisites**

- **.NET 6.0 SDK**: Ensure you have the .NET SDK installed.
- **MySQL**: A MySQL server set up for the database.
- **Git**: For version control (optional, if you want to clone the repository).

---

### **Getting Started**

#### **API Setup**

1. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/OMDb-API-Movie-Management-Console.git
   cd OMDb-API-Movie-Management-Console
   ```

2. **Configure the Database**
   - Create a MySQL database named `OmdbApiDB`.
   - Update the connection string in `appsettings.json` in the API project.
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "server=localhost;database=OmdbApiDB;user=root;password=your_password;"
     }
     ```

3. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```

4. **Set Up User Secrets (for OMDb API Key)**
   ```bash
   dotnet user-secrets set "OMDbService:ApiKey" "YOUR_OMDB_API_KEY"
   ```

5. **Run the API**
   ```bash
   dotnet run
   ```

#### **Console Application Setup**

1. **Update API Endpoint URL**: Ensure that the console application is pointing to the correct API endpoint in its configuration.
   
2. **Run the Console Application**
   ```bash
   dotnet run --project MoviesConsoleClient
   ```

---

### **Usage**

#### **Running the API**

1. Open a terminal and navigate to the API project.
2. Run the command:
   ```bash
   dotnet run
   ```
3. Use a tool like **Swagger UI** (automatically available when the API runs) to interact with the endpoints.

#### **Running the Console Application**

1. Open a separate terminal and navigate to the console project.
2. Run the command:
   ```bash
   dotnet run
   ```
3. Follow the interactive prompts to manage movie entries.

---

### **Endpoints & Functionality**

| HTTP Method | Endpoint               | Description                                  |
|-------------|------------------------|----------------------------------------------|
| `GET`       | `/api/CachedEntries`   | Retrieves all cached movie entries with OData support. |
| `GET`       | `/api/CachedEntries/{id}` | Retrieves a specific movie by ID. |
| `POST`      | `/api/CachedEntries`   | Creates a new movie entry.                   |
| `PUT`       | `/api/CachedEntries/{id}` | Updates a specific movie by ID.             |
| `DELETE`    | `/api/CachedEntries/{id}` | Deletes a specific movie by ID.             |

---

### **Configuration**

1. **API Key Configuration**: The API uses `dotnet user-secrets` to securely store the OMDb API key.
2. **Database Configuration**: The connection string for MySQL is stored in `appsettings.json`. Ensure it matches your database setup.
3. **Memory Cache**: Caching is configured in the `OMDbService` class for efficient data retrieval.

---

### **Contributing**

1. **Fork the Repository**: Create your own copy of the repo by forking it.
2. **Clone the Repository**: Use `git clone` to download the code to your machine.
3. **Create a New Branch**: Always create a feature branch for your work.
   ```bash
   git checkout -b feature-branch-name
   ```
4. **Commit and Push**: Once you are done with your changes, commit and push to your fork.
   ```bash
   git add .
   git commit -m "Feature: Add a new feature"
   git push origin feature-branch-name
   ```
5. **Create a Pull Request**: Navigate to the original repo and create a pull request for review.

---
