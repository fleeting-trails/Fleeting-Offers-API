# Project Name

## Overview

This project is designed to **gamify the advertisement experience** by rewarding users with points based on their savings. Users can engage in quests and challenges while discovering great deals. The application follows a structured architecture to ensure scalability, maintainability, and efficiency.

## Features

* **Gamified advertisement engagement**
* **Savings-based point system**
* **Quests and challenges to encourage purchases**
* **Admin panel for managing advertisements**
* **User dashboard for tracking rewards and purchases**
* **Secure authentication system**

## Tech Stack

### **Backend**

* **C# / .NET** (API & Business Logic)
* **Entity Framework Core** (Database ORM)
* **SQL Server / PostgreSQL** (Database)
* **Redis** (Caching)
* **JWT Authentication** (User authentication & authorization)

### **Frontend**

* **React / Next.js** (User & Admin Panel)
* **Ant Design** (UI Components)
* **TypeScript** (Frontend Logic)

### **Infrastructure**

* **Docker** (Containerization)
* **Azure / AWS** (Cloud Hosting)
* **CI/CD Pipelines** (Automated Deployment)

## Project Structure

The project follows a structured hierarchy to ensure clarity and maintainability.&#x20;

📂 project-root
&#x20;├── 📂 Modules          # Business logic components (e.g., Auth, Advertisements)
&#x20;├── 📂 Providers        # Service providers (Database, Email, Cache, etc.)
&#x20;├── 📂 Settings         # Global configurations
&#x20;├── 📂 Migrations       # Database schema migrations
&#x20;├── 📂 HTTP             # Request helpers (Middlewares, Filters, Validators)
&#x20;├── 📂 Common          # Shared types, enums, constants
&#x20;├── 📂 Utils           # Utility functions
&#x20;├── 📂 AdminApp        # Admin Panel (React/Next.js)
&#x20;├── 📂 UserApp         # User Interface (React/Next.js)
&#x20;├── 📂 Tests           # Unit & Integration Tests
&#x20;├── 📜 README.md       # Project Overview
&#x20;├── 📜 .gitignore      # Git ignored files
&#x20;└── 📜 docker-compose.yml # Docker configuration


## Installation

### **Prerequisites**

* .NET SDK
* Node.js & npm
* Docker (optional)

### **Setup Steps**

1. **Clone the repository:**
   git clone [https://github.com/fleeting-trails/Fleeting-Offers-API.git](https://github.com/fleeting-trails/Fleeting-Offers-API.git)
   cd project-name

2. **Set up environment variables:**
   Reach out to [abtahitajwar@gmail.com](mailto\:abtahitajwar@gmail.com) for user-secrets
3. **Install backend dependencies:**
   cd backend
   dotnet restore

4. **Provide scripts to necessary permissions**
   **`chmod +x reset-migration.sh && chmod +x run.sh`**
5. **Redis installation**
   Setup redis server and provide the connection string into appsettings.json
6. **Run the backend:**
   ./run.sh


## Contributing

1. Fork the repository
2. Create a new branch: `git checkout -b feature-branch`
3. Make changes and commit: `git commit -m 'Add feature'`
4. Push the changes: `git push origin feature-branch`
5. Submit a pull request
