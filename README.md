# StoreMobile 📱🛒

Welcome to **StoreMobile**, an intuitive and feature-rich mobile application designed for e-commerce and store management. This project provides a seamless shopping experience for users while offering efficient tools for browsing catalog items, managing carts, and placing orders.

---

## 🌟 Features

- **User Authentication:** Secure sign-up, login, and user profile management.
- **Product Catalog:** Browse products by categories with real-time search and filter capabilities.
- **Product Details:** View rich product descriptions, images, pricing, and availability.
- **Shopping Cart:** Add, remove, and update quantities of items dynamically.
- **Checkout Process:** Smooth order placement flow with summary breakdowns.
- **Responsive UI:** Modern, clean, and mobile-first user interface design.

---

## StoreMobile API & Backend Services

[![C#](https://img.shields.io/badge/Language-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET Core / ASP.NET Core](https://img.shields.io/badge/Framework-ASP.NET%20Core-512BD4.svg)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC292B.svg)](https://www.microsoft.com/sql-server)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

**StoreMobile** is a robust backend RESTful API built with **C#** and **ASP.NET Core**, backed by **Microsoft SQL Server**. It provides backend services for e-commerce and mobile store applications, supporting user authentication, product catalog management, shopping cart operations, order processing, and inventory management.

---

## 🚀 Features

- **User Authentication & Authorization**: Secure JWT-based login, registration, and role-based access control (e.g., Admin vs. Customer).
- **Product & Catalog Management**: CRUD operations for products, categories, stock tracking, and searching/filtering.
- **Cart & Order Processing**: Shopping cart persistence, order placement, order status updates, and history tracking.
- **SQL Server Data Persistence**: Relational database modeling using **Entity Framework Core** (EF Core) with LINQ queries and migrations.
- **Swagger / OpenAPI Documentation**: Interactive API testing and schema documentation out of the box.

---

## 🛠️ Tech Stack

- **Language**: C# (.NET Core / ASP.NET Core)
- **Web Framework**: ASP.NET Core Web API
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core / Dapper
- **Authentication**: JSON Web Tokens (JWT) / ASP.NET Core Identity
- **API Documentation**: Swagger (Swashbuckle)

---

## 📁 Project Structure

```text
StoreMobile/
├── Controllers/         # API endpoints and route handlers
├── Models/              # Entity models and database representations
├── DTOs/                # Data Transfer Objects for API request/response
├── Data/                # DbContext, EF Core migrations, and seeds
├── Services/            # Business logic and service implementations
├── Repositories/        # Data access layer / repository pattern
├── Middleware/          # Custom middleware (Error handling, JWT auth)
├── appsettings.json     # Configuration file (Connection strings, JWT keys)
└── Program.cs           # Application entry point & dependency injection setup
```

---

## ⚙️ Getting Started
Prerequisites
Make sure you have the following installed on your development machine:

• .NET SDK (Version 6.0 / 7.0 / 8.0 or later)

• Microsoft SQL Server or SQL Server Express / LocalDB

• SQL Server Management Studio (SSMS) or Azure Data Studio

• Git

Installation & Configuration
1 Clone the repository:

```Bash
git clone [https://github.com/nightylang/StoreMobile.git](https://github.com/nightylang/StoreMobile.git)
cd StoreMobile
```

2 Configure Database Connection String:
Open appsettings.json (or appsettings.Development.json) and update the SQL Server connection string to match your database instance:

```JSON
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=DB_StoreMobile;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyHere1234567890!",
    "Issuer": "StoreMobileAPI",
    "Audience": "StoreMobileApp"
  }
}
```

3 Apply Database Migrations:
Run the following commands in your terminal to create the database schema:

```Bash
dotnet tool install --global dotnet-ef
dotnet ef database update
```

(Alternatively, run Update-Database in Package Manager Console in Visual Studio.)

4 Run the Application:

```Bash
dotnet run
```

5 Explore API Documentation:
Once running, open your browser and navigate to:

```Plaintext
https://localhost:7XXX/swagger
```

📡 Sample Endpoints
Method	Endpoint	Description	Auth Required
POST	/api/auth/register	Register a new user account	No
POST	/api/auth/login	Authenticate user and return JWT	No
GET	/api/products	Get list of all products	No
GET	/api/products/{id}	Get product details by ID	No
POST	/api/products	Create a new product	Yes (Admin)
POST	/api/cart/items	Add item to shopping cart	Yes
POST	/api/orders	Place a new order	Yes

---

## 🤝 Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

Distributed under the MIT License. See `LICENSE` for more information.

---

## 📬 Contact & Support

Developer - [@nightylang](https://github.com/nightylang)  
Project Link: [https://github.com/nightylang/StoreMobile](https://github.com/nightylang/StoreMobile)
