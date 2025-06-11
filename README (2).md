# 🌍 Carbon Emission Tracker API

A secure, JWT-authenticated REST API built with **ASP.NET Core 8.0** to track and log carbon emissions by category.

> 🚀 Live Swagger UI: [https://carbon-emmison-tracker-backend.onrender.com/swagger](https://carbon-emmison-tracker-backend.onrender.com/swagger)

---

## 📁 Project Structure

```
Carbon_Emmison_Tracker_Backend/
├── CarbonPulse.API/               # Main API project
│   ├── Controllers/               # API Endpoints
│   ├── DTOs/                      # Data Transfer Objects
│   ├── Interfaces/                # Service Interfaces
│   ├── Middleware/                # (Pluggable middleware components)
│   ├── Models/                    # Database Models (User, Emission)
│   ├── Profiles/                  # AutoMapper Profiles
│   ├── Services/                  # Business Logic Layer
│   ├── appsettings.json          # Configuration (JWT key)
│   ├── Dockerfile                 # Deployment setup
│   └── Program.cs                 # API entrypoint & service configuration
└── CarbonPulse.API.sln            # Solution file
```

---

## ✅ Features

- 🔐 **JWT Authentication** (Register/Login)
- 📊 **Emission Recording** (Category + CO2 amount)
- 🧠 **In-memory Database** (can be upgraded to SQL/EF Core)
- 🔁 **AutoMapper** integrated for DTO mapping
- 🌐 **Swagger UI** with `Authorize` support
- 🐳 **Dockerized** for seamless deployment (via Render.com)

---

## 🚀 Getting Started (Local Setup)

### 1. Clone the Repository

```bash
git clone https://github.com/Shaik-36/Carbon_Emmison_Tracker_Backend.git
cd Carbon_Emmison_Tracker_Backend/CarbonPulse.API
```

### 2. Run the API

```bash
dotnet restore
dotnet run
```

API will start at:
```
https://localhost:7176
```

---

## 🔐 Using Swagger Auth

1. Register or Login to receive a JWT token.
2. Click on `Authorize 🔒` in Swagger.
3. Paste your token as:

```
Bearer YOUR_JWT_TOKEN
```

---

## 🐳 Docker Deployment

### Build & Run Locally

```bash
docker build -t carbon-tracker .
docker run -d -p 8080:80 carbon-tracker
```

### Deployed On Render

- **Dockerfile path**: `CarbonPulse.API/Dockerfile`
- **Root directory**: `CarbonPulse.API`

---

## 📅 Deployment Log

Deployed on Render: `June 11, 2025`  
URL: [https://carbon-emmison-tracker-backend.onrender.com](https://carbon-emmison-tracker-backend.onrender.com)

---

## 🧠 TODO / Future Features

- [ ] Add persistent database (PostgreSQL/SQL Server)
- [ ] Add user roles (Admin, User)
- [ ] Add monthly CO2 stats report
- [ ] Add unit tests
