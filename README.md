# Players & Teams Management System (ASP.NET MVC 5)

## 📌 Project Overview
A full-stack web application designed to manage Cricket Teams and Players using a **Master-Detail** architecture. This project demonstrates the implementation of **ASP.NET MVC 5** with **Entity Framework (Database First)** workflow.

The system handles complex relationships where a Team (Master) acts as a container for multiple Players (Detail), ensuring referential integrity and seamless data navigation.

## ⚙️ Tech Stack
* **Framework:** ASP.NET MVC 5 (.NET Framework)
* **ORM:** Entity Framework 6 (Database First approach)
* **Database:** Microsoft SQL Server
* **Frontend:** Razor View Engine, Bootstrap, jQuery, AJAX
* **Tools:** Visual Studio, SQL Server Management Studio (SSMS)

## 🚀 Key Features
* **Master-Detail CRUD:** Create, Read, Update, and Delete operations for Teams and Players.
* **Cascading Actions:**
  * Selecting a Team dynamically loads associated Players.
  * Validation rules ensure a Player cannot exist without a valid Team.
* **Database Integration:**
  * Uses an existing SQL Schema (`.edmx` generation).
  * Implements `DbContext` for data access.
* **Error Handling:** Custom handling for `DbUpdateException` and concurrency conflicts.
* **Responsive UI:** Built with Bootstrap for mobile-friendly access.

## 📂 Project Structure
* **📂 Controllers:**
  * `TeamsController.cs` - Manages team logic.
  * `PlayersController.cs` - Handles player registration and mapping to teams.
* **📂 Models:**
  * `Model1.edmx` - The Entity Framework visual designer file.
  * `Team.cs` / `Player.cs` - Entity classes auto-generated from the DB.
* **📂 Views:**
  * Razor (`.cshtml`) files utilizing strongly typed models and partial views.
* **📂 SQL:**
  * Contains `DDL.sql` and `DML.sql` to reconstruct the database.

## 🛠️ How to Run
1.  **Database Setup:**
    * Open SSMS.
    * Run the script located in `SQL/DDL.sql` to create the schema.
    * Run `SQL/DML.sql` to populate initial data.
2.  **Application Config:**
    * Clone the repository.
    * Open the solution in Visual Studio.
    * Open `web.config` and update the `connectionString` `Data Source` to match your local SQL Server name.
3.  **Run:**
    * Press **F5** to launch the application.