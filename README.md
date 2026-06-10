## APC Nexus – Enterprise Membership & Financial Management System

## Overview

APC Nexus is a full-featured enterprise desktop application designed to manage membership operations, 
financial contributions, attendance tracking, fines, events, and reporting for organizations and associations.

The system is built using Clean Architecture, providing clear separation of concerns, maintainability, scalability, and improved testability.

It is built using:

- C# (.NET Framework)
- Windows Forms (WinForms)
- Entity Framework
- SQL Server
- LINQ
- Clean Architecture

### ## Production Usage

APC Nexus has been actively used in a real community organization for over one year. The system supports day-to-day operations including membership administration, attendance tracking, financial contributions, event management, and reporting.

Its continued use in a live environment demonstrates the reliability, practicality, and maintainability of the application.


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

### 📈 Financial Reports

Generate:

- Financial summaries
- Expenditure reports
- Yearly breakdowns
- Contribution statistics


![Login](Images/image4.PNG)

### 👤 Member Profile

Detailed member information including:

- Personal information
- Attendance history
- Contributions
- Fines
- Special contributions

![Login](Images/image2.PNG)

### 📅 Meeting Management

Track:

- Monthly attendance
- Present and absent members
- Monthly dues
- Expected contributions
- Meeting summaries

### 🎉 Event Management

Manage:

- Event sales
- Expenditures
- Receipts
- Event images
- Event balances

![Login](Images/image3.PNG)

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

## Layer Responsibilities

### Presentation Layer

- Windows Forms UI
- User interaction
- Input validation

### Application Layer
- Business rules
- DTOs
- Service interfaces
- Application services

### Domain Layer
- Core entities
- Business models

### Infrastructure Layer
- Entity Framework
- Database access
- Repository implementations

## Design Patterns
- Clean Architecture
- Repository Pattern
- Service Layer Pattern
- DTO Pattern
- Dependency Injection
- Soft Delete Pattern

## Core Features

### 🔐 Authentication & Permissions
- Login system
- Role-based access control
- Permission management

### 👥 Membership Management

- Add/Edit/Delete members
- Birthday tracking
- Status management
- Profile images

### 📅 Attendance Management

- Present/Absent tracking
- Attendance history
- Three-month absentee detection

### 💰 Financial Management

- Monthly dues
- Fines
- Special contributions
- Balance calculations

### 🎉 Event Management
- Event sales
- Expenditures
- Receipts
- Image storage

### 📊 Reporting
- Financial reports
- Attendance statistics
- Member analytics

## ⚙ Settings

Manage:

- Countries
- Positions
- Professions
- Employment statuses
- Membership statuses
- Permissions
- Nationalities
- Marital Statuses


### 🧰 Setup Instructions

#### 1. Requirements

- Visual Studio 2022
- .NET Framework
- SQL Server
- Entity Framework installed

- #### 2. Clone Repository

git clone https://github.com/isoboye24/APC_APP_.git

#### 3. Restore NuGet Packages

Open the solution in Visual Studio and restore all NuGet packages if necessary.

#### 4. Configure Database

Update the connection string in:

App.config

to point to your SQL Server instance.

#### 5. Run Project

- Set APC (UI) as startup project
- Press F5 to build and run the application.

### 🏢 Project Type

ERP System for Community Organizations

Designed for:

Community groups
Non-profit organizations

👨‍💻 Author

Isoboye Vincent Dan-Obu

Professional membership and financial management system developed using Clean Architecture principles.

📜 License

Private / Organizational Use