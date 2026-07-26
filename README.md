# Party RaidR

![Status](https://img.shields.io/badge/Status-In--Development-yellow?style=for-the-badge&logo=codepen)

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Vue.js](https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D)
![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)

![License](https://img.shields.io/badge/license-MIT-blue.svg)

## 📄 Description

Party RaidR (or Party Raider) is a web- and mobile application that gives users an easy solution for finding leasure activities.

Once registered, the user can post new events such as parties, gatherings, concerts etc. Other users can apply to these activities.

This is a really great option for finding activities in our free time, wether it be outdoors or indoors.

## 🛠️ Setup Instructions

If you want to try the current state of the app, follow these steps:

### 📋 Prerequisites

 + **General:**
    - [Docker Desktop](https://www.docker.com/products/docker-desktop/)
    - [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
    - [Node 24.9.0](https://nodejs.org/en/download)
    - [Visual Studio](https://visualstudio.microsoft.com/vs/)
 + **Mobile:**
    - Android SDK & Emulator - Testing on Android. On Windows 11, enabling Hyper-V is recommended.
    - iOS SDK (macOS only) - For testing on iOS Xcode is needed.

### ⚙️ Setup & Installation Guide

Clone project repository and enter project folder:

```bash
git clone https://github.com/toth-mate/party-raidr.git
cd party-raidr/src
```

Create a ```.env``` file in ```src/``` with the following content:

```
DB_ROOT_PASSWORD=YOUR_PASSWORD
DB_USER=YOUR_USER
DB_PASSWORD=YOUR_PASSWORD
DB_NAME=YOUR_DB_NAME
DB_PORT=YOUR_DB_PORT
BACKEND_PORT=8080
FRONTEND_PORT=5173
```

Navigate to ```src/PartyRaidR.Backend```, and create ```appsettings.json``` based on the provided sample. Note that your **API key needs to be at least 32 characters long.**

There is also an NGINX container that acts as a reverse-proxy and API-gateway. It makes the API and the web application accessible from the `party.test` URL. For it to work, you will need to make this URL point to your own device.

**Edit your hosts file**:
```bash
> nano /etc/hosts
```
**Add this line at the end:**
```
127.0.0.1 party.test
```

**Run Docker container:**
```bash
> docker compose up --build
```

This will setup the MySQL database and the API. The backend API documentation can be accessed through *```http://party.test/swagger```* by default.

The backend runs a DB seeder, so some sample data is ready for you to play around with.

###### Running MAUI:

+ Open ```src/PartyRaidR.sln``` in Visual Studio.
+ Set ```PartyRaidR.Mobile``` as the *Startup project.*
+ Select the target platform *(Android Emulator or Windows Machine)* and hit F5.

###### Running Web:

If you want to try the Vue app, you will need to create a ```.env``` file in ```party-raidr/src/PartyRaidR.Web/src/```. Add this line:

```VITE_API_URL=http://party.test/api```

Docker also starts the web app, so after running ```docker-compose,``` you will be able to access it in your web browser on ```http://party.test```.

## 💻 Tech Stack

+ Frontend: Vue JS & MAUI
+ Backend: .NET 9 (C#)
+ Database: MySQL
+ Infrastructure: Docker - The whole development environment and the database are containerized for portability and consistent running.

## 📝 Version Control
Since **3 April 2026** *Conventional Commits* are used in the project (including Issues, Pull Requests and branches).
