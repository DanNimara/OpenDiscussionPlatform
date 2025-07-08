# OpenDiscussionPlatform

A web-based discussion forum platform developed as part of the Web Application Development Course at the Faculty of Mathematics and Informatics. The platform allows users to create and participate in structured discussions organized by categories and topics.

## Features

### Core Functionality
- **Category Management**: Organize discussions into different categories
- **Discussion Subjects**: Create discussion topics within categories with optional image uploads
- **Reply System**: Users can reply to discussion subjects
- **Sorting Options**: Sort discussions by date or title (ascending/descending)

### User Management
- **User Registration & Authentication**: Secure user account creation and login
- **Role-Based Access Control**: Three user roles with different permissions:
  - **Admin**: Full platform management capabilities
  - **Moderator**: Content moderation privileges
  - **User**: Standard discussion participation
- **User Profiles**: Extended user information including name and about me section

### Security Features
- ASP.NET Identity integration for secure authentication
- Role-based authorization for different platform features
- User can only edit their own replies and subjects

## Technology Stack

- **Framework**: ASP.NET MVC (.NET Framework)
- **Database**: Entity Framework with SQL Server
- **Authentication**: ASP.NET Identity
- **Frontend**: Razor Views with HTML, CSS, JavaScript
- **Architecture**: Model-View-Controller (MVC) pattern

## Prerequisites

Before running this application, ensure you have:

- Visual Studio 2017 or later (or Visual Studio Code with C# extension)
- .NET Framework 4.5 or later
- SQL Server or SQL Server Express
- IIS Express (included with Visual Studio)

## Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/DanNimara/OpenDiscussionPlatform.git
   cd OpenDiscussionPlatform
   ```

2. **Database Setup**
   - The application uses Entity Framework Code First approach
   - Connection strings should be configured in `Web.config`
   - Database and tables will be created automatically on first run
   - Initial data seeding includes default admin user and roles

3. **Build and Run**
   - Open the project in Visual Studio
   - Restore NuGet packages (if not automatically restored)
   - Build the solution (Build → Build Solution)
   - Run the application (F5 or Debug → Start Debugging)

4. **First Time Setup**
   - The application will automatically create the database schema
   - Default admin account will be created:
     - **Email**: admin@gmail.com
     - **Password**: !1Admin

## Default User Accounts

The system automatically creates the following on first startup:

| Role | Email | Password | Permissions |
|------|--------|----------|-------------|
| Admin | admin@gmail.com | !1Admin | Full platform access |

New users can register through the registration page and will be assigned the "User" role by default.

## Project Structure

```
OpenDiscussionPlatform/
├── Controllers/           # MVC Controllers
│   ├── AccountController.cs      # User authentication
│   ├── CategoriesController.cs   # Category management
│   ├── SubjectsController.cs     # Discussion subjects
│   ├── RepliesController.cs      # Reply management
│   ├── UsersController.cs        # User management
│   └── HomeController.cs         # Home page
├── Models/               # Data models and ViewModels
│   ├── Category.cs              # Category entity
│   ├── Subject.cs               # Discussion subject entity
│   ├── Reply.cs                 # Reply entity
│   ├── IdentityModels.cs        # User and database context
│   └── AccountViewModels.cs     # Authentication ViewModels
├── Views/                # Razor view templates
│   ├── Categories/              # Category views
│   ├── Subjects/                # Subject views
│   ├── Replies/                 # Reply views
│   ├── Account/                 # Authentication views
│   └── Shared/                  # Shared layouts and partials
└── Startup.cs            # Application configuration
```

## Usage

1. **Access the Platform**: Navigate to the application URL (typically `http://localhost:xxxx`)
2. **Login**: Use the default admin credentials or register a new account
3. **Create Categories**: Admins can create discussion categories
4. **Start Discussions**: Users can create new discussion subjects in any category
5. **Participate**: Reply to existing discussions and engage with the community

## Database Models

### Category
- **CategoryID**: Unique identifier
- **Name**: Category name (max 25 characters)
- **Description**: Optional category description

### Subject
- **SubjectID**: Unique identifier
- **Title**: Discussion title (max 100 characters)
- **Content**: Discussion content
- **Image**: Optional image upload
- **Date**: Creation timestamp
- **CategoryID**: Associated category
- **UserID**: Creator's user ID

### Reply
- **ReplyID**: Unique identifier
- **Content**: Reply content
- **Date**: Creation timestamp
- **SubjectID**: Associated discussion subject
- **UserID**: Author's user ID

## Contributing

This project was developed for educational purposes as part of a Web Application Development course. If you're a student or instructor working with this codebase:

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## License

This project is developed for educational purposes at the Faculty of Mathematics and Informatics.
