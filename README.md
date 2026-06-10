## APC Nexus – Enterprise Membership & Financial Management System

### Overview

APC Nexus is a full-featured enterprise desktop application designed to manage membership operations, 
financial contributions, attendance tracking, fines, events, and reporting for organizations and associations.

The system is built using Clean Architecture, providing clear separation of concerns, maintainability, scalability, and improved testability.

It is built using:

- C# (.NET Framework)
- Windows Forms (WinForms)
- Entity Framework
- SQL Server
- LINQ


### 🔐 Authentication

Secure login system for authorized users.

![Login](Images/image1.PNG)

### 📊 Dashboard

Real-time overview of:

- Registered members
- Attendance statistics
- Dues raised
- Expenses
- Financial summaries

### 👥 Membership Management

Manage:

- Active members
- Former members
- Inactive members
- Deceased members
- Birthdays
- Contacts
- Commitments





### Architecture

The application follows the Clean Architecture pattern:

````
APC
│
├── AllForms            → Presentation Layer (WinForms)
│   
├── Application 
│   ├── DTO             → Data Transfer Objects
│   ├── Interfaces      → Contracts
│   └── Services        → Application Business Logic
│
└── Domain ()
|   ├── Entities        → Core Domain Models
|
├── Infrastruture 
│   ├── Data            → DbContext / EDMX
│   ├── Repositories    → Data Access Implementations
│ 
  
````


### 🧰 Setup Instructions

#### 1. Requirements

- Visual Studio 2022
- .NET Framework
- SQL Server
- Entity Framework installed

- #### 2. Clone Repository

git clone https://github.com/isoboye24/APC_APP_.git

#### 3. Configure Database

Update connection string in App.config

#### 4. Run Project

- Set APC (UI) as startup project
- Press F5

### 🏢 Project Type

Enterprise Desktop Application
Association / Organization Management System

👨‍💻 Author

Developed as a professional enterprise-grade membership management platform using structured architecture and clean separation of concerns.

📜 License

Private / Organizational Use