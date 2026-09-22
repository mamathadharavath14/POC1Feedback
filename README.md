# POC1Feedback
#  "SmtpHost": "smtp.gmail.com", server
#   "SmtpPort": "587",

# I built a Customer Feedback Management System using Azure Functions. Customers submit feedback through an HTTP-triggered function. The feedback is validated and stored in Cosmos DB. A Service Bus message is generated for asynchronous processing. Negative feedback is identified and routed to a notification queue for alerts. Feedback files are stored in Blob Storage, which triggers downstream processing through Blob and Event Grid triggers. A Timer Trigger generates daily reports. Secrets are managed using Key Vault with Managed Identity, and monitoring is handled through Application Insights. Error handling includes retries, logging, and Dead Letter Queues.
