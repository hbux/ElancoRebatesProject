# Elanco Rebates
Elanco wants to explore the use of cloud-based cognitive services to complete text analysis on receipts to improve our customer experiences.

We believe this can be achieved by accelerating our rebates process, which today requires our customers to complete multiple forms incorporating details from multiple
receipts/products. 

## Task
Using Azure Cognitive Services, build a web-based application which represents the use of image-to-text to extract and display key details from product images and receipts.
Elanco will provide sample images of test receipts/products and we’d like to see which useful data we can programmatically extract and display in a web interface to validate this
approach to streamlining the customer experience. 

#### New Requirements and requirement changes
There have been new requirements added by the client. Essentially, the client wants every field on the rebate form
to be auto-filled. This can be achieved by adding a login/register functionality where a customer account contains 
additional details such as:
* First name and surname
* Email address
* Address
* Multiple pets

**Requirement changes**
The client wants to replace the pet input field to an image of the customer's pet(s). Where the user can select an image of their pet.

## System Design and Flow
* Customer uploads an invoice from their device - if using mobile, a camera can be used to take a picture of the invoice.
* Azure Form Recogniser analyses the invoice and returns key value pairs of invoice details.
* An option will be displayed to allow the user to take a picture of their product, which will find a matching rebate.
* System auto-auto fills the corresponding form fields with the analysed data.
* Form validation notifies the customer as they enter details

Following the new requiremets and requirement changes:
* Microsoft Entity Framework with authorisation will be scaffolded into the project. Additional fields within the EF Core tables will be required.
* The pages for Authorisation/authentication will be scaffolded into the project and styled with Elanco CSS.

## Tools and Frameworks
Technologies that have and will be brought into this application down the line.
* Logging
* Dependency Injection
* Unit Testing and Mocking
* Entity Framework Core
* Identity Framework Core
* SendGrid API for email sending
* Azure Form Recognizer for invoice analysis
* SOLID Principles
* SSDT
* Git

## Resources
Below are resources and documentation links to aid with the development.

* [ASP.NET Core Blazor forms and validation](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-6.0#handle-form-submission)
* [ASP.NET Core Blazor file uploads](https://docs.microsoft.com/en-us/aspnet/core/blazor/file-uploads?view=aspnetcore-6.0&pivots=server#upload-files-to-a-server)
* [ASP.NET Core Blazor performance best practices](https://docs.microsoft.com/en-us/aspnet/core/blazor/performance?view=aspnetcore-6.0)
* [ASP.NET Core Blazor State Management](https://docs.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-6.0&pivots=server#aspnet-core-protected-browser-storage)
* [ASP.NET Core Blazor Image Streaming](https://docs.microsoft.com/en-us/aspnet/core/blazor/images?view=aspnetcore-6.0#streaming-examples)
