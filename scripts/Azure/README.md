- Resource Group
```bash
az group create --name "DddDotNet" \
                --location "southeastasia" \
                --tags "Environment=Development" "Project=DddDotNet" "Department=SD" "ResourceType=Mixed"
```

- Storage
```bash
az storage account create --resource-group "DddDotNet" \
                          --name "ddddotnetintegrationtest" \
                          --location "southeastasia" \
                          --tags "Environment=Development" "Project=DddDotNet" "Department=SD"
```

- Service Bus
```bash
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
```
- Event Hub
```bash
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
```

- Event Grid
```bash
az eventgrid domain create --resource-group "DddDotNet" \
                           --name DddDotNetEventGridIntegrationTest \
                           --location "southeastasia" \
                           --sku Basic \
                           --tags "Environment=Development" "Project=DddDotNet" "Department=SD"
```

- Azure Function:
```bash

az storage account create --resource-group "DddDotNet" \
                          --name "functionappddddotnet" \
                          --location "southeastasia" \
                          --tags "Environment=Development" "Project=DddDotNet" "Department=SD"

az extension add -n application-insights

az monitor app-insights component create --resource-group "DddDotNet" \
                                         --app "functionappddddotnet" \
                                         --location "southeastasia" \
                                         --tags "Environment=Development" "Project=DddDotNet" "Department=SD"
						  
az functionapp create --resource-group DddDotNet \
                      --consumption-plan-location southeastasia \
                      --name ddddotnet \
                      --os-type Windows \
                      --runtime dotnet-isolated \
                      --storage-account functionappddddotnet \
                      --app-insights "functionappddddotnet" \
                      --tags "Environment=Development" "Project=DddDotNet" "Department=SD"

az functionapp config set --resource-group DddDotNet \
                          --name ddddotnet \
                          --net-framework-version "v8.0"
					  
az functionapp show --name ddddotnet --resource-group DddDotNet
```
