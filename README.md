# OpenDiscussionPlatform

A discussion platform web application built with ASP.NET MVC and Entity Framework. This platform allows users to create categories, post discussion subjects, and engage in conversations through replies.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Installation & Setup](#installation--setup)
- [Authentication & Authorization](#authentication--authorization)
- [Data Models](#data-models)
- [API Endpoints](#api-endpoints)
- [Usage Examples](#usage-examples)
- [Deployment](#deployment)

## Overview

OpenDiscussionPlatform is a web-based discussion forum that provides:
- **Hierarchical discussion structure**: Categories → Subjects → Replies
- **Role-based access control**: Admin, Moderator, and User roles
- **Rich content support**: Text discussions with image attachments
- **Search functionality**: Find relevant discussions across subjects and replies
- **User management**: Comprehensive user profiles and account management

## Features

### Core Functionality
- ✅ **Categories Management**: Organize discussions into categories
- ✅ **Subject Creation**: Create discussion topics with optional images
- ✅ **Reply System**: Threaded replies to discussion subjects
- ✅ **Search**: Full-text search across subjects and replies
- ✅ **Image Upload**: Attach images to discussion subjects
- ✅ **Pagination**: Efficient browsing of large discussion lists

### User Management
- ✅ **Authentication**: Secure login/logout with ASP.NET Identity
- ✅ **User Profiles**: Extended user information (FirstName, LastName, AboutMe)
- ✅ **Role-based Permissions**: Three-tier permission system
- ✅ **Account Management**: Password changes, profile updates

### Administrative Features
- ✅ **User Administration**: Admin can manage all users and roles
- ✅ **Content Moderation**: Moderators can move subjects between categories
- ✅ **Category Management**: Admins can create, edit, and delete categories

## Installation & Setup

### Prerequisites
- .NET Framework 4.5+
- SQL Server (LocalDB supported)
- Visual Studio 2015+ or Visual Studio Code
- IIS Express (for development)

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/DanNimara/OpenDiscussionPlatform.git
   cd OpenDiscussionPlatform
   ```

2. **Database Configuration**
   - Update the connection string in `web.config`
   - The application uses Entity Framework Code First with automatic migrations
   - Default connection string: `DefaultConnection`

3. **Build and Run**
   ```bash
   # Build the solution
   dotnet build
   
   # Run the application
   dotnet run
   ```

4. **Initial Setup**
   - The application automatically creates default roles (Admin, Moderator, User)
   - Default admin account: `admin@gmail.com` / `!1Admin`

## Authentication & Authorization

### Roles and Permissions

| Role | Permissions |
|------|-------------|
| **Admin** | • Full system access<br>• User management<br>• Category CRUD operations<br>• Content moderation<br>• Delete any content |
| **Moderator** | • Move subjects between categories<br>• Delete any subject or reply<br>• Standard user permissions |
| **User** | • Create subjects and replies<br>• Edit own content<br>• View all public content |

### Authentication Flow

1. **Registration**: New users are automatically assigned the "User" role
2. **Login**: Uses ASP.NET Identity with cookie authentication
3. **Authorization**: Controller actions are protected with `[Authorize]` attributes
4. **Role Management**: Admins can modify user roles through the Users controller

## Data Models

### Core Entities

#### Category
```csharp
public class Category
{
    public int CategoryID { get; set; }
    public string Name { get; set; }           // Max 25 characters
    public string Description { get; set; }
    public virtual ICollection<Subject> Subjects { get; set; }
}
```

#### Subject
```csharp
public class Subject
{
    public int SubjectID { get; set; }
    public string Title { get; set; }          // Max 100 characters, required
    public string Content { get; set; }        // Required
    public byte[] Image { get; set; }          // Optional image attachment
    public DateTime Date { get; set; }
    public int CategoryID { get; set; }        // Foreign key
    public string UserID { get; set; }         // Foreign key
    
    public virtual Category Category { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Reply> Replies { get; set; }
}
```

#### Reply
```csharp
public class Reply
{
    public int ReplyID { get; set; }
    public string Content { get; set; }       // Required
    public DateTime Date { get; set; }
    public int SubjectID { get; set; }        // Foreign key
    public string UserID { get; set; }        // Foreign key
    
    public virtual Subject Subject { get; set; }
    public virtual ApplicationUser User { get; set; }
}
```

#### ApplicationUser (Extended Identity User)
```csharp
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }     // Max 100 characters
    public string LastName { get; set; }      // Max 100 characters
    public string AboutMe { get; set; }       // Max 500 characters
    public DateTime RegisterDate { get; set; }
    public IEnumerable<SelectListItem> AllRoles { get; set; }
}
```

### Entity Relationships
```
Category (1) ─── (Many) Subject (1) ─── (Many) Reply
                    │                      │
                    └── User              └── User
```

## API Endpoints

### Categories Controller

| Method | Endpoint | Description | Auth Required | Roles |
|--------|----------|-------------|---------------|-------|
| GET | `/Categories` | List all categories with pagination | No | Public |
| GET | `/Categories/Show/{id}` | Get category with subjects (sorted, paginated) | No | Public |
| GET | `/Categories/New` | Show create category form | Yes | Admin |
| POST | `/Categories/New` | Create new category | Yes | Admin |
| GET | `/Categories/Edit/{id}` | Show edit category form | Yes | Admin |
| PUT | `/Categories/Edit/{id}` | Update category | Yes | Admin |
| DELETE | `/Categories/Delete/{id}` | Delete category | Yes | Admin |

**Category Show Sorting Options:**
- `dateAsc` - Subjects by date ascending
- `dateDesc` - Subjects by date descending  
- `titleAsc` - Subjects by title ascending
- `titleDesc` - Subjects by title descending

### Subjects Controller

| Method | Endpoint | Description | Auth Required | Roles |
|--------|----------|-------------|---------------|-------|
| GET | `/Subjects` | Search subjects with pagination | No | Public |
| GET | `/Subjects/Show/{id}` | Get subject with replies | No | Public |
| POST | `/Subjects/Show/{id}` | Add reply to subject | Yes | User, Moderator, Admin |
| GET | `/Subjects/ShowImage/{id}` | Get subject image | No | Public |
| GET | `/Subjects/New/{categoryId}` | Show create subject form | Yes | User, Moderator, Admin |
| POST | `/Subjects/New` | Create new subject | Yes | User, Moderator, Admin |
| GET | `/Subjects/Edit/{id}` | Show edit subject form | Yes | Owner, Moderator, Admin |
| PUT | `/Subjects/Edit/{id}` | Update subject | Yes | Owner, Moderator, Admin |
| GET | `/Subjects/EditCategory/{id}` | Show move subject form | Yes | Moderator, Admin |
| PUT | `/Subjects/EditCategory/{id}` | Move subject to different category | Yes | Moderator, Admin |
| DELETE | `/Subjects/Delete/{id}` | Delete subject | Yes | Owner, Moderator, Admin |

**Subject Search Parameters:**
- `search` - Search string (searches title, content, and replies)
- `page` - Page number for pagination

**Reply Sorting Options:**
- `dateAsc` - Replies by date ascending (default)
- `dateDesc` - Replies by date descending

### Replies Controller

| Method | Endpoint | Description | Auth Required | Roles |
|--------|----------|-------------|---------------|-------|
| GET | `/Replies/Edit/{id}` | Show edit reply form | Yes | Owner, Moderator, Admin |
| PUT | `/Replies/Edit/{id}` | Update reply | Yes | Owner, Moderator, Admin |
| DELETE | `/Replies/Delete/{id}` | Delete reply | Yes | Owner, Moderator, Admin |

### Users Controller (Admin Only)

| Method | Endpoint | Description | Auth Required | Roles |
|--------|----------|-------------|---------------|-------|
| GET | `/Users` | List all users | Yes | Admin |
| GET | `/Users/Show/{id}` | Get user details | Yes | Admin |
| GET | `/Users/Edit/{id}` | Show edit user form | Yes | Admin |
| PUT | `/Users/Edit/{id}` | Update user (including role changes) | Yes | Admin |
| DELETE | `/Users/Delete/{id}` | Delete user | Yes | Admin |

### Account Controller

| Method | Endpoint | Description | Auth Required | Roles |
|--------|----------|-------------|---------------|-------|
| GET | `/Account/Login` | Show login form | No | Public |
| POST | `/Account/Login` | User login | No | Public |
| GET | `/Account/Register` | Show registration form | No | Public |
| POST | `/Account/Register` | User registration | No | Public |
| POST | `/Account/LogOff` | User logout | Yes | Any |
| GET | `/Account/Show/{id}` | View user profile | No | Public |
| GET | `/Account/Edit/{id}` | Show edit profile form | Yes | Owner |
| PUT | `/Account/Edit/{id}` | Update user profile | Yes | Owner |

### Manage Controller (User Account Management)

| Method | Endpoint | Description | Auth Required | Roles |
|--------|----------|-------------|---------------|-------|
| GET | `/Manage` | Account management dashboard | Yes | Any |
| GET | `/Manage/ChangePassword` | Show change password form | Yes | Any |
| POST | `/Manage/ChangePassword` | Change password | Yes | Any |
| GET | `/Manage/SetPassword` | Show set password form (for external logins) | Yes | Any |
| POST | `/Manage/SetPassword` | Set password | Yes | Any |
| POST | `/Manage/RemoveLogin` | Remove external login | Yes | Any |
| GET | `/Manage/AddPhoneNumber` | Show add phone form | Yes | Any |
| POST | `/Manage/AddPhoneNumber` | Add phone number | Yes | Any |
| POST | `/Manage/RemovePhoneNumber` | Remove phone number | Yes | Any |

## Usage Examples

### Creating a Discussion Flow

1. **Create a Category** (Admin only)
   ```http
   POST /Categories/New
   Content-Type: application/x-www-form-urlencoded
   
   Name=General+Discussion&Description=General+topics+for+discussion
   ```

2. **Create a Subject**
   ```http
   POST /Subjects/New
   Content-Type: multipart/form-data
   
   Title=Welcome+to+the+platform&Content=This+is+our+first+discussion&CategoryID=1&ImageData=[file]
   ```

3. **Add a Reply**
   ```http
   POST /Subjects/Show/1
   Content-Type: application/x-www-form-urlencoded
   
   Content=Thanks+for+creating+this+platform!&SubjectID=1
   ```

### Search Discussions
```http
GET /Subjects?search=platform+discussion&page=1
```

### User Registration and Login

1. **Register**
   ```http
   POST /Account/Register
   Content-Type: application/x-www-form-urlencoded
   
   Email=user@example.com&Password=MyPassword123!&ConfirmPassword=MyPassword123!&UserName=user@example.com
   ```

2. **Login**
   ```http
   POST /Account/Login
   Content-Type: application/x-www-form-urlencoded
   
   Email=user@example.com&Password=MyPassword123!&RememberMe=false
   ```

### Admin Operations

1. **Change User Role**
   ```http
   PUT /Users/Edit/user-id
   Content-Type: application/x-www-form-urlencoded
   
   Email=user@example.com&NewRole=2  // Role ID for Moderator
   ```

2. **Move Subject Between Categories**
   ```http
   PUT /Subjects/EditCategory/1
   Content-Type: application/x-www-form-urlencoded
   
   CategoryID=3
   ```

## Deployment

### Production Deployment

1. **Web.config Configuration**
   ```xml
   <connectionStrings>
     <add name="DefaultConnection" 
          connectionString="Server=production-server;Database=OpenDiscussionPlatform;Integrated Security=true;" 
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

2. **IIS Setup**
   - Deploy to IIS 7.5+
   - Ensure .NET Framework 4.5+ is installed
   - Configure application pool for integrated mode

3. **Database Migration**
   - Entity Framework will automatically create/update database schema
   - Default admin account will be created on first run

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `DefaultConnection` | Database connection string | LocalDB connection |
| `ASPNET_ENV` | Environment (Development/Production) | Development |

### Security Considerations

- Change default admin password immediately
- Use HTTPS in production
- Configure appropriate CORS policies
- Regular security updates for dependencies
- Implement proper input validation
- Use secure connection strings

---

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project was created for the Web Application Development Course at the Faculty of Mathematics and Informatics.

---

**Note**: This documentation covers the current API structure. For any additional features or modifications, please refer to the controller implementations and update this documentation accordingly.
