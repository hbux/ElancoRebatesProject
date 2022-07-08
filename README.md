### Table of contents
1. [Introduction and task](#introduction-and-task)
2. [System design](#system-design)
    1. [Project structure](#project-structure)
    2. [UML diagrams](#uml-diagrams)
        1. [Offer database design](#offer-database-design)
        2. [Account database design](#account-database-design)
3. [Development progress](#development-progress)
    1. [Phase 1](#phase-1-white_check_mark)
    2. [Phase 2](#phase-2-white_check_mark)
    3. [Phase 3](#phase-3-hammer_and_wrench)
4. [Resources](#resources)

Looking for the up-to-date [development preview](#development-preview) video?

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

### UML Diagrams
#### Offer Database Design
An offer can contain multiple brand logos and multiple offer details. Brands can contain multiple products.
<img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/Wireframes/OfferDbV2.png" />

#### Account Database Design
An account is associated to a user which can contain multiple addresses and pets. The account entity userId is associated with the Entity Framework Core Database.
<img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/Wireframes/AccountDb.png" />

## Development Progress
#### Development preview
![Alt Text](https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/DevelopmentPreview.gif)

### Phase 1 :white_check_mark:
* Initialised GitHub repository for the project
* Planned out the project using UML's, user interface diagrams and established goals of the project.
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

### Phase 3 :hammer_and_wrench:
* Clean up work, including:
  * Fixing current bugs
  * Ironing out issues of the system
  * More complete more unit tests and mocking
  * Refactoring auto-select offer to detect more accurately
  * Adding account pet and address validation
  * Adding login loading indicator
* Adding SendGrid as email sender
* Hosting web application on Azure
* Migrating from SQLServer to Azure SQL
* Adding more user interfaces, such as:
  * Scaffolded pages
  * Information card modal
  * MyRebates account section error modals

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

---

:exclamation: **Important**

Below is appsettings.json code which is not included in the Elanco project files as this contains API key's and endpoints for API services. This json file should be included in the root of your ElancoUI project.
 
````
{
    "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=aspnet-ElancoUI-CDA36F0B-4463-4A16-9EDF-38D3BBF5DB5F;Trusted_Connection=True;MultipleActiveResultSets=true",
    "ElancoData": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ElancoData;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",

  // Api Key is the key to access Azure Form Recognizer API.
  "ApiKey": "your-api-key",

  // Endpoint is the URL of the Form Recognizer.
  "Endpoint": "your-form-recognizer-endpoint",

  // Model ID is the custom trained model for retrieving specific details from an uploaded image.
  "ModelId": "your-custom-trained-model-id"
  
  // Blob Storage Key is the access key for the storage account, not an individual container
  "BlobStorageKey": "your-storage-account-access-key",

  // Blob Storage Name is the name of the storage account, not an individual container
  "BlobStorageAccountName": "your-storage-account-name"
}
````
