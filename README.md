PulseLink — Distributed Blood Donation Management System

PulseLink is a full-stack blood donation management system I designed and built by re-architecting an earlier PHP-based college project into a production-ready system using modern backend practices.

The focus of this project was not just feature development, but building a system that is structured, scalable, and deployable — with clear separation of concerns, real-time capabilities, and production debugging experience.

---

🌐 Live System

Frontend: https://pulse-link.netlify.app
Backend API: https://pulselink-v6cr.onrender.com

---

🧠 Context & Evolution

This project originated from a basic PHP CRUD application I built during college. Instead of incrementally improving that version, I chose to rebuild the system from scratch using ASP.NET Core and Angular.

This allowed me to:

Introduce proper architectural boundaries
Implement authentication and role-based access
Add real-time communication and caching layers
Understand deployment and runtime behavior in production

The result is a system that reflects engineering decisions, not just implementation.

---

🏗 Architecture

The system follows a layered architecture inspired by clean architecture principles.

API Layer ("BDMS.Api")

Entry point of the system
Handles routing, middleware, and authentication
Responsible for request orchestration

---

Application Layer ("BDMS.Application")

Contains business logic and use cases
Services:
  - "AuthService"
  - "DonationService"
  - "DonorService"
  - "DashboardService"
Coordinates domain operations without direct infrastructure dependency

---

Domain Layer ("BDMS.Domain")

Core entities:
  - "User"
  - "Donor"
  - "Donation"
Represents business rules and structure
No external dependencies

---

Infrastructure Layer ("BDMS.Infrastructure")

Handles all external integrations:
  - Entity Framework Core (data access)
  - Repository implementations
  - Redis caching abstraction
  - SignalR real-time communication
  - PDF generation (QuestPDF)

---

🔄 Request Flow

Client → API Controller → Application Service → Repository → Database
↘ Redis Cache (optional)
↘ SignalR (event-driven updates)

---

⚙️ Core Capabilities

Authentication & Authorization

JWT-based authentication
Role-based access control (Admin / User)
Password hashing with BCrypt

---

Donation Workflow

Initiation → Approval → Completion lifecycle
Admin-controlled validation flow
Status tracking and auditability

---

Real-Time Layer (SignalR)

Infrastructure supports event-driven updates
Enables live UI updates for donation state changes

---

Caching Layer (Redis)

Introduced "ICacheService" abstraction
Designed to reduce redundant DB access
Structured for scalability, even though not fully utilized yet

---

Document Generation

Automated PDF certificates for completed donations

---

🛠 Tech Stack

Backend

ASP.NET Core Web API
Entity Framework Core
Redis (StackExchange.Redis)
SignalR

Frontend

Angular
TypeScript
Bootstrap

Database

SQL Server (development)
SQLite (production)

Deployment

Backend: Render (containerized deployment)
Frontend: Netlify

---

⚙️ Engineering Decisions

Rebuild vs Extend

Chose to rebuild the PHP project instead of extending it to:

Eliminate tight coupling
Introduce layered architecture
Enable long-term scalability

---

SQLite in Production

Used SQLite for:

Simplicity in containerized deployment
Avoiding external DB dependencies

Tradeoff:

Not ideal for high concurrency, but sufficient for current scope

---

Redis Integration

Added caching abstraction early to:

Enable performance optimization
Decouple caching strategy from business logic

---

SignalR Integration

Introduced real-time capability to:

Support event-driven UI updates
Prepare system for future live features

---

Deployment Strategy

Separated frontend and backend deployments
Handled CORS and environment-specific issues manually
Debugged runtime issues in a live environment

---

🧪 Challenges & Debugging

Database migration (SQL Server → SQLite)
Handling DB initialization in production
CORS issues between Netlify and Render
Docker build and runtime failures
JWT authentication debugging across environments
Redis connection handling in production

These challenges were critical in understanding system behavior beyond local development.

---

⚙️ Local Setup

Clone

git clone https://github.com/your-username/pulselink.git
cd pulselink

---

Backend

cd BDMS.Api
dotnet restore
dotnet run

"appsettings.json":

"ConnectionStrings": {
  "DefaultConnection": "Data Source=bdms.db"
}

---

Frontend

cd bdms-client
npm install
ng serve

apiUrl: 'https://localhost:5001/api'

---

🔑 Default Admin

Email: admin@test.com
Password: Admin@123

---

🚀 Future Work

Smart donor matching (rule-based → ML-assisted)
Full Redis caching strategy
Email notifications
Advanced analytics dashboard
Migration to scalable database (PostgreSQL / Azure SQL)

---

💼 What This Demonstrates

Ability to design and structure full-stack systems
Understanding of backend architecture and separation of concerns
Experience with real-time systems and caching layers
Hands-on debugging of production deployment issues
End-to-end ownership from development → deployment

---

👨‍💻 About Me

I focus on building systems that are not just functional, but structured and scalable. I’m particularly interested in backend engineering, system design, and understanding how real-world applications behave in production environments.
