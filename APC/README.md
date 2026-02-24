## APC Nexus – Enterprise Membership & Financial Management System

### Overview

APC Nexus is a full-featured enterprise desktop application designed to manage membership operations, 
financial contributions, attendance tracking, fines, events, and reporting for organizations and associations.

The system provides a structured 3-Layer Architecture (UI – BLL – DAL) ensuring maintainability, scalability, 
and clean separation of concerns.

It is built using:

- C# (.NET Framework)
- Windows Forms (WinForms)
- Entity Framework
- SQL Server

### Architecture

The application follows a structured enterprise architecture:

APC
│
├── UI Layer (WinForms)
│   ├── Forms
│   ├── Controls
│   └── User Interaction Logic
│
├── BLL (Business Logic Layer)
│   ├── Validation
│   ├── Business Rules
│   └── DTO Handling
│
└── DAL (Data Access Layer)
    ├── Entity Framework Context
    ├── DAO Classes
    ├── LINQ Queries
    └── Database Operations


### Design Patterns Used

- DTO Pattern
- DAO Pattern
- Layered Architecture
- Repository-like Data Handling
- LINQ-based Querying
- Soft Delete Strategy

### Core Features

#### 🔐 Authentication & Access Control

- Secure login system
- Role-based permissions
- Dynamic access level management
- Member credential auto-generation

#### 👥 Membership Management

- Add / Edit / View / Delete Members
- Profile image upload & storage
- Membership status tracking:
    - Current
    - Former
    - Inactive
    - Deceased
- Birthday tracking
- Nationality statistics
- Gender distribution analytics
- 3-Month Absentee detection

#### 📅 Meeting Management

- Monthly meeting tracking
- Attendance monitoring:
    - Present
    - Absent
- Monthly dues tracking
- Dues paid vs expected
- Absentee detection logic (last 3 meetings)



