### Crypto Tracker API

RESTful API for fetching, storing, and tracking historical cryptocurrency data 
from the BlockCypher API, built with ASP.NET Core 9.0 and EF Core with SQLite.

### Prerequisites

- .NET 9.0 SDK
- Docker Desktop (for containerized execution)

### How to Run (Option 1: Docker Container)

1. Clone the project and navigate to the root folder. (where the `.sln` resides).
2. Build the Docker image:
   ```
   docker build -t cryptotracker-api .
   ```
3. Run the containerized environment:
   ```
   docker run -d -p 8080:8080 --name cryptotracker-app cryptotracker-api
   ```
4. Access the API via Swagger UI: Open your browser and navigate directly to:
   `http://localhost:8080/`

---

### How to Run (Option 2: Visual Studio run (or any other IDE))

1. Clone the project and navigate to the root folder.
2. Database Initialization (Automated Flow):
   - No manual database setup required.
   - On the very first runtime execution, the application automatically handles structural persistence initialization.
   - It will dynamically create the isolated `crypto.db` SQLite database bundle inside the `Data/` folder.
4. Run the application.
5. Access the API via Swagger UI: The application is configured to automatically launch 
   the browser directly into the Swagger UI page at startup.

---

### How to Run Tests

The project includes Unit, Integration, and Functional tests. 
You can run all of them at the same time using xUnit.

To execute all tests, run the following command from the root directory:
```
dotnet test
```

Author: Kyriaki Theocharous
