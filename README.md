## Elanco Rebates - Table of contents

Looking for [source code examples](#source-code-examples)? No problem!

1. [Introduction](#introduction)
2. [Task](#task)
3. [System design and flow](#system-design-and-flow)
    1. [Project folder structure](#project-structure)
    2. [User interface showcase](#user-interface)
    3. [UML rebate offer database design](#uml-rebate-offer-database-design)
4. [New requirements and requirement changes](#new-requirements-and-requirement-changes)
    1. [UML account details database design](#uml-user-account-database-design)
    2. [User interface showcase](#user-interface-requirement-changes)
5. [Tools and frameworks used](#tools-and-frameworks)

Click [here](#required-for-locally-running) to view the code **required** to locally run this project.

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
* **ElancoUI.Tests:** Holds the unit testing of the ElancoUI classes.
* **ElancoLibrary:** Holds the business logic of the application. Acts as a layer between the UI and Data Access.
* **ElancoLibrary.Tests:** Holds the unit testing and mocking of the ElancoLibrary classes.
* **ElancoData:** Holds the tables, stored procedures and data publisher for the products/offers. 
* **Documentation:** Holds the example receipts, products logos, UML diagrams and user interface mockups.

---

#### UML Rebate Offer Database Design
An offer has multiple details and multiple brands. A brand can contain multiple products, products are the medications that Elanco sells.

<img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/Wireframes/DatabaseProductDesignV2.png" />

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

---

**Form Helper for formatting data into form input fields.**
<p float="left">
  <img src="https://github.com/hbux/ElancoRebatesProject/blob/main/Documentation/markdown-images/formHelper_code.png" width="50%" height="50%"/>
</p>

## Required for Locally Running
Below is json code which is not included in the Elanco project files as this contains API key's and endpoints for API services. 

**appsettings.json:** This json file should be included in the root of your ElancoUI project.
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
