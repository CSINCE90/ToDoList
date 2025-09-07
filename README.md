# ToDoList API

This project is a **REST API for managing ToDoLists and TaskActivities**, built with **.NET 8, C#, and MySQL**.  
It showcases clean code practices, separation of concerns (Entities vs DTOs), and patterns such as **Service Layer** and **Repository**.  
The API supports **create, update, and soft delete** operations, with **Swagger UI** for easy testing and exploration.  
Designed as a technical demo project, it highlights **professional coding standards** and **best practices** for backend development.

## 👤 Author

Developed by **Francesco Chifari** as a technical demonstration project.

---

## 🚀 Features
- Create, update, and soft delete **ToDoLists**.
- Create, update, and soft delete **TaskActivities** inside a ToDoList.
- Database relational design using **EF Core** and MySQL.
- Clean separation between **Entities** (database models) and **DTOs** (API request/response).
- Swagger UI for testing endpoints.

---

## 🛠️ Tech Stack
- **.NET 8 (C#)**
- **Entity Framework Core**
- **MySQL** (via Pomelo.EntityFrameworkCore.MySql)
- **Swagger (Swashbuckle)**
- **DotNetEnv** for environment variable management

## ✅ Patterns & Best Practices
- DTOs → prevent exposing database entities directly.
- Repository Pattern → separates data access logic.
- Service Layer → encapsulates business logic.
- Soft Delete → data is never lost, only hidden from queries.

---

## 📂 Project Structure

ToDoListAPI  
├── Controllers  
│   ├── ToDoListsController.cs  
│   └── TaskActivitiesController.cs  
├── Models  
│   ├── ToDoList.cs  
│   └── TaskActivity.cs  
├── DTO  
│   ├── ToDoListDTO.cs  
│   └── TaskActivityDTO.cs  
├── Data  
│   └── AppDbContext.cs  
├── Services  
│   ├── ToDoListService.cs  
│   └── TaskActivityService.cs  
├── Repositories  
│   ├── ToDoListRepository.cs  
│   └── TaskActivityRepository.cs  
├── Program.cs  
└── .env *(local only, not pushed to GitHub)*  

---

## 🗄️ Database Schema
Two tables:
- **ToDoLists**
  - `Id` (PK)
  - `Name`
  - `CreatedAt`
  - `UpdatedAt`

- **TaskActivities**
  - `Id` (PK)
  - `Title`
  - `Description`
  - `DueDate`
  - `IsCompleted`
  - `IsDeleted` (for soft delete)
  - `CreatedAt`
  - `UpdatedAt`
  - `ToDoListId` (FK → ToDoLists)

---

## ⚙️ Setup & Run

### 1. Clone the repo
```bash
git clone https://github.com/<your-username>/ToDoListAPI.git
cd ToDoListAPI/ToDoListAPI




