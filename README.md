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
* System displays the list of available rebates that the customer can claim a rebate for, the customer is able to select a rebate to claim.
* An option will be displayed to allow the user to take a picture of their product, which will find a matching rebate.
* System auto-auto fills the corresponding form fields with the analysed data.
* Form validation notifies the customer as they enter details

For now, each available rebate offer will be loaded in from a rebateOffers.txt file. Then once the invoice details have been loaded, the system will loop over each available rebate offer and check whether any of the invoice details match rebate offers, and return a List of Rebate offers to display to the user.

Once the system works as expected, a database will be created to host the available rebates offers instead of using a text file.