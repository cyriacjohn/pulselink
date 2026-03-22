# PulseLink — Distributed Blood Donation Management System

![Status](https://img.shields.io/badge/Status-Live-success)
![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![Angular](https://img.shields.io/badge/Angular-17-red)
![SQLite](https://img.shields.io/badge/Database-SQLite-blue)
![Redis](https://img.shields.io/badge/Cache-Redis-orange)
![SignalR](https://img.shields.io/badge/Realtime-SignalR-green)

---

PulseLink is a full-stack blood donation management system I built by re-architecting an earlier PHP college project into a **production-ready system** using modern backend practices.

This project focuses on **system design, scalability, and real-world deployment**, not just feature development.

---

## Live System

- **Frontend:** https://pulse-link.netlify.app  
- **Backend API:** https://pulselink-v6cr.onrender.com  

---

## Architecture

### API Layer (`BDMS.Api`)
- Routing, middleware, authentication  

### Application Layer (`BDMS.Application`)
- Business logic  
- Services:
  - AuthService  
  - DonationService  
  - DonorService  

### Domain Layer (`BDMS.Domain`)
- Core entities:
  - User  
  - Donor  
  - Donation  

### Infrastructure Layer (`BDMS.Infrastructure`)
- EF Core  
- Repositories  
- Redis  
- SignalR  
- QuestPDF  

---

## System Flow

```
Client (Angular)
        ↓
ASP.NET Core API
        ↓
Application Services
        ↓
Repositories
        ↓
SQLite Database
        ↓
Redis (optional)
        ↓
SignalR (real-time)
```

---

## Core Features

### Authentication
- JWT-based login  
- Role-based access  
- BCrypt password hashing  

### Donation Workflow
- Request → Approval → Completion  
- Admin-controlled validation  

### Real-Time
- SignalR integration  

### Caching
- Redis abstraction layer  

### PDF Generation
- Donation certificates  

---

## Tech Stack

**Backend**
- ASP.NET Core  
- Entity Framework Core  
- Redis  
- SignalR  

**Frontend**
- Angular  
- TypeScript  
- Bootstrap  

**Database**
- SQL Server (dev)  
- SQLite (prod)  

**Deployment**
- Render (backend)  
- Netlify (frontend)  

---

## ⚙️ Local Setup

### Clone

```bash
git clone https://github.com/your-username/pulselink.git
cd pulselink
```

### Backend

```bash
cd BDMS.Api
dotnet restore
dotnet run
```

### Frontend

```bash
cd bdms-client
npm install
ng serve
```

---

## Default Admin

- Email: admin@test.com  
- Password: Admin@123  

---

## Future Work

- Smart donor matching  
- Email notifications  
- Advanced analytics  
- PostgreSQL migration  

---