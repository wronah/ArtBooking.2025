# ArtBooking.2025

Contains ArtBooking demo application for "Backend applications programming" university course. Building application starts from simple web api project and advances to layered monolith application with possibility to use in microservices architecture.

## LAB #1 - creating dotnet projects

**prerequisites:**
1. `DotNet SDK v.8` installed - https://dotnet.microsoft.com/en-us/download/dotnet/8.0
2. `Visual Studio Code` as code editor - https://code.visualstudio.com/docs/languages/dotnet
3. `C# Dev Kit` extension for VS Code - https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit

**dotnet CLI** (command line iterface)
https://learn.microsoft.com/en-us/dotnet/core/tools/

> `dotnet -v`
> Displays the version of the installed .NET SDK.

> `dotnet new --help`
> Shows help information for the dotnet new command, listing available templates and usage details.

> `dotnet new {template} -n {project-name}`
> Creates a new project based on the specified template, with the given project name.

**Various types of web projects**
> `dotnet new mvc -n MVC_demo`
> Generates a new ASP.NET Core MVC project in MVC_demo folder.

> `dotnet new webapi -n WebApi_demo`
> Creates a new ASP.NET Core Web API (minimal api) project in WebApi_demo folder.

> `dotnet new webapi --help`
> Displays help information for the dotnet new webapi template, showing available options.

> `dotnet new webapi --controllers`
> Creates a Web API project with controllers enabled.

**Running the application**
> dotnet run

Debugging With Visual Studio Code:
1. In sidebar open the "Run and Debug" panel (Ctrl+Shift+D).
2. In the "Run and Debug" panel, click blue button "Run and Debug".
3. Top select menu should popup.
4. Pick C# project configuration.
5. In the next steps choose default options.
6. In the END: Once confiugred you can start debugging picking configuration from pull down directly in "Run and Debug", and start with "green arrow".

## LAB #2 - building business model and endpoints

**prerequisites:**
1. Ensure you have Git CLI installed [link](https://git-scm.com/downloads)
2. Clone `git clone https://github.com/true-vue/ArtBooking.2025.git` or fork this repo.
3. Switch to `lab#2.a` branch: `git checkout `lab#2.a`.

**Data model design**
1. Application data model is located within separate project `Business.Model`
2. Model consists of classes that represent fundamental business objects (like: Organization, User, Event, Ticker, Price and so one...) within [application domain](https://www.sciencedirect.com/topics/computer-science/application-domain).
3. Those classes are called `Entites` and are located within folder with that exact name [link](https://github.com/true-vue/ArtBooking.2025/tree/lab%232.a/Business.Model/Entities).

**Database interop**
1. App requires place for storing its data. We will use SQL type database [link](https://www.w3schools.com/sql/default.asp).
2. To operate between application and actual database `Entity Framework` will be used [link](https://learn.microsoft.com/en-us/ef/).
3. First ArtBooking specific [DbContext](https://www.learnentityframeworkcore.com/dbcontext) needs to be configured [link](https://github.com/true-vue/ArtBooking.2025/blob/lab%232.a/Business.Model/Data/ArtBookingDbContext.cs).
4. Then we have to register application `ArtBookingDbContext` for dependecy inject container (Services). For purpose of demonstation will use InMemory database [link](https://github.com/true-vue/ArtBooking.2025/blob/e57c703eb33d69cec16af4cea94601fd10b3e442/Backend/Program.cs#L13C5-L13C94).

**Enpoints - controller methods**
1. ArtBooking Backend application will expose Web API over [endpoints](https://www.ibm.com/think/topics/api-endpoint)
2. To build endpoints, Controller pattern is used [link](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio).
3. Initially single controller for single Entity type will be created, but as application grows controller might not have direct mapping to single Entity [link](https://github.com/true-vue/ArtBooking.2025/blob/lab%232.a/Backend/Controllers/ArtOrganizationController.cs).
4. `ArtBookingDbContext` configured in eariler steps can be used with [Dependency Injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) pattern within Controller [link](https://github.com/true-vue/ArtBooking.2025/blob/e57c703eb33d69cec16af4cea94601fd10b3e442/Backend/Controllers/ArtOrganizationController.cs#L11).
5. Endpoints are represented by particular Controller methods [CreateOrganization](https://github.com/true-vue/ArtBooking.2025/blob/e57c703eb33d69cec16af4cea94601fd10b3e442/Backend/Controllers/ArtOrganizationController.cs#L17) and [GetOrganization](https://github.com/true-vue/ArtBooking.2025/blob/e57c703eb33d69cec16af4cea94601fd10b3e442/Backend/Controllers/ArtOrganizationController.cs#L38C42-L38C57)
