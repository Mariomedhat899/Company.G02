# Company Management System (MVC) 🏢

A web-based management portal built with **ASP.NET Core MVC**. This project provides a full administrative interface to manage organizational structures, specifically focusing on Departments and Employee lifecycles.

## 🛠️ Features
* **Full CRUD Operations:** Manage Employees and Departments with data persistence via EF Core.
* **Architecture:** Separation of concerns using the **Repository Pattern**.
* **File Uploads:** Integrated logic for handling employee profile pictures or documents.
* **Client-Side & Server-Side Validation:** Ensures data integrity using Data Annotations and jQuery Validation.
* **Partial Views:** Optimized UI components for reusable elements across the portal.
* **AutoMapper:** Seamlessly maps database entities to ViewModels to keep the UI layer clean.

## 🏗️ Design Patterns
* **MVC Pattern:** Model-View-Controller for organized web development.
* **Dependency Injection:** Used throughout the project for services and repository registration.
* **ViewModel Pattern:** Utilized to pass specific data to views without exposing sensitive database models.

## 🚀 Technical Stack
* **Backend:** ASP.NET Core 7/8 MVC
* **Frontend:** Razor Views, Bootstrap, HTML5, CSS3, jQuery
* **Database:** SQL Server / Entity Framework Core
* **Mapping:** AutoMapper

## 🔧 Installation
1. Clone the repo.
2. Update `appsettings.json` with your local SQL Server connection string.
3. Run `Update-Database` in the Package Manager Console.
4. Press `F5` to launch the portal in your browser.
