# ASGWebApp-ASP ![Azure DevOps builds](https://img.shields.io/azure-devops/build/ryanchristian4427/41e401ef-1612-4af8-9903-694ad7fe606a/4.svg)

This project is a rewrite of my ASGWebApp that was a client project from university. For that project, we met with a client who requested a web server and a relational database to replace his previous booking and progress system that was handled in Excel. The project we built was written in Spring Boot using MySQL as the database system. However, I had wanted to try out other technologies, and redoing a project was the simplest solution.

Here I am using ASP.NET Core as the framework, and PostgreSQL as the database. In addition, this project is being written with as a RESTful API (somehting I've never done before) so that it can be integrated with a mobile app (undecided as to whether it should be plain Android or Xamarin) and consumed with a Vue.js SPA. 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
.Net Core 2.2
PostgreSQL
```

### Running

To run the web server on localhost, you will need a local PostgreSQL Database and .Net Core 2.2 installed. I have my PostgreSQL DB running on port 5432 using the credentials user: postgres password: admin, though these settings can be tweaked in ~/Persistence/Configuration/DatabaseConnectionHandler.cs.

With a DB running, navigate to ~/Web and run

```
dotnet run
```

and then to ~/Web/Vue 

```
npm install
npm run serve
```

Navigate to https://localhost:8080/login in your browser of choice to begin using the system. As of now, the content is quite limited as I continue to learn how to really use APIs, SPAs, different tools like Webpack and Axios, and lite languages like SASS and Typescript.

## Authors

* **Ryan Christian** - *Entire Project* - [Ryan Christian](https://github.com/RyanChristian4427)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Thanks to Luke for setting up a template for me to learn and build from. Super grateful.
