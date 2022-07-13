### Table of contents
1. [Introduction and task](#introduction-and-task)
2. [System design](#system-design)
    1. [Project structure](#project-structure)
    2. [ERD diagrams](#erd-diagrams)
        1. [Offer database design](#offer-database-design)
        2. [Account database design](#account-database-design)
3. [Development progress](#development-progress-white_check_mark)
    1. [Phase 1 :white_check_mark:](#phase-1-white_check_mark)
    2. [Phase 2 :white_check_mark:](#phase-2-white_check_mark)
    3. [Phase 3 :hammer_and_wrench:](#phase-3-hammer_and_wrench)
4. [Resources](#resources)

Looking for the up-to-date (**13/07/2022**) [production preview](#production-preview) video?

## Introduction and Task
Elanco wants to explore the use of cloud-based cognitive services to complete text analysis on receipts to improve our customer experiences.

We believe this can be achieved by accelerating our rebates process, which today requires our customers to complete multiple forms incorporating details from multiple
receipts/products. 

Using Azure Cognitive Services, build a web-based application which represents the use of image-to-text to extract and display key details from product images and receipts.
Elanco will provide sample images of test receipts/products and we’d like to see which useful data we can programmatically extract and display in a web interface to validate this approach to streamlining the customer experience.

## System Design
### Project Structure

| Folder                | Description                                                                                   |
| -------------         |-------------                                                                                  |
| ElancoUI              | Holds the user interface pages, user interface models and classes.                            |
| ElancoUI.Tests        | Holds the unit testing of the ElancoUI classes.                                               |
| ElancoLibrary         | Holds the business logic of the application. Acts as a layer between the UI and Data Access.  |
| ElancoLibrary.Tests   | Holds the unit testing and mocking of the ElancoLibrary classes.                              |
| ElancoData            | Holds the tables, stored procedures and data publisher for the products/offers.               |
| Documentation         | Holds the example receipts, products logos, UML diagrams and user interface mockups.          |

### ERD Diagrams
#### Offer Database Design
An offer can contain multiple brand logos and multiple offer details. Brands can contain multiple products.
<img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/Wireframes/OfferDbV2.png" />

#### Account Database Design
An account is associated to a user which can contain multiple addresses and pets. The account entity userId is associated with the Entity Framework Core Database.
<img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/Wireframes/AccountDb.png" />

## Development Progress :white_check_mark:
#### Production preview
![Alt Text](https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/DevelopmentPreview.gif)

### Phase 1 :white_check_mark:
* Initialised GitHub repository for the project
* Planned out the project using ERD's, user interface diagrams and established goals of the project.
* Created necessary .NET Core projects, including:
    * UI (Blazor Server)
    * UI Library (business logic for UI)
    * Data project (houses tables and stored procedures)
* Created the MVP (minimum viable product) and achieved a working application, allowing users to:
    * Manually enter required form fields
    * Upload a receipt/invoice which auto-fills selected form fields
    * Auto-save form via local storage
    * Select an offer to claim
    * Submit the form

### Phase 2 :white_check_mark:
Received new requirements from the client.
* Created a login/register (Entity Framework) system to authenticate users
* Created a registered user's account page to auto-fill into form fields, containing:
    * Personal information
    * Pet information
* Created rebates section on user's account page to display submitted rebates
* Ability for the system to auto-select an offer based on an uploaded image
* Implemented logging
* Implemented unit testing and mocking

### Phase 3 :white_check_mark:
Received new requirements from the client, and requirement changes.
* Clean up work, including:
  * Fixed bugs
  * Ironed out issues of the system
  * Added account pet and address validation
* Hosted web application on Azure
* Setup CI/CD pipeline using Azure DevOps
* Migrated from SQLServer to Azure SQL

### Project Conclusion

After the 3 phases/sprints of development, the project meets its requirements provided by the client. A live demo of the application was showcased to the client, where we re-irrerated the choice of technlogies used, and why we made certain decisions. the client had the ability to ask any questions during the demo. 

Ultimately, the client was happy final product as well as with the technologies used for this application. A few weeks later, the client asked if a non-technical recorded demo could be made of the application, as client's upper management will watch.

As the client is apart of a very large company, a vast change to the customer experience of submitting rebates, developed by students, is not feasible. This will need to be authorised by upper management.

:exclamation: **Important update for Elanco Rebates Project**

The live webpage for the application has been halted as of **13/07/2022**, this is due to the incurring charges and costs to maintain the working web application. The incurring charges are from resources such as Azure SQL server & connected databases and the Azure form recognizer API.

If you wish to see the **Production preview 13/07/2022**, please see [this link](#production-preview). 

## Resources
Below are resources and documentation links to aid with the development.

* [Blazor forms and validation](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-6.0#handle-form-submission)
* [Blazor file uploads](https://docs.microsoft.com/en-us/aspnet/core/blazor/file-uploads?view=aspnetcore-6.0&pivots=server#upload-files-to-a-server)
* [Blazor performance best practices](https://docs.microsoft.com/en-us/aspnet/core/blazor/performance?view=aspnetcore-6.0)
* [Blazor State Management](https://docs.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-6.0&pivots=server#aspnet-core-protected-browser-storage)
* [Blazor Image Streaming](https://docs.microsoft.com/en-us/aspnet/core/blazor/images?view=aspnetcore-6.0#streaming-examples)
* [Razor Pages CRUD with EF Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/crud?view=aspnetcore-6.0)
* [Migrating from SQL Server to Azure SQL](https://docs.microsoft.com/en-us/azure/dms/tutorial-sql-server-to-azure-sql)
* [Azure Blob Storage in .NET](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=environment-variable-windows)
* [Ignoring Accented Letters in strings](https://stackoverflow.com/questions/359827/ignoring-accented-letters-in-string-comparison)