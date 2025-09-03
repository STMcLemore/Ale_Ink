# Ale Ink

Ale Ink is a work in progress project.

Ale Ink – Note Management App

Ale Ink is a Blazor web application for creating and organizing notes, with the ability to assign them to items, people, or places. Notes and their relationships are stored in a relational database, making it easy to track and retrieve information across multiple contexts.

Key Features

Create, edit, and delete notes with real-time form validation.

Assign notes to entities (items, people, places); new entities are created automatically when needed.

Prevents duplicate assignments to maintain clean data.

Interactive UI with MudBlazor modals, tables, and form components.

Tech Stack

Frontend: Blazor Server + MudBlazor

Validation: FluentValidation integrated with Blazor forms

Backend: ASP.NET Core Web API

Database: Entity Framework Core with SQLite/SQL Server

Architecture: Separation of concerns with dedicated services for API logic and Blazor HTTP communication
