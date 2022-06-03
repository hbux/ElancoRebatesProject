## Elanco Rebates - Table of contents
1. [Introduction](#introduction)
2. [Task](#task)
3. [System design and flow](#system-design-and-flow)
    1. [Project folder structure](#project-structure)
    2. [User interface showcase](#user-interface)
    3. [UML rebate offer database design](#uml-rebate-offer-database-design)
3. [New requirements and requirement changes](#new-requirements-and-requirement-changes)
    1. [UML account details database design](#uml-user-account-database-design)
    2. [User interface showcase](#user-interface-requirement-changes)
5. [Progress tracking](#progress)
6. [Tools and frameworks used](#tools-and-frameworks)
7. [Resources and documentation links](#resources)
8. [Source code examples](#source-code-examples)


## Introduction
Elanco wants to explore the use of cloud-based cognitive services to complete text analysis on receipts to improve our customer experiences.

We believe this can be achieved by accelerating our rebates process, which today requires our customers to complete multiple forms incorporating details from multiple
receipts/products. 

## Task
Using Azure Cognitive Services, build a web-based application which represents the use of image-to-text to extract and display key details from product images and receipts.
Elanco will provide sample images of test receipts/products and we’d like to see which useful data we can programmatically extract and display in a web interface to validate this approach to streamlining the customer experience.

![Alt Text](https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/markdown-images/ER_gif.gif)

## System Design and Flow
### Project Structure
* **ElancoUI:** Holds the user interface pages, user interface models and classes.
* **ElancoLibrary:** Holds the business logic of the application. Acts as a layer between the UI and Data Access.
* **ElancoLibrary.Tests:** Holds the unit testing and mocking of the ElancoLibrary classes.
* **ElancoData:** Holds the tables, stored procedures and data publisher for the products/offers. 
* **Documentation:** Holds the example receipts, products logos, UML diagrams and user interface mockups.

---

#### UML Rebate Offer Database Design
An offer has multiple details and multiple product logos.

<img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/Wireframes/DatabaseProductDesignInitial.png" />

### User Interface

**Form Page:** This is the UI for the rebate form.
<p float="left">
  <img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/markdown-images/main_desktop.png" />
</p>

---

**Select Rebate Page:** This is the UI for selecting a rebate offer.
<p float="left">
  <img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/markdown-images/rebates_desktop.png" />
</p>

## New Requirements and Requirement Changes

There have been new requirements added by the client. Essentially, the client wants every field on the rebate form
to be auto-filled. This can be achieved by adding a login/register functionality where a customer account contains 
additional details such as:
* First name and surname
* Email address
* Address
* Multiple pets

The client wants to replace the pet input field to an image of the customer's pet(s). Where the user can select an image of their pet.

---

#### UML User Account Database Design
An account can have multiple addresses and multiple pets.

<img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/Wireframes/DatabaseAccountDesign.png" />

---

#### User Interface Requirement Changes 
**Form Page Authenticated:** This is the UI for the rebate form that has been filled when logged in.
<p float="left">
  <img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/markdown-images/complete_desktop.png" />
</p>

---

**Account Page:** This is the UI for an authenticated user's account page.
<p float="left">
  <img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/markdown-images/account_desktop.png" />
</p>


## Progress
**To-do**
- [x] Create UI for rebate submitted
- [x] Use local storage to save form data
- [ ] Add functionality to analyse product image and match with a rebate
- [ ] Add logging
- [ ] Add unit testing and mocking

**Additional features**
- [ ] Add UI to all scaffolded pages
- [ ] Add UI to information card modal
- [ ] Add UI for unverified email on the account page
- [ ] Add UI for successfully/unsuccessfully updating profile
- [ ] Add SendGrid API for emails
- [ ] Add validation for account, pet and address

**Optimisation**
- [ ] Use razor components in identity
- [ ] Move sections into components

**Publishing**
- [ ] Migrate from SQLServer to Azure
- [ ] Host web application on Azure

## Tools and Frameworks
Technologies that have and will be brought into this application down the line.
* Blazor Server
* Razor Pages
* Logging
* Dependency Injection
* Unit Testing and Mocking
* Entity Framework Core
* Identity Framework Core
* SendGrid API for email sending
* Azure Form Recognizer for invoice analysis
* SOLID Principles
* SSDT & Dapper
* Git

## Resources
Below are resources and documentation links to aid with the development.

* [ASP.NET Core Blazor forms and validation](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-6.0#handle-form-submission)
* [ASP.NET Core Blazor file uploads](https://docs.microsoft.com/en-us/aspnet/core/blazor/file-uploads?view=aspnetcore-6.0&pivots=server#upload-files-to-a-server)
* [ASP.NET Core Blazor performance best practices](https://docs.microsoft.com/en-us/aspnet/core/blazor/performance?view=aspnetcore-6.0)
* [ASP.NET Core Blazor State Management](https://docs.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-6.0&pivots=server#aspnet-core-protected-browser-storage)
* [ASP.NET Core Blazor Image Streaming](https://docs.microsoft.com/en-us/aspnet/core/blazor/images?view=aspnetcore-6.0#streaming-examples)
* [Razor Pages CRUD with EF Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/crud?view=aspnetcore-6.0)
* [Migrating from SQL Server to Azure SQL](https://docs.microsoft.com/en-us/azure/dms/tutorial-sql-server-to-azure-sql)

## Source Code Examples
**SQL data access for retrieving and saving data.**
<p float="left">
  <img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/markdown-images/sql_code.png" width="50%" height="50%"/>
</p>

--- 

**Azure Form Recognizer API call.**
<p float="left">
  <img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/markdown-images/api_code.png" width="50%" height="50%"/>
</p>
