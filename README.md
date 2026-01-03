# Party RaidR

![Status](https://img.shields.io/badge/Status-In--Development-yellow?style=for-the-badge&logo=codepen)

![License](https://img.shields.io/badge/license-MIT-blue.svg)

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Vue.js](https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D)
![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)

## 📈 Motivation

Party RaidR is a pet project of mine. I started work during my first semester.

I started working on this project for hobby and learning purposes. I use technologies that I like while also trying to deepen my knowledge.

## 📄 Description

Party RaidR (or Party Raider) is a web- and mobile application that gives users an easy solution for finding leasure activities.

Once registered, the user can post new events such as parties, gatherings, concerts etc. Other users can apply to these activities.

This is a really great option for finding activities in our free time, wether it be outdoors or indoors.

### ✨ Features

There are two types of users. Basic users, and administrators. Basic users have access to a number of features.

After authentication with an email and a password, they can:
+ Create new places,
+ Post new events,
+ Edit, manage and delete their existing events,
+ Apply to events.

To post a new event, the user has to give the event a title, a description, a place and a category. If the user wants to post an event at a place that does not exist in the database yet, one needs to create it by providing the name, the city, the address, a category (pub, public space etc.), as well as coordinates. Of course, it is not required to set the coordinates manually. A map is provided, on which the user can browse the place. Furthermore, the use of an API is on my plan list, which would help the user look for the place by address.

Also, visitors of the website (or the mobile app) can browse events, even in a filtered list. Clicking on an event in the list would provide the user with further information about the event in a pop-up panel.

I am also planning to make a *random event* feature, which provides the user with a random event near their location.

Administrators would have the ability to:
+ Everything regular users do,
+ Moderation: Edit and delete events posted by other users,
+ Edit user data.

A moderation-panel is provided for admin users.

## 🚀 Future

As mentioned above, the app is still under development. Therefore, I plan on concentrating on my current goals, which I defined in a document. Though, I do have some plans on how I could make the app even better.

First of all, I would like to heavily build on community, as it can spread the popularity of the app. To make it happen, I would like to introduce a system using which users can keep contact. I imagined something like "friendship", or following others.

Nextly, categories. Currently, there are a few categories for places and events, and they are stored as database models with a couple of set values. However, I would like users to be able to add their own categories. It would not be a huge challenge to carry out at all. However, at the moment I do not have any idea what I would like it to look like. I can not let people to just create new categories any time they want. I could set up a system where users can post their new category ideas and administrators would need to accept is. Nevertheless, after some time it would require a lot of unnecessary effort from the moderation team, as of course I do not want a lot of categories for users to choose from.

## 🧱 Project structure

Party RaidR consists of a **Backend project** and two **Frontend projects.**

### Backend

Backend is responsible for the data. It handles operations initiated by users.

During production I follow common conventions among .NET developers. So the backend can be broken down into basic responsibilities:

+ **Repository** - Communication with the database | CRUD operations,
+ **Service** - Business logic,
+ **Controllers** - API endpoints.

It is basically an API built with an ASP.NET project. It handles authentication and authorization very well.

### Frontend

#### 🌐 Web

A soon-to-be website built using Vue JS. I have a good amount of experience with this framework, but I plan on further expanding my knowledge during production.

I do not have experience with using maps on the frontend, so I am very excited to learn how to do it right.

#### 📱 Mobile

The other part of the frontend is a mobile application. It was the main idea. Although I have no experience with mobile technologies at all, I am very determined and I already have a lot of ideas. The project is going to be made using .NET MAUI.

Branch guide (prefixes):
+ F - *Feature*
+ B - *Bugfix*
+ C - *Chore*
