# Guide of Turkey - Admin Panel

An ASP.NET Core web application for managing content of the Guide of Turkey platform. This admin panel provides a comprehensive interface for administrators to manage locations, photos, and user data across Turkey.

## Features

- **User Authentication & Authorization**
  - Secure login system with phone number and password
  - Cookie-based authentication
  - Admin-only access control

- **Content Management**
  - Countries management
  - Cities management
  - Districts management
  - Places management
  - Type categories management
  - Photo galleries for each location
  - User comments moderation

- **Media Management**
  - Photo upload functionality
  - Gallery management for countries, cities, districts, and places
  - Automatic file naming and organization

- **Homepage Features**
  - Control homepage visibility for cities
  - Manage slider content
  - Toggle display status for different elements

## Technical Stack

- **Backend Framework**: ASP.NET Core
- **Database**: Entity Framework Core
- **Authentication**: Custom cookie-based authentication
- **Password Security**: BCrypt for password hashing
- **File Management**: Built-in file handling for photos

## Project Structure

- `Controllers/`: Contains the application's controllers
  - `HomeController.cs`: Main controller handling all admin operations
- `Models/`: Data models and database entities
- `ViewModels/`: View-specific models
- `Views/`: Application views and templates
- `wwwroot/`: Static files including uploaded photos
  - `countryPhotos/`
  - `cityPhotos/`
  - `districtPhotos/`
  - `placePhotos/`
  - `sliderPhotos/`

## Setup Requirements

1. .NET Core SDK
2. SQL Server
3. Visual Studio or preferred IDE
4. Required NuGet packages:
   - Microsoft.AspNetCore.Mvc
   - Microsoft.EntityFrameworkCore
   - BCrypt.Net
   - Newtonsoft.Json

## Configuration

1. Update the connection string in `appsettings.json`
2. Ensure all required directories exist in `wwwroot/` for photo storage
3. Run database migrations
4. Create an initial admin account

## Security Features

- Password hashing using BCrypt
- Token-based session management
- Secure file upload handling
- Admin-only route protection

## API Endpoints

The admin panel provides various endpoints for managing content:

### Authentication
- GET/POST `/`: Login page
- GET `/logout`: Logout

### Content Management
- GET `/home`: Admin dashboard
- GET/POST `/users`: User management
- GET/POST `/sliders`: Slider management
- GET/POST `/types`: Place types management
- GET/POST `/cities`: Cities management
- GET/POST `/countries`: Countries management
- GET/POST `/districts`: Districts management
- GET/POST `/places`: Places management
- GET/POST `/comments`: Comment management
