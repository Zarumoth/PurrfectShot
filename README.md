# 📸 PurrfectShot

> A modern family cat photo archive and monthly ranking system designed to organize and celebrate our feline companions.

![.NET Version](https://img.shields.io/badge/.NET-8.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-blue)
![Architecture](https://img.shields.io/badge/Architecture-N--Tier-orange)
![Database](https://img.shields.io/badge/Database-SQL_Server-red)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📋 Table of Contents

- [About the Project](#-about-the-project)
- [Technologies Used](#-technologies-used)
- [Prerequisites](#-prerequisites)
- [Getting Started](#-getting-started-plug--play)
- [Project Structure](#-project-structure)
- [Features](#-features)
- [Usage](#-usage)
- [Test Account Credentials](#-test-account-credentials)
- [Contributing](#-contributing)
- [Contact](#-contact)

---

## 📖 About the Project

**Purrfect Shot** is a specialized web platform built for cat-loving households. It solves the problem of scattered family photos by organizing them into a structured monthly calendar. Users can create detailed profiles for their cats, upload memories, and vote for the "Photo of the Month", which automatically becomes the calendar cover.

The application serves as a demonstration of advanced ASP.NET Core concepts, including **N-Tier architecture**, **Secure Identity integration**, and **Custom Data Logic (Soft Delete/Archive)**.

---

## 🛠️ Technologies Used

| Technology            | Version  | Purpose                          |
|-----------------------|----------|----------------------------------|
| ASP.NET Core MVC      | 8.0      | Main Web framework               |
| Entity Framework Core | 8.0      | ORM / Database access            |
| SQL Server            | 2022     | Database (running via Docker)    |
| ASP.NET Identity	    | 8.0	   | Authentication & Authorization   |
| Bootstrap             | 5.3      | Frontend styling                 |
| Razor Pages / Views   | -        | Server-side HTML rendering       |
| jQuery Validation	    | -	       | Client-side form validation      |

---

## ✅ Prerequisites

Before running the project, ensure you have the following:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (**Recommended** for the easiest setup))
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) (optional for manual setup)
- [Git](https://git-scm.com/)
- [Optional: SQL Server](https://www.microsoft.com/en-us/sql-server) or SQLite (if preferred)

---

## 🚀 Getting Started (Plug & Play)

This project is configured for **automatic setup**. You don't need to manually run migrations or configure SQL Server if you have Docker.  
If you still want to manually setup things, refer to *Option B: Visual Studio 2022* outlined below.

### 1. Clone and Run

```bash
git clone https://github.com/Zarumoth/PurrfectShot.git
cd PurrfectShot
```
### 2. Run The application

You can choose between two methods:

#### Option A: Docker Compose (Recommended)

1. Open your terminal in the project root folder.
2. Run:

```bash
docker-compose up --build
```

3. Wait for the build to finish and the containers to start.
4. Open your browser at `http://localhost:8080`

**Note**: If port 8080 is in use, you can modify `docker-compose.yaml`.

#### Option B: Visual Studio 2022

1. Open `PurrfectShot.sln` in **Visual Studio 2022**.
2. Ensure your Docker Desktop is running (or wherever you are running SQL).
3. Update `appsettings.json` and ensure the connection string matches your Docker/LocalDB setup:

**Example for Docker (Default)**:
```JSON
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=PurrfectShotDb;User Id=sa;Password=SoftUniTestDb1423!;TrustServerCertificate=True;MultipleActiveResultSets=true"
}
```  

**Example for LocalDB / SQL Express**:
```JSON
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PurrfectShotDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

4. Press **F5** (Start Debugging).
	- The app will automatically connect to the SQL Server container.
	- Database will be created and seeded automatically.
	- App launches at `https://localhost:7194`.

**Note**: If port 7194 is not available, Visual Studio might assign a different port. Check the browser URL.

---

## 📁 Project Structure

The solution follows a clean 6-layer **N-Tier architecture**:

```
PurrfectShot/
│
├── PurrfectShot.Web/              # Presentation Layer (Controllers & Views)
├── PurrfectShot.Services.Data/    # Business Logic and Interfaces
├── PurrfectShot.Web.ViewModels/   # InputModels and ViewModels
├── PurrfectShot.Data/             # Data Access (DbContext, Configs, Migrations)
├── PurrfectShot.Data.Models/      # Domain Entities (ApplicationUser, Cat, Photo, Vote)
└── PurrfectShot.Common/           # Shared Constants and Validation helpers
```

---

## ✨ Features

- **N-Tier Architecture** Complete separation of concerns for scalability.  
- **Identity System** Fully localized (Bulgarian) ASP.NET Core Identity.  
- **Smart Archive** "Soft delete" cat profiles while preserving photo history.  
- **Photo Management** Upload/Delete with physical file cleanup on the server.  
- **Dynamic Calendar** Automatic monthly grouping with Bulgarian localization.  
- **Voting System** Seamless "Upsert" voting logic with "Unvote" capability.  
- **User Favorites** Many-to-Many relationship for personal collections.  
- **Security** GUID-based identifiers for photos to prevent ID scraping.  
- **Comprehensive Seeding** Ready-to-use data for 4 cats and 36 photos.  

---

## 💻 Usage

### 🏠 Explore & Navigate

- **Home Page**: Check the "Hero" section for global stats and browse the "Pride" section to see all active cat residents.
- **The Band (Cats)**: Visit the full list of cats via the "Бандата" (The Band) link in the navigation.

![home-page](docs/screenshots/home-page.jpeg)

### 🗓️ Monthly Calendar

- Navigate to **Calendar**.
- Browse the archive by month/year covers.
- Click on a specific month to view the full gallery of uploaded photos, sorted by rating.

### 🐈 Profiles & Photos

- **Cat Details**: Click on any cat card to view their bio, breed info, and personal photo album.
- **Photo Details**: Click on any photo to see it in full size. Here you can see the rating, upload date, and navigation buttons.

![cat-details-page](docs/screenshots/profile-details.jpeg)
![photo-details-page](docs/screenshots/photo-details.jpeg)

### 🗳️ Interaction (Login Required)

- **Register/Login**: Create an account to unlock interactive features.
- **Vote**: Rate photos from 1 to 5 stars. You can change your vote anytime (Seamless Upsert).
- **Favorites**: Click the Heart icon ❤️ on a photo to add it to your personal "Favorites" collection.
- **My Photos**: View all photos uploaded by you in your personal dashboard.

### ⚙️ Management (CRUD)

- **Add Cat**: Use the "Add Cat" button to introduce a new member to the household.
- **Upload Photo**: Upload new memories via the "Upload" button. You can assign the photo to a specific cat.
- **Edit/Delete**:
	- Manage cat profiles (Edit details or Archive them).
	- Manage photos (Edit captions or Delete permanently).
	- Note: Archiving a cat hides it from the main lists but preserves its photos in history.

---

### 🧪 Test Account Credentials

To test administrative features, use the seeded account:
- **Email**: admin@purrfect.com
- **Password**: Admin123!

**Note**: Currently, the system allows all registered users to perform CRUD operations (Family mode).

---

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new branch: `git checkout -b feature/AmazingFeature`
3. Commit your changes: `git commit -m "Add some AmazingFeature"`
4. Push to the branch: `git push origin feature/AmazingFeature*`
5. Open a Pull Request.

---

## 📄 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE.txt) file for details.

---

## 📬 Contact

**Zarumoth** – [@Zarumoth](https://github.com/Zarumoth)

Project Link: [https://github.com/Zarumoth/PurrfectShot](https://github.com/Zarumoth/PurrfectShot)

---

*Built as part of the **ASP.NET Fundamentals** course.*