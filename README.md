# 🎫 HelpDesk - Enterprise Ticket Management System

<div align="center">

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-7.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-07405E?style=for-the-badge&logo=sqlite&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

**A modern, full-featured help desk ticketing system built with ASP.NET Core MVC**

[Features](#-features) • [Technologies](#-technologies) • [Architecture](#-architecture) • [Installation](#-installation) • [Screenshots](#-screenshots)

</div>

---

## 📋 Overview

HelpDesk is a comprehensive enterprise-grade ticket management system designed to streamline customer support operations. Built with ASP.NET Core 7.0, it provides a robust platform for managing support tickets, user roles, product categories, and FAQs with an intuitive interface and powerful admin capabilities.

I used the Brevo email marketing service for my project, but I had to leave the "Username" and "Password" fields in the appsettings.Development.json file blank. However, the system is usable with the admin email address and password.

Admin Email: boraoker42@gmail.com

Admin Password: Admin_123

&nbsp;

## ✨ Features

### 🎟️ Ticket Management
- **Create & Track Tickets** - Users can submit detailed support tickets with file attachments
- **Product-Based Categorization** - Organize tickets by product and category
- **Assignment System** - Assign tickets to specific support staff
- **My Tickets View** - Users can track their submitted tickets in real-time
- **Email Notifications** - Automated email notifications for ticket updates

### 👥 User Management
- **Role-Based Access Control** - Admin, Support Staff, and User roles
- **User Registration & Authentication** - Secure login with ASP.NET Core Identity
- **Email Confirmation** - Verify user accounts via email
- **User Administration** - Full CRUD operations for user management

### 🛡️ Security & Authentication
- **ASP.NET Core Identity** - Industry-standard authentication and authorization
- **Role-Based Authorization** - Granular permissions for different user types
- **Password Policies** - Configurable password requirements
- **Account Lockout** - Protection against brute force attacks
- **CSRF Protection** - Built-in anti-forgery tokens

### 📦 Product & Category Management
- **Product Catalog** - Manage products requiring support
- **Dynamic Categories** - Create categories linked to specific products
- **AJAX-Powered UI** - Seamless category selection based on product

### ❓ FAQ System
- **Knowledge Base** - Create and manage FAQs for self-service support
- **Product-Specific FAQs** - Link FAQs to relevant products
- **Easy Content Management** - Admin interface for FAQ CRUD operations

### 📊 Dashboard & Analytics
- **Admin Dashboard** - Overview of all tickets and system statistics
- **Ticket Counting** - Real-time ticket count display
- **Data Visualization** - Comprehensive ticket listing with related data

&nbsp;

## 🚀 Technologies

### Backend
- **ASP.NET Core 7.0** - Modern web framework
- **Entity Framework Core 7.0** - ORM for database operations
- **ASP.NET Core Identity** - Authentication & authorization framework
- **SQLite** - Lightweight embedded database

### Frontend
- **Razor Pages** - Server-side rendering
- **Bootstrap 5** - Responsive UI framework
- **jQuery** - Dynamic client-side interactions
- **AJAX** - Asynchronous data loading

### Architecture & Patterns
- **MVC Pattern** - Model-View-Controller architecture
- **Repository Pattern** - Data access abstraction
- **Dependency Injection** - Built-in IoC container
- **Entity Framework Migrations** - Database version control

&nbsp;

## 🏗️ Architecture

### Project Structure

```
HelpDesk/
├── Controllers/          # MVC Controllers
│   ├── AccountController.cs       # Authentication & registration
│   ├── DashboardController.cs     # Admin dashboard
│   ├── TicketController.cs        # Ticket management
│   ├── UserController.cs          # User administration
│   ├── RolesController.cs         # Role management
│   ├── ProductController.cs       # Product management
│   ├── ProductCategoryController.cs
│   └── FAQController.cs           # FAQ management
├── Models/              # Domain Models
│   ├── Ticket.cs                  # Ticket entity
│   ├── Product.cs                 # Product entity
│   ├── ProductCategory.cs         # Category entity
│   ├── FAQ.cs                     # FAQ entity
│   ├── AppUser.cs                 # Custom Identity user
│   ├── AppRole.cs                 # Custom Identity role
│   ├── DataContext.cs             # Main DB context
│   ├── IdentityContext.cs         # Identity DB context
│   └── SmtpEmailSender.cs         # Email service
├── ViewModels/          # Data Transfer Objects
│   ├── TicketViewModel.cs
│   ├── CreateUserViewModel.cs
│   ├── EditUserViewModel.cs
│   ├── LoginViewModel.cs
│   ├── ProductCategoryViewModel.cs
│   └── FAQViewModel.cs
├── Views/               # Razor Views
│   ├── Account/                   # Login, Register, Confirm
│   ├── Dashboard/                 # Admin dashboard
│   ├── Ticket/                    # Ticket views
│   ├── User/                      # User management
│   ├── Roles/                     # Role management
│   ├── Product/                   # Product views
│   ├── ProductCategory/           # Category views
│   ├── FAQ/                       # FAQ views
│   └── Shared/                    # Shared layouts & partials
├── Migrations/          # EF Core Migrations
└── wwwroot/             # Static files (CSS, JS, Images)
```

### Database Schema

The application uses two separate database contexts:
- **DataContext** - Application data (Tickets, Products, Categories, FAQs)
- **IdentityContext** - User authentication and authorization

**Key Relationships:**
- Tickets → Products (Many-to-One)
- Tickets → ProductCategories (Many-to-One)
- Tickets → Users (Many-to-One - Assignment)
- ProductCategories → Products (Many-to-One)
- FAQs → Products (Many-to-One)

&nbsp;

## 🔧 Installation

### Prerequisites
- .NET 7.0 SDK or later
- Visual Studio 2022 / VS Code / JetBrains Rider
- SQLite (included with .NET)

### Setup Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/HelpDesk.git
   cd HelpDesk
   ```

2. **Configure Email Settings**
   
   Update `appsettings.json` or `appsettings.Development.json`:
   ```json
   {
     "EmailSender": {
       "Host": "smtp.gmail.com",
       "Port": 587,
       "EnableSSL": true,
       "Username": "your-email@gmail.com",
       "Password": "your-app-password"
     }
   }
   ```

3. **Apply Database Migrations**
   ```bash
   dotnet ef database update --context DataContext
   dotnet ef database update --context IdentityContext
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```

5. **Access the Application**
   - Navigate to `https://localhost:5001` or `http://localhost:5000`
   - Default admin credentials are seeded via `SeedData.cs`

&nbsp;

## 🎨 Key Features Showcase

### 🔐 Authentication System
- Custom registration with email confirmation
- Secure login with lockout protection
- Role-based access control (Admin, User)

### 🎫 Ticket Workflow
1. User submits a ticket with product selection
2. System dynamically loads relevant categories via AJAX
3. User provides problem summary and detailed description
4. Optional file attachment support
5. Admin reviews and assigns ticket to support staff
6. Email notifications sent to relevant parties

### 📧 Email Integration
- SMTP email sender implementation
- Configurable email settings
- Email confirmation for new registrations
- Ticket notification system

### 🎯 Admin Capabilities
- Full CRUD operations on all entities
- User and role management
- Dashboard with ticket statistics
- Product and category management
- FAQ content management

&nbsp;

## 📸 Screenshots

### Home Page
![Home Page](path/to/your/screenshot.png)
&nbsp;

&nbsp;

&nbsp;

### Ticket Creation
![Ticket Creation](path/to/your/screenshot.png)
&nbsp;

&nbsp;

&nbsp;

### Admin Dashboard
![Admin Dashboard](path/to/your/screenshot.png)
&nbsp;

&nbsp;

&nbsp;

---

## 🔑 Default Credentials

The application seeds test data on first run. Check `Models/SeedData.cs` for default credentials.

&nbsp;

## 🛠️ Configuration

### Identity Options
The application has flexible identity configuration in `Program.cs`:
- Password requirements (length, complexity)
- Email confirmation requirements
- Account lockout settings
- Cookie authentication settings

### Database
- Uses SQLite for easy deployment and portability
- Connection string configured in `appsettings.json`
- Can be easily switched to SQL Server or PostgreSQL

&nbsp;

## 📝 Code Highlights

### Clean Architecture
- Separation of concerns with MVC pattern
- Dependency injection throughout
- ViewModels for data transfer
- Tag helpers for reusable UI components

### Security Best Practices
- Anti-forgery token validation
- Role-based authorization attributes
- Parameterized queries via EF Core
- Secure password hashing

### Modern ASP.NET Core Features
- Minimal hosting model
- Scoped service lifetimes
- Configuration system
- Built-in logging

&nbsp;

## 🎓 Learning Outcomes

This project demonstrates proficiency in:
- ✅ ASP.NET Core MVC development
- ✅ Entity Framework Core & database design
- ✅ ASP.NET Core Identity & security
- ✅ RESTful API design patterns
- ✅ Responsive web design with Bootstrap
- ✅ AJAX & dynamic client-side interactions
- ✅ Email integration & SMTP configuration
- ✅ File upload & storage management
- ✅ Role-based authorization
- ✅ Database migrations & version control

&nbsp;

## 🚧 Future Enhancements

- [ ] Real-time notifications with SignalR
- [ ] Advanced ticket filtering and search
- [ ] Ticket priority levels
- [ ] SLA (Service Level Agreement) tracking
- [ ] Reporting and analytics dashboard
- [ ] Multi-language support
- [ ] Mobile app integration
- [ ] API endpoints for third-party integration
- [ ] File preview capabilities
- [ ] Ticket commenting system

&nbsp;

## 📄 License

This project is available for portfolio and educational purposes.

&nbsp;

## 👤 Author

**Bora Öker**
- GitHub: [@BoraOker](https://github.com/BoraOker)
- LinkedIn: [Bora Öker](https://www.linkedin.com/in/bora-%C3%B6ker-98685425b/)
- Email: boraoker.dev@gmail.com
&nbsp;

---

<div align="center">


</div>
