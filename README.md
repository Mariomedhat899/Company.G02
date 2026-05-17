# Company Management System (MVC) 🏢

> A professional web-based management portal built with **ASP.NET Core MVC** for managing organizational structures, departments, and employee lifecycles.

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-7/8-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)

---

## 📋 Table of Contents
- [✨ Features](#-features)
- [🏗️ Architecture & Design Patterns](#️-architecture--design-patterns)
- [🚀 Technical Stack](#-technical-stack)
- [🔧 Installation & Setup](#-installation--setup)
- [🗄️ Database Migration](#️-database-migration)
- [🧪 Testing](#-testing)
- [🤝 Contributing](#-contributing)
- [📄 License](#-license)
- [👨‍💻 Author](#-author)

---

## ✨ Features

| Feature | Description |
|---------|-------------|
| 🔹 CRUD Operations | Full Create, Read, Update, Delete for Employees & Departments |
| 🔹 Repository Pattern | Clean separation of data access logic |
| 🔹 File Uploads | Handle employee profile pictures & documents securely |
| 🔹 Dual Validation | Client-side (jQuery) + Server-side (Data Annotations) |
| 🔹 Partial Views | Reusable UI components for maintainable Razor views |
| 🔹 AutoMapper | Clean mapping between Entities ↔ ViewModels |
| 🔹 Dependency Injection | Loose coupling & testable architecture |


**Patterns Implemented:**
- ✅ **MVC Pattern** – Organized web development flow
- ✅ **Repository Pattern** – Abstracted data access
- ✅ **Unit of Work** – Coordinated transactions (if applicable)
- ✅ **ViewModel Pattern** – Secure, tailored data transfer to views
- ✅ **Dependency Injection** – Built-in .NET Core DI container

---

## 🚀 Technical Stack

| Layer | Technology |
|-------|-----------|
| **Backend** | ASP.NET Core 7/8 MVC, C# 12 |
| **Frontend** | Razor Views, Bootstrap 5, HTML5, CSS3, jQuery |
| **Database** | SQL Server, Entity Framework Core |
| **Mapping** | AutoMapper |
| **Validation** | Data Annotations, jQuery Validation |
| **Auth** | ASP.NET Core Identity *(if implemented)* |

---

## 🔧 Installation & Setup

### Prerequisites
- [.NET 7/8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) or LocalDB
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code + C# Dev Kit

### Steps
```bash
# 1. Clone the repository
git clone https://github.com/Mariomedhat899/Company.G02.git
cd Company.G02

# 2. Restore dependencies
dotnet restore

# 3. Configure connection string
# Edit: Company.G02.PL/appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CompanyG02;Trusted_Connection=True;"
}

# 4. Apply migrations
Update-Database -Context ApplicationDbContext
# OR via CLI:
dotnet ef database update

# 5. Run the application
dotnet run --project Company.G02.PL
# Or press F5 in Visual Studio

🗄️ Database Migration

powershell
# Add a new migrationAdd-Migration MigrationName -Context ApplicationDbContext# Update databaseUpdate-Database -Context ApplicationDbContext# Remove last migration (if needed)Remove-Migration -Context ApplicationDbContext

---

## 🏗️ Architecture & Design Patterns
