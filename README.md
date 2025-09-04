# Ale Ink

Ale Ink is a work in progress project.

# Ale Ink – Note Management App  

**Ale Ink** is a Blazor web application for creating and organizing notes, with the ability to assign them to **items, people, or places**. Notes and their relationships are stored in a relational database, making it easy to track and retrieve information across multiple contexts.  

## How to run this app
1. Clone the repository

Click the green code button at the top of this page and copy the URL into Visual Studio to clone the repository.

3. Restore NuGet packages

Right-click on the solution in Solution Explorer → Restore NuGet Packages.

Ensure all projects build successfully.

3. Configure multiple startup projects

Right-click the solution → Properties.

In the Startup Project section, select Multiple startup projects.

Set the Action column for:

Ale_Ink.API → Start

Ale_Ink → Start

Click Apply → OK.

4. Run the application

Press F5 or click Start.

Both the API and Blazor app will launch.

The Blazor app should open in your browser automatically.

## Key Features  
- Create, edit, and delete notes with real-time form validation.  
- Assign notes to entities (items, people, places); new entities are created automatically when needed.  
- Prevents duplicate assignments to maintain clean data.  
- Interactive UI with **MudBlazor** modals, tables, and form components.  

## Tech Stack  
- **Frontend**: Blazor Server + MudBlazor  
- **Validation**: FluentValidation integrated with Blazor forms  
- **Backend**: ASP.NET Core Web API  
- **Database**: Entity Framework Core with SQLite/SQL Server  
- **Architecture**: Separation of concerns with dedicated services for API logic and Blazor HTTP communication  

