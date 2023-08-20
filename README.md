## Identity Providers
  | Identity  | Status | Health Check | Path |
  | --------- | :----: | :----------: | ---- |
  | Auth0 by Okta| ✅ | | [/Identity/Auth0](/src/DddDotNet/DddDotNet.Infrastructure/Identity/Auth0) |
  | Azure Active Directory| ✅ | | [/Identity/Azure](/src/DddDotNet/DddDotNet.Infrastructure/Identity/Azure) |
  | Azure Active Directory B2C| ✅ | | [/Identity/Azure](/src/DddDotNet/DddDotNet.Infrastructure/Identity/Azure) |
  | Google Cloud Identity Platform| ✅ | | [/Identity/GoogleCloud](/src/DddDotNet/DddDotNet.Infrastructure/Identity/GoogleCloud) |

## Storage Providers
  | Storage  | Status | Health Check | Path |
  | -------- | :----: | :----------: | ---- |
  | Amazon S3 | ✅ | ✅ | [/Storages/Amazon](/src/DddDotNet/DddDotNet.Infrastructure/Storages/Amazon) |
  | Azure Blob Storage| ✅ | ✅ | [/Storages/Azure](/src/DddDotNet/DddDotNet.Infrastructure/Storages/Azure) |
  | Azure File Share | ✅ | | [/Storages/Azure](/src/DddDotNet/DddDotNet.Infrastructure/Storages/Azure) |
  | FTP / FTPS | ✅ | ✅ | [/Storages/Ftp](/src/DddDotNet/DddDotNet.Infrastructure/Storages/Ftp) |
  | Google Cloud Storage | ✅ | ✅ | [/Storages/Google](/src/DddDotNet/DddDotNet.Infrastructure/Storages/Google) |
  | Local | ✅ | ✅ | [/Storages/Local](/src/DddDotNet/DddDotNet.Infrastructure/Storages/Local) |
  | SFTP | ✅ | ✅ | [/Storages/Sftp](/src/DddDotNet/DddDotNet.Infrastructure/Storages/Sftp) |
  | SMB | ✅ | | [/Storages/Smb](/src/DddDotNet/DddDotNet.Infrastructure/Storages/Smb) |
  | Win32 Network Share | ✅ | | [/Storages/WindowsNetworkShare](/src/DddDotNet/DddDotNet.Infrastructure/Storages/WindowsNetworkShare) |
  
## Message Broker Providers
  | Message Broker  | Status | Encryption | Health Check | Path |
  | --------------- | :----: | :--------: | :----------: | ---- |
  | Amazon Event Bridge | ✅ | | ✅ | [/MessageBrokers/AmazonEventBridge](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/AmazonEventBridge) |
  | Amazon Kinesis | ✅ | | ✅ | [/MessageBrokers/AmazonKinesis](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/AmazonKinesis) |
  | Amazon SNS | ✅ | | ✅ | [/MessageBrokers/AmazonSNS](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/AmazonSNS) |
  | Amazon SQS | ✅ | | ✅ | [/MessageBrokers/AmazonSQS](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/AmazonSQS) |
  | Apache ActiveMQ | ✅ | | ✅ | [/MessageBrokers/ApacheActiveMQ](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/ApacheActiveMQ) |
  | Azure Event Grid | ✅ | | ✅ | [/MessageBrokers/AzureEventGrid](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/AzureEventGrid) |
  | Azure Event Hub | ✅ | | ✅ | [/MessageBrokers/AzureEventHub](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/AzureEventHub) |
  | Azure Queue Storage| ✅ | | ✅ | [/MessageBrokers/AzureQueue](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/AzureQueue) |
  | Azure Service Bus | ✅ | | ✅ | [/MessageBrokers/AzureServiceBus](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/AzureServiceBus) |
  | Google Cloud Pub/Sub | ✅ | | ✅ | [/MessageBrokers/GooglePubSub](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/GooglePubSub) |
  | Kafka | ✅ | | ✅ | [/MessageBrokers/Kafka](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/Kafka) |
  | RabbitMQ | ✅ | ✅ | ✅ | [/MessageBrokers/RabbitMQ](/src/DddDotNet/DddDotNet.Infrastructure/MessageBrokers/RabbitMQ) |

## Email Providers
  | Email  | Status | Health Check | Path |
  | ------ | :----: | :----------: | ---- |
  | Amazon SES | ✅ | ✅ | [/Notification/Email/Amazon](/src/DddDotNet/DddDotNet.Infrastructure/Notification/Email/Amazon) |
  | SendGrid | ✅ | ✅ | [/Notification/Email/SendGrid](/src/DddDotNet/DddDotNet.Infrastructure/Notification/Email/SendGrid) |
  | SMTP | ✅ | ✅ | [/Notification/Email/Smtp](/src/DddDotNet/DddDotNet.Infrastructure/Notification/Email/Smtp) |

## SMS Providers
  | SMS  | Status | Health Check | Path |
  | ---- | :----: | :----------: | ---- |
  | Amazon SNS | ✅ | | [/Notification/Sms/Amazon](/src/DddDotNet/DddDotNet.Infrastructure/Notification/Sms/Amazon) |
  | Azure Communication | ✅ | | [/Notification/Sms/Azure](/src/DddDotNet/DddDotNet.Infrastructure/Notification/Sms/Azure) |
  | Twilio | ✅ | ✅ | [/Notification/Sms/Twilio](/src/DddDotNet/DddDotNet.Infrastructure/Notification/Sms/Twilio) |

## Configuration & Secrets Providers
  | Configuration & Secrets  | Status | Health Check | Path |
  | ------------------------ | :----: | :----------: | ---- |
  | SQL Server | ✅ | | [/Configuration](/src/DddDotNet/DddDotNet.Infrastructure/Configuration) |
  | HashiCorp Vault | ✅ | | [/Configuration](/src/DddDotNet/DddDotNet.Infrastructure/Configuration) |
  | Azure App Configuration | ✅ | | [/Configuration](/src/DddDotNet/DddDotNet.Infrastructure/Configuration) |
  | Azure Key Vault | ✅ | | [/Configuration](/src/DddDotNet/DddDotNet.Infrastructure/Configuration) |
  | AWS Secrets Manager | | | [/Configuration](/src/DddDotNet/DddDotNet.Infrastructure/Configuration) |
  | Google Cloud Secret Manager | ✅ | | [/Configuration](/src/DddDotNet/DddDotNet.Infrastructure/Configuration) |
