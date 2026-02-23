# ClassClaud
-- This is a schoolassignment --

In this assignment, I will develop a system that manages data storage for an educational company. 
My main focus is on database management and backend development in C# and .NET, but the system will also include a frontend that makes it possible to interact with the backend API. 
The frontend part is not the primary focus of the assessment; it mainly serves as a tool to test and demonstrate the functionality of the system. You can find the fontend part on following link, https://github.com/pandy89/ClassCloudFontend

# Use of AI
It is permitted to use AI-based tools as support in the development process, both for the frontend and for testing. 
However, it is important that I personally understand and can stand behind the code that is submitted. I am responsible for ensuring that the code functions correctly.
We also had instructor-led lessons during which parts of the code were created.

# Technologis and topics
 - C#
 - ASP.NET Core Minimal API
 - Entity Framework Core (EF Core) by Code First-principles
 - DDD(Domain-Driven-Design) and Clean Architecture
 - CRUD(Create-Read-Update-Delete)
 - Microsoft SQL Server
 - Swagger
 - React
 - Vite

# How to start the project 
I have used a local database so you need to create some data to the database with exampel Swagger, https://localhost:XXXX/swagger/index.html
or you can do that with the frontend. 

## Back-end
1. Open your Visual Studio and then open the terminal to clone the project. (https://github.com/pandy89/datalagring-maria-magnell.git)
2. Open appsettings.json.
3. Ensure that the ConnectionString points to your local SQL Server instance.
4. Open the terminal and run Update-Database to migrate the tabels to your local server.
5. When you got your database structured, run the API application.
6. You can create some data with swagger och with the frontend.

## Front-end
1. Open you Visual Code and the open the termmail to clone the project. (https://github.com/pandy89/ClassCloudFontend.git)
2. Make sure that the bankend API application is running.
3. Start the fontend by type "npm run dev" in the terminal.
4. Click the link in teminal (http:localhost:5173) to start the application.
