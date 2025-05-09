# 👨‍💼 Employee Management System

A modern, scalable Employee Management System built with **ASP.NET Core WebAPI** and **Blazor WebAssembly**. This project features **Role-Based Authentication and Authorization**, **JWT Token Authentication**, and full **CRUD operations** for managing employees. The backend follows **Clean Architecture** principles and leverages **Dependency Injection**, **EF Core**, and **Dapper** for data access.

---

## 🔧 Technologies Used

### 🖥 Backend
- **ASP.NET Core WebAPI**
- **JWT Authentication**
- **Role-based Authorization** (Admin, User)
- **Entity Framework Core (EF Core)**
- **Dapper** (for optimized queries)
- **Dependency Injection**
- **Clean Architecture**

### 🌐 Frontend
- **Blazor WebAssembly (WASM)**
- **HttpClient for API Integration**
- **Local Storage for JWT Handling**
- **Role-based UI Rendering**

## 📦 NuGet Packages Used

```xml
<PackageReference Include="Blazored.LocalStorage" Version="4.5.0" />
<PackageReference Include="Blazored.Toast" Version="4.2.1" />
<PackageReference Include="Microsoft.AspNetCore.Components.Authorization" Version="8.0.15" />
<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="8.0.15" />
<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="8.0.15" PrivateAssets="all" />
<PackageReference Include="Microsoft.AspNetCore.Http.Abstractions" Version="2.3.0" />
<PackageReference Include="Microsoft.AspNetCore.WebUtilities" Version="8.0.15" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.15" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
```
---

## ✅ Features

- 🔐 **Authentication & Authorization**
  - Secure login with **JWT tokens**
  - Role-based access for **Admin** and **User**
    
- 👥 **Employee Management**
  - CRUD operations: Create, Read, Update, Delete
  - Authorization applied to each action
 
- 🧱 **Clean Architecture**
  - Separation of concerns: Presentation, Business Logic, Data Access
 
- ⚙️ **Dependency Injection**
  - Fully decoupled and testable services
    
- ⚡ **Hybrid ORM**
  - EF Core for full ORM operations
  - Dapper for performance-critical queries
 
---

## 🚀 Getting Started

### Prerequisites
- .NET SDK 8.0 
- SQL Server (Microsoft SQL Server (MSSQL))
- Visual Studio or JetBrain Rider

---
