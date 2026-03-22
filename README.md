🚀 PulseLink — Distributed Blood Donation Management System

PulseLink is a full-stack blood donation management system I designed and built by re-architecting an earlier PHP-based college project into a production-ready system using modern backend practices.

The focus of this project was not just feature development, but building a system that is structured, scalable, and deployable — with clear separation of concerns, real-time capabilities, and real production debugging experience.

---

🌐 Live System

- 🔗 Frontend: https://pulse-link.netlify.app
- 🔗 Backend API: https://pulselink-v6cr.onrender.com

---

🧠 Context & Evolution

This project originated from a basic PHP CRUD application I built during college.

Instead of incrementally improving that version, I chose to rebuild the system from scratch using ASP.NET Core and Angular.

This allowed me to:

- Introduce proper architectural boundaries
- Implement authentication and role-based access
- Add real-time communication and caching layers
- Understand deployment and runtime behavior in production

«The result is a system that reflects engineering decisions, not just implementation.»

---

🏗 Architecture

The system follows a layered architecture inspired by clean architecture principles.

---

🔹 API Layer ("BDMS.Api")

- Entry point of the system
- Handles routing, middleware, and authentication
- Responsible for request orchestration

---

🔹 Application Layer ("BDMS.Application")

- Contains business logic and use cases

Services:

- "AuthService"

- "DonationService"

- "DonorService"

- "DashboardService"

- Coordinates domain operations without direct infrastructure dependency

---

🔹 Domain Layer ("BDMS.Domain")

- Core entities:
  
  - "User"
  - "Donor"
  - "Donation"

- Represents business rules and structure

- No external dependencies

---

🔹 Infrastructure Layer ("BDMS.Infrastructure")

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

🔐 Authentication & Authorization

- JWT-based authentication
- Role-based access control (Admin / User)
- Password hashing with BCrypt

---

🩸 Donation Workflow

- Initiation → Approval → Completion lifecycle
- Admin-controlled validation flow
- Status tracking and auditability

---

⚡ Real-Time Layer (SignalR)

- Infrastructure supports event-driven updates
- Enables live UI updates for donation state changes

---

🧠 Caching Layer (Redis)

- Introduced "ICacheService" abstraction
- Designed to reduce redundant DB access
- Structured for scalability

---

📄 Document Generation

- Automated PDF certificates using QuestPDF

---

🛠 Tech Stack

🔧 Backend

- ASP.NET Core Web API
- Entity Framework Core
- Redis (StackExchange.Redis)
- SignalR

🎨 Frontend

- Angular
- TypeScript
- Bootstrap

🗄 Database

- SQL Server (Development)
- SQLite (Production)

🚀 Deployment

- Backend → Render (containerized)
- Frontend → Netlify

---

⚙️ Engineering Decisions

🔁 Rebuild vs Extend

- Eliminated tight coupling
- Introduced layered architecture
- Enabled long-term scalability

---

🗄 SQLite in Production

Why:

- Lightweight
- Easy deployment

Tradeoff:

- Not ideal for high concurrency

---

⚡ Redis Integration

- Abstracted caching via "ICacheService"
- Prepared system for performance optimization

---

🔔 SignalR Integration

- Enabled real-time capabilities
- Supports event-driven architecture

---

🌍 Deployment Strategy

- Decoupled frontend & backend
- Managed CORS manually
- Debugged real production issues

---

🧪 Challenges & Debugging

- Database migration (SQL Server → SQLite)
- DB initialization issues in production
- CORS issues (Netlify ↔ Render)
- Docker build/runtime failures
- JWT authentication debugging
- Redis connection handling

«These were critical in understanding real-world system behavior»

---

⚙️ Local Setup

📥 Clone Repository

git clone https://github.com/your-username/pulselink.git
cd pulselink

---

🔧 Backend Setup

cd BDMS.Api
dotnet restore
dotnet run

appsettings.json

"ConnectionStrings": {
  "DefaultConnection": "Data Source=bdms.db"
}

---

🎨 Frontend Setup

cd bdms-client
npm install
ng serve

apiUrl: 'https://localhost:5001/api'

---

🔑 Default Admin

- Email: admin@test.com
- Password: Admin@123

---

🚀 Future Work

- Smart donor matching (rule-based → ML-assisted)
- Full Redis caching strategy
- Email notifications
- Advanced analytics dashboard
- Migration to PostgreSQL / Azure SQL

---

💼 What This Project Demonstrates

- Full-stack system design
- Clean architecture implementation
- Real-time & caching integration
- Production debugging experience
- End-to-end ownership

---

