# Bug Ticketing System API

A complete solution for tracking and managing software bugs with user authentication, project organization, and file attachments.

## System Overview

The Bug Ticketing System API provides a comprehensive solution for software development teams to track, manage, and resolve bugs throughout the development lifecycle.

## Project Structure

```
BugTicketingSystem/
│
├── PresentationLayer/
│   ├── Controllers/
│   │   ├── AttachmentController.cs
│   │   ├── BugController.cs
│   │   ├── ProjectController.cs
│   │   ├── UserBugController.cs
│   │   └── UserController.cs
│
├── BusinessLayer/
│   ├── Managers/
│   │   ├── AttachmentManager.cs
│   │   ├── BugManager.cs
│   │   ├── ProjectManager.cs
│   │   ├── UserBugManager.cs
│   │   └── UserManager.cs
│   │
│   ├── DTOs/
│   │   ├── AttachmentDtos/
│   │   │   ├── AttachmentDto.cs
│   │   │   └── DownloadAttachmentDto.cs
│   │   │
│   │   ├── BugDtos/
│   │   │   ├── AddBugDto.cs
│   │   │   ├── BugDto.cs
│   │   │   └── UpdateBugDto.cs
│   │   │
│   │   ├── Common/
│   │   │   ├── GeneralResult.cs
│   │   │   └── TokenDto.cs
│   │   │
│   │   ├── ProjectDtos/
│   │   │   ├── AddProjectDto.cs
│   │   │   ├── ProjectDto.cs
│   │   │   └── UpdatedProjectDto.cs
│   │   │
│   │   ├── UserBugDtos/
│   │   │   └── UserBugDto.cs
│   │   │
│   │   └── UserDtos/
│   │       ├── UserDto.cs
│   │       ├── UserLogDto.cs
│   │       └── UserRegDto.cs
│   │
│   └── BusinessExtensions.cs
│
├── DataAccessLayer/
│   ├── Context/
│   │   ├── BTSDbContext.cs
│   │   └── Configurations/
│   │       ├── BugConfig.cs
│   │       └── UserConfig.cs
│   │
│   ├── Models/
│   │   ├── Attachment.cs
│   │   ├── Bug.cs
│   │   ├── Project.cs
│   │   ├── Role.cs
│   │   ├── User.cs
│   │   └── User_Bug.cs
│   │
│   ├── Repositories/
│   │   ├── AttachmentRepository.cs
│   │   ├── BugRepository.cs
│   │   ├── GeneraicRepository.cs
│   │   ├── IGenaricRepository.cs
│   │   ├── ProjectRepository.cs
│   │   ├── UserBugRepository.cs
│   │   └── UserRepository.cs
│   │
│   ├── Migrations/
│   └── DataAccessExtensions.cs
│
│   └── Uploads/
│
├── appsettings.json
└── Program.cs
```

### Core Features

| Feature | Description |
|---------|-------------|
| **User Authentication** | Secure JWT-based registration and login with role-based access control |
| **Project Management** | Organize bugs by projects with full CRUD operations |
| **Bug Tracking** | Complete CRUD operations with status and priority tracking |
| **File Attachments** | Upload, download, and manage files related to bugs |
| **User Assignments** | Assign team members to specific bugs for accountability |

## Architecture

### System Layers

| Layer | Components | Description |
|-------|------------|-------------|
| **Presentation Layer** | - REST API controllers<br>- JWT authentication middleware<br>- Error handling middleware | Exposes functionality via HTTP endpoints and handles cross-cutting concerns |
| **Business Layer** | - Entity managers<br>- DTOs for data transfer<br>- Authentication services | Implements core business logic and data transformation |
| **Data Access Layer** | - Entity Framework Core<br>- Repository pattern<br>- Data seeding<br>- Database migrations | Handles database interactions and data persistence |

## API Reference

### Authentication

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/User/Register` | Register new user | No |
| POST | `/api/User/Login` | User login | No |

### Users

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/User` | Get all users | Yes |

### Projects

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/Project` | Get all projects | Yes |
| POST | `/api/Project` | Create project | Yes |
| GET | `/api/Project/{id}` | Get project by ID | Yes |
| PUT | `/api/Project/{id}` | Update project | Yes |
| DELETE | `/api/Project/{id}` | Delete project | Yes |

### Bugs

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/Bug` | Get all bugs | Yes |
| POST | `/api/Bug` | Create bug | Yes |
| GET | `/api/Bug/{id}` | Get bug by ID | Yes |
| PUT | `/api/Bug/{id}` | Update bug | Yes |
| DELETE | `/api/Bug/{id}` | Delete bug | Yes |

### Assignments

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/bugs/{bugId}/assignees/{userId}` | Assign user to bug | Yes |
| DELETE | `/api/bugs/{bugId}/assignees/{userId}` | Remove user from bug | Yes |

### Attachments

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/Attachment?bugId={bugId}` | Upload attachment | Yes |
| GET | `/api/Attachment?bugId={bugId}` | Get bug attachments | Yes |
| GET | `/api/Attachment/{attachmentId}/download?bugId={bugId}` | Download attachment | Yes |
| DELETE | `/api/Attachment/{attachmentId}?bugId={bugId}` | Delete attachment | Yes |

## Authentication Flow

1. Register user at `/api/User/Register`
2. Login at `/api/User/Login` to get JWT token
3. Include token in subsequent requests:
   ```
   Authorization: Bearer {your_token}
   ```

## Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=BTS_DB;Trusted_Connection=True;TrustServerCertificate=true;"
  },
  "Jwt": {
    "SecretKey": "YourSecureKeyHere",
    "Issuer": "BugTicketingSystem",
    "Audience": "BugTicketingSystemClient",
    "ExpiryInMinutes": 10080
  }
}
```

## Getting Started

### Prerequisites
| Requirement | Version |
|-------------|---------|
| .NET SDK | 6.0 or higher |
| SQL Server | 2019 or higher |
| Visual Studio | 2022 (recommended) |

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/BugTicketingSystem.git
   ```

2. Navigate to the project directory:
   ```bash
   cd BugTicketingSystem
   ```

3. Update connection string in `appsettings.json` to match your environment.

4. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

6. Access the API at `https://localhost:5001` or `http://localhost:5000`

## Initial Data

The system is pre-seeded with the following data:

| Entity | Details |
|--------|---------|
| User Roles | Manager, Developer, Tester |
| Users | Sample users for each role |
| Projects | Initial demonstration projects |
| Bugs | Sample bugs with various statuses and priorities |
| Attachments | Test attachments for bugs |

## API Usage Examples

### User Registration
```http
POST /api/User/Register
Content-Type: application/json

{
  "username": "developer1",
  "email": "dev@example.com",
  "password": "DevPassword123!",
  "roleIds": [2]
}
```

### Bug Creation
```http
POST /api/Bug
Content-Type: application/json
Authorization: Bearer {your_token}

{
  "title": "Login page broken",
  "description": "Login button not responding",
  "status": "Open",
  "priority": "High",
  "projectId": 1
}
```


## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## Contact

Project Maintainer - [your-email@example.com](mailto:Mohamedabdelwahabali5@gmail.com)

Project Link: [https://github.com/yourusername/BugTicketingSystem](https://github.com/mohamedabdelwahab5/BugTicketingSystem)