PulseLink — Distributed Blood Donation Management System

PulseLink is a full-stack blood donation management system I designed and built by re-architecting an earlier PHP-based college project into a production-ready system using modern backend practices.

The focus of this project was not just feature development, but building a system that is structured, scalable, and deployable — with clear separation of concerns, real-time capabilities, and real production debugging experience.

🌐 Live System
🔗 Frontend: https://pulse-link.netlify.app⁠�
🔗 Backend API: https://pulselink-v6cr.onrender.com⁠�

This project originated from a basic PHP CRUD application I built during college.
Instead of incrementally improving that version, I chose to rebuild the system from scratch using ASP.NET Core and Angular.
This allowed me to:
Introduce proper architectural boundaries
Implement authentication and role-based access
Add real-time communication and caching layers
Understand deployment and runtime behavior in production
The result is a system that reflects engineering decisions, not just implementation.

Architecture
The system follows a layered architecture inspired by clean architecture principles.
🔹 API Layer (BDMS.Api)
Entry point of the system
Handles routing, middleware, and authentication
Responsible for request orchestration
🔹 Application Layer (BDMS.Application)
Contains business logic and use cases
Services:
AuthService
DonationService
DonorService
DashboardService
🔹 Domain Layer (BDMS.Domain)
Core entities:
User
Donor
Donation
🔹 Infrastructure Layer (BDMS.Infrastructure)
Entity Framework Core
Repository implementations
Redis caching
SignalR
QuestPDF
🔄 Request Flow

Client → API Controller → Application Service → Repository → Database
                                 ↘ Redis Cache
                                 ↘ SignalR
⚙️ Core Capabilities
🔐 Authentication
JWT authentication
Role-based access
BCrypt hashing
🩸 Donation Workflow
Initiation → Approval → Completion
Admin validation
⚡ Real-Time (SignalR)
Live updates
🧠 Redis Caching
Reduced DB calls
📄 PDF Generation
Certificates using QuestPDF
🛠 Tech Stack
Backend
ASP.NET Core
EF Core
Redis
SignalR
Frontend
Angular
TypeScript
Bootstrap
Database
SQL Server (dev)
SQLite (prod)
Deployment
Render (backend)
Netlify (frontend)

⚙️ Local Setup
Clone
Bash
git clone https://github.com/your-username/pulselink.git
cd pulselink

Backend
Bash
cd BDMS.Api
dotnet restore
dotnet run

Frontend
Bash
cd bdms-client
npm install
ng serve

🔑 Default Admin
Email: admin@test.com
Password: Admin@123

🚀 Future Work
Smart donor matching
Email notifications
Advanced analytics
PostgreSQL migration
