```bash
az group create --name "DddDotNet" \
                --location "southeastasia" \
                --tags "Environment=Development" "Project=DddDotNet" "Department=SD" "ResourceType=Mixed"

# Storage
az storage account create --resource-group "DddDotNet" \
                          --name "ddddotnetintegrationtest" \
                          --location "southeastasia" \
                          --tags "Environment=Development" "Project=DddDotNet" "Department=SD"

# Service Bus
az servicebus namespace create --resource-group "DddDotNet" \
                               --name DddDotNetServiceBusIntegrationTest \
                               --location "southeastasia" \
                               --sku Standard \
                               --tags "Environment=Development" "Project=DddDotNet" "Department=SD"

az servicebus queue create --resource-group "DddDotNet" \
                           --namespace-name "DddDotNetServiceBusIntegrationTest" \
                           --name integration-test \
                           --max-size 1024

az servicebus topic create --resource-group "DddDotNet" \
                           --namespace-name "DddDotNetServiceBusIntegrationTest" \
                           --name topic-integration-test \
                           --max-size 1024

az servicebus topic subscription create --resource-group "DddDotNet" \
                                        --namespace-name "DddDotNetServiceBusIntegrationTest" \
                                        --topic-name topic-integration-test \
                                        --name sub-integration-test

# Event Hub
az eventhubs namespace create --resource-group "DddDotNet" \
                              --name DddDotNetEventHubIntegrationTest \
                              --location "southeastasia" \
                              --sku Basic \
                              --tags "Environment=Development" "Project=DddDotNet" "Department=SD"

az eventhubs eventhub create --resource-group "DddDotNet" \
                             --namespace-name "DddDotNetEventHubIntegrationTest" \
                             --name integration-test \
                             --message-retention 1 \
                             --partition-count 2

# Event Grid
az eventgrid domain create --resource-group "DddDotNet" \
                           --name DddDotNetEventGridIntegrationTest \
                           --location "southeastasia" \
                           --sku Basic \
                           --tags "Environment=Development" "Project=DddDotNet" "Department=SD"
```
