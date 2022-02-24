# Elanco Rebates
Elanco wants to explore the use of cloud-based cognitive services to complete text analysis on receipts to improve our customer experiences.

We believe this can be achieved by accelerating our rebates process, which today requires our customers to complete multiple forms incorporating details from multiple
receipts/products. 

## Task
Using Azure Cognitive Services, build a web-based application which represents the use of image-to-text to extract and display key details from product images and receipts.
Elanco will provide sample images of test receipts/products and we’d like to see which useful data we can programmatically extract and display in a web interface to validate this
approach to streamlining the customer experience. 

## System Design and Flow
* Customer uploads an invoice from their device - if using mobile, a camera can be used to take a picture of the invoice.
* Azure Form Recogniser analyses the invoice and returns key value pairs of invoice details.
* An option will be displayed to allow the user to take a picture of their product, which will find a matching rebate.
* System auto-auto fills the corresponding form fields with the analysed data.
* Form validation notifies the customer as they enter details